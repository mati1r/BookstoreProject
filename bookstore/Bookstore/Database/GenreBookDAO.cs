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
    public static class GenreBookDAO
    {
        public static void InsertXMLToDatabase(OracleConnection connection)
        {
            List<GenreDTO> genre_data = new List<GenreDTO>();
            genre_data = GenreXML.LoadGenreData();

            for (int i = 0; i < genre_data.Count; i++)
            {
                string query = "INSERT INTO GATUNEK_KSIAZKA (ID_GATUNKU, ID_KSIAZKI) " +
                            "VALUES (:value1, :value2)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("value1", OracleDbType.Int32).Value = Convert.ToInt32(genre_data[i].Genre_id);
                    command.Parameters.Add("value2", OracleDbType.Int32).Value = Convert.ToInt32(genre_data[i].Book_id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
