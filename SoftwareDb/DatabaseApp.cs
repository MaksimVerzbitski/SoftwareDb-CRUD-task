using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CarData;
using Data;


namespace SoftwareDb
{


    public class DatabaseApp : ConsoleMenuApp
    {
        // Список с рабочими данными
        List<Software> data = new List<Software>();
        List<Car> cars = new List<Car>();
        //IEnumerable data;

        string filename = "data.json";

        string carData = "cars.json";
        CarJsonDataRepository carRepo = new CarJsonDataRepository();
        CarPresentaition carPresentation = new CarPresentaition();


        // Здесь будем производить регистрацию операций
        protected override void AppSetup()
        {

            carRepo.Load(carData);
            data = Software.ReadJson(filename);
            
            //cars = Car.ReadingJson(filenm);
            ConsoleMenu sw = new ConsoleMenu("Software");
            ConsoleMenu crs = new ConsoleMenu("Cars");
            menus.Add(sw);
            menus.Add(crs);

            sw.RegisterMenuItem("List software", SwList);
            sw.RegisterMenuItem("Add software", Add);
            sw.RegisterMenuItem("Change software", Change);
            sw.RegisterMenuItem("Delete software", Delete);
            sw.RegisterMenuItem("Adding Test Software", AddTestSoftware);
            sw.RegisterMenuItem("Delete All", DeleteAll);

            crs.RegisterMenuItem("List Cars", CarList);
            crs.RegisterMenuItem("Add test Car's data", AddTestCar);
            crs.RegisterMenuItem("Change Car's field", ChangeCar);
            crs.RegisterMenuItem("Add Car", AddCar);
            crs.RegisterMenuItem("Search Index", Search);
            crs.RegisterMenuItem("Delete Car",DeleteCar);


            AddExitToMenu(sw);
            AddExitToMenu(crs);

            RegisterSubmenu(main_menu, sw, "Software management");
            RegisterSubmenu(main_menu, crs, "Cars management");

            //data = Software.ReadCsv("software.csv");
        }

        protected override void AppExit()
        {
            /*//File.Copy
            if (File.Exists("software.csv"))
            {
                if (File.Exists("software.csv.bak"))
                {
                    File.Delete("software.csv.bak");
                }
                File.Copy("software.csv", "software.csv.bak");
            }

            Software.SaveCsv("software.csv", data);*/
            if (Utility.CheckUser("Wish to save your changes?.."))
            {
                Software.WriteJson(filename, data);
                carRepo.Save(carData);
            }
            running = false;
        }
        void WriteFile(string name, string data)
        {
            File.WriteAllText(name, data);
        }
        

        void SwList()
        {
            Console.WriteLine("Listing software...");

            // ============================================================================
            // | Name       | Version     | Developer       | Install Date   | User       |
            // ============================================================================
            // | Office     | 2019        | Microsoft       | 01.08.2020     | Opilane    |
            // ============================================================================

            //int witdh = Console.WindowWidth;
            StringBuilder sb = new StringBuilder(80);
            sb.Append('=', 80);

            string border = sb.ToString();

          

            string header = FormatTableEntry("Name", "Version","Developer", "Install Date", "User");

            Console.WriteLine(border);
            Console.WriteLine(header);
            Console.WriteLine(border);
            // 

            Console.WriteLine(border);
            foreach (Software sw in data)
            {
                Console.WriteLine(FormatTableEntry(sw.Name, sw.Version, sw.Developer, sw.InstallDateString, sw.User));
                //Console.WriteLine($"{sw.Name}\t{sw.Version}\t{sw.Developer}\t" +
                   // $"{sw.InstallDate}\t{sw.User}");
                Console.WriteLine(border);
            }
        }
        void CarList()
        {
            List<CarData.Car> cars = carRepo.List();
            carPresentation.Show(cars);
        }

        string FormatTableEntry(string name,string version, string developer, string installdate, string user)
        {
            StringBuilder sb = new StringBuilder(80);

            
            sb.Append("| ");
            sb.Append($"{Utility.TruncateString(developer,15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(name,15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(version,10),-10}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(installdate,12),-12}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(user,12), -12}");
            sb.Append(" |");

            return sb.ToString();
        }
        string FormatTableEntry1(string brand, string model, string type, int  weight, string date)
        {
            StringBuilder sb = new StringBuilder(80);


            sb.Append("| ");
            sb.Append($"{Utility.TruncateString(brand, 15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(model, 15),-15}");
            sb.Append(" | ");
            sb.Append($"{Utility.TruncateString(type, 10),-10}");
            sb.Append(" | ");
            sb.Append($"{(weight),-12}");
            sb.Append(" | ");
            sb.Append($"{(date),-12}");
            sb.Append(" |");

            return sb.ToString();
        }

