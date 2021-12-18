using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static bool IsDBNull(this SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName));
        }

        public static int GetInt32(this SqlDataReader reader, string columnName)
        {
            return Convert.ToInt32(reader[columnName]);
        }

        public static int GetDecimal(this SqlDataReader reader, string columnName)
        {
            return Convert.ToInt32(reader[columnName]);
        }

        public static string GetString(this SqlDataReader reader, string columnName)
        {
            return Convert.ToString(reader[columnName]);
        }

        public static bool GetBoolean(this SqlDataReader reader, string columnName)
        {
            return Convert.ToBoolean(reader[columnName]);
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string columnName)
        {
            return Convert.ToDateTime(reader[columnName]);
        }
    }
}
