using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris_Like
{
    public class GameManager
    {
        
        private Grille grille;
        private int refresh; // pour l'affichage
        private int speed; // pour le reste
        private bool finishGame;
        private bool finishWithPiece;

        public GameManager(Grille grille, int refresh, int speed)
        {
            this.grille = grille;
            this.refresh = refresh;
            this.speed = speed;
            this.finishGame = false;
            this.finishWithPiece = false;
        }

        public void startGame()
        {
            while (!grille.grilleFull())
            {
                addPiece(grille);

                while (!grille.verifBelowPiece())
                {
                    if (grille.beforeTheEnd()) break;
                    goDown(grille);
                    if (!grille.verifBelowPiece()) grille.suppressionPiece();
                    grille.deleteLine();
                    //Console.WriteLine(grille.grilleFull());
                }
            }

        }

        public bool KeyPressed(Grille grille)
        {
            while (true)
            {
                ConsoleKeyInfo cki;
                cki = Console.ReadKey(true);
                this.userCommand(cki);

            }
        }

        //géré par un thread
        public void userCommand(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.LeftArrow && !grille.OutOfLimitLeft() && !grille.verifLeftPiece())  goLeft(grille);
           
            if (cki.Key == ConsoleKey.RightArrow && !grille.OutOfLimitRight() && !grille.verifRightPiece()) goRight(grille);

        }
        //gérer par un thread
        public void CommunicationWithServer()
        {

        }
        public void display(GameManager game)
        {
            while (true)
            {
                game.Grille.AffichageGrille();
                Thread.Sleep(this.refresh);
                Console.SetCursorPosition(0, 0);
            }

        }

        public void addPiece(Grille grille)
        {
            //Ajout Piece
            grille.AjoutPiece(new Piece());
            Thread.Sleep(this.speed);
        }

        public void goRight(Grille grille)
        {
            grille.suppressionPiece();          
            grille.deplacementPiece(true);
            Thread.Sleep(this.speed);
        }

        public void goLeft(Grille grille)
        {
            grille.suppressionPiece();
            grille.deplacementPiece(false);
            Thread.Sleep(this.speed);
        }

        public void goDown(Grille grille)
        {
            grille.suppressionPiece();
            grille.descendrePiece();
            Thread.Sleep(this.speed);
        }

        public Grille Grille { get { return this.grille; } }

    }


}


