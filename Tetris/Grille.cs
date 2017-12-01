using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    public class Grille
    {
        private int dim_x = 12; // hauteur de la grille
        private int dim_y = 8; // largeur de la grille
        private char[,] tab; // la grille en elle meme
        private Piece currentPiece; // la piece que l'on est en train de bouger sur la grille
        private ConsoleKey ck;
        private GameManager gameManager;


        public Grille(int dim_x,int dim_y) {

            this.dim_x = dim_x;
            this.dim_y = dim_y;
            this.tab = new char[dim_x, dim_y];
            for (int x = 0; x < dim_x; x++)
            {
                for (int y = 0; y < dim_y; y++)
                {
                    this.tab[x, y] = ' ';
                }
            }
        }

        public ConsoleKey Keyboard {
            get { return this.ck; }
            set { this.ck = value; }
        }

        public char[,] Tab
        {
            get {
                return this.tab;
            }
            set
            {
                this.tab = value;
            }
        }

        public Piece CurrentPiece { get; set; }


        // OK
        public void AffichageGrille()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (j == 0) Console.Write("|");

                    Console.Write(tab[i, j]);

                    if (j == dim_y - 1)
                    {
                        Console.Write("|");
                       
                    }
                }
                if (i == 4) Console.Write(" Speed : " + gameManager.Speed);
                Console.Write("\n");
            }
            Console.WriteLine("----------");

        }

        // OK
        public void AjoutPiece(Piece piece) // ajouter la piece sur un indice x, y précedemment ajouté à l'objet  "currentPiece"
        {
            //add the piece to the grille
            this.currentPiece = piece;

            //add piece.array to (0,0) of the grille
            for (int i = 0; i < this.currentPiece.Array.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.Array.GetLength(1); j++)
                {
                    this.tab[i, j] = '#';
                    this.currentPiece.ArrayPosition[i, j][0] = i;
                    this.currentPiece.ArrayPosition[i, j][1] = j;
                }
            }
        }

        // OK
        public void suppressionPiece() // //cacher la piece
        {
            //par rapport à arrayPosition
            for (int i = 0; i < this.currentPiece.Array.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.Array.GetLength(1); j++)
                {
                    int n1 = this.currentPiece.ArrayPosition[i, j][0];//x
                    int n2 = this.currentPiece.ArrayPosition[i, j][1];//y
                    this.tab[n1, n2] = ' ';
                }
            }
        }

        // gère uniquement la descente gauche / droite
        // OK
        public void deplacementPiece(bool str)
        {
            //true => right
            //false => left

            int b = 0;
            if (str == true) b = 1; // right
            else b = -1; // left

            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.ArrayPosition.GetLength(1); j++)
                {
                    //nouvelle coordonnées

                    int n1 = this.currentPiece.ArrayPosition[i, j][0];//x
                    int n2 = this.currentPiece.ArrayPosition[i, j][1] + b;//y

                    this.currentPiece.ArrayPosition[i, j][1] += b;
                  this.tab[n1, n2] = '#';
                }
            }
        }

        // OK
        public void descendrePiece()
        {
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.ArrayPosition.GetLength(1); j++)
                {
                    //nouvelle coordonnées

                    int n1 = this.currentPiece.ArrayPosition[i, j][0] + 1;//x
                    int n2 = this.currentPiece.ArrayPosition[i, j][1];//y

                    this.currentPiece.ArrayPosition[i, j][0] += 1;
                    this.tab[n1, n2] = '#';
                }
            }
        }
        // OK
        public bool verifBelowPiece() //retourne true s'il existe une piece en dessous, false sinon
        {
            //index ligne fixé sur la dernière ligne
            int indexLigne = this.currentPiece.ArrayPosition.GetLength(0) - 1;

            //colonnes
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                int indexX = this.currentPiece.ArrayPosition[indexLigne, i][0]; // index lde la ligne
                int indexY = this.currentPiece.ArrayPosition[indexLigne, i][1]; // index lde la colonne

                //verification sur la ligne suivante (les conditions d'arrêts pour laisser la pièce à un endroit et pour passer à une nouvelle piece
                if (indexX == this.tab.GetLength(0) - 1) return true;
                if (this.tab[indexX + 1, indexY] == '#') return true;
            }
            return false;
        }

        public bool verifLeftPiece() //retourne true s'il existe une piece en dessous, false sinon
        {
            //index colonne fixé sur la colonne 0
            int indexColonne = 0;

            //colonnes
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                int indexX = this.currentPiece.ArrayPosition[i, indexColonne][0]; // index lde la ligne
                int indexY = this.currentPiece.ArrayPosition[i, indexColonne][1]; // index lde la colonne

                //verification sur la colonne à gauche
                if (indexY > 0)
                {
                    if (this.tab[indexX, indexY - 1] == '#') return true;
                }
            }
            return false;

        }

        public bool verifRightPiece() //retourne true s'il existe une piece en dessous, false sinon
        {
            //index colonne fixé sur la dernière colonne
            int indexColonne = this.currentPiece.ArrayPosition.GetLength(1) - 1;

            //colonnes
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                int indexX = this.currentPiece.ArrayPosition[i, indexColonne][0]; // index lde la ligne
                int indexY = this.currentPiece.ArrayPosition[i, indexColonne][1]; // index lde la colonne

                if (indexY != this.tab.GetLength(1) - 1)
                {
                    //verification sur la colonne à gauche
                    if (this.tab[indexX, indexY + 1] == '#') return true;
                }
            }
            return false;
        }

        public bool beforeTheEnd()
        {
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(1); i++)
            {
                if (this.currentPiece.ArrayPosition[this.currentPiece.ArrayPosition.GetLength(0) - 1, i][0] < this.tab.GetLength(0) - 1)
                {
                    return false;
                }
            }
            return true;
        }


        public bool grilleFull() // fin de partie //todo
        {//la grille est full si la partie basse de la position de la pièce est en conflit avec le tab de la grille aux memes position
            int i = 0;
            for (int j = 0; j < this.tab.GetLength(1); j++)
            {
                if (this.tab[0, j] == '#') return true;
            } return false;
        }

        public bool limits()
        {
            return this.OutOfLimitLeft() || this.OutOfLimitRight();
        }
        public bool OutOfLimitLeft()
        {
            int indexColonne = this.currentPiece.ArrayPosition[0, 0][1]; //colonne fixé
            if (indexColonne == 0) return true;
            return false;

        }

        public bool OutOfLimitRight()
        {

            int indexColonne = this.currentPiece.ArrayPosition[0, this.currentPiece.ArrayPosition.GetLength(1) - 1][1];
            if (indexColonne == this.tab.GetLength(1) - 1) return true;
            return false;
        }

        public bool verifDeleteLineOn(int x)//todo
        {
            int cpt = 0;

            for (int y = 0; y < this.tab.GetLength(1); y++)
            {
                if (this.tab[x, y] == '#') cpt++;
                else break;
            }
            if (cpt == this.tab.GetLength(1)) return true;
            return false;

        }

        public void deleteLine(GameManager gameManager)//to be tested
        {
            for (int x = 0; x < this.tab.GetLength(0); x++)
            {
                if (verifDeleteLineOn(x)==true)
                {
                    moveElementAbove(x);
                    if(this.gameManager.Speed > 0) this.gameManager.Speed -= 10;
                }
            }
        }

        public void moveElementAbove(int x) // to be tested
        {
            for (int i = x; i > 0; i--)
            {
                for (int j = 0; j < this.tab.GetLength(1); j++)
                {
                    this.tab[i, j] = this.tab[i - 1, j];
                }
            }
            //first line (new line)
            for (int j = 0; j < this.tab.GetLength(1); j++)
            {
                this.tab[0, j] = ' ';
            }
        }

        public void setGameManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
            
        }

    }
}

