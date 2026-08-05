using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroSmartCardSystem
{
    public class TravelSummary
    {
        public long lastEntryStation;
        public long lastExitStation;
        public long lastEntryTime;
        public long lastExitTime;
        public double totalFarePaid;
        public int totalTrips;
        public double averageFarePerTrip;
    }

    public class Commuter
    {
        public int cardNumber;
        public string commuterName;
        public string commuterType;
        public TravelSummary travelSummary;
    }

    public class Station
    {
        public int stationId;
        public string stationName;
        public int zone;
        public double latitude;
        public double longitude;
    }

    public interface IMetroOperations
    {
        void IssueCard(int cardNumber, string commuterName, string commuterType);
        bool TapIn(int cardNumber, int stationId, long epochTime);
        bool TapOut(int cardNumber, int stationId, long epochTime);
        Commuter GetCommuterInfo(int cardNumber);
        List<double> FareHistory(int cardNumber);
        Dictionary<string, double> GetZoneWiseRevenue(long startTime, long endTime);
        List<string> GetFrequentRoute(int cardNumber);
        double GetDailyPassSavings(int cardNumber, long date);
    }

    public class Journey
    {
        public int EntryStationId;
        public long EntryTime;
        public Journey(int stationId, long entryTime)
        {
            EntryStationId = stationId;
            EntryTime = entryTime;
        }
    }

    public class JourneyRecord
    {
        public int EntryStationId;
        public int ExitStationId;
        public long EntryTime;
        public long ExitTime;
        public double Fare;
        public string Route;
        public string ZoneKey;
        public long Day;

        public JourneyRecord(
            int entryStationId,
            int exitStationId,
            long entryTime,
            long exitTime,
            double fare,
            string route,
            string zoneKey,
            long day)
        {
            EntryStationId = entryStationId;
            ExitStationId = exitStationId;
            EntryTime = entryTime;
            ExitTime = exitTime;
            Fare = fare;
            Route = route;
            ZoneKey = zoneKey;
            Day = day;
        }
    }
    public class MetroCardManager : IMetroOperations
    {
        private Dictionary<int, Station> stations;
        private Dictionary<int, Commuter> commuters;
        private Dictionary<int, Journey> activeJourneys;
        private Dictionary<int, List<double>> fareHistory;
        private Dictionary<int, Dictionary<string, int>> routeFrequency;
        private List<JourneyRecord> allJourneys;
        private Dictionary<int, Dictionary<long, double>> dailyFare;
        private Dictionary<string, double> discounts;

        private double baseFare;
        private double ratePerKm;
        private double maxDailyCapital;

        public MetroCardManager(
            List<Station> stationList,
            double baseFare,
            double ratePerKm,
            double maxDailyCapital)
        {
            this.baseFare = baseFare;
            this.ratePerKm = ratePerKm;
            this.maxDailyCapital = maxDailyCapital;

            stations = new Dictionary<int, Station>();
            foreach (Station s in stationList)
            {
                stations[s.stationId] = s;
            }

            commuters = new Dictionary<int, Commuter>();
            activeJourneys = new Dictionary<int, Journey>();
            fareHistory = new Dictionary<int, List<Double>>();
            routeFrequency = new Dictionary<int, Dictionary<string, int>>();
            dailyFare = new Dictionary<int, Dictionary<long, double>>();
            allJourneys = new List<JourneyRecord>();
            discounts = new Dictionary<string, double>()
            {
                {"ADULT",0},
                {"SENIOR",0.50},
                {"STUDENT", 0.25},
                {"CHILD",0.75}
            };
        }
        // ===========================
        // ISSUE CARD
        // ===========================

        public void IssueCard(int cardNumber, string commuterName, string commuterType)
        {
            if (!discounts.ContainsKey(commuterType))
            {
                return;
            }
            if (commuters.ContainsKey(cardNumber))
                return;

            Commuter commuter = new Commuter();

            commuter.cardNumber = cardNumber;
            commuter.commuterName = commuterName;
            commuter.commuterType = commuterType;

            commuter.travelSummary = new TravelSummary()
            {
                lastEntryStation = 0,
                lastExitStation = 0,
                lastEntryTime = 0,
                lastExitTime = 0,
                totalFarePaid = 0,
                totalTrips = 0,
                averageFarePerTrip = 0
            };

            commuters.Add(cardNumber, commuter);

            fareHistory[cardNumber] = new List<double>();

            routeFrequency[cardNumber] = new Dictionary<string, int>();

            dailyFare[cardNumber] = new Dictionary<long, double>();
        }

        // ===========================
        // TAP IN
        // ===========================

        public bool TapIn(int cardNumber, int stationId, long epochTime)
        {
            if (!commuters.ContainsKey(cardNumber))
                return false;

            if (!stations.ContainsKey(stationId))
                return false;

            if (activeJourneys.ContainsKey(cardNumber))
                return false;

            activeJourneys.Add(cardNumber,
                new Journey(stationId, epochTime));

            commuters[cardNumber].travelSummary.lastEntryStation = stationId;
            commuters[cardNumber].travelSummary.lastEntryTime = epochTime;

            return true;
        }

        // ===========================
        // TAP OUT
        // ===========================

        public bool TapOut(int cardNumber, int stationId, long epochTime)
        {
            if (!commuters.ContainsKey(cardNumber))
                return false;

            if (!activeJourneys.ContainsKey(cardNumber))
                return false;

            if (!stations.ContainsKey(stationId))
                return false;

            Journey journey = activeJourneys[cardNumber];

            if (journey.EntryStationId == stationId)
                return false;

            if (epochTime <= journey.EntryTime)
                return false;

            Station entryStation = stations[journey.EntryStationId];
            Station exitStation = stations[stationId];

            double distance = CalculateDistance(entryStation, exitStation);

            double duration =
                (epochTime - journey.EntryTime) /
                (1000.0 * 60.0);

            double fare;

            if (duration > 120)
            {
                fare = baseFare * 3;
            }
            else
            {
                fare = baseFare + (distance * ratePerKm);
            }

            string type = commuters[cardNumber].commuterType;

            if (discounts.ContainsKey(type))
            {
                fare = fare * (1 - discounts[type]);
                fare = Math.Round(fare, 2);
            }

            long day = GetDay(journey.EntryTime);

            if (!dailyFare[cardNumber].ContainsKey(day))
            {
                dailyFare[cardNumber][day] = 0;
            }

            double spentToday = dailyFare[cardNumber][day];

            if (spentToday >= maxDailyCapital)
            {
                fare = 0;
            }
            else if (spentToday + fare > maxDailyCapital)
            {
                fare = maxDailyCapital - spentToday;
            }

            fare = Math.Round(fare,2);
            dailyFare[cardNumber][day] += fare;

            commuters[cardNumber].travelSummary.lastExitStation = stationId;
            commuters[cardNumber].travelSummary.lastExitTime = epochTime;
            commuters[cardNumber].travelSummary.totalFarePaid += fare;
            commuters[cardNumber].travelSummary.totalTrips++;

            commuters[cardNumber].travelSummary.averageFarePerTrip = Math.Round(
                commuters[cardNumber].travelSummary.totalFarePaid /
                commuters[cardNumber].travelSummary.totalTrips, 2);

            fareHistory[cardNumber].Add(fare);
            if (fareHistory[cardNumber].Count > 5)
            {
                fareHistory[cardNumber].RemoveAt(0);
            }

            string route =
                entryStation.stationName + " to " + exitStation.stationName;

            if (!routeFrequency[cardNumber].ContainsKey(route))
            {
                routeFrequency[cardNumber][route] = 0;
            }

            routeFrequency[cardNumber][route]++;

            string zoneKey =
                GetZoneKey(entryStation.zone, exitStation.zone);

            allJourneys.Add(new JourneyRecord(
                journey.EntryStationId,
                stationId,
                journey.EntryTime,
                epochTime,
                fare,
                route,
                zoneKey,
                day));

            activeJourneys.Remove(cardNumber);

            return true;
        }

        // ===========================
        // COMMUTER INFO
        // ===========================

        public Commuter GetCommuterInfo(int cardNumber)
        {
            if (!commuters.ContainsKey(cardNumber))
                return null;

            return commuters[cardNumber];
        }

        // ===========================
        // FARE HISTORY
        // ===========================

        public List<double> FareHistory(int cardNumber)
        {
            if (!fareHistory.ContainsKey(cardNumber))
                return new List<double>();

            return fareHistory[cardNumber]
                    .OrderByDescending(x => x)
                    .Take(5)
                    .ToList();
        }

        // ===========================
        // ZONE WISE REVENUE
        // ===========================

        public Dictionary<string, double> GetZoneWiseRevenue(
            long startTime,
            long endTime)
        {
            Dictionary<string, double> revenue =
                new Dictionary<string, double>();

            foreach (JourneyRecord j in allJourneys)
            {
                if (j.ExitTime < startTime ||
                    j.ExitTime > endTime)
                    continue;

                if (!revenue.ContainsKey(j.ZoneKey))
                {
                    revenue[j.ZoneKey] = 0;
                }

                revenue[j.ZoneKey] += j.Fare;
            }

            return revenue
                    .Where(x => x.Value > 0)
                    .OrderByDescending(x => x.Value)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value);
        }

        // ===========================
        // FREQUENT ROUTES
        // ===========================

        public List<string> GetFrequentRoute(int cardNumber)
        {
            if (!routeFrequency.ContainsKey(cardNumber))
                return new List<string>();

            return routeFrequency[cardNumber]
                    .OrderByDescending(x => x.Value)
                    .Take(3)
                    .Select(x => x.Key)
                    .ToList();
        }

        // ===========================
        // DAILY PASS SAVINGS
        // ===========================

        public double GetDailyPassSavings(
            int cardNumber,
            long date)
        {
            if (!dailyFare.ContainsKey(cardNumber))
                return 0;

            if (!dailyFare[cardNumber].ContainsKey(date))
                return 0;

            double actualFare =
                dailyFare[cardNumber][date];

            if (actualFare <= 0)
                return 0;

            double passCost = maxDailyCapital * 0.8;

            double savings = actualFare - passCost;

            return savings > 0 ? savings : 0;
        }

        // ===========================
        // DISTANCE CALCULATION
        // ===========================

        private double CalculateDistance(Station s1, Station s2)
        {
            double lat1 = DegreesToRadians(s1.latitude);
            double lon1 = DegreesToRadians(s1.longitude);

            double lat2 = DegreesToRadians(s2.latitude);
            double lon2 = DegreesToRadians(s2.longitude);

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a =
                Math.Pow(Math.Sin(dLat / 2), 2) +
                Math.Cos(lat1) *
                Math.Cos(lat2) *
                Math.Pow(Math.Sin(dLon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            double radius = 6371;

            return radius * c;
        }

        private double DegreesToRadians(double degree)
        {
            return degree * Math.PI / 180.0;
        }

        // ===========================
        // GET DAY
        // ===========================

        private long GetDay(long epochTime)
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(epochTime);
            return long.Parse(dto.ToString("yyyyMMdd"));
        }

        // ===========================
        // ZONE KEY
        // ===========================

        private string GetZoneKey(int zone1, int zone2)
        {
            int a = Math.Min(zone1, zone2);
            int b = Math.Max(zone1, zone2);

            return $"Zone{a}-Zone{b}";
        }
    }
    // ===========================
    // PROGRAM
    // ===========================

    class Program
    {
        static void Main(string[] args)
        {
            string? firstLine = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(firstLine))
            {
                return;
            }
            string[] first = firstLine.Split();

            int numberOfRequests = int.Parse(first[0]);
            double baseFare = double.Parse(first[1]);
            double perKmRate = double.Parse(first[2]);
            double maxDailyCap = double.Parse(first[3]);

            int numberOfStations = int.Parse(Console.ReadLine());

            List<Station> stations = new List<Station>();

            for (int i = 0; i < numberOfStations; i++)
            {
                string? line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] input = line.Split();

                Station s = new Station();

                s.stationId = int.Parse(input[0]);

                s.latitude = double.Parse(input[input.Length - 2]);

                s.longitude = double.Parse(input[input.Length - 1]);

                s.zone = int.Parse(input[input.Length - 3]);

                s.stationName = string.Join(" ",
                                input.Skip(1)
                                .Take(input.Length - 4));

                stations.Add(s);
            }

            MetroCardManager manager =
                new MetroCardManager(
                    stations,
                    baseFare,
                    perKmRate,
                    maxDailyCap);

            for (int i = 0; i < numberOfRequests; i++)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                List<string> tokens = ParseCommand(line);

                string command = tokens[0];

                switch (command)
                {
                    case "issueCard":
                        {
                            int cardNumber = int.Parse(tokens[1]);
                            string commuterName = tokens[2];
                            string commuterType = tokens[3];

                            manager.IssueCard(
                                cardNumber,
                                commuterName,
                                commuterType);

                            break;
                        }

                    case "tapIn":
                        {
                            bool result =
                                manager.TapIn(
                                    int.Parse(tokens[1]),
                                    int.Parse(tokens[2]),
                                    long.Parse(tokens[3]));

                            Console.WriteLine(result.ToString().ToLower());

                            break;
                        }

                    case "tapOut":
                        {
                            bool result =
                                manager.TapOut(
                                    int.Parse(tokens[1]),
                                    int.Parse(tokens[2]),
                                    long.Parse(tokens[3]));

                            Console.WriteLine(result.ToString().ToLower());

                            break;
                        }

                    case "commuterInfo":
                        {
                            Commuter c =
                                manager.GetCommuterInfo(
                                    int.Parse(tokens[1]));

                            if (c != null)
                            {
                                Console.WriteLine(
                                    $"{c.cardNumber} " +
                                    $"{c.commuterName} " +
                                    $"{c.commuterType} " +
                                    $"{c.travelSummary.lastEntryStation} " +
                                    $"{c.travelSummary.lastExitStation} " +
                                    $"{c.travelSummary.lastEntryTime} " +
                                    $"{c.travelSummary.lastExitTime} " +
                                    $"{c.travelSummary.totalFarePaid:F2} " +
                                    $"{c.travelSummary.totalTrips} " +
                                    $"{c.travelSummary.averageFarePerTrip:F2}");
                            }

                            break;
                        }

                    case "fareHistory":
                        {
                            foreach (double fare in manager.FareHistory(
                                         int.Parse(tokens[1])))
                            {
                                Console.WriteLine(Math.Round(fare, 2));
                            }

                            break;
                        }

                    case "zoneRevenue":
                        {
                            Dictionary<string, double> revenue =
                                manager.GetZoneWiseRevenue(
                                    long.Parse(tokens[1]),
                                    long.Parse(tokens[2]));

                            foreach (var item in revenue)
                            {
                                Console.WriteLine(
                                    $"{item.Key}:{Math.Round(item.Value, 2)}");
                            }

                            break;
                        }

                    case "frequentRoute":
                        {
                            foreach (string route in
                                     manager.GetFrequentRoute(
                                         int.Parse(tokens[1])))
                            {
                                Console.WriteLine(route);
                            }

                            break;
                        }

                    case "dailySavings":
                        {
                            double savings =
                                manager.GetDailyPassSavings(
                                    int.Parse(tokens[1]),
                                    long.Parse(tokens[2]));

                            Console.WriteLine(Math.Round(savings, 2));

                            break;
                        }
                }
            }
        }

        // ===========================
        // COMMAND PARSER
        // ===========================

        static List<string> ParseCommand(string line)
        {
            List<string> result = new List<string>();

            bool insideQuotes = false;

            string current = "";

            foreach (char c in line)
            {
                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                    continue;
                }

                if (c == ' ' && !insideQuotes)
                {
                    if (current.Length > 0)
                    {
                        result.Add(current);
                        current = "";
                    }
                }
                else
                {
                    current += c;
                }
            }

            if (current.Length > 0)
            {
                result.Add(current);
            }

            return result;
        }
    }
}