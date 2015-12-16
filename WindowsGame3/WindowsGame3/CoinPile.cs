using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class CoinPile
    {
        public int CoinPileLocationX { get; set; }
        public int CoinPileLocationY { get; set; }
        public int lifetime { get; set; }
        public int price { get; set; }

        public override string ToString()
        {
            return " X Coordinate " + this.CoinPileLocationX + "\n Y Coordinate " + this.CoinPileLocationY
                + "\n Lifetime " + this.lifetime + "\n Price " + this.price + "\n";
        }
    }
}
