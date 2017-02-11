using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tenka_ichi_Budokai_Elimination
{
    public partial class Options : Form
    {

        public delegate void GetFileDelegate(string file); // Created for passing the choosen file name
        public delegate void GetAutoDelegate(bool auto); // Created for passing the option for automatic elimination (true or false)
        public delegate void GetWaitDelegate(float wait); // Created for passing what wait time will it be, between elimination

        public Options()
        {
            InitializeComponent();
        }
        
        public GetFileDelegate GetFileCallback; // Created for the file name case
        private void btnLoad_Click(object sender, EventArgs e)
        {

            string fileName = null;
            openFileDialog1.Title = "Select a Excel file to load";
            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            if (userClickedOK != DialogResult.OK) return;

            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                fileName = openFileDialog1.FileName;
                GetFileCallback(openFileDialog1.FileName);
            }
        }

        public GetAutoDelegate GetAutoCallback; // Created for the boolean value of "automatic" in Form1.cs
        public GetWaitDelegate GetWaitCallback; // Created for the waitTime value defined in Form1.cs
        private void btnDone_Click(object sender, EventArgs e)
        {
            GetAutoCallback(checkBoxAuto.Checked);
            GetWaitCallback((float)(numericUpDown1.Value));
            this.Close();
        }
    }
}
