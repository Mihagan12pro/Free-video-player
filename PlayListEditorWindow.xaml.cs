using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Free_video_player
{
    /// <summary>
    /// Логика взаимодействия для PlayListEdotor.xaml
    /// </summary>
    public partial class PlayListEditorWindow : Window
    {

        private const string playlistsFolderPath = "Playlists";

        public PlayListEditorWindow()
        {
            InitializeComponent();

            CheckPlayLists();
        }






        private void CheckPlayLists()
        {
            if (Directory.Exists("Playlists") == false)
            {
                Directory.CreateDirectory("Playlists");
            }




            PlaylistTrVwIt.Items.Clear();

            ChoosePlaylistCb.Items.Clear();
            ChoosePlaylistCb.Items.Add("Playlist");

            CreateFileTrVwIt(new DirectoryInfo(playlistsFolderPath), PlaylistTrVwIt);
            foreach (DirectoryInfo folder in new DirectoryInfo("Playlists").GetDirectories())
            {
                ChoosePlaylistCb.Items.Add(folder.Name);


                TreeViewItem folderItem = new TreeViewItem();

                folderItem.Header = folder.Name;

                PlaylistTrVwIt.Items.Add(folderItem);

                CreateFileTrVwIt(folder, folderItem);

            }




        }


        private void CreateFileTrVwIt(DirectoryInfo folder, TreeViewItem folderItem)
        {
            foreach (FileInfo file in folder.GetFiles())
            {
                bool isVideo = false;

                string[] extensions = { ".avi", ".mp4", ".mkv" };


                foreach (string extension in extensions)
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

        private void WritNewNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateNewBtn_Click(object sender, RoutedEventArgs e)
        {
            string newPlayListName = CreateNewTb.Text;

            if (newPlayListName == "Playlists")
            {
                MessageBox.Show("Invalid name!");
                return;
            }

            if (Directory.Exists(playlistsFolderPath+"\\" + newPlayListName))
            {
                MessageBox.Show("You can't create a playlist with that name because a playlist with that name exists!");
                return;
            }

            


            Directory.CreateDirectory((playlistsFolderPath + "\\" + newPlayListName));

            CheckPlayLists();


        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var exten = MainWindow.extensions;
            

            foreach(var ext in exten)
            {
                if (AddNewTb.Text.EndsWith(ext))
                {


                    break;
                }
            }

        }
    }
}
