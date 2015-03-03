namespace TheGiraffeGame
{
    using System;
    public interface IMusicPlayer
    {
        void open(string file,bool isLooping = true);
        void pause();
        void play();
        void stop();
    }
}
