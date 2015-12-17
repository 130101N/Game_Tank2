using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Texture2D texture;
        //Rectangle rectangle;
        Texture2D tank;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 820;
            graphics.PreferredBackBufferHeight = 620;
            map = new Map();
            base.Initialize();

            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.Content = Content;
            //rectangle = new Rectangle( 1,1,texture.Width,texture.Height);
            map.Genarate(new int[,]{
                {4,4,4,2,4,2,4,3,4,1},
                {3,4,4,4,4,4,4,2,4,4},
                {4,3,4,4,4,2,4,4,4,2},
                {1,4,4,4,3,4,4,4,4,4},
                {4,4,4,2,4,1,4,4,4,4},
                {3,1,2,4,4,2,4,2,3,4},
                {4,4,3,2,1,4,4,1,4,4},
                {2,4,4,4,4,4,4,3,2,4},
                {4,3,4,4,4,2,4,4,4,2},
                {4,4,4,2,4,1,4,4,4,4},

            }, 50);

            texture = Content.Load<Texture2D>("TankRush");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.Draw(texture, new Rectangle(50, 50, 50, 50), new Rectangle(0, 0, 50, 50), Color.White);
            Move m = new Move();
            m.PlayerMovement();
            spriteBatch.End();
            base.Draw(gameTime);
        }


        /*
        private List<Vector2> FindPath_(Node start, Vector2 end)
        {
            if (start.X == end.X && start.Y == end.Y)
            {
                List<Vector2> final = mClosed.Select(node => node.Position).ToList();
                return final;
            }

            int xLeft = start.X - 1;
            int xRight = start.X + 1;
            int yUp = start.Y - 1;
            int yDown = start.Y + 1;


            Tile left = GetNearestTile_(xLeft, start.Y);
            Tile right = GetNearestTile_(xRight, start.Y);
            Tile up = GetNearestTile_(start.X, yUp);
            Tile down = GetNearestTile_(start.X, yDown);

            List<Node> adjacentNodes = new List<Node>();

            Node lNode = null;
            Node rNode = null;
            Node uNode = null;
            Node dNode = null;

            if (up != null)
            {
                uNode = new Node(new Vector2(start.X, yUp), start.CostFromA + 1, Math.Abs((int)end.X - start.X) + Math.Abs((int)end.Y - (start.Y - 1)), start);
                adjacentNodes.Add(uNode);
            }
            if (left != null)
            {
                lNode = new Node(new Vector2(xLeft, start.Y), start.CostFromA + 1, Math.Abs((int)end.X - (start.X - 1)) + Math.Abs((int)end.Y - start.Y), start);
                adjacentNodes.Add(lNode);
            }
            if (down != null)
            {
                dNode = new Node(new Vector2(start.X, yDown), start.CostFromA + 1, Math.Abs((int)end.X - start.X) + Math.Abs((int)end.Y - (start.Y + 1)), start);
                adjacentNodes.Add(dNode);
            }
            if (right != null)
            {
                rNode = new Node(new Vector2(xRight, start.Y), start.CostFromA + 1, Math.Abs((int)end.X - (start.X + 1)) + Math.Abs((int)end.Y - start.Y), start);
                adjacentNodes.Add(rNode);
            }



            foreach (Node node in adjacentNodes)
            {
                if (ClosedListContainsTile_(node))
                {
                    continue;
                }
                else if (!OpenListContainsTile_(node))
                {
                    InsertNode(node);
                }
                else if (OpenListContainsTile_(node))
                {
                    if (node.Parent != null && node.CostFromA + 1 < node.Parent.CostFromA)
                    {
                        node.Parent.CostFromA = node.CostFromA + 1;
                        Node parentNode = mOpen.FirstOrDefault(n => n.X == node.Parent.X && n.Y == node.Parent.Y);
                        int index = mOpen.IndexOf(parentNode);
                        mOpen.RemoveAt(index);
                        InsertNode(parentNode);
                    }
                }
            }

            if (mOpen.Count == 0)
                return new List<Vector2>();

            int cost = int.MaxValue;

            Node closest = mOpen[0];


            mOpen.Remove(closest);
            mClosed.Add(closest);


            return FindPath_(closest, end);

        }

        private Tile GetNearestTile_(int x, int y)
        {
            Tile tile;
            try
            {
                tile = Map(x, y);
                if (tile.Type != TileType.Empty)
                    return null;
            }
            catch
            {
                return null;
            }
            return tile;
        }

        private bool OpenListContainsTile_(Node node)
        {
            return mOpen.FirstOrDefault(t => t.X == node.X && t.Y == node.Y) != null;
        }

        private bool ClosedListContainsTile_(Node node)
        {
            return mClosed.FirstOrDefault(t => t.X == node.X && t.Y == node.Y) != null;
        }

        private void InsertNode(Node node)
        {
            int i = 0;
            for (; i < mOpen.Count; i++)
            {
                Node n = mOpen.ElementAt(i);
                if (node.Total <= n.Total)
                {

                    break;
                }
            }
            if (i < mOpen.Count)
            {
                mOpen.Insert(i, node);
            }
            else
            {
                mOpen.Add(node);
            }
        }

    }*/
    }
}
