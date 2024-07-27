using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
namespace Free_video_player
{
    internal static class WindowsMethods
    {
        private static string[] extensions = { ".mp4",".avi",".mkv"};

        public static string[] Extensions 
        {
            get { return extensions; }
        }


        public static void CheckPlaylists(TreeViewItem mainItem)
        {
            mainItem.Items.Clear();

            if (Directory.Exists("Playlists") == false)
            {
                Directory.CreateDirectory("Playlists");
            }

            DirectoryInfo mainDirectory = new DirectoryInfo("Playlists");

            foreach (FileInfo file in mainDirectory.GetFiles())
            {
                foreach(string extension in extensions)
                {
                    if (file.FullName.EndsWith(extension))
                    {
                        TreeViewItem fileItem = new TreeViewItem();

                        fileItem.Header = file.FullName;

                        mainItem.Items.Add(fileItem);
                    }
                }

            }


            foreach (DirectoryInfo directory in mainDirectory.GetDirectories())
            {
                TreeViewItem directoryItem = new TreeViewItem() { Header = directory.Name };


                mainItem.Items.Add(directoryItem);


                foreach (FileInfo file in directory.GetFiles())
                {
                    foreach(string extension in extensions)
                    {
                        if (file.FullName.EndsWith(extension))
                        {
                            TreeViewItem fileItem = new TreeViewItem();

                            fileItem.Header = file.FullName;

                            directoryItem.Items.Add(fileItem);
                        }
                    }
                }
            }
        }

      

    }
}
