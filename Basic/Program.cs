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

            string ConnectionString = "Server=localhost;" +
                "Database=AdventureWorks2017;" +
                "Uid=Mac;" +
                "Pwd = 1234; ";


            string Query = "select * from Person.PhoneNumberType";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, conn);

                conn.Open();

                SqlDataReader Results = cmd.ExecuteReader();

                while (Results.Read())
                {
                    Console.WriteLine(Results.GetInt32(0) + " " + Results.GetString(1) + " " + Results.GetDateTime(2));
                }
            }

            
            Console.ReadKey();
        }

    }
}
