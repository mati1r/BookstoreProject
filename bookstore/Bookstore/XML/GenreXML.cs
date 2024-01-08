using Bookstore.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Bookstore.XML
{
    public static class GenreXML
    {
        public static void DatabaseToXMLGenre(OracleConnection connection, XmlDocument xmlDocument, XmlElement rootElement)
        {
            string sqlQuery = "SELECT gk.id_gatunku, id_ksiazki, g.nazwa FROM gatunek_ksiazka gk JOIN gatunek g ON gk.id_gatunku = g.id_gatunku ";
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
                        XmlElement rowElement = xmlDocument.CreateElement("Gatunek");

                        // Iteracja przez kolumny w wierszu
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // Pobieranie nazwy kolumny i wartości
                            string columnName = reader.GetName(i);
                            object columnValue = reader.GetValue(i);

                            // Tworzenie elementu XML dla kolumny
                            XmlElement columnElement = xmlDocument.CreateElement(columnName);
                            columnElement.InnerText = columnValue.ToString();

                            // Dodawanie elementu kolumny do wiersza
                            rowElement.AppendChild(columnElement);

                        }

                        // Dodawanie wiersza do dokumentu XML
                        rootElement.AppendChild(rowElement);
                    }
                }
            }
        }

        public static List<GenreDTO> LoadGenreData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<GenreDTO> genreList = new List<GenreDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList genreNodeList = xmlDoc.SelectNodes("Ksiazki/Gatunek");

            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < genreNodeList.Count; i++)
            {
                XmlNode Nodes = genreNodeList[i];

                genreList.Add(new GenreDTO
                {
                    Genre_id = Nodes["ID_GATUNKU"].InnerText,
                    Book_id = Nodes["ID_KSIAZKI"].InnerText,
                    Genre_name = Nodes["NAZWA"].InnerText
                });
            }

            return genreList;
        }

        public static void AddGenreToBook(XmlDocument xmlDoc, XmlNodeList genreList, int bookId, string genre)
        {
            if (genre != "Brak")
            {
                string genreId = "";

                //Znalezienie id gatunku gatunku
                for (int j = 0; j < genreList.Count; j++)
                {
                    XmlNode Nodes_genre = genreList[j];

                    if (Nodes_genre["NAZWA"].InnerText == genre)
                    {
                        genreId = Nodes_genre["ID_GATUNKU"].InnerText;
                    }
                }
                XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Gatunek", null);

                XmlElement author_id_element = xmlDoc.CreateElement("ID_GATUNKU");
                author_id_element.InnerText = genreId;
                newRecord.AppendChild(author_id_element);

                XmlElement book_id_element = xmlDoc.CreateElement("ID_KSIAZKI");
                book_id_element.InnerText = bookId.ToString();
                newRecord.AppendChild(book_id_element);

                XmlElement author_name_element = xmlDoc.CreateElement("NAZWA");
                author_name_element.InnerText = genre;
                newRecord.AppendChild(author_name_element);

                // Dodawanie nowego rekordu do istniejącego pliku XML
                XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                rootNode.AppendChild(newRecord);
            }
        }

        public static void EditGenre(XmlDocument xmlDoc, XmlNodeList genreList, string bookId, string genre)
        {
            if (genre == "Brak")
            {
                //Jeżeli zmieniamy na brak to należy usunąć
                string query = "//Gatunek[ID_KSIAZKI = '" + bookId + "']";
                XML.DeleteSelectedNode(xmlDoc, query);
            }
            else
            {
                string genreId = "";

                //Znalezienie danych nowego gatunku
                for (int j = 0; j < genreList.Count; j++)
                {
                    XmlNode genreNodes = genreList[j];

                    if (genreNodes["NAZWA"].InnerText == genre)
                    {
                        genreId = genreNodes["ID_GATUNKU"].InnerText;
                    }
                }

                int counter = 0;

                //Podmiana danych
                for (int j = 0; j < genreList.Count; j++)
                {
                    XmlNode genreNodes = genreList[j];

                    if (genreNodes["ID_KSIAZKI"].InnerText == bookId && counter < 1)
                    {
                        genreNodes["ID_GATUNKU"].InnerText = genreId;
                        genreNodes["NAZWA"].InnerText = genre;

                        counter++;
                    }
                }
                if (counter == 0)
                {
                    //Tutaj insert
                    XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Gatunek", null);

                    XmlElement authorIdElement = xmlDoc.CreateElement("ID_GATUNKU");
                    authorIdElement.InnerText = genreId;
                    newRecord.AppendChild(authorIdElement);

                    XmlElement bookIdElement = xmlDoc.CreateElement("ID_KSIAZKI");
                    bookIdElement.InnerText = bookId;
                    newRecord.AppendChild(bookIdElement);

                    XmlElement authorNameElement = xmlDoc.CreateElement("NAZWA");
                    authorNameElement.InnerText = genre;
                    newRecord.AppendChild(authorNameElement);

                    // Dodawanie nowego rekordu do istniejącego pliku XML
                    XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                    rootNode.AppendChild(newRecord);
                }
            }
        }
    }
}
