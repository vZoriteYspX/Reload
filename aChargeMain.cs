using System;
using System.Collections.Generic;

namespace ReloadGame;

public class MainProgram
{
    //  MAIN METHOD
    static void Main(string[] args)
    {   
        new Start();  //  BEGINS THE START INPUT
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


            //  CATCHES INPUTS THAT ARE NOT Y OR N
            try
            {
                startInput = Convert.ToChar((Console.ReadLine() ?? "").ToUpper());
            }

            catch (Exception e)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" to start the game.");
                Console.WriteLine("----------------------------");
                Console.WriteLine($"{e.Message}");
                continue;
            }


            //  STARTS GAME IF Y WAS INPUTTED
            if (startInput == 'Y')
            {
                Console.WriteLine("Have fun! You are now proceeding to the game's setup.");
                Console.WriteLine("----------------------------");

                try
                {
                    Gameplay gameStart = new(true);  //  STARTS THE GAME PROPER
                }
                    
                catch
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Oops, something went wrong with the game.");
                    Console.WriteLine("----------------------------");   
                }

                break;
            }


            //  ENDS CODE IF N WAS INPUTTED
            else if (startInput == 'N')
            {   
                Console.WriteLine("All right, see you again next time!");
                Console.WriteLine("----------------------------");
                break;
            }
                
            else
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("An invalid input was entered. Please input \"Y/y\" or \"N/n\" to start the game.");
            }
        }
    }
}
