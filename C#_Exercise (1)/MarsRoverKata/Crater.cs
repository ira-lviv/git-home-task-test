using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverKata
{
    public class Crater : Obstacle
    {
        
        public Crater(Point location,bool isDestructable) :
            base(location, isDestructable)
        {
        }

        public override bool IsDestructable
        {
            get { return false; }
        }
    }
}
