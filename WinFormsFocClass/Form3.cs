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
    public partial class Form3 : Form
    {
        int userid;
        int productid;
        int quantity;
        SqlConnection sqlConnection;
        public Form3()
        {
            InitializeComponent();
        }

        public void sendEnteredUser(int id)
        {
            userid = id;
        }

        public void ReadProducts()
        {
            sqlConnection.Open();

            using (SqlDataAdapter sqlData = new SqlDataAdapter("select * from Products", sqlConnection))
            {
                DataTable dataTable = new DataTable();

                sqlData.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                if (comboBox1.Items.Count == 0)
                {
                    foreach (DataRow data in dataTable.Rows)
                    {
                        comboBox1.Items.Add(data.Field<string>("name"));
                    }
                }
            }

            sqlConnection.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection("Server=MHEYDAROV;Database=Shopping;Trusted_Connection=True;");

            ReadProducts();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("select id, quantity from Products where name = @productname", sqlConnection);
            sqlCommand.Parameters.AddWithValue("productname", comboBox1.GetItemText(comboBox1.SelectedItem));

            var reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    productid = reader.GetInt32(0);
                    quantity = reader.GetInt32(1);

                    numericUpDown1.Maximum = quantity;
                }
            }

            sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();

            var orderCount = 0;
            if (numericUpDown1.Value > quantity)
            {
                orderCount = quantity;
                quantity = 0;
            }
            else
            {
                orderCount = Convert.ToInt32(numericUpDown1.Value);
                quantity = quantity - Convert.ToInt32(numericUpDown1.Value);
            }

            SqlCommand sqlCommand = new SqlCommand("insert into Orders values (@customerid, @productid, @orderdate, @quantity)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("customerid", userid);
            sqlCommand.Parameters.AddWithValue("productid", productid);
            sqlCommand.Parameters.AddWithValue("quantity", orderCount);
            sqlCommand.Parameters.AddWithValue("orderdate", DateTime.Now);

            var result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            if (result > 0)
            {
                MessageBox.Show("Order created");

                sqlConnection.Open();

                sqlCommand = new SqlCommand("update products set quantity = @quantity where id = @productid", sqlConnection);
                sqlCommand.Parameters.AddWithValue("quantity", quantity);
                sqlCommand.Parameters.AddWithValue("productid", productid);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                ReadProducts();
            }
            else
            {
                MessageBox.Show("Not created");
            }
        }
    }
}
