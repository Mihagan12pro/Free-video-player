using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Free_video_player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlay = true;
        private PlayingVideo playingVideo;

        private string[] invalidStringsArray = { "System.Windows.Controls.ListBoxItem: "};


        private DispatcherTimer timer;


        public MainWindow()
        {
            InitializeComponent();

            VideoPlayerMedia.LoadedBehavior = MediaState.Manual;

            foreach (UIElement el in mainGrid.Children)
            {

                PlayingVideo.iElement(el);

            }
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(100);

            timer.Tick += new EventHandler(Timer_tick);

            timer.Start();

            VideoPlayerMedia.MediaOpened += VideoPlayerMedia_MediaOpened;


            ClearVideoListBtn.Click += ClearVideoListBtn_Click;
        }



        private void Timer_tick(object sender, EventArgs e)
        {
            VideoControlSlr.Value = VideoPlayerMedia.Position.TotalSeconds;

        }

        


        private void VideoPlayerMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (VideoPlayerMedia.NaturalDuration.HasTimeSpan)
            {
                VideoControlSlr.Maximum = VideoPlayerMedia.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }


        private void AddToPlayListBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Video files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All files (*.*)|*.*";

            bool? result = openFile.ShowDialog();

            if (result == true)
            {
                PlayListLb.Items.Add(openFile.FileName);
               



            }

        }

        private void PlayListLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlayListLb.SelectedIndex != -1 )
            {
                if (playingVideo != null)
                {
                    //playingVideo = playingVideo.DestructInstance();
                }

                playingVideo = PlayingVideo.Instance(Convert.ToString(PlayListLb.ItemContainerGenerator.ContainerFromIndex(PlayListLb.SelectedIndex)), PlayListLb.SelectedIndex);

                VideoPlayerMedia.Source =new Uri(playingVideo.videoPath.Replace(invalidStringsArray[0],"") );

                VideoPlayerMedia.Play();

                VideoPlayerMedia.Stop();
            }
        }

        private void RemoveFromPlayListBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PlayListLb.SelectedItem;

            if (selectedItem != null)
            {

                playingVideo = playingVideo.DestructInstance();

                var a = playingVideo;

                PlayListLb.Items.Remove(selectedItem);
            }

           
        }

        private void ClearVideoListBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playingVideo != null)
            {
                foreach (var item in PlayListLb.Items)
                {



                    if (Convert.ToString(playingVideo.videoPath.Replace("System.Windows.Controls.ListBoxItem: ", "")) == Convert.ToString(item))
                    {
                        playingVideo = playingVideo.DestructInstance();
                        break;
                    }


                }
            }
            PlayListLb.Items.Clear();
        }

        private void PlayAllVideosBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in PlayListLb.Items)
            {
                
            }
        }
    }
}
