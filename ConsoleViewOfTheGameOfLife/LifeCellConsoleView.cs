using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConwaysGameOfLife;

namespace ConsoleViewOfTheGameOfLife
{
    class LifeCellConsoleView
    {
        private LifeCell celly;

        public bool IsAlive { get => celly.IsAlive; }

        public LifeCellConsoleView(bool isAlive)
        {
            celly = new LifeCell(isAlive);
        }

        public void AddNeighbor(LifeCellConsoleView neighbor)
        {
            celly.AddNeighbor(neighbor.celly);
        }

        public void SubscribeToEvolution(ref EventHandler @event)
        {
            celly.SubscribeToEvolutionEvent(ref @event);
        }

        public void TalkToEverybody()
        {
            celly.CheckIfYouAreAlive();
        }

        public string GetSymbol()
        {
            return celly.IsAlive ? " O " : " X "; 
        }
    }
}
