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
    public partial class Deals : Form
    {
        public Deals()
        {
            InitializeComponent();
            reset();
        }

        private void getItems() {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.ITEMS_TBs select new { ID = x.ID, ITEM = x.ITEM_NAME };
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "ITEM";
            comboBox1.DataSource = q.ToList();
        }

        private void reset() {

            textBox1.Focus();

            textBox1.Text = textBox3.Text = textBox2.Text = "";

            textBox4.Text = "1";

            textBox1.Enabled = textBox2.Enabled = true;

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.DEALS_TBs select new { ID = x.ID, Deals = x.DEAL_NAME, Price = x.PRICE };
            GRID_DEALS.DataSource = q.ToList();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            getItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string textInput = "";
            if (e.KeyChar == (char)13)
            {
                if (sender is TextBox)
                {
                    TextBox txb = (TextBox)sender;
                    textInput = txb.Text;
                }
                Data.ConnectionDataContext db = new Data.ConnectionDataContext();
                var q = from x in db.DEALS_TBs
                        where x.DEAL_NAME.Contains(textBox3.Text)
                        select new { ID = x.ID, Deals = x.DEAL_NAME, Price = x.PRICE };
                GRID_DEALS.DataSource = q.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Deal name is required");
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Deal price is required in numbers");
                textBox2.Focus();
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Quantity is required in numbers");
                textBox2.Focus();
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
                MessageBox.Show("Deal name is required");
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Deal price is required in numbers");
                textBox2.Focus();
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Quantity is required in numbers");
                textBox2.Focus();
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items where added for this deal");
                return;
            }
            try {
                using (TransactionScope ts = new TransactionScope()) {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                        Data.DEALS_TB deal = new Data.DEALS_TB();
                        deal.DEAL_NAME = textBox1.Text;
                        deal.PRICE = Convert.ToDecimal(textBox2.Text);

                        db.DEALS_TBs.InsertOnSubmit(deal);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            Data.DEALS_DETAIL details = new Data.DEALS_DETAIL();
                            details.ITEM_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            details.DEAL_ID = deal.ID;
                            details.ITEM_QUANTITY = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());

                            db.DEALS_DETAILs.InsertOnSubmit(details);
                            db.SubmitChanges();
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Deal added");
                reset();
            }
            catch (TransactionAbortedException err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void GRID_DEALS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Enabled = textBox2.Enabled = true;
            int rowindex = GRID_DEALS.CurrentCell.RowIndex;
            int id = Convert.ToInt32(GRID_DEALS.Rows[rowindex].Cells[0].Value);
            textBox1.Text = GRID_DEALS.Rows[rowindex].Cells[1].Value.ToString();
            textBox2.Text = GRID_DEALS.Rows[rowindex].Cells[2].Value.ToString();

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.DEALS_DETAILs
                       join p in db.ITEMS_TBs on x.ITEM_ID equals p.ID
                       where x.DEAL_ID == id
                       select new { pid = p.ID, pname = p.ITEM_NAME, qty = x.ITEM_QUANTITY };
            foreach (var d in data)
            {
                dataGridView1.Rows.Add(d.pid, d.pname, d.qty);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Deal name is required");
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Deal price is required in numbers");
                textBox2.Focus();
                return;
            }
            if (textBox4.Text == null || textBox4.Text == "")
            {
                MessageBox.Show("Quantity is required in numbers");
                textBox2.Focus();
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items where added for this deal");
                return;
            }
            try {
                using (TransactionScope ts = new TransactionScope()) {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                        int rowindex = GRID_DEALS.CurrentCell.RowIndex;
                        int id = Convert.ToInt32(GRID_DEALS.Rows[rowindex].Cells[0].Value);

                        Data.DEALS_TB deal = db.DEALS_TBs.Single(x => x.ID == id);
                        deal.DEAL_NAME = textBox1.Text;
                        deal.PRICE = Convert.ToDecimal(textBox2.Text);

                        db.SubmitChanges();

                        var del = db.DEALS_DETAILs.Where(x => x.DEAL_ID == id);
                        db.DEALS_DETAILs.DeleteAllOnSubmit(del);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            Data.DEALS_DETAIL details = new Data.DEALS_DETAIL();
                            details.ITEM_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            details.DEAL_ID = deal.ID;
                            details.ITEM_QUANTITY = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());

                            db.DEALS_DETAILs.InsertOnSubmit(details);
                            db.SubmitChanges();
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Deal updated");
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
            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                        {
                            int rowindex = GRID_DEALS.CurrentCell.RowIndex;
                            int id = Convert.ToInt32(GRID_DEALS.Rows[rowindex].Cells[0].Value);

                            Data.DEALS_TB deal = db.DEALS_TBs.Single(x => x.ID == id);
                            var del = db.DEALS_DETAILs.Where(x => x.DEAL_ID== id);
                            db.DEALS_DETAILs.DeleteAllOnSubmit(del);
                            db.DEALS_TBs.DeleteOnSubmit(deal);
                            db.SubmitChanges();
                            ts.Complete();
                        }
                    }
                    MessageBox.Show("Deal deleted");
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
