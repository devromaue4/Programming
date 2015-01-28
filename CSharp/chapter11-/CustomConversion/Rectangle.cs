using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConversion
{
    struct Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public Rectangle(int w, int h) : this() { Width = w; Height = h; }

        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write("*");     

                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return string.Format("[Widht = {0}; Height = {1}]", Width, Height);
        }

        public static implicit operator Rectangle(Square sq)
        {
            Rectangle newRec = new Rectangle();
            newRec.Height = sq.Length;
            newRec.Width = sq.Length * 2;
            return newRec;
        }
    }

    struct Square
    {
        public int Length { get; set; }

        public Square(int l) : this() { Length = l; }

        public void Draw()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                    Console.Write("*");
             
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return string.Format("[Length = {0}]", Length);
        }

        // Можно явно преобразовать в Square
        public static explicit operator Square(Rectangle rec)
        {
            Square sq = new Square();
            sq.Length = rec.Height;
            return sq;
        }

        public static explicit operator Square(int sideLength)
        {
            Square newSq = new Square();
            newSq.Length = sideLength;
            return newSq;
        }

        public static explicit operator int(Square s)
        { return s.Length; }
    }
}
