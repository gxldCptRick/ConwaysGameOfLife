using ConwaysGameOfLife;
using System;
using System.Collections.Generic;

namespace ConsoleViewOfTheGameOfLife
{
    public class ConwaysGameController<T> where T : class, ICell
    {
        private static Random rnJesus;

        private static T GenerateCell()
        {
            var isAlive = false;
            var type = typeof(T);
            var ctor = type.GetConstructor(new Type[] { typeof(bool)});
            return ctor.Invoke(new object[] { isAlive }) as T;
        }

        private T[][] CellBoard { get; set; }
        public IEnumerable<T> Cells
        {
            get
            {
                foreach (var cellRow in CellBoard)
                {
                    foreach (var cell in cellRow)
                    {
                        yield return cell;
                    }
                }
            }
        }

        public int Row_Length { get; }

        private Action<T> IntializationAction { get; }

        static ConwaysGameController()
        {
            rnJesus = new Random();
        }

        public ConwaysGameController(int gridSize, Action<T> initializationAction = null)
        {
            var type = typeof(T);
            var ctor = type.GetConstructor(new Type[] { typeof(bool) });
            if (ctor is null) throw new ArgumentException("Type Must Implement a Constructor with a boolean parameter");
            Row_Length = gridSize;
            IntializationAction = initializationAction;
            InitalizeTableOfCells(gridSize);
        }

        public void RandomizeBoard()
        {
            foreach (var cell in Cells)
            {
                cell.IsAlive = rnJesus.Next() % 2 == 0;
            }
        }

        public void ClearBoard()
        {
            foreach (var cell in Cells)
            {
                cell.IsAlive = false;
            }
        }

        public void RunEvolution()
        {
            Evolve();
        }

        private void Evolve()
        {
            foreach (var cell in Cells)
            {
                cell.AnnounceState();
            }

            foreach (var cell in Cells)
            {
                cell.DetermineIfStillLiving();
            }

        }

        private void InitalizeTableOfCells(int gridSize)
        {
            CellBoard = new T[gridSize][];
            for (var i = 0; i < CellBoard.Length; i++)
            {
                CellBoard[i] = new T[gridSize];
                for (var j = 0; j < CellBoard[i].Length; j++)
                {
                    var cell = GenerateCell();
                    CellBoard[i][j] = cell;
                    IntializationAction?.Invoke(cell);
                }
            }

            ConnectCellsWithNeighbors();
        }

        private void ConnectCellsWithNeighbors()
        {
            for (var i = 0; i < CellBoard.Length; i++)
            {
                for (var otherI = 0; otherI < CellBoard.Length; otherI++)
                {
                    AddNeighbors(i, otherI);
                }
            }
        }

        private ICell GetCellFromTable(int[] cell)
        {
            if (cell is null) throw new ArgumentNullException(nameof(cell));
            if (cell.Length != 2) throw new ArgumentException("Cell must be an integer array of length two.", nameof(cell));
            return ValidIndexInTableOfCells(cell) ? CellBoard[cell[0]][cell[1]] : null;
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
            var xValue = index[0];
            var yValue = index[1];
            return xValue >= 0 && xValue < CellBoard.Length &&
            yValue >= 0 && yValue < CellBoard[xValue].Length;
        }
    }
}
