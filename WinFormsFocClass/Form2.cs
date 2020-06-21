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

namespace WinFormsFocClass
{
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        public Form2()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("insert into Customers values (@username,@password,@email,@phone_number)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("username", username.Text);
            sqlCommand.Parameters.AddWithValue("password", password.Text);
            sqlCommand.Parameters.AddWithValue("email", email.Text);
            sqlCommand.Parameters.AddWithValue("phone_number", phonenumber.Text);

            var result = sqlCommand.ExecuteNonQuery();

            if (result>0)
            {
                MessageBox.Show("Customer created");
            }
            else
            {
                MessageBox.Show("Not created");
            }

            sqlConnection.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection("Server=MHEYDAROV;Database=Shopping;Trusted_Connection=True;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("select id from Customers where username = @username and password = @password", sqlConnection);
            sqlCommand.Parameters.AddWithValue("username", loginusername.Text);
            sqlCommand.Parameters.AddWithValue("password", loginpassword.Text);

            var userid = sqlCommand.ExecuteScalar();

            if (userid != null)
            {
                Form3 form3 = new Form3();
                form3.sendEnteredUser((int)userid);
                form3.ShowDialog();
            }
            else
            {
                MessageBox.Show("User does not exist","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            sqlConnection.Close();
        }
    }
}
