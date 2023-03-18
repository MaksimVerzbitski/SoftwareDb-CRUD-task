using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarData
{
    public class CarJsonDataRepository : JsonDataRepository<Car>
    {
        public List<Car> Get(string brand) {
            return objects.FindAll(o => ((Car)o).Brand.Equals(brand));
        }


    }
}
