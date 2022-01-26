using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            reset();
        }

        private void reset() {
            textBox1.Focus();

            textBox1.Text = textBox2.Text = "";
        }

        private void naviagte() {
            this.Hide();
            dashboard d = new dashboard();
            d.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null) {
                MessageBox.Show("Username and password both are required");
                reset();
                return;
            }
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.USERS_TBs where x.USERNAME == textBox1.Text && x.PASSWORD == textBox2.Text select x;

            if (q.Any()) {

                foreach (var data in q) {
                    Models.Users.NAME = data.NAME;
                    Models.Users.USERNAME = data.USERNAME;
                    Models.Users.ROLE = data.ROLE;
                }

                MessageBox.Show("Login successfull");
                naviagte();
            }
            else {
                MessageBox.Show("user not Find");
                reset();
            }
        }
    }
}
