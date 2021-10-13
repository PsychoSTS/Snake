using System;

namespace Snake
{
    public class Position
    {
        public int X = 0;
        public int Y = 0;
        
        private bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public Position(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position a, Position b)
        {
            if (ReferenceEquals(null, a)) return false;
            if (ReferenceEquals(null, b)) return false;
            return a.X == b.X && a.Y == b.Y;
        }
        
        public static bool operator !=(Position a, Position b)
        {
            if (ReferenceEquals(null, a)) return false;
            if (ReferenceEquals(null, b)) return false;
            return a.X != b.X || a.Y != b.Y;
        }
    }
}