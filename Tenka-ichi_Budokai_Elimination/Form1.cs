using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Tenka_ichi_Budokai_Elimination.Options;

namespace Tenka_ichi_Budokai_Elimination
{
    public partial class Form1 : Form
    {
        int remain = 0;
        int waitTime = 500;
        bool automatic = true;
        bool hidden = false;
        List<string> listA = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {

            if(listA.Count() == 0)
            {
                labelLoadFirst.Show();
                return;
            }

            if (listA.Count() == 1)
            {
                return;
            }


            /* Seed Generator found in:
             * http://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values
             */
            labelCase.Text = "Eliminated";
            labelCase.ForeColor = Color.Red;
            labelName.ForeColor = Color.Red;
            labelCase.Location = new Point(195, 404);
            labelCase.Show();
            labelName.Show();

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int n = rand.Next(remain);

            if (automatic)
            {
                while (remain != 0)
                {

                    rand = new Random(Guid.NewGuid().GetHashCode());
                    n = rand.Next(remain);

                    if (remain - 1 < 1000)
                    {
                        labelNumber.Location = new Point(239, 245);
                    }
                    if (remain-1 < 100)
                    {
                        labelNumber.Location = new Point(252,245);
                    }
                    if (remain - 1 < 10)
                    {
                        labelNumber.Location = new Point(265, 245);
                    }
                    labelNumber.Text = (--remain).ToString();
                    labelName.Text = listA[n];

                    if (remain != 0)
                    {
                        listA.RemoveAt(n);
                        listView1.Items[n].Remove();
                    }
                    else
                    {
                        labelCase.Text = "Winner";
                        labelCase.ForeColor = Color.Green;
                        labelName.ForeColor = Color.Green;
                        labelCase.Location = new Point(220, 404);

                    }

                    Application.DoEvents();
                    Thread.Sleep(waitTime);
                }
            }
            else
            {
                rand = new Random(Guid.NewGuid().GetHashCode());
                n = rand.Next(remain);

                if (remain - 1 < 1000)
                {
                    labelNumber.Location = new Point(239, 245);
                }
                if (remain - 1 < 100)
                {
                    labelNumber.Location = new Point(252, 245);
                }
                if (remain - 1 < 10)
                {
                    labelNumber.Location = new Point(265, 245);
                }

                labelNumber.Text = (--remain).ToString();
                labelName.Text = listA[n];

                if (remain != 0)
                {
                    listA.RemoveAt(n);
                    listView1.Items[n].Remove();
                }
                else
                {
                    labelCase.Text = "Winner";
                    labelCase.ForeColor = Color.Green;
                    labelName.ForeColor = Color.Green;
                    labelCase.Location = new Point(220, 404);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!hidden)
            {
                btnLoad.Hide();
                btnExit.Hide();
                btnGO.Location = new Point(13, 607);
                btnGO.Size = new Size(552, 61);
                hidden = true;
            } else
            {
                btnLoad.Show();
                btnExit.Show();
                btnGO.Location = new Point(204, 607);
                btnGO.Size = new Size(175, 61);
                hidden = false;
            }

        }
        private void GetFileCallbackFn(string fileName)
        {
            listA.Clear();
            listView1.Clear();
            labelLoadFirst.Hide();

            using (var fs = File.OpenRead(fileName))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    listA.Add(reader.ReadLine());
                }
            }

            remain = listA.Count();
            if (remain - 1 < 1000)
            {
                labelNumber.Location = new Point(239, 245);
            }
            if (remain - 1 < 100)
            {
                labelNumber.Location = new Point(252, 245);
            }
            if (remain - 1 < 10)
            {
                labelNumber.Location = new Point(265, 245);
            }

            labelNumber.Text = remain.ToString();


            for (var i = 0; i < listA.Count; i++)
            {
                listView1.Items.Add(listA[i]);
            }
            
            /* Other way of doing the above
             * 
             * foreach (string name in listA) { listView1.Items.Add(name); }
             */

        }
        private void GetAutoCallbackFn(bool option)
        {
            automatic = option;
        }
        private void GetWaitCallbackFn(float wait)
        {
            waitTime = (int)wait*1000;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Options option = new Options();
            option.GetFileCallback = new GetFileDelegate(this.GetFileCallbackFn);
            option.GetAutoCallback = new GetAutoDelegate(this.GetAutoCallbackFn);
            option.GetWaitCallback = new GetWaitDelegate(this.GetWaitCallbackFn);
            option.Show();
        }
    }
}
