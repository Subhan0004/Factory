using Factory.Core.Domain.Abstract;
using Factory.Core.Domain.Entities;
using Factory.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.DataAccess.SqlServer
{
    public class SqlCategoryRepository : SqlBaseRepository, ICategoryRepository
    {
        public SqlCategoryRepository(SqlContext context) : base(context)
        {

        }

        public int Add(Category category)
        {
            using (SqlConnection conn = new SqlConnection(context.ConnectionString))
            {
                conn.Open();

                string cmdText = @"Insert into Categories output inserted.id
                                    values(@Name, @Note, @CreatorId,
                                    @LastModifiedDate, @IsDeleted)";

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    AddParameters(cmd, category);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<Category> Get()
        {
            using (SqlConnection conn = new SqlConnection(context.ConnectionString))
            {
                conn.Open();

                string cmdText = @"select * from Categories where IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Category> categories = new List<Category>();

                    while (reader.Read())
                    {
                        Category category = GetFromReader(reader);

                        categories.Add(category);
                    }

                    return categories;
                }
            }
        }

        public Category Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = "Select * from Categories where IsDeleted = 0 and Id = @Id";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Category category = GetFromReader(reader);

                        return category;
                    }

                    return null;
                }
            }
        }

        public bool Update(Category category)
        {
            using (SqlConnection conn = new SqlConnection(context.ConnectionString))
            {
                conn.Open();

                string cmdText = @"Update Categories set Name = @Name, 
                                 Note = @Note, 
                                 LastModifiedDate = @LastModifiedDate,
                                 CreatorId = @CreatorId, IsDeleted = @IsDeleted
                                 where Id = @Id";

                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    AddParameters(cmd, category);
                    cmd.Parameters.AddWithValue("@Id", category.Id);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        private Category GetFromReader(SqlDataReader reader)
        {
            Category category = new Category();

            category.Id = reader.GetInt32("Id");
            category.Name = reader.GetString("Name");
            category.Note = reader.GetString("Note");
            category.LastModifiedDate = reader.GetDateTime("LastModifiedDate");

            if (!reader.IsDBNull("CreatorId"))
            {
                category.Creator = new User()
                {
                    Id = reader.GetInt32("CreatorId")
                };
            }

            category.IsDeleted = reader.GetBoolean("IsDeleted");

            return category;
        }

        private void AddParameters(SqlCommand cmd, Category category)
        {
            cmd.Parameters.AddWithValue("@Name", category.Name);
            cmd.Parameters.AddWithValue("@Note", category.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatorId", category.Creator.Id);
            cmd.Parameters.AddWithValue("@LastModifiedDate", category.LastModifiedDate);
            cmd.Parameters.AddWithValue("@IsDeleted", category.IsDeleted);
        }
    }
}
