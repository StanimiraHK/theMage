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
        public static string playerName = string.Empty;
        public static GiraffesHead GiraffesHead;
        private static Random numGenerator = new Random();

        private static List<Particle> Particles;
        private static char[,] Screen;

        private static int rows = 20;
        private static int columns = 60;

        public static bool isHit = false;

        private static int ApplesEaten = 0;
        private static int level = 200;

        private static string timeAlive;
        private static string GiraffesBody = @"
         @@@@@@@@@@
         @@@@@@@@@@
        @ @      @ @
       @   @    @   @     
      @     @  @     @      ";

        private static void ChooseLevel()
        {
            Console.Clear();
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

            ShowMenu();
        }

        private static void LoadGame()
        {
            throw new NotImplementedException();
        }

        private static void Leaderbord()
        {
            throw new NotImplementedException();
        }

        private static void CustomizeGiraffe()
        {
            throw new NotImplementedException();
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }

        private static void ShowMenu()
        {
            Console.Clear();

            int choice = 0;
            Console.SetCursorPosition(25, 8);
            Console.WriteLine(@"MENU:
                        - - - - - - - -
                         1. New Game
                         2. Load Game (Not implemented yet)
                         3. Choose difficulty
                         4. Leaderbord(Not implemented yet)
                         5. Customize giraffe(Not implemented yet)
                         6. Exit");

            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: Console.Clear(); PlayGame(); break;
                case 2: Console.Clear(); LoadGame();  break;
                case 3: Console.Clear(); ChooseLevel();  break;
                case 4: Console.Clear(); Leaderbord(); break;
                case 5: Console.Clear(); CustomizeGiraffe(); break;
                case 6: Console.Clear(); Exit(); break;
                default:
                    break;
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
            SetupConsole();
            ShowMenu();
        }

        private static void SetupConsole()
        {
            Console.SetWindowSize(70, 27);
            SetDefaultForegroundColor();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        private static void PlayGame()
        {
            Particles = new List<Particle>();
            Screen = new Char[rows, columns];
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
                MoveParticles(Particles);
                MoveNeck(Screen);
                ShowRealtimeScore(ApplesEaten);

                Console.SetCursorPosition(20, 19);
                Console.WriteLine(GiraffesBody);

                if (isHit)
                {
                    stopwatch.Stop();
                    EmptyParticlesList();
                    Console.Clear();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Your ate {0} apples!", ApplesEaten);

                    timeAlive = string.Format("{0}{1}{2}",
                        stopwatch.Elapsed.Hours == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 hour" : stopwatch.Elapsed.Hours + " hours"),
                        stopwatch.Elapsed.Minutes == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 minute" : stopwatch.Elapsed.Minutes + " minutes"),
                        stopwatch.Elapsed.Seconds == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 second" : stopwatch.Elapsed.Seconds + " seconds"));

                    SaveScoreToTextFile();
                    ShowMenu();
                    return;
                }

                Thread.Sleep(level);
            }
        }

        private static void EmptyParticlesList()
        {
            Particles = new List<Particle>();
        }

        private static void SaveScoreToTextFile()
        {
            //Saving the score to text file ->>>
            Console.WriteLine("Your managed to stay alive for: {0}", timeAlive);
            Console.WriteLine(@"What is your name, you brave GiraffeWarrior? (score will be saved in TheGiraffeGame\bin\Debug directory)");
            playerName = Console.ReadLine();
            
            string savePath = Path.Combine(Environment.CurrentDirectory, "Score.txt"); //save to current directory
            using (StreamWriter Writer = new StreamWriter(@savePath))
            {
                Writer.WriteLine("Player name: " + playerName + " | score: " + timeAlive);
            }

            Console.WriteLine(@"Your score has been saved on your TheGiraffeGame\bin\Debug directory - {0}.txt", playerName);

            Thread.Sleep(2000);
        }

        private static void SetDefaultForegroundColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }
}
