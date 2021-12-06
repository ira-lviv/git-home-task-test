namespace MarsRoverKata
{
    public abstract class Obstacle
    {
        public Point Location { get; protected set; }

        public Obstacle(Point location, bool isDestructable)
        {
            Location = location;
            IsDestructable = isDestructable;
        }

      public virtual bool IsDestructable { get; private set; }
    }
}
