using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues8
{
    // Represents a cricket player.
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }

        public Player(
            int playerId,
            string playerName)
        {
            PlayerId = playerId;
            PlayerName = playerName;
        }

        // Displays player details.
        public void Display()
        {
            Console.WriteLine(
                $"Player ID: {PlayerId} | Player Name: {PlayerName}");
        }
    }


    // Represents a cricket team.
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        // Each team must contain at least 13 players.
        public List<Player> Players { get; set; }

        public Team(
            int teamId,
            string teamName)
        {
            TeamId = teamId;
            TeamName = teamName;
            Players = new List<Player>();
        }

        // Adds a player to the team.
        public bool AddPlayer(Player player)
        {
            // Prevent duplicate players.
            if (Players.Any(
                p => p.PlayerId == player.PlayerId))
            {
                Console.WriteLine(
                    "Error: Player already exists in the team.");

                return false;
            }

            Players.Add(player);

            return true;
        }

        // Validates minimum team size.
        public bool IsValidTeam()
        {
            return Players.Count >= 13;
        }

        // Displays all players of the team.
        public void DisplayPlayers()
        {
            Console.WriteLine(
                $"========== PLAYERS OF {TeamName.ToUpper()} ==========");

            if (Players.Count == 0)
            {
                Console.WriteLine(
                    "No players registered.");

                return;
            }

            foreach (Player player in Players)
            {
                player.Display();
            }
        }
    }


    // Represents a fixture between two teams.
    public class Fixture
    {
        public int FixtureId { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }

        public Fixture(
            int fixtureId,
            Team team1,
            Team team2,
            DateTime matchDate,
            string venue)
        {
            FixtureId = fixtureId;
            Team1 = team1;
            Team2 = team2;
            MatchDate = matchDate;
            Venue = venue;
        }

        // Displays fixture details.
        public void Display()
        {
            Console.WriteLine(
                $"Fixture ID : {FixtureId}");

            Console.WriteLine(
                $"Match      : {Team1.TeamName} vs {Team2.TeamName}");

            Console.WriteLine(
                $"Date       : {MatchDate:dd-MM-yyyy}");

            Console.WriteLine(
                $"Time       : {MatchDate:hh:mm tt}");

            Console.WriteLine(
                $"Venue      : {Venue}");

            Console.WriteLine("----------------------------------------");
        }
    }


    // Manages the cricket tournament.
    public class CricketTournament
    {
        private readonly List<Team> teams;
        private readonly List<Fixture> fixtures;

        public string TournamentName { get; set; }

        public CricketTournament(
            string tournamentName)
        {
            TournamentName = tournamentName;
            teams = new List<Team>();
            fixtures = new List<Fixture>();
        }


        // Adds a team to the tournament.
        public bool AddTeam(Team team)
        {
            // Ensure the team contains at least 13 players.
            if (!team.IsValidTeam())
            {
                Console.WriteLine(
                    $"Error: {team.TeamName} must have at least 13 players.");

                return false;
            }

            // Prevent duplicate team IDs.
            if (teams.Any(
                t => t.TeamId == team.TeamId))
            {
                Console.WriteLine(
                    "Error: Team ID already exists.");

                return false;
            }

            teams.Add(team);

            Console.WriteLine(
                $"Team '{team.TeamName}' added successfully.");

            return true;
        }


        // Adds a fixture between two participating teams.
        public bool AddFixture(
            int fixtureId,
            int team1Id,
            int team2Id,
            DateTime matchDate,
            string venue)
        {
            // Find first team.
            Team? team1 =
                teams.FirstOrDefault(
                    team => team.TeamId == team1Id);

            // Find second team.
            Team? team2 =
                teams.FirstOrDefault(
                    team => team.TeamId == team2Id);

            if (team1 == null || team2 == null)
            {
                Console.WriteLine(
                    "Error: Both teams must be registered in the tournament.");

                return false;
            }


            // A team cannot play against itself.
            if (team1Id == team2Id)
            {
                Console.WriteLine(
                    "Error: A team cannot play against itself.");

                return false;
            }


            // Prevent duplicate fixture IDs.
            if (fixtures.Any(
                fixture =>
                    fixture.FixtureId == fixtureId))
            {
                Console.WriteLine(
                    "Error: Fixture ID already exists.");

                return false;
            }


            // Create fixture.
            Fixture newFixture =
                new Fixture(
                    fixtureId,
                    team1,
                    team2,
                    matchDate,
                    venue);


            fixtures.Add(newFixture);

            Console.WriteLine(
                "Fixture added successfully.");

            return true;
        }


        // Returns the total number of participating teams.
        public int GetTotalTeams()
        {
            return teams.Count;
        }


        // Finds a team by its ID.
        public Team? GetTeamById(
            int teamId)
        {
            return teams.FirstOrDefault(
                team => team.TeamId == teamId);
        }


        // Returns all fixtures involving a particular team.
        public List<Fixture> GetTeamFixtures(
            int teamId)
        {
            return fixtures
                .Where(
                    fixture =>
                        fixture.Team1.TeamId == teamId
                        ||
                        fixture.Team2.TeamId == teamId)
                .ToList();
        }


        // Displays fixtures for a particular team.
        public void DisplayTeamFixtures(
            int teamId)
        {
            Team? team =
                GetTeamById(teamId);

            if (team == null)
            {
                Console.WriteLine(
                    "Error: Team not found.");

                return;
            }


            Console.WriteLine(
                $"========== FIXTURES FOR {team.TeamName.ToUpper()} ==========");

            List<Fixture> teamFixtures =
                GetTeamFixtures(teamId);

            if (teamFixtures.Count == 0)
            {
                Console.WriteLine(
                    "No fixtures scheduled for this team.");

                return;
            }


            foreach (Fixture fixture
                in teamFixtures)
            {
                fixture.Display();
            }
        }


        // Displays players of a particular team.
        public void DisplayTeamPlayers(
            int teamId)
        {
            Team? team =
                GetTeamById(teamId);

            if (team == null)
            {
                Console.WriteLine(
                    "Error: Team not found.");

                return;
            }

            team.DisplayPlayers();
        }


        // Displays all participating teams.
        public void DisplayAllTeams()
        {
            Console.WriteLine(
                "========== PARTICIPATING TEAMS ==========");

            foreach (Team team in teams)
            {
                Console.WriteLine(
                    $"Team ID: {team.TeamId} | " +
                    $"Team Name: {team.TeamName} | " +
                    $"Players: {team.Players.Count}");
            }
        }


        // Displays all tournament fixtures.
        public void DisplayAllFixtures()
        {
            Console.WriteLine(
                "========== TOURNAMENT FIXTURES ==========");

            if (fixtures.Count == 0)
            {
                Console.WriteLine(
                    "No fixtures scheduled.");

                return;
            }

            foreach (Fixture fixture
                in fixtures)
            {
                fixture.Display();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // ==========================================
            // CREATE TOURNAMENT
            // ==========================================

            CricketTournament tournament =
                new CricketTournament(
                    "Inter College Cricket Tournament");


            // ==========================================
            // CREATE TEAM 1
            // ==========================================

            Team team1 =
                new Team(
                    101,
                    "Delhi Warriors");


            // Add minimum 13 players.
            for (int i = 1; i <= 13; i++)
            {
                team1.AddPlayer(
                    new Player(
                        1000 + i,
                        $"Delhi Player {i}"));
            }


            // ==========================================
            // CREATE TEAM 2
            // ==========================================

            Team team2 =
                new Team(
                    102,
                    "Mumbai Strikers");


            // Add minimum 13 players.
            for (int i = 1; i <= 13; i++)
            {
                team2.AddPlayer(
                    new Player(
                        2000 + i,
                        $"Mumbai Player {i}"));
            }


            // ==========================================
            // CREATE TEAM 3
            // ==========================================

            Team team3 =
                new Team(
                    103,
                    "Chennai Kings");


            // Add minimum 13 players.
            for (int i = 1; i <= 13; i++)
            {
                team3.AddPlayer(
                    new Player(
                        3000 + i,
                        $"Chennai Player {i}"));
            }


            // ==========================================
            // REGISTER TEAMS
            // ==========================================

            tournament.AddTeam(team1);

            tournament.AddTeam(team2);

            tournament.AddTeam(team3);


            // ==========================================
            // DISPLAY TOTAL NUMBER OF TEAMS
            // ==========================================

            Console.WriteLine();

            Console.WriteLine(
                $"Total Number of Teams: " +
                $"{tournament.GetTotalTeams()}");


            // ==========================================
            // DISPLAY ALL TEAMS
            // ==========================================

            Console.WriteLine();

            tournament.DisplayAllTeams();


            // ==========================================
            // CREATE FIXTURES
            // ==========================================

            Console.WriteLine(
                "\n========== CREATING FIXTURES ==========");

            tournament.AddFixture(
                1,
                101,
                102,
                new DateTime(
                    2026,
                    8,
                    1,
                    10,
                    0,
                    0),
                "Delhi Cricket Ground");


            tournament.AddFixture(
                2,
                101,
                103,
                new DateTime(
                    2026,
                    8,
                    3,
                    14,
                    0,
                    0),
                "Delhi Cricket Ground");


            tournament.AddFixture(
                3,
                102,
                103,
                new DateTime(
                    2026,
                    8,
                    5,
                    10,
                    0,
                    0),
                "Mumbai Cricket Stadium");


            // ==========================================
            // DISPLAY ALL FIXTURES
            // ==========================================

            Console.WriteLine();

            tournament.DisplayAllFixtures();


            // ==========================================
            // DISPLAY FIXTURES OF PARTICULAR TEAM
            // ==========================================

            Console.WriteLine();

            tournament.DisplayTeamFixtures(
                101);


            // ==========================================
            // DISPLAY PLAYERS OF PARTICULAR TEAM
            // ==========================================

            Console.WriteLine();

            tournament.DisplayTeamPlayers(
                101);
        }
    }
}