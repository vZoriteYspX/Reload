using System;

namespace Game
{
    public class MainProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What will be your next move?");
            string userInput1 = Console.ReadLine();
            Player1 turn = new(userInput1);
        }
    }

    public class Charges
    {
        public int defCharge = 0;
        public float halfCharge = 0.5F;
    }

    public class Player1 : Charges
    {
        public Player1(string userInput1)
        {
            MoveUsed p1Move001 = new();
            switch(userInput1)
            {
                case "Teleport":
                {
                    Console.WriteLine($"Player 1 used: {p1Move001.teleportMove} for {p1Move001.defCharge} charge(s)!");
                    break;
                }

                case "Vanish":
                {
                    Console.WriteLine($"Player 1 used: {p1Move001.vanishMove} for {p1Move001.defCharge} charge(s)!");
                    break;
                }

                case "Shield":
                {
                    Console.WriteLine($"Player 1 used: {p1Move001.shieldMove} for {p1Move001.defCharge} charge(s)!");
                    break;
                }

                case "Barrier":
                {
                    Console.WriteLine($"Player 1 used: {p1Move001.barrierMove} for {p1Move001.defCharge} charge(s)!");
                    break;
                }

                case "Nuke Barrier":
                {
                    Console.WriteLine($"Player 1 used: {p1Move001.nukeBarrierMove} for {p1Move001.defCharge} charge(s)!");
                    break;
                }
            }
        }
    }

    public class MoveUsed : Charges
    {
        public string teleportMove = "Teleport";
        public string vanishMove = "Vanish";
        public string shieldMove = "Shield";
        public string barrierMove = "Barrier";
        public string nukeBarrierMove = "Nuke Barrier";
        public MoveUsed() { }
    }
}
