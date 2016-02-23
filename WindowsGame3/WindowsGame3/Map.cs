using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace WindowsGame3
{
    class Map
    {
        private List<CollisionTiles> colisionTiles = new List<CollisionTiles>();
        private List<PlayerTiles> playerTiles = new List<PlayerTiles>();
        private List<CoinTiles> coinTiles = new List<CoinTiles>();
        private List<LifePackTiles> lifePackTiles = new List<LifePackTiles>();


        public List<CollisionTiles> CollisionTiles
        {
            get { return colisionTiles; }
        }

        public List<PlayerTiles> PlayerTiles
        {
            get { return playerTiles; }
        }

        public List<CoinTiles> CoinTiles
        {
            get { return coinTiles; }
        }

        public List<LifePackTiles> LifePackTiles
        {
            get { return lifePackTiles; }
        }



        private int width, height;
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map() { }

        public void Genarate(int[,] map, int size)
        {
            for (int x = 1; x <= map.GetLength(1); x++)
            {
                for (int y = 1; y <= map.GetLength(0); y++)
                {
                    int number = map[y - 1, x - 1];
                    if (number >= 0)
                    {
                        colisionTiles.Add(new CollisionTiles(number, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));

                    }
                    width = (x) * size;
                    height = (y) * size;
                }
            }
        }
        /*
        public void UpdateMap(int number,int x,int y, int size,int time)
        {
            if ( number== 6 ) {

                System.Timers.Timer aTimer = new System.Timers.Timer();

                aTimer.Interval = 1;
                aTimer.Start();
                  colisionTiles.Add(new CollisionTiles(number, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));

                
                width = (x) * size;
                height = (y) * size;
                aTimer.Stop();

            }else if (number == 7)
            {
                System.Timers.Timer aTimer = new System.Timers.Timer();

                aTimer.Interval = 1;
                aTimer.Start();
                
                    colisionTiles.Add(new CollisionTiles(number, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));
                
                width = (x) * size;
                height = (y) * size;
                aTimer.Stop();

                colisionTiles.Add(new CollisionTiles(1, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));

                width = (x) * size;
                height = (y) * size;
            }
            else
            {
                if (number >= 0)
                {
                    colisionTiles.Add(new CollisionTiles(number, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));

                }
                width = (x) * size;
                height = (y) * size;
            }


        }

    */
        public void UpdatePlayer(int x, int y, int size, int dir, bool isMove)
        {
            if (isMove == true)
            {
                PlayerTiles.RemoveAt(PlayerTiles.Count() - 1);
            }
            PlayerTiles.Add(new PlayerTiles(dir, new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));
            width = (x) * size;
            height = (y) * size;

        }

        public void UpdateCoins(int x, int y, int size, int time)
        {
            CoinTiles c = new CoinTiles(new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size));
            CoinTiles.Add(c);
            width = (x) * size;
            height = (y) * size;
            /*
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true)
            {
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.Milliseconds >= time)
                {
                    Console.WriteLine("elapsed time :" + ts.Milliseconds);
                    stopWatch.Stop();
                    CoinTiles.Remove(c);
                    break;
                }
            }
            
            
            
            */

            System.Timers.Timer aTimer = new System.Timers.Timer(100000);
            //aTimer.Interval = 10000;
            aTimer.Start();
            //if (aTimer.Elapsed == 10000 )
            //aTimer.Stop();
            //Thread.Sleep(50000);
            CoinTiles.Remove(c);

        }

        public void UpdateLifePack(int x, int y, int size, int time)
        {
            LifePackTiles.Add(new LifePackTiles(new Microsoft.Xna.Framework.Rectangle(x * size, y * size, size, size)));
            width = (x) * size;
            height = (y) * size;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (CollisionTiles tile in colisionTiles)
            {
                tile.Draw(spritebatch);

            }

            foreach (CoinTiles tile in coinTiles)
            {
                tile.Draw(spritebatch);

            }

            foreach (LifePackTiles tile in lifePackTiles)
            {
                tile.Draw(spritebatch);

            }

            foreach (PlayerTiles tile in playerTiles)
            {
                tile.Draw(spritebatch);

            }
        }

    }
}
