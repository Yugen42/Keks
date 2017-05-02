using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class GuetzliCaller
{
    readonly string guetzliResource;
    readonly string workingDirectory = Path.GetTempPath();
    readonly string fullExecutablePath;
    readonly Keks.Keks gui;
    bool extracted = false;

    public GuetzliCaller(string guetzliResource, Keks.Keks gui, string instanceId)
    {
        gui.logMessage(string.Empty);
        gui.logMessage("Beginning wrapper construction.");
        this.guetzliResource = guetzliResource;
        fullExecutablePath = workingDirectory + instanceId + guetzliResource;
        this.gui = gui;
        gui.logMessage("Wrapper construction completed successfully");
    }

    public void Execute()
    {
        gui.logMessage("Starting execution.");
        ExtractResource(guetzliResource, Path.GetTempPath());
        LaunchCommandLineApp();
        CleanUp();
    }

    public void ExtractResource(string resource, string path)
    {
        if (!extracted)
        {
            gui.logMessage("Extracting resource " + resource + " to path " + path + ".");
            Directory.CreateDirectory(path);
            Stream stream = GetType().Assembly.GetManifestResourceStream(resource);
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(fullExecutablePath, bytes);
            extracted = true;
        }
    }

    private void LaunchCommandLineApp()
    {
        gui.logMessage("Configuring Guetzli parameters.");
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        startInfo.FileName = fullExecutablePath;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        string filePath = gui.openFileDialog1.FileName;
        string directoryPath = Path.GetDirectoryName(filePath);
        string targetFilePath = directoryPath + "\\guetzli." + gui.openFileDialog1.SafeFileName;
        File.Delete(targetFilePath);

        startInfo.Arguments
            = "--nomemlimit --quality "
            + gui.GetSelectedQuality() + " "
            + filePath + " "
            + targetFilePath;

        try
        {
            gui.logMessage("Attempting to call Guetzli with parameters " + startInfo.Arguments + ".");
            gui.progressBar1.Visible = true;
            using (Process exeProcess = Process.Start(startInfo))
            {

                gui.logMessage("Processing, please wait. This can take a very long time depending on your hardware, the input and the desired quality.");

                Keks.GuetzliThread t = new Keks.GuetzliThread(exeProcess, gui, targetFilePath);
                Thread oThread = new Thread(new ThreadStart(t.Run));
            }
        }
        catch (Exception e)
        {
            gui.logMessage("An uncaught exception occured: " + e.ToString());
        }
    }

    private void CleanUp()
    {
        //gui.logMessage("Cleaning extracted resource " + fullExecutablePath + ".");
        // if (File.Exists(fullExecutablePath))
        // {
        //    File.Delete(fullExecutablePath);
        // }
    }
}
