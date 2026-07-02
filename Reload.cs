using System;

namespace Game
{

    
    public class MainProgram
    {
        public bool startGame;
        public bool turnCondition;
        public int gameTurns = 1;
        static void Main(string[] args)
        {
            Console.WriteLine("Start game? [YES / NO]");
            string userInput0 = Console.ReadLine().ToUpper();
            if (userInput0 == "YES")
            {
                Gameplay gameStart = new(true);
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


    public class Gameplay : MainProgram
    {
        public bool turnStart;
        public int player1Lives = 1;
        public int player2Lives = 1;
        public bool successfulDef;
        public Gameplay(bool startGame)
        {
            this.startGame = startGame;
            Player1 player1 = new();
            Player2 player2 = new();
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Turn {gameTurns}");
            while (this.startGame)
            {
                turnStart = true;
                Console.WriteLine("Player 1, what will be your next move?");
                Console.WriteLine("----------------------------");
                string userInput1 = Console.ReadLine().ToUpper();
                Console.WriteLine("Player 2, what will be your next move?");
                Console.WriteLine("----------------------------");
                string userInput2 = Console.ReadLine().ToUpper();
                bool validTurnChecker1 = player1.TakeTurn(userInput1, this);
                bool validTurnChecker2 = player2.TakeTurn(userInput2, this);
                if (validTurnChecker1 && validTurnChecker2)
                {
                    gameTurns++;
                    Console.WriteLine($"----------------------------");
                    Console.WriteLine($"Round {gameTurns}");
                }
            }
        }
    }


    public class MoveUsed : Charges
    {
        public const string chargeMove = "CHARGE";
        public const string teleportMove = "TELEPORT";
        public const string vanishMove = "VANISH";
        public const string shieldMove = "SHIELD";
        public const string barrierMove = "BARRIER";
        public const string nukeBarrierMove = "NUKE BARRIER";
        public const string bangMove = "BANG";
        public const string dMove = "D";
    }


    public class Player1 : Charges
    {
        public bool TakeTurn(string userInput1, Gameplay match)
        {
            switch(userInput1)
            {
                case "CHARGE":
                {
                    p1Charge += 1;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.chargeMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "TELEPORT":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.teleportMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "VANISH":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.vanishMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "SHIELD":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.shieldMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.barrierMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "NUKE BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.nukeBarrierMove} for {defCharge} charge(s)!");
                    return true;
                }

                case "BANG" when p1Charge >= halfCharge:
                {   
                    p1Charge = p1Charge - halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.bangMove} for {halfCharge} charge(s)!");
                    return true;
                }

                case "BANG" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    return false;
                }        

                case "D" when p1Charge >= halfCharge:
                {
                    p1Charge = p1Charge - halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 1 used {MoveUsed.dMove} for {halfCharge} charge(s)!");
                    return true;
                }

                case "D" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    return false;
                }  

                default:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was provided.");
                    Console.WriteLine("----------------------------");
                    return false;
                }
            }
        }
    }


    public class Player2 : Charges
    {
        public bool TakeTurn(string userInput2, Gameplay match)
        {
            switch(userInput2)
            {
                case "CHARGE":
                {
                    p1Charge += 1;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.chargeMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "TELEPORT":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.teleportMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "VANISH":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.vanishMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "SHIELD":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.shieldMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.barrierMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "NUKE BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.nukeBarrierMove} for {defCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "BANG" when p1Charge >= halfCharge:
                {   
                    p1Charge = p1Charge - halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.bangMove} for {halfCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "BANG" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    return false;
                }        

                case "D" when p1Charge >= halfCharge:
                {
                    p1Charge = p1Charge - halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Player 2 used {MoveUsed.dMove} for {halfCharge} charge(s)!");
                    match.turnStart = false;
                    return true;
                }

                case "D" when p1Charge < halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    return false;
                }  

                default:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was provided.");
                    Console.WriteLine("----------------------------");
                    match.turnStart = false;
                    return false;
                }
            }
        }
    }
}
