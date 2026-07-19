using System;

namespace Game
{

    
    public class MainProgram
    {
        static void Main(string[] args)
        {   
            Start start = new();
        }
    }


    public class Start
    {
        public bool invalidInput = true;
        public Start()
        {
            while (true)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Start game? [Y / N]");
                Console.WriteLine("----------------------------");

                try
                {
                    char startInput = Convert.ToChar(Console.ReadLine().ToUpper());
                    
                    if (startInput == 'Y')
                    {
                        Console.WriteLine("Have fun! You are now proceeding to the game's setup.");
                        Gameplay gameStart = new(true);
                        break;
                    }

                    else if (startInput == 'N')
                    {
                        Console.WriteLine("See you again next time!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" when starting the game.");
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" when starting the game.");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{e.Message}");
                }
            }
        }
    }

    public class Gameplay
    {
        // CONDITIONS
        public bool startGame;


        // TRACKERS
        public bool turnEnd = false;
        public int gameTurns = 1;
        public int decisionsTracker = 0;
        public int playerDecisionsTracker = 0;
        public List<Player> players = new();
        public List<string> movesMade = new();
        

        // MOVE NAMES
        public const string chargeMove = "CHARGE";
        public const string teleportMove = "TELEPORT";
        public const string vanishMove = "VANISH";
        public const string shieldMove = "SHIELD";
        public const string barrierMove = "BARRIER";
        public const string nukeBarrierMove = "NUKE BARRIER";
        public const string bangMove = "BANG";
        public const string dMove = "D";

        // MOVE ATTRIBUTES
        public bool vulnerable;
        public bool teleportBreak;
        public bool vanishBreak;
        public bool shieldBreak;
        public bool barrierBreak;


        // CHARGES
        public const float defCharge = 0f;
        public const float halfCharge = 0.5f;
        public const float oneCharge = 1f;
        public const float twoCharge = 2f;
        public const float threeCharge = 3f;
        public const float fourCharge = 4f;


        public Gameplay(bool startGame)
        {
            this.startGame = startGame;
            

            // GAME SETUP
            Console.WriteLine("How many players will be playing?");

            int playerCount;
            while (true)
            {
                try
                {
                    playerCount = Convert.ToInt32(Console.ReadLine());

                    if (playerCount <= 1)
                    {
                        Console.WriteLine("Player count must at least be 1.");
                            continue;
                    }
                    Console.WriteLine($"Current player count: {playerCount}");
                    break;
                }

                catch (Exception e)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was received. Please input an integer.");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine("----------------------------");
                }
            }

            for (int p = 0; p < playerCount; p++)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine($"Enter name for player {p + 1}: ");
                string username =  Console.ReadLine();

                while (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be blank!");
                    username = Console.ReadLine();
                }

                players.Add(new Player(username));
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"Turn {gameTurns}");


            //ACTUAL GAMEPLAY
            while (this.startGame)
            {
                bool validTurnChecker = false;


                // PLAYER DECISION
                foreach (Player player in players)
                {
                    if (player.playerLives <= 0)
                        continue;

                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{player.playerName}, what will be your next move?");
                    Console.WriteLine("----------------------------");
                    string moveInput = Console.ReadLine().ToUpper();
                    bool validTurn = player.TakeTurn(this, moveInput);

                    if (validTurn)
                        validTurnChecker = true;
                        decisionsTracker++;
                        movesMade.Add(moveInput);
                }


                // PLAYER ELIMINATION
                players.RemoveAll(p => p.playerLives <= 0);
                if (players.Count <= 1)
                {
                    this.startGame = false;
                    Console.WriteLine("----------------------------");

                    if (players.Count == 1)
                        Console.WriteLine($"{players[0].playerName} wins the game!");
                    break;
                }


                // END OF A TURN
                if (decisionsTracker == players.Count)
                {
                    turnEnd = true;
                }


                // VALID TURNS
                if (validTurnChecker && turnEnd)
                {
                    gameTurns++;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"Turn {gameTurns}");

                    foreach (Player player in players)
                    {
                        playerDecisionsTracker++;
                        player.EndTurn(this, movesMade[playerDecisionsTracker - 1]);
                    }
                }
            }            
        }
    }


    public class Player
    {
        public string playerName;
        public float playerCharge = 1;
        public int playerLives = 1;

        public Player(string playerName)
        {
            this.playerName = playerName;
            Console.WriteLine($"{playerName} has just entered the game!");
        }

        public bool TakeTurn(Gameplay match, string moveInput)
        {
            switch(moveInput)
            {
                case "CHARGE":
                case "TELEPORT":
                case "VANISH":
                case "SHIELD":
                case "BARRIER":
                case "NUKE BARRIER":
                    return true;

                case "BANG" when playerCharge >= Gameplay.halfCharge:
                {   
                    return true;
                }

                case "BANG" when playerCharge < Gameplay.halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    return false;
                }        

                case "D" when playerCharge >= Gameplay.halfCharge:
                {
                    return true;
                }

                case "D" when playerCharge < Gameplay.halfCharge:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient charges.");
                    Console.WriteLine("----------------------------");
                    return false;
                }  

                default:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was provided. Please try again.");
                    Console.WriteLine("----------------------------");
                    return false;
                }
            }
        }


        public void EndTurn(Gameplay match, string moveInput)
        {

            switch(moveInput)
            {
                case "CHARGE":
                {
                    playerCharge += Gameplay.oneCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.chargeMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "TELEPORT":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.teleportMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "VANISH":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.vanishMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "SHIELD":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.shieldMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.barrierMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "NUKE BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.nukeBarrierMove} for {Gameplay.defCharge} charge(s)!");
                    break;
                }

                case "BANG" when playerCharge >= Gameplay.halfCharge:
                {   
                    playerCharge -= Gameplay.halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.bangMove} for {Gameplay.halfCharge} charge(s)!");
                    break;
                }

                case "D" when playerCharge >= Gameplay.halfCharge:
                {
                    playerCharge -= Gameplay.halfCharge;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used {Gameplay.dMove} for {Gameplay.halfCharge} charge(s)!");
                    break;
                }
            }
        }
    }
}
