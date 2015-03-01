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

        public Particle(int row, int col, bool isGood)
        {
            this.row = row; 
            this.col = col;
            this.isGood = isGood;
            this.symbol = isGood ? 'Ơ' : '¤';
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
