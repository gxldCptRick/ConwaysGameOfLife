
using ConwaysGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLifeGui
{
    class ConwayCell : ICell
    {
        private LifeCell celly;
        Rectangle shape;
        public event EventHandler StillAliveEvent
        {
            add => celly.StillAliveEvent += value;
            remove => celly.StillAliveEvent -= value;
        }

        public bool IsAlive { get => celly.IsAlive; set => celly.IsAlive = value; }
        public Rectangle Shape { get => shape; private set => shape = value ?? shape; }

        public ConwayCell(bool isAlive)
        {
            celly = new LifeCell(isAlive);
            Shape = new Rectangle()
            {
                Width = 100,
                Height = 100
            };

            CheckTheStateOfTheCell(this, EventArgs.Empty);
        }

        private void CheckTheStateOfTheCell(object sender, EventArgs e) =>
            Shape.Fill = celly.IsAlive ? Brushes.Blue: Brushes.White; 

        public void SubscribeToEvolutionEvent(ref EventHandler handler)
        {
            celly.SubscribeToEvolutionEvent(ref handler);
            handler += CheckTheStateOfTheCell;
        }

        public void CheckIfYouAreAlive() =>
            celly.CheckIfYouAreAlive();
    }
}
