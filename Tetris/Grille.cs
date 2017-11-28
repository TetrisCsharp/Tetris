using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    public class Grille
    {
        private const int dim_x = 8; // hauteur de la grille
        private const int dim_y = 8; // largeur de la grille
        private char[,] tab; // la grille en elle meme
        private Piece currentPiece; // la piece que l'on est en train de bouger sur la grille

        public Grille() {
            this.tab = new char[dim_x, dim_y];
            for(int x = 0; x < dim_x; x++)
            {
                for(int y = 0; y < dim_y; y++)
                {
                   this.tab[x, y] = ' ';
                }
            }
        }

        public char[,] Tab
        {
            get { return this.tab; }
            set
            {
                this.tab = value;
            }
        }

        public Piece CurrentPiece { get; set; }

        public void AffichageGrille()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if(j == 0 ) Console.Write("|");
                    
                    Console.Write(tab[i, j]);

                    if (j == dim_y - 1)
                    {
                        Console.Write("|");
                        Console.Write("\n");
                    }
                }
            }
            Console.WriteLine("----------------");
        }

        public void AjoutPiece(Piece piece) // ajouter la piece sur un indice x, y précedemment ajouté à l'objet  "currentPiece"
        {
            //add the piece to the grille
            this.currentPiece = piece;

            //add piece.array to (0,0) of the grille
            for (int i = 0; i < this.currentPiece.Array.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.Array.GetLength(1); j++)
                {
                    this.tab[i, j] = 'x';
                    this.currentPiece.ArrayPosition[i, j][0] = i;
                    this.currentPiece.ArrayPosition[i, j][1] = j;
                }
            }
        }

        public void suppressionPiece() // //cacher la piece
        {
            //par rapport à arrayPosition
            for(int i = 0; i < this.currentPiece.Array.GetLength(0); i++)
            {
                for(int j = 0;  j <this.currentPiece.Array.GetLength(1); j++)
                {
                    int n1 = this.currentPiece.ArrayPosition[i, j][0];//x
                    int n2 = this.currentPiece.ArrayPosition[i, j][1];//y
                    this.tab[n1, n2] = ' ';
                }
            }
        }

        // gère uniquement la descente de la pièce (c'est à dire la suppression de la piece entre les indices de la matrice de position (piece.arrayPosition)
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
                    this.tab[n1, n2] = 'x';
                }
            }
        }

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
                    this.tab[n1, n2] = 'x';
                }
            }
        }

        public bool fondBoard()// Todo
        {
            if (this.currentPiece.ArrayPosition[this.currentPiece.ArrayPosition.GetLength(0), 0][0] == this.tab.GetLength(0)) return true;
            else return false;
        }

        public bool verifUnderPiece() //retourne true s'il existe une piece en dessous, false sinon
        {
                //index ligne fixé sur la dernière ligne
                int indexLigne = this.currentPiece.ArrayPosition.GetLength(0) - 1;
               
                //colonnes
                for(int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
                {
                    int indexX = this.currentPiece.ArrayPosition[indexLigne, i][0]; // index lde la ligne
                    int indexY = this.currentPiece.ArrayPosition[indexLigne, i][1]; // index lde la colonne
                                                                                
                    //verification sur la ligne suivante
                    if (this.tab[indexX + 1, indexY] == 'x') return true;
                }
                return false ;
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
                if (this.tab[indexX, indexY - 1] == 'x') return true;
            }
            return false;
        }

        public bool verifRightPiece() //retourne true s'il existe une piece en dessous, false sinon
        {
            //index colonne fixé sur la dernière colonne
            int indexColonne = this.currentPiece.ArrayPosition.GetLength(1)-1;

            //colonnes
            for (int i = 0; i < this.currentPiece.ArrayPosition.GetLength(0); i++)
            {
                int indexX = this.currentPiece.ArrayPosition[i, indexColonne][0]; // index lde la ligne
                int indexY = this.currentPiece.ArrayPosition[i, indexColonne][1]; // index lde la colonne

                //verification sur la colonne à gauche
                if (this.tab[indexX, indexY + 1] == 'x') return true;
            }
            return false;
        }


        public bool grilleFull() // fin de partie
        {
            int i = 0;
            for(int j = 0; j < this.tab.GetLength(1); j++)
            {
                if (this.tab[0, j] == 'x') return true;
            } return false;
        }
        public void limit()
        {
            
        }
    }
}
