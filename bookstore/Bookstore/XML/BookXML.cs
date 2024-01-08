using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Xml;
using Bookstore.DTO;
using System.IO;

namespace Bookstore.XML
{
    public static class BookXML
    {
        public static void DatabaseToXMLBook(OracleConnection connection, XmlDocument xmlDocument, XmlElement rootElement)
        {
            string sqlQuery = "SELECT * FROM Ksiazka";
            using (OracleCommand command = new OracleCommand(sqlQuery, connection))
            {
                // Wykonanie zapytania i pobranie danych
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    // Tworzenie dokumentu XML
                    xmlDocument.AppendChild(rootElement);

                    // Iteracja przez wiersze danych
                    while (reader.Read())
                    {
                        // Tworzenie elementu XML dla każdego wiersza
                        XmlElement rowElement = xmlDocument.CreateElement("Ksiazka");

                        // Iteracja przez kolumny w wierszu
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // Pobieranie nazwy kolumny i wartości
                            string columnName = reader.GetName(i);
                            if (columnName == "ZDJECIE")
                            {

                                // Konwertuj OracleBinary na tablicę bajtów
                                byte[] blobData = (byte[])reader.GetValue(i);

                                // Konwertuj tablicę bajtów na wartość Base64
                                string base64Data = Convert.ToBase64String(blobData);
                                XmlElement columnElement = xmlDocument.CreateElement(columnName);
                                columnElement.InnerText = base64Data.ToString();

                                // Dodawanie elementu kolumny do wiersza
                                rowElement.AppendChild(columnElement);
                            }
                            else
                            {
                                object columnValue = reader.GetValue(i);

                                // Tworzenie elementu XML dla kolumny
                                XmlElement columnElement = xmlDocument.CreateElement(columnName);
                                columnElement.InnerText = columnValue.ToString();

                                // Dodawanie elementu kolumny do wiersza
                                rowElement.AppendChild(columnElement);
                            }
                        }

                        // Dodawanie wiersza do dokumentu XML
                        rootElement.AppendChild(rowElement);
                    }
                }
            }
        }

        public static List<BookShortDTO> LoadBookShortData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<BookShortDTO> bookShortList = new List<BookShortDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList bookNodeList = xmlDoc.SelectNodes("Ksiazki/Ksiazka");

            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < bookNodeList.Count; i++)
            {
                XmlNode Nodes = bookNodeList[i];

                bookShortList.Add(new BookShortDTO { Title = Nodes["TYTUL"].InnerText, Id = Nodes["ID_KSIAZKI"].InnerText });
            }

            return bookShortList;
        }

        public static List<BookDTO> LoadBookData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<BookDTO> bookList = new List<BookDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList booksNodeList = xmlDoc.SelectNodes("Ksiazki/Ksiazka");


            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < booksNodeList.Count; i++)
            {
                XmlNode Nodes = booksNodeList[i];
                var Picture = Nodes["ZDJECIE"];

                string base64Data = Picture.InnerText;
                byte[] binaryData = Convert.FromBase64String(base64Data);

                bookList.Add(new BookDTO
                {
                    Id = Nodes["ID_KSIAZKI"].InnerText,
                    Publisher = Nodes["WYDAWNICTWO"].InnerText,
                    Pub_date = Nodes["ROK_WYDANIA"].InnerText,
                    Price = Nodes["CENA"].InnerText,
                    Zdjecie = binaryData
                });
            }

            return bookList;
        }
    }
}
