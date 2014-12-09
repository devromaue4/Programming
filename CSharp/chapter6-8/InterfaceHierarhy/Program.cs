using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceHierarhy
{
    public interface IDrawable
    {
        void Draw();
    }

    public interface IAdvancedDraw : IDrawable
    {
        void DrawInBoundingBox(int top, int left, int bottom, int right);
        void DrawUpsideDown();
    }

    public class BitmapImage : IAdvancedDraw
    {
        public void Draw() { Console.WriteLine("Drawing..."); }
        public void DrawInBoundingBox(int top, int left, int bottom, int right) { Console.WriteLine("Drawing in a box..."); }
        public void DrawUpsideDown() { Console.WriteLine("Drawing upside down!"); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BitmapImage btm = new BitmapImage();
            btm.Draw();
            btm.DrawInBoundingBox(10, 10, 10, 150);
            btm.DrawUpsideDown();

            IAdvancedDraw aDraw = btm as IAdvancedDraw;
            if (aDraw != null)
                aDraw.DrawUpsideDown();

            Console.ReadLine();
        }
    }
}
