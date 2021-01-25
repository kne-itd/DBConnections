using System;
using System.Configuration;
using System.Data.SqlClient;

/*
Right-click on the References / Dependencies
Choose Add Reference
Find and add System.Configuration.
on Mac:
Nuget manager:search for System.Configuration
*/
namespace ConnFromConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorksUnTrusted"].ConnectionString;
            string Query = "select * from Person.PhoneNumberType";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(Query, conn);
                conn.Open();

                SqlDataReader Results = cmd.ExecuteReader();
                while (Results.Read())
                {
                    Console.WriteLine(Results.GetInt32(0) + " " + Results.GetString(1) + " " + Results.GetDateTime(2));
                }
            }

            Console.WriteLine("Hello World!");
        }
    }
}
