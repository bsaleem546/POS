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
    public partial class AllUsers : Form
    {
        public AllUsers()
        {
            InitializeComponent();
            reset();
        }

        private bool validate() {
            if ( textBox1.Text == "" ) { return false; }
            if ( textBox3.Text == "" ) { return false; }
            if ( textBox5.Text == "" ) { return false; }
            if ( comboBox1.Text == "" ) { return false; }
            if ( Models.Users.USERNAME == "" ) { return false; }
            return true;
        }

        private void reset() {
            textBox1.Focus();
            textBox1.Text = textBox2.Text = textBox3.Text = textBox5.Text = textBox6.Text = "";
            comboBox1.Text = "";

            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var users = from x in db.USERS_TBs  select new {
                ID = x.ID,
                Name = x.NAME,
                Username = x.USERNAME,
                Contact = x.CONTACT,
                Role = x.ROLE
            };

            dataGridView1.DataSource = users.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool validation = validate();
            if (validation == false) {
                MessageBox.Show("All Fields are required except contact no");
                return;
            }
            try {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    var user = from x in db.USERS_TBs where x.USERNAME == textBox3.Text select x;
                    if (user.Any()) {
                        MessageBox.Show("User name already exists");
                        return;
                    }
                    Data.USERS_TB newUser = new Data.USERS_TB();
                    newUser.NAME = textBox1.Text;
                    newUser.USERNAME = textBox3.Text;
                    newUser.PASSWORD = textBox5.Text;
                    newUser.CONTACT = textBox2.Text;
                    newUser.ROLE = comboBox1.SelectedItem.ToString();
                    newUser.STATUS = true;
                    db.USERS_TBs.InsertOnSubmit( newUser );
                    db.SubmitChanges();

                    reset();

                    MessageBox.Show( "New User Added" );
                }
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete this user", "Title", MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                //int columnindex = dataGridView1.CurrentCell.ColumnIndex;
                string id = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                try
                {
                    using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                    {
                        Data.USERS_TB objUser = db.USERS_TBs.Single(x => x.ID == Convert.ToInt32(id) );
                        db.USERS_TBs.DeleteOnSubmit(objUser);
                        db.SubmitChanges();
                        reset();
                        MessageBox.Show( "User Deleted Successfully" );
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool validation = validate();
            if (validation == false)
            {
                MessageBox.Show("All Fields are required except contact no");
                return;
            }

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            string id = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            try
            {
                using (Data.ConnectionDataContext db = new Data.ConnectionDataContext())
                {
                    Data.USERS_TB objUser = db.USERS_TBs.Single(x => x.ID == Convert.ToInt32(id));
                    objUser.NAME = textBox1.Text;
                    objUser.USERNAME = textBox3.Text;
                    objUser.PASSWORD = textBox5.Text;
                    objUser.CONTACT = textBox2.Text;
                    objUser.ROLE = comboBox1.SelectedItem.ToString();
                    db.SubmitChanges();
                    reset();
                    MessageBox.Show("User Updated Successfully");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text == null) { reset(); }
            Data.ConnectionDataContext db = new Data.ConnectionDataContext();
            var users = from x in db.USERS_TBs where
                        x.NAME.Contains(textBox6.Text) || x.USERNAME.Contains(textBox6.Text) || x.ROLE.Contains(textBox6.Text)
                        select new
                        {
                            ID = x.ID,
                            Name = x.NAME,
                            Username = x.USERNAME,
                            Contact = x.CONTACT,
                            Role = x.ROLE
                        };

            dataGridView1.DataSource = users.ToList();
        }
    }
}
