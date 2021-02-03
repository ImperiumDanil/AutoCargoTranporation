using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseAche2
{
    class DB
    {
        SqlConnection connection = new SqlConnection(@"Data Source = " + GlobalVar.Namesrv + "; Initial Catalog = AutoCargoTransportation; Integrated Security = Yes");
        public Boolean CheckConnection()
        {
            if (connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
        public void OpenConnection()
        {
            if(CheckConnection() == false)
                connection.Open();
        }
        public void CloseConnection()
        {
            if (CheckConnection())
                connection.Close();
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
