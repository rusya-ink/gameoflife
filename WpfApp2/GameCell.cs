using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class GameCell : IComparable
    {
        //Construct
        public GameCell(int X, int Y, bool Color=true)
        {
            this.X = X;
            this.Y = Y;
            this.Color = Color;
            Neihbours = 1;
        }

        //Comparing
        public int CompareTo(object obj)
        {
            int r = (TableOfGame.GetInstance().WIDTH * (X - ((GameCell)obj).X)) - (Y - ((GameCell)obj).Y);
            if (r == 0)
            {
                this.Neihbours = ((GameCell)obj).Neihbours = Math.Max(((GameCell)obj).Neihbours, this.Neihbours) + 1;
                if (this.Color || ((GameCell)obj).Color)
                    this.Color = ((GameCell)obj).Color = true;
            }
            return r;
        }

        //Parameters
        public int X { get; set; }
        public int Y { get; set; }
        public int Neihbours { get; set; }
        public bool Color { get; set; }
    }
}
