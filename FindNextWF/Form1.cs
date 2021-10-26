using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindNextWF
{
    public partial class Form1 : Form
    {
        int[] numbers = new int[24];
        int number=1;
        int _ticks;
        int _ticks2;
        public Form1()
        {
            InitializeComponent();
            foreach (var playBtn in Controls.OfType<Button>())
            {
                playBtn.Text = "";
                playBtn.Font = new Font("", 30);
                playBtn.BackColor = Color.Green;
                playBtn.Click += Btn_Click;
                playBtn.Enabled = false;
            }
            progresBar.Minimum = 1;
            progresBar.Maximum = 25;
            next.Text = number.ToString();
            ticks.Text = hardBar.Value.ToString();
        }
       

        private void Start_Click(object sender, EventArgs e)
        {
            if (hardBar.Value == 1)
            {
                _ticks = 300;
            }
            else if (hardBar.Value == 2)
            {
                _ticks = 120;
            }
            else if (hardBar.Value == 3)
            {
                _ticks = 60;
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }
            Shufle();
            EnablePlayBttns();
            Start.Enabled = false;
            hardBar.Enabled = false;
            timer1.Start();

        }
        private void EnablePlayBttns()
        {
            foreach (var btn in Controls.OfType<Button>())
            {
                btn.Enabled = true;
            }
        }
        private void Shufle()
        {
            Random random = new Random();
            numbers = numbers.OrderBy(x => random.Next()).ToArray();
            int count = 0;
            foreach (var playBtn in Controls.OfType<Button>())
            {
                playBtn.Text = numbers[count].ToString();
                count++;
            }
        }
        private bool IsDisable()
        {
            foreach (var btn in Controls.OfType<Button>())
            {
                if (btn.Enabled == true)
                    return false;
            }
            return true;
        }
        private void End()
        {
            timer1.Stop();
            foreach (var btn in Controls.OfType<Button>())
            {
                btn.Text = "X";
            }
            number = 1;
            next.Text = number.ToString();
            hardBar.Enabled = true;
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            if (_ticks != 0)
            {
                if ((sender as Button).Text == number.ToString())
                {
                    next.Text = (number + 1).ToString();
                    (sender as Button).Text ="X";
                    (sender as Button).BackColor = Color.Red;
                    (sender as Button).Enabled = false;
                    number++;
                    progresBar.Value++;
                }
                if (IsDisable() == true)
                {
                    End();
                    MessageBox.Show($"You won\n For {_ticks2} seconds!", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progresBar.Value = 1; ;
                    Start.Enabled = true;
                    foreach (var item in Controls.OfType<Button>())
                    {
                        item.BackColor = Color.Green;
                    }

                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks.Text = $"Time: {_ticks--} seconds left";
            _ticks2++;
            if (_ticks == 0)
            {
                End();
                MessageBox.Show("You lose", "Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                foreach (var btn in Controls.OfType<Button>())
                {
                    btn.BackColor = Color.Green;
                }
                progresBar.Value = 1;
                Start.Enabled = true;
            }
        }


    }
}
