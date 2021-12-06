using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverKata
{
    class Missile : Projectile
    {
        private const int maxrange = 5;

        public Missile(Mars mars) : base(mars)
        {
            base.MaxRange = maxrange;
        }

        protected override bool IsCollisionDetected(Point desired)
        {
            Point newDestination = desired;
            newDestination = CalculatePositionY(desired, newDestination);
            newDestination = CalculatePositionX(desired, newDestination);
            var obstacle = FindObstacle(newDestination);

            if (obstacle != null && obstacle.IsDestructable)
            {
                return true;
            }
            return false;
        }
    }
}
