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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName; // label2
            btnAdd.Text = Resource1.Add; // button1
            button2.Text = Resource1.Delete;
            button1.Text = Resource1.FileWrite;

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text
            };
            users.Add(u);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (users.Count==0)
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    foreach (var user in users)
                    {
                        sw.WriteLine(user.FullName);
                        sw.WriteLine(";");
                    }
                    sw.WriteLine();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedIndex!=null)
            {
                User u = (User)listUsers.SelectedItem;
                foreach (var user in users)
                {
                    if (u.ID == user.ID)
                    {
                        users.Remove(user);
                        return;
                    }
                }
            }
        }
    }
}
