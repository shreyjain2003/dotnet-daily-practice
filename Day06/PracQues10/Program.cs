using System;

namespace JobApplicationFilterSystem
{
    class Candidate
    {
        public int Age;
        public int Experience;
        public string Education;
        public int Certifications;

        public Candidate(int age, int experience, string education, int certifications)
        {
            Age = age;
            Experience = experience;
            Education = education;
            Certifications = certifications;
        }

        public bool IsEligible()
        {
            return Age >= 21 &&
                   Age <= 60 &&
                   Experience >= 2 &&
                   (Education.ToUpper() == "BACHELOR" ||
                    Education.ToUpper() == "MASTER" ||
                    Education.ToUpper() == "PHD");
        }

        public int CalculateScore()
        {
            int score = Experience * 10;

            switch (Education.ToUpper())
            {
                case "MASTER":
                    score += 20;
                    break;

                case "PHD":
                    score += 30;
                    break;
            }

            // Max 15 points from certifications
            score += Math.Min(Certifications * 5, 15);

            return score;
        }

        public string Recommendation(int score)
        {
            if (!IsEligible())
                return "Rejected";

            if (score >= 70)
                return "Strong Hire";
            else if (score >= 50)
                return "Hire";
            else
                return "Consider";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Job Application Filter System ===");

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Experience (Years): ");
            int experience = int.Parse(Console.ReadLine());

            Console.Write("Enter Education (Bachelor/Master/PhD): ");
            string education = Console.ReadLine();

            Console.Write("Enter Number of Certifications: ");
            int certifications = int.Parse(Console.ReadLine());

            Candidate candidate = new Candidate(age, experience, education, certifications);

            bool eligible = candidate.IsEligible();
            int score = candidate.CalculateScore();

            Console.WriteLine("\n===== RESULT =====");
            Console.WriteLine($"Eligible       : {(eligible ? "Yes" : "No")}");
            Console.WriteLine($"Total Score    : {score}");
            Console.WriteLine($"Recommendation : {candidate.Recommendation(score)}");
        }
    }
}