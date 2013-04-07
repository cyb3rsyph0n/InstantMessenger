using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Common.Windows
{
    public static class FlashWindow
    {
        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        public static void Flash(Form WindowToFlash, FLASHWFlags Flags)
        {
            FLASHWINFO fw = new FLASHWINFO();

            //SETUP THE FLASH CALL FOR SENDING TO THE API
            fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
            fw.hwnd = WindowToFlash.Handle;
            fw.dwFlags = (int)Flags;
            fw.uCount = UInt32.MaxValue;

            //ADD EVENT HANDLER TO STOP FLASHING ONCE THE WINDOW IS BROUGHT TO FOCUS
            if (Flags != FLASHWFlags.FLASHW_STOP)
                WindowToFlash.Activated += new EventHandler(WindowToFlash_Activated);

            //UPDATE THE FLASHING OF THE WINDOW TO THE PROPERTIES
            FlashWindowEx(ref fw);
        }

        static void WindowToFlash_Activated(object sender, EventArgs e)
        {
            //STOP FLASHING THIS WINDOW WHICH CALLED THIS FUNCTION
            Flash((Form)sender, FLASHWFlags.FLASHW_STOP);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public Int32 dwFlags;
        public UInt32 uCount;
        public Int32 dwTimeout;
    }

    public enum FLASHWFlags
    {
        // stop flashing
        FLASHW_STOP = 0,

        // flash the window title 
        FLASHW_CAPTION = 1,

        // flash the taskbar button
        FLASHW_TRAY = 2,

        // 1 | 2
        FLASHW_ALL = 3,

        // flash continuously 
        FLASHW_TIMER = 4,

        // flash until the window comes to the foreground 
        FLASHW_TIMERNOFG = 12
    }
}
