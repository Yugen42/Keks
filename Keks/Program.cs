using System;
using System.Windows.Forms;

namespace Keks
{
    static class Program
    {

        readonly static string GUETZLI_RESOURCE = "Keks.guetzli_windows_x86-64_v1.0.1.exe";
        readonly static string INSTANCE_ID = Guid.NewGuid().ToString();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Keks());
        }

        public static void CallGuetzli(Keks gui)
        {
            GuetzliCaller gc = new GuetzliCaller(GUETZLI_RESOURCE, gui, INSTANCE_ID);
            gc.Execute();
        }
    }
}
