class ATM
{
    static void Main(String[] args)
    {
        int balance = 1000;
        int pin = 1234;

        string name = "Rene";

        Console.WriteLine("Welcome to Cashmoney Bank, " + name);

        while (true)
        {
            Console.WriteLine("1. Show Balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Change pin");
            Console.WriteLine("5. Help");

            int Choice = Convert.ToInt32(Console.ReadLine());

            if (Choice == 1)
            {
                Console.WriteLine("Enter pin to show balance...");

                int pinInput = Convert.ToInt32(Console.ReadLine());

                if (pinInput == pin)
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine("Your balance is: $" + balance);
                    continue;
                }
            }

            else if (Choice == 2)
            {
                Console.WriteLine("Enter pin to withdraw");

                int pinInput = Convert.ToInt32(Console.ReadLine());

                if (pinInput == pin)
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine("Your balance is: $" + balance);
                    Console.WriteLine("Enter amount to withdraw: ");

                    int amountWithdraw = Convert.ToInt32(Console.ReadLine());

                    if (amountWithdraw < balance)
                    {
                        Console.WriteLine("Your withdrawal of $" + amountWithdraw + " was successfull");
                        balance -= amountWithdraw;
                        Console.WriteLine("Your balance is: $" + balance);
                        continue;
                    }

                    else if (amountWithdraw > balance)
                    {
                        Console.WriteLine("Error, insufficient funds!");
                        continue;
                    }
                }
            }

            else if (Choice == 3)
            {
                Console.WriteLine("Enter pin to deposit");

                int pinInput = Convert.ToInt32(Console.ReadLine());

                if (pinInput == pin)
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine("Your balance is: $" + balance);
                    Console.WriteLine("Enter amount to deposit: ");

                    int amountDeposit = Convert.ToInt32(Console.ReadLine());

                    balance += amountDeposit;
                    Console.WriteLine("Your balance is: $" + balance);
                    continue;
                }
            }

            else if (Choice == 4)
            {
                Console.WriteLine("Enter pin to continue...");

                int pinInput = Convert.ToInt32(Console.ReadLine());

                if (pinInput == pin)
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine("Enter new pin: ");

                    int newPin = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Successfully changed pin to " + newPin + "!");
                    pin = newPin;
                    continue;
                }
            }

            else if (Choice == 5)
            {
                Console.WriteLine("Go ask yer mum");
                continue;
            }
        }
        
    }
}