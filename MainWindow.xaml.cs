using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;





namespace LibraryApp
{
    public partial class MainWindow : Window
    {
        private int totalQuestions;
        private int correctAnswers;
        private int points;

        // Dictionary to store call number descriptions
        private Dictionary<string, string> CallNumberDescriptions = new Dictionary<string, string>
        {
            { "000-099", "Generalities" },
            { "100-199", "Philosophy and Psychology" },
            { "200-299", "Religion" },
            { "300-399", "Social Sciences" },
            { "400-499", "Language" },
            { "500-599", "Natural Sciences and Mathematics" },
            { "600-699", "Technology (Applied Sciences)" },
            { "700-799", "Arts and Recreation" },
            { "800-899", "Literature" },
            { "900-999", "History and Geography" },
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReplacingBooks_Click(object sender, RoutedEventArgs e)
        {
            List<string> callNumbers = GenerateCallNumbers();
            DisplayCallNumbers(callNumbers);

            MessageBox.Show("Reorder the call numbers in ascending order.");
            List<string> userOrder = GetUserOrder();
            bool isCorrect = CheckOrder(callNumbers, userOrder);

            if (isCorrect)
            {
                MessageBox.Show("Correct order! You're doing great!");
                UpdatePoints(10); // Adjust points as needed
            }
            else
            {
                MessageBox.Show("Incorrect order. Keep practicing!");
            }
        }

        private List<string> GenerateCallNumbers()
        {
            Random random = new Random();
            List<string> callNumbers = Enumerable.Range(1, 10).Select(_ => random.Next(1000).ToString("000")).ToList();
            return callNumbers;
        }

        private void DisplayCallNumbers(List<string> callNumbers)
        {
            MessageBox.Show("Generated Call Numbers:\n" + string.Join("  ", callNumbers));
        }

        private List<string> GetUserOrder()
        {
            MessageBox.Show("Enter space-separated call numbers:");
            string userOrder = Console.ReadLine();
            return userOrder.Split().ToList();
        }

        private bool CheckOrder(List<string> originalOrder, List<string> userOrder)
        {
            return originalOrder.SequenceEqual(userOrder.OrderBy(x => x).ToList());
        }

        private void IdentifyingAreas_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                totalQuestions++;

                MessageBox.Show("Match the descriptions with the call numbers:");
                List<string> shuffledCallNumbers = CallNumberDescriptions.Keys.OrderBy(x => Guid.NewGuid()).ToList();
                List<string> correctMatches = shuffledCallNumbers.Take(4).ToList();

                var matchDialog = new MatchDialog(correctMatches, CallNumberDescriptions.Values.ToList());
                matchDialog.ShowDialog();

                string userAnswer = matchDialog.UserAnswer;

                if (correctMatches.Contains(userAnswer))
                {
                    MessageBox.Show("Correct! Good job!");
                    correctAnswers++;
                    UpdatePoints(5); // Adjust points as needed
                }
                else
                {
                    MessageBox.Show("Incorrect. Keep practicing!");
                }

                MessageBox.Show($"Your current score: {correctAnswers}/{totalQuestions}");

                var result = MessageBox.Show("Do you want to continue?", "Continue", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    MessageBox.Show("Exiting Identifying areas task.");
                    break;
                }
            }
        }

        private void FindingCallNumbers_Click(object sender, RoutedEventArgs e)
        {
            // Prompt user for the call number to find
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter call number:", "Finding Call Numbers");

            // Check if the entered call number exists in the dictionary
            if (CallNumberDescriptions.ContainsKey(userInput))
            {
                MessageBox.Show($"Call number {userInput} corresponds to: {CallNumberDescriptions[userInput]}");
            }
            else
            {
                MessageBox.Show($"Call number {userInput} not found.");
            }
        }

        private void UpdatePoints(int additionalPoints)
        {
            points += additionalPoints;
            MessageBox.Show($"You earned {additionalPoints} points! Total points: {points}");
        }
    }
}

