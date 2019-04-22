using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Toast.Models
{
    public class DBStoredProcedure
    {
        private readonly string _connectionString;

        public DBStoredProcedure()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString;
        }


    }
}