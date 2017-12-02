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
            Console.WriteLine("Enter the commands to start");
            string commands = Console.ReadLine();

            // commands : TetrisPlayer.exe server_address server_port high_key right_key low_key left_key
            // example : TetrisPlayer.exe 127.0.0.1 4321 z q s d
            // example : TetrisPlayer.exe 127.0.0.1 5555

            String[] tab = commands.Split(' ');

            if (tab.Length > 3 || tab.Length < 3)
            {
                Console.WriteLine("Wrong commands, enter the commands to start");
                commands = Console.ReadLine();

                tab = commands.Split(' ');
            }

            //name (must be TetrisPlayer.exe)
            String name = tab[0];

            //server_address
            string server_address = tab[1];

            //server_port
            int server_port = Int32.Parse(tab[2]);


            GameManager gameManager = new GameManager(server_address,server_port);

            Console.WriteLine("Appuyez pour jouer");
            Console.ReadKey();
            Console.Clear();

            gameManager.startGame();

        }
    }
}
