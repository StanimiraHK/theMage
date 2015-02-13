using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGiraffeGame
{
    class Particle
    {
        private int col;
        private int row;
        private char symbol;
        private bool isGood;

        public Particle(int row, int col, char symbol)
        {
            this.row = row; 
            this.col = col;
            this.symbol = symbol;
        }
        
        public int getCol(){
            return col ;
        }

        public int getRow() {
            return row;
        }

        public char getSymbol() {
            return symbol;
        }

        public void setCol(int col) {
            this.col = col;
        }

        public void setRow(int row) {
            this.row = row;
        }
    }
}
