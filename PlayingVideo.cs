using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Free_video_player
{
    internal class PlayingVideo
    {
        private static PlayingVideo instance;

        private string videoPath;

        private bool isPause = true;

        private MediaElement mediaElement;

        public static PlayingVideo Instance(string videoPath,MediaElement mediaElement)
        {
            if (instance == null)
            {
                instance = new PlayingVideo(videoPath,mediaElement);
            }



            return instance;
        }

        private PlayingVideo(string videoPath, MediaElement mediaElement)
        {
            this.videoPath = videoPath;
            this.mediaElement = mediaElement;
        }

        public void PauseAndPlay(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (isPause)
            {
                

                button.Content = "▶";

                isPause = false;

                mediaElement.Play();

                return;
            }

            button.Content = "||";

            mediaElement.Pause();

            isPause = true;

        }
    }
}
