using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class MP3Player
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int returnLength, int hw);

        public void Open (string file)
        {
            string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
            string Command = string.Format(Format,file);
            mciSendString(Command, null, 0, 0);

        }

        public void Play()
        {
            string Command = "Play MediaFile";
            mciSendString(Command, null, 0, 0);
        }

        public void Stop()
        {
            string Command = "Stop MediaFile";
            mciSendString(Command, null, 0, 0);
        }


    }
}
