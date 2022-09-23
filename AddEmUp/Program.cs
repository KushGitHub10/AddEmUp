// See https://aka.ms/new-console-template for more information
using AddEmUp;
using AddEmUp.Model;
using Newtonsoft.Json;
namespace AddEmUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Readlocation = @"./abc.txt";
            var Writelocation = @"./xyz.txt";

            Console.WriteLine("Reading from Textfile");
            var playerData = Winner.ReadTextFile(Readlocation);

            Console.WriteLine("All Player scores");
            foreach (var item in playerData)
            {
                Console.WriteLine($"{item.Name} : {item.Score}");
            }

            var getWinner = Winner.GetWinner(playerData);

            foreach (var winner in getWinner)
            {
                if(getWinner.Count == 1)
                {
                    Console.WriteLine($"Winner: {getWinner.FirstOrDefault().Name} : {getWinner.FirstOrDefault().Score}");

                }
                else
                {
                    
                    Console.WriteLine($"Winner: {getWinner.FirstOrDefault().Name} : {getWinner.FirstOrDefault().Score}, ");

                }
            }


            Console.WriteLine("Writing to Textfile");
            Winner.WriteToFile(Writelocation, getWinner);
            Console.WriteLine("Game Over Thank you");

        }
    }
}

    
