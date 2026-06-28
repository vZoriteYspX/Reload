using System;

namespace Game
{

    
    public class MainProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What will be your next move?");
            string userInput1 = Console.ReadLine().ToLower();
            Player1 turnUsed = new(userInput1);
        }
    }


    public class Charges
    {
        public float p1Charge = 1;
        public const int defCharge = 0;
        public const float halfCharge = 0.5F;
        public const int oneCharge = 1;
        public const int twoCharge = 2;
        public const int threeCharge = 3;
        public const int fourCharge = 4;
    }


    public class Player1 : Charges
    {
        public Player1(string userInput1)
        {
            switch(userInput1)
            {
                case "teleport":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.teleportMove} for {defCharge} charge(s)!");
                    break;
                }

                case "vanish":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.vanishMove} for {defCharge} charge(s)!");
                    break;
                }

                case "shield":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.shieldMove} for {defCharge} charge(s)!");
                    break;
                }

                case "barrier":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.barrierMove} for {defCharge} charge(s)!");
                    break;
                }

                case "nuke barrier":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.nukeBarrierMove} for {defCharge} charge(s)!");
                    break;
                }

                case "bang" when p1Charge >= halfCharge:
                {
                    if (p1Charge % 1 == 0)
                    {
                        p1Charge = (int)(p1Charge - halfCharge);
                    }
                    
                    else
                    {
                        p1Charge -= halfCharge;    
                    }
                    Console.WriteLine($"Player 1 used {MoveUsed.bangMove} for {halfCharge} charge(s)!");
                    break;
                }

                case "bang" when p1Charge < halfCharge:
                {
                    Console.WriteLine("You currently do not have enough charges to perform this move.");
                    break;
                }

                        
            }
        }
    }


    public class MoveUsed : Charges
    {
        public const string teleportMove = "teleport";
        public const string vanishMove = "vanish";
        public const string shieldMove = "shield";
        public const string barrierMove = "barrier";
        public const string nukeBarrierMove = "nuke barrier";
        public const string bangMove = "bang";
        public const string dMove = "d";
        public MoveUsed() { }
    }
}
