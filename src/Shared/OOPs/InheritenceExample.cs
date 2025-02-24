namespace Shared.OOPs
{

    //Class: A class is like a blueprint, for ex: House is a class having props like windows, rooms
    //Object: Object is instance of a class.

    //Inheritance: OOP priciple where a class(derived/child) inherits properties and behviors from another class(parent/bas/super)
    //It enables code reusability, hierarchy, extensibility
    public class SuperAnimal
    {
        public string Sound(string sound)
        {
            return sound;
        }
    }

    //Single inheritance: A child class inherits from one parent class.
    public class BaseDog : SuperAnimal
    {
        public string Eat(string eat)
        {
            return eat;
        }
    }

    //Multi-level inheritance: A class inherits another class, which itself is derived from another class
    public class ChildDog : BaseDog
    {
        public string Sleep()
        {
            return "sleeps";
        }
    }

    public class BaseDogMain()
    {
        static void Main()
        {
            var dog = new ChildDog();
            var dogSounds = dog.Sound("bow bow"); //inherits from super animal class
            var dogEats = dog.Eat("yum bow"); //inherits from base dog class
            Console.WriteLine($"{dogSounds} {dogEats}");
        }
    }

    //Multiple inheritance:A class inherits props and behaviors fron one or more classes.
    // c# doesnot support Multiple inheritance, but can be achived by using interfaces.

    //Interface: It is same like abstract class but in interface we have only method declaration
    //Methods of inteface must be implemented by implementing class using inheritance.
    public interface IAnimal
    {
        public string Sound();
    }

    public interface IDog
    {
        public string Eat();
    }

    public class DogImplementation : IAnimal, IDog
    {
        public string Eat()
        {
            return "Eats";
        }

        public string Sound()
        {
            return "bow bow";
        }
    }
    class DogProgram
    {
        static void Main()
        {
            DogImplementation dog = new DogImplementation();
            dog.Eat();
            dog.Sound();

            FinalClass obj = new FinalClass();
            obj.Show();
        }
    }

    //A sealed class is class that is protected from not being inheritance. No class inherit sealed class.
    public sealed class FinalClass
    {
        public void Show() => Console.WriteLine("This class cannot be inherited.");
    }



    //Static Class: for a Static Class it's not possible to create an Object.
    //In a Static Class only Static members are allowed.
    // That means in a static class it's not possible to write non-static methods.    
    //Static class cannot be inherited by abother class.
    //Static class can not be initialized.
    static class StaticExample
    {
        private static int[] GetArray()
        {
            return [1, 2, 3];
        }

        public static void PrintArray()
        {
            Console.WriteLine(GetArray().ToString());
        }
    }

    //Difference between static class vs Sealed class:
    //Sealed class can initialized and for a Static Class it's not possible to create an Object.



    //Notes:
    //It is possible to create a static methods in non-static classes.
    

}
