using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.DataAccess.SqlServer
{
    public class SqlContext
    {
         public SqlContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }
}
}
