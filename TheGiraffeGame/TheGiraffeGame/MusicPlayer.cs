namespace TheGiraffeGame
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using WMPLib;

    public class MusicPlayer : TheGiraffeGame.IMusicPlayer
    {
        private WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
        
        public void open(string file, bool isLooping = true)
        {
            wplayer.settings.autoStart = false;
            wplayer.settings.setMode("loop", isLooping);
            wplayer.URL = file;
        }

        public void play()
        {
            wplayer.controls.play();
        }

        public void pause()
        {
            wplayer.controls.pause();
        }


        public void stop()
        {
            wplayer.controls.stop();
        }
    }
}
