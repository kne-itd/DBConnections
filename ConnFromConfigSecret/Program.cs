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
/* DOES NOT WORK */
/* config.Save(ConfigurationSaveMode.Full) - crashes */
namespace ConnFromConfig
{
    class Program
    {
        static void GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    Console.WriteLine(cs.Name);
                    Console.WriteLine(cs.ProviderName);
                    Console.WriteLine(cs.ConnectionString);
                }
            }
        }
        static void ProtectConnectionStrings()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                Console.WriteLine(config.FilePath);
                ConnectionStringsSection section = (ConnectionStringsSection)config.GetSection("connectionStrings");
                Console.WriteLine(section.ConnectionStrings.Count);

                //    ConnectionStringsSection section = config.ConnectionStrings;
                section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                //config.ConnectionStrings.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                config.Save(ConfigurationSaveMode.Full);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("#########");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("#########");
                Console.WriteLine("##INNER##");
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);

                Console.WriteLine("#########");
                Console.WriteLine(ex.InnerException.Data);
            }
        }
        static void Main(string[] args)
        {
            GetConnectionStrings();
            ProtectConnectionStrings();

            string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
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

            Console.WriteLine("Hello from SECRET");

        }
    }
}
