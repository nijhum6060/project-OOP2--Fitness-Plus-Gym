using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Fitnesspluss
{
    public partial class Eqiupment : Form
    {
        public Eqiupment()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtEquipment.Text) ||
                string.IsNullOrWhiteSpace(txtMuscles.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtCost.Text))
            {
                MessageBox.Show("Please fill all fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string equipName = txtEquipment.Text.Trim();
            string Description = txtDescription.Text.Trim();
            string MUsed = txtMuscles.Text.Trim();
            string DDate = dateTimePickerDD.Value.ToString("yyyy-MM-dd");

            Int64 Cost;
            if (!Int64.TryParse(txtCost.Text.Trim(), out Cost))
            {
                MessageBox.Show("Please enter a valid Cost (numbers only).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";

            try
            {
                con.Open();

                string query = @"INSERT INTO Equipment
                        (equipName, description, MUsed, DDate, Cost)
                        VALUES
                        (@equipName, @description, @MUsed, @DDate, @Cost)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@equipName", equipName);
                cmd.Parameters.AddWithValue("@description", Description);
                cmd.Parameters.AddWithValue("@MUsed", MUsed);
                cmd.Parameters.AddWithValue("@DDate", DDate);
                cmd.Parameters.AddWithValue("@Cost", Cost);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Data Saved Successfully!", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        

        private void button2_Click(object sender, EventArgs e)
        {
            txtEquipment.Clear();
            txtDescription.Clear();
            txtMuscles.Clear();
            txtCost.Clear();
            dateTimePickerDD.Value = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewEquipment viewEq = new viewEquipment();
            viewEq.Show();
        }
    }
}
