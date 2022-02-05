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
    public partial class Discount : Form
    {
        public Discount()
        {
            InitializeComponent();
            reset();
        }

        private void getProducts()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.PRODUCT_TBs where x.STATUS == true select new { id = x.ID, product = x.PRODUCT };
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "product";
            comboBox1.DataSource = data.ToList();
        }
        private void getItems()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.ITEMS_TBs select new { id = x.ID, item = x.ITEM_NAME };
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "item";
            comboBox1.DataSource = data.ToList();
        }
        private void getDeals()
        {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.DEALS_TBs select new { id = x.ID, deal = x.DEAL_NAME };
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "deal";
            comboBox1.DataSource = data.ToList();
        }

        private void reset()
        {
            comboBox2.Focus();
            comboBox2.Text = comboBox4.Text = comboBox5.Text = textBox1.Text =
                textBox2.Text = textBox3.Text = "";

            label4.Enabled = comboBox1.Enabled = false;

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var discounts = from x in db.DISCOUNT_TBs select x;
            List<Models.discount> obj = new List<Models.discount>();
            foreach (var dis in discounts) {
                if (dis.TYPE == "PRODUCT")
                {
                    var pro = from x in db.PRODUCT_TBs where x.ID == dis.TYPE_ID select new { product = x.PRODUCT };
                    foreach (var p in pro) {
                        Models.discount objDis = new Models.discount();
                        objDis.id = dis.ID;
                        objDis.type = dis.TYPE;
                        objDis.type_name = p.product;
                        objDis.code = dis.CODE;
                        objDis.amount = (Decimal)dis.AMOUNT;
                        objDis.cal_type = dis.CALCULATION_TYPE;
                        objDis.status = (bool)dis.STATUS;
                        obj.Add(objDis);
                    }
                }
                if (dis.TYPE == "ITEM")
                {
                    var items = from x in db.ITEMS_TBs where x.ID == dis.TYPE_ID select new { item = x.ITEM_NAME };
                    foreach (var i in items)
                    {
                        Models.discount objDis = new Models.discount();
                        objDis.id = dis.ID;
                        objDis.type = dis.TYPE;
                        objDis.type_name = i.item;
                        objDis.code = dis.CODE;
                        objDis.amount = (Decimal)dis.AMOUNT;
                        objDis.cal_type = dis.CALCULATION_TYPE;
                        objDis.status = (bool)dis.STATUS;
                        obj.Add(objDis);
                    }
                }
                if (dis.TYPE == "DEAL")
                {
                    var deals = from x in db.DEALS_TBs where x.ID == dis.TYPE_ID select new { deal = x.DEAL_NAME };
                    foreach (var d in deals)
                    {
                        Models.discount objDis = new Models.discount();
                        objDis.id = dis.ID;
                        objDis.type = dis.TYPE;
                        objDis.type_name = d.deal;
                        objDis.code = dis.CODE;
                        objDis.amount = (Decimal)dis.AMOUNT;
                        objDis.cal_type = dis.CALCULATION_TYPE;
                        objDis.status = (bool)dis.STATUS;
                        obj.Add(objDis);
                    }
                }
            }
            dataGridView1.DataSource = obj;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            if (comboBox2.Text == "PRODUCT")
            {
                label4.Text = "Products";
                label4.Enabled = comboBox1.Enabled = true;
                getProducts();
            }
            else if (comboBox2.Text == "ITEM")
            {
                label4.Text = "Items";
                label4.Enabled = comboBox1.Enabled = true;
                getItems();
            }
            else if (comboBox2.Text == "DEAL")
            {
                label4.Text = "Deals";
                label4.Enabled = comboBox1.Enabled = true;
                getDeals();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == null || comboBox2.Text == "")
            {
                MessageBox.Show("Type is required");
                return;
            }
            if (comboBox1.Text == null || comboBox1.Text == "")
            {
                MessageBox.Show("Type items is required");
                return;
            }
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Code is required");
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Amount is required in numbers");
                return;
            }
            if (comboBox4.Text == null || comboBox4.Text == "")
            {
                MessageBox.Show(" Calculation Type is required");
                return;
            }
            if (comboBox5.Text == null || comboBox5.Text == "")
            {
                MessageBox.Show(" Status is required");
                return;
            }
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    var q = from x in db.DISCOUNT_TBs where x.CODE == textBox1.Text select x;
                    if (q.Any())
                    {
                        MessageBox.Show("Code already exists");
                        return;
                    }
                    bool status = comboBox5.Text == "True" ? true : false;
                    Data.DISCOUNT_TB data = new Data.DISCOUNT_TB();
                    data.TYPE = comboBox2.Text;
                    data.TYPE_ID = Convert.ToInt32(comboBox1.SelectedValue);
                    data.CODE = textBox1.Text;
                    data.AMOUNT = Convert.ToDecimal(textBox2.Text);
                    data.CALCULATION_TYPE = comboBox4.Text;
                    data.STATUS = status;

                    db.DISCOUNT_TBs.InsertOnSubmit(data);
                    db.SubmitChanges();

                    MessageBox.Show("Discount added");
                    reset();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == null || comboBox2.Text == "")
            {
                MessageBox.Show("Type is required");
                return;
            }
            if (comboBox1.Text == null || comboBox1.Text == "")
            {
                MessageBox.Show("Type items is required");
                return;
            }
            if (textBox1.Text == null || textBox1.Text == "")
            {
                MessageBox.Show("Code is required");
                return;
            }
            if (textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Amount is required in numbers");
                return;
            }
            if (comboBox4.Text == null || comboBox4.Text == "")
            {
                MessageBox.Show(" Calculation Type is required");
                return;
            }
            if (comboBox5.Text == null || comboBox5.Text == "")
            {
                MessageBox.Show(" Status is required");
                return;
            }
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    int rowindex = dataGridView1.CurrentCell.RowIndex;
                    int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
                    var q = from x in db.DISCOUNT_TBs where x.CODE == textBox1.Text && x.ID != id select x;
                    if (q.Any())
                    {
                        MessageBox.Show("Code already exists");
                        return;
                    }
                    bool status = comboBox5.Text == "True" ? true : false;
                    Data.DISCOUNT_TB data = db.DISCOUNT_TBs.Single(x => x.ID == id);
                    data.TYPE = comboBox2.Text;
                    data.TYPE_ID = Convert.ToInt32(comboBox1.SelectedValue);
                    data.CODE = textBox1.Text;
                    data.AMOUNT = Convert.ToDecimal(textBox2.Text);
                    data.CALCULATION_TYPE = comboBox4.Text;
                    data.STATUS = status;

                    db.SubmitChanges();

                    MessageBox.Show("Discount updated");
                    reset();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    int rowindex = dataGridView1.CurrentCell.RowIndex;
                    int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);

                    Data.DISCOUNT_TB data = db.DISCOUNT_TBs.Single(x => x.ID == id);

                    db.DISCOUNT_TBs.DeleteOnSubmit(data);
                    db.SubmitChanges();

                    MessageBox.Show("Discount deleted");
                    reset();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
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
                var discounts = from x in db.DISCOUNT_TBs where x.CODE.Contains(textBox3.Text) select x;
                List<Models.discount> obj = new List<Models.discount>();
                foreach (var dis in discounts)
                {
                    if (dis.TYPE == "PRODUCT")
                    {
                        var pro = from x in db.PRODUCT_TBs where x.ID == dis.TYPE_ID select new { product = x.PRODUCT };
                        foreach (var p in pro)
                        {
                            Models.discount objDis = new Models.discount();
                            objDis.id = dis.ID;
                            objDis.type = dis.TYPE;
                            objDis.type_name = p.product;
                            objDis.code = dis.CODE;
                            objDis.amount = (Decimal)dis.AMOUNT;
                            objDis.cal_type = dis.CALCULATION_TYPE;
                            objDis.status = (bool)dis.STATUS;
                            obj.Add(objDis);
                        }
                    }
                    if (dis.TYPE == "ITEM")
                    {
                        var items = from x in db.ITEMS_TBs where x.ID == dis.TYPE_ID select new { item = x.ITEM_NAME };
                        foreach (var i in items)
                        {
                            Models.discount objDis = new Models.discount();
                            objDis.id = dis.ID;
                            objDis.type = dis.TYPE;
                            objDis.type_name = i.item;
                            objDis.code = dis.CODE;
                            objDis.amount = (Decimal)dis.AMOUNT;
                            objDis.cal_type = dis.CALCULATION_TYPE;
                            objDis.status = (bool)dis.STATUS;
                            obj.Add(objDis);
                        }
                    }
                    if (dis.TYPE == "DEAL")
                    {
                        var deals = from x in db.DEALS_TBs where x.ID == dis.TYPE_ID select new { deal = x.DEAL_NAME };
                        foreach (var d in deals)
                        {
                            Models.discount objDis = new Models.discount();
                            objDis.id = dis.ID;
                            objDis.type = dis.TYPE;
                            objDis.type_name = d.deal;
                            objDis.code = dis.CODE;
                            objDis.amount = (Decimal)dis.AMOUNT;
                            objDis.cal_type = dis.CALCULATION_TYPE;
                            objDis.status = (bool)dis.STATUS;
                            obj.Add(objDis);
                        }
                    }
                }
                dataGridView1.DataSource = obj;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);

            comboBox2.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();

            if (comboBox2.Text == "PRODUCT") { label4.Text = "Products";  }
            if (comboBox2.Text == "ITEM") { label4.Text = "Items"; }
            if (comboBox2.Text == "DEAL") { label4.Text = "Deals"; }
            comboBox1.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            label4.Enabled = comboBox1.Enabled = true;

            textBox1.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();

        }
    }
}
