using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using TheGiraffeGame;

namespace CursorTest
{
    class StartingPoint
    {

        private static void MoveHead(ConsoleKeyInfo keyinfo, char[,] screen)
        {
            char giraffeHeadChar = '@';

            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHead.Row > 2)
                    {
                        Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
                        Console.Write(' ');

                        GiraffesHead.Row--;
                        Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
                        Console.Write(giraffeHeadChar);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (GiraffesHead.Row < rows - 1)
                    {
                        Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
                        Console.Write(' ');

                        GiraffesHead.Row++;
                        Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
                        Console.Write("@");
                    } break;
                default:
                    break;
            }
        }

        private static void MoveNeck(char[,] screen)
        {
            char giraffeNeckChar = 'M';
            for (int i = GiraffesHead.Row + 1; i < rows; i++)
            {
                Console.SetCursorPosition(GiraffesHead.Col - 3, i);
                Console.Write(giraffeNeckChar);
                Console.SetCursorPosition(GiraffesHead.Col - 2, i);
                Console.Write(giraffeNeckChar);
            }
            Console.SetCursorPosition(GiraffesHead.Col - 6, GiraffesHead.Row - 3);
            Console.Write("      ");
            Console.SetCursorPosition(GiraffesHead.Col - 1, GiraffesHead.Row - 1);
            Console.Write(" ");
            Console.SetCursorPosition(GiraffesHead.Col - 1, GiraffesHead.Row + 1);
            Console.Write(" ");
            Console.SetCursorPosition(GiraffesHead.Col - 6, GiraffesHead.Row - 2);
            Console.Write(@"  ^_^
                 OO
                 MMM");
        }

        private static void GenerateParticle(List<Particle> particles)
        {
            char particleChar = '#';

            int particleRow = numGenerator.Next(0, rows);
            particles.Add(new Particle(particleRow, columns - 1, particleChar));
        }

        private static void MoveParticles(List<Particle> particles)
        {
            GenerateParticle(particles);

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].getCol() > 0)
                {
                    Console.SetCursorPosition(particles[i].getCol(), particles[i].getRow());
                    Console.Write(' ');

                    int col = particles[i].getCol();
                    col--;
                    particles[i].setCol(col);
                    if (particles[i].getRow() == GiraffesHead.Row && particles[i].getCol() == GiraffesHead.Col)
                    {
                        isHit = true;
                        break;
                    }
                    Console.SetCursorPosition(particles[i].getCol(), particles[i].getRow());
                    Console.Write(particles[i].getSymbol());
                }
                else
                {
                    Console.SetCursorPosition(particles[i].getCol(), particles[i].getRow());
                    Console.Write(' ');
                    particles.Remove(particles[i]);
                }
            }
        }

        private static void PrintHead()
        {
            Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
            Console.Write('@');
        }

        public static GiraffesHead GiraffesHead;
        private static int rows = 20;
        private static int columns = 60;
        public static bool isHit = false;
        private static Random numGenerator = new Random();

        static void Main()
        {
            Console.SetWindowSize(70, 25);
            Console.ForegroundColor = ConsoleColor.Yellow;

            List<Particle> particles = new List<Particle>();
            char[,] Screen = new Char[rows, columns];
            GiraffesHead = new GiraffesHead(5, 20);

            //Creating and starting a stopwatch as a way to get score
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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

                PrintHead();
                MoveParticles(particles);
                MoveNeck(Screen);

                if (isHit)
                {
                    Console.Clear();
                    Console.WriteLine("Game over");
                    stopwatch.Stop();
                    string score = stopwatch.Elapsed.ToString();
                    //Saving the score to text file ->>>
                    Console.WriteLine("Your managed to stay alive for: {0}", score);
                    Console.WriteLine("What is your name, you brave GiraffeWarrior?");
                    string player = Console.ReadLine();
                    Console.WriteLine(@"Your score has been saved on your TheGiraffeGame\bin\Debug directory - {0}.txt", player);
                    Console.WriteLine("Your score has been saved on your TheGiraffeGame\\bin\\Debug directory - Score.txt");

                    string savePath = Path.Combine(Environment.CurrentDirectory, "Score.txt"); //save to current directory
                    StreamWriter Writer = new StreamWriter(@savePath);
                    Writer.WriteLine("Player name: " + player + " | score: " + score);
                    Writer.Close();
                    break;
                }
                Thread.Sleep(250);
            }

        }
    }
}
