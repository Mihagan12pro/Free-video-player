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
using System.Windows.Interop;
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
    /// 
    
    public partial class MainWindow : Window
    {
        const bool development = true;

        private const string playlistsFolderPath = "Playlists";

        private static List<UIElement> Elements =  new List<UIElement>();


        public static string[] extensions = { ".avi", ".mp4", ".mkv" };


        private bool isPlay = true;
        private VideoPlayer playingVideo;

        private string[] invalidStringsArray = { "System.Windows.Controls.ListBoxItem: "};


        private DispatcherTimer timer;

        public static List<UIElement>GetElements()
        {
            return Elements;
        }


        public MainWindow()
        {
            InitializeComponent();

            CheckPlayLists();

            VideoPlayerMedia.LoadedBehavior = MediaState.Manual;

            foreach (UIElement el in mainGrid.Children)
            {
                Elements.Add(el);
                VideoPlayer.iElement(el);

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

                playingVideo = VideoPlayer.Instance(Convert.ToString(PlayListLb.ItemContainerGenerator.ContainerFromIndex(PlayListLb.SelectedIndex)), PlayListLb.SelectedIndex);

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
            if(development)
            {
                MessageBox.Show("Coming soon!");


                return;
            }
            //PlayAllVideosBtn.Click -= PlayAllVideosBtn_Click;
            //PlayAllVideosBtn.Click += AbortPlayAllVideosBtn_Click;

            //PlayAllVideosBtn.Content = "Abort playing all";

            //if (playingVideo != null)
            //{
            //    playingVideo = playingVideo.DestructInstance();
            //}

            //PlayListLb.SelectionChanged -= PlayListLb_SelectionChanged;


            //PlayListLb.SelectedIndex = -1;


            //foreach(string item in PlayListLb.Items)
            //{
            //    AllVideosPlayer allVideosPlayer = AllVideosPlayer.Instance(item);


               
            //        allVideosPlayer.PlayCurrentVideo();
                
            //}

        }
        private void AbortPlayAllVideosBtn_Click(object sender, RoutedEventArgs e)
        {
           
            PlayAllVideosBtn.Click -= AbortPlayAllVideosBtn_Click;
            PlayAllVideosBtn.Click += PlayAllVideosBtn_Click;

            PlayAllVideosBtn.Content = "Play all";

            PlayListLb.SelectionChanged += PlayListLb_SelectionChanged;
        }



        private void CheckPlayLists()
        {
            if (Directory.Exists("Playlists") == false)
            {
                Directory.CreateDirectory("Playlists");
            }

            //foreach(FileInfo file in new DirectoryInfo("Playlists").GetFiles())
            //{
            //    TreeViewItem fileItem = new TreeViewItem();

            //    fileItem.Header = file.Name;

            //    PlaylistTrVwIt.Items.Add(fileItem);
            //}


            PlaylistTrVwIt.Items.Clear();


            CreateFileTrVwIt(new DirectoryInfo(playlistsFolderPath),PlaylistTrVwIt);
            foreach(DirectoryInfo folder in new DirectoryInfo("Playlists").GetDirectories())
            {
                TreeViewItem folderItem = new TreeViewItem();

                folderItem.Header = folder.Name;

                PlaylistTrVwIt.Items.Add(folderItem);

                CreateFileTrVwIt(folder,folderItem);

            }
        }


        private void CreateFileTrVwIt(DirectoryInfo folder,TreeViewItem folderItem)
        {
            foreach(FileInfo file in folder.GetFiles())
            {
                bool isVideo = false;

               


                foreach(string extension in extensions)
                {
                    if (file.FullName.EndsWith(extension))
                    {
                        isVideo = true;
                    }
                }

                if (isVideo)
                {
                    TreeViewItem fileItem = new TreeViewItem();

                    fileItem.Header = file.FullName;    

                    folderItem.Items.Add(fileItem);
                }
                
            }
        }

        private void AddToVideosListBtn_Click(object sender, RoutedEventArgs e)
        {
            string  selectedItem = PlaylistTrVw.ExtensionSelectedTrVwIt();

            if (selectedItem != "Playlist")
            {
                FileInfo videoFile = new FileInfo(selectedItem);

                DirectoryInfo playlistFolder;



               
                    
                
                if (videoFile.Exists)
                {
                    PlayListLb.Items.Add(selectedItem);
                    return;
                }

                else 
                {
                    playlistFolder = new DirectoryInfo( playlistsFolderPath + "\\" + selectedItem);

                    if (playlistFolder.Exists)
                    {
                        foreach (FileInfo file in playlistFolder.GetFiles())
                        {
                            PlayListLb.Items.Add(file.FullName);
                        }
                        return;
                    }

                    
                }

                
                    MessageBox.Show("Unfortunately, we can't find this playlist or video file!", "Not exist error!");
                
            
            }

        }

        private void EditPlayLists_Click(object sender, RoutedEventArgs e)
        {
            PlayListEditorWindow playListEditor = new PlayListEditorWindow();


            playListEditor.ShowDialog();


            VideoPlayerMedia.Pause();
        }
    }
}
