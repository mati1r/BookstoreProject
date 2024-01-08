using Bookstore.DTO;
using Bookstore.XML;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bookstore.Database
{
    public static class BookDAO
    {
        private static void UpdateBook(List<BookDTO> bookData, List<string> tableBookId, OracleConnection connection, List<BookShortDTO> bookShortDataXML, int i)
        {
            string query = "UPDATE KSIAZKA SET ID_KSIAZKI = :value1, WYDAWNICTWO = :value2, TYTUL = :value3, ROK_WYDANIA = :value4, CENA = :value5, ZDJECIE = :value6" +
                            " WHERE ID_KSIAZKI = :id";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("value1", OracleDbType.Int32).Value = Convert.ToInt32(bookData[i].Id);
                command.Parameters.Add("value2", OracleDbType.Varchar2).Value = bookData[i].Publisher;
                command.Parameters.Add("value3", OracleDbType.Varchar2).Value = bookShortDataXML[i].Title;
                command.Parameters.Add("value4", OracleDbType.Int32).Value = Convert.ToInt32(bookData[i].Pub_date);
                command.Parameters.Add("value5", OracleDbType.Decimal).Value = Convert.ToDouble(bookData[i].Price);
                command.Parameters.Add("value6", OracleDbType.Blob).Value = bookData[i].Zdjecie;
                command.Parameters.Add("id", OracleDbType.Int32).Value = Convert.ToInt32(tableBookId[i]);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertBook(List<BookDTO> bookData, OracleConnection connection, List<BookShortDTO> bookShortDataXML, int i)
        {
            string query = "INSERT INTO KSIAZKA (ID_KSIAZKI, WYDAWNICTWO, TYTUL, ROK_WYDANIA, CENA, ZDJECIE) " +
                        "VALUES (:value1, :value2, :value3, :value4, :value5, :value6)";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("value1", OracleDbType.Int32).Value = Convert.ToInt32(bookData[i].Id);
                command.Parameters.Add("value2", OracleDbType.Varchar2).Value = bookData[i].Publisher;
                command.Parameters.Add("value3", OracleDbType.Varchar2).Value = bookShortDataXML[i].Title;
                command.Parameters.Add("value4", OracleDbType.Int32).Value = Convert.ToInt32(bookData[i].Pub_date);
                command.Parameters.Add("value5", OracleDbType.Decimal).Value = Convert.ToDouble(bookData[i].Price);
                command.Parameters.Add("value6", OracleDbType.Blob).Value = bookData[i].Zdjecie;

                command.ExecuteNonQuery();
            }
        }

        private static void DeleteBook(List<string> tableBookIdDatabase, List<string> tableBookId, OracleConnection connection)
        {
            for (int i = 0; i < tableBookIdDatabase.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < tableBookId.Count; j++)
                {
                    if (tableBookIdDatabase[i] == tableBookId[j])
                    {
                        //to znaczy ze sa obydwa w bazie
                        count++;
                    }
                }

                if (count < 1)
                {
                    //To znaczy że takiego id w bazie dancyh nie znaleziono w XML czyli Delete

                    //Usunięcie z tabeli podrzędnej
                    string query = "DELETE FROM KSIAZKA_HISTORIA WHERE ID_KSIAZKI = :id";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("id", OracleDbType.Int32).Value = tableBookIdDatabase[i];
                        command.ExecuteNonQuery();
                    }
                    //Usunięcie z bazy
                    string query2 = "DELETE FROM KSIAZKA WHERE ID_KSIAZKI = :id";

                    using (OracleCommand command = new OracleCommand(query2, connection))
                    {
                        command.Parameters.Add("id", OracleDbType.Int32).Value = tableBookIdDatabase[i];
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void InsertXMLToDatabase(OracleConnection connection)
        {
            List<string> tableBookId = new List<string>();
            List<string> tableBookIdDatabase = new List<string>();

            List<BookShortDTO> bookShortDataXML = BookXML.LoadBookShortData();

            for (int i = 0; i < bookShortDataXML.Count; i++)
            {
                tableBookId.Add(bookShortDataXML[i].Id);
            }

            //Pobranie dnaych ksiazek
            string sqlQuery = "SELECT * FROM Ksiazka";
            using (OracleCommand command = new OracleCommand(sqlQuery, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        tableBookIdDatabase.Add(id);
                    }
                }
            }

            List<BookDTO> bookDataXML = BookXML.LoadBookData();

            //Sprawdzenie czy jest w XML ale nie ma w bazie (aktualizacja lub dodanie)
            for (int i = 0; i < tableBookId.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < tableBookIdDatabase.Count; j++)
                {
                    if (tableBookId[i] == tableBookIdDatabase[j])
                    {
                        //to znaczy ze sa obydwa w bazie więc należy modyfikować
                        UpdateBook(bookDataXML, tableBookId, connection, bookShortDataXML, i);
                        count++;
                    }
                }

                if (count < 1)
                {
                    //To znaczy że takiego id w XML nie znaleziono w bazie danych czyli Insert
                    InsertBook(bookDataXML, connection, bookShortDataXML, i);
                }
            }

            //Sprawdzenie czy jest w bazie ale nie ma w XML (usuwanie)
            DeleteBook(tableBookIdDatabase, tableBookId, connection);          
        }
    }
}
