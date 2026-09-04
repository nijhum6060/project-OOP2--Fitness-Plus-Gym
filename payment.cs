using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fitnesspluss
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberId.Text) ||
                string.IsNullOrWhiteSpace(txtMemberName.Text) ||
                string.IsNullOrWhiteSpace(txtPaymentType.Text) ||
                string.IsNullOrWhiteSpace(txtAmount.Text) ||
                string.IsNullOrWhiteSpace(txtPaymentMethod.Text) ||
                string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show("Please fill all fields.", "Missing Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId;
            if (!int.TryParse(txtMemberId.Text.Trim(), out memberId))
            {
                MessageBox.Show("Please enter a valid Member ID (numbers only).",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Int64 amount;
            if (!Int64.TryParse(txtAmount.Text.Trim(), out amount))
            {
                MessageBox.Show("Please enter a valid Amount (numbers only).",
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string memberName = txtMemberName.Text.Trim();
            string paymentType = txtPaymentType.Text.Trim();
            string paymentDate = dateTimePickerPayment.Value.ToString("yyyy-MM-dd");
            string paymentMethod = txtPaymentMethod.Text.Trim();
            string status = txtStatus.Text.Trim();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-BFVGGDA\\SQLEXPRESS; database = gym2; integrated security = True";

            try
            {
                con.Open();
                string query = @"INSERT INTO Payment
                    (MemberId, MemberName, PaymentType, Amount, PaymentDate, PaymentMethod, Status)
                    VALUES
                    (@MemberId, @MemberName, @PaymentType, @Amount, @PaymentDate, @PaymentMethod, @Status)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemberId", memberId);
                cmd.Parameters.AddWithValue("@MemberName", memberName);
                cmd.Parameters.AddWithValue("@PaymentType", paymentType);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                cmd.Parameters.AddWithValue("@Status", status);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Payment saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnViewPayments_Click(object sender, EventArgs e)
        {
            ViewPayments vp = new ViewPayments();
            vp.Show();
        }

        private void ClearFields()
        {
            txtMemberId.Clear();
            txtMemberName.Clear();
            txtPaymentType.Clear();
            txtAmount.Clear();
            txtPaymentMethod.Clear();
            txtStatus.Clear();
            dateTimePickerPayment.Value = DateTime.Now;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStatus.Text))
                txtStatus.Text = "Paid";
        }

        private void btnViewPayments_Click_1(object sender, EventArgs e)
        {
            ViewPayments vp = new ViewPayments();
            vp.Show();
        }
    }
}