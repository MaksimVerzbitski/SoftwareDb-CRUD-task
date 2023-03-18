using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    public class MenuHolder: ConsoleMenuApp
    {
        // Список записей, составляющих меню
        protected List<MenuItem> menu = new List<MenuItem>();

        // Флажок "продолжение исполнения", изначально поднят
        protected bool running = true;

        // Символ, который будет присвоен следующей добавленной операции
        protected char next_symbol = '1';

        public virtual void Setup()
        {
            AppSetup();
            RegisterMenuItem("Exit", Exit);
        }
        protected void Exit()
        {
            Console.WriteLine("Exiting...");
            // Если выбрана операция "выход" - опускаем флажок
            running = false;
        }
        public virtual void AppSetup()
        {

        }

        public void RegisterMenuItem(string caption, Action func)
        {
            // Регистрируем новую операцию в меню со следующим доступным символом
            menu.Add(new MenuItem(next_symbol, caption, func));

            // Если добавленный символ - '9', сразу перескакиваенм на 'a'
            if (next_symbol == '9') next_symbol = 'a';
            // В ином случае сдвигаем следующий символ вперёд
            else ++next_symbol;
        }
    }
}
