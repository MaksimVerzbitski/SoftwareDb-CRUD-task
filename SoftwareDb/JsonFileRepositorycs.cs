using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
     class JsonFileRepositorycs : ISoftwareRepository
    {
        private List<Software> data = new List<Software>();
        private string filename;

        

        public void Add(Software sw)
        {
            data.Add(sw);
        }

        public IEnumerable<Software> GetList()
        {
            return data;
        }

        public void Remove(Software sw)
        {
            data.Remove(sw);
        }

        public void RemoveAt(int number)
        {
            data.RemoveAt(number);
        }

        public void SaveChanges()
        {
            //Software.WriteJson(filename);
        }
    }
}
