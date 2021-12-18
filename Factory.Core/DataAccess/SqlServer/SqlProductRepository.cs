using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.DataAccess.SqlServer
{
    public class SqlProductRepository : SqlBaseRepository, IProductRepository
    {
        public SqlProductRepository(SqlContext contex) : base(contex)
        {

        }
        public int Add(Product product)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"insert into Products output inserted.Id
                        values( @CategoriesId, @Name, @Price, @Quantity, @Note, 
                        @CreatorId, @LastModifiedDate, @IsDeleted)";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, product);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public Product FindById(int id)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"select m.*,
                           c.Id as CategoriesId, c.Name as CategoryName
                           from Products as m
                           Inner Join Categories as c ON c.Id = m.CategoriesId 
                           where m.IsDeleted = 0 and m.Id = @Id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Product product = GetFromReader(reader);

                        return product;
                    }

                    return null;
                }
            }
        }

        public List<Product> Get()
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"select m.*,
                           c.Id as CategoriesId, c.Name as CategoryName
                           from Products as m
                           Inner Join Categories as c ON c.Id = m.CategoriesId where m.IsDeleted = 0";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    List<Product> products = new List<Product>();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = GetFromReader(reader);

                        products.Add(product);
                    }

                    return products;
                }
            }
        }

        public bool Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"update Products set CategoriesId = @CategoriesId, Name = @Name, Price = @Price, 
                                  Quantity = @Quantity, Note = @Note, CreatorId = @CreatorId, 
                                  LastModifiedDate = @LastModifiedDate,
                                  IsDeleted = @IsDeleted where Id = @Id";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);

                    AddParameters(command, product);

                    return command.ExecuteNonQuery() == 1;

                }
            }
        }
        private void AddParameters(SqlCommand command, Product product)
        {
            command.Parameters.AddWithValue("@CategoriesId", product.Category?.Id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@Note", product.Note ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreatorId", product.Creator.Id);
            command.Parameters.AddWithValue("@LastModifiedDate", product.LastModifiedDate);
            command.Parameters.AddWithValue("@IsDeleted", product.IsDeleted);
        }

        private Product GetFromReader(SqlDataReader reader)
        {
            Product product = new Product();

            product.Id = reader.GetInt32("Id");

            product.Category = new Category()
            {
                Id = reader.GetInt32("CategoriesId"),
                Name = reader.GetString("CategoryName")
            };

            product.Name = reader.GetString("Name");
            product.Price = reader.GetDecimal("Price");
            product.Quantity = reader.GetInt32("Quantity");

            if (!reader.IsDBNull("Note"))
            {
                product.Note = reader.GetString("Note");
            }

            return product;
        }
    }
}
