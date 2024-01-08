using Bookstore.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Bookstore.XML
{
    public static class AuthorXML
    {
        public static void DatabaseToXMLAuthor(OracleConnection connection, XmlDocument xmlDocument, XmlElement rootElement)
        {
            string sqlQuery = "SELECT ak.id_autora, id_ksiazki, a.imie, a.nazwisko FROM autor_ksiazka ak JOIN autor a ON ak.id_autora = a.id_autora";
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
                        XmlElement rowElement = xmlDocument.CreateElement("Autor");

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
        public static List<AuthorDTO> LoadAuthorData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<AuthorDTO> authorList = new List<AuthorDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                //Wczytanie dokumentu
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList authorNodeList = xmlDoc.SelectNodes("Ksiazki/Autor");

            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < authorNodeList.Count; i++)
            {
                XmlNode Nodes = authorNodeList[i];

                authorList.Add(new AuthorDTO
                {
                    Author_id = Nodes["ID_AUTORA"].InnerText,
                    Book_id = Nodes["ID_KSIAZKI"].InnerText,
                    Name = Nodes["IMIE"].InnerText,
                    Surname = Nodes["NAZWISKO"].InnerText
                });
            }

            return authorList;
        }

        public static void AddAuthorToBook(XmlDocument xmlDoc, XmlNodeList authorList, int bookId, string author)
        {
            if (author != "Brak")
            {
                string author_id = "";

                //Znalezienie id autora
                for (int j = 0; j < authorList.Count; j++)
                {
                    XmlNode Nodes_author = authorList[j];

                    if ((Nodes_author["IMIE"].InnerText + " " + Nodes_author["NAZWISKO"].InnerText) == author)
                    {
                        author_id = Nodes_author["ID_AUTORA"].InnerText;
                    }
                }

                string[] words = author.Split(' ');
                //Znaczy że nie znaleziono pasujacego id książki w tablei autor co znaczy że dana książka nie miała wcześniej autora
                //Tutaj insert
                XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Autor", null);

                XmlElement author_id_element = xmlDoc.CreateElement("ID_AUTORA");
                author_id_element.InnerText = author_id;
                newRecord.AppendChild(author_id_element);

                XmlElement book_id_element = xmlDoc.CreateElement("ID_KSIAZKI");
                book_id_element.InnerText = bookId.ToString();
                newRecord.AppendChild(book_id_element);

                XmlElement author_name_element = xmlDoc.CreateElement("IMIE");
                author_name_element.InnerText = words[0];
                newRecord.AppendChild(author_name_element);

                XmlElement author_surname_element = xmlDoc.CreateElement("NAZWISKO");
                author_surname_element.InnerText = words[1];
                newRecord.AppendChild(author_surname_element);

                // Dodawanie nowego rekordu do istniejącego pliku XML
                XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                rootNode.AppendChild(newRecord);
            }
        }

        public static void EditAuthor(XmlDocument xmlDoc, XmlNodeList authorList, string bookId, string author)
        {
            if (author == "Brak")
            {
                //Jeżeli zmieniamy na brak to należy usunąć
                string query = "//Autor[ID_KSIAZKI = '" + bookId + "']";
                XML.DeleteSelectedNode(xmlDoc, query);
            }
            else
            {
                string authorId = "";
                string authorNewName = "";
                string authorNewSurname = "";

                //Znalezienie danych nowego autora
                for (int j = 0; j < authorList.Count; j++)
                {
                    XmlNode authorNodes = authorList[j];

                    if ((authorNodes["IMIE"].InnerText + " " + authorNodes["NAZWISKO"].InnerText) == author)
                    {
                        authorId = authorNodes["ID_AUTORA"].InnerText;
                        authorNewName = authorNodes["IMIE"].InnerText;
                        authorNewSurname = authorNodes["NAZWISKO"].InnerText;
                    }
                }

                int counter = 0;
                //Podmiana danych
                for (int j = 0; j < authorList.Count; j++)
                {
                    XmlNode authorNodes = authorList[j];

                    if (authorNodes["ID_KSIAZKI"].InnerText == bookId && counter < 1)
                    {
                        authorNodes["ID_AUTORA"].InnerText = authorId;
                        authorNodes["IMIE"].InnerText = authorNewName;
                        authorNodes["NAZWISKO"].InnerText = authorNewSurname;

                        counter++;
                    }
                }
                if (counter == 0)
                {
                    string[] words = author.Split(' ');
                    //Znaczy że nie znaleziono pasujacego id książki w tablei autor co znaczy że dana książka nie miała wcześniej autora
                    //Tutaj insert
                    XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Autor", null);

                    XmlElement authorIdElement = xmlDoc.CreateElement("ID_AUTORA");
                    authorIdElement.InnerText = authorId;
                    newRecord.AppendChild(authorIdElement);

                    XmlElement bookIdElement = xmlDoc.CreateElement("ID_KSIAZKI");
                    bookIdElement.InnerText = bookId;
                    newRecord.AppendChild(bookIdElement);

                    XmlElement authorNameElement = xmlDoc.CreateElement("IMIE");
                    authorNameElement.InnerText = words[0];
                    newRecord.AppendChild(authorNameElement);

                    XmlElement authorSurnameElement = xmlDoc.CreateElement("NAZWISKO");
                    authorSurnameElement.InnerText = words[1];
                    newRecord.AppendChild(authorSurnameElement);

                    // Dodawanie nowego rekordu do istniejącego pliku XML
                    XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                    rootNode.AppendChild(newRecord);
                }
            }
        }
    }
}
