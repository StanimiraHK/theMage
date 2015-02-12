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
            char emptySpace = ' ';
            char giraffeHeadChar = '@';

            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHeadVar.Row > 0)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col+1] = emptySpace;
                        Screen[GiraffesHeadVar.Row - 1, GiraffesHeadVar.Col+1] = giraffeHeadChar;
                        GiraffesHeadVar.Row--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (GiraffesHeadVar.Row < rows - 1)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col+1] = emptySpace;
                        Screen[GiraffesHeadVar.Row + 1, GiraffesHeadVar.Col+1] = giraffeHeadChar;
                        GiraffesHeadVar.Row++;
                    } break;
                default:
                    break;
            }
        }

        private static void particleMove(char[,] screen)
        {
            char emptySpace = ' ';
            char giraffeHeadChar = '@';
            char particleChar = '#';

            Random numGenerator = new Random();
            int particleY = numGenerator.Next(0, rows);
            int particleX = screen.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                char[] buffer = new char[rows];

                for (int i = 0; i < columns-1; i++)
                {
                    if (GiraffesHeadVar.Col == i)
                    {
                        if (screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col + 1]=='#') {//checks if the head of the giraffe is hit by a particle
                            isHit = true;
                            break;
                        }
                        screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = emptySpace;
                        screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col+1] = giraffeHeadChar;
                    }
                    screen[row,i] = screen[row, i + 1];
                }
                if (particleX == columns && particleY == row)
                {
                    screen[row, columns - 1] = particleChar;
                }
                else {
                    screen[row, columns - 1] = ' ';
                }
                
            }
        }

        private static void PrintMatrix(char[,] screen)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Console.Write(screen[row, col]);
                }
                Console.WriteLine();
            }
        }

        public static char[,] Screen;

        public static GiraffesHead GiraffesHeadVar = new GiraffesHead(5, 20);
        private static int rows = 20;
        private static int columns = 60;
        public static bool isHit = false;

        static void Main()
        {
            Screen = new Char[rows, columns];


            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Screen[row, col] = ' ';
                }
            }

            Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = '@';
            //lolo
            while (true) 
            {
                Console.Clear();
                if (Console.KeyAvailable) // true if a key press is available in the input stream
                {

                    ConsoleKeyInfo pressedKey = Console.ReadKey(true); // reads the next key
                    while (Console.KeyAvailable) // flushes the input stream(the pressed keys are inserted in a queue
                    {                            // and readKey empties the queue
                        Console.ReadKey(true);
                    }
                    MoveHead(pressedKey);
                }
                PrintMatrix(Screen);
                particleMove(Screen);
                if (isHit) {
                    Console.WriteLine("Game over");
                    break;
                }
                Thread.Sleep(250);
            }
            
        }

    }
}