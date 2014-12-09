using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneablePoint
{
    public class PointDescription
    {
        public string PetName { get; set; }
        public Guid PointID { get; set; }

        public PointDescription()
        {
            PetName = "No-name";
            PointID = Guid.NewGuid();  
        }
    }

    public class Point : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointDescription desc = new PointDescription();

        public Point() { }
        public Point(int x, int y, string petName)
        {
            X = x;
            Y = y;
            desc.PetName = petName;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("X = {0}; Y = {1}; Name = {2};\nID = {3}\n", X, Y, desc.PetName, desc.PointID);
        }

        public object Clone() 
        { 
            //return new Point(this.X, this.Y);
            //return this.MemberwiseClone();

            Point newPoint = (Point)this.MemberwiseClone();
            PointDescription currDesc = new PointDescription();
            currDesc.PetName = this.desc.PetName;
            newPoint.desc = currDesc;
            return newPoint;
        }
    }
}
