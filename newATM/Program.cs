using System;
using System.Text;
using System.Collections.Generic;

class ATM
{
    // Simuleret database af brugere
    class User
    {
        public string Username { get; set; }
        public int Pin { get; set; }
        public int Balance { get; set; }
        public List<string> Transactions { get; set; } = new List<string>();
    }

    static User[] users = new User[]
    {
        new User { Username = "rene", Pin = 1234, Balance = 1000 },
        new User { Username = "nicklas", Pin = 1234, Balance = 2000 },
        new User { Username = "jesper", Pin = 1234, Balance = 1500 }
    };

    static User currentUser;

    static void Main()
    {
        Console.Title = "Cashmoney Bank ATM";
        Console.OutputEncoding = Encoding.UTF8;

        PrintHeader("Welcome to Cashmoney Bank!");

        if (!AuthenticateUser())
        {
            Error("Too many failed attempts. Exiting...");
            return;
        }

        while (true)
        {
            ShowMenu();

            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ShowBalance();
                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Withdraw();
                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Deposit();
                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    ShowTransactions();
                    break;

                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    Transfer();
                    break;

                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    Success("Thank you for banking with us. Goodbye!");
                    return;

                default:
                    Error("Invalid selection. Try again.");
                    break;
            }
        }
    }

    // ---------- UI funktioner ----------
    static void PrintHeader(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("================================");
        Console.WriteLine($" {message}");
        Console.WriteLine("================================\n");
        Console.ResetColor();
    }

    static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
        Pause();
    }

    static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey(true);
    }

    // ---------- Core funktioner ----------
    static void ShowMenu()
    {
        PrintHeader("Cashmoney Bank ATM");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"--- Main Menu (User: {currentUser.Username}) ---");
        Console.ResetColor();

        Console.WriteLine(" [1] Show Balance");
        Console.WriteLine(" [2] Withdraw");
        Console.WriteLine(" [3] Deposit");
        Console.WriteLine(" [4] View Transactions");
        Console.WriteLine(" [5] Transfer Money");
        Console.WriteLine(" [0] Exit");

        Console.Write("\nChoose an option: ");
    }

    static bool AuthenticateUser()
    {
        const int maxAttempts = 3;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine()?.Trim().ToLower();

            int pinInput = GetMaskedPin("Enter your 4-digit PIN: ");

            foreach (var user in users)
            {
                if (user.Username == username && user.Pin == pinInput)
                {
                    currentUser = user;
                    Success($"Welcome back, {currentUser.Username}!");
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Incorrect login. Attempts left: {maxAttempts - attempt}");
            Console.ResetColor();
        }
        return false;
    }

    static void ShowBalance()
    {
        PrintHeader("Account Balance");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Your balance is: ${currentUser.Balance}");
        Console.ResetColor();
        Pause();
    }

    static void Withdraw()
    {
        PrintHeader("Withdraw Money");
        Console.WriteLine($"Current Balance: ${currentUser.Balance}");
        int amount = GetIntInput("\nEnter amount to withdraw: ");

        if (amount <= 0)
        {
            Error("Invalid amount.");
            return;
        }

        if (amount <= currentUser.Balance)
        {
            currentUser.Balance -= amount;
            string record = $"- Withdraw ${amount} | New Balance: ${currentUser.Balance}";
            currentUser.Transactions.Add(record);

            Success($"Withdrawal of ${amount} successful.");
        }
        else
        {
            Error("Error: Insufficient funds!");
        }
    }

    static void Deposit()
    {
        PrintHeader("Deposit Money");
        Console.WriteLine($"Current Balance: ${currentUser.Balance}");
        int amount = GetIntInput("\nEnter amount to deposit: ");

        if (amount <= 0)
        {
            Error("Invalid amount.");
            return;
        }

        currentUser.Balance += amount;
        string record = $"+ Deposit ${amount} | New Balance: ${currentUser.Balance}";
        currentUser.Transactions.Add(record);

        Success($"Deposit of ${amount} successful.");
    }

    static void ShowTransactions()
    {
        PrintHeader("Transaction History");

        if (currentUser.Transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet.");
        }
        else
        {
            foreach (var t in currentUser.Transactions)
            {
                Console.WriteLine(" • " + t);
            }
        }
        Pause();
    }

    static void Transfer()
    {
        PrintHeader("Transfer Money");
        Console.Write("Enter recipient username: ");
        string recipientName = Console.ReadLine()?.Trim().ToLower();

        User recipient = null;
        foreach (var user in users)
        {
            if (user.Username == recipientName && user != currentUser)
            {
                recipient = user;
                break;
            }
        }

        if (recipient == null)
        {
            Error("Recipient not found.");
            return;
        }

        int amount = GetIntInput("Enter amount to transfer: ");

        if (amount <= 0)
        {
            Error("Invalid amount.");
            return;
        }

        if (amount > currentUser.Balance)
        {
            Error("Error: Insufficient funds.");
            return;
        }

        // Overførsel af penge
        currentUser.Balance -= amount;
        recipient.Balance += amount;

        string senderRecord = $"- Transfer ${amount} to {recipient.Username} | New Balance: ${currentUser.Balance}";
        string recipientRecord = $"+ Transfer ${amount} from {currentUser.Username} | New Balance: ${recipient.Balance}";

        currentUser.Transactions.Add(senderRecord);
        recipient.Transactions.Add(recipientRecord);

        Success($"Transfer of ${amount} to {recipient.Username} successful.");
    }

    // ---------- Input funktioner ----------
    static int GetIntInput(string prompt)
    {
        int result;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid input. Please enter a number: ");
            Console.ResetColor();
        }
        return result;
    }

    static int GetMaskedPin(string prompt)
    {
        Console.Write(prompt);
        StringBuilder pinBuilder = new StringBuilder();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter && pinBuilder.Length > 0)
                break;

            if (key.Key == ConsoleKey.Backspace && pinBuilder.Length > 0)
            {
                pinBuilder.Remove(pinBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (char.IsDigit(key.KeyChar) && pinBuilder.Length < 4)
            {
                pinBuilder.Append(key.KeyChar);
                Console.Write("*");
            }
        }

        Console.WriteLine();
        return int.Parse(pinBuilder.ToString());
    }
}
