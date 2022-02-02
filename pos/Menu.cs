using Microsoft.Reporting.WinForms;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            reset();
        }

        private void getMenu()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.MENU_TBs
                       join mp in db.MENU_PRODUCTs on x.ID equals mp.MENU_ID into mpp
                       from mp in mpp.DefaultIfEmpty()
                       join pro in db.PRODUCT_TBs on mp.PRODUCT_ID equals pro.ID into proo
                       from pro in proo.DefaultIfEmpty()
                       join mi in db.MENU_ITEMs on x.ID equals mi.MENU_ID into mii
                       from mi in mii.DefaultIfEmpty()
                       join it in db.ITEMS_TBs on mi.ITEM_ID equals it.ID into itt
                       from it in itt.DefaultIfEmpty()
                       join md in db.MENU_DEALs on x.ID equals md.MENU_ID into mdd
                       from md in mdd.DefaultIfEmpty()
                       join deals in db.DEALS_TBs on md.DEAL_ID equals deals.ID into dealss
                       from deals in dealss.DefaultIfEmpty()
                       join dd in db.DEALS_DETAILs on deals.ID equals dd.DEAL_ID into ddd
                       from dd in ddd.DefaultIfEmpty()
                       join did in db.ITEMS_TBs on dd.ITEM_ID equals did.ID into didi
                       from did in didi.DefaultIfEmpty()
                       where x.STATUS == true
                       select new
                       {
                           MENU_NAME = x.MENU_NAME,
                           PRODUCT_NAME = pro.PRODUCT,
                           PRODUCT_PRICE = pro.SELL_PRICE,
                           ITEM_NAME = it.ITEM_NAME,
                           ITEM_PRICE = it.PRICE,
                           DEAL_NAME = deals.DEAL_NAME,
                           DEAL_PRICE = deals.PRICE,
                           DEAL_ITEM_NAME = did.ITEM_NAME,
                           DEAL_ITEM_QUANTITY = dd.ITEM_QUANTITY
                       };


            //Data.Menu mds = new Data.Menu();
            //DataTable dt = mds.Tables["DataTable_Menu"];
            //DataRow row = null;

            foreach (var rowObj in data)
            {
                //row = dt.NewRow();
                //dt.Rows.Add();
                
            }
            //ReportDataSource datasource = new ReportDataSource("DataSet_Menu", mds.Tables[0]);
            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(datasource);
            //this.reportViewer1.RefreshReport();
        }

        private void getProducts()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.PRODUCT_TBs where x.STATUS == true select new { ID = x.ID, PRODUCT = x.PRODUCT };
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "PRODUCT";
            comboBox1.DataSource = data.ToList();
        }

        private void getItems()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.ITEMS_TBs select new { ID = x.ID, ITEM = x.ITEM_NAME };
            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "ITEM";
            comboBox2.DataSource = data.ToList();
        }

        private void getDeals()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.DEALS_TBs select new { ID = x.ID, DEAL = x.DEAL_NAME };
            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "DEAL";
            comboBox3.DataSource = data.ToList();
        }

        private void reset()
        {
            textBox1.Focus();

            textBox1.Text = textBox3.Text = "";

            comboBox4.Text = "";

            comboBox1.Enabled = comboBox2.Enabled = comboBox3.Enabled = false;

            getProducts();
            getItems();
            getDeals();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            textBox1.Enabled = true;

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.MENU_TBs select new { ID = x.ID, Name = x.MENU_NAME, Status = x.STATUS };
            GRID_MENU.DataSource = data.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                comboBox1.Enabled = comboBox2.Enabled = comboBox3.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = comboBox2.Enabled = comboBox3.Enabled = true;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            dataGridView1.Rows.Add(comboBox1.SelectedValue, comboBox1.Text, "PRODUCT");
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            dataGridView1.Rows.Add(comboBox2.SelectedValue, comboBox2.Text, "ITEM");
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            dataGridView1.Rows.Add(comboBox3.SelectedValue, comboBox3.Text, "DEAL");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Menu name is required");
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Menu empty");
                return;
            }
            if (comboBox4.Text == "" || comboBox4.Text == null)
            {
                MessageBox.Show("Menu status is required");
                return;
            }

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                    {

                        bool status = comboBox4.Text == "True" ? true : false;

                        Data.MENU_TB menu = new Data.MENU_TB();
                        menu.MENU_NAME = textBox1.Text;
                        menu.STATUS = status;

                        db.MENU_TBs.InsertOnSubmit(menu);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            string cat = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            if (cat == "PRODUCT")
                            {
                                Data.MENU_PRODUCT pro = new Data.MENU_PRODUCT();
                                pro.MENU_ID = menu.ID;
                                pro.PRODUCT_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_PRODUCTs.InsertOnSubmit(pro);
                                db.SubmitChanges();
                            }
                            else if (cat == "ITEM")
                            {
                                Data.MENU_ITEM item = new Data.MENU_ITEM();
                                item.MENU_ID = menu.ID;
                                item.ITEM_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_ITEMs.InsertOnSubmit(item);
                                db.SubmitChanges();
                            }
                            else if (cat == "DEAL")
                            {
                                Data.MENU_DEAL deal = new Data.MENU_DEAL();
                                deal.MENU_ID = menu.ID;
                                deal.DEAL_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_DEALs.InsertOnSubmit(deal);
                                db.SubmitChanges();
                            }
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Menu added");
                reset();
            }
            catch (TransactionAbortedException err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void GRID_MENU_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = GRID_MENU.CurrentCell.RowIndex;
            int id = Convert.ToInt32(GRID_MENU.Rows[rowindex].Cells[0].Value);
            textBox1.Text = GRID_MENU.Rows[rowindex].Cells[1].Value.ToString();

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var pro = from x in db.MENU_PRODUCTs
                      join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                      where x.MENU_ID == id
                      select new { pid = p.ID, pname = p.PRODUCT };
            foreach (var p in pro)
            {
                dataGridView1.Rows.Add(p.pid, p.pname, "PRODUCT");
            }
            var items = from x in db.MENU_ITEMs
                       join i in db.ITEMS_TBs on x.ITEM_ID equals i.ID
                       where x.MENU_ID == id
                       select new { iid = i.ID, iname = i.ITEM_NAME };
            foreach (var i in items)
            {
                dataGridView1.Rows.Add(i.iid, i.iname, "ITEM");
            }
            var deals = from x in db.MENU_DEALs
                        join d in db.DEALS_TBs on x.DEAL_ID equals d.ID
                        where x.MENU_ID == id
                        select new { did = d.ID, dname = d.DEAL_NAME };
            foreach (var d in deals)
            {
                dataGridView1.Rows.Add(d.did, d.dname, "DEAL");
            }
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
                var data = from x in db.MENU_TBs
                           where x.MENU_NAME.Contains(textBox3.Text)
                           select new { ID = x.ID, Name = x.MENU_NAME, Status = x.STATUS };
                GRID_MENU.DataSource = data.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Menu name is required");
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Menu empty");
                return;
            }
            if (comboBox4.Text == "" || comboBox4.Text == null)
            {
                MessageBox.Show("Menu status is required");
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                    {
                        bool status = comboBox4.Text == "True" ? true : false;

                        int rowindex = GRID_MENU.CurrentCell.RowIndex;
                        int id = Convert.ToInt32(GRID_MENU.Rows[rowindex].Cells[0].Value);

                        Data.MENU_TB menu = db.MENU_TBs.Single(x=> x.ID == id);
                        menu.MENU_NAME = textBox1.Text;
                        menu.STATUS = status;

                        db.SubmitChanges();

                        var del1 = from x in db.MENU_PRODUCTs where x.MENU_ID == id select x;
                        db.MENU_PRODUCTs.DeleteAllOnSubmit(del1);
                        db.SubmitChanges();

                        var del2 = from x in db.MENU_ITEMs where x.MENU_ID == id select x;
                        db.MENU_ITEMs.DeleteAllOnSubmit(del2);
                        db.SubmitChanges();

                        var del3 = from x in db.MENU_DEALs where x.MENU_ID == id select x;
                        db.MENU_DEALs.DeleteAllOnSubmit(del3);
                        db.SubmitChanges();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            string cat = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            if (cat == "PRODUCT")
                            {
                                Data.MENU_PRODUCT pro = new Data.MENU_PRODUCT();
                                pro.MENU_ID = menu.ID;
                                pro.PRODUCT_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_PRODUCTs.InsertOnSubmit(pro);
                                db.SubmitChanges();
                            }
                            else if (cat == "ITEM")
                            {
                                Data.MENU_ITEM item = new Data.MENU_ITEM();
                                item.MENU_ID = menu.ID;
                                item.ITEM_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_ITEMs.InsertOnSubmit(item);
                                db.SubmitChanges();
                            }
                            else if (cat == "DEAL")
                            {
                                Data.MENU_DEAL deal = new Data.MENU_DEAL();
                                deal.MENU_ID = menu.ID;
                                deal.DEAL_ID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());
                                db.MENU_DEALs.InsertOnSubmit(deal);
                                db.SubmitChanges();
                            }
                        }
                        ts.Complete();
                    }
                }
                MessageBox.Show("Menu updated");
                reset();
            }
            catch (TransactionAbortedException err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                            int rowindex = GRID_MENU.CurrentCell.RowIndex;
                            int id = Convert.ToInt32(GRID_MENU.Rows[rowindex].Cells[0].Value);
                            var del = from x in db.MENU_TBs where x.ID == id select x;
                            db.MENU_TBs.DeleteAllOnSubmit(del);
                            db.SubmitChanges();
                            ts.Complete();
                        }
                    }
                    MessageBox.Show("Menu deleted");
                    reset();
                }
                catch (TransactionAbortedException err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var menu = from x in db.MENU_TBs where x.STATUS == true select x;
            e.Graphics.DrawString("Simple POS", new Font("Arial", 35, FontStyle.Bold), Brushes.Black, 300, 50);
            int marginTop = 100;
            foreach (var rowObj in menu)
            {
                var product = from x in db.MENU_PRODUCTs
                              join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                              where x.MENU_ID == rowObj.ID
                              select new
                              { PRODUCT_NAME = p.PRODUCT, PRODUCT_PRICE = p.SELL_PRICE };
                e.Graphics.DrawString(rowObj.MENU_NAME, new Font("Arial", 25, FontStyle.Bold), Brushes.Black, 50, marginTop);
                if (product.Any())
                {
                    foreach (var p in product)
                    {
                        marginTop = marginTop + 50;
                        e.Graphics.DrawString(p.PRODUCT_NAME,
                            new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 50, marginTop);
                        e.Graphics.DrawString(p.PRODUCT_PRICE.ToString(),
                            new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 500, marginTop);
                    }
                }

                var item = from x in db.MENU_ITEMs
                           join it in db.ITEMS_TBs on x.ITEM_ID equals it.ID
                           where x.MENU_ID == rowObj.ID
                           select new
                           { ITEM_NAME = it.ITEM_NAME, ITEM_PRICE = it.PRICE };
                if (item.Any())
                {
                    foreach (var i in item)
                    {
                        marginTop = marginTop + 50;
                        e.Graphics.DrawString(i.ITEM_NAME,
                           new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 50, marginTop);
                        e.Graphics.DrawString(i.ITEM_PRICE.ToString(),
                            new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 500, marginTop);
                    }
                }

                var deal = (from x in db.MENU_DEALs
                           join d in db.DEALS_TBs on x.DEAL_ID equals d.ID into ddd
                           from d in ddd.DefaultIfEmpty()
                           join dd in db.DEALS_DETAILs on d.ID equals dd.DEAL_ID into dddd
                           from dd in dddd.DefaultIfEmpty()
                           where x.MENU_ID == rowObj.ID
                           select new
                           {
                               ID = d.ID,
                               DEAL_NAME = d.DEAL_NAME,
                               DEAL_PRICE = d.PRICE,
                               DEAL_ITEM_QUANTITY = dd.ITEM_QUANTITY
                           }).Distinct();

                if (deal.Any())
                {
                    foreach (var d in deal)
                    {
                        marginTop = marginTop + 50;
                        e.Graphics.DrawString(d.DEAL_NAME,
                       new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 50, marginTop);
                        e.Graphics.DrawString(d.DEAL_PRICE.ToString(),
                            new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 500, marginTop);
                        var deal_items = from x in db.DEALS_DETAILs
                                         join di in db.ITEMS_TBs on x.ITEM_ID equals di.ID
                                         where x.DEAL_ID == d.ID
                                         select new { deal_id = x.DEAL_ID, DEAL_ITEM_NAME = di.ITEM_NAME, };
                        foreach (var itt in deal_items)
                        {
                            marginTop = marginTop + 50;
                            e.Graphics.DrawString("- " + itt.DEAL_ITEM_NAME,
                                new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 150, marginTop);
                            e.Graphics.DrawString(d.DEAL_ITEM_QUANTITY.ToString(),
                                new Font("Arial", 12, FontStyle.Regular), Brushes.Black, 500, marginTop);
                        }
                    }
                }
                marginTop = marginTop + 50;
            }
        }
    }
}
