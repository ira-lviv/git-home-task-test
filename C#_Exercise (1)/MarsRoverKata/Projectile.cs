using System.Collections.Generic;
using System.Linq;

namespace MarsRoverKata
{
    public abstract class Projectile
    {
        protected virtual int MaxRange { get; set; }
        private Mars Mars { get; set; }

        public Projectile(Mars mars)
        {
            Mars = mars;
        }

        
        private static readonly Dictionary<Direction, Point> PositionalAdjustments = new Dictionary<Direction, Point>() 
        { 
            { Direction.North, new Point(0, 1) },
            { Direction.South, new Point(0, -1) },
            { Direction.East, new Point(1, 0) },
            { Direction.West, new Point(-1, 0) }
        };

      

        public void Launch(Direction facing, Point location)
        {
            bool collidedWithTarget = false;
            int moveIndex = 0;
            Point target = location;
            Point desiredPosition;
            while (!collidedWithTarget && moveIndex < MaxRange)
            {
                desiredPosition = CreateDesiredPosition(1, facing, target);
                target = CalculateProjectileFinalPosition(target, desiredPosition);
                collidedWithTarget = IsCollisionDetected(target);
                moveIndex++;
            }
            HitTarget(target);
        }

        private Point CreateDesiredPosition(int adjustmentFactor, Direction facing, Point location)
        {
            var adjustment = PositionalAdjustments[facing] * adjustmentFactor;
            return location + adjustment;
        }

        private Point CalculateProjectileFinalPosition(Point from, Point desired)
        {
            Point newDestination = desired;
            newDestination = CalculatePositionY(desired, newDestination);
            newDestination = CalculatePositionX(desired, newDestination);

            return newDestination;
        }

        protected abstract bool IsCollisionDetected(Point desired);
        //{
        //    if (!IsMortar)
        //    {
        //        Point newDestination = desired;
        //newDestination = CalculatePositionY(desired, newDestination);
        //newDestination = CalculatePositionX(desired, newDestination);
        //var obstacle = FindObstacle(newDestination);

        //        if (obstacle != null && obstacle.IsDestructable)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    return false;
        //}

protected Point CalculatePositionX(Point desired, Point newDestination)
        {
            if (desired.X > Mars.Bounds.Width)
            {
                newDestination = new Point(0, desired.Y);
            }
            if (desired.X < 0)
            {
                newDestination = new Point(Mars.Bounds.Width, desired.Y);
            }
            return newDestination;
        }

        protected Point CalculatePositionY(Point desired, Point newDestination)
        {
            if (desired.Y > Mars.Bounds.Height)
            {
                newDestination = new Point(desired.X, 0);
            }
            if (desired.Y < 0)
            {
                newDestination = new Point(desired.X, Mars.Bounds.Height);
            }
            return newDestination;
        }

        protected Obstacle FindObstacle(Point point)
        {
            return Mars.Obstacles.SingleOrDefault(x => x.Location.Equals(point));
        }

        private void HitTarget(Point point)
        {
            var obstacle = FindObstacle(point);

            if (obstacle != null && obstacle.IsDestructable)
            {
                DestroyObstacle(obstacle);
            }
            else if (obstacle == null)
            {
                CreateObstacle(point);
            }
        }

        private void DestroyObstacle(Obstacle obstacle)
        {
            Mars.RemoveObstacle(obstacle);
        }

        private void CreateObstacle(Point point)
        {
            Obstacle obstacle = new Crater(point, true);
            Mars.AddObstacle(obstacle);
        }
    }
}
