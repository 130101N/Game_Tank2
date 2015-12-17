using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Move
    {
        public void PlayerMovement()
        {
            Map m = new Map();
            //Console.WriteLine( m.CollisionTiles[10]);
            Vector2 position = Vector2.Zero;
            Vector2 firstSquare = new Vector2(position.X+50, position.Y+50);
            Console.WriteLine(firstSquare);

        }
    }
}
