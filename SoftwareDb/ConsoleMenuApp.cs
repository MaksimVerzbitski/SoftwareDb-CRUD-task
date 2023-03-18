using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    // Класс "Запись в меню приложения"
    public class MenuItem
    {
        // Связанный символ (кнопка на клавиатуре, которую необходимо нажать
        public char Symbol;
        // Описание операции в меню
        public string Caption;
        // Ссылка на функцию, которая предоставляет реализацию операции (Add() и т.д.)
        public Action Function;
        public MenuItem(char sym, string caption, Action func)
        {
            Symbol = sym;
            Caption = caption;
            Function = func;
        }
    }

    public class ConsoleMenu
    {
        public string Name { get; private set; }
        // Список записей, составляющих меню
        public List<MenuItem> Items { get; private set; } = new List<MenuItem>();
        public ConsoleMenu Parent { get; set; } = null;

        // Символ, который будет присвоен следующей добавленной операции
        private char next_symbol = '1';

        public ConsoleMenu(string name)
        {
            Name = name;
        }

        public void RegisterMenuItem(string caption, Action func)
        {
            // Регистрируем новую операцию в меню со следующим доступным символом
            Items.Add(new MenuItem(next_symbol, caption, func));

            // Если добавленный символ - '9', сразу перескакиваенм на 'a'
            if (next_symbol == '9') next_symbol = 'a';
            // В ином случае сдвигаем следующий символ вперёд
            else ++next_symbol;
        }
    }

    public class ConsoleMenuApp
    {
        protected ConsoleMenu main_menu = new ConsoleMenu("MainMenu");
        protected List<ConsoleMenu> menus = new List<ConsoleMenu>();
        protected ConsoleMenu current_menu;

        // Флажок "продолжение исполнения", изначально поднят
        protected bool running = true;
        // Флажок "инициализация приложения произведена"
        protected bool setupFinished = false;

        public ConsoleMenuApp()
        {
            menus.Add(main_menu);
            current_menu = main_menu;
        }

        public void RegisterSubmenu(ConsoleMenu menu,
            ConsoleMenu submenu, string caption)
        {
            // Если меню уже является дочерним для другого меню
            // считаем такой случай ошибочным
            // TODO: Кидать в таком случае исключение
            if (submenu.Parent != null) return;

            menu.RegisterMenuItem(caption, () => current_menu = submenu);
            submenu.Parent = menu;
        }

        // Процедура для добавления операции "Выход" в любое подменю
        public void AddExitToMenu(ConsoleMenu menu)
        {
            menu.RegisterMenuItem("Exit", () => current_menu = menu.Parent);
        }

        // Настройка всего ConsoleMenuApp
        // Включает настройку конкретного приложения (дочернего класса)
        // и добавление в меню операции "Exit"
        public void Setup()
        {
            // Если приложение уже инициализировано - сразу выходим из Setup()
            if (setupFinished) return;
            AppSetup();
            main_menu.RegisterMenuItem("Exit", AppExit);
            setupFinished = true;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnCancel);
        }

        // Виртуальный метод, где дочерний класс может
        // произвести свои действия по инициализации данных/меню и т.д.
        protected virtual void AppSetup()
        {

        }

        // Аналогично для операций при выходе
        protected virtual void AppExit()
        {
            Console.WriteLine("Exiting....");
            running = false;
        }
        private void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Interrupted");
            DisplayMenu();
            HandleOp();
        }
        // Вывод меню
        public void DisplayMenu()
        {
            Console.WriteLine("Software database");
            Console.WriteLine("=================");
            foreach (MenuItem item in current_menu.Items)
            {
                Console.WriteLine($"{item.Symbol}. {item.Caption}");
            }
        }

        public void HandleOp()
        {
            // Выбор операции
            Console.Write("Choose an operation: ");
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");

            // Ищем в меню запись для выбранного пользователем символа
            // (проходит по всем записям меню и сравнивает символ записи
            // и символ, введённый пользователем)
            MenuItem item = current_menu.Items.Find((it) => it.Symbol == c);

            // Если нашли запись в меню - вызываем связанную с ней функцию
            // (задана в классе MenuItem через Action)
            if (item != null) item.Function();
            // В ином случае выводим ошибку
            else Console.WriteLine("Unknown operation");

            Console.WriteLine();
        }

        // Функция - основной цикл приложения
        public void Run()
        {
            // Пока поднят флажок, продолжаем выводить меню и принимать
            // ввод от пользователя
            while (running)
            {
                DisplayMenu();
                HandleOp();
            }


        }
    }
}

