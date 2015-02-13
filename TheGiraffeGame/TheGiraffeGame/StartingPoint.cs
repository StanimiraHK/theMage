using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheGiraffeGame
{
    public class StartingPoint
    {

        private static void MoveHead(ConsoleKeyInfo keyinfo)
        {
            char giraffeHeadChar = '@';

            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHead.Row > 0)
                    {
                        GiraffesHead.Row--;
                        Screen[GiraffesHead.Row, GiraffesHead.Col] = giraffeHeadChar;

                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (GiraffesHead.Row < rows - 1)
                    {
                        GiraffesHead.Row++;
                        Screen[GiraffesHead.Row, GiraffesHead.Col] = giraffeHeadChar;

                    } break;
                default:
                    break;
            }
        }

        private static void particleMove(char[,] screen, List<Particle> particles)
        {
            char particleChar = '#';

            Random numGenerator = new Random();
            int particleRow = numGenerator.Next(0, rows);
            particles.Add(new Particle(particleRow, columns - 1, particleChar));

            clearScreen(screen);

            for (int i = 0; i < particles.Count; i++)
            {
                int particleCol = particles[i].getCol();
                particleRow = particles[i].getRow();

                if (particleCol > 0 && particleRow < rows && particleRow > 0)
                {

                    if (particleRow == GiraffesHead.Row && particleCol == GiraffesHead.Col) {
                        isHit = true;
                        break;
                    }

                    screen[particleRow, particleCol] = particles[i].getSymbol();

                    particleCol--;
                    particles[i].setCol(particleCol);
                }
                else {
                    particles.RemoveAt(i);
                }
            }
        }

        private static void PrintMatrix(char[,] screen)
        {
            Console.Clear();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Console.Write(screen[row, col]);
                }
                Console.WriteLine();
            }
        }

        public static void clearScreen(char[,] screen)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    screen[row, col] = ' ';
                }
            }
            Screen[GiraffesHead.Row, GiraffesHead.Col] = '@';
        }


        public static char[,] Screen;
        public static GiraffesHead GiraffesHead;
        private static int rows = 20;
        private static int columns = 60;
        public static bool isHit = false;

        static void Main()
        {
            List<Particle> particles = new List<Particle>();
            Screen = new Char[rows, columns];
            GiraffesHead = new GiraffesHead(5, 20);

            clearScreen(Screen);

            while (true)
            {
                if (Console.KeyAvailable) // true if a key press is available in the input stream
                {

                    ConsoleKeyInfo pressedKey = Console.ReadKey(true); // reads the next key
                    while (Console.KeyAvailable) // flushes the input stream(the pressed keys are inserted in a queue
                    {                            // and readKey empties the queue
                        Console.ReadKey(true);
                    }
                    MoveHead(pressedKey);
                }
                particleMove(Screen, particles);
                PrintMatrix(Screen);
                if (isHit)
                {
                    Console.WriteLine("Game over");
                    break;
                }
                Thread.Sleep(250);
            }

        }

    }
}