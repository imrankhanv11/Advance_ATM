using System;
using System.Collections.Generic;

namespace Advance_ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Create obj = new Create();

            Console.WriteLine("<--------- Welcome to the Bank --------->");
            while (true)
            {
                Console.WriteLine("============ Available Operations =========");
                Console.WriteLine("1. Creating New Account");
                Console.WriteLine("2. Checking Balance");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withraw");
                Console.WriteLine("==========================================");
                Console.Write("Enter the Operation : ");

                int Maininput;
                while (true)
                {
                    try
                    {
                        Maininput = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Please enter a valid operation: ");
                    }
                }

                switch (Maininput)
                {
                    case 1:
                        obj.Create_Acc();
                        break;
                    case 2:
                        obj.check_Balance();
                        break;
                    case 3:
                        obj.Deposit();
                        break;
                    case 4:
                        obj.Withraw();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }

    class Account
    {
        public string Acc_Num { get; set; }
        public string Name { get; set; }
        protected string Pin { get; set; }
        protected double Balance { get; set; }

        public Account(string num, string name, string pin, double balance)
        {
            Acc_Num = num;
            Name = name;
            Pin = pin;
            Balance = balance;
        }

        public bool ValidatePin(string pin)
        {
            return this.Pin == pin;
        }

        public double GetBalance()
        {
            return Balance;
        }

        public void AddAmount(double amount)
        {
            Balance += amount;
        }
        public void WithrawAmount(double amount)
        {
            Balance -= amount;
        }
    }

    class Create : Account
    {
        private Dictionary<string, Account> Details = new Dictionary<string, Account>
        {
            {"ACC1000", new Account("ACC1000", "Imran", "1234", 500)},
            {"ACC1001", new Account("ACC1001", "Santhosh", "5678", 500)},
            {"ACC1002", new Account("ACC1002", "Felix", "9101", 500)},
        };

        private static int Acc_Counter = 1003;

        public Create() : base("", "", "", 0) { }

        public void Create_Acc()
        {
            string acc_num = "ACC" + Acc_Counter++;
            Console.Write("Enter the Account Holder Name: ");
            string name = Console.ReadLine();

            string pin;
            do
            {
                Console.Write("Set a 4-digit PIN: ");
                pin = Console.ReadLine();
            } while (pin.Length != 4 || !int.TryParse(pin, out _));

            double balance;
            Console.Write("Enter Initial Balance: ");
            while (!double.TryParse(Console.ReadLine(), out balance) || balance < 0)
            {
                Console.Write("Invalid amount. Enter a positive number: ");
            }

            Details.Add(acc_num, new Account(acc_num, name, pin, balance));
            Console.WriteLine($"Account successfully created!\nAccount Number: {acc_num}");
        }

        public void check_Balance()
        {
            Console.Write("Enter your Account Number: ");
            string acc = Console.ReadLine().ToUpper();

            if (Details.ContainsKey(acc))
            {
                Account user = Details[acc];
                Console.WriteLine($"Welcome {user.Name}");
                Console.Write("Enter your 4-digit PIN: ");
                string pin = Console.ReadLine();

                if (user.ValidatePin(pin))
                {
                    Console.WriteLine($"Your current balance is: {user.GetBalance()}");
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void Deposit()
        {
            Console.Write("Enter your Account Number: ");
            string acc = Console.ReadLine().ToUpper();

            if (Details.ContainsKey(acc))
            {
                Account user = Details[acc];
                Console.Write("Enter your 4-digit PIN: ");
                string pin = Console.ReadLine();

                if (user.ValidatePin(pin))
                {
                    Console.Write("Enter amount to deposit: ");
                    double amount;
                    while (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                    {
                        Console.Write("Enter a valid amount: ");
                    }

                    user.AddAmount(amount);
                    Console.WriteLine("Deposit successful.");
                    Console.WriteLine($"New Balance: {user.GetBalance()}");
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
        public void Withraw()
        {
            Console.Write("Enter your Account Number: ");
            string acc = Console.ReadLine().ToUpper();

            if (Details.ContainsKey(acc))
            {
                Account user = Details[acc];
                Console.Write("Enter your 4-digit PIN: ");
                string pin = Console.ReadLine();

                if (user.ValidatePin(pin))
                {
                    Console.Write("Enter amount to Withraw: ");
                    double amount;
                    while (!double.TryParse(Console.ReadLine(), out amount) || amount > 0)
                    {
                        Console.Write("Enter a valid amount: ");
                    }

                    user.WithrawAmount(amount);

                    Console.WriteLine($"New Balance: {user.GetBalance()}");
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }

        }
    }
}
