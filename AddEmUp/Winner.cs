using AddEmUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddEmUp
{
    public class Winner
    {
        public static List<Player> ReadTextFile(string location)
        {
            try
            {
                var listOfPlayers = new List<Player>();

                // Read out input text file here
                using (StreamReader sr = new StreamReader(location))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // replace and split data into a list per player 
                        var x = line.Replace(":", ",").Replace("\r", "").Split(',', '\n').ToList();
                        var player = new Player();
                        player.Name = x[0];
                        var cards = new List<string>();
                        for (int i = 1; i < x.Count; i++)
                        {
                            cards.Add(x[i]);
                        }
                        player.Cards = cards;

                        foreach (var card in player.Cards)
                        {   
                            // run through each players cards and assign them a value based on the enum 
                            foreach (int val in Enum.GetValues(typeof(Value)))
                            {
                                string name = Enum.GetName(typeof(Value), val);

                                if (name.Equals(card[0].ToString()))
                                {
                                    player.Score = player.Score + val;
                                }
                            }
                            if (card.Length > 2)
                            {
                                var twoDigit = card[0].ToString() + card[1].ToString();
                                int.TryParse(twoDigit, out int t);
                                player.Score = player.Score + t;
                            }
                            else if (card.Length == 2 && int.TryParse(card[0].ToString(), out int n))
                            {
                                player.Score = player.Score + n;
                            }
                        }

                        listOfPlayers.Add(player);
                    }

                }
                return listOfPlayers;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static List<Player> CheckForTie(List<Player> orderedPlayerList)
        {
            var tieList = new List<Player>();
            var winnerList = new List<Player>();

            tieList = orderedPlayerList.FindAll(x => x.Score == x.Score);

            if (tieList.Count >= 2)
            {
                // return the list of all players tied so we can calculate them based on suits.
                var tieWinner = CalculateSuitScore(orderedPlayerList);
                winnerList.Add(tieWinner);
                return winnerList;
            }
            else
            {
                //otherwise return a blank list
                return winnerList;
            }
        }

        public static Player CalculateSuitScore(List<Player> tieList)
        {
            // need to zero out the scores first before processing;
            foreach (var playerScore in tieList)
            {
                playerScore.Score = 0;
            }
            foreach (var player in tieList) 
            {
                foreach (var card in player.Cards)
                {
                    foreach (int val in Enum.GetValues(typeof(Suit)))
                    {
                        string name = Enum.GetName(typeof(Suit), val);

                        if (name.Equals(card[1].ToString()))
                        {
                            player.Score = player.Score + val;
                        }
                    }
                    if (card.Length > 2)
                    {
                        var lastValue = card[2].ToString();
                        int.TryParse(lastValue, out int t);
                        player.Score = player.Score + t;
                    }
                    else if (card.Length == 2 && int.TryParse(card[1].ToString(), out int n))
                    {
                        player.Score = player.Score + n;
                    }
                }
            }

            return tieList.OrderByDescending(x => x.Score).FirstOrDefault();
           
        }

        public static List<Player> GetWinner(List<Player> players)
        {
            try
            {
                var orderedByScoreList = players.OrderByDescending(x => x.Score);

                // check for tie
                var tieList = CheckForTie(players);

                if(tieList.Count != 0)
                {
                    // we have a tie
                    // run logic to calculate suit count
                    return tieList;
                }
                else
                {
                    return orderedByScoreList.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public static void WriteToFile(string WriteLocation, List<Player> players)
        {
            try
            {   // Write to our output file here
                using (StreamWriter writetext = new StreamWriter(WriteLocation))
                {
                    foreach(var player in players) {
                        writetext.WriteLine($"{player.Name} : {player.Score}");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
