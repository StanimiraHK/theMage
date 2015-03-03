namespace CursorTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using TheGiraffeGame;

    class StartingPoint
    {
        public static string playerName = string.Empty;
        public static GiraffesHead GiraffesHead;
        private static Random numGenerator = new Random();

        private static List<Particle> Particles;

        private static int rows = 20;
        private static int columns = 60;

        public static bool isHit = false;

        private static int score = 0;
        private static int ApplesEaten = 0;
        private static int level = 250;
        private static String currentLevel = Level.LevelOneName;

        private static string timeAlive;
        private static string GiraffesBody = @"
         @@@@@@@@@@
         @@@@@@@@@@
        @ @      @ @
       @   @    @   @     
      @     @  @     @      ";
        private static string giraffesColor = "Yellow";
        private static string defaultColor = "Yellow";


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
                case 1: level = 250; break;
                case 2: level = 150; break;
                case 3: level = 100; break;
            }

            ShowMenu();
        }

        private static void LevelScoring(int level)
        {
            if (level <= 0)
            {
                level = 0;

                if (level == 0)
                {
                    currentLevel = Level.LevelSixName;
                    score += Level.LevelSixScore;
                }

            }
            else if (level < 50)
            {
                currentLevel = Level.LevelFiveName;
                score += Level.LevelFiveScore;
            }
            else if (level < 100)
            {
                currentLevel = Level.LevelFourName;
                score += Level.LevelFourScore;
            }
            else if (level < 150)
            {
                currentLevel = Level.LevelThreeName;
                score += Level.LevelThreeScore;
            }
            else if (level < 200)
            {
                currentLevel = Level.LevelTwoName; ;
                score += Level.LevelTwoScore;
            }
            else
            {
                currentLevel = Level.LevelOneName;
                score += Level.LevelOneScore;
            }
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
            Console.WriteLine("Choose your favorite color from all this :");
            Console.WriteLine("Yellow");
            Console.WriteLine("Cyan");
            Console.WriteLine("Blue");
            Console.WriteLine("Green");
            Console.WriteLine("Red");
            Console.WriteLine("Gray");
            giraffesColor = Console.ReadLine();
            SetDefaultForegroundColor(giraffesColor);
            Console.Clear();
            PlayGame();
        }

        private static void Exit()
        {
            Console.Clear();
            Console.SetCursorPosition(10, Console.WindowHeight / 2);
            Console.CursorVisible = true;

            Console.Write("Are you sure you want to exit the game (y/n)? ");

            string answer = Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.CursorVisible = false;
                ShowMenu();
            }
        }

        private static void InteractiveMenu()
        {
            char arrowSymbol = '*';
            int consoleCol = 23;
            int consoleRow = 10;
            while (true)
            {

                Console.CursorVisible = false;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.WriteLine(arrowSymbol);
                Console.SetCursorPosition(consoleCol, consoleRow);
                ConsoleKeyInfo arrow = Console.ReadKey();

                if (consoleRow > 10 || consoleRow < 16)
                {

                    if (arrow.Key == ConsoleKey.UpArrow)
                    {
                        Console.SetCursorPosition(consoleCol, consoleRow);
                        Console.WriteLine(" ");

                        consoleRow--;
                    }
                    else if (arrow.Key == ConsoleKey.DownArrow)
                    {
                        Console.SetCursorPosition(consoleCol, consoleRow);
                        Console.WriteLine(" ");
                        consoleRow++;
                    }
                }
                if (consoleRow < 10)
                {
                    consoleRow = 15;
                }
                if (consoleRow > 15)
                {
                    consoleRow = 10;
                }
                if (arrow.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            switch (consoleRow - 9)
            {
                case 1: Console.Clear(); PlayGame(); break;
                case 2: Console.Clear(); LoadGame(); break;
                case 3: Console.Clear(); ChooseLevel(); break;
                case 4: Console.Clear(); Leaderbord(); break;
                case 5: Console.Clear(); CustomizeGiraffe(); break;
                case 6: Console.Clear(); Exit(); break;
                default:
                    break;
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();

            Console.SetCursorPosition(25, 8);
            Console.WriteLine(@"MENU:
                        - - - - - - - -
                         New Game
                         Load Game (Not implemented yet)
                         Choose difficulty
                         Leaderbord(Not implemented yet)
                         Customize giraffe
                         Exit");
            InteractiveMenu();
        }

        private static void MoveHead(ConsoleKeyInfo keyinfo)
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

        private static void MoveNeck()
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

            int particleRow = numGenerator.Next(3, rows);
            particles.Add(new Particle(particleRow, columns - 1, isGoodParticle));
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
                            level -= 5;
                            LevelScoring(level);
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
            SetDefaultForegroundColor(giraffesColor);
        }

        private static void PrintHead()
        {
            Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
            Console.Write('@');
        }

        private static void ShowRealtimeScore(int apples)
        {
            SetDefaultForegroundColor(defaultColor);
            Console.SetCursorPosition(45, 22);
            Console.WriteLine(">>>  {0}  <<<", currentLevel);
            Console.SetCursorPosition(45, 23);
            Console.WriteLine(">>>  Score: {0}  <<<", score);
            Console.SetCursorPosition(45, 24);
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
            SetDefaultForegroundColor("Yellow");
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        private static void PlayGame()
        {
            Particles = new List<Particle>();
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

                    MoveHead(pressedKey);
                }


                SetDefaultForegroundColor(giraffesColor);
                PrintHead();
                MoveParticles(Particles);
                MoveNeck();
                Console.SetCursorPosition(20, 19);
                Console.WriteLine(GiraffesBody);
                ShowRealtimeScore(ApplesEaten);

                if (isHit)
                {
                    isHit = false;
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
            Console.WriteLine("You managed to stay alive for: {0}", timeAlive);
            Console.WriteLine(@"What is your name, you brave GiraffeWarrior? (score will be saved in TheGiraffeGame\bin\Debug directory)");
            playerName = Console.ReadLine();

            string savePath = Path.Combine(Environment.CurrentDirectory, "Score.txt"); //save to current directory
            using (StreamWriter Writer = new StreamWriter(@savePath))
            {
                Writer.WriteLine("Player name: " + playerName + " | score: " + timeAlive);
            }

            Console.WriteLine(@"Your score has been saved on your TheGiraffeGame\bin\Debug directory - {0}.txt", playerName);

            //            Thread.Sleep(2000);
        }

        private static void SetDefaultForegroundColor(string color)
        {
            switch (color)
            {
                case "Yellow":
                case "yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "green":
                case "Green": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Red":
                case "red": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Blue":
                case "blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "Cyan":
                case "cyan": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Gray":
                case "gray": Console.ForegroundColor = ConsoleColor.Gray; break;
            }
        }
    }
}
