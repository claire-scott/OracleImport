using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace OracleImport.Models
{
    class LandingRepository
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public List<Landing_Table> GetLandingTables()
        {
            string sql = "select * from Control.Landing_Table where Active = 1;";

            return (List<Landing_Table>)GetConnection().Query<Landing_Table>(sql);
        }

        public Landing_Table_Log New_Log_Record(int LandingTableId)
        {
            var o = new Landing_Table_Log();
            o.Landing_Table_Id = LandingTableId;
            GetConnection().Insert(o);

            return o;
        }

        public void Update_Log(Landing_Table_Log TableLog)
        {
            GetConnection().Update(TableLog);
        }

        public bool TableExists(string Schema, string TableName)
        {
            int count = GetConnection().QuerySingle<int>("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = @schema and TABLE_NAME = @table;", new { schema = Schema, table = TableName });

            if (count == 1) return true;

            return false;
        }
    }
}
