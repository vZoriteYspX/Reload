using System;

namespace Game
{
  public class MainProgram
  {
    static void Main(string[] args)
    {
      DefensiveMove move001 = new();
      Console.WriteLine($"Player 1 used: {move001.teleportMove} for {move001.charge} charge(s)!");
    }
  }
  public class Charges
  {
    public int charge;
    public float halfCharge;
  }
  public class DefensiveMove : Charges
  {
    public string teleportMove;
    public string vanishMove;
    public string shieldMove;
    public string barrierMove;
    public string nukeBarrierMove;

    public DefensiveMove()
    {
      charge = 0;
      teleportMove = "Teleport";
      vanishMove = "Vanish";
      shieldMove = "Shield";
      barrierMove = "Barrier";
      nukeBarrierMove = "Nuke Barrier";
    }
  }
  public class HalfChargeMove : Charges
  {
    public string bangMove;
    public string DMove;

    public HalfChargeMove()
    {
      halfCharge = 0.5F;
      bangMove = "Bang";
      DMove = "D";
    }
  }
}
