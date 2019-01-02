using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConwaysGameOfLife;

namespace ConsoleViewOfTheGameOfLife
{
    class LifeCellConsoleView: ICell
    {
        private ICell celly;

        public event EventHandler StillAliveEvent
        {
            add
            {
                celly.StillAliveEvent += value;
            }
            remove
            {
                celly.StillAliveEvent -= value;
            }
        }

        public bool IsAlive { get => celly.IsAlive; set => celly.IsAlive = value; }

        public LifeCellConsoleView(bool isAlive)
        {
            celly = new LifeCell(isAlive);
        }

        public void AddNeighbor(ICell neighbor)
        {
            celly.AddNeighbor(neighbor);
        }


        public string GetSymbol()
        {
            return celly.IsAlive ? " O " : " X "; 
        }

        public void AnnounceState()
        {
            celly.AnnounceState();
        }

        public void DetermineIfStillLiving()
        {
            celly.DetermineIfStillLiving();
        }
    }
}
