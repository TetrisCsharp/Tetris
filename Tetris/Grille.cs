using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    public class Grille
    {
        private const int dim_x = 15; // hauteur de la grille
        private const int dim_y = 7; // largeur de la grille
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

        public char[,] Tab { get;set; }

        public Piece CurrentPiece { get; set; }

        public void AffichageGrille()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if(j == 0 ) Console.Write(" |");
                    Console.Write(" ");
                    Console.Write(tab[i, j]);

                    if (j == dim_y - 1)
                    {
                        Console.Write("| ");
                        Console.Write("\n");
                    }
                }
            }
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
            //UPDATE Array position

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
        public void descentePiece(string str)
        {
            int b = 0;
            if (str == "right") b = 1; // right
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

        public bool grilleFull()
        {
            int i = 0;
            for(int j = 0; j < this.tab.GetLength(1); j++)
            {
                if (this.tab[i, j] == '#') return true;
            } return false;
        }

    }
}
