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
            //Connecion();

            //variables
            int refresh = 25;
            int speed = 300;

            GameManager gameManager = new GameManager(refresh, speed);

            Console.WriteLine("Appuyez pour jouer");
            Console.ReadKey();
            Console.Clear();

            gameManager.startGame();

        }

        public static void Connecion(){
            Client client = new Client();
            client.StartClient();
    }

    }
}
