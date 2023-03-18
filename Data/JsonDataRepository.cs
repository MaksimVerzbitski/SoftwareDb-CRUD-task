using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class JsonDataRepository<T> : IDataRepository<T> where T : Data
    {
        protected List<T> objects = new List<T>();
        public void Add(T t)
        {

            objects.Add(t);
        }

        public void Change(T t)
        {
            objects.RemoveAll(o => (o.Id == t.Id));
            objects.Add(t);

        }

        public T Get(int id)
        {

            return (T)objects.Find(o => o.Id == id);
        }

        public List<T> List()
        {
            return objects;
        }

        public void Load(string filename)
        {
            // error handling
            string json;
            if (File.Exists(filename))
            {
                using (StreamReader file = new StreamReader(filename))
                {
                    json = file.ReadToEnd();
                }
                    if(json != null && json.Length > 0)
                        objects = JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void RemoveAll()
        {
            objects.Clear();
        }
        public void Save(string filename)
        {
            string json = JsonConvert.SerializeObject(objects);
            File.WriteAllText(filename, json);
        }
    }
}
