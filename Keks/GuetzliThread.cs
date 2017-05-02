using System.Diagnostics;
using System.IO;

namespace Keks
{
    class GuetzliThread
    {
        readonly Process exeProcess;
        readonly Keks gui;
        readonly string targetFilePath;

        public GuetzliThread(Process exeProcess, Keks gui, string targetFilePath)
        {
            this.exeProcess = exeProcess;
            this.gui = gui;
            this.targetFilePath = targetFilePath;
        }

        public void Run()
        {
            gui.logMessage("Thread started: " + exeProcess.StartInfo.ToString());
            exeProcess.WaitForExit();
            gui.logMessage("Thread finished: " + exeProcess.StartInfo.ToString());
            gui.progressBar1.Visible = false;
            if (File.Exists(targetFilePath))
            {
                gui.logMessage("Success!");
            }
            else
            {
                gui.logMessage("Something went wrong. Perhaps the selected file is unsupported.");
            }
        }
    }
}
