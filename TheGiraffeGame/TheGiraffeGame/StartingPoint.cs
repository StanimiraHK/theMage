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
                    if (GiraffesHeadVar.Col < columns - 1)
                    {
                        Screen[GiraffesHeadVar.Col, GiraffesHeadVar.Row] = emptySpace;
                        Screen[GiraffesHeadVar.Col - 1, GiraffesHeadVar.Row] = giraffeHeadChar;
                        GiraffesHeadVar.Col--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (GiraffesHeadVar.Col > 0)
                    {
                        Screen[GiraffesHeadVar.Col, GiraffesHeadVar.Row] = emptySpace;
                        Screen[GiraffesHeadVar.Col + 1, GiraffesHeadVar.Row] = giraffeHeadChar;
                        GiraffesHeadVar.Col++;
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
            int particleY = numGenerator.Next(0, columns - 2);
            int particleX = screen.GetLength(1);

            for (int col = 0; col < columns; col++)
            {
                char[] buffer = new char[rows];

                for (int i = 0; i < rows-1; i++)
                {
                    if (GiraffesHeadVar.Row == i)
                    {
                        screen[GiraffesHeadVar.Col, GiraffesHeadVar.Row] = emptySpace;
                        screen[GiraffesHeadVar.Col, GiraffesHeadVar.Row+1] = giraffeHeadChar;
                    }
                    screen[col,i] = screen[col, i + 1];
                }
                if (particleX == rows && particleY == col)
                {
                    screen[col, rows - 1] = particleChar;
                }
                else {
                    screen[col, rows - 1] = ' ';
                }
                
            }
        }

        private static void PrintMatrix(char[,] screen)
        {
            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Console.Write(screen[col, row]);
                }
                Console.WriteLine();
            }
        }

        public static char[,] Screen;

        public static GiraffesHead GiraffesHeadVar = new GiraffesHead(20, 5);
        private static int rows = 60;
        private static int columns = 20;

        static void Main()
        {
            Screen = new Char[columns, rows];


            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Screen[col, row] = ' ';
                }
            }

            Screen[GiraffesHeadVar.Col, GiraffesHeadVar.Row] = '@';
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
                Thread.Sleep(250);
            }
            
        }

    }
}