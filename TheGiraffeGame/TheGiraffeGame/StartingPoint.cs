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
        public static string playerName = "Anonymous";
        public static GiraffesHead GiraffesHead;
        private static Random numGenerator = new Random();

        private static List<Particle> Particles;

        public static bool isHit = false;

        private static int Score = 0;
        private static int ApplesEaten = 0;

        private static int level = GlobalConstants.DefaultLevel;
        private static String currentLevel = Level.LevelOneName;

        private static string timeAlive;
        private static Stopwatch stopwatch;

        private static string giraffesColor = "Yellow";
        private static ConsoleColor defaultColor = ConsoleColor.Yellow;


        private static void ChooseLevel()
        {
            Console.Clear();
            var levelOptions = new string[] { "Easy", "Medium", "Hard", "Impossibru" };
            PrintMenu("Choose Level: ", levelOptions);
            int choice = InteractiveMenu(levelOptions.Length);

            switch (choice + 1)
            {
                case 1: level = 250; break;
                case 2: level = 150; break;
                case 3: level = 100; break;
                case 4: level = 0; break;
                default: break;
            }

            ShowMainMenu();
        }

        private static void LevelScoring(int level)
        {
            if (level <= 0)
            {
                level = 0;

                if (level == 0)
                {
                    currentLevel = Level.LevelSixName;
                    Score += Level.LevelSixScore;
                }

            }
            else if (level < 50)
            {
                currentLevel = Level.LevelFiveName;
                Score += Level.LevelFiveScore;
            }
            else if (level < 100)
            {
                currentLevel = Level.LevelFourName;
                Score += Level.LevelFourScore;
            }
            else if (level < 150)
            {
                currentLevel = Level.LevelThreeName;
                Score += Level.LevelThreeScore;
            }
            else if (level < 200)
            {
                currentLevel = Level.LevelTwoName; ;
                Score += Level.LevelTwoScore;
            }
            else
            {
                currentLevel = Level.LevelOneName;
                Score += Level.LevelOneScore;
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

        private static void PrintMenu(string menuMessage, string[] menuOptions)
        {
            int cursorRow = 8;
            Console.SetCursorPosition(25, cursorRow);
            Console.WriteLine(menuMessage);
            cursorRow++;

            Console.SetCursorPosition(25, cursorRow);
            Console.WriteLine();
            cursorRow++;
            foreach (var option in menuOptions)
            {
                Console.SetCursorPosition(25, cursorRow);
                Console.WriteLine(option);
                cursorRow++;
            }
        }

        private static void ShowCustomizeGiraffeMenu()
        {
            string[] colorOptions = new string[] { "Yellow", "Cyan", "Blue", "Green", "Red", "Gray" };
            PrintMenu("Choose your favorite color from all this :", colorOptions);

            int choice = InteractiveMenu(colorOptions.Length);

            giraffesColor = colorOptions[choice];
            SetForegroundColor(giraffesColor);
            Console.Clear();
            ShowMainMenu();
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
                ShowMainMenu();
            }
        }

        private static int InteractiveMenu(int menuOptionsCount)
        {
            char arrowSymbol = '*';
            int consoleCol = 23;
            int startConsoleRow = 10;
            int endConsoleRow = startConsoleRow + menuOptionsCount - 1;
            int consoleRow = startConsoleRow;

            while (true)
            {

                Console.CursorVisible = false;
                Console.SetCursorPosition(consoleCol, consoleRow);
                Console.WriteLine(arrowSymbol);
                Console.SetCursorPosition(consoleCol, consoleRow);
                ConsoleKeyInfo arrow = Console.ReadKey();

                if (consoleRow > startConsoleRow || consoleRow < endConsoleRow)
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
                if (consoleRow < startConsoleRow)
                {
                    consoleRow = endConsoleRow;
                }
                if (consoleRow > endConsoleRow)
                {
                    consoleRow = startConsoleRow;
                }
                if (arrow.Key == ConsoleKey.Enter)
                {
                    return consoleRow - startConsoleRow;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            SetDefaultForegroundColor();
            
            var menuOptions = new string[]{ "New Game",
                                            "Load Game (Not implemented yet)",
                                            "Choose difficulty",
                                            "Leaderbord(Not implemented yet)",
                                            "Customize giraffe",
                                            "Exit"};

            PrintMenu("Menu: ", menuOptions);

            int choice = InteractiveMenu(menuOptions.Length);

            Console.Clear();
            switch (choice + 1)
            {
                case 1: PlayGame(); break;
                case 2: LoadGame(); break;
                case 3: ChooseLevel(); break;
                case 4: Leaderbord(); break;
                case 5: ShowCustomizeGiraffeMenu(); break;
                case 6: Exit(); break;
                default:
                    break;
            }
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
                    if (GiraffesHead.Row < GlobalConstants.rows - 1)
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
            for (int i = GiraffesHead.Row + 1; i < GlobalConstants.rows; i++)
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

            int particleRow = numGenerator.Next(3, GlobalConstants.rows);
            particles.Add(new Particle(particleRow, GlobalConstants.columns - 1, isGoodParticle));
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
                            if (level < 0)
                            {
                                level = 0;
                            }

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
            SetForegroundColor(giraffesColor);
        }

        private static void PrintHead()
        {
            Console.SetCursorPosition(GiraffesHead.Col, GiraffesHead.Row);
            Console.Write('@');
        }

        private static void ShowRealtimeScore(int apples)
        {
            SetDefaultForegroundColor();
            Console.SetCursorPosition(45, 22);
            Console.WriteLine(">>>  {0}  <<<", currentLevel);
            Console.SetCursorPosition(45, 23);
            Console.WriteLine(">>>  Score: {0}  <<<", Score);
            Console.SetCursorPosition(45, 24);
            Console.WriteLine(">>>  Apples eaten: {0}  <<<", apples);

        }

        static void Main()
        {
            SetupConsole();
            ShowMainMenu();
        }

        private static void SetupConsole()
        {
            Console.SetWindowSize(70, 27);
            SetForegroundColor("Yellow");
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        private static void PlayGame()
        {
            Particles = new List<Particle>();
            GiraffesHead = new GiraffesHead(5, 20);

            //Creating and starting a stopwatch as a way to get score
            stopwatch = new Stopwatch();
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


                SetForegroundColor(giraffesColor);
                PrintHead();
                MoveParticles(Particles);
                MoveNeck();
                Console.SetCursorPosition(20, 19);
                Console.WriteLine(GlobalConstants.GiraffesBody);
                ShowRealtimeScore(ApplesEaten);

                if (isHit) // Game over
                {
                    stopwatch.Stop();

                    Console.Clear();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Your ate {0} apples!", ApplesEaten);

                    timeAlive = ReturnFormatedTimeString(stopwatch);

                    SaveScoreToTextFile();
                    ResetAllVariables();
                    ShowMainMenu();

                    return;
                }

                Thread.Sleep(level);
            }
        }

        private static void ResetAllVariables()
        {
            isHit = false;
            EmptyParticlesList();
            stopwatch.Reset();
            ApplesEaten = 0;
            Score = 0;
            level = GlobalConstants.DefaultLevel;
        }

        private static string ReturnFormatedTimeString(Stopwatch stopwatch)
        {
            return string.Format("{0}{1}{2}",
               stopwatch.Elapsed.Hours == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 hour" : stopwatch.Elapsed.Hours + " hours"),
               stopwatch.Elapsed.Minutes == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 minute" : stopwatch.Elapsed.Minutes + " minutes"),
               stopwatch.Elapsed.Seconds == 0 ? string.Empty : (stopwatch.Elapsed.Hours == 1 ? "1 second" : stopwatch.Elapsed.Seconds + " seconds"));
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

        private static void SetDefaultForegroundColor()
        { Console.ForegroundColor = defaultColor;
        }

        private static void SetForegroundColor(string color)
        {
            switch (color.ToLower())
            {
                case "yellow": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Green": Console.ForegroundColor = ConsoleColor.Green; break;
                case "red": Console.ForegroundColor = ConsoleColor.Red; break;
                case "blue": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "cyan": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "gray": Console.ForegroundColor = ConsoleColor.Gray; break;

                default: break;
            }
        }
    }
}
