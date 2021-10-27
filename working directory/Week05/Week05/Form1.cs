using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week05.Entities;

namespace Week05
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortFolioItem> Portfolio = new List<PortFolioItem>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            CreatePortfolio();
        }
        private void CreatePortfolio()
        {
            Portfolio.Add(new PortFolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortFolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortFolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }
    }
}
