using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Common
{
    public static class Project
    {
        //string odbc = "Software\\ODBC\\ODBC.INI\\SWMSoftDB";
        private const string ODBC_INI_REG_PATH = @"SOFTWARE\\ODBC\\ODBC.INI\\SWMSoftDB";
        private const string ODBC_SWMSOFT_PATH = @"SOFTWARE\\SWMSOFT\\OPTIONS";

        static string connectionString = String.Format(@"metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string='data source=CIV-047;initial catalog=SWMSoft;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'");
        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        static string baseDirectory = "C:\\Users\\admin\\Documents\\SWMSoft-NetStandard Demo\\SWMSoftSampleOne\\Drawings";
        public static string BaseDirectory
        {
            get
            {
                return baseDirectory;
            }
            set
            {
                baseDirectory = value;
            }
        }

        static string baseAddress = "http://localhost:55500/";
        public static string BaseAddress
        {
            get
            {
                return baseAddress;
            }
            set
            {
                baseAddress = value;
            }
        }

        /*
        public Project()
        {
            GetRegKeys();
        }

        public Project(string cs, string bfp)
        {
            this.ConnectionString = cs;
            this.BaseDirectory = bfp;
        }
        */

        public static void GetRegKeys()
        {
            try
            {
                var dsnKey = Registry.CurrentUser.OpenSubKey(ODBC_INI_REG_PATH, false);

                var database = dsnKey.GetValue("Database");
                var server = dsnKey.GetValue("Server");

                var dirKey = Registry.CurrentUser.OpenSubKey(ODBC_SWMSOFT_PATH, false);
                var dir = dirKey.GetValue("RecentPath0");
                if (database != null && server != null && dir != null)
                {
                    ConnectionString = String.Format(@"metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'", server.ToString(), database.ToString());
                    var lastIndex = dir.ToString().LastIndexOf("\\Drawings");
                    BaseDirectory = dir.ToString().Substring(0, lastIndex);
                }
            }
            catch (Exception e)
            {
                //System.Diagnostics.Debug.WriteLine("Error while retrieving DSN " + e.ToString());
                FileHandler.WriteAsync("Error while retrieving DSN " + e.ToString());
            }
        }
    }
}
