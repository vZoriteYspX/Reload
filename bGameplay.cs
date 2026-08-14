using System;
using System.Collections.Generic;

namespace ReloadGame;

public class Gameplay
{
    // CONDITIONS
    public bool startGame;


    // TRACKERS
    public bool turnEnd = false;
    public int gameTurns = 1;
    public int decisionsTracker;
    public int playerMovesTracker;
    public List<string> movesMade = new();
    public List<Player> players = new();
    public List<Player> eliminated = new();
    public Dictionary<Player, Player> targetsDict = new();
    public Dictionary<Player, string> playerForfeit = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesDefDict = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesHalfDict = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesOneDict = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesTwoDict = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesThreeDict = new Dictionary<Player, string>();
    public Dictionary<Player, string> attributesFourDict = new Dictionary<Player, string>();


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
            movesMade.Clear();
            targetsDict.Clear();
            playerForfeit.Clear();
            attributesDefDict.Clear();
            attributesHalfDict.Clear();
            attributesOneDict.Clear();
            attributesTwoDict.Clear();
            attributesThreeDict.Clear();
            attributesFourDict.Clear();

            decisionsTracker = 0;
            playerMovesTracker = 0;
            
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Turn {gameTurns}");

            // PLAYER DECISION
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
                }

                catch (Exception e)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was received. Please input a valid move.");
                }
            }


            // END OF A TURN
            if (decisionsTracker == players.Count)
                turnEnd = true;


            // VALID TURNS
            if (turnEnd)
            {
                gameTurns++;
                
                Console.WriteLine("----------------------------");
                foreach (Player player in players)
                {
                    player.EndTurn(this, movesMade[playerMovesTracker]);
                    playerMovesTracker++;
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
                        Console.WriteLine("Have fun! You are now proceeding to the game's setup.");
                        Console.WriteLine("----------------------------");

                        try
                        {
                            Gameplay gameStart = new(true);
                        }
                            
                        catch
                        {
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("Oops, something went wrong with the game.");
                            Console.WriteLine("----------------------------");   
                        }

                        break;
                    }

                    else if (startInput == 'N')
                    {   
                        Console.WriteLine("All right, see you again next time!");
                        break;
                    }
                        
                    else
                    {
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" to restart the game.");
                        Console.WriteLine("----------------------------");
                    }
                }

                break;
            }
                    
            if (players.Count <= 1)
            {
                this.startGame = false;
                Console.WriteLine("No one won!");
                break;
            }
        }    
    }
}
