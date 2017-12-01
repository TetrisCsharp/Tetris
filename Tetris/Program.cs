using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris_Like
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Provide IP:");
            String ip = Console.ReadLine();

            Console.WriteLine("Provide Port:");
            int port = Int32.Parse(Console.ReadLine());

           
            //variables
            int refresh = 25;
            int speed = 300;

            GameManager gameManager = new GameManager(refresh, speed, ip, port);

            Console.WriteLine("Appuyez pour jouer");
            Console.ReadKey();
            Console.Clear();

            gameManager.startGame();

        }

      

    }
}
