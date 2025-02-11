﻿using Microsoft.Win32;
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


            PlaylistsEditorTrVwIt.Header = playlistsFolderPath;
            PlaylistTrVw.SelectedItemChanged += PlaylistTrVw_SelectionChanged;


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


            if (File.Exists((AddNewTb.Text)))
            {


                foreach (var ext in exten)
                {
                    if (AddNewTb.Text.EndsWith(ext))
                    {



                        FileInfo file = new FileInfo(fileName);


                        string newName = "";

                        if (ChoosePlaylistCb.SelectedItem == playlistsFolderPath)
                        {
                            newName = playlistsFolderPath + "\\" + file.Name;
                        }
                        else
                        {
                            newName = playlistsFolderPath + "\\" + ChoosePlaylistCb.SelectedItem+"\\" + file.Name;
                        }

                        if (File.Exists(newName))
                        {
                            MessageBox.Show("A file with a similar name exists in this playlist!");
                        }
                        else
                        {
                            File.Copy(fileName,newName);
                        }


                        CheckPlaylists();

                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("This file does not exists!");
            }

        }





        private  void CheckPlaylists()
        {
            WindowsMethods.CheckPlaylists(PlaylistsEditorTrVwIt);


            ChoosePlaylistCb.Items.Clear();

            ChoosePlaylistCb.Items.Add(playlistsFolderPath);

            ChoosePlaylistCb.SelectedIndex = 0;

            foreach(string path in Directory.GetDirectories(playlistsFolderPath))
            {
                ChoosePlaylistCb.Items.Add(new DirectoryInfo(path).Name);
            }

        }

        private void FindFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "Video files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All files (*.*)|*.*";

            bool? result = openFile.ShowDialog();


            if (result == true)
            {
                AddNewTb.Text = openFile.FileName;
            }
        }

        private void DeletePlaylistBtn_Click(object sender, RoutedEventArgs e)
        {

            CheckPlaylists();
            if (!(DeletePlaylistTb.Text.Length > 0  && ( Directory.Exists(playlistsFolderPath+ "\\" + DeletePlaylistTb.Text) || Directory.Exists(playlistsFolderPath))  ))
            {

                MessageBox.Show("This playlists does not exist!");


                return;
                
            }

            if (DeletePlaylistTb.Text == playlistsFolderPath)
            {


                Directory.Delete(playlistsFolderPath,true);
               
            }
            else
            {
                Directory.Delete(playlistsFolderPath + "\\" + DeletePlaylistTb.Text, true);
            }
            


            CheckPlaylists();

        }


        private void PlaylistTrVw_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string selectedTrVwIt = ExtensionMethods.ExtensionSelectedTrVwIt(PlaylistTrVw);


            if (File.Exists(selectedTrVwIt) == false && Directory.Exists(selectedTrVwIt ))
            {
                DeletePlaylistTb.Text = selectedTrVwIt;


            }
           if(File.Exists(selectedTrVwIt))
            {
                DeleteFileTb.Text = selectedTrVwIt;
            }
           
        }

        private void DeleteFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(DeleteFileTb.Text))
            {
           

                MessageBox.Show("This video file does not exists!");
                return;
            }

            CheckPlaylists();

         

            File.Delete(DeleteFileTb.Text);

            CheckPlaylists();

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (ChoosePlaylistCb.SelectedIndex == 0 && Directory.Exists(ChoosePlaylistCb.SelectedItem.ToString()))
            {
                 Directory.Delete(Convert.ToString(ChoosePlaylistCb.SelectedItem),true);


                Directory.CreateDirectory(ChoosePlaylistCb.SelectedItem.ToString());
            }
            else if(Directory.Exists(playlistsFolderPath + "\\" + ChoosePlaylistCb.SelectedItem))
            {
                Directory.Delete(Convert.ToString(playlistsFolderPath+"\\"+ChoosePlaylistCb.SelectedItem), true);

                Directory.CreateDirectory(Convert.ToString(playlistsFolderPath + "\\" + ChoosePlaylistCb.SelectedItem));
            }
            else
            {
                MessageBox.Show("This playlist does not exists!");
            }
            CheckPlaylists();
        }
    }
}
