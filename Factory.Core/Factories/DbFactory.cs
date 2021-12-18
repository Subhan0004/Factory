using Factory.Core.DataAccess.SqlServer;
using Factory.Core.Domain.Abstract;
using Factory.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Factories
{
    public static class DbFactory
    {
        public static IUnitOfWork Create(ServerType serverType, string connectionString)
        {
            switch (serverType)
            {
                case ServerType.SqlServer:
                    {
                        return new SqlUnitOfWork(connectionString);
                    }
                default:
                    {
                        throw new NotSupportedException($"{serverType} not supported");
                    }
            }
        }
    }
}
