using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsGame3
{
    class serverResponce
    {
        private Contestant contestant = new Contestant();
        private LifePack lifepack = new LifePack();
        private CoinPile coinpile = new CoinPile();

        public String joinserver()
        {
            return "JOIN#";
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

                default: return 0;
            }

        }

        public void accept(String msg)
        {

            int number = this.serverReply(msg);

            if (number > 0 && number < 10)
            {
                this.serverReply(msg);
            }
            else if (number == 0)
            {
                msg = msg.Remove(msg.Length - 1);
                if (msg.EndsWith("#"))
                {
                    msg = msg.Remove(msg.Length - 1);
                    char[] charArray = { ':' };
                    String[] tokens = msg.Split(charArray);

                    if (msg.StartsWith("I"))
                    {

                        char[] CharArray2 = { ';' };
                        String[] brickWalls = tokens[2].Split(CharArray2);
                        String[] obstacles = tokens[3].Split(CharArray2);
                        String[] water = tokens[4].Split(CharArray2);

                        Console.WriteLine("\n\nInitial map deatails\n");
                        Console.WriteLine("brick wall locations : ");
                        for (int i = 0; i < brickWalls.Length; i++) { Console.Write(brickWalls[i] + "\t"); }

                        Console.WriteLine("\nobstacle locations : ");
                        for (int i = 0; i < obstacles.Length; i++) { Console.Write(obstacles[i] + "\t"); }

                        Console.WriteLine("\nwater locations : ");
                        for (int i = 0; i < water.Length; i++) { Console.Write(water[i] + "\t"); }
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
                        Console.WriteLine(contestant.ToString());
                    }
                    else if (msg.StartsWith("C"))
                    {
                        Console.WriteLine("\nCurrent coinpiles");
                        coinpile.CoinPileLocationX = int.Parse(tokens[1].Substring(0, 1));
                        coinpile.CoinPileLocationY = int.Parse(tokens[1].Substring(2, 1));
                        coinpile.lifetime = int.Parse(tokens[2]);
                        coinpile.price = int.Parse(tokens[3]);
                        Console.WriteLine(coinpile.ToString());
                    }
                    else if (msg.StartsWith("L"))
                    {
                        Console.WriteLine("\nCurrent life packs");
                        lifepack.LifePackLocationX = int.Parse(tokens[1].Substring(0, 1));
                        lifepack.LifePackLocationY = int.Parse(tokens[1].Substring(2, 1));
                        lifepack.lifetime = int.Parse(tokens[2]);
                        Console.WriteLine(coinpile.ToString());
                    }

                }
                else
                {
                    Console.WriteLine("Error in message received..");

                }
            }

            Thread.Sleep(5000);

        }

        public void acceptance(String acceptanceText)
        {

            Console.WriteLine(acceptanceText);
            if (acceptanceText != "")
            {
                acceptanceText = acceptanceText.Remove(acceptanceText.Length - 1);
                char[] charArray = { ':', ';', ',' };
                String[] tokens = acceptanceText.Split(charArray);
                contestant.playerName = tokens[1];
                contestant.playerLocationX = int.Parse(tokens[2]);
                contestant.playerLocationY = int.Parse(tokens[3]);
                contestant.Direction = int.Parse(tokens[4]);
                Console.WriteLine(contestant.ToString());
            }
            else
            {
                Console.WriteLine("Error in message received..");

            }
            Thread.Sleep(5000);

        }

        public int initiation(String initialtionText)
        {
            initialtionText = initialtionText.Remove(initialtionText.Length - 1);
            if (initialtionText != null)  //initialtionText.StartsWith("I") && initialtionText.EndsWith("#")
            {
                initialtionText = initialtionText.Remove(initialtionText.Length - 1);
                char[] charArray = { ':' };
                String[] tokens = initialtionText.Split(charArray);

                char[] CharArray2 = { ';' };
                String[] brickWalls = tokens[2].Split(CharArray2);
                String[] obstacles = tokens[3].Split(CharArray2);
                /*
                if (tokens[1].Equals(contestant.playerName))
                {
                    char[] CharArray2 = { ';' };
                    String[] brickWalls = tokens[2].Split(CharArray2);
                    String[] obstacles = tokens[3].Split(CharArray2);


                }
                else
                {
                    Console.WriteLine("There is an error with the initiation String player mismatch");
                }*/
            }
            else
            {
                Console.WriteLine("There is an error with the initiation String");
                return 0;
            }
            return 1;
        }
    }
}
