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
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            resetBrand();
            resetCategory();
            resetProduct();
            resetInventory();
        }

        private void getBrands() {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.BRAND_TBs where x.STATUS == true select new { ID = x.ID, BRAND = x.BRAND  };
            PRODUCT_BRANDS.DisplayMember = "BRAND";
            PRODUCT_BRANDS.ValueMember = "ID";
            PRODUCT_BRANDS.DataSource = q.ToList();
        }

        private void getCategories() {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.CATEGORY_TBs where x.STATUS == true select new { ID = x.ID, CATEGORY = x.CATEGORY };
            PRODUCT_CATE.ValueMember = "ID";
            PRODUCT_CATE.DisplayMember = "CATEGORY";
            PRODUCT_CATE.DataSource = q.ToList();
        }

        private void getProducts() {
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.PRODUCT_TBs where x.STATUS == true select new { ID = x.ID,  PRODUCT = x.PRODUCT };
            INV_PRODUCT.ValueMember = "ID";
            INV_PRODUCT.DisplayMember = "PRODUCT";
            INV_PRODUCT.DataSource = q.ToList();
        }

        private void resetInventory() {
            INV_PRODUCT.Focus();

            INV_QUANTITY.Text = INV_BUYING.Text = INV_TOTAL.Text = "0";

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.INVENTORY_TBs
                       join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                       select new {
                           ID = x.ID,
                           Product = p.PRODUCT,
                           Buying = x.BUYING_PRICE,
                           Quantity = x.QUANTITY,
                           Total = x.TOTAL,
                           Created_at = x.CREATED_AT,
                       };
            GRID_INV.DataSource = data.ToList();

            getProducts();
        }

        private void resetProduct() {
            PRODUCT_NAME.Focus();

            PRODUCT_NAME.Text = PRODUCT_PRICE.Text = "";

            getBrands();
            getCategories();

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var q = from x in db.PRODUCT_TBs
                    join b in db.BRAND_TBs on x.BRAND_ID equals b.ID
                    join c in db.CATEGORY_TBs on x.CATEGORY_ID equals c.ID
                    where x.STATUS == true
                    select new {
                        Id = x.ID,
                        Brand = b.BRAND,
                        Category = c.CATEGORY,
                        Product = x.PRODUCT,
                        Sell_Price = x.SELL_PRICE,
                        Quantity = x.QUANTITY,
                        Total = x.TOTAL,
                        Created = x.CREATED_AT
                    };
            GRID_PRODUCTS.DataSource = q.ToList();

            getProducts();
        }

        private void resetCategory() {
            CAT_NAME.Focus();

            CAT_NAME.Text = "";

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.CATEGORY_TBs where x.STATUS == true select new { ID = x.ID, Category = x.CATEGORY   };
            GRID_CAT.DataSource = data.ToList();
            getBrands();
            getCategories();
        }

        private void resetBrand() {
            BRAND_NAME.Focus();

            BRAND_NAME.Text = "";

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var data = from x in db.BRAND_TBs where x.STATUS == true select new { ID = x.ID, Brand = x.BRAND };
            GRiD_BRAND.DataSource = data.ToList();
            getBrands();
            getCategories();
        }

        private void CAT_SAVE_Click(object sender, EventArgs e)
        {
            if ( CAT_NAME.Text == "" || CAT_NAME.Text == null ) {
                MessageBox.Show("Category Name is required");
                return;
            }

            try {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {

                    var cat = from x in db.CATEGORY_TBs where x.CATEGORY == CAT_NAME.Text select x;
                    if (cat.Any())
                    {
                        MessageBox.Show("Category name already exists");
                        return;
                    }

                    Data.CATEGORY_TB data = new Data.CATEGORY_TB();
                    data.CATEGORY = CAT_NAME.Text;
                    data.STATUS = true;
                    db.CATEGORY_TBs.InsertOnSubmit(data);
                    db.SubmitChanges();

                    MessageBox.Show("Category Added");
                    resetCategory();
                }
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
        }

        private void BRAND_SAVE_Click(object sender, EventArgs e)
        {
            if (BRAND_NAME.Text == "" || BRAND_NAME.Text == null)
            {
                MessageBox.Show("Brand Name is required");
                return;
            }

            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    var brand = from x in db.BRAND_TBs where x.BRAND == BRAND_NAME.Text select x;
                    if (brand.Any())
                    {
                        MessageBox.Show("Brand name already exists");
                        return;
                    }

                    Data.BRAND_TB data = new Data.BRAND_TB();
                    data.BRAND = BRAND_NAME.Text;
                    data.STATUS = true;
                    db.BRAND_TBs.InsertOnSubmit(data);
                    db.SubmitChanges();

                    MessageBox.Show("Brand Added");
                    resetBrand();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CAT_RESET_Click(object sender, EventArgs e)
        {
            resetCategory();
        }

        private void BRAND_RESET_Click(object sender, EventArgs e)
        {
            resetBrand();
        }

        private void CAT_DELETE_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete this category", "Title", MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                int rowindex = GRID_CAT.CurrentCell.RowIndex;
                string id = GRID_CAT.Rows[rowindex].Cells[0].Value.ToString();
                try {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                        Data.CATEGORY_TB data = db.CATEGORY_TBs.Single(x => x.ID == Convert.ToInt32(id));
                        data.STATUS = false;
                        db.SubmitChanges();

                        MessageBox.Show("Category Deleted");
                        resetCategory();
                    }
                }
                catch (Exception err) {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void BRAND_DELETE_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete this brand", "Title", MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                int rowindex = GRiD_BRAND.CurrentCell.RowIndex;
                string id = GRiD_BRAND.Rows[rowindex].Cells[0].Value.ToString();
                try
                {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                    {
                        Data.BRAND_TB data = db.BRAND_TBs.Single(x => x.ID == Convert.ToInt32(id));
                        data.STATUS = false;
                        db.SubmitChanges();

                        MessageBox.Show("Brand Deleted");
                        resetBrand();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void GRID_CAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = GRID_CAT.CurrentCell.RowIndex;
            CAT_NAME.Text = GRID_CAT.Rows[rowindex].Cells[1].Value.ToString();
        }

        private void GRiD_BRAND_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = GRiD_BRAND.CurrentCell.RowIndex;
            BRAND_NAME.Text = GRiD_BRAND.Rows[rowindex].Cells[1].Value.ToString();
        }

        private void CAT_UPDATE_Click(object sender, EventArgs e)
        {
            if (CAT_NAME.Text == "" || CAT_NAME.Text == null)
            {
                MessageBox.Show("Category Name is required");
                return;
            }

            int rowindex = GRID_CAT.CurrentCell.RowIndex;
            string id = GRID_CAT.Rows[rowindex].Cells[0].Value.ToString();
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {

                    //var cat = from x in db.CATEGORY_TBs where x.CATEGORY == CAT_NAME.Text select x;
                    //if (cat.Any())
                    //{
                    //    MessageBox.Show("Category name already exists");
                    //    return;
                    //}

                    Data.CATEGORY_TB data = db.CATEGORY_TBs.Single(x => x.ID == Convert.ToInt32(id));
                    data.CATEGORY = CAT_NAME.Text;
                    data.STATUS = true;
                    db.SubmitChanges();

                    MessageBox.Show("Category Updated");
                    resetCategory();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void BRAND_UPDATE_Click(object sender, EventArgs e)
        {
            if (BRAND_NAME.Text == "" || BRAND_NAME.Text == null)
            {
                MessageBox.Show("Brand Name is required");
                return;
            }

            int rowindex = GRiD_BRAND.CurrentCell.RowIndex;
            string id = GRiD_BRAND.Rows[rowindex].Cells[0].Value.ToString();
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {

                    //var brand = from x in db.BRAND_TBs where x.BRAND == BRAND_NAME.Text select x;
                    //if (brand.Any())
                    //{
                    //    MessageBox.Show("Brand name already exists");
                    //    return;
                    //}

                    Data.BRAND_TB data = db.BRAND_TBs.Single(x => x.ID == Convert.ToInt32(id));
                    data.BRAND = BRAND_NAME.Text;
                    data.STATUS = true;
                    db.SubmitChanges();

                    MessageBox.Show("Brand Updated");
                    resetBrand();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CAT_SEARCH_KeyPress(object sender, KeyPressEventArgs e)
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
                var data = from x in db.CATEGORY_TBs
                           where
                              x.STATUS == true
                              && x.CATEGORY.Equals(textInput)
                           select new { ID = x.ID, Category = x.CATEGORY };
                GRID_CAT.DataSource = data.ToList();
            }
        }

        private void BRAND_SEARCH_KeyPress(object sender, KeyPressEventArgs e)
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
                var data = from x in db.BRAND_TBs
                           where
                              x.STATUS == true
                              && x.BRAND.Equals(textInput)
                           select new { ID = x.ID, Brand = x.BRAND};
                GRiD_BRAND.DataSource = data.ToList();
            }
        }

        private void PRODUCT_RESET_Click(object sender, EventArgs e)
        {
            resetProduct();
        }

        private void PRODUCT_SAVE_Click(object sender, EventArgs e)
        {
            if (PRODUCT_NAME.Text == "" || PRODUCT_NAME.Text == null)
            {
                MessageBox.Show("Product Name required");
                return;
            }
            if (PRODUCT_PRICE.Text == "" || PRODUCT_PRICE.Text == null)
            {
                MessageBox.Show("Product Price required and in decimals");
                return;
            }
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    var prod = from x in db.PRODUCT_TBs where x.PRODUCT == PRODUCT_NAME.Text select x;
                    if (prod.Any())
                    {
                        MessageBox.Show("Product already exists");
                        return;
                    }
                    Data.PRODUCT_TB data = new Data.PRODUCT_TB();
                    data.BRAND_ID = Convert.ToInt32(PRODUCT_BRANDS.SelectedValue);
                    data.CATEGORY_ID = Convert.ToInt32(PRODUCT_CATE.SelectedValue);
                    data.PRODUCT = PRODUCT_NAME.Text;
                    data.SELL_PRICE = Convert.ToDecimal(PRODUCT_PRICE.Text);
                    data.QUANTITY = 0;
                    data.TOTAL = 0;
                    data.CREATED_AT = DateTime.Now;
                    data.STATUS = true;
                    db.PRODUCT_TBs.InsertOnSubmit(data);
                    db.SubmitChanges();

                    MessageBox.Show("Product Added");
                    resetProduct();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void PRODUCT_DELETE_Click(object sender, EventArgs e)
        {
            try {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                    DialogResult dr = MessageBox.Show("Are you sure to delete this product", "Title", MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes) {
                        int rowindex = GRID_PRODUCTS.CurrentCell.RowIndex;
                        string id = GRID_PRODUCTS.Rows[rowindex].Cells[0].Value.ToString();

                        Data.PRODUCT_TB data = db.PRODUCT_TBs.Single(x => x.ID == Convert.ToInt32(id));
                        data.STATUS = false;
                        db.SubmitChanges();
                        MessageBox.Show("Product Deleted");
                        resetProduct();
                    }
                }
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void PRODUCT_UPDATE_Click(object sender, EventArgs e)
        {
            if (PRODUCT_NAME.Text == "" || PRODUCT_NAME.Text == null)
            {
                MessageBox.Show("Product Name required");
                return;
            }
            if (PRODUCT_PRICE.Text == "" || PRODUCT_PRICE.Text == null)
            {
                MessageBox.Show("Product Price required and in decimals");
                return;
            }
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    //var prod = from x in db.PRODUCT_TBs where x.PRODUCT == PRODUCT_NAME.Text select x;
                    //if (prod.Any())
                    //{
                    //    MessageBox.Show("Product already exists");
                    //    return;
                    //}
                    int rowindex = GRID_PRODUCTS.CurrentCell.RowIndex;
                    string id = GRID_PRODUCTS.Rows[rowindex].Cells[0].Value.ToString();

                    Data.PRODUCT_TB data = db.PRODUCT_TBs.Single(x => x.ID == Convert.ToInt32(id));

                    data.BRAND_ID = Convert.ToInt32(PRODUCT_BRANDS.SelectedValue);
                    data.CATEGORY_ID = Convert.ToInt32(PRODUCT_CATE.SelectedValue);
                    data.PRODUCT = PRODUCT_NAME.Text;
                    data.SELL_PRICE = Convert.ToDecimal(PRODUCT_PRICE.Text);
                    data.CREATED_AT = DateTime.Now;
                    data.STATUS = true;
                    db.SubmitChanges();

                    MessageBox.Show("Product Updated");
                    resetProduct();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }
        }

        private void GRID_PRODUCTS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = GRID_PRODUCTS.CurrentCell.RowIndex;
            PRODUCT_BRANDS.Text = GRID_PRODUCTS.Rows[rowindex].Cells[1].Value.ToString();
            PRODUCT_CATE.Text = GRID_PRODUCTS.Rows[rowindex].Cells[2].Value.ToString();
            PRODUCT_NAME.Text = GRID_PRODUCTS.Rows[rowindex].Cells[3].Value.ToString();
            PRODUCT_PRICE.Text = GRID_PRODUCTS.Rows[rowindex].Cells[4].Value.ToString();
        }

        private void PRODUCT_SEARCH_KeyPress(object sender, KeyPressEventArgs e)
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
                var q = from x in db.PRODUCT_TBs
                        join b in db.BRAND_TBs on x.BRAND_ID equals b.ID
                        join c in db.CATEGORY_TBs on x.CATEGORY_ID equals c.ID
                        where x.STATUS == true 
                        && b.BRAND.Contains(PRODUCT_SEARCH.Text)
                        || c.CATEGORY.Contains(PRODUCT_SEARCH.Text)
                        || x.PRODUCT.Contains(PRODUCT_SEARCH.Text)
                        select new
                        {
                            Id = x.ID,
                            Brand = b.BRAND,
                            Category = c.CATEGORY,
                            Product = x.PRODUCT,
                            Sell_Price = x.SELL_PRICE,
                            Quantity = x.QUANTITY,
                            Total = x.TOTAL,
                            Created = x.CREATED_AT
                        };
                GRID_PRODUCTS.DataSource = q.ToList();
            }
        }

        private void INV_RESET_Click(object sender, EventArgs e)
        {
            resetInventory();
        }

        private void INV_SEARCH_KeyPress(object sender, KeyPressEventArgs e)
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
                var data = from x in db.INVENTORY_TBs
                           join p in db.PRODUCT_TBs on x.PRODUCT_ID equals p.ID
                           where p.PRODUCT.Contains(INV_SEARCH.Text)
                           select new
                           {
                               ID = x.ID,
                               Product = p.PRODUCT,
                               Buying = x.BUYING_PRICE,
                               Quantity = x.QUANTITY,
                               Total = x.TOTAL,
                               Created_at = x.CREATED_AT,
                           };
                GRID_INV.DataSource = data.ToList();
            }
        }

        private void INV_SAVE_Click(object sender, EventArgs e)
        {
            if (INV_QUANTITY.Text == "" || INV_QUANTITY.Text == null) {
                MessageBox.Show("Quantity is required in numbers");
                return;
            }
            if (INV_BUYING.Text == "" || INV_BUYING.Text == null) {
                MessageBox.Show("Price is required in numbers");
                return;
            }
            
            try {
                using (TransactionScope ts = new TransactionScope()) {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {

                                Data.INVENTORY_TB data = new Data.INVENTORY_TB();
                                data.PRODUCT_ID = Convert.ToInt32(INV_PRODUCT.SelectedValue);
                                data.BUYING_PRICE = Convert.ToDecimal(INV_BUYING.Text);
                                data.QUANTITY = Convert.ToInt32(INV_QUANTITY.Text);
                                data.TOTAL = Convert.ToDecimal(INV_TOTAL.Text);
                                data.CREATED_AT = DateTime.Now;
                                data.UPDATED_AT = DateTime.Now;

                                db.INVENTORY_TBs.InsertOnSubmit(data);

                                Data.PRODUCT_TB prod = db.PRODUCT_TBs.Single(x => x.ID == data.PRODUCT_ID);
                                prod.QUANTITY = Convert.ToInt32(INV_QUANTITY.Text);
                                prod.TOTAL = prod.QUANTITY * prod.SELL_PRICE;

                                db.SubmitChanges();
                                ts.Complete();
                            }
                    }
                MessageBox.Show("Inventory Added");
                resetInventory();
            }
            catch (TransactionAbortedException err) {
                MessageBox.Show(err.Message);
            }
            
        }

        private void INV_QUANTITY_TextChanged(object sender, EventArgs e)
        {
            if (INV_QUANTITY.Text == "" || INV_QUANTITY.Text == null) { return; }
            if (INV_BUYING.Text == "" || INV_BUYING.Text == null) { return; }
            int qty = Convert.ToInt32(INV_QUANTITY.Text);
            decimal price = Convert.ToDecimal(INV_BUYING.Text);
            INV_TOTAL.Text = (qty * price).ToString();

        }

        private void INV_BUYING_TextChanged(object sender, EventArgs e)
        {
            if (INV_QUANTITY.Text == "" || INV_QUANTITY.Text == null) { return; }
            if (INV_BUYING.Text == "" || INV_BUYING.Text == null) { return; }
            int qty = Convert.ToInt32(INV_QUANTITY.Text);
            decimal price = Convert.ToDecimal(INV_BUYING.Text);
            INV_TOTAL.Text = (qty * price).ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                {
                        resetBrand();
                        resetCategory();
                        break;
                }
                case 1:
                {
                        resetProduct();
                        break;
                }
                case 2:
                {
                        resetInventory();
                        break;
                }
            }
        }

        private void INV_DELETE_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete this inventory", "Title", MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Information);
            if (dr == DialogResult.Yes) {
                int rowindex = GRID_INV.CurrentCell.RowIndex;
                string id = GRID_INV.Rows[rowindex].Cells[0].Value.ToString();
                try {
                    using (TransactionScope ts = new TransactionScope()) {
                        using (Data.ConnectionDataContext db = new Data.ConnectionDataContext()) {
                            Data.INVENTORY_TB inv = db.INVENTORY_TBs.Single(x => x.ID == Convert.ToInt32(id));
                            Data.PRODUCT_TB prod = db.PRODUCT_TBs.Single(x => x.ID == inv.PRODUCT_ID);
                            prod.QUANTITY = prod.QUANTITY - inv.QUANTITY;
                            prod.TOTAL = prod.QUANTITY * prod.SELL_PRICE;

                            db.INVENTORY_TBs.DeleteOnSubmit(inv);
                            db.SubmitChanges();

                            ts.Complete();
                        }
                    }
                    resetInventory();
                    MessageBox.Show("Inventory Deleted");
                }
                catch (TransactionAbortedException err) {
                    MessageBox.Show(err.Message);
                }
            }
        }

    }
}
