using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Free_video_player
{
    internal class AllVideosPlayer : AbstractPlayer
    {
        protected override void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void PauseAndPlay_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SkipAndForward_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void VideoSlider_Change(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void VolumeSlider_Change(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
