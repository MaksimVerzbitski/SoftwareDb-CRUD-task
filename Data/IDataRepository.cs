using System;
using System.Collections.Generic;

namespace Data
{
    // Интерфейс = класс, в котором есть только публичные вирутальные методы без реализации
    // Repository = паттерн(шаблон) проектирования 
    // описывающий абстрагированный доступ к хранилищу какото-то рода обьектов
    public interface IDataRepository<T>
    {
        void Add(T t);
        T Get(int id);
        List<T> List();
        void Change(T t);
        // number = index
        void Remove(int id);
        void Save(string filename);
        void Load(string filename);
    }
}
