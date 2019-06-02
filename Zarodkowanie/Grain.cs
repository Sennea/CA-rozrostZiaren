using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zarodkowanie
{
    public class Grain
    {
        private int index;
        private SolidBrush brush;
        private int x;
        private int y;

        public Grain(int index, int[] color, int x, int y)
        {
            this.index = index;
            brush = new SolidBrush(Color.FromArgb(color[0], color[1], color[2]));
            this.x = x;
            this.y = y;
        }

        

        public SolidBrush GetBrush()
        {
            return brush;
        }

        public int GetIndex()
        {
            return index;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}
