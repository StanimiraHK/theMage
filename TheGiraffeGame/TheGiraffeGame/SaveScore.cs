using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheGiraffeGame
{
    public class SaveScore
    {
        public void ScoreToText(string playerName, string score)
        {
            string savePath = Environment.GetFolderPath(
                                 System.Environment.SpecialFolder.DesktopDirectory);
            StreamWriter Writer = new StreamWriter(@savePath);
            Writer.WriteLine("Player name: " + playerName + " score: " + score);
        }
    }
}
