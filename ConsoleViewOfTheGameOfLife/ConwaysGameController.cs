using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleViewOfTheGameOfLife
{
    internal class ConwaysGameController
    {
        private static Random rnJesus;
        private static LifeCellConsoleView GenerateCell()
        {
            bool isAlive = rnJesus.Next() % 3 == 0;
            return new LifeCellConsoleView(isAlive);
        }

        static ConwaysGameController()
        {
            rnJesus = new Random();
        }

        private LifeCellConsoleView[][] TableOfCells { get; set; }
        private event EventHandler DGenerationX;
        private readonly int Row_Length;

        public ConwaysGameController(int gridSize)
        {
            Row_Length = gridSize;
            InitalizeTableOfCells(gridSize);
        }

        public void GameStart()
        {
            Display();

            Console.WriteLine(new string('-', this.Row_Length * 3));

            Evolve();

            Display();
        }

        private void Evolve()
        {
            foreach (var rowOfCells in TableOfCells)
            {
                foreach (var cell in rowOfCells)
                {
                    cell.TalkToEverybody();
                }
            }

            this.DGenerationX?.Invoke(this, EventArgs.Empty);
        }

        private void Display()
        {
            for (int i = 0; i < TableOfCells.Length; i++)
            {
                for (int otherI = 0; otherI < TableOfCells[i].Length; otherI++)
                {
                    Console.Write(TableOfCells[otherI][i].GetSymbol());
                }
                Console.WriteLine();
            }
        }

        private void InitalizeTableOfCells(int gridSize)
        {
            
            TableOfCells = new LifeCellConsoleView[gridSize][];
            for (int i = 0; i < TableOfCells.Length; i++)
            {
                TableOfCells[i] = new LifeCellConsoleView[gridSize];
            }
            FillTableWithCellsAndHookUpToEvolutionEvent();
            ConnectCellsWithNeighbors();
        }

        private void ConnectCellsWithNeighbors()
        {
            for (int i = 0; i < TableOfCells.Length; i++)
            {
                for (int otherI = 0; otherI < TableOfCells.Length; otherI++)
                {
                    AddNeighbors(i, otherI);
                }
            }
        }

        private LifeCellConsoleView GetCellFromTable(int[] cell)
        {
            return TableOfCells[cell[0]][cell[1]];
        }

        private void AddNeighbors(params int[] currentCell)
        {
            var cellViewForCurrentCell = GetCellFromTable(currentCell);
            int[] leftNeighbor = { currentCell[0] - 1, currentCell[1] },
                        rightNeighbor = { currentCell[0] + 1, currentCell[1] },
                        bottomNeighbor = { currentCell[0], currentCell[1] + 1 },
                        topNeighbor = { currentCell[0], currentCell[1] - 1 },
                        bottomLeftNeighbor = { bottomNeighbor[0] - 1, bottomNeighbor[1] },
                        bottomRightNeighbor = { bottomNeighbor[0] + 1, bottomNeighbor[1] },
                        topLeftNeighbor = { topNeighbor[0] - 1, topNeighbor[1] },
                        topRightNeighbor = { topNeighbor[0] + 1, topNeighbor[1] };

            //if (ValidIndexInTableOfCells(topRightNeighbor))
            //{
                
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topRightNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(rightNeighbor));

            //    if (ValidIndexInTableOfCells(bottomLeftNeighbor))
            //    {
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomLeftNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomRightNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topLeftNeighbor));
            //    }
            //    else if (ValidIndexInTableOfCells(leftNeighbor))
            //    {
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topLeftNeighbor));
            //    }
            //}
            //else if (ValidIndexInTableOfCells(topLeftNeighbor))
            //{
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topLeftNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topNeighbor));

            //    if (ValidIndexInTableOfCells(bottomNeighbor))
            //    {
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomLeftNeighbor));
            //    }
            //}
            //else if (ValidIndexInTableOfCells(bottomRightNeighbor))
            //{
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomRightNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(rightNeighbor));

            //    if (ValidIndexInTableOfCells(leftNeighbor))
            //    {
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor));
            //        cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomLeftNeighbor));
            //    }
            //}
            //else
            //{
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomLeftNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomNeighbor));
            //    cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor));
            //}

            if (ValidIndexInTableOfCells(rightNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(rightNeighbor));
            if (ValidIndexInTableOfCells(leftNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(leftNeighbor)); 
            if (ValidIndexInTableOfCells(topNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topNeighbor));
            if (ValidIndexInTableOfCells(bottomNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomNeighbor));
            if (ValidIndexInTableOfCells(bottomRightNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomRightNeighbor));
            if (ValidIndexInTableOfCells(bottomLeftNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(bottomLeftNeighbor));
            if (ValidIndexInTableOfCells(topRightNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topRightNeighbor));
            if (ValidIndexInTableOfCells(topLeftNeighbor)) cellViewForCurrentCell.AddNeighbor(GetCellFromTable(topLeftNeighbor));
        }

        private bool ValidIndexInTableOfCells(int[] index)
        {
            int xValue = index[0];
            int yValue = index[1];

            return xValue >= 0 && xValue < TableOfCells.Length &&
            yValue >= 0 && yValue < TableOfCells[xValue].Length;

        }

        private void FillTableWithCellsAndHookUpToEvolutionEvent()
        {
            for (int i = 0; i < TableOfCells.Length; i++)
            {
                for (int otherI = 0; otherI < TableOfCells[i].Length; otherI++)
                {

                TableOfCells[i][otherI] = GenerateCell();
                TableOfCells[i][otherI].SubscribeToEvolution(ref DGenerationX);
                }
            }
        }
    }
}
