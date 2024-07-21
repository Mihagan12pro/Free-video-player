using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free_video_player
{
    internal class Video
    {
        public static Video instance;
        private FileInfo videoFile;
        public static Video Instance(FileInfo videoFile)
        {
            return instance;
        }

        private Video(FileInfo videoFile)
        {
            this.videoFile = videoFile; 
        }
    

    }
}
