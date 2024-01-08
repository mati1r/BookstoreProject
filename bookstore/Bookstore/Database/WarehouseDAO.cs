using Bookstore.DTO;
using Bookstore.XML;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Database
{
    public static class WarehouseDAO
    {
        public static void InsertXMLToDatabase(OracleConnection connection)
        {
            List<WarehouseDTO> magazyn_data = new List<WarehouseDTO>();
            magazyn_data = WarehouseXML.LoadWarehouseData();

            for (int i = 0; i < magazyn_data.Count; i++)
            {
                string query = "INSERT INTO MAGAZYN (ID_WPISU, ID_KSIAZKI, ILOSC_SZTUK) " +
                            "VALUES (:value1, :value2, :value3)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("value1", OracleDbType.Int32).Value = i;
                    command.Parameters.Add("value2", OracleDbType.Int32).Value = Convert.ToInt32(magazyn_data[i].Book_id);
                    command.Parameters.Add("value3", OracleDbType.Int32).Value = Convert.ToInt32(magazyn_data[i].Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
