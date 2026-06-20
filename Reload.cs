using System;

namespace Game
{
  public class MainProgram
  {
  static void Main(string[] args)
    {
      DefensiveMove teleport = new DefensiveMove();
      Console.WriteLine(teleport.teleportMove);
      Console.WriteLine(teleport.defCharge);
      }
    }

  public class DefensiveMove
  {
    public int defCharge;
    public string teleportMove;
    public string vanishMove;
    public string shieldMove;
    public string barrierMove;
    public string nukeBarrierMove;
    public DefensiveMove()
    {
    defCharge = 0;
     teleportMove = "Teleport";
     vanishMove = "Vanish";
     shieldMove = "Shield";
     barrierMove = "Barrier";
     nukeBarrierMove = "Nuke Barrier";
    }
  }
}
