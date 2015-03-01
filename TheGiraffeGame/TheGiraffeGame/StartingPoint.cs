namespace CursorTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Diagnostics;
    using System.IO;
    using TheGiraffeGame;

    class StartingPoint
    {
        public static string player = string.Empty;
        public static GiraffesHead GiraffesHead;
        private static Random numGenerator = new Random();

        private static int rows = 20;
        private static int columns = 60;

        public static bool isHit = false;

        private static int ApplesEaten = 0;
        private static int level = 0;

        private static string timeAlive;
        private static string GiraffesBody = @"
         @@@@@@@@@@
         @@@@@@@@@@
        @ @      @ @
       @   @    @   @     
      @     @  @     @      ";

        private static void ChooseLevel()
        {
            level = 0;
            Console.SetCursorPosition(25, 8);
            Console.WriteLine(@"CHOOSE LEVEL:
                        - - - - - - - -
                         EASY      - 1
                         MEDIUM    - 2
                         DIFFICULT - 3");
            Console.SetCursorPosition(25, 13);
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1: level = 200; break;
                case 2: level = 150; break;
                case 3: level = 100; break;
            }

        }
        private static void NewGame()
        {
            
        }
        private static void LoadGame()
        {
 
        }
        private static void Leaderbord()
        {
 
        }
        private static void CostomizeGiraffe()
        {
 
        }
        private static void Exit()
        {
 
        }
        private static void Menu()
        {
            int choise = 0;
            Console.SetCursorPosition(25, 8);
            Console.WriteLine(@"MENU:
                        - - - - - - - -
                         1. New Game
                         2. Load Game
                         3.Chose difficulty
                         4.Leaderbord
                         5.Costomize giraffe
                         6.Exit");
            choise = int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1: NewGame(); break;
                case 2: LoadGame(); break;
                case 3: ChooseLevel(); break;
                case 4: Leaderbord(); break;
                case 5: CostomizeGiraffe(); break;
                case 6: Exit(); break;

            }
        }

        private static void MoveHead(ConsoleKeyInfo keyinfo, char[,] screen)
        {
            char giraffeHeadChar = '@';

            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHead.Row > 3)
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
            bool isGoodParticle = (numGenerator.Next() % 5 == 0 ? true : false);

            char particleChar = isGoodParticle ? 'Ơ' : '¤';//'#';

            int particleRow = numGenerator.Next(3, rows);
            particles.Add(new Particle(particleRow, columns - 1, particleChar, isGoodParticle));
        }

        private static void MoveParticles(List<Particle> particles)
        {
            GenerateParticle(particles);

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].getCol() > GiraffesHead.Col - 1)
                {
                    Console.SetCursorPosition(particles[i].getCol(), particles[i].getRow());
                    Console.Write(' ');

                    int col = particles[i].getCol();
                    col--;
                    particles[i].setCol(col);

                    if (particles[i].getRow() == GiraffesHead.Row && particles[i].getCol() == GiraffesHead.Col)
                    {
                        if (particles[i].IsGood)
                        {
                            ApplesEaten++;
                            particles.Remove(particles[i]);
                            Console.BackgroundColor = ConsoleColor.Red;
                            ShowRealtimeScore(ApplesEaten);
                            Console.ResetColor();
                        }
                        else
                        {
                            isHit = true;
                            break;
                        }
                    }

                    DrawParticle(particles[i]);
                }
                else
                {
                    Console.SetCursorPosition(particles[i].getCol(), particles[i].getRow());
                    Console.Write(' ');
                    particles.Remove(particles[i]);
                }
            }
        }

        private static void DrawParticle(Particle particle)
        {
            Console.SetCursorPosition(particle.getCol(), particle.getRow());

            // If the particle is good it will be green, else it will be red
            Console.ForegroundColor = particle.IsGood ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(particle.getSymbol());
            SetDefaultForegroundColor();
        }

        private static void PrintHead()
        {
            Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
            Console.Write('@');
        }

        private static void ShowRealtimeScore(int apples)
        {
            Console.SetCursorPosition(45, 22);
            Console.WriteLine(">>>  Apples eaten: {0}  <<<", apples);
        }

        static void Main()
        {
            Console.SetWindowSize(70, 27);
            SetDefaultForegroundColor();
            Console.OutputEncoding = System.Text.Encoding.Unicode;


            List<Particle> particles = new List<Particle>();
            char[,] Screen = new Char[rows, columns];
            GiraffesHead = new GiraffesHead(5, 20);

            //Creating and starting a stopwatch as a way to get score
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ChooseLevel();

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
                ShowRealtimeScore(ApplesEaten);
                Console.SetCursorPosition(20, 19);
                Console.WriteLine(GiraffesBody);

                if (isHit)
                {
                    stopwatch.Stop();
                    Console.Clear();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Your ate {0} apples!", ApplesEaten);

                    timeAlive = string.Format("{0}{1}{2}",
                        stopwatch.Elapsed.Hours == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 hour" : stopwatch.Elapsed.Hours + " hours"),
                        stopwatch.Elapsed.Minutes == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 minute" : stopwatch.Elapsed.Minutes + " minutes"),
                        stopwatch.Elapsed.Seconds == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 second" : stopwatch.Elapsed.Seconds + " seconds"));

                    SaveScoreToTextFile();
                }

                Thread.Sleep(level);
            }

        }

        private static void SaveScoreToTextFile()
        {
            //Saving the score to text file ->>>
            Console.WriteLine("Your managed to stay alive for: {0}", timeAlive);
            Console.WriteLine("What is your name, you brave GiraffeWarrior?");
            player = Console.ReadLine();
            Console.WriteLine(@"Your score has been saved on your TheGiraffeGame\bin\Debug directory - {0}.txt", player);
            Console.WriteLine("Your score has been saved on your TheGiraffeGame\\bin\\Debug directory - Score.txt");

            string savePath = Path.Combine(Environment.CurrentDirectory, "Score.txt"); //save to current directory
            using (StreamWriter Writer = new StreamWriter(@savePath))
            {
                Writer.WriteLine("Player name: " + player + " | score: " + timeAlive);
            }
        }

        private static void SetDefaultForegroundColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }
}
