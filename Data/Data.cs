﻿using System;
using System.Collections.Generic;

namespace Data
{
    public abstract class Data : IData
    {
        private static Dictionary<string,int> ids = new Dictionary<string,int>();
        private static readonly object _lock = new object();
        private readonly int id;

        public int Id   // property
        {
            get { return id; }
        }

        public void Test()
        {

        }
        protected Data() {
            id = GenerateId();
        }

        private static int GenerateId()
        {
            string typeName = typeof(Data).FullName;
            int id = 0;
            lock (_lock)
            {
                ids.TryGetValue(typeName, out id);
                ids[typeName] = id + 1;
            }
            return id;
        }

    }


}
