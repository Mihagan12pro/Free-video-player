using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
namespace Free_video_player
{
    public abstract class  AbstractPlayer
    {
        protected abstract void VolumeSlider_Change(object sender, RoutedEventArgs e);

        protected abstract void VideoSlider_Change(object sender, RoutedEventArgs e);
        protected abstract void MediaElement_MediaEnded(object sender, RoutedEventArgs e);
        protected abstract void SkipAndForward_Click(object sender, RoutedEventArgs e);

        protected abstract void PauseAndPlay_Click(object sender, RoutedEventArgs e);
    }
}
