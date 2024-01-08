using Oracle.ManagedDataAccess.Client;
using System.Windows;

namespace Bookstore.Database
{
    public static class DB
    {
        public static string connectionString = "Data Source={IP}:{PORT}/{SERVICE}; User ID={USER ID}; Password={PASSWORD};";

        public static void UpdateDB()
        {

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string[] tableName = new string[] { "AUTOR_KSIAZKA", "GATUNEK_KSIAZKA", "MAGAZYN" };

                    //Usuwanie danych z bazy dla tabel magazyn i posrednich
                    foreach (string x in tableName)
                    {

                        //SQL do usunięcia wszystkich wierszy z wybranej tabeli
                        string deleteQuery = $"DELETE FROM {x}";

                        using (OracleCommand command = new OracleCommand(deleteQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    BookDAO.InsertXMLToDatabase(connection);
                    WarehouseDAO.InsertXMLToDatabase(connection);
                    AuthorBookDAO.InsertXMLToDatabase(connection);
                    GenreBookDAO.InsertXMLToDatabase(connection);


                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("BRAK POŁĄCZENIA Z BAZĄ DANCYH");
                }

            }
        }
    }
}
