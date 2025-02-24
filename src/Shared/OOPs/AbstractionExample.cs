namespace Shared.OOPs
{
    //🔹 Abstraction → Hiding implementation details and showing only necessary functionalities.
    //Think of a TV remote: You press the power button, but you don’t need to know how the circuit inside works.
    //In code, we achieve this using abstract classes and interfaces,
    //where we define methods but don’t implement them in the base class.
    public abstract class Vehicle
    {
        // Abstract methods: No implementation in the base class, must be implemented by derived classes.
        public abstract void StartEngine();

        // Non-abstract method with a default implementation.
        // Derived classes inherit this behavior but can also override it.
        public List<string> GeVehicleNames()
        {
            return new List<string> { "Honda", "Tata" };
        }

        // Virtual method: Provides a default implementation, but derived classes can override it.
        public virtual List<string> GetVirtualGeVehicleNames()
        {
            return new List<string> { "Tata" };
        }
    }

    //Car (Derived class) inherits the Vehicle (Base class)
    public class Car : Vehicle
    {
        //Implement the abstract method by overriding to expose to users.
        public override void StartEngine()
        {
            Console.WriteLine("Car engine started...");
        }

        //Override the virtual method to change behaviour
        public override List<string> GetVirtualGeVehicleNames()
        {
            return new List<string> { "Tata punch" };
        }
    }

    class Program
    {
        static void Main()
        {
            Vehicle myCar = new Car();
            myCar.StartEngine(); // Calls the abstract method implementation
        }

        //💡 Here, the user only knows that StartEngine() starts the engine but not how it's implemented.
    }
}
