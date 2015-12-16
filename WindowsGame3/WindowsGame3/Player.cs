using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Player
    {
        static public Vector2 Location = Vector2.Zero;
        //Vector2 firstSquare = new Vector2(Location.X / 32, Location.Y / 32);
        int firstX = 0;
        int firstY = 0;
        private Texture2D texture;
        public Player() { }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("TankRush");

        }

        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.Draw(texture,new Rectangle((0,0,50,50),));

        }


    }
}
