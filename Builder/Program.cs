using System;

using System.Data.SqlClient;
/*
 * https://www.connectionstrings.com/microsoft-data-sqlclient/
 */
namespace SQLserverConnection
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "AdventureWorks2017",
                UserID = "Mac",
                Password = "1234",
                DataSource = "172.16.226.128"
            };

            string Query = "select * from Person.PhoneNumberType";

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, conn);

                conn.Open();

                SqlDataReader Results = cmd.ExecuteReader();

                while (Results.Read())
                {
                    Console.WriteLine(Results.GetInt32(0) + " " + Results.GetString(1) + " " + Results.GetDateTime(2));
                }
            }
            Console.WriteLine("Hello from Builder");
            
        }

    }
}
