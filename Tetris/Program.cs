﻿using System;
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
           

            //Ajout Piece
            grille.AjoutPiece(randomPiece());
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
            

            //delete piece
            grille.suppressionPiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
            

            // go on the right

            
            grille.descentePiece("right");
            Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);
                
            //problème
            grille.suppressionPiece();
            Console.Clear();
            grille.AffichageGrille();
            Thread.Sleep(500);
            
            grille.descentePiece("right");
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
            int rand = random.Next(1,3); // 1 ou 2
            return new Piece(rand);

        }
    }
}
