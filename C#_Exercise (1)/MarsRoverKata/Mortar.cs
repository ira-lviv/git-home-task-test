using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverKata
{
    class Mortar : Projectile
    {
        private const int maxrange = 10;

        public Mortar(Mars mars) : base(mars)
        {
            base.MaxRange = maxrange;
        }

        protected override bool IsCollisionDetected(Point desired)
        {
            return false;
        }
    }


}
