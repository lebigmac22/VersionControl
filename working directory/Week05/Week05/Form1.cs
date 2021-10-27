using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            List<decimal> Nyereségek = new List<decimal>();
            List<ProfitListItem> profitlist = new List<ProfitListItem>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                profitlist.Add(new ProfitListItem() { időszak = i, nyereség = ny });

                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();
            
            var profitlistrendezve = (from x in profitlist
                                      orderby x.nyereség
                                      select x).ToList();
            SafetoFile(profitlistrendezve);
            //MessageBox.Show(nyereségekRendezve.Count().ToString());
            //MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 6].ToString());
        }
        private void SafetoFile(List<ProfitListItem> list)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath; // Alapértelmezetten az exe fájlunk mappája fog megnyílni a dialógus ablakban
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv"; // A kiválasztható fájlformátumokat adjuk meg ezzel a sorral. Jelen esetben csak a csv-t fogadjuk el
            sfd.DefaultExt = "csv"; // A csv lesz az alapértelmezetten kiválasztott kiterjesztés
            sfd.AddExtension = true; // Ha ez igaz, akkor hozzáírja a megadott fájlnévhez a kiválasztott kiterjesztést, de érzékeli, ha a felhasználó azt is beírta és nem fogja duplán hozzáírni

            // Ez a sor megnyitja a dialógus ablakot és csak akkor engedi tovább futni a kódot, ha az ablakot az OK gombbal zárták be
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                sw.Write("Időszak");
                sw.Write(";");
                sw.Write("Nyereség");
                sw.WriteLine();
                foreach (var s in list)
                {

                    sw.Write(s.időszak);
                    sw.Write(";");
                    sw.Write(s.nyereség);
                    
                    sw.WriteLine(); // Ez a sor az alábbi módon is írható: sr.Write("\n");
                }
            }
        }


            private void CreatePortfolio()
        {
            Portfolio.Add(new PortFolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortFolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortFolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }
        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
