using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public class LifeCell : ICell
    {
        public const int MAX_ALIVE_NEIGHBORS_FOR_SURVIVAL = 3;
        public const int MIN_ALIVE_NEIGHBORS_FOR_SURVIVAL = 2;
        public const int THE_AMOUNT_OF_NEIGHBORS_NEEDED_TO_COME_BACK_TO_LIFE = 3;

        private int amountOfNeighborsStillAlive;

        public virtual bool IsAlive { get; set; }

        public LifeCell(bool isAlive)
        {
            IsAlive = isAlive;
            amountOfNeighborsStillAlive = 0;
        }

        public event EventHandler StillAliveEvent;

        private void CheckIfStayingAlive()
        {
            IsAlive = amountOfNeighborsStillAlive >= MIN_ALIVE_NEIGHBORS_FOR_SURVIVAL && amountOfNeighborsStillAlive <= MAX_ALIVE_NEIGHBORS_FOR_SURVIVAL;
        }

        private void CheckIfYouAreEvolving()
        {
            IsAlive = amountOfNeighborsStillAlive == THE_AMOUNT_OF_NEIGHBORS_NEEDED_TO_COME_BACK_TO_LIFE;
            
        }

        private void StillAliveEventHandler(object sender, EventArgs e)
        {
            amountOfNeighborsStillAlive++;
        }

        private void CheckHowManyNeighborsSurvived()
        {
            if (IsAlive)
            {
                CheckIfStayingAlive();
            }
            else
            {
                CheckIfYouAreEvolving();
            }

            ResetAliveCounter();
        }

        public void AnnounceState()
        {
            if (IsAlive) StillAliveEvent?.Invoke(this, EventArgs.Empty);
        }

        private void ResetAliveCounter()
        {
            amountOfNeighborsStillAlive = 0;
        }
        
        public void AddNeighbor(ICell neighbor)
        {
            neighbor.StillAliveEvent += this.StillAliveEventHandler;

        }

        public void DetermineIfStillLiving()
        {
            CheckHowManyNeighborsSurvived();
        }
    }
}
