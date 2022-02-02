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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            roleCheck();
            reset();
        }

        private void roleCheck() {
            if (Models.Users.ROLE == "ADMIN")
            {
                user_btn.Visible = true;
                inventory_btn.Visible = true;
            }
            else if (Models.Users.ROLE == "MANAGER")
            {
                user_btn.Visible = false;
                inventory_btn.Visible = true;
            }
            else
            {
                user_btn.Visible = false;
                inventory_btn.Visible = false;
            }
        }

        private Form currentChildFrom;

        private void OpenChildForm(Form childForm)
        {
            if (currentChildFrom != null) {
                currentChildFrom.Close();
            }
            currentChildFrom = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelHolder.Controls.Add(childForm);
            panelHolder.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void reset()
        {
            label3.Text = "You are logged in as " + Models.Users.NAME;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AllUsers());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            currentChildFrom.Close();
            reset();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            login l = new login();
            l.Show();
        }

        private void inventory_btn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Inventory());
        }

        private void items_btn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Items());
        }

        private void menus_btn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Menu());
        }

        private void deal_btn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Deals());
        }

        private void discount_btn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Discount());
        }
    }
}
