using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife
{
    public interface ICell
    {
        bool IsAlive { get; set; }
        event EventHandler StillAliveEvent;
        void AnnounceState();
        void DetermineIfStillLiving();
        void AddNeighbor(ICell Evolution);
    }
}
