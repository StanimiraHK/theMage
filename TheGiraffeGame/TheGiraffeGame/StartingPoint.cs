﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TheGiraffeGame
{
    public class StartingPoint
    {

        private static void MoveHead(ConsoleKeyInfo keyinfo, char[,] screen)
        {
            char giraffeHeadChar = '@';

            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHead.Row > 2)
                    {
                        GiraffesHead.Row--;
                        screen[GiraffesHead.Row, GiraffesHead.Col] = giraffeHeadChar;

                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (GiraffesHead.Row < rows - 1)
                    {
                        GiraffesHead.Row++;
                        screen[GiraffesHead.Row, GiraffesHead.Col] = giraffeHeadChar;

                    } break;
                default:
                    break;
            }
        }

        private static void MoveNeck(char[,] screen)
        {
            for (int i = GiraffesHead.Row; i < screen.GetLength(0); i++)
            {
                screen[i, GiraffesHead.Col - 3] = 'M';
                screen[i, GiraffesHead.Col - 2] = 'M';
            }
            screen[GiraffesHead.Row, GiraffesHead.Col - 1] = 'M';
            screen[GiraffesHead.Row - 1, GiraffesHead.Col - 2] = 'O';
            screen[GiraffesHead.Row - 1, GiraffesHead.Col - 3] = 'O';
            screen[GiraffesHead.Row - 2, GiraffesHead.Col - 2] = '^';
            screen[GiraffesHead.Row - 2, GiraffesHead.Col - 3] = '_';
            screen[GiraffesHead.Row - 2, GiraffesHead.Col - 4] = '^';
        }

        private static void GenerateParticle(List<Particle> particles) {
            char particleChar = '#';

            Random numGenerator = new Random();
            int particleRow = numGenerator.Next(0, rows);
            particles.Add(new Particle(particleRow, columns - 1, particleChar));
        }

        private static void MoveParticles(char[,] screen, List<Particle> particles)
        {
            GenerateParticle(particles);

            clearScreen(screen);

            for (int i = 0; i < particles.Count; i++)
            {
                int particleCol = particles[i].getCol();
                int particleRow = particles[i].getRow();

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
            screen[GiraffesHead.Row, GiraffesHead.Col] = '@';
        }


        public static GiraffesHead GiraffesHead;
        private static int rows = 20;
        private static int columns = 60;
        public static bool isHit = false;

        static void Main()
        {
            List<Particle> particles = new List<Particle>();
            char[,] Screen = new Char[rows, columns];
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
                    MoveHead(pressedKey, Screen);
                }
                MoveParticles(Screen, particles);
                MoveNeck(Screen);
                PrintMatrix(Screen);
                Console.WriteLine(@"            @@@@@@
           @    @ @
          @    @   @
         @    @     @
    ");
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