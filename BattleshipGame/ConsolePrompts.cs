using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    internal class ConsolePrompts
    {
        public static void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Battleship Game!");
        }

        public static void ShowActivePlayerAndActiveOpponent(GameModel game)
        {
            Console.WriteLine($"Active player: {game.ActivePlayer.Name} | Active opponent: {game.ActiveOpponent.Name}");
        }
        

        public static void ShowOpponentGrid(GameModel game, PlayerInfoModel currentOpponent)
        {
            // Loop through all letters
            foreach (var gridLetter in game.AllowedGridSpotRows)
            {
                Console.WriteLine();

                // Display all grid spots for specific letter / Mark sunk with X, missed shots as O
                var letterGridSpots = currentOpponent.ShipList.FindAll(x => x.SpotLetter == gridLetter);
                foreach (var spot in letterGridSpots)
                {
                    
                    switch (spot.Status)
                    {
                        case GridSpotStatus.Hit:
                            Console.Write("XX ");
                            break;
                        case GridSpotStatus.Miss:
                            Console.Write("OO ");
                            break;
                        default:
                            Console.Write($"{spot.SpotLetter}{spot.SpotNumber} ");
                            break;
                    }
                }
            }

            Console.WriteLine("\n");

        }

        public static string AskForGridRowsAmount()
        {
            string output;

            Console.Write("Please specify how many rows (i.e. A, B, C, D) ship grid should contain: ");
            output = Console.ReadLine();

            return output;
        }

        public static string AskForGridColumnsAmount()
        {
            string output;

            Console.Write("Please specify how many colums (i.e. 1, 2, 3, 4) ship grid should contain: ");
            output = Console.ReadLine();

            return output;
        }

        public static bool AskForCustomConfiguration()
        {
            bool output;

            Console.WriteLine("Do you want to apply custom game configuration?");
            Console.Write("Type \"Yes\" to confirm: ");

            string userInput = Console.ReadLine();
            output = userInput.ToLower() == "yes" ? true : false;

            return output;
        }

        public static void NotifyPressKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void NotifyGameFinished()
        {
            Console.WriteLine("You've won the game! Press any key to continue...");
            Console.ReadKey();
        }

        public static void ShowOpponentStatus(PlayerInfoModel activeOpponent)
        {
            if (activeOpponent.Status == PlayerStatus.Defeated)
            {
                Console.WriteLine($"{activeOpponent.Name} fleet has been destroyed!");
            }
            else if (activeOpponent.Status == PlayerStatus.Active)
            {
                int shipsLeftCount = activeOpponent.ShipList.FindAll(x => x.Status == GridSpotStatus.Ship).Count;
                Console.WriteLine($"Remaining ships for {activeOpponent.Name}: {shipsLeftCount}");
            } 
        }

        public static void ShowShotResult(PlayerInfoModel activePlayer)
        {
            GridSpotModel lastShot = activePlayer.ShotList.Last().Item2;

            if (lastShot.Status == GridSpotStatus.Hit)
            {
                Console.WriteLine("You've sunk your opponent's ship!");
            }
            else if (lastShot.Status == GridSpotStatus.Miss)
            {
                Console.WriteLine("Unfortunately, your shot was a miss!");
            }
        }

        public static void ShowPlayerNumber(int playerNumber)
        {
            Console.WriteLine($"Player {playerNumber}");
        }

        public static string AskPlayerName()
        {
            Console.Write("Please provide your name: ");

            string output = Console.ReadLine();
            return output;
        }

        public static string AskForPlayersAmount()
        {
            string output;

            Console.Write("How many players will be playing today?: ");
            output = Console.ReadLine();

            return output;
        }

        public static void ShowPlacedShips(PlayerInfoModel player)
        {
            var placedShips = player.ShipList.FindAll(x => x.Status == GridSpotStatus.Ship);

            Console.Write("Current selection: ");
            foreach (var ship in placedShips)
            {

            }
        }

        public static void ShowWinningPlayer(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations {winner.Name}! You have won the game!");
        }

        public static string AskForShotPlacement()
        {
            string output;

            Console.Write("Please select spot for your next shot: ");
            output = Console.ReadLine();

            return output;
        }

        public static string AskForPlayersShipsAmount()
        {
            string output;

            Console.Write("Please specify number of player ships: ");
            output = Console.ReadLine();

            return output;
        }

        public static void ShowWinningPlayerStatistics(PlayerInfoModel winner)
        {
            Console.WriteLine("Your statistics:");
            Console.WriteLine($"Total shots: {winner.ShotList.Count}");
            Console.WriteLine($"Hit shots: {winner.ShotList.FindAll(x => x.Item2.Status == GridSpotStatus.Hit).Count}");
            Console.WriteLine($"Miss shots: {winner.ShotList.FindAll(x => x.Item2.Status == GridSpotStatus.Miss).Count}");
        }

        public static string AskForShipPlacement(int nextShipCount, string[] selectedShipSpots)
        {
            string output;

            if (nextShipCount > 1) { Console.WriteLine($"Current selection: {String.Join(", ", selectedShipSpots)}"); }
            Console.Write($"Please select spot for ship number {nextShipCount}: ");
            
            output = Console.ReadLine();

            return output;
        }
    }
}
