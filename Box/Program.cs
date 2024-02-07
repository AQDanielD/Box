using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box
{
    internal class Program
    {
        public static Random RNG = new Random();

        public static int RollDice()
        {
            int Dice1 = RNG.Next(1, 7);
            int Dice2 = RNG.Next(1, 7);
            return Dice1 + Dice2;
        }

        public static void DisplayBox(bool[] boxes)
        {
            Console.WriteLine("[" + string.Join(",", boxes.Skip(1).Take(12).Select(b => b ? "1" : "0")) + "]");
        }

        static void CheckWin(ref bool[] doors)
        {
            doors[0] = doors.Skip(1).Take(12).Contains(false) ? false : true;
        }

        public static void SinglePlayer()
        {
            //Create the doors
            bool[] doors = new bool[13];

            int rolls = 19;
            while (rolls > 0)
            {
                //Say how many goes left
                Console.WriteLine($"You have {rolls--} rolls left.");
                //Roll Dice and show result
                int dice = RollDice();
                Console.Write($"Rolling...\r");
                Thread.Sleep(1000);
                Console.Write($"You Rolled {dice}!\n");
                //Set the door rolled as true
                doors[dice] = true;
                //Display the box
                DisplayBox(doors);
                //Check the win
                CheckWin(ref doors);
                Console.ReadLine();
                Console.Clear();

            }
            Console.WriteLine(doors[0] == true ? "You Won!": "You Lost");
            Console.ReadLine();
        }

        struct Player
        {
            public bool Alive;
            public int Losses;

        }
        static void CheckLoss(ref Player One,ref Player Two, int dice,bool[] Rolls, int roll)
        {
            //Uses disard variable so that the lamda operation can be completed
            _ = Rolls[dice]==true ? (roll % 2) == 0 ? One.Losses++ : Two.Losses++ : 0;
            _ = One.Losses < 3 ? One.Alive = false : One.Alive = true;
            _ = Two.Losses < 3 ? Two.Alive = false : Two.Alive = true;
        }
        public static void Multiplayer()
        {
            Player Player1 = new Player();
            Player Player2 = new Player();
            bool[] doors = new bool[13];
            int roll = 0;
            while (Player1.Alive == false&&Player2.Alive==false) 
            {
                Console.WriteLine((roll%2)==0 ? $"Player Ones roll!  Lives->{3-Player1.Losses}" : $"Player Twos roll!  Lives->{3 - Player2.Losses}");
                //Roll Dice and show result
                int dice = RollDice();
                //Checks if rolls has already been rolled
                //if so Losses is ++
                //also checks if player is still alive (less than 3 losses)
                CheckLoss(ref Player1,ref Player2,dice,doors,roll);
                roll++;
                //Add roll after since if not then roll will always be detected
                Console.Write($"Rolling...\r");
                Thread.Sleep(2000);
                Console.Write($"You Rolled {dice}!\n");
                //Tell them if its already been rolled
                Console.WriteLine(doors[dice] == true ? (roll % 2) != 0 ? "Player One lost a life!" : "Player Two lost a life!" : "");
                //Set the door rolled as true
                doors[dice] = true;
                //Display the box
                DisplayBox(doors);
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine(Player1.Alive==true? "Player One Lost!\nCongratulations to Player Two!": "Player Two Lost!\nCongratulations to Player One!");
            Console.ReadLine();

        }

        static void Main(string[] args)
        {
            Console.WriteLine("1. Single Player\n2. Multiplayer");
            Console.Write("-->");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1: 
                    Console.WriteLine("You have selected Single Player");Console.WriteLine("Loading...");Thread.Sleep(2000);Console.Clear();
                    SinglePlayer();
                        break;
                case 2: 
                    Console.WriteLine("You have selected Multiplayer"); Console.WriteLine("Loading..."); Thread.Sleep(2000); Console.Clear();
                    Multiplayer();
                    break;
            }

        }
    }
}
