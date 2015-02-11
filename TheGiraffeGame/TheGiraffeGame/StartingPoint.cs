namespace TheGiraffeGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StartingPoint
    {
        public static string[,] Screen;

        public static GiraffesHead GiraffesHeadVar = new GiraffesHead(5, 5);

        static void Main()
        {
            Screen = new string[10, 10];


            for (int row = 0; row < Screen.GetLength(0); row++)
            {
                for (int col = 0; col < Screen.GetLength(1); col++)
                {
                    Screen[row, col] = " ";
                }
            }

            Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = "@";

            string[,] matrix = new string[,]
                {
                   { "1", "2"},
                   { "3", "4"}
                };

            ConsoleKeyInfo keyinfo;

            do
            {
                PrintMatrix(Screen);
                keyinfo = Console.ReadKey();
                MoveHead(keyinfo);
                Console.Clear();
            }
            while (keyinfo.Key != ConsoleKey.X);
        }

        private static void MoveHead(ConsoleKeyInfo keyinfo)
        {
            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (GiraffesHeadVar.Row > 0)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = " ";
                        Screen[GiraffesHeadVar.Row - 1, GiraffesHeadVar.Col] = "@";
                        GiraffesHeadVar.Row = GiraffesHeadVar.Row - 1;
                    }
                    break;

                case ConsoleKey.DownArrow: 
                    if (GiraffesHeadVar.Row < Screen.GetLength(0)-1)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = " ";
                        Screen[GiraffesHeadVar.Row + 1, GiraffesHeadVar.Col] = "@";
                        GiraffesHeadVar.Row = GiraffesHeadVar.Row + 1;
                    } break;

                case ConsoleKey.LeftArrow: if (GiraffesHeadVar.Col > 0)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = " ";
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col - 1] = "@";
                        GiraffesHeadVar.Col = GiraffesHeadVar.Col - 1;
                    } break;

                case ConsoleKey.RightArrow: if (GiraffesHeadVar.Col < Screen.GetLength(1)-1)
                    {
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col] = " ";
                        Screen[GiraffesHeadVar.Row, GiraffesHeadVar.Col + 1] = "@";
                        GiraffesHeadVar.Col = GiraffesHeadVar.Col + 1;
                    } break;
                default:
                    break;
            }
        }

        private static void PrintMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }


    }
}
