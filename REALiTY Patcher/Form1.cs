using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// To make buttons work as they should, add a Panel the size of the button, and make the background color Transparent
/// Then just add an event handler to "Panel.Click"
/// </summary>

namespace REALiTY_Patcher
{
    public partial class Form1 : Form
    {

        #region "Movable form"
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        public string filename;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Movable form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// Open file (btnPlus)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = Text;
                ofd.Filter = "Executable *.exe|*.exe";
                ofd.ShowDialog();
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    this.filename = ofd.FileName;
                    textBox1.Text = this.filename.Substring(this.filename.LastIndexOf("\\"));
                }
            }
        }

        /// <summary>
        /// Patch (btnPatch)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Click(object sender, EventArgs e)
        {
            Patch(this.filename);
        }

        /// <summary>
        /// Exit (btnClose)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Click(object sender, EventArgs e)
        {
            GC.Collect();
            Environment.Exit(0);
        }

        private void Patch(string file)
        {
            MessageBox.Show(this.filename + " patched!");
        }
    }
}
