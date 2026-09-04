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
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { if (string.IsNullOrWhiteSpace(textBox1.Text))
    {
        MessageBox.Show("Please enter a Member ID", "Missing ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
 
    int memberId;
    if (!int.TryParse(textBox1.Text.Trim(), out memberId))
    {
        MessageBox.Show("Please enter a valid numeric ID", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }
 
    
    DialogResult result = MessageBox.Show("Are you sure you want to delete this member?",
                                          "Delete Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
 
    if (result == DialogResult.Yes)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";
 
        try
        {
            con.Open();
 
            string query = "DELETE FROM newMember WHERE Id = @Id";
 
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", memberId);
 
            int rows = cmd.ExecuteNonQuery();
 
            if (rows > 0)
            {
                MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                LoadAllMembers();   
            }
            else
            {
                MessageBox.Show("No member found with this ID", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
 
private void Delete_Load(object sender, EventArgs e)
{
    LoadAllMembers();   
}
 
private void LoadAllMembers()
{
    SqlConnection con = new SqlConnection();
    con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";
 
    try
    {
        con.Open();
 
        string query = "SELECT * FROM newMember";
 
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DataSet DS = new DataSet();
        DA.Fill(DS);
 
        dataGridView1.DataSource = DS.Tables[0];
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM newMembers";

            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            dataGridView1.DataSource = DS.Tables[0];
        }

        
    }
}
