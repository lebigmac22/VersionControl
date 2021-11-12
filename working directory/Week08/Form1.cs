using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week08.Entites;

namespace Week08
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;

        public BallFactory Factory
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
            Ball b = Factory.CreateNew();
            _balls.Add(b);
            mainPanel.Controls.Add(b);
            b.Left = -60;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
           int rightest = 0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                rightest = (item.Left > rightest) ? item.Left : rightest;
            }

            if (rightest>1000)
            {
                Ball removeball = _balls[0];
                _balls.Remove(removeball);
                mainPanel.Controls.Remove(removeball);
            }
        }
    }
}
