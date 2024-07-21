using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Free_video_player
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlay = true;
        public MainWindow()
        {
            InitializeComponent();

            VideoPlayerMedia.LoadedBehavior = MediaState.Manual;

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Window_DragEnter);
            this.Drop += new DragEventHandler(Window_Drop);
        }

        private void PauseAndPlayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay)
            {
                isPlay = false;
                PauseAndPlayBtn.Content = "||";
                return;
            }

            isPlay = true;
            PauseAndPlayBtn.Content = "▶";

        }

        private void VideoSkipperSlr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoPlayerMedia.Position = TimeSpan.FromSeconds(VideoSkipperSlr.Value);
        }

        private void VolumeControlSlr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           VideoPlayerMedia.Volume = (double)VolumeControlSlr.Value;
        }

        private void SkipBackwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (VideoPlayerMedia.Position >= TimeSpan.FromSeconds(5))
                 VideoPlayerMedia.Position -= TimeSpan.FromSeconds(5);
        }

        private void SkipForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayerMedia.Position += TimeSpan.FromSeconds(5);
        }


        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    VideoPlayerMedia.Source = new Uri(files[0]);
                    VideoPlayerMedia.Play();
                    isPlay = true;
                    PauseAndPlayBtn.Content = "||";
                }
            }
        }
    }
}
