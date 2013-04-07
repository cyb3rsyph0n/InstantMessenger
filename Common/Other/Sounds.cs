﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Common.Sounds
{
    public class Sounds
    {

        [DllImport("WinMM.dll")]
        public static extern bool PlaySound(string fname, int Mod, int flag);

        // these are the SoundFlags we are using here, check mmsystem.h for more
        public int SND_ASYNC = 0x0001; // play asynchronously
        public int SND_FILENAME = 0x00020000; // use file name
        public int SND_PURGE = 0x0040; // purge non-static events

        public void Play(string fname)
        {
            PlaySound(fname, 0, SND_FILENAME | SND_ASYNC);
        }

        public void StopPlay()
        {
            PlaySound(null, 0, SND_PURGE);
        }
    }
}