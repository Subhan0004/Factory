using Factory.Core.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.DataAccess.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly SqlContext context;
        public SqlUnitOfWork(string connectionString)
        {
            context = new SqlContext(connectionString);
        }
        public ICustomerRepository CustomerRepository => new SqlCustomerRepository(context);

        public ICategoryRepository CategoryRepository => new SqlCategoryRepository(context);

        public IUserRepository UserRepository => new SqlUserRepository(context);

        public IProductRepository ProductRepository => new SqlProductRepository(context);


        public bool CheckServer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(context.ConnectionString))
                {
                    conn.Open();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
