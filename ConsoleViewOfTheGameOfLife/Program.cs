using System;

namespace ConsoleViewOfTheGameOfLife
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var gc = new ConwaysGameController<LifeCellConsoleView>(10);
            StartGame(gc);
        }

        private static void StartGame(ConwaysGameController<LifeCellConsoleView> gc)
        {
            gc.RandomizeBoard();
            PrintGame(gc);
            gc.RunEvolution();
            PrintGame(gc);
        }

        private static void PrintGame(ConwaysGameController<LifeCellConsoleView> gc)
        {
            var i = 0;
            foreach (var cell in gc.Cells)
            { 
                Console.Write(cell.GetSymbol());
                if (++i >= gc.Row_Length)
                {
                    Console.WriteLine();
                    i = 0;
                }
            }
            Console.WriteLine(new string('-', gc.Row_Length * 3));
        }
    }
}
