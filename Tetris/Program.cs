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

            //threads declarations
            Thread threadKey = new Thread(() => KeyPressed(grille));
            threadKey.Start();

            //waiting a key to begin
            Console.WriteLine("Appuyez pour jouer");
            Console.Read();
            // grille.Tab[4, 1] = 'x';

            bool finishGame = false;
            bool finishWithPiece = false;

            while (!finishGame)
            {
                addPiece(grille);
                deletePiece(grille);

                while (!grille.verifUnderPiece()) {

                    if (grille.Keyboard == ConsoleKey.LeftArrow)
                    {
                        goLeft(grille);
                        deletePiece(grille);
                    }
                    else if (grille.Keyboard == ConsoleKey.RightArrow)
                    {
                        goRight(grille);
                        deletePiece(grille);
                    }
                    else { }
                    goDown(grille);
                    deletePiece(grille);
                   }
                }








                /*
                goDown(grille);


                deletePiece(grille);
                goDown(grille);
                Console.WriteLine(grille.verifUnderPiece());
                Thread.Sleep(500);
                deletePiece(grille);

                goRight(grille);
                Console.WriteLine(grille.verifUnderPiece());

                deletePiece(grille);

                goDown(grille);
                Console.WriteLine(grille.verifUnderPiece()|| grille.verifLeftPiece());
                Thread.Sleep(500);
                deletePiece(grille);
                */

                Console.ReadKey();
            }

            public static bool KeyPressed(Grille grille)
            {
                while (true)
                {
                    ConsoleKeyInfo cki;
                    cki = Console.ReadKey();
                    grille.Keyboard = cki.Key;

                }
            }


            public static void addPiece(Grille grille)
            {
                //Ajout Piece
                grille.AjoutPiece(new Piece());
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);



            }

            public static void deletePiece(Grille grille)
            {
                //Supprimer Piece
                grille.suppressionPiece();
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);
            }

            public static void goRight(Grille grille)
            {
                // Aller à droite            
                grille.deplacementPiece(true);
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);
            }

            public static void goLeft(Grille grille)
            {
                // Aller à droite            
                grille.deplacementPiece(false);
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);
            }

            public static void goDown(Grille grille)
            {
                //Descendre Pièce
                grille.descendrePiece();
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(500);
            }
        }
    } 
