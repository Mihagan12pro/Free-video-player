using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Free_video_player
{
    sealed class VideoPlayer:AbstractPlayer
    {
        private static VideoPlayer instance;

        public string videoPath { get; }

        private  bool isPause = true;

        private  MediaElement mediaElement;

        private static int indexInLb = -1;

        private static List<UIElement>UIElementsList = new List<UIElement> ();

        private   Slider volumeControl, videoControl;

        private  Button pauseAndPlay,skipForward,skipBackward;

       

        private double videoDuration; 



        public  static  void iElement(UIElement uIElement)
        {
            UIElementsList.Add(uIElement);
        }




        public static VideoPlayer Instance(string videoPath,int index)
        {
            if (instance == null)
            {
                instance = new VideoPlayer(videoPath);
                return instance;
            }

            
               
            instance = instance.DestructInstance();

            indexInLb = index;

            instance = new VideoPlayer(videoPath);

            return instance;
                
               
            



         
        }




        private VideoPlayer(string videoPath)
        {
            this.videoPath = videoPath;
           




            foreach(var ui in UIElementsList)
            {
                if (ui is Button)
                {
                    Button button = (Button) ui;

                    switch(button.Content)
                    {
                        case "▶":

                            pauseAndPlay = button;

                            button.Click += this.PauseAndPlay_Click;

                            break;
                        case "►►":
                        case "◄◄":
                            /*
                             * Content="◄◄" Height="30" Width="30" Margin="320,352,432,20" Background="Gray"></Button>
            <Button Name="PauseAndPlayBtn" Content="▶" FontWeight="Heavy" Background="Gray" Height="30" Width="30" Margin="380,353,372,19"  ></Button>
            <Button Name="SkipForwardBtn"   Content="►►" W
                             */

                            button.Click += this.SkipAndForward_Click;

                            if (button.Name == "SkipForwardBtn")
                            {

                           
                                skipForward = button;
                            }
                            else
                            {

                                skipBackward = button;
                            }
                            break;
                    }
                }
                
                if (ui is MediaElement)
                {
                    mediaElement =(MediaElement)ui;

                    mediaElement.Volume = 0;


                    mediaElement.MediaEnded += this.MediaElement_MediaEnded;
                }
               if (ui is Slider)
               {
                    var slider = (Slider)ui;

                    slider.IsEnabled = true;

                    if (slider.Name == "VolumeControlSlr")
                    {
                        volumeControl = slider;

                        volumeControl.ValueChanged += this.VolumeSlider_Change;


                    }
                    else
                    {
                        videoControl = slider;

                        videoControl.ValueChanged += this.VideoSlider_Change;
                    }


               }

            }
   



        }

        protected override  void PauseAndPlay_Click(object sender, RoutedEventArgs e)
        {

            if (mediaElement.Source != null)
            {
                


                Button button = sender as Button;

                if(mediaElement.Position.TotalSeconds == videoDuration)
                {
                    mediaElement.Position= TimeSpan.FromSeconds(0);
                }


                if (isPause)
                {
                    videoDuration = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;

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
        protected override  void SkipAndForward_Click(object sender, RoutedEventArgs e)
        {
            

            Button button = sender as Button;

            switch(button.Content)
            {
                case "►►":

                    var videoDurationTimeSpan =(mediaElement.NaturalDuration.TimeSpan);

                    double videoDuration = videoDurationTimeSpan.TotalSeconds;

                    if(mediaElement.Position.TotalSeconds + 5 <= videoDuration)
                    {
                        mediaElement.Position = TimeSpan.FromSeconds(mediaElement.Position.TotalSeconds + 5);
                    }

                    break;
                default:

                    if(mediaElement.Position.TotalSeconds - 5 >= 0)
                    {
                        mediaElement.Position = TimeSpan.FromSeconds(mediaElement.Position.TotalSeconds - 5);
                    }

                    break;
            }
             
            
        }
        

        protected override   void VolumeSlider_Change(object sender, RoutedEventArgs e)
        {
            mediaElement.Volume = volumeControl.Value;
        }


        protected override void VideoSlider_Change(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(videoControl.Value);
        }
        protected override void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            isPause = true;
            pauseAndPlay.Content = "▶";
            volumeControl.Value = 0;
        }


        public  VideoPlayer DestructInstance()
        {
            isPause = true;
            pauseAndPlay.Content = "▶";

            mediaElement.Stop();
            mediaElement.Source = null;
            mediaElement.Close();

            instance.pauseAndPlay.Click -= instance.PauseAndPlay_Click;
            instance.skipBackward.Click -= instance.SkipAndForward_Click;
            instance.skipForward.Click -= instance.SkipAndForward_Click;

            instance.volumeControl.IsEnabled = false;
            instance.volumeControl.Value = 0;
            instance.volumeControl.ValueChanged -= instance.VolumeSlider_Change;

            instance.videoControl.IsEnabled = false;
            instance.videoControl.Value = 0;
            instance.videoControl.ValueChanged -= instance.VideoSlider_Change;

           


            return null;
        }

        
    }
}
