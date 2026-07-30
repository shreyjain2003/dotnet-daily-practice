using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues2
{
    // Represents a training program with its associated modules.
    public class TrainingProgram
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public List<Module> Modules { get; set; }

        public TrainingProgram(
            int programId,
            string programName)
        {
            ProgramId = programId;
            ProgramName = programName;
            Modules = new List<Module>();
        }

        // Adds a module to the training program.
        public void AddModule(Module module)
        {
            Modules.Add(module);
        }

        // Calculates the total duration of all modules.
        public int GetTotalDuration()
        {
            return Modules.Sum(module => module.DurationInHours);
        }

        // Returns all modules that match the specified category.
        public List<Module> GetModulesByCategory(string category)
        {
            return Modules
                .Where(module =>
                    module.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }


    // Represents an individual module within a training program.
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Category { get; set; }
        public int DurationInHours { get; set; }

        public Module(
            int moduleId,
            string moduleName,
            string category,
            int durationInHours)
        {
            ModuleId = moduleId;
            ModuleName = moduleName;
            Category = category;
            DurationInHours = durationInHours;
        }

        // Displays the details of the module.
        public void Display()
        {
            Console.WriteLine($"Module ID       : {ModuleId}");
            Console.WriteLine($"Module Name     : {ModuleName}");
            Console.WriteLine($"Category        : {Category}");
            Console.WriteLine($"Duration        : {DurationInHours} hours");
            Console.WriteLine("--------------------------------");
        }
    }


    // Provides operations for managing multiple training programs.
    public class TrainingProgramManager
    {
        private readonly List<TrainingProgram> trainingPrograms;

        public TrainingProgramManager()
        {
            trainingPrograms = new List<TrainingProgram>();
        }

        // Adds a training program to the system.
        public void AddTrainingProgram(
            TrainingProgram trainingProgram)
        {
            trainingPrograms.Add(trainingProgram);
        }

        // Returns all training programs available in the system.
        public List<TrainingProgram> GetAllPrograms()
        {
            return trainingPrograms;
        }

        // Returns the total number of modules across all programs.
        public int GetTotalModuleCount()
        {
            return trainingPrograms.Sum(
                program => program.Modules.Count);
        }

        // Returns the total duration of all modules across all programs.
        public int GetTotalDuration()
        {
            return trainingPrograms.Sum(
                program => program.GetTotalDuration());
        }

        // Returns all modules belonging to a specific category
        // across all training programs.
        public List<Module> GetModulesByCategory(
            string category)
        {
            return trainingPrograms
                .SelectMany(program => program.Modules)
                .Where(module =>
                    module.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Displays all training programs and their modules.
        public void DisplayAllPrograms()
        {
            foreach (TrainingProgram program in trainingPrograms)
            {
                Console.WriteLine(
                    $"Program ID   : {program.ProgramId}");

                Console.WriteLine(
                    $"Program Name : {program.ProgramName}");

                Console.WriteLine(
                    $"Total Hours  : {program.GetTotalDuration()}");

                Console.WriteLine(
                    "Modules:");

                foreach (Module module in program.Modules)
                {
                    module.Display();
                }

                Console.WriteLine();
            }
        }
    }


    // Application entry point.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the training program manager.
            TrainingProgramManager manager =
                new TrainingProgramManager();


            // Create the first training program.
            TrainingProgram dotNetProgram =
                new TrainingProgram(
                    1,
                    "C# and .NET Full Stack Development");

            // Add modules to the .NET training program.
            dotNetProgram.AddModule(
                new Module(
                    101,
                    "C# Programming",
                    "Programming",
                    40));

            dotNetProgram.AddModule(
                new Module(
                    102,
                    "Object-Oriented Programming",
                    "Programming",
                    30));

            dotNetProgram.AddModule(
                new Module(
                    103,
                    "ASP.NET Core",
                    "Backend",
                    45));

            dotNetProgram.AddModule(
                new Module(
                    104,
                    "Entity Framework Core",
                    "Database",
                    25));


            // Create the second training program.
            TrainingProgram dataProgram =
                new TrainingProgram(
                    2,
                    "Data Science with Python");

            // Add modules to the Data Science program.
            dataProgram.AddModule(
                new Module(
                    201,
                    "Python Programming",
                    "Programming",
                    35));

            dataProgram.AddModule(
                new Module(
                    202,
                    "Machine Learning",
                    "Data Science",
                    50));

            dataProgram.AddModule(
                new Module(
                    203,
                    "SQL and Databases",
                    "Database",
                    30));


            // Add programs to the manager.
            manager.AddTrainingProgram(dotNetProgram);
            manager.AddTrainingProgram(dataProgram);


            // Display all training programs.
            Console.WriteLine(
                "==========================================");

            Console.WriteLine(
                "       TRAINING PROGRAM MANAGEMENT");

            Console.WriteLine(
                "==========================================");

            manager.DisplayAllPrograms();


            // Display total number of modules.
            Console.WriteLine(
                "==========================================");

            Console.WriteLine(
                $"Total Modules: {manager.GetTotalModuleCount()}");

            Console.WriteLine(
                $"Total Training Hours: {manager.GetTotalDuration()}");


            // Display modules belonging to a specific category.
            string category = "Programming";

            Console.WriteLine(
                "\n==========================================");

            Console.WriteLine(
                $"MODULES IN CATEGORY: {category}");

            Console.WriteLine(
                "==========================================");

            List<Module> programmingModules =
                manager.GetModulesByCategory(category);

            foreach (Module module in programmingModules)
            {
                module.Display();
            }
        }
    }
}