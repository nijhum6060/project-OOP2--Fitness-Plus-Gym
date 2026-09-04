using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitnesspluss
{
    public partial class newStaff : Form
    {
        public newStaff()
        {
            InitializeComponent();
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                
                if (string.IsNullOrWhiteSpace(txtFname.Text) ||
                    string.IsNullOrWhiteSpace(txtLname.Text) ||
                    string.IsNullOrWhiteSpace(txtMobile.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Please fill all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string Fname = txtFname.Text.Trim();
                string Lname = txtLname.Text.Trim();
                string gender = radioButton1.Checked ? "Male" : "Female";
                string dob = dateTimePickerDob.Value.ToString("yyyy-MM-dd");
                string joindate = dateTimePickerJd.Value.ToString("yyyy-MM-dd");
                string email = txtEmail.Text.Trim();
                string address = txtAddress.Text.Trim();

                Int64 mobile;
                if (!Int64.TryParse(txtMobile.Text.Trim(), out mobile))
                {
                    MessageBox.Show("Please enter a valid Mobile number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";

                try
                {
                    con.Open();

                    string query = "INSERT INTO newStaff (Fname, Lname, Gender, Dob, Mobile, Email, JoinDate, address) " +
                                   "VALUES (@Fname, @Lname, @Gender, @Dob, @Mobile, @Email, @JoinDate, @address)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    
                    cmd.Parameters.AddWithValue("@Fname", Fname);
                    cmd.Parameters.AddWithValue("@Lname", Lname);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Dob", dob);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@JoinDate", joindate);
                    cmd.Parameters.AddWithValue("@address", address);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        button2_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Failed to save data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFname.Clear();
            txtLname.Clear();
            txtEmail.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtMobile.Clear();
            txtAddress.Clear();
            dateTimePickerDob.Value = DateTime.Now;
            dateTimePickerJd.Value = DateTime.Now;
        }
    }
}
