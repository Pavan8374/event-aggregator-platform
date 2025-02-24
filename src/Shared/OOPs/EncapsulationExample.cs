namespace Shared.OOPs
{
    //Encapsulation → Hiding data and allowing controlled access using access modifiers.
    //Think of a bank account: Your balance is private, but you can access it through secure methods like GetBalance().
    //In code, we achieve this using private fields with public getter/setter methods to restrict direct access.
    public class BankAccount
    {
        private double balance; // Private data

        public void Deposit(double amount)
        {
            if (amount > 0)
                balance += amount;
        }

        public double GetBalance() // Controlled access
        {
            return balance;
        }
    }

    class User
    {
        static void Main()
        {
            BankAccount account = new BankAccount();
            account.Deposit(1000);
            Console.WriteLine("Balance: " + account.GetBalance());
        }
    }
}
