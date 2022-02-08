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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            reset();
        }
        private int itemCount = 0;
        private string code = "";
        private void getMenu()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var menus = from x in db.MENU_TBs where x.STATUS == true select x;
            foreach (var menu in menus)
            {
                var products = from x in db.MENU_PRODUCTs
                               join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                               where x.MENU_ID == menu.ID
                               select new { pid = p.ID, pname = p.PRODUCT };
                if (products.Any())
                {
                    comboBox1.ValueMember = "pid";
                    comboBox1.DisplayMember = "pname";
                    comboBox1.DataSource = products.ToList();
                }
                var items = from x in db.MENU_ITEMs
                               join i in db.ITEMS_TBs on x.ITEM_ID equals i.ID
                               where x.MENU_ID == menu.ID
                               select new { itid = i.ID, itname = i.ITEM_NAME };
                if (items.Any())
                {
                    comboBox2.ValueMember = "itid";
                    comboBox2.DisplayMember = "itname";
                    comboBox2.DataSource = items.ToList();
                }
                var deals = from x in db.MENU_DEALs
                               join d in db.DEALS_TBs on x.DEAL_ID equals d.ID
                               where x.MENU_ID == menu.ID
                               select new { did = d.ID, dname = d.DEAL_NAME };
                if (deals.Any())
                {
                    comboBox3.ValueMember = "did";
                    comboBox3.DisplayMember = "dname";
                    comboBox3.DataSource = deals.ToList();
                }
            }
        }

        private void reset()
        {
            comboBox1.Focus();

            getMenu();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            itemCount = 0;
            textBox1.Text = "1";
            textBox6.Text = textBox7.Text = comboBox4.Text = "";
            textBox2.Text = textBox3.Text = textBox5.Text = "0";
            textBox4.Text = "NaN";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            itemCount = itemCount + 1;
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            var product = db.PRODUCT_TBs.Single(x => x.ID == id);
            decimal price = (decimal)product.SELL_PRICE * Convert.ToInt32(textBox1.Text);
            dataGridView1.Rows.Add(itemCount, id, product.PRODUCT, product.SELL_PRICE, textBox1.Text, price, "Product");
            decimal subtotal = Convert.ToDecimal(textBox2.Text) + price;
            textBox2.Text = subtotal.ToString();
            textBox5.Text = (subtotal - Convert.ToDecimal(textBox3.Text)).ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            itemCount = itemCount + 1;
            int id = Convert.ToInt32(comboBox2.SelectedValue);
            var item = db.ITEMS_TBs.Single(x => x.ID == id);
            decimal price = (decimal)item.PRICE * Convert.ToInt32(textBox1.Text);
            dataGridView1.Rows.Add(itemCount, id, item.ITEM_NAME, item.PRICE, textBox1.Text, price, "Item");
            decimal subtotal = Convert.ToDecimal(textBox2.Text) + price;
            textBox2.Text = subtotal.ToString();
            textBox5.Text = (subtotal - Convert.ToDecimal(textBox3.Text)).ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            itemCount = itemCount + 1;
            int id = Convert.ToInt32(comboBox3.SelectedValue);
            var deal = db.DEALS_TBs.Single(x => x.ID == id);
            decimal price = (decimal)deal.PRICE * Convert.ToInt32(textBox1.Text);
            dataGridView1.Rows.Add(itemCount, id, deal.DEAL_NAME, deal.PRICE, textBox1.Text, price, "Deal");
            decimal subtotal = Convert.ToDecimal(textBox2.Text) + price;
            textBox2.Text = subtotal.ToString();
            textBox5.Text = (subtotal - Convert.ToDecimal(textBox3.Text)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == null || textBox7.Text == "")
            {
                MessageBox.Show("Amount required in numbers");
                return;
            }
            if (comboBox4.Text == null || comboBox4.Text == "")
            {
                MessageBox.Show("Calculation type is required");
                return;
            }
            string type = comboBox4.Text;
            if (type == "AMOUNT")
            {
                textBox3.Text = textBox7.Text;
                textBox4.Text = "In Amount";
            }
            if (type == "PERCENTAGE")
            {
                decimal subtotal = Convert.ToDecimal(textBox2.Text);
                int per = Convert.ToInt32(textBox7.Text);
                decimal cutPer = (subtotal * per) / 100;
                textBox3.Text = cutPer.ToString();
                textBox4.Text = "In Percentage";
            }
            textBox7.Text = comboBox4.Text = "";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = (Convert.ToDecimal(textBox2.Text) - Convert.ToDecimal(textBox3.Text)).ToString();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            decimal price = Convert.ToDecimal(dataGridView1.Rows[rowindex].Cells[5].Value);
            decimal subtotal = Convert.ToDecimal(textBox2.Text) - price;
            textBox2.Text = subtotal.ToString();
            textBox5.Text = (subtotal - Convert.ToDecimal(textBox3.Text)).ToString();
        }
    }
}
