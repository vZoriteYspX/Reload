using System;
using System.Collections.Generic;

namespace ReloadGame;

public class AttackPhase
{
    public Gameplay match;
    public Player? target;
    public string move;


    //  MOVE PROCESSING (TARGETLESS MOVES)
    public AttackPhase(Gameplay match, string move) : this(match, null, move) { }


    //  MOVE PROCESSING (MOVES WITH TARGETING)
    public AttackPhase(Gameplay match, Player? target, string move)
    {   
        this.match = match;
        this.target = target;
        this.move = move;
        Player? targetPlayer = target;
        bool targetMoves = move is "DX-BANG-BANG" or "MINI-HO" or "MINI-HIT" or "DX" or "BANG-BANG" or "BANG";

        if (targetMoves && target is null)
            throw new InvalidOperationException($"'{move}' requires a target, but none was provided.");

        foreach (Player usernameFour in match.fourChDict.Keys)
        {
            switch (move)
            {
                case "HIT-HOMING":
                {   
                    foreach (Player usernameDef in match.defChDict.Keys)
                    {   
                        if (match.defChDict[usernameDef] == "TELEPORT")
                            match.fourChDict[usernameFour] = "ELIMINATED";

                        else
                            match.eliminated.Add(usernameDef);
                    }

                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    foreach (Player usernameOne in match.oneChDict.Keys)
                        match.eliminated.Add(usernameOne);

                    foreach (Player usernameTwo in match.twoChDict.Keys)
                        match.eliminated.Add(usernameTwo);
                        
                    foreach (Player usernameThree in match.threeChDict.Keys)
                        match.eliminated.Add(usernameThree);

                    break;
                }

                case "NUKE":
                {
                    foreach (Player usernameDef in match.defChDict.Keys)
                    {   
                        if (match.defChDict[usernameDef] != "NUKE BARRIER")
                            match.eliminated.Add(usernameDef);
                    }
                            
                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    foreach (Player usernameOne in match.oneChDict.Keys)
                        match.eliminated.Add(usernameOne);

                    foreach (Player usernameTwo in match.twoChDict.Keys)
                        match.eliminated.Add(usernameTwo);
                        
                    foreach (Player usernameThree in match.threeChDict.Keys)
                        match.eliminated.Add(usernameThree);

                    break;
                }
            }
        }

        foreach (Player usernameThree in match.threeChDict.Keys)
        {
            switch (move)
            {
                case "A-BOMB":
                {
                    foreach (Player usernameDef in match.defChDict.Keys)
                    {   
                        if (match.defChDict[usernameDef] != "VANISH")
                            match.eliminated.Add(usernameDef);
                    }

                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    foreach (Player usernameOne in match.oneChDict.Keys)
                        match.eliminated.Add(usernameOne);

                    foreach (Player usernameTwo in match.twoChDict.Keys)
                        match.eliminated.Add(usernameTwo);

                    break;
                }
            }
        }

        foreach (Player usernameTwo in match.twoChDict.Keys)
        {
            switch (move)
            {
                case "HOMING":
                {
                    foreach (Player usernameDef in match.defChDict.Keys)
                    {   
                        if (match.defChDict[usernameDef] != "VANISH" && match.defChDict[usernameDef] != "SHIELD" && match.defChDict[usernameDef] != "BARRIER")
                            match.eliminated.Add(usernameDef);
                    }

                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    foreach (Player usernameOne in match.oneChDict.Keys)
                        match.eliminated.Add(usernameOne);

                    break;
                }

                case "HIT":
                {
                    foreach (Player usernameDef in match.defChDict.Keys)
                    {   
                        if (match.defChDict[usernameDef] == "TELEPORT")
                            match.twoChDict[usernameTwo] = "ELIMINATED";

                        else if (match.defChDict[usernameDef] != "SHIELD" && match.defChDict[usernameDef] != "BARRIER")
                            match.eliminated.Add(usernameDef);
                    }

                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    foreach (Player usernameOne in match.oneChDict.Keys)
                        match.eliminated.Add(usernameOne);

                    break;
                }
            }
        }

        foreach (Player usernameTwo in match.twoChDict.Keys)
        {
            switch (move, targetPlayer)
            {
                case ("DX-BANG-BANG", _):
                {
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defDXBangBang) && (defDXBangBang == "CHARGE" || defDXBangBang == "SHIELD" || defDXBangBang == "BARRIER" || defDXBangBang == "NUKE BARRIER" || defDXBangBang == "BANG" || defDXBangBang == "MINI-HO" || defDXBangBang == "MINI-HIT" || defDXBangBang == "DX" || defDXBangBang == "BANG-BANG"))
                        match.eliminated.Add(targetPlayer!);

                    break;
                }
            }
        }

        foreach (Player usernameOne in match.oneChDict.Keys)
        {           
            switch (move, targetPlayer)
            {
                case ("MINI-HO", _):
                {
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defMiniHo) && (defMiniHo == "CHARGE" || defMiniHo == "TELEPORT" || defMiniHo == "NUKE BARRIER"))
                        match.eliminated.Add(targetPlayer!);

                    
                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    break;
                }

                case ("MINI-HIT", _):
                {
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defMiniHit1) && defMiniHit1 == "TELEPORT")
                        match.oneChDict[usernameOne] = "ELIMINATED";

                    else if (match.defChDict.TryGetValue(targetPlayer!, out var defMiniHit2) && (defMiniHit2 == "CHARGE" || defMiniHit2 == "VANISH" || defMiniHit2 == "NUKE BARRIER"))
                        match.eliminated.Add(targetPlayer!);

                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    break;
                }

                case ("DX", _):
                {
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defDX) && (defDX == "CHARGE" || defDX == "SHIELD" || defDX == "NUKE BARRIER"))
                        match.eliminated.Add(targetPlayer!);
                    
                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    break;
                }
                
                case ("BANG-BANG", _):
                {
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defBangBang) && (defBangBang == "CHARGE" || defBangBang == "BARRIER" || defBangBang == "NUKE BARRIER"))
                        match.eliminated.Add(targetPlayer!);
                    
                    foreach (Player usernameHalf in match.halfChDict.Keys)
                        match.eliminated.Add(usernameHalf);
                    
                    break;
                }
            }
        }

        foreach (Player usernameHalf in match.halfChDict.Keys)
        {  
            switch(move, targetPlayer!)
            {
                case ("BANG", _):
                    if (match.defChDict.TryGetValue(targetPlayer!, out var defBang) && (defBang == "CHARGE" || defBang == "BARRIER" || defBang == "NUKE BARRIER"))
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
            
        foreach (Player usernameDef in match.defChDict.Keys)
        {
            if (match.defChDict[usernameDef] == "ELIMINATED")
                match.players.Remove(usernameDef);
        }

        match.defChDict.Clear();

        foreach (Player usernameHalf in match.halfChDict.Keys)
        {
            if (match.halfChDict[usernameHalf] == "ELIMINATED")
                match.players.Remove(usernameHalf);
            
        }

        match.halfChDict.Clear();

        foreach (Player usernameOne in match.oneChDict.Keys)
        {
            if (match.oneChDict[usernameOne] == "ELIMINATED")
                match.players.Remove(usernameOne);                
        }

        match.oneChDict.Clear();

        foreach (Player usernameTwo in match.twoChDict.Keys)
        {
            if (match.twoChDict[usernameTwo] == "ELIMINATED")
                match.players.Remove(usernameTwo);
        }

        match.twoChDict.Clear();
        match.threeChDict.Clear();

        foreach (Player usernameFour in match.fourChDict.Keys)
        {
            if (match.fourChDict[usernameFour] == "ELIMINATED")
                match.players.Remove(usernameFour);
        }

        match.fourChDict.Clear();

        foreach (Player eliminatedPlayer in match.eliminated)
            match.players.Remove(eliminatedPlayer);
        
        match.eliminated.Clear();
    }
}
