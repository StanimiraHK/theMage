namespace TheGiraffeGame
{
    public class Particle
    {
        private int col;
        private int row;
        private char symbol;
        private bool isGood;

        public bool IsGood
        {
            get { return isGood; }
            set { isGood = value; }
        }

        public Particle(int row, int col, char symbol, bool isGood)
        {
            this.row = row; 
            this.col = col;
            this.symbol = symbol;
            this.isGood = isGood;
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
