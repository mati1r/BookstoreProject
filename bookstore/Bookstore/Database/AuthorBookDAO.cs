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
    public static class AuthorBookDAO
    {
        public static void InsertXMLToDatabase(OracleConnection connection)
        {
            List<AuthorDTO> autor_data = new List<AuthorDTO>();
            autor_data = AuthorXML.LoadAuthorData();

            for (int i = 0; i < autor_data.Count; i++)
            {
                string query = "INSERT INTO AUTOR_KSIAZKA (ID_AUTORA, ID_KSIAZKI) " +
                            "VALUES (:value1, :value2)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("value1", OracleDbType.Int32).Value = Convert.ToInt32(autor_data[i].Author_id);
                    command.Parameters.Add("value2", OracleDbType.Int32).Value = Convert.ToInt32(autor_data[i].Book_id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
