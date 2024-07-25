using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Free_video_player
{
    internal class AllVideosPlayer : AbstractPlayer
    {
        private static AllVideosPlayer instance;
        public string videoPath { get; }

        private bool isPause = true;

        private MediaElement mediaElement;

        private static int indexInLb = -1;

       

        private Slider volumeControl, videoControl;

        private Button pauseAndPlay, skipForward, skipBackward;


        private double videoDuration;

        public static AllVideosPlayer Instance(string listboxItem)
        {
            if (instance == null)
            {
                instance = new AllVideosPlayer(listboxItem);

            }
            return instance;
        }

        private AllVideosPlayer(string listBoxItem)
        {
            videoPath = listBoxItem;



            foreach(var ui in MainWindow.GetElements())
            {
                if (ui is Button)
                {
                    Button button = (Button)ui;

                    switch(button.Content)
                    {
                        case "||":
                        case "▶":

                            pauseAndPlay = button;

                            pauseAndPlay.Click += PauseAndPlay_Click;

                            break;

                        case "►►":
                            skipForward = button;
                            skipForward.Click += SkipAndForward_Click;

                            

                            break;

                        case "◄◄":

                            skipBackward = button;

                            skipBackward.Click += SkipAndForward_Click;
                            break;
                    }
                }
                else if (ui is Slider)
                {
                    Slider slider = (Slider)ui;

                    slider.IsEnabled = true;

                    if (slider.Name == "VolumeControlSlr")
                    {
                        volumeControl = slider;
                    }
                    else
                    {
                        videoControl = slider;
                    }    
                }

                else if (ui is MediaElement)
                {
                    mediaElement = (MediaElement)ui;
                }
            }

            var a = videoPath;
            mediaElement.Source = new Uri(videoPath);


            //videoDuration = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;

            //mediaElement.Play();
            
            
        }

     

        protected override void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void PauseAndPlay_Click(object sender, RoutedEventArgs e)
        {
            if (pauseAndPlay.Content == "||")
            {
                pauseAndPlay.Content = "▶";

                isPause = true;

                mediaElement.Pause();


                return;
            }

            pauseAndPlay.Content = "||";

            isPause = false;

            mediaElement.Play();

            videoDuration = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;

            var s = videoDuration;
        }

        protected override void SkipAndForward_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void VideoSlider_Change(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void VolumeSlider_Change(object sender, RoutedEventArgs e)
        {
            mediaElement.Volume = volumeControl.Value;
        }



        public void PlayCurrentVideo()
        {
               while(true)
                {
                    if (pauseAndPlay.Content == "||")
                    {
                    break;
                    }
                }



                while (mediaElement.Position.TotalSeconds < videoDuration)
                {
                    var a = mediaElement.Position.TotalSeconds;


                    var b = a;


                }
                instance = null;
            
        }
    }
}
