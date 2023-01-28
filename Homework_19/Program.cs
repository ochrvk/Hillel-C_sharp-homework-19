using System.Threading.Tasks;

class Account
{
    public int Balance { get; set; }
    public int Id { get; set; }
}


class Program
{
    static void Transfer(Account account1, Account account2)
    {
        int amount = 2000000;
        if (account1.Balance == amount)
        {
            List<Task> tasks = new List<Task>();
            object locker = new object();

            for (int j = 0; j < 2; j++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        lock (locker)
                        {
                            account1.Balance--;
                            account2.Balance++;
                        }
                    }
                }));
            }
            //Thanks for this ;)
            Task.WaitAll(tasks.ToArray());
        }
        else
        {
            Console.WriteLine("Not enough money. Transfer failed.");
            return;
        }

    }

    static void Main(string[] args)
    {
        Account account1 = new Account();
        account1.Balance = 0;
        account1.Id = 100;

        Account account2 = new Account();
        account2.Balance = 0;
        account2.Id = 200;


        Console.WriteLine("Before transfer: ");
        Console.WriteLine($"счет {account1.Id}: {account1.Balance}");
        Console.WriteLine($"счет {account2.Id}: {account2.Balance}");

        Transfer(account1, account2);

        Console.WriteLine("After transfer: ");
        Console.WriteLine($"счет {account1.Id}: {account1.Balance}");
        Console.WriteLine($"счет {account2.Id}: {account2.Balance}");

        Console.ReadKey();
    }
}
