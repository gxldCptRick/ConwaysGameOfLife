
using ConwaysGameOfLife;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ConwaysGameOfLifeGui
{
    internal class ConwayCell : LifeCell, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChanging([CallerMemberName]string propName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        protected bool isAlive;

        public override bool IsAlive
        {
            get => isAlive;
            set
            {
                isAlive = value;
                PropertyChanging();
            }
        }

        public ConwayCell(bool isAlive = false) : base(isAlive){}
    }
}
