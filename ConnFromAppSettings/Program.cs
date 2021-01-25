using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;

/*
Right-click on the References / Dependencies
Choose Add Reference
Find and add Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.FileExtension
Microsoft.Extensions.Configuration.Json
on Mac:
Nuget manager:search for Microsoft.Extensions
*/
/* HUSK:
 * appsetting.json -> properties -> copy if newer
 */
namespace ConnFromAppSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectToDB();
        }

        static string Query = "select * from Person.PhoneNumberType";

        static void ConnectToDB()
        {
            string connectionString = GetConnectionString();
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
            
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory
                .GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            string connectionString = builder.Build().GetConnectionString("AdventureWorksUnTrusted");
            Console.WriteLine($"ConnectionString: {connectionString}");
            return connectionString;
        }
    }
}
