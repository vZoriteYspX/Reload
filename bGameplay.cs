using System;
using System.Collections.Generic;

namespace ReloadGame;

public class Gameplay
{
    // CONDITIONS
    public bool startGame;


    // TRACKERS
    public bool turnEnd = false;
    //  Confirms that the turn has ended

    public int gameTurns = 1;
    //  Keeps track of the game's turns
    public int decisionsTracker;
    //  Checks if every player has made their move

    public int movesPrint;
    //  for loop substitute

    public List<string> movesMade = new();
    //  The valid moves that each player has made is stored here

    public List<Player> players = new();
    //  Players that are currently active are listed here

    public List<Player> eliminated = new();
    //  Players that will be eliminated are stored here for elimination

    public Dictionary<Player, Player> targetsDict = new();
    //  Players that are being targeted by another player goes here

    public Dictionary<Player, string> playerForfeit = new Dictionary<Player, string>();
    //  Players who have forfeited the game goes here

    public Dictionary<Player, string> defChDict = new Dictionary<Player, string>();
    //  Defensive moves made by a player goes here

    public Dictionary<Player, string> halfChDict = new Dictionary<Player, string>();
    //  Half-charge moves made by a player goes here

    public Dictionary<Player, string> oneChDict = new Dictionary<Player, string>();
    //  One-charge moves made by a player goes here

    public Dictionary<Player, string> twoChDict = new Dictionary<Player, string>();
    //  Two-charge moves made by a player goes here

    public Dictionary<Player, string> threeChDict = new Dictionary<Player, string>();
    //  Three-charge moves made by a player goes here
    
    public Dictionary<Player, string> fourChDict = new Dictionary<Player, string>();
    //  Four-charge moves made by a player goes here


    public Gameplay(bool startGame)
    {
        this.startGame = startGame;
        int playerCount;
        

        // PLAYER COUNT INPUT
        Console.WriteLine("How many players will be playing?");
        while (true)
        {
            try
            {
                playerCount = Convert.ToInt32(Console.ReadLine());

                if (playerCount < 1)
                {
                    Console.WriteLine("Player count must at least be 1.");
                        continue;
                }

                Console.WriteLine($"Players playing in this session: {playerCount} Players");
                break;
            }

            catch (Exception e)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{e.Message}");
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was received. Please input an integer.");
            }
        }


        //  PLAYER USERNAME INPUT
        for (int p = 0; p < playerCount; p++)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Enter name for player {p + 1}: ");
            string? username =  Convert.ToString(Console.ReadLine());

            while (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be blank! Please enter a valid name.");
                username = Console.ReadLine();
            }

            players.Add(new Player(username));
        }


        //  ACTUAL GAMEPLAY
        while (this.startGame)
        {
            //  RESETS DICTIONARIES AND TRACKERS
            movesMade.Clear();
            targetsDict.Clear();
            playerForfeit.Clear();
            defChDict.Clear();
            halfChDict.Clear();
            oneChDict.Clear();
            twoChDict.Clear();
            threeChDict.Clear();
            fourChDict.Clear();
            
            decisionsTracker = 0;
            movesPrint = 0;
            
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Turn {gameTurns}");

            // PLAYER DECISION
            while(true)
            {
                foreach (Player player in players)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{player.playerName}, what will be your move?");
                    
                    try
                    {
                        string moveInput = (Console.ReadLine() ?? "").ToUpper();
                        bool validTurn = player.TakeTurn(this, moveInput);
                        
                        if (validTurn)
                        {
                            decisionsTracker++;
                            movesMade.Add(moveInput);
                        }
                        
                        //  INVALID TURNS RESTARTS THE TURN, MAKES EVERYONE PISSED TOO
                        else
                            break;
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"{e.Message}");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("An invalid input was received. Please input a valid move.");
                    }
                }

                // ENDS TURN
                if (decisionsTracker == players.Count)
                {
                    turnEnd = true;
                    break;
                }
            }


            // VALID TURN
            if (turnEnd)
            {
                gameTurns++;
                
                Console.WriteLine("----------------------------");
                foreach (Player player in players)
                {
                    player.EndTurn(this, movesMade[movesPrint]);
                    movesPrint++;
                }
                
                AttackPhase.Elimination(this);
                turnEnd = false;
            }


            // GAME END
            if (players.Count == 1)
            {
                this.startGame = false;
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{players[0].playerName} wins the game!");


                //  REMATCHES
                while (true)
                {
                    char startInput;

                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Would you like to play another game? [Y / N]");

                    try
                    {
                        startInput = Convert.ToChar((Console.ReadLine() ?? "").ToUpper());
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" to restart the game.");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"{e.Message}");
                        continue;
                    }

                    if (startInput == 'Y')
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("Have fun! You are now proceeding to the game's setup.");

                        try
                        {
                            Gameplay gameStart = new(true);
                        }
                            
                        catch
                        {
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("Oops, something went wrong with the game.");
                        }

                        break;
                    }

                    else if (startInput == 'N')
                    {   
                        Console.WriteLine("All right, see you again next time!");
                        Console.WriteLine("----------------------------");
                        break;
                    }
                        
                    else
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" to restart the game.");
                    }
                }

                break;
            }
            

            //  FAILSAFE IN CASE NO ONE WINS
            if (players.Count <= 1)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("No one won!");
                break;
            }
        }    
    }
}
