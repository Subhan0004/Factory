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
    public class SqlUserRepository : SqlBaseRepository, IUserRepository
    {
        public SqlUserRepository(SqlContext context) : base(context)
        {

        }

        public User Get(string username)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"select u.*, ur.Id as UserRoleId, ur.UserId, ur.RoleId, r.Name as RoleName from UserRoles as ur
				   Inner Join Users as u  ON u.Id = ur.UserId 
                    Inner Join Roles as r ON ur.RoleId = r.Id
                    where u.Username = @username and u.IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    var reader = cmd.ExecuteReader();

                    User user = null;

                    while (reader.Read())
                    {
                        ReadFromReader(reader, ref user);
                    }

                    return user;
                }
            }
        }

        public User Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(context.ConnectionString))
            {
                connection.Open();

                string cmdText = @"select u.*, ur.Id as UserRoleId, ur.UserId, ur.RoleId, r.Name as RoleName from Users as u Inner Join UserRoles as ur  ON u.Id = ur.UserId 
                    Inner Join Roles as r ON ur.RoleId = r.Id
                    where u.Id = @id and u.IsDeleted = 0";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    User user = null;

                    while (reader.Read())
                    {
                        ReadFromReader(reader, ref user);
                    }

                    return user;
                }
            }
        }

        private void ReadFromReader(SqlDataReader reader, ref User user)
        {
            if (user == null)
            {
                user = new User();

                user.Id = reader.GetInt32("Id");
                user.Username = reader.GetString("Username");
                user.Password = reader.GetString("Password");
                user.CanOperatorCrm = reader.GetBoolean("CanOperatorCrm");
                user.LastModifiedDate = reader.GetDateTime("LastModifiedDate");

                if (!reader.IsDBNull("CreatorId"))
                {
                    user.Creator = new User()
                    {
                        Id = Convert.ToInt32(reader["CreatorId"])
                    };
                }

                user.IsDeleted = reader.GetBoolean("IsDeleted");
            }

            UserRole userRole = new UserRole();

            userRole.Id = reader.GetInt32("UserRoleId");
            userRole.User = user;
            userRole.Role = new Role();
            userRole.Role.Id = reader.GetInt32("RoleId");
            userRole.Role.Name = reader.GetString("RoleName");

            user.UserRoles.Add(userRole);
        }
    }
}
