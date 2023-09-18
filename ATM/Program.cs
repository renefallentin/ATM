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

            int Choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter pin");

            int pinInput = Convert.ToInt32(Console.ReadLine());
            if (pinInput != pin)
            {
                Console.WriteLine("Wrong pin!");
                continue;
            }

            switch (Choice)
            {
                case 1:
                    Console.WriteLine("Your balance is: $" + balance);
                    break;

                case 2:
                    Console.WriteLine("Your balance is: $" + balance);
                    Console.WriteLine("Enter amount to withdraw: ");

                    int amountWithdraw = Convert.ToInt32(Console.ReadLine());

                    if (amountWithdraw < balance)
                    {
                        Console.WriteLine("Your withdrawal of $" + amountWithdraw + " was successfull");
                        balance -= amountWithdraw;
                        Console.WriteLine("Your balance is: $" + balance);
                    }

                    else if (amountWithdraw > balance)
                    {
                        Console.WriteLine("Error, insufficient funds!");
                    }
                    break;
                
                case 3:
                    Console.WriteLine("Your balance is: $" + balance);
                    Console.WriteLine("Enter amount to deposit: ");

                    int amountDeposit = Convert.ToInt32(Console.ReadLine());

                    balance += amountDeposit;
                    Console.WriteLine("Your balance is: $" + balance);
                    break;

                default:
                    Console.WriteLine("There was an error!");
                    break;
            }
        }
    }
}
