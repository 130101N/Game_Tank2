using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Tile
    {
        protected Texture2D texture;
        public SpriteFont font;
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
            font = Content.Load<SpriteFont>("SpriteFont");
            spriteBatch.DrawString(font, "Tank Game", new Vector2(20, 20), Color.White);
        }
    }
    class CollisionTiles : Tile
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("TTile" + i);

            this.Rectangle = newRectangle;
        }
    }

    class RocketTiles : Tile
    {
        public RocketTiles(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("roc");

            this.Rectangle = newRectangle;
        }
    }

    class PlayerTiles : Tile
    {
        public PlayerTiles(int dir, Rectangle newRectangle,int p)
        {
            texture = Content.Load<Texture2D>(p+"Tile5dir" + dir);

            this.Rectangle = newRectangle;
        }
    }

    class CoinTiles : Tile
    {
        public CoinTiles(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Tile6");

            this.Rectangle = newRectangle;
        }
    }

    class LifePackTiles : Tile
    {
        public LifePackTiles(Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Tile7");

            this.Rectangle = newRectangle;
        }
    }


}
