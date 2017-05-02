using System.Diagnostics;
using System.IO;

namespace Keks
{
    class GuetzliThread
    {
        readonly ProcessStartInfo info;
        readonly Keks gui;
        readonly string targetFilePath;

        public GuetzliThread(ProcessStartInfo info, Keks gui, string targetFilePath)
        {
            this.info = info;
            this.gui = gui;
            this.targetFilePath = targetFilePath;
        }

        public void Run()
        {
            gui.logMessage("Thread started.");
            Process p = Process.Start(info);
            p.WaitForExit();
            gui.logMessage("Thread ended.");
            gui.toggleBar(false);
        }
    }
}
