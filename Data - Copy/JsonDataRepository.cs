using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class JsonDataRepository<T> : IDataRepository<Data<T>>
    {
        List<Data<T>> objects = new List<Data<T>>();
        public void Add(Data<T> t)
        {

            objects.Add(t);
        }

        public void Change(Data<T> t)
        {
            objects.RemoveAll(o => o.Id == t.Id);
            objects.Add(t);

        }

        public Data<T> Get(int id)
        {

            return objects.Find(o => o.Id == id);
        }

        public void Load(string filename)
        {
            string json;
            objects = new List<Data<T>>();

            using (StreamReader file = new StreamReader(filename))
            {
                json = file.ReadToEnd();
            }
            if (File.Exists(filename))
            {
                objects = JsonConvert.DeserializeObject<List<Data<T>>>(json);
            }
            else
            {
                File.WriteAllText(filename, "[]");
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(string filename)
        {
            string json = JsonConvert.SerializeObject(objects);
            File.WriteAllText(filename, json);
        }
    }
}
