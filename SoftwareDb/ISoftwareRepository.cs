using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    // Интерфейс = класс, в котором есть только публичные вирутальные методы без реализации
    // Repository = паттерн(шаблон) проектирования 
    // описывающий абстрагированный доступ к хранилищу какото-то рода обьектов
    public interface ISoftwareRepository
    {
        IEnumerable<Software> GetList();
        void Add(Software sw);
        void Remove(Software sw);
        // number = index
        void RemoveAt(int number);
        void SaveChanges();
    }
}
