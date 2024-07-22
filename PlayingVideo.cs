using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Free_video_player
{
    internal class PlayingVideo
    {
        private static PlayingVideo instance;

        public string videoPath { get; }

        private bool isPause = true;

        private MediaElement mediaElement;

        private static int indexInLb = -1;

        private static List<UIElement>UIElementsList = new List<UIElement> ();

        private Slider volumeControl, videoControl;


        

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

                            button.Click += PauseAndPlay_Click;

                            break;
                        case "►►":
                        case "◄◄":

                            button.Click += SkipAndForward_Click;

                            break;
                    }
                }
                
                if (ui is MediaElement)
                {
                    mediaElement =(MediaElement)ui;
                }
               if (ui is Slider)
               {
                    var slider = (Slider)ui;

                    slider.IsEnabled = true;

                    if (slider.Name == "VolumeControlSlr")
                    {
                        volumeControl = slider;

                        volumeControl.ValueChanged += VolumeSlider_Change;


                    }
                    else
                    {
                        videoControl = slider;

                        videoControl.ValueChanged += VideoSlider_Change;
                    }


               }

            }
        }

        public void PauseAndPlay_Click(object sender, RoutedEventArgs e)
        {

            if (mediaElement.Source != null)
            {


                Button button = sender as Button;

                if (isPause)
                {


                    button.Content = "||";

                    isPause = false;

                    mediaElement.Play();

                    return;
                }

                button.Content = "▶";

                mediaElement.Pause();

                isPause = true;
            }

        }
        public void SkipAndForward_Click(object sender, RoutedEventArgs e)
        {
            

            Button button = sender as Button;

            switch(button.Content)
            {
                case "►►":
                    break;
                default:
                    break;
            }
             
            
        }
        

        public void VolumeSlider_Change(object sender, RoutedEventArgs e)
        {

        }


        public void VideoSlider_Change(object sender, RoutedEventArgs e)
        {

        }
    }
}
