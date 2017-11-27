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
            //init grille
            Grille grille = new Grille();
            grille.AffichageGrille();

            //waiting a key to begin
            Console.WriteLine("Appuyez pour jouer");
            Console.Read();
            Console.Clear();

            //generate the piece

            grille.CurrentPiece = randomPiece();
            grille.DescentePiece();
            grille.AjoutPiece(p);
            grille.AffichageGrille();
            Thread.Sleep(1000);
            grille.DescentePiece()

            Console.Clear();
            grille.AjoutPiece2();
            grille.AffichageGrille();
            Console.WriteLine("press up");
            Thread.Sleep(1000);
            ConsoleKeyInfo a = Console.ReadKey();
            Thread.Sleep(2000);
            if (a.Key == ConsoleKey.UpArrow)
            {
                Console.WriteLine("up !!!");
            }

            Console.ReadKey();
        }

        public void keyPressed(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.UpArrow) { }
            if (cki.Key == ConsoleKey.DownArrow) { }
            if (cki.Key == ConsoleKey.LeftArrow) { }
            if (cki.Key == ConsoleKey.RightArrow) { }
        }


        public static Piece randomPiece()
        {
            Random random = new Random();
            int rand = random.Next(2);
            if (rand == 1) return new Piece(rand, 0, 0);
            else return new Piece(rand, 1, 1);

        }
    }
}
