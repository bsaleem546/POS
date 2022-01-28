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
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            reset();
        }

        private void getProducts()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.PRODUCT_TBs where x.STATUS == true select new { ID = x.ID, PRODUCT = x.PRODUCT };
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "PRODUCT";
            comboBox1.DataSource = q.ToList();
        }

        private void reset() {
            getProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = comboBox1.SelectedText + "------" + textBox4.Text;
            ListViewItem items = new ListViewItem();
            items.SubItems.Add(comboBox1.SelectedValue.ToString());
            items.SubItems.Add(textBox4.Text);
            listView1.Items.Add(items);
        }
    }
}
