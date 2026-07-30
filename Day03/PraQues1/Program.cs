using System;
using System.Collections.Generic;
using System.Linq;

namespace PracQues1
{
    // ==========================================
    // BASE CLASS
    // ==========================================

    public abstract class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Topic { get; set; }
        public string Category { get; set; }

        protected Question(
            int id,
            string text,
            string topic,
            string category)
        {
            Id = id;
            Text = text;
            Topic = topic;
            Category = category;
        }

        public virtual void Display()
        {
            Console.WriteLine($"Question ID: {Id}");
            Console.WriteLine($"Question: {Text}");
            Console.WriteLine($"Topic: {Topic}");
            Console.WriteLine($"Category: {Category}");
        }
    }


    // ==========================================
    // MULTIPLE CHOICE QUESTION
    // ==========================================

    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }

        public MultipleChoiceQuestion(
            int id,
            string text,
            string topic,
            string category,
            List<string> options,
            string correctAnswer)
            : base(id, text, topic, category)
        {
            Options = options;
            CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            base.Display();

            Console.WriteLine("Question Type: Multiple Choice");

            Console.WriteLine("Options:");

            foreach (string option in Options)
            {
                Console.WriteLine($"- {option}");
            }

            Console.WriteLine(
                $"Correct Answer: {CorrectAnswer}"
            );

            Console.WriteLine();
        }
    }


    // ==========================================
    // PARAGRAPH QUESTION
    // ==========================================

    public class ParagraphQuestion : Question
    {
        public int MaximumWords { get; set; }

        public ParagraphQuestion(
            int id,
            string text,
            string topic,
            string category,
            int maximumWords)
            : base(id, text, topic, category)
        {
            MaximumWords = maximumWords;
        }

        public override void Display()
        {
            base.Display();

            Console.WriteLine(
                "Question Type: Paragraph"
            );

            Console.WriteLine(
                $"Maximum Words: {MaximumWords}"
            );

            Console.WriteLine();
        }
    }


    // ==========================================
    // EXAM PORTAL
    // ==========================================

    public class ExamPortal
    {
        private readonly List<Question> questions;

        public ExamPortal()
        {
            questions = new List<Question>();
        }


        // Add question
        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }


        // ==========================================
        // 1. FIND TOTAL NUMBER OF QUESTIONS
        // ==========================================

        public int GetTotalQuestionCount()
        {
            return questions.Count;
        }


        // ==========================================
        // 2. LIST QUESTIONS BY TOPIC
        // ==========================================

        public List<Question> GetQuestionsByTopic(
            string topic)
        {
            return questions
                .Where(q =>
                    q.Topic.Equals(
                        topic,
                        StringComparison.OrdinalIgnoreCase
                    ))
                .ToList();
        }


        // ==========================================
        // 3. LIST QUESTIONS BY TOPIC AND CATEGORY
        // ==========================================

        public List<Question>
            GetQuestionsByTopicAndCategory(
                string topic,
                string category)
        {
            return questions
                .Where(q =>
                    q.Topic.Equals(
                        topic,
                        StringComparison.OrdinalIgnoreCase
                    )
                    &&
                    q.Category.Equals(
                        category,
                        StringComparison.OrdinalIgnoreCase
                    ))
                .ToList();
        }


        // Display all questions
        public void DisplayQuestions(
            List<Question> questionList)
        {
            if (questionList.Count == 0)
            {
                Console.WriteLine(
                    "No questions found."
                );

                return;
            }

            foreach (Question question in questionList)
            {
                question.Display();
            }
        }
    }


    // ==========================================
    // PROGRAM
    // ==========================================

    public class Program
    {
        public static void Main(string[] args)
        {
            // Create Exam Portal
            ExamPortal portal =
                new ExamPortal();


            // ==========================================
            // ADD MULTIPLE CHOICE QUESTIONS
            // ==========================================

            portal.AddQuestion(
                new MultipleChoiceQuestion(
                    1,
                    "What is a class in C#?",
                    "C#",
                    "Programming",
                    new List<string>
                    {
                        "A. Blueprint of an object",
                        "B. Database",
                        "C. Loop",
                        "D. Variable"
                    },
                    "A"
                )
            );


            portal.AddQuestion(
                new MultipleChoiceQuestion(
                    2,
                    "Which keyword is used to inherit a class?",
                    "C#",
                    "Programming",
                    new List<string>
                    {
                        "A. implements",
                        "B. extends",
                        "C. :",
                        "D. inherits"
                    },
                    "C"
                )
            );


            // ==========================================
            // ADD PARAGRAPH QUESTION
            // ==========================================

            portal.AddQuestion(
                new ParagraphQuestion(
                    3,
                    "Explain the concept of inheritance in C#.",
                    "C#",
                    "OOP",
                    300
                )
            );


            // ==========================================
            // ADD SQL QUESTION
            // ==========================================

            portal.AddQuestion(
                new MultipleChoiceQuestion(
                    4,
                    "What does SQL stand for?",
                    "SQL",
                    "Database",
                    new List<string>
                    {
                        "A. Structured Query Language",
                        "B. Simple Query Language",
                        "C. System Query Language",
                        "D. Standard Question Language"
                    },
                    "A"
                )
            );


            // ==========================================
            // 1. TOTAL NUMBER OF QUESTIONS
            // ==========================================

            Console.WriteLine(
                "================================"
            );

            Console.WriteLine(
                "TOTAL NUMBER OF QUESTIONS"
            );

            Console.WriteLine(
                "================================"
            );

            Console.WriteLine(
                portal.GetTotalQuestionCount()
            );


            // ==========================================
            // 2. QUESTIONS BY TOPIC
            // ==========================================

            Console.WriteLine(
                "\n================================"
            );

            Console.WriteLine(
                "QUESTIONS FOR TOPIC: C#"
            );

            Console.WriteLine(
                "================================"
            );

            List<Question> csharpQuestions =
                portal.GetQuestionsByTopic("C#");

            portal.DisplayQuestions(
                csharpQuestions
            );


            // ==========================================
            // 3. QUESTIONS BY TOPIC AND CATEGORY
            // ==========================================

            Console.WriteLine(
                "\n================================"
            );

            Console.WriteLine(
                "QUESTIONS FOR TOPIC: C#"
            );

            Console.WriteLine(
                "CATEGORY: Programming"
            );

            Console.WriteLine(
                "================================"
            );

            List<Question>
                programmingQuestions =
                    portal.GetQuestionsByTopicAndCategory(
                        "C#",
                        "Programming"
                    );

            portal.DisplayQuestions(
                programmingQuestions
            );
        }
    }
}