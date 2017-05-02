using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Keks
{
    public partial class Keks : Form
    {
        readonly private string ORIGINAL_TRACKBAR_TEXT;
        readonly private string PROGRAM_TITLE = "Keks";
        readonly private string PROGRAM_VERSION = "0.1";
        readonly private string GUETZLI_VERSION = "1.0.1";

        public Keks()
        {
            InitializeComponent();
            ORIGINAL_TRACKBAR_TEXT = groupBox1.Text;
            startButton.Click += new EventHandler(this.OnStartButtonPressed);
            selectButton.Click += new EventHandler(this.OnSelectButtonPressed);
            DoubleBuffered = true;
            
            SetDoubleBuffered(groupBox1);
            SetDoubleBuffered(qualitySelector);

            logMessage(String.Empty);
            logMessage("Welcome to " + PROGRAM_TITLE + " " + PROGRAM_VERSION);
            logMessage(PROGRAM_TITLE + " " + PROGRAM_VERSION + " uses Google's Guetzli version " + GUETZLI_VERSION + ". Visit https://github.com/google/guetzli and read the README.md and LICENSE files before using.");
            logMessage(String.Empty);
        }

        private void OnStartButtonPressed(object sender, EventArgs e)
        {
            if (String.Empty.Equals(openFileDialog1.FileName))
            {
                logMessage("Please select a file before starting.");
            }
            else
            {
                Program.CallGuetzli(this);
            }
        }

        private void OnSelectButtonPressed(object sender, EventArgs e)
        {
            logMessage("Opening file selector.");
            openFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateTrackBarPositionText();
        }

        private void UpdateTrackBarPositionText()
        {
            groupBox1.Text = ORIGINAL_TRACKBAR_TEXT + qualitySelector.Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            logMessage("Selected file: " + openFileDialog1.FileName + ".");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UpdateTrackBarPositionText();
        }

        public void logMessage(string msg)
        {
            log.AppendText("\r\n" + msg);
        }

        public int GetSelectedQuality()
        {
            return qualitySelector.Value;
        }

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo property =
                  typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            property.SetValue(c, true, null);
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
