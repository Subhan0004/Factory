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
    public class SqlCustomerRepository : SqlBaseRepository, ICustomerRepository
    {
        public SqlCustomerRepository(SqlContext context) : base(context)
        {

        }
        public int Add(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"Insert into Customers output inserted.id 
                                   values(@Name, @Surname, @Address, @Phone, 
                                   @Note, @CreatorId, @LastModifiedDate, @IsDeleted)";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    AddParameters(cmd, customer);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<Customer> Get()
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"Select * from Customers where IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Customer> customers = new List<Customer>();

                    while (reader.Read())
                    {
                        Customer customer = GetFromReader(reader);
                        customers.Add(customer);
                    }

                    return customers;
                }
            }
        }

        public Customer Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = "Select * from Customers where IsDeleted = 0 and Id = @Id";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Customer customer = GetFromReader(reader);

                        return customer;
                    }

                    return null;
                }
            }
        }

        public bool Update(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"Update Customers set Name=@Name, Surname=@Surname, Address=@Address,
                                  Phone=@Phone, Note=@Note, CreatorId=@CreatorId, 
                                  LastModifiedDate=@LastModifiedDate, IsDeleted=@IsDeleted where Id=@Id";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    AddParameters(cmd, customer);
                    cmd.Parameters.AddWithValue("@Id", customer.Id);

                    return cmd.ExecuteNonQuery() == 1;

                }
            }
        }

        private Customer GetFromReader(SqlDataReader reader)
        {
            Customer customer = new Customer();

            customer.Id = reader.GetInt32("Id");
            customer.Name = reader.GetString("Name");
            customer.Surname = reader.GetString("Surname");
            customer.Address = reader.GetString("Address");
            customer.Phone = reader.GetString("Phone");
            customer.Note = reader.GetString("Note");

            if (!reader.IsDBNull("CreatorId"))
            {
                customer.Creator = new User()
                {
                    Id = reader.GetInt32("CreatorId")
                };
            }

            customer.LastModifiedDate = reader.GetDateTime("LastModifiedDate");
            customer.IsDeleted = reader.GetBoolean("IsDeleted");

            return customer;
        }
        private void AddParameters(SqlCommand cmd, Customer customer)
        {
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Surname", customer.Surname);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
            cmd.Parameters.AddWithValue("@Note", customer.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatorId", customer.Creator.Id);
            cmd.Parameters.AddWithValue("@LastModifiedDate", customer.LastModifiedDate);
            cmd.Parameters.AddWithValue("@IsDeleted", customer.IsDeleted);
        }
    }
}
