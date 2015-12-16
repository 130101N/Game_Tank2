using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Contestant
    {
        public string playerName { get; set; }
        public int playerLocationX { get; set; }
        public int playerLocationY { get; set; }
        private int direction = 0;
        private Boolean shot = false;
        private int pointsEarned = 0;
        private int coins = 0;
        private int health = 0;
        private bool isAlive = true;
        private bool invalidCell = false;
        private DateTime updatedTime;

        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }


        public Boolean Shot
        {
            get { return shot; }
            set { shot = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }


        public DateTime UpdatedTime
        {
            get { return updatedTime; }
            set { updatedTime = value; }
        }

        public int PointsEarned
        {
            get { return pointsEarned; }
            set { pointsEarned = value; }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public bool InvalidCell
        {
            get { return invalidCell; }
            set { invalidCell = value; }
        }



        public override string ToString()
        {
            return " X Coordinate " + this.playerLocationX
                + "\n Y Coordinate " + this.playerLocationY + "\n Current Direction " + this.direction
                + "\n";
        }
    }
}
