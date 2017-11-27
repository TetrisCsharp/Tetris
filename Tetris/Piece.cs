using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    public class Piece
    {
        private char[,] array; // remplissage de la grille
        private int id;
        private int[,] [] arrayPosition; //position de la piece par rapport la grille (index de la piece dans la grille)

        public Piece(int id)
        {
            this.array = new char[id, id];
            this.remplirArrayPosition();
            this.id = id;
            this.arrayPosition = new int[id,id] [];

            for(int i = 0; i < id + 1; i++)
            {
                for(int j = 0; j < id + 1; j++)
                {
                    this.arrayPosition[i, j] = new int[2];
                }
            }
        }

        public void remplirArrayPosition() // remplis la grille au tout début (donc 1 seule fois)
        {
            for (int x = 0; x < this.id + 1; x++)
            {
                for (int y = 0; y < this.id + 1; y++)
                {
                    this.array[x, y] = '#';
                }
            }
        }
        public char[,] Array { get; set; }
        public int Id { get; }
        public int[,] [] ArrayPosition { get; set; }
    }

}
