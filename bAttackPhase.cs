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

        foreach (Player usernameFour in match.attributesFourDict.Keys)
        {
            switch (move)
            {
                case "HIT-HOMING":
                {   
                    foreach (Player usernameDef in match.attributesDefDict.Keys)
                    {   
                        if (match.attributesDefDict[usernameDef] == "TELEPORT")
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
                        if (match.attributesDefDict[usernameDef] != "NUKE BARRIER")
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
                        if (match.attributesDefDict[usernameDef] != "VANISH")
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
                        if (match.attributesDefDict[usernameDef] != "VANISH" || match.attributesDefDict[usernameDef] != "SHIELD" || match.attributesDefDict[usernameDef] != "BARRIER")
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
                        if (match.attributesDefDict[usernameDef] == "TELEPORT")
                            match.attributesTwoDict[usernameTwo] = "ELIMINATED";

                        else if (match.attributesDefDict[usernameDef] != "SHIELD" || match.attributesDefDict[usernameDef] != "BARRIER")
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
