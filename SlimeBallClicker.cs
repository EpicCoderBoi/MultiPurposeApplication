using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace TestApplication
{

    class SlimeBallClicker
    {

        public SoundPlayer soundPlayer = new SoundPlayer(@"C:\Users\Admin\Downloads\slimeEffect.wav (online-audio-converter.com).wav");

        public void SoundPlay()
        {
            soundPlayer.Play();
        }
    }
}
