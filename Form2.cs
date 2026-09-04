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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(comboBoxGymTime.Text) ||
                string.IsNullOrWhiteSpace(comboBoxMemberTime.Text))
            {
                MessageBox.Show("Please fill all fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Fname = txtFirstName.Text.Trim();
            string Lname = txtLastName.Text.Trim();
            string gender = radioButton1.Checked ? "Male" : "Female";
            string dob = dateTimePickerDOb.Value.ToString("yyyy-MM-dd");
            string joindate = dateTimePickerJd.Value.ToString("yyyy-MM-dd");
            string email = txtEmail.Text.Trim();
            string gymTime = comboBoxGymTime.Text;
            string address = txtAddress.Text.Trim();
            string membership = comboBoxMemberTime.Text;

            Int64 mobile;
            if (!Int64.TryParse(txtNumber.Text.Trim(), out mobile))
            {
                MessageBox.Show("Please enter a valid Mobile number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";

            try
            {
                con.Open();

                string query = @"INSERT INTO newMember
                        (Fname, Lname, Gender, Dob, Mobile, Email, JoinDate, Gymtime, Maddress, MembershipTime)
                        VALUES
                        (@Fname, @Lname, @Gender, @Dob, @Mobile, @Email, @JoinDate, @Gymtime, @Maddress, @MembershipTime)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Fname", Fname);
                cmd.Parameters.AddWithValue("@Lname", Lname);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Dob", dob);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@JoinDate", joindate);
                cmd.Parameters.AddWithValue("@Gymtime", gymTime);
                cmd.Parameters.AddWithValue("@Maddress", address);
                cmd.Parameters.AddWithValue("@MembershipTime", membership);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Data Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReset_Click(null, null);
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
        

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtNumber.Clear();
            txtAddress.Clear();
            comboBoxGymTime.ResetText();
            comboBoxMemberTime.ResetText();
            dateTimePickerDOb.Value = DateTime.Now;
            dateTimePickerJd.Value = DateTime.Now;

        }
    }
}
