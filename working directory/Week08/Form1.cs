using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week08.Abstractions;
using Week08.Entites;

namespace Week08
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy b = Factory.CreateNew();
            _toys.Add(b);
            mainPanel.Controls.Add(b);
            b.Left = -60;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
           int rightest = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                rightest = (item.Left > rightest) ? item.Left : rightest;
            }

            if (rightest>1000)
            {
                Toy removetoy = _toys[0];
                _toys.Remove(removetoy);
                mainPanel.Controls.Remove(removetoy);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
            label1.Text = "Coming next: Ball";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
            label1.Text = "Coming next: Car";
        }
    }
}
