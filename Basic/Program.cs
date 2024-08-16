using System;

using System.Data.SqlClient;
using System.Net;
/*
 * https://www.connectionstrings.com/microsoft-data-sqlclient/
 */
namespace SQLserverConnection
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string HomeIP = "172.16.226.2";
            string WorkIp = "10.130.64.202";
            string DbServerAddress = string.Empty;
            string hostName = Dns.GetHostName(); ;
            string CurrentIp = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            //string CurrentIp = Dns.GetHostByName(hostName).AddressList[1].ToString();
            if (CurrentIp.StartsWith("172."))
            {
                DbServerAddress = HomeIP;
            }
            else if (CurrentIp.StartsWith("10."))
            {
                 DbServerAddress=WorkIp;
            }
            string ConnectionString = $"Server={DbServerAddress};" +
                "Database=AdventureWorks2017;" +
                "Uid=Mac;" +
                "Pwd = 1234; ";
            Console.WriteLine(ConnectionString);
            //string ConnectionString = "Server=Kajs-MacBook-Pro.local;" +
            //    "Database=AdventureWorks2017;" +
            //    "Uid=Mac;" +
            //    "Pwd = 1234;";


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
