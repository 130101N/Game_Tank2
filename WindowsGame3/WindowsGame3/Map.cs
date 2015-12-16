using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Map
    {
        private List<CollisionTiles> colisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return colisionTiles; }
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


        public void Draw(SpriteBatch spritebatch)
        {
            foreach (CollisionTiles tile in colisionTiles)
                tile.Draw(spritebatch);
        }
    }
}