        void Add()
        {
            // Задание

            Console.Write("Enter Software name: ");
            string name = Console.ReadLine();
            // check if not empty

            if (name.Length == 0)
                throw new ApplicationException("Empty Name");


            Console.Write("Enter the Developer name: ");
            string dev = Console.ReadLine();
            // check if not empty

            if (dev.Length == 0)
                throw new ApplicationException("Empty Developer name");

            Console.Write("Enter Software version: ");
            string version = Console.ReadLine();
            // check if not empty

            if (version.Length == 0)
                throw new ApplicationException("Empty Version");

            Console.Write("Enter Software date: ");
            DateTime install_date = Utility.ParseDate(Console.ReadLine());

            Console.Write("Enter Software  Username: ");
            string user = Console.ReadLine();

            Software sw = new Software()
            {
                Name = name,
                Developer = dev,
                Version = version,
                InstallDate = install_date,
                User = user



            };
            data.Add(sw);


            Console.WriteLine($"Added Software \"{sw.Name}\"");
        }

        void Change()
        {
            //List<Car> cars = new List<Car>();
            Console.WriteLine("Changing our software's field");
            foreach (Car crs in cars)
            {
                Console.WriteLine($"{crs.Brand}\t{crs.Model}\t{crs.Type}\t{crs.Weight}\t{crs.Year_of_make}");
            }
            Console.WriteLine("Cars List's number");
            // int number = -1;
            int number = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid Number");
                }
                else break;
            }
            Console.Write("Choose field's name(brand, model, type, weight, year_of_make):");
            string pole = Console.ReadLine();
            switch (pole)
            {
                case "brand":
                    Console.WriteLine("Enter Cars's Brand:");
                    cars[number].Brand = Console.ReadLine();
                    break;

                case "model":
                    Console.WriteLine("Enter Car's model:");
                    cars[number].Model = Console.ReadLine();
                    break;

                case "type":
                    Console.WriteLine("Enter Car's type");
                    cars[number].Type = Console.ReadLine();
                    break;


                case "weight":
                    Console.WriteLine("Enter Software's user");
                    cars[number].Weight = int.Parse(Console.ReadLine());
                    break;

                case "year_of_make":
                    Console.WriteLine("Enter Car's date");
                    cars[number].Year_of_make = DateTime.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Enter valid field's name(brand,model,type,weight,year_of_make)");
                    break;
            }
                    /*int index = -1;
                    while (true)
                    {
                        Console.Write("Enter the index of item to change: ");
                        if (!int.TryParse(Console.ReadLine(), out index))
                        {
                            Console.WriteLine("Error");
                        }
                        else break;
                        *//*try
                        {
                            index = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                        }*//*
                    }
                    do { Console.WriteLine("Enter an index of item to delete"); }
                    while (!int.TryParse(Console.ReadLine(), out index));

                    Software sw = data[index];
                    sw.Name = Console.ReadLine();
                    sw.Version = "1.0.0f";

                    bool valid = false;
                    int value = 0;
                    while (!valid)
                    {
                        try
                        {
                            value = int.Parse(Console.ReadLine());
                            valid = true;
                        }
                        catch (Exception e)
                        {

                        }

                    }

                    Console.WriteLine("Changing software...");*/
            }
        void ChangeCar()
        {
            Console.WriteLine("Select Car ID you want to change");
            int number = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid Number");
                }
                else break;
            }
            // check if number is valid index
            CarData.Car carToChange = (CarData.Car)carRepo.Get(number);
            Console.Write("Choose field's name(brand, model, type, weight, year_of_make):");
            string pole = Console.ReadLine();
            switch (pole)
            {
                case "brand":
                    Console.WriteLine("Enter Cars's Brand:");
                    carToChange.Brand = Console.ReadLine();
                    break;

                case "model":
                    Console.WriteLine("Enter Car's model:");
                    carToChange.Model = Console.ReadLine();
                    break;

                case "type":
                    Console.WriteLine("Enter Car's type");
                    carToChange.Type = Console.ReadLine();
                    break;


                case "weight":
                    Console.WriteLine("Enter Software's user");
                    carToChange.Weight = int.Parse(Console.ReadLine());
                    break;

                case "year_of_make":
                    Console.WriteLine("Enter Car's date");
                    carToChange.Year_of_make = DateTime.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("Enter valid field's name(brand,model,type,weight,year_of_make)");
                    break;
            }

        }

        public static int validateInt(string input) {
            return Int32.Parse(input);
          }
        void AddCar()

        {
/*            int weight = 0;
            if (!Utility.ValidateInput<int>("Enter Car's weight:  ", "Incorrect value supplied", validateInt, out weight))
                return;*/

            Console.Write("Enter Car's brand: ");
                string brand = Console.ReadLine();
                // check if not empty

                if (brand.Length == 0)
                    throw new ApplicationException("Empty brand:");


                Console.Write("Enter the Car's model: ");
                string model = Console.ReadLine();
                // check if not empty

                if (model.Length == 0)
                    throw new ApplicationException("Empty model:");

                Console.Write("Enter Car's type: ");
                string type = Console.ReadLine();
                // check if not empty

                if (type.Length == 0)
                    throw new ApplicationException("Empty Version");

                Console.Write("Enter Car's weight: ");
            //DateTime install_date = Utility.ParseDate(Console.ReadLine());
            int weight = Int32.Parse(Console.ReadLine());
            if (weight < 0)
            {
                throw new ApplicationException("Weight is negative or 0!\nInput valid data");
            }

            Console.Write("Enter Car's date: ");

                DateTime year_of_make = Utility.ParseDate(Console.ReadLine());
                
                CarData.Car crs = new CarData.Car()
                {
                    Brand = brand,
                    Model = model,
                    Type = type,
                    Weight = weight,
                    Year_of_make = year_of_make,
                };
                carRepo.Add(crs);


                Console.WriteLine($"Added Car \"{crs.Brand}\"");
            }
        
        public 
        void Search()
        {
            Console.WriteLine("Which element you would like to find-->:");
            string criteria = Console.ReadLine();
            int id = 0;
            bool isInt = Int32.TryParse(criteria, out id);
            List<CarData.Car> result = new List<CarData.Car>();
            if (isInt)
            {
                CarData.Car c = carRepo.Get(id);
                if (c != null)
                {
                    
                    result.Add(c);
                }
            }
            else {
                result.AddRange(carRepo.Get(criteria));
            }
            //Console.WriteLine($"{cars[element].Brand}\t{cars[element].Model}\t{cars[element].Type}\t{cars[element].Weight}\t{cars[element].Year_of_make}");
            //Console.WriteLine($"{cars[element].Brand}");
            carPresentation.Show(result);

        }

        // Метод который удаляет по номеру списка всех программных обеспечений
        void Delete()
        { 
            Console.Write("Tell me position, you wish to delete: ");
            // Переменная которая отвечает за ввод пользователя
            string del = Console.ReadLine();
            // Переменную перевод в целочисленную переменную "1" => 1
            if (int.TryParse(del, out int number))
            {
                if (number >= 1)
                {
                    if (data.Count > number - 1 && Utility.CheckUser($"Delete Software data at positon {number}"))
                    {
                        data.RemoveAt(number);
                        Console.WriteLine("Deleting software...");
                    }
                    else
                    {
                        Console.WriteLine("Number you have entered is bigger than data count");
                    }
                }
                else
                {
                    Console.WriteLine("Number is negative or zero");
                }

            }
            else
            {
                Console.WriteLine("String is not an integer");
            }
        }
        void AddTestSoftware()
        {
            data.Add(new Software()
            {
                Name = "SoloLearn",
                Developer = "2020B",
                Version = "HAToday",
                InstallDate = new DateTime(2010, 12, 31),
                User = "All Users"

            });
            
        }

        void DeleteAll()
        {
            // проверка хотите ли вы удалить весь список прогрммного обеспечения
            if (Utility.CheckUser("Delete all Software's data"))
            {
                data.Clear();

            }
            else
            {
                Console.WriteLine("Think about it twice. Action canceled...");
            }

        }
        void AddTestCar()
        {
            //List<Car> cars = new List<Car>();
            if (Utility.CheckUser("Add test car?"))
            {
                CarData.Car c = new CarData.Car()
                {
                    Brand = "Audi",
                    Type = "Hatchback",
                    Model = "A1",
                    Weight = 1500,
                    Year_of_make = new DateTime(2020, 01, 12)

                };
                CarData.Car c2 = new CarData.Car()
                {
                    Brand = "Audi",
                    Type = "Hatchback",
                    Model = "A2",
                    Weight = 1500,
                    Year_of_make = new DateTime(2020, 01, 12)

                };
                CarData.Car c3 = new CarData.Car()
                {
                    Brand = "Ford",
                    Type = "Hatchback",
                    Model = "A2",
                    Weight = 1500,
                    Year_of_make = new DateTime(2020, 01, 12)

                };
                CarData.Car c4 = new CarData.Car()
                {
                    Brand = "BMW",
                    Type = "Hatchback",
                    Model = "A2",
                    Weight = 1500,
                    Year_of_make = new DateTime(2020, 01, 12)

                };
                carRepo.Add(c);
                carRepo.Add(c2);
                carRepo.Add(c3);
                carRepo.Add(c4);

            }
            else
            {
                Console.WriteLine("Think about it twice. Action canceled...");
            }
        }
        // еще не сделалм сохранение данных
        void DeleteCar()
        {
            Console.Write("Tell me position, you wish to delete: ");
            // Переменная которая отвечает за ввод пользователя
            string del = Console.ReadLine();
            // Переменную перевод в целочисленную переменную "1" => 1
            if (int.TryParse(del, out int number))
            {
                if (number >= 1)
                {
                    if (cars.Count > number - 1 && Utility.CheckUser($"Delete Software data at positon {number}"))
                    {
                        cars.RemoveAt(number);
                        Console.WriteLine("Deleting software...");
                    }
                    else
                    {
                        Console.WriteLine("Number you have entered is bigger than data count");
                    }
                }
                else
                {
                    Console.WriteLine("Number is negative or zero");
                }

            }
            else
            {
                Console.WriteLine("String is not an integer");
            }
        }
        void SaveChages() {
        
        }
    }
}
