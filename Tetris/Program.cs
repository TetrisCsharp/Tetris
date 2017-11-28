using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris_Like
{
    class Program
    {
        private Grille grille;

        static void Main(string[] args)
        {

            //init grille
            Grille grille = new Grille();
            grille.AffichageGrille();

            //Déclaration des threads
            Thread keyThread = new Thread(keyPressed(grille));

            //waiting a key to begin
            Console.WriteLine("Appuyez pour jouer");
            Console.Read();
           

            //Ajout Piece
            grille.AjoutPiece(randomPiece());
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
            

            //Supprimer Piece
            grille.suppressionPiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
            

            // Aller à droite            
            grille.deplacementPiece(true);
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
                
            // Re - suppression
            grille.suppressionPiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);


            //Descendre Pièce
            grille.descendrePiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);

            grille.suppressionPiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);

            
            // Aller encore à droite
            grille.deplacementPiece(true);
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);




            //go o the left

            /*
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
            */

            Console.ReadKey();
        }

        public bool keyPressed(Grille grille)
        {
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();

            if (cki.Key == ConsoleKey.LeftArrow) return false;
            else return true;
        }



        public static Piece randomPiece()
        {
            Random random = new Random();
            int rand = random.Next(1,3); // 1 ou 2
            return new Piece(rand);

        }
    }
}
