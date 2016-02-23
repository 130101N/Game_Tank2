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
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Diagnostics;

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

        private bool isConnected;
        private Contestant contestant = new Contestant();
        private LifePack lifepack = new LifePack();
        private CoinPile coinpile = new CoinPile();
        int[,] initialLocations = new int[10, 10];
        Texture2D tank;
        Texture2D coin;
        Texture2D life;
        Texture2D backgroundTexture;
        Vector2 playerpos;
        int playercurrentdir;
        int playercurrentX;
        int playercurrentY;
        Vector2 lifepackpos;
        Vector2 coinpilepos;
        List<LifePack> lifepacklist = new List<LifePack>();
        List<CoinPile> coinpilelist = new List<CoinPile>();


        //Rectangle rectangle;
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
            if (isConnected == false)
            {
                Thread aThread = new Thread(new ThreadStart(joinserver));
                aThread.Start();

                Thread bThread = new Thread(new ThreadStart(waitForConnection));
                bThread.Start();


                isConnected = true;

            }
            else
            {
                Console.WriteLine("You are already connected to the server");
            }
            /*
            Thread cThread = new Thread(new ThreadStart(sendupdates));
            cThread.Start();
            */

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

            backgroundTexture = Content.Load<Texture2D>("background1");
            //tank = Content.Load<Texture2D>("Tile5");
            coin = Content.Load<Texture2D>("Tile6");
            life = Content.Load<Texture2D>("Tile7");

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




            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            map.Draw(spriteBatch);
            /*drawplayer();
            drawLifepacks();
            drwaCoins();

            */
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void joinserver()
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();      //create a TcpCLient socket to connect to server
            NetworkStream stream = null;

            //connecting to server socket with port 6000
            clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
            stream = clientSocket.GetStream();

            //joining message to server
            byte[] ba = Encoding.ASCII.GetBytes("JOIN#");
            /*
            for (int x = 0; x < ba.Length; x++)
            {
                Console.WriteLineLine(ba[x]);
            }
            */
            stream.Write(ba, 0, ba.Length);        //send message to server
            stream.Flush();
            stream.Close();          //close network stream
        }

        public void sendupdates()
        {

            String s = move();
            Console.WriteLine("playercurX " + playercurrentX);
            Console.WriteLine("playercurY " + playercurrentY);
            Console.WriteLine("move " + s);
            connect(s);
        }

        public void connect(String s)
        {

            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();      //create a TcpCLient socket to connect to server
            NetworkStream stream = null;

            //connecting to server socket with port 6000
            clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
            stream = clientSocket.GetStream();

            //joining message to server
            byte[] ba = Encoding.ASCII.GetBytes(s);
            /*
            for (int x = 0; x < ba.Length; x++)
            {
                Console.WriteLineLine(ba[x]);
            }
            */
            stream.Write(ba, 0, ba.Length);        //send message to server
            stream.Flush();
            stream.Close();          //close network stream
        }

        public void waitForConnection()
        {
            try
            {
                //Creating listening Socket
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);

                Console.WriteLine("waiting for server response");

                //Starts listening
                listener.Start();

                //Establish connection upon server request

                while (true)
                {
                    TcpClient connection = listener.AcceptTcpClient();   //connection is connected socket

                    Console.WriteLine("Connetion is established");

                    //get the incoming data through a network stream---
                    NetworkStream serverStream = connection.GetStream();
                    byte[] buffer = new byte[connection.ReceiveBufferSize];

                    //read incoming stream
                    int bytesRead = serverStream.Read(buffer, 0, connection.ReceiveBufferSize);

                    String messageFromServer = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                    Console.WriteLine("Response from server \n" + messageFromServer);
                    accept(messageFromServer);


                    serverStream.Close();                         //close the netork stream


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.StackTrace);
            }
        }


        public int serverJoinReply(String reply)
        {
            switch (reply)
            {
                case "PLAYERS_FULL#": Console.WriteLine("Players full"); return 1;
                case "ALREADY_ADDED#": Console.WriteLine("Already added"); return 2;
                case "GAME_ALREADY_STARTED#": Console.WriteLine("Game already started"); return 3;
                default: return 0;
            }

        }

        public int serverReply(String reply)
        {

            switch (reply)
            {
                case "PLAYERS_FULL#": Console.WriteLine("Players full"); return 1;
                case "ALREADY_ADDED#": Console.WriteLine("Already added"); return 2;
                case "GAME_ALREADY_STARTED#": Console.WriteLine("Game already started"); return 3;

                case "INVALID_CELL": Console.WriteLine("Invalid cell"); return 4;
                case "NOT_A_VALID_CONTESTANT": Console.WriteLine("Invalid contestant"); return 5;
                case "TOO_QUICK#": Console.WriteLine("Too quick"); return 6;
                case "CELL_OCCUPIED#": Console.WriteLine("Cell occupied"); return 7;
                case "OBSTACLE#": Console.WriteLine("Obstacle"); return 8;
                case "PITFALL#": Console.WriteLine("Pitfall"); return 9;

                case "DEAD#": Console.WriteLine("Game finished"); return 9;
                case "GAME_HAS_FINISHED#": Console.WriteLine("dead"); return 9;



                default: return 0;
            }
        }

        public void accept(String msg)
        {


            int number = this.serverReply(msg);

            if (number != 0)
            {
                serverReply(msg);
            }
            else if (number == 0)
            {

                if (msg.EndsWith("#"))
                {
                    msg = msg.Remove(msg.Length - 1);
                    char[] charArray = { ':' };
                    String[] tokens = msg.Split(charArray);

                    if (msg.StartsWith("I"))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                initialLocations[i, j] = 4;
                            }
                        }

                        char[] CharArray2 = { ';' };
                        String[] brickWalls = tokens[2].Split(CharArray2);

                        String[] obstacles = tokens[3].Split(CharArray2);
                        String[] water = tokens[4].Split(CharArray2);

                        Console.WriteLine("\n\nInitial map deatails\n");
                        Console.WriteLine("brick wall locations : \n");
                        for (int i = 0; i < brickWalls.Length; i++)
                        {
                            int[] ints = brickWalls[i].Split(',').Select(int.Parse).ToArray();
                            initialLocations[ints[1], ints[0]] = 2;
                            Console.WriteLine(brickWalls[i] + "\t");
                        }

                        Console.WriteLine("\nobstacle locations : ");
                        for (int i = 0; i < obstacles.Length; i++)
                        {
                            int[] ints = obstacles[i].Split(',').Select(int.Parse).ToArray();
                            initialLocations[ints[1], ints[0]] = 3;
                            Console.WriteLine(obstacles[i] + "\t");
                        }

                        Console.WriteLine("\nwater locations : ");
                        for (int i = 0; i < water.Length; i++)
                        {
                            int[] ints = water[i].Split(',').Select(int.Parse).ToArray();
                            initialLocations[ints[1], ints[0]] = 1;
                            Console.WriteLine(water[i] + "\t");
                        }

                        map.Genarate(initialLocations, 50);




                    }
                    else if (msg.StartsWith("S"))
                    {
                        char[] CharArray2 = { ';' };
                        string[] playerDetails = tokens[1].Split(CharArray2);
                        contestant.playerName = playerDetails[0];
                        Console.WriteLine("\nNew player :" + contestant.playerName);
                        contestant.playerLocationX = int.Parse(playerDetails[1].Substring(0, 1));
                        contestant.playerLocationY = int.Parse(playerDetails[1].Substring(2, 1));
                        contestant.Direction = int.Parse(playerDetails[2]);

                        playerpos.X = (contestant.playerLocationX + 2) * 50;
                        playerpos.Y = (contestant.playerLocationY + 2) * 50;
                        playercurrentdir = contestant.Direction;

                        map.UpdatePlayer(contestant.playerLocationX + 1, contestant.playerLocationY + 1, 50, contestant.Direction, false);
                        Console.WriteLine(contestant.ToString());
                    }
                    else if (msg.StartsWith("G"))
                    {
                        char[] CharArray2 = { ';' };
                        string[] playerDetails = tokens[1].Split(CharArray2);
                        contestant.playerName = playerDetails[0];
                        Console.WriteLine("\nCurrent deatails of " + contestant.playerName);
                        contestant.playerLocationX = int.Parse(playerDetails[1].Substring(0, 1));
                        contestant.playerLocationY = int.Parse(playerDetails[1].Substring(2, 1));
                        contestant.Direction = int.Parse(playerDetails[2]);

                        playerpos.X = (contestant.playerLocationX + 2) * 50;
                        playerpos.Y = (contestant.playerLocationY + 2) * 50;

                        playercurrentX = contestant.playerLocationX;
                        playercurrentY = contestant.playerLocationY;

                        string tm = tankmove(contestant.playerLocationX, contestant.playerLocationY);
                        if (tm != "invalid")
                        {
                            connect(tm);
                            Thread.Sleep(20);
                            map.UpdatePlayer(contestant.playerLocationX + 1, contestant.playerLocationY + 1, 50, contestant.Direction, true);
                        }


                        Console.WriteLine(contestant.ToString());
                    }
                    else if (msg.StartsWith("C"))
                    {
                        Console.WriteLine("\nCurrent coinpiles");
                        coinpile.CoinPileLocationX = int.Parse(tokens[1].Substring(0, 1));
                        coinpile.CoinPileLocationY = int.Parse(tokens[1].Substring(2, 1));
                        coinpile.lifetime = int.Parse(tokens[2]);
                        coinpile.price = int.Parse(tokens[3]);

                        coinpilelist.Add(coinpile);

                        map.UpdateCoins(coinpile.CoinPileLocationX + 1, coinpile.CoinPileLocationY + 1, 50, 0);
                        Console.WriteLine(coinpile.ToString());
                    }
                    else if (msg.StartsWith("L"))
                    {
                        Console.WriteLine("\nCurrent life packs");
                        lifepack.LifePackLocationX = int.Parse(tokens[1].Substring(0, 1));
                        lifepack.LifePackLocationY = int.Parse(tokens[1].Substring(2, 1));
                        lifepack.lifetime = int.Parse(tokens[2]);

                        lifepacklist.Add(lifepack);

                        map.UpdateLifePack(lifepack.LifePackLocationX + 1, lifepack.LifePackLocationY + 1, 50, 0);
                        Console.WriteLine(coinpile.ToString());
                    }

                }
                else
                {
                    Console.WriteLine("Error in message received..");

                }
            }


        }

        public void drawLifepacks()
        {
            for (int i = 0; i < lifepacklist.Count; i++)
            {
                lifepackpos.X = (lifepacklist[i].LifePackLocationX + 2) * 50;
                lifepackpos.Y = (lifepacklist[i].LifePackLocationY + 2) * 50;
                spriteBatch.Draw(life, lifepackpos, null, Color.White, 0, new Vector2(50, 50), 1, SpriteEffects.None, 0f);

            }
        }

        public void drwaCoins()
        {
            for (int i = 0; i < coinpilelist.Count; i++)
            {
                coinpilepos.X = (coinpilelist[i].CoinPileLocationX + 2) * 50;
                coinpilepos.Y = (coinpilelist[i].CoinPileLocationY + 2) * 50;
                spriteBatch.Draw(coin, coinpilepos, null, Color.White, 0, new Vector2(50, 50), 1, SpriteEffects.None, 0f);

            }
        }

        public void drawplayer()
        {
            spriteBatch.Draw(tank, playerpos, null, Color.White, ((float)Math.PI / 2) * playercurrentdir, new Vector2(50, 50), 1, SpriteEffects.None, 0f);

        }

        // AI

        public bool ValidCoordinates(int x, int y)
        {

            if (x >= 0 && x < 10 && y < 10 && y >= 0)
            {
                if (initialLocations[y, x] == 4)
                {
                    Console.WriteLine(initialLocations[x, y]);
                    return true;
                }
            }

            return false;
        }

        public String move()
        {
            if (ValidCoordinates(playercurrentX + 1, playercurrentY))
            {
                playerpos.X = (playercurrentX + 1 + 2) * 50;
                playerpos.Y = (playercurrentY + 2) * 50;
                return "DOWN#";
            }
            if (ValidCoordinates(playercurrentX, playercurrentY + 1))
            {
                playerpos.X = (playercurrentX + 2) * 50;
                playerpos.Y = (playercurrentY + 1 + 2) * 50;
                return "RIGHT#";
            }
            if (ValidCoordinates(playercurrentX - 1, playercurrentY))
            {
                playerpos.X = (playercurrentX - 1 + 2) * 50;
                playerpos.Y = (playercurrentY + 2) * 50;
                return "UP#";
            }
            if (ValidCoordinates(playercurrentX, playercurrentY - 1))
            {
                playerpos.X = (playercurrentX + 2) * 50;
                playerpos.Y = (playercurrentY - 1 + 2) * 50;
                return "LEFT#";
            }
            return "";
        }

        public String tankmove(int x, int y)
        {

            string command = "invalid";
            if (ValidCoordinates(x, y + 1))
            {
                command = "DOWN#";
            }
            else if (ValidCoordinates(x + 1, y))
            {
                command = "RIGHT#";
            }
            else if (ValidCoordinates(x, y - 1))
            {
                command = "UP#";
            }
            else if (ValidCoordinates(x - 1, y))
            {
                command = "LEFT#";
            }
            return command;
        }
    }
}
