using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Focus_Idle
{
    class Program
    {
        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        protected static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                Console.WriteLine(sb.ToString() + " - " + hWnd.ToString());
            }
            return true;
        }

        public static void GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                Console.WriteLine(Buff.ToString() + " - " + handle.ToString());
            }
        }


        static void Main(string[] args)
        {
            //while(true)
            //{
            //    Console.WriteLine(GetActiveWindowTitle());
            //    Console.WriteLine(GetForegroundWindow());
            //    System.Threading.Thread.Sleep(10000);
            //}

            //IntPtr selectedWindow = GetForegroundWindow();
            //GetWindows();

            EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);
#if DEBUG
            Console.ReadKey();
#endif
            GetActiveWindowTitle();

        }

    }
}
