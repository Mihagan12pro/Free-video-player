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

            //this.AllowDrop = true;
            //this.DragEnter += new DragEventHandler(Window_DragEnter);
            //this.Drop += new DragEventHandler(Window_Drop);

            //timer = new DispatcherTimer();

            //timer.Interval = TimeSpan.FromMilliseconds(500);

            //timer.Tick += new EventHandler(Timer_tick);

            //timer.Start(); // Запуск таймера

            //VideoPlayerMedia.MediaOpened += VideoPlayerMedia_MediaOpened;





            //RemoveFromPlayListBtn.Click += RemoveFromPlayListBtn_Click;
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
                playingVideo = PlayingVideo.Instance(Convert.ToString(PlayListLb.ItemContainerGenerator.ContainerFromIndex(PlayListLb.SelectedIndex)), PlayListLb.SelectedIndex);

                VideoPlayerMedia.Source =new Uri(playingVideo.videoPath.Replace(invalidStringsArray[0],"") );

                VideoPlayerMedia.Play();

                VideoPlayerMedia.Stop();
            }
        }

        //private void Timer_tick(object sender, EventArgs e)
        //{
        //    VideoSkipperSlr.Value = VideoPlayerMedia.Position.TotalSeconds;
        //}
        //private void VideoPlayerMedia_MediaOpened(object sender, RoutedEventArgs e)
        //{
        //    if (VideoPlayerMedia.NaturalDuration.HasTimeSpan)
        //    {
        //        VideoSkipperSlr.Maximum = VideoPlayerMedia.NaturalDuration.TimeSpan.TotalSeconds;
        //    }
        //}
        //private void PauseAndPlayBtn_Click(object sender, RoutedEventArgs e)
        //{

        //    if (VideoPlayerMedia.Source != null)
        //    {


        //        if (isPlay)
        //        {
        //            isPlay = false;
        //            PauseAndPlayBtn.Content = "▶";

        //            VideoPlayerMedia.Pause();
        //            return;
        //        }





        //        VideoPlayerMedia.Play();
        //        isPlay = true;
        //        PauseAndPlayBtn.Content = "||";
        //    }
        //}






        //private void VideoSkipperSlr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    VideoPlayerMedia.Position = TimeSpan.FromSeconds(VideoSkipperSlr.Value);
        //}





        //private void VolumeControlSlr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //   VideoPlayerMedia.Volume = (double)VolumeControlSlr.Value;
        //}





        //private void SkipBackwardBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (VideoPlayerMedia.Position >= TimeSpan.FromSeconds(5))
        //         VideoPlayerMedia.Position -= TimeSpan.FromSeconds(5);
        //}








        //private void SkipForwardBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    VideoPlayerMedia.Position += TimeSpan.FromSeconds(5);



        //}






        //private void AddToPlayList_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Video files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        VideoPlayerMedia.Source = new Uri(openFileDialog.FileName);



        //        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);


        //        filesList.Add(fileInfo);

        //        if (IsFullPathCb.IsChecked == true)
        //        {

        //            PlayListLb.Items.Add(fileInfo.FullName);
        //            return;
        //        }

        //        PlayListLb.Items.Add(fileInfo.Name);
        //        //VideoPlayerMedia.Play();
        //        //isPlay = true;
        //        //PauseAndPlayBtn.Content = "||";
        //    }
        //}







        //private void Window_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effects = DragDropEffects.Copy;
        //    }
        //    else
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}






        //private void Window_Drop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        //        if (files.Length > 0)
        //        {
        //            VideoPlayerMedia.Source = new Uri(files[0]);
        //            VideoPlayerMedia.Play();



        //            VideoPlayerMedia.Pause();


        //            FileInfo fileInfo = new FileInfo(files[0]);

        //            filesList.Add(fileInfo);

        //            if (IsFullPathCb.IsChecked != true)
        //            {


        //                PlayListLb.Items.Add(fileInfo.Name);

        //                return;
        //            }
        //            PlayListLb.Items.Add(fileInfo.FullName);



        //        }
        //    }
        //}






        //private void PlayListLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int indexInLb = PlayListLb.SelectedIndex;



        //    ListBoxItem listBoxItem = (ListBoxItem)PlayListLb.ItemContainerGenerator.ContainerFromIndex(indexInLb);

        //    foreach(var file in filesList)
        //    {



        //        if (file.FullName == Convert.ToString(listBoxItem).Replace("System.Windows.Controls.ListBoxItem: ","") || file.Name == Convert.ToString(listBoxItem).Replace("System.Windows.Controls.ListBoxItem: ", ""))
        //        {
        //            VideoPlayerMedia.Source = new Uri(file.FullName);

        //            VideoPlayerMedia.Play();




        //            VideoPlayerMedia.Pause();


        //            break;
        //        }

        //    }

        //}






        //private void VideoPlayer_PositionChanged(object sender, RoutedPropertyChangedEventArgs<TimeSpan> e)
        //{

        //        VideoSkipperSlr.Value = e.NewValue.TotalSeconds;

        //}






        //private void RemoveFromPlayListBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    int indexInLb = PlayListLb.SelectedIndex;

        //    if (indexInLb >= 0)
        //    {

        //        ListBoxItem listBoxItem = (ListBoxItem)PlayListLb.ItemContainerGenerator.ContainerFromIndex(indexInLb);

        //        string itemString = Convert.ToString(listBoxItem).Replace("System.Windows.Controls.ListBoxItem: ", "");

        //        foreach (var file in filesList)
        //        {
        //            if (file.Name == itemString)
        //            {
        //                filesList.Remove(file);

        //                if (VideoPlayerMedia.Source == new Uri(file.FullName))
        //                {
        //                    ClearVideoPlayer();

        //                    isPlay = false;

        //                    PauseAndPlayBtn.Content = "▶";
        //                }

        //                break;
        //            }
        //        }



        //        PlayListLb.Items.RemoveAt(indexInLb);
        //    }
        //}





        //private void ClearVideoPlayer()
        //{
        //    VideoPlayerMedia.Stop();
        //    VideoPlayerMedia.Source = null; 
        //    VideoPlayerMedia.Close(); 
        //    VideoSkipperSlr.Value = 0; 
        //}


    }
}
