using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataCore;

namespace DapperLayer
{
    
    public class RepoDapperLayer
    {

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        public IDisposable Dispose
        {
            set
            {
                this.Connection.Dispose();
            }
        }

        public async Task<IEnumerable<Medicament>> getMedicament()
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return await cn.QueryAsync<Medicament>("SELECT * FROM dbo.Medicament");               
            }
        }
    }
}
