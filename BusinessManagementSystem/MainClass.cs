using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BusinessManagementSystem
{
    public class MainClass
    {
        public static SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);
    }
}