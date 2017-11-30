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
        private int[,][] arrayPosition; //position de la piece par rapport la grille (index de la piece dans la grille)

        public Piece()
        {
            Random random = new Random();
            this.id = random.Next(1, 3);
            this.array = new char[id, id];
            this.remplirArrayPosition();

            this.arrayPosition = new int[id, id][];

            for (int i = 0; i < id; i++)
            {
                for (int j = 0; j < id; j++)
                {
                    this.arrayPosition[i, j] = new int[2];
                }
            }
        }

        public void remplirArrayPosition() // remplis la grille au tout début (donc 1 seule fois)
        {
            if (this.id == 1) this.array[0, 0] = '#';
            else
            {
                for (int x = 0; x < this.id; x++)
                {
                    for (int y = 0; y < this.id; y++)
                    {
                        this.array[x, y] = '#';
                    }
                }
            }
        }
        public char[,] Array
        {
            get
            {
                return this.array;
            }
            set
            {
                this.array = value;
            }
        }

        public int Id { get; }
        public int[,][] ArrayPosition
        {
            get { return this.arrayPosition; }
            set { this.arrayPosition = value; }
        }

    }
}
