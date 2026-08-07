# Gameplay FAQs

-------------------------------------------------------  
__What is the objective of the game?__  
Your goal is to eliminate all your opponents and be the last player standing in this rock-paper-scissors-style game!   

-------------------------------------------------------\
__Are the user inputs case sensitive?__\
No, they are not. Every char and string input is completely converted into uppercase. The only exception to this however, is during targeting; they require a player's username character-for-character.\
-------------------------------------------------------

-------------------------------------------------------\
__How many charges do each player start with?__\
Each player gets a single charge at the start of each match.\
-------------------------------------------------------

-------------------------------------------------------\
__What type of moves can I make?__\
Aside from charge and forfeit, there are four categories of moves that you can make:
- Defensive Moves
- Half-charge Moves
- One-charge Moves
- Two-charge Moves
- Three-charge Moves
- Four-charge Moves

Currently, there is only one half-charge and three-charge moves available.\
-------------------------------------------------------

-------------------------------------------------------\
__What are defensive moves?__\
Defensive moves are moves that you make to block, and sometimes reflect oncoming attacks.\
The list of defensive moves are as follows:
- Teleport
- Vanish
- Shield
- Barrier
- Nuke Barrier

Fact: Despite "charge" leaving you or your opponents vulnerable to oncoming attacks, the code treats it as a defensive move.\
-------------------------------------------------------

-------------------------------------------------------\
__What are half-charge moves?__\
Half-charge moves are a type of offensive move that requires half a charge to perform.\
Additionally, these moves require a valid target when used.\

Currently, there is only one half-charge move in this iteration of the game which is "Bang".\
-------------------------------------------------------

-------------------------------------------------------\
__What are one-charge moves?__\
One-charge moves are a type of offensive move that requires a charge to perform.\
Additionally, these moves require a valid target when used.

The list of one-charge moves are as follows:
- Mini-Ho
- Mini-Hit
- DX
- Bang-Bang
-------------------------------------------------------

-------------------------------------------------------\
__What are two-charge moves?__\
Two-charge moves are a type of offensive move that requires two charges to perform.

The list of two-charge moves are as follows:
- Homing
- Hit
- DX-Bang-Bang

It is also worth noting that DX-Bang-Bang requires a valid target when used. Homing and Hit, when used, will attack everyone except its user.\
-------------------------------------------------------

-------------------------------------------------------\
__What are three-charge moves?__\
Three-charge moves are a type of offensive move that requires three charges to perform.


Currently, there is only one three-charge move in this iteration of the game which is "A-Bomb".\
It is also worth noting that when used, "A-Bomb" will attack everyone except its user.\
-------------------------------------------------------

-------------------------------------------------------\
__What are four-charge moves?__\
Four-charge moves are a type of offensive move that requires four charges to perform.

The list of four-charge moves are as follows:
- Hit-Homing
- Nuke

It is also worth nothing that both moves will attack everyone except its user.\
-------------------------------------------------------

-------------------------------------------------------\
__Isn't A-Bomb a specific type of nuke? Why are they different in this game?__\
To be honest, I do not know why it is like that either. Besides, I didn't make that decision. I'm just following the game's rules according to my memory.\
-------------------------------------------------------

-------------------------------------------------------\
__Why does it say that I have an invalid input when I perform certain moves?__\
Currently, the game only accepts character-for-character inputs. So yes, you need to include the hyphen.\
-------------------------------------------------------

-------------------------------------------------------\
__What will happen to the person that I target if they are targeting a different player?__\
In the original game, the person you are targeting is treated as vulnerable to any attack. However, I forgot to code that. Whoops.\
-------------------------------------------------------

-------------------------------------------------------\
__What happens when two or more players use the same charge move against each other?__\
Nothing will happen to the players who performed those moves. Other players will still have to defend against each and every attack though if any of the moves affect everyone.\
-------------------------------------------------------

-------------------------------------------------------\
__What does each move do exactly?__
- Forfeit: The move's user forfeits the game.
- Charge: The move's user gains one more charge. This also leaves the player vulnerable to any attack.\

- Teleport: The move's user becomes immune to certain attacks. Attacks that have "hit" in their names get reflected.
- Vanish: The move's user becomes immune to certain attacks.
- Shield: The move's user becomes immune to certain attacks.
- Barrier: The move's user becomes immune to certain attack.
- Nuke Barrier: The move's user becomes immune to nukes. This also leaves the player vulnerable to any other attack.\

- Bang: The move's user eliminates players who have used "Charge" and "Barrier". This move requires a target.\


- Mini-Ho: The move's user eliminates players who have used "Charge", "Bang", "Teleport", and "Nuke Barrier". This move requires a target.
- Mini-Hit: The move's user eliminates players who have used "Charge, "Bang", "Vanish", and "Nuke Barrier". This move requires a target. If the target used "Teleport", the attack will be reflected and the move's user will be eliminated.
- DX: The move's user eliminates players who have used "Charge", "Bang", "Shield", and "Nuke Barrier". This move requires a target.
- Bang-Bang: The move's user eliminates players have used "Charge", "Bang", "Barrier", and "Nuke Barrier". This move requires a target.\

- Homing: The move's user eliminates players who have used "Charge", "Teleport", "Nuke Barrier", and every lower charge move. This move attacks everyone except the user.
- Hit: The move's user eliminates players who have used "Charge", "Vanish", "Nuke Barrier", and every lower charge move. This move attacks everyone except the user. If any opponent used "Teleport", the attack will be reflected and the move's user will be eliminated."
- DX-Bang-Bang: The move's user eliminates players who have used "Charge", "Shield", "Barrier", "Nuke Barrier", and every lower charge move. This move requires a target.\

- A-Bomb: The move's user eliminates players who have used "Charge", "Teleport", "Shield", "Barrier", "Nuke Barrier", and every lower charge move. This move attacks everyone except the user.\

- Hit-Homing: The move's user eliminates players who have used a move that is not "Teleport". This move attacks everyone except the user. If any opponent used "Teleport", the attack will be reflected and the move's user will be eliminated."
-------------------------------------------------------
- Nuke: The move's user eliminates every player who have used a move that is not "Nuke Barrier". This move attacks everyone except the move's user.
