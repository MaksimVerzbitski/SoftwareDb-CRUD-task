using Data;
using System;


namespace CarData
{
    public class Car : Data.Data
    {
        private string brand; // audi
        private string type; // hatchback
        private string model; // a1
        private int weight; // 1500kg
        private DateTime year_of_make;

        public Car() : base() { 
        }
        public Car(string brand, string type, string model, int weight, DateTime year_of_make) : base()
        {
            this.brand = brand;
            this.type = type;
            this.model = model;
            this.weight = weight;
            this.year_of_make = year_of_make;

        }
        public string Brand { get => brand; set => brand = value; }

        public string Type { get => type; set => type = value; }

        public string Model { get => model; set => model = value; }

        public int Weight { get => weight; set => weight = value; }

        public DateTime Year_of_make { get => year_of_make; set => year_of_make = value; }

        public string YearOfMakeString => Year_of_make.ToString("dd.MM.yyyy");


    }
}
