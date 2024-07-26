using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Free_video_player
{
    static class ExtensionMethods
    {

        //System.Windows.Controls.TreeViewItem Header:0001-0250.mp4 Items.Count:0
        public static string ExtensionSelectedTrVwIt(this TreeView treeView)
        {
            string info = Convert.ToString(treeView.SelectedItem).Replace("System.Windows.Controls.TreeViewItem Header:", "").Replace(" Items.Count:","");

            char lastSymbol;

            //foreach(char i in  info.Reverse())
            //{
            //    lastSymbol = i;

            //    break;
            //}

            info = info.Remove(info.Length - 1);


            return info;
        }
    }
}
