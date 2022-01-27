using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Circle circle;
        List<string> playList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            circle = new Circle();
            Player.sound.MediaOpened += Sound_MediaOpened;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            circle.Paint(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = (int)Player.sound.Position.TotalSeconds;
            this.Invalidate();
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> files = new List<string>();
            files.AddRange((string[])e.Data.GetData(DataFormats.FileDrop, false));
            for(int i = 0; i<files.Count; i++)
            {
                string format = files[i].Substring(files[0].LastIndexOf('.') + 1).ToLower();
                if(format !="wav" && format !="riff")
                {
                    MessageBox.Show("Bad file format");
                    files.RemoveAt(i);
                }
            }
            playList.AddRange(files);
            listView1.Items.Clear();
            int x = 0;
            foreach(string s in playList)
            {
                var i = listView1.Items.Add(s.Substring(files[0].LastIndexOf((char)92) + 1, files[0].Length - 5 - files[0].LastIndexOf((char)92)));
                i.Tag = x++;
                listView1.TileSize = new Size(100, 50);
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Player.sound.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player.sound.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Player.sound.Pause();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            label2.Text = "Now Playing:" + listView1.Items[(int)listView1.SelectedItems[0].Tag].Text;
            Player.sound.Open(new Uri(playList[(int)listView1.SelectedItems[0].Tag]));
        }

        private void Sound_MediaOpened(object sender, EventArgs e)
        {
            var d = Player.sound.NaturalDuration;
            progressBar1.Maximum = (int)(d.TimeSpan.TotalSeconds);
            Player.sound.Play();
        }

        private void progressBar1_MouseClick(object sender, MouseEventArgs e)
        {
            //Player.sound.Balance
            //Player.sound.SpeedRatio
            //Player.sound.Volume
            Player.sound.Position = new TimeSpan(0, 0, (int)(e.X / 200.0 * progressBar1.Maximum));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
