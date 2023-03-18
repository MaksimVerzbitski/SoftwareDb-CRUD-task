using System;
using System.Collections.Generic;

namespace CarData
{
    public class CarPresentaition : Data.DataPresentation<Car>
    {
        private List<string> headers = new List<string>(){"Id", "Brand", "Model", "Type", "Weight", "Year of Make"};
        public override void Show(List<Car> data)
        {
            DrawRow(headers, true);
            foreach (Car car in data)
                DrawRow(new List<string>() { car.Id.ToString(), car.Brand, car.Model, car.Type, car.Weight.ToString(), car.YearOfMakeString }, false);
        }
    }
}
