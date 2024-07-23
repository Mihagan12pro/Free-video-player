﻿using System;
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

        private static Button pauseAndPlay;

        private double videoDuration; 



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
                instance = DestructInstance();

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

                            pauseAndPlay = button;

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

                    mediaElement.Volume = 0;



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

        private void PauseAndPlay_Click(object sender, RoutedEventArgs e)
        {

            if (mediaElement.Source != null)
            {


                Button button = sender as Button;

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
        private void SkipAndForward_Click(object sender, RoutedEventArgs e)
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
        

        private void VolumeSlider_Change(object sender, RoutedEventArgs e)
        {
            mediaElement.Volume = volumeControl.Value;
        }


        private void VideoSlider_Change(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(videoControl.Value);
        }


        public static PlayingVideo DestructInstance()
        {
            pauseAndPlay.Content = "▶";  

            return null;
        }
    }
}
