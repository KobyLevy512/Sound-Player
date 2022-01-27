

using System;
using System.IO;
using System.Windows.Media;

namespace WindowsFormsApp1
{
    static class Player
    {
        public static MediaPlayer sound = new MediaPlayer();
        public static void Play(string url)
        {
            sound.Open(new Uri(url));
            sound.Play();
        }
    }
}
