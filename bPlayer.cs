using System;
using System.Collections.Generic;

namespace ReloadGame;

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
                Console.WriteLine($"{playerName} forfeited the game!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "CHARGE":
            {
                playerCharge += 1f;
                Console.WriteLine($"{playerName} used CHARGE for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "TELEPORT":
            {
                Console.WriteLine($"{playerName} used TELEPORT for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "VANISH":
            {
                Console.WriteLine($"{playerName} used VANISH for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "SHIELD":
            {
                Console.WriteLine($"{playerName} used SHIELD for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "BARRIER":
            {
                Console.WriteLine($"{playerName} used BARRIER for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "NUKE BARRIER":
            {
                Console.WriteLine($"{playerName} used NUKE BARRIER for 0 charge(s)!");
                new AttackPhase (match, moveInput);
                break;
            }

            case "BANG":
            {   
                playerCharge -= 0.5f;
                Console.WriteLine($"{playerName} used BANG for 0.5 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "MINI-HO":
            {
                playerCharge -= 1f;
                Console.WriteLine($"{playerName} used MINI-HO for 1 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "MINI-HIT":
            {
                playerCharge -= 1f;
                Console.WriteLine($"{playerName} used MINI-HIT for 1 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "DX":
            {
                playerCharge -= 1f;
                Console.WriteLine($"{playerName} used DX for 1 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "BANG-BANG":
            {
                playerCharge -= 1f;
                Console.WriteLine($"{playerName} used BANG-BANG for 1 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "HOMING":
            {
                playerCharge -= 2f;
                Console.WriteLine($"{playerName} used HOMING for 2 charge(s)!");
                AttackPhase begin = new(match, moveInput);
                break;
            }

            case "HIT":
            {
                playerCharge -= 2f;
                Console.WriteLine($"{playerName} used HIT for 2 charge(s)!");
                AttackPhase begin = new(match, moveInput);
                break;
            }

            case "DX-BANG-BANG":
            {
                playerCharge -= 2f;
                Console.WriteLine($"{playerName} used DX-BANG-BANG for 2 charge(s)!");
                AttackPhase begin = new(match, match.targetsDict[this], moveInput);
                break;
            }

            case "A-BOMB":
            {
                playerCharge -= 3f;
                Console.WriteLine($"{playerName} used A-BOMB for 3 charge(s)!");
                AttackPhase begin = new(match, moveInput);
                break;
            }

            case "HIT-HOMING":
            {
                playerCharge -= 4f;
                Console.WriteLine($"{playerName} used HIT-HOMING for 4 charge(s)!");
                AttackPhase begin = new(match, moveInput);
                break;
            }

            case "NUKE":
            {
                playerCharge -= 4f;
                Console.WriteLine($"{playerName} used NUKE for 4 charge(s)!");
                AttackPhase begin = new(match, moveInput);
                break;
            }
        }
    }
}
