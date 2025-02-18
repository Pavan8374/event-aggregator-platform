namespace Shared.OOPs
{
    //Polymorphism(Many forms) : polymorphism allows methods, founctions, or operators to behave differently  based on the object or data type.
    public abstract class Animal
    {
        public abstract string Sound();
    }

    public class Crow : Animal
    {
        public override string Sound()
        {
            return "Kav kav";
        }
    }
        
    public class Human : Animal
    {
        //Method overriding (Run time polymorphism): the method have same name, same arguments, same type, but can overridden the methods from base class. overriding can be achieved by abstarct, vitual, sealed methods
        public override string Sound()
        {
            return "ha ha";
        }

        public string Sound(string mood)
        {
            return $"ha ha {mood}";
        }

        //Method Overloading (compile time polymorphism): Method having same name but with different arguments or different return type.
        public (int, string) Sound(string mood, int count)
        {
            return (count, $"ha ha {mood}");
        }
    }

    public class Dog : Animal
    {
        public override string Sound()
        {
            return "bow bow";
        }
    }

    public class AniamalProgram
    {
        static void Main()
        {
            Human human = new Human();
            var humanSound = human.Sound(); // Output: Dog barks (Run-time polymorphism)
            Console.Write(humanSound);

            Dog dog = new Dog();
            var dogSound = dog.Sound(); // Output: Dog barks (Run-time polymorphism)
            Console.Write(dogSound);
        }
    }
}
