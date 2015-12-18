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
         Vector2 position;
        float rotation=0.0f;
        float speed=0.0f;
        Texture2D tank;
        string name;
        SpriteBatch sp;
        public Player(Vector2 pos)
        {
            position = pos;
            
        }
        public Player()
        {

        }

        public void LoadContent(ContentManager content, string name)
        {
            this.name = name;
            tank = content.Load<Texture2D>("TankRush");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tank, new Rectangle(50, 50, 50, 50), new Rectangle(0, 0, 50, 50), Color.White);
        }
       



    }
}
