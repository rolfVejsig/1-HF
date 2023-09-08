using System;

namespace Cool_car
{
    // Base Vehicle class
    public class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }

        public Vehicle(string make, string model)
        {
            this.Make = make;
            this.Model = model;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"This is a {Make} {Model}.");
        }
    }

    // Car class inherits from Vehicle
    public class Car : Vehicle
    {
        public int HorsePower { get; set; }
        public string Color { get; set; }

        public Car(string make, string model, int horsePower, string color)
            : base(make, model)
        {
            this.HorsePower = horsePower;
            this.Color = color;
        }

        // Override the DisplayInfo method
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"It has {HorsePower} horsepower and its color is {Color}.");
        }

        // Additional method specific to Car
        public void Honk()
        {
            Console.WriteLine("Honk! Honk!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Car object
            Car myCar = new Car("Tesla", "Model S", 670, "Red");

            // Display car information
            myCar.DisplayInfo();

            // Honk the car horn
            myCar.Honk();
        }
    }
}
