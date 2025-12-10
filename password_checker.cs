using System;
using System.Text.RegularExpressions;

class PasswordChecker
{
    static void Main()
    {
        // Main loop: ask user for a password until it's strong or user decides to keep it
        bool tryAgain;

        do
        {
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            int score = CalculateScore(password);       // Calculate password strength score (0-5)
            int percentage = score * 20;               // Convert score to percentage
            string strength = GetStrength(score);     // Get textual description

            Console.WriteLine($"\nPassword Strength: {strength} ({percentage}%)");
            DisplayStrengthBar(score);                // Show visual strength bar
            ShowRecommendations(password);           // Suggest improvements if needed

            if (score < 5)
            {
                Console.Write("\nYour password is not very strong. Do you want to try again? (y/n): ");
                string answer = Console.ReadLine().Trim().ToLower();
                tryAgain = (answer == "y" || answer == "yes");
                if (!tryAgain)
                    Console.WriteLine("\nYou chose to keep your password as is.");
            }
            else
            {
                tryAgain = false;
                Console.WriteLine("\n Congrats! Your password is very strong!");
            }

        } while (tryAgain);
    }

    // Returns a score 0-5 based on password complexity
    static int CalculateScore(string password)
    {
        int score = 0;

        if (password.Length >= 8) score++;
        if (Regex.IsMatch(password, "[A-Z]")) score++;
        if (Regex.IsMatch(password, "[a-z]")) score++;
        if (Regex.IsMatch(password, "[0-9]")) score++;
        if (Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]")) score++;

        return score;
    }

    // Convert numeric score to textual strength
    static string GetStrength(int score)
    {
        return score switch
        {
            5 => "Very Strong",
            4 => "Strong",
            3 => "Medium",
            2 => "Weak",
            _ => "Very Weak",
        };
    }

    // Display visual bar representing password strength
    static void DisplayStrengthBar(int score)
    {
        Console.Write("Strength: ");
        for (int i = 0; i < 5; i++)
        {
            if (i < score) Console.Write("█"); // Filled block
            else Console.Write("░");           // Empty block
        }
        Console.WriteLine();
    }

    // Show suggestions to improve password
    static void ShowRecommendations(string password)
    {
        if (password.Length < 8) Console.WriteLine("- Use at least 8 characters");
        if (!Regex.IsMatch(password, "[A-Z]")) Console.WriteLine("- Add uppercase letters");
        if (!Regex.IsMatch(password, "[a-z]")) Console.WriteLine("- Add lowercase letters");
        if (!Regex.IsMatch(password, "[0-9]")) Console.WriteLine("- Add digits");
        if (!Regex.IsMatch(password, "[!@#$%^&*(),.?\":{}|<>]")) Console.WriteLine("- Add special characters");
    }
}
