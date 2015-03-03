namespace TheGiraffeGame
{
    using System;

    public class GlobalConstants
    {
        public static readonly int LevelOneScore = 10;
        public static readonly int LevelTwoScore = 20;
        public static readonly int LevelThreeScore = 30;
        public static readonly int LevelFourScore = 50;
        public static readonly int LevelFiveScore = 100;
        public static readonly int LevelSixScore = 150;

        public static readonly string LevelOneName = "Level 1";
        public static readonly string LevelTwoName = "Level 2";
        public static readonly string LevelThreeName = "Level 3";
        public static readonly string LevelFourName = "Level 4";
        public static readonly string LevelFiveName = "Level 5";
        public static readonly string LevelSixName = "Level 6";

        public static Random numGenerator = new Random();

        public const int rows = 20;
        public const int columns = 60;
        public const int DefaultLevel = 250;

        public const string GiraffesBody = @"
         @@@@@@@@@@
         @@@@@@@@@@
        @ @      @ @
       @   @    @   @     
      @     @  @     @      ";
    }
}
