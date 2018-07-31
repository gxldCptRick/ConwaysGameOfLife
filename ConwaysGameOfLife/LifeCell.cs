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

        public bool IsAlive { get; set; }

        public LifeCell(bool isAlive)
        {
            IsAlive = isAlive;
            amountOfNeighborsStillAlive = 0;
        }

        public event EventHandler StillAliveEvent;

        private void CheckIfStayingAlive(int amountOfNeighborsWhoAreAlive)
        {

            if (amountOfNeighborsWhoAreAlive < MIN_ALIVE_NEIGHBORS_FOR_SURVIVAL
                || amountOfNeighborsWhoAreAlive > MAX_ALIVE_NEIGHBORS_FOR_SURVIVAL)
            {
                IsAlive = false;
            }
        }

        private void CheckIfYouAreEvolving(int amountOfNeighborsWhoAreAlive)
        {
            if (amountOfNeighborsWhoAreAlive == THE_AMOUNT_OF_NEIGHBORS_NEEDED_TO_COME_BACK_TO_LIFE)
            {
                IsAlive = true;
            }
        }

        private void StillAliveEventHandler(object sender, EventArgs e)
        {
            amountOfNeighborsStillAlive++;
        }

        private void CheckHowManyNeighborsSurvived()
        {
            if (IsAlive)
            {
                CheckIfStayingAlive(amountOfNeighborsStillAlive);
            }
            else
            {
                CheckIfYouAreEvolving(amountOfNeighborsStillAlive);
            }

            ResetAliveCounter();
        }

        public void CheckIfYouAreAlive()
        {
            if (IsAlive) StillAliveEvent?.Invoke(this, EventArgs.Empty);
        }

        private void ResetAliveCounter()
        {
            amountOfNeighborsStillAlive = 0;
        }

        private void DoSomeJazz(object sender, EventArgs e)
        {
            CheckHowManyNeighborsSurvived();
        }

        public void SubscribeToEvolutionEvent(ref EventHandler Evolution) =>
            Evolution += DoSomeJazz;
    }
}
