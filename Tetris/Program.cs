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
        {   //variables
            int refresh = 50;
            int speed = 250;
            Grille grille = new Grille();
            GameManager gameManager = new GameManager(grille, refresh,speed);


            //Threads declarations
            Thread threadKey = new Thread(() => gameManager.KeyPressed(grille));
            Thread threadDipslay = new Thread(() => gameManager.display(gameManager));
            Thread threadCommuncation = new Thread(() => gameManager.CommunicationWithServer());
            
            //Waiting a key to begin
            Console.WriteLine("Appuyez pour jouer");
            Console.ReadKey();
            Console.Clear();

            threadKey.Start();
            threadDipslay.Start();
            threadCommuncation.Start();

            gameManager.startGame();

        }

    }
}
