using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Repositories.Implementacion.AdoRepository.AdpHelpres
{
    public class GetDataReaderValues
    {

        public static string? GetNullableString(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? null : Convert.ToString(reader[colName]);
        }
        public static int GetNullableInt32(SqlDataReader reader, string colName) 
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? 0 : Convert.ToInt32(reader[colName]);
        }

        public static bool GetBoolean(SqlDataReader reader, string colName)
        {
            return reader.IsDBNull(reader.GetOrdinal(colName)) ? default(bool) : Convert.ToBoolean(reader[colName]);
        }

        public static DateTime GetDatetime(SqlDataReader reader, string colName)
        {
            return Convert.ToDateTime(reader.IsDBNull(reader.GetOrdinal(colName)) ? (DateTime?)null : Convert.ToDateTime(reader[colName]))!;
        }

        public static bool IsColumnExists(System.Data.IDataRecord dr,string colName )
        {
            try
            {
                return (dr.GetOrdinal(colName) >= 0);
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
