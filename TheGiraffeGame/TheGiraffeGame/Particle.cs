namespace TheGiraffeGame
{

    public class Particle
    {
        private int col;

        public int Col
        {
            get { return col; }
            set { col = value; }
        }
        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        private char symbol;

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

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

        public static Particle GenerateParticle()
        {
            bool isGoodParticle = (GlobalConstants.numGenerator.Next() % 5 == 0 ? true : false);

            int particleRow = GlobalConstants.numGenerator.Next(3, GlobalConstants.rows);
            return new Particle(particleRow, GlobalConstants.columns - 1, isGoodParticle);
        }
    }
}
