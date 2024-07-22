using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private static int indexInLb = -1;

        private static List<UIElement>UIElementsList = new List<UIElement> ();

        public static void iElement(UIElement uIElement)
        {
            UIElementsList.Add(uIElement);
        }

        public static PlayingVideo Instance(string videoPath,int index)
        {
            if (instance == null)
            {
                instance = new PlayingVideo(videoPath);
            }

            if (index != indexInLb)
            {
                instance = null;

                indexInLb = index;

                instance = new PlayingVideo(videoPath);
            }


            return instance;
        }

        private PlayingVideo(string videoPath)
        {
            this.videoPath = videoPath;
            this.mediaElement = mediaElement; 




            foreach(var ui in UIElementsList)
            {
                if (ui is Button)
                {
                    Button button = (Button) ui;

                    switch(button.Content)
                    {
                        case "▶":

                            button.Click += PauseAndPlay;

                            break;
                    }
                }
            }
        }

        public void PauseAndPlay(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (isPause)
            {
                

                button.Content = "▶";

                isPause = false;

               

                return;
            }

            button.Content = "||";

            

            isPause = true;

        }
    }
}
