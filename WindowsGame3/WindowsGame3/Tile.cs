using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    static class Tile
    {
        static public Texture2D TileSetTexture;

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            return new Rectangle(tileIndex * 50, 0, 50, 50);
        }


    }
}
