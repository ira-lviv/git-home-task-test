using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverKata
{
    public class Mars
    {
        public Size Bounds { get; private set; }
        public Point CenterOfThePlanet { get; private set; }
        public readonly List<Obstacle> _obstacles;
        public Mars(Size bounds)
        {
            Bounds = bounds;
            CenterOfThePlanet = new Point(Bounds.Width / 2, Bounds.Height / 2);
            _obstacles = new List<Obstacle>();
//            LandOnMars(CenterOfThePlanet);
        }

        public IReadOnlyList<Obstacle> Obstacles
        {
            get { return _obstacles; }
        }

        public void AddObstacle(Obstacle obstacle)
        {
            _obstacles.Add(obstacle);
        }
        public bool IsValidPosition(Point point)
        {
            bool anyInstance = _obstacles.Any(x => x.Location.Equals(point));

            return !anyInstance;
        }

        public Point CalculateFinalPosition(Point from, Point desired)
        {
            Point newDestination = desired;
            newDestination = CalculatePositionY(desired, newDestination);
            newDestination = CalculatePositionX(desired, newDestination);

            if (!IsValidPosition(newDestination))
            {
                return from;
            }

            return newDestination;
        }

        private Point CalculatePositionX(Point desired, Point newDestination)
        {
            if (desired.X > Bounds.Width)
            {
                newDestination = new Point(0, desired.Y);
            }
            if (desired.X < 0)
            {
                newDestination = new Point(Bounds.Width, desired.Y);
            }
            return newDestination;
        }

        private Point CalculatePositionY(Point desired, Point newDestination)
        {
            if (desired.Y > Bounds.Height)
            {
                newDestination = new Point(desired.X, 0);
            }
            if (desired.Y < 0)
            {
                newDestination = new Point(desired.X, Bounds.Height);
            }
            return newDestination;
        }
    }
}
