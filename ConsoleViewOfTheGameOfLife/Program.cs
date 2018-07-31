using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleViewOfTheGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            ConwaysGameController gc = new ConwaysGameController(10);
            gc.GameStart();
        }
    }
}
