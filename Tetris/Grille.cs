using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    class Grille
    {
        private const int dim_x = 15;
        private const int dim_y = 7;
        private char[,] tab;
        private Piece currentPiece;

        public Grille() { this.tab = new char[dim_x, dim_y]; }

        public char[,] Tab
        {
            get { return this.tab; }
            set { this.tab = value; }
        }

        public Piece CurrentPiece { get; set; }


        public void AffichageGrille()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(" | ");
                    Console.Write(tab[i, j]);

                    if (j == dim_y - 1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        public void initPieceGrille(Piece piece) //position initiale
        {
            AjoutPiece(piece);
        }

        public void AjoutPiece(Piece piece) // ajouter la piece sur un indice x, y précedemment ajouté à l'objet  "currentPiece"
        {
            this.currentPiece = piece;

            for (int i = 0; i < this.currentPiece.Array.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.Array.GetLength(1); j++)
                {
                    this.tab[i, j] = this.currentPiece.Array[i, j];
                }
            }
           // this.currentPiece.ArrayPosition;
        }

        // gère uniquement la descente de la pièce (c'est à dire la suppression de la piece entre les indices de la matrice de position (piece.arrayPosition)
        public void supressionPiece()
        {
            for (int i = 0; i < this.CurrentPiece.ArrayPosition.GetLength(0); i++)
            {
                for (int j = 0; j < this.currentPiece.ArrayPosition.GetLength(1); j++)
                {
                    this.Tab[i, j] = ' ';
                }
            }
        }

        public bool grilleFull()
        {
            return;
        }

    }
}
