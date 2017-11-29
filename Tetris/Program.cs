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
            int refresh = 250;
            while (!finishGame)
            {
                addPiece(grille,refresh);
                deletePiece(grille,refresh);
                
                while (!grille.verifUnderPiece()) {
                    
                    if (grille.Keyboard == ConsoleKey.LeftArrow && !grille.OutOfLimitLeft())
                    {
                        if (!grille.verifLeftPiece())
                        {
                            goLeft(grille, refresh);
                            deletePiece(grille, refresh);
                        }
                    }
                    else if (grille.Keyboard == ConsoleKey.RightArrow && !grille.OutOfLimitRight())
                    {
                        if (!grille.verifRightPiece())
                        {
                            goRight(grille, refresh);
                            deletePiece(grille, refresh);
                        }
                    }
                    if (grille.beforeTheEnd()) break;
                    goDown(grille,refresh);
                    if(!grille.verifUnderPiece())  deletePiece(grille,refresh);
                    grille.deleteLine();
                    
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


            public static void addPiece(Grille grille,int n)
            {
                //Ajout Piece
                grille.AjoutPiece(new Piece());
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(n);



            }

            public static void deletePiece(Grille grille,int n)
            {
                //Supprimer Piece
                grille.suppressionPiece();
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(n);
            }

            public static void goRight(Grille grille,int n)
            {
                // Aller à droite            
                grille.deplacementPiece(true);
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(n);
            }

            public static void goLeft(Grille grille,int n)
            {
                // Aller à droite            
                grille.deplacementPiece(false);
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(n);
            }

            public static void goDown(Grille grille,int n)
            {
                //Descendre Pièce
                grille.descendrePiece();
                Console.Clear();
                grille.AffichageGrille();
                Thread.Sleep(n);
            }
        }
    } 
