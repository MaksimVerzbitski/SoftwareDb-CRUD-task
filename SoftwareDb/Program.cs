using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleMenuApp app = new DatabaseApp();
                app.Setup();
                app.Run();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Fatal error: {e.Message}\n{e.StackTrace}");
                Console.ReadKey();
            }
        }
    }
}
