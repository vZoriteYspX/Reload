using System;

namespace Game
{

    
    public class MainProgram
    {
        public bool startGame;
        public bool turnCondition;
        public int gameTurns = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Start game?");
            string userInput0 = Console.ReadLine().ToLower();
            if (userInput0 == "yes")
            {
                MatchProgress gameStart = new(true);
            }
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


    public class MatchProgress : MainProgram
    {
        public bool turnStart;
        public MatchProgress(bool startGame)
        {
            Player1 player1 = new Player1();
            while (startGame)
            {
                turnStart = true;
                Console.WriteLine("----------------------------");
                Console.WriteLine("What will be your next move?");
                Console.WriteLine("----------------------------");
                string userInput1 = Console.ReadLine().ToLower();
                gameTurns++;
                player1.TakeTurn(userInput1, this);
            }
        }
    }


    public class MoveUsed : Charges
    {
        public const string chargeMove = "charge";
        public const string teleportMove = "teleport";
        public const string vanishMove = "vanish";
        public const string shieldMove = "shield";
        public const string barrierMove = "barrier";
        public const string nukeBarrierMove = "nuke barrier";
        public const string bangMove = "bang";
        public const string dMove = "d";
    }


    public class Player1 : Charges
    {
        public void TakeTurn(string userInput1, MatchProgress match)
        {
            switch(userInput1)
            {
                case "charge":
                {
                    p1Charge += 1;
                    Console.WriteLine($"Player 1 used {MoveUsed.chargeMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "teleport":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.teleportMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "vanish":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.vanishMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "shield":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.shieldMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "barrier":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.barrierMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "nuke barrier":
                {
                    Console.WriteLine($"Player 1 used {MoveUsed.nukeBarrierMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "bang" when p1Charge >= halfCharge:
                {
                    if (p1Charge % 1 == 0)
                    {
                        p1Charge = p1Charge - halfCharge;
                    }
                    
                    else
                    {
                        p1Charge -= halfCharge;    
                    }
                    Console.WriteLine($"Player 1 used {MoveUsed.bangMove} for {halfCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "bang" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("You currently do not have enough charges to perform this move.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    break;
                }        

                case "d" when p1Charge >= halfCharge:
                {
                    if (p1Charge % 1 == 0)
                    {
                        p1Charge = (int)(p1Charge - halfCharge);
                    }
                    
                    else
                    {
                        p1Charge -= halfCharge;    
                    }
                    Console.WriteLine($"Player 1 used {MoveUsed.dMove} for {halfCharge} charge(s)!");
                    match.turnStart = false;
                    break;
                }

                case "d" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("You currently do not have enough charges to perform this move.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    break;
                }  

                default:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was provided.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    break;
                }
            }
        }
    }
}
