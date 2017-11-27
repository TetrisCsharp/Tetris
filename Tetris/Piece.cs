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
        private int[,] arrayPosition; //position de la piece par rapport la grille (index de la price dans la grille)

        public Piece(int id, int x, int y)
        {
            this.array = new char[x, y];
            this.id = id;
            this.arrayPosition = null;
        }

        public void Update(bool rl) // update arrayPostion
        {
            if (rl)// left
            {
                for (int x = 0; x < this.arrayPosition.GetLength(0); x++)
                {
                    for (int y = 0; y < this.arrayPosition.GetLength(1); y++)
                    {
                        if (id == 0)//Piece 1
                        {

                        }
                        else //Piece 2 id == 1
                        {
                            this.arrayPosition] += 1
                        }
                    }
                }
            }
            else // right
            {

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
        public int[] ArrayPosition { get; set; }
    }

}
