using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    internal class Program
    {
        public static Random RNG = new Random();

        public static int RollDice()
        {
            int Dice1 = RNG.Next(2, 7);
            int Dice2 = RNG.Next(2, 7);
            return Dice1 + Dice2;
        }

        public static void DisplayBox(bool[] boxes)
        {
            Console.Write("\n");
            Console.Write("[");
            for (int i = 1; i <= boxes.Length - 1; i++)
            {
                switch (boxes[i])
                {
                    case false:
                        Console.Write("0");
                        break;
                    case true:
                        Console.Write("1");
                        break;

                }
                Console.Write(",");
            }
            Console.Write("]");
        }
        static void CheckWin(ref bool[] doors)
        {
            for(int i = 1; i< doors.Length - 1; i++)
            {
                if (doors[i] == true)
                {
                    doors[0] = true;
                }
            }
        }

        static void Main(string[] args)
        {
            //Create the doors
            bool[] doors = new bool[13];

            int rolls = 20;
            while (rolls > 0 && doors[0] != true)
            {
                //Roll Dice and show result
                int dice = RollDice();
                Console.WriteLine($"You Rolled {dice}!");
                //Set the door rolled as true
                doors[dice] = true;
                //Display the box
                DisplayBox(doors);
                //Check the win
                CheckWin(ref doors);
            }
            if (doors[0] == true)
            {
                Console.WriteLine("You Won!");
            }

        }
    }
}
