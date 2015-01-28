using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadOps
{
    class Point : IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get;  set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.X, this.Y);
        }

        public static Point operator + (Point p1, Point p2)
        { return new Point(p1.X + p2.X, p1.Y + p2.Y); }

        public static Point operator - (Point p1, Point p2)
        { return new Point(p1.X - p2.X, p1.Y - p2.Y); }

        public static Point operator ++(Point p)
        { return new Point(p.X+1, p.Y+1);}

        public static Point operator --(Point p)
        { return new Point(p.X-1, p.Y-1); }

        public override bool Equals(Object obj)
        { return obj.ToString() == this.ToString(); }

        public override int GetHashCode()
        { return this.ToString().GetHashCode(); }

        public static bool operator ==(Point p1, Point p2)
        { return p1.Equals(p2); }

        public static bool operator !=(Point p1, Point p2)
        { return !p1.Equals(p2); }

        public int CompareTo(Point other)
        {
            if (this.X > other.X && this.Y > other.Y)
                return 1;
            if (this.X < other.X && this.Y < other.Y)
                return -1;
            else 
                return 0;
        }

        public static bool operator <(Point p1, Point p2)
        { return (p1.CompareTo(p2) < 0); }

        public static bool operator >(Point p1, Point p2)
        { return (p1.CompareTo(p2) > 0); }

        public static bool operator <=(Point p1, Point p2)
        { return (p1.CompareTo(p2) <= 0); }

        public static bool operator >=(Point p1, Point p2)
        { return (p1.CompareTo(p2) >= 0); }
    }
}
