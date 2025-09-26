using System;

class ATM
{
    const int Pin = 1234;
    static int balance = 1000;
    static string name = "Rene";

    static void Main()
    {
        Console.WriteLine($"Welcome to Cashmoney Bank, {name}");

        while (true)
        {
            ShowMenu();

            int choice = GetIntInput("Enter your choice: ");

            if (choice == 0) // Exit option
            {
                Console.WriteLine("Thank you for banking with us. Goodbye!");
                break;
            }

            if (!VerifyPin())
            {
                Console.WriteLine("Wrong pin!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    ShowBalance();
                    break;
                case 2:
                    Withdraw();
                    break;
                case 3:
                    Deposit();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n1. Show Balance");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("0. Exit");
    }

    static bool VerifyPin()
    {
        int pinInput = GetIntInput("Enter PIN: ");
        return pinInput == Pin;
    }

    static void ShowBalance()
    {
        Console.WriteLine($"Your balance is: ${balance}");
    }

    static void Withdraw()
    {
        ShowBalance();
        int amount = GetIntInput("Enter amount to withdraw: ");

        if (amount <= 0)
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        if (amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrawal of ${amount} successful.");
            ShowBalance();
        }
        else
        {
            Console.WriteLine("Error, insufficient funds!");
        }
    }

    static void Deposit()
    {
        ShowBalance();
        int amount = GetIntInput("Enter amount to deposit: ");

        if (amount <= 0)
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        balance += amount;
        Console.WriteLine($"Deposit of ${amount} successful.");
        ShowBalance();
    }

    static int GetIntInput(string prompt)
    {
        int result;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Invalid input. Please enter a number: ");
        }
        return result;
    }
}
