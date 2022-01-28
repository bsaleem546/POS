using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

            textBox1.Focus();

            textBox4.Text = "1";

            textBox1.Text = textBox2.Text = textBox3.Text = "";

            textBox1.Enabled = textBox2.Enabled = true;

            getProducts();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.ITEMS_TBs select new { ID = x.ID, Item = x.ITEM_NAME, Price = x.PRICE };
            GRID_ITEMS.DataSource = data.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "") {
                MessageBox.Show("Item name is required");
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "") {
                MessageBox.Show("Item price is required in numbers");
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "") {
                MessageBox.Show("Quantity is required in numbers");
                return;
            }
            textBox1.Enabled = textBox2.Enabled = false;

            dataGridView1.Rows.Add(comboBox1.SelectedValue, comboBox1.Text, textBox4.Text);

            comboBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Item name is required");
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Item price is required in numbers");
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Quantity is required in numbers");
                return;
            }
            if (dataGridView1.Rows.Count == 0) {
                MessageBox.Show("No products where added for this item");
                return;
            }
            try {
                using (TransactionScope ts = new TransactionScope()) {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                        Data.ITEMS_TB item = new Data.ITEMS_TB();
                        item.ITEM_NAME = textBox1.Text;
                        item.PRICE = Convert.ToDecimal(textBox2.Text);

                        db.ITEMS_TBs.InsertOnSubmit(item);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                            Data.ITEMS_DETAIL details = new Data.ITEMS_DETAIL();
                            details.PRODUCT_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            details.ITEM_ID = item.ID;
                            details.QUANTITY = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());

                            db.ITEMS_DETAILs.InsertOnSubmit(details);
                            db.SubmitChanges();
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Item added");
                reset();
            }
            catch (TransactionAbortedException err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void GRID_ITEMS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = GRID_ITEMS.CurrentCell.RowIndex;
            int id = Convert.ToInt32(GRID_ITEMS.Rows[rowindex].Cells[0].Value);
            textBox1.Text = GRID_ITEMS.Rows[rowindex].Cells[1].Value.ToString();
            textBox2.Text = GRID_ITEMS.Rows[rowindex].Cells[2].Value.ToString();

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.ITEMS_DETAILs
                       join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                       where x.ITEM_ID == id
                       select new { pid = p.ID, pname = p.PRODUCT, qty = x.QUANTITY };
            foreach (var d in data) {
                dataGridView1.Rows.Add(d.pid, d.pname, d.qty);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Item name is required");
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Item price is required in numbers");
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Quantity is required in numbers");
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No products where added for this item");
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                    {
                        int rowindex = GRID_ITEMS.CurrentCell.RowIndex;
                        int id = Convert.ToInt32(GRID_ITEMS.Rows[rowindex].Cells[0].Value);
                        Data.ITEMS_TB item = db.ITEMS_TBs.Single(x => x.ID == id);
                        item.ITEM_NAME = textBox1.Text;
                        item.PRICE = Convert.ToDecimal(textBox2.Text);
                        db.SubmitChanges();

                        var del = db.ITEMS_DETAILs.Where(x => x.ITEM_ID == id);
                        db.ITEMS_DETAILs.DeleteAllOnSubmit(del);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            Data.ITEMS_DETAIL details = new Data.ITEMS_DETAIL();
                            details.PRODUCT_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            details.ITEM_ID = item.ID;
                            details.QUANTITY = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            db.ITEMS_DETAILs.InsertOnSubmit(details);
                            db.SubmitChanges();
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Item updated");
                reset();
            }
            catch (TransactionAbortedException err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete this item", "Title", MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Information);
            if(dr == DialogResult.Yes){
                try {
                    using (TransactionScope ts = new TransactionScope()) {
                        using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                            int rowindex = GRID_ITEMS.CurrentCell.RowIndex;
                            int id = Convert.ToInt32(GRID_ITEMS.Rows[rowindex].Cells[0].Value);

                            Data.ITEMS_TB item = db.ITEMS_TBs.Single(x => x.ID == id);
                            var del = db.ITEMS_DETAILs.Where(x => x.ITEM_ID == id);
                            db.ITEMS_DETAILs.DeleteAllOnSubmit(del);
                            db.ITEMS_TBs.DeleteOnSubmit(item);
                            db.SubmitChanges();
                            ts.Complete();
                        }
                    }
                    MessageBox.Show("Item deleted");
                    reset();
                }
                catch (TransactionAbortedException err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
    }
}
