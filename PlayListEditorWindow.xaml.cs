using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

            CheckPlaylists();


           
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

            CheckPlaylists();


        }

        private void AddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var exten = MainWindow.extensions;

            string fileName = AddNewTb.Text;

            foreach(var ext in exten)
            {
                if (AddNewTb.Text.EndsWith(ext))
                {
                 
                    FileInfo file = new FileInfo(fileName);

                    string name = file.Name;

                    if (File.Exists(ChoosePlaylistCb.SelectedItem + "\\" + name))
                    {
                        MessageBox.Show("A file with a similar name exists in this playlist!");
                    }
                    else
                    {
                        if (ChoosePlaylistCb.SelectedIndex == 0)
                        {
                            File.Copy(fileName, ChoosePlaylistCb.SelectedItem + "\\" + name);
                        }
                        else
                        {
                            File.Copy(fileName,"Playlists"+"\\" + ChoosePlaylistCb.SelectedItem + "\\" + name);
                        }
                    }
                    CheckPlaylists();

                    break;
                }
            }

        }





        private  void CheckPlaylists()
        {
            WindowsMethods.CheckPlaylists(PlaylistsEditorTrVwIt);


            ChoosePlaylistCb.Items.Clear();

            ChoosePlaylistCb.Items.Add("Playlists");

            ChoosePlaylistCb.SelectedIndex = 0;

            foreach(string path in Directory.GetDirectories("Playlists"))
            {
                ChoosePlaylistCb.Items.Add(new DirectoryInfo(path).Name);
            }

        }
    }
}
