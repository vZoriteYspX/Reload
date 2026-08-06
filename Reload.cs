using System;
using System.Collections.Generic;

namespace ReloadGame
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
        public Start()
        {
            while (true)
            {
                char startInput;

                Console.WriteLine("----------------------------");
                Console.WriteLine("Start game? [Y / N]");

                try
                {
                    startInput = Convert.ToChar((Console.ReadLine() ?? "").ToUpper());
                }

                catch (Exception e)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" when starting the game.");
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
                    Console.WriteLine("See you again next time!");
                    break;
                }
                    
                else
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" when starting the game.");
                    Console.WriteLine("----------------------------");
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
        public int playerMovesTracker = 0;
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
                    Console.WriteLine("An invalid input was received. Please input an integer.");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine("----------------------------");
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
                    Console.WriteLine("Username cannot be blank!");
                    username = Console.ReadLine();
                }

                players.Add(new Player(username));
            }


            // TURN 1 PRINT
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Turn {gameTurns}");


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
                        Console.WriteLine("An invalid input was received. Please input a valid move.");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"{e.Message}");
                        Console.WriteLine("----------------------------");  
                    }
                }


                // END OF A TURN
                if (decisionsTracker == players.Count)
                    turnEnd = true;


                // VALID TURNS
                if (turnEnd)
                {
                    gameTurns++;
                    
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
                    break;
                }
                        
                if (players.Count <= 1)
                {
                    this.startGame = false;
                    Console.WriteLine("No one won!");
                    break;
                }

                Console.WriteLine("----------------------------");
                Console.WriteLine($"Turn {gameTurns}");

            }    
        }
    }


    public class Player
    {
        public string playerName;
        public float playerCharge = 1;

        public Player(string playerName)
        {
            this.playerName = playerName;

            Console.WriteLine($"{playerName} has just entered the game!");
        }


        //  CHECKS MOVE INPUT
        public bool TakeTurn(Gameplay match, string moveInput)
        {
            switch (moveInput) 
            {
                case "FORFEIT":
                {
                    match.playerForfeit[this] = "FORFEIT";
                    return true;
                }

                case "CHARGE":
                {
                    match.attributesDefDict[this] = "CHARGE";
                    return true;
                }

                case "TELEPORT":
                {
                    match.attributesDefDict[this] = "TELEPORT";
                    return true;
                }

                case "VANISH":
                {
                    match.attributesDefDict[this] = "VANISH";
                    return true;
                }

                case "SHIELD":
                {
                    match.attributesDefDict[this] = "SHIELD";
                    return true;
                }

                case "BARRIER":
                {
                    match.attributesDefDict[this] = "BARRIER";
                    return true;
                }

                case "NUKE BARRIER":
                {
                    match.attributesDefDict[this] = "NUKE BARRIER";
                    return true;
                }

                case "BANG" when playerCharge >= 0.5f:
                {
                    return HalfChargeAttack(match, "BANG", moveInput);
                }      

                case "MINI-HO" when playerCharge >= 1f:
                    return OneChargeAttack(match, "MINI-HO", moveInput);

                case "MINI-HIT" when playerCharge >= 1f:
                    return OneChargeAttack(match, "MINI-HIT", moveInput);

                case "DX" when playerCharge >= 1f:
                    return OneChargeAttack(match, "DX", moveInput);

                case "BANG-BANG" when playerCharge >= 1f:
                    return OneChargeAttack(match, "BANG-BANG", moveInput);

                case "HOMING" when playerCharge >= 2f:
                {
                    match.attributesTwoDict[this] = "HOMING";
                    return true;
                }

                case "HIT" when playerCharge >= 2f:
                {
                    match.attributesTwoDict[this] = "HIT";
                    return true;
                }

                case "DX-BANG-BANG" when playerCharge >= 2f:
                {
                    match.attributesTwoDict[this] = "DX-BANG-BANG";
                    return TwoChargeAttack(match, "DX-BANG-BANG", moveInput);
                }

                case "A-BOMB" when playerCharge >= 3f:
                {
                    match.attributesThreeDict[this] = "A-BOMB";
                    return true;
                }

                case "HIT-HOMING" when playerCharge >= 4f:
                {
                    match.attributesFourDict[this] = "HIT-HOMING";
                    return true;
                }

                case "NUKE" when playerCharge >= 4f:
                {
                    match.attributesFourDict[this] = "NUKE";
                    return true;
                }

                default:
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Insufficient Charges / Invalid Input");
                    Console.WriteLine("Please try again.");
                    Console.WriteLine("----------------------------");
                    return false;
                }
            }
        }


        //  HALF CHARGE VALIDITY
        private bool HalfChargeAttack(Gameplay match, string halfCharge, string moveInput)
        {
            match.attributesHalfDict[this] = halfCharge;

            try
            {
                Console.WriteLine("Please input your target's username.");
                string? target = Convert.ToString(Console.ReadLine());

                while (string.IsNullOrWhiteSpace(target))
                {
                    Console.WriteLine("Please input your target's username.");
                    target =  Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Your target cannot be blank!");
                }
            
                Player? validTarget = match.players.Find(p => p.playerName == target);

                if (validTarget == null)
                {
                    Console.WriteLine("An invalid input was entered. Please input a player's username.");
                    return false;
                }

                if (validTarget == this)
                {
                    Console.WriteLine("You cannot attack yourself! Please select a valid player!");
                    return false;
                }

                match.targetsDict[this] = validTarget;

                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was entered. Please input a player's username.");
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{e.Message}");
                return false;
            }
        }


        //  ONE CHARGE VALIDITY
        private bool OneChargeAttack(Gameplay match, string oneCharge, string moveInput)
        {
            match.attributesOneDict[this] = oneCharge;

            try
            {
                Console.WriteLine("Please input your target's username.");
                string? target = Convert.ToString(Console.ReadLine());

                while (string.IsNullOrWhiteSpace(target))
                {
                    Console.WriteLine("Please input your target's username.");
                    target =  Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Your target cannot be blank!");
                }

                Player? validTarget = match.players.Find(p => p.playerName == target);

                if (validTarget == null)
                {
                    Console.WriteLine("An invalid input was entered. Please input a player's username.");
                    return false;
                }

                if (validTarget == this)
                {
                    Console.WriteLine("You cannot attack yourself! Please select a valid player!");
                    return false;
                }

                match.targetsDict[this] = validTarget;
                
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was entered. Please input a player's username.");
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{e.Message}");
                return false;
            }
        }


        //  TWO CHARGE VALIDITY
        private bool TwoChargeAttack(Gameplay match, string twoCharge, string moveInput)
        {
            match.attributesTwoDict[this] = twoCharge;

            try
            {
                Console.WriteLine("Please input your target's username.");
                string? target = Convert.ToString(Console.ReadLine());

                while (string.IsNullOrWhiteSpace(target))
                {
                    Console.WriteLine("Please input your target's username.");
                    target =  Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Your target cannot be blank!");
                }

                Player? validTarget = match.players.Find(p => p.playerName == target);

                if (validTarget == null)
                {
                    Console.WriteLine("An invalid input was entered. Please input a player's username.");
                    return false;
                }

                if (validTarget == this)
                {
                    Console.WriteLine("You cannot attack yourself! Please select a valid player!");
                    return false;
                }

                match.targetsDict[this] = validTarget;
                
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was entered. Please input a player's username.");
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{e.Message}");
                return false;
            }
        }


        //  END OF TURN
        public void EndTurn(Gameplay match, string moveInput)
        {

            switch(moveInput)
            {   
                case "FORFEIT":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} forfeited the game!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "CHARGE":
                {
                    playerCharge += 1f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used CHARGE for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "TELEPORT":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used TELEPORT for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "VANISH":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used VANISH for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "SHIELD":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used SHIELD for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used BARRIER for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "NUKE BARRIER":
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used NUKE BARRIER for 0 charge(s)!");
                    new AttackPhase (match, moveInput);
                    break;
                }

                case "BANG":
                {   
                    playerCharge -= 0.5f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used BANG for 0.5 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "MINI-HO":
                {
                    playerCharge -= 1f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used MINI-HO for 1 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "MINI-HIT":
                {
                    playerCharge -= 1f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used MINI-HIT for 1 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "DX":
                {
                    playerCharge -= 1f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used DX for 1 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "BANG-BANG":
                {
                    playerCharge -= 1f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used BANG-BANG for 1 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "HOMING":
                {
                    playerCharge -= 2f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used HOMING for 2 charge(s)!");
                    AttackPhase begin = new(match, moveInput);
                    break;
                }

                case "HIT":
                {
                    playerCharge -= 2f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used HIT for 2 charge(s)!");
                    AttackPhase begin = new(match, moveInput);
                    break;
                }

                case "DX-BANG-BANG":
                {
                    playerCharge -= 2f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used DX-BANG-BANG for 2 charge(s)!");
                    AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                    break;
                }

                case "A-BOMB":
                {
                    playerCharge -= 3f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used A-BOMB for 3 charge(s)!");
                    AttackPhase begin = new(match, moveInput);
                    break;
                }

                case "HIT-HOMING":
                {
                    playerCharge -= 4f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used HIT-HOMING for 4 charge(s)!");
                    AttackPhase begin = new(match, moveInput);
                    break;
                }

                case "NUKE":
                {
                    playerCharge -= 4f;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"{playerName} used NUKE for 4 charge(s)!");
                    AttackPhase begin = new(match, moveInput);
                    break;
                }
            }
        }
    }


    public class AttackPhase
    {
        public Gameplay match;
        public Player? target;
        public string move;
        public AttackPhase(Gameplay match, string move) : this(match, null, move) { }


        //  MOVE PROCESSING
        public AttackPhase(Gameplay match, Player? target, string move)
        {   
            this.match = match;
            this.target = target;
            this.move = move;
            Player? targetPlayer = target;
            bool targetMoves = move is "DX-BANG-BANG" or "MINI-HO" or "MINI-HIT" or "DX" or "BANG-BANG" or "BANG";

            if (targetMoves && target is null)
                throw new InvalidOperationException($"'{move}' requires a target, but none was provided.");

            foreach (Player usernameFour in match.attributesFourDict.Keys)
            {
                switch (move)
                {
                    case "HIT-HOMING":
                    {   
                        foreach (Player usernameDef in match.attributesDefDict.Keys)
                        {   
                            if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defHitHoming) && defHitHoming == "TELEPORT")
                                match.attributesFourDict[usernameFour] = "ELIMINATED";

                            else
                                match.eliminated.Add(usernameDef);
                        }

                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        foreach (Player usernameOne in match.attributesOneDict.Keys)
                            match.eliminated.Add(usernameOne);

                        foreach (Player usernameTwo in match.attributesTwoDict.Keys)
                            match.eliminated.Add(usernameTwo);
                            
                        foreach (Player usernameThree in match.attributesThreeDict.Keys)
                            match.eliminated.Add(usernameThree);

                        break;
                    }

                    case "NUKE":
                    {
                        foreach (Player usernameDef in match.attributesDefDict.Keys)
                        {   
                            if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defNuke) && defNuke != "NUKE BARRIER")
                                match.eliminated.Add(usernameDef);
                        }
                                
                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        foreach (Player usernameOne in match.attributesOneDict.Keys)
                            match.eliminated.Add(usernameOne);

                        foreach (Player usernameTwo in match.attributesTwoDict.Keys)
                            match.eliminated.Add(usernameTwo);
                            
                        foreach (Player usernameThree in match.attributesThreeDict.Keys)
                            match.eliminated.Add(usernameThree);

                        break;
                    }
                }
            }

            foreach (Player usernameThree in match.attributesThreeDict.Keys)
            {
                switch (move)
                {
                    case "A-BOMB":
                    {
                        foreach (Player usernameDef in match.attributesDefDict.Keys)
                        {   
                            if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defABomb) && defABomb != "VANISH")
                                match.eliminated.Add(usernameDef);
                        }

                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        foreach (Player usernameOne in match.attributesOneDict.Keys)
                            match.eliminated.Add(usernameOne);

                        foreach (Player usernameTwo in match.attributesTwoDict.Keys)
                            match.eliminated.Add(usernameTwo);

                        break;
                    }
                }
            }

            foreach (Player usernameTwo in match.attributesTwoDict.Keys)
            {
                switch (move)
                {
                    case "HOMING":
                    {
                        foreach (Player usernameDef in match.attributesDefDict.Keys)
                        {   
                            if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defHoming) && defHoming == "TELEPORT")
                                match.eliminated.Add(usernameDef);

                            else
                                continue;
                        }

                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        foreach (Player usernameOne in match.attributesOneDict.Keys)
                            match.eliminated.Add(usernameOne);

                        break;
                    }

                    case "HIT":
                    {
                        foreach (Player usernameDef in match.attributesDefDict.Keys)
                        {   
                            if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defHit1) && defHit1 == "TELEPORT")
                                match.attributesTwoDict[usernameTwo] = "ELIMINATED";

                            else if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defHit2) && defHit2 == "VANISH")
                                match.eliminated.Add(usernameDef);

                            else
                                continue;
                        }

                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        foreach (Player usernameOne in match.attributesOneDict.Keys)
                            match.eliminated.Add(usernameOne);

                        break;
                    }
                }
            }

            foreach (Player usernameTwo in match.attributesTwoDict.Keys)
            {
                switch (move, targetPlayer)
                {
                    case ("DX-BANG-BANG", _):
                    {
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defDXBangBang) && (defDXBangBang == "CHARGE" || defDXBangBang == "SHIELD" || defDXBangBang == "BARRIER" || defDXBangBang == "NUKE BARRIER" || defDXBangBang == "BANG" || defDXBangBang == "MINI-HO" || defDXBangBang == "MINI-HIT" || defDXBangBang == "DX" || defDXBangBang == "BANG-BANG"))
                            match.eliminated.Add(targetPlayer!);

                        else
                            continue;

                        break;
                    }
                }
            }

            foreach (Player usernameOne in match.attributesOneDict.Keys)
            {           
                switch (move, targetPlayer)
                {
                    case ("MINI-HO", _):
                    {
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defMiniHo) && (defMiniHo == "CHARGE" || defMiniHo == "TELEPORT" || defMiniHo == "NUKE BARRIER"))
                            match.eliminated.Add(targetPlayer!);

                        else
                            continue;
                        
                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        break;
                    }

                    case ("MINI-HIT", _):
                    {
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defMiniHit1) && defMiniHit1 == "TELEPORT")
                            match.attributesOneDict[usernameOne] = "ELIMINATED";

                        else if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defMiniHit2) && (defMiniHit2 == "CHARGE" || defMiniHit2 == "VANISH" || defMiniHit2 == "NUKE BARRIER"))
                            match.eliminated.Add(targetPlayer!);

                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        break;
                    }

                    case ("DX", _):
                    {
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defDX) && (defDX == "CHARGE" || defDX == "SHIELD" || defDX == "NUKE BARRIER"))
                            match.eliminated.Add(targetPlayer!);
                        
                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        break;
                    }
                    
                    case ("BANG-BANG", _):
                    {
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defBangBang) && (defBangBang == "CHARGE" || defBangBang == "BARRIER" || defBangBang == "NUKE BARRIER"))
                            match.eliminated.Add(targetPlayer!);
                        
                        foreach (Player usernameHalf in match.attributesHalfDict.Keys)
                            match.eliminated.Add(usernameHalf);
                        
                        break;
                    }
                }
            }

            foreach (Player usernameHalf in match.attributesHalfDict.Keys)
            {  
                switch(move, targetPlayer!)
                {
                    case ("BANG", _):
                        if (match.attributesDefDict.TryGetValue(targetPlayer!, out var defBang) && (defBang == "CHARGE" || defBang == "BARRIER" || defBang == "NUKE BARRIER"))
                            match.eliminated.Add(targetPlayer!);
                    
                    break;
                }             
            }
        }


        // ELIMINATION PROPER
        public static void Elimination(Gameplay match)
        {
            foreach (Player usernameForfeit in match.playerForfeit.Keys)
                    match.players.Remove(usernameForfeit);
            
            match.playerForfeit.Clear();
                
            foreach (Player usernameDef in match.attributesDefDict.Keys)
            {
                if (match.attributesDefDict[usernameDef] == "ELIMINATED")
                    match.players.Remove(usernameDef);
            }

            match.attributesDefDict.Clear();

            foreach (Player usernameHalf in match.attributesHalfDict.Keys)
            {
                if (match.attributesHalfDict[usernameHalf] == "ELIMINATED")
                    match.players.Remove(usernameHalf);
                
            }

            match.attributesHalfDict.Clear();

            foreach (Player usernameOne in match.attributesOneDict.Keys)
            {
                if (match.attributesOneDict[usernameOne] == "ELIMINATED")
                    match.players.Remove(usernameOne);                
            }

            match.attributesOneDict.Clear();

            foreach (Player usernameTwo in match.attributesTwoDict.Keys)
            {
                if (match.attributesTwoDict[usernameTwo] == "ELIMINATED")
                    match.players.Remove(usernameTwo);
            }

            match.attributesTwoDict.Clear();
            match.attributesThreeDict.Clear();

            foreach (Player usernameFour in match.attributesFourDict.Keys)
            {
                if (match.attributesFourDict[usernameFour] == "ELIMINATED")
                    match.players.Remove(usernameFour);
            }

            match.attributesFourDict.Clear();

            foreach (Player eliminatedPlayer in match.eliminated)
                match.players.Remove(eliminatedPlayer);
            
            match.eliminated.Clear();
        }
    }
}
