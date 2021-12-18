using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.DataAccess.SqlServer
{
    public abstract class SqlBaseRepository
    {
        protected readonly SqlContext context;

        public SqlBaseRepository(SqlContext context)
        {
            this.context = context;
        }
    }
}
