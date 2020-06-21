using System;
using System.Data.SqlClient;

namespace TestForClass
{
    class Program
    {
        static void Main()
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=MHEYDAROV;Database=Demo;Trusted_Connection=True;"))
            {
                sqlConnection.Open();


                SqlCommand sqlCommand = new SqlCommand("Insert into Users values ('Name3','Surname3','email3',56)", sqlConnection);

                var result = sqlCommand.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Inserted");
                }
                else
                {
                    Console.WriteLine("Not Inserted");
                }
            }
        }
    }
}
