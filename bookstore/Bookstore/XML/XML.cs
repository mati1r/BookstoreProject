using Oracle.ManagedDataAccess.Client;
using System;
using System.Xml;
using System.IO;
using System.Windows;
using Bookstore.Database;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Bookstore.XML
{
    public static class XML
    {
        public static void WriteToXML()
        {
            // Łączenie z bazą danych Oracle
            using (OracleConnection connection = new OracleConnection(DB.connectionString))
            {
                try
                {
                    connection.Open();
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlElement rootElement = xmlDocument.CreateElement("Ksiazki");

                    BookXML.DatabaseToXMLBook(connection, xmlDocument, rootElement);
                    GenreXML.DatabaseToXMLGenre(connection, xmlDocument, rootElement);
                    AuthorXML.DatabaseToXMLAuthor(connection, xmlDocument, rootElement);
                    EmployeeXML.DatabaseToXMLEmployee(connection, xmlDocument, rootElement);
                    WarehouseXML.DatabaseToXMLWarehouse(connection, xmlDocument, rootElement);

                    string xmlFilePath = "output.xml";
                    xmlDocument.Save(xmlFilePath);

                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("BRAK POŁĄCZENIA Z BAZĄ DANCYH");
                }
            }
        }

        public static void DeleteSelectedNode(XmlDocument xmlDoc, string query)
        {
            var recordNode = xmlDoc.SelectNodes(query);

            foreach (XmlNode a in recordNode)
            {
                if (a != null)
                {
                    // Usunięcie rekordu
                    a.ParentNode.RemoveChild(a);
                }
            }
        }

        public static void DeleteFromXML(string bookId)
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                //Wczytanie dokumentu
                xmlDoc.Load("output.xml");
            }
            else
            {
                MessageBox.Show("Wystąpił błąd odczytu pliku XML");
                return;
            }


            string query = "//Autor[ID_KSIAZKI = '" + bookId + "']";
            DeleteSelectedNode(xmlDoc, query);

            query = "//Gatunek[ID_KSIAZKI = '" + bookId + "']";
            DeleteSelectedNode(xmlDoc, query);

            query = "//Ksiazka[ID_KSIAZKI = '" + bookId + "']";
            DeleteSelectedNode(xmlDoc, query);

            query = "//Magazyn[ID_KSIAZKI = '" + bookId + "']";
            DeleteSelectedNode(xmlDoc, query);

            xmlDoc.Save("output.xml");

        }

        public static void AddXML(ref TextBox title, ref TextBox price, ref TextBox releaseDate, ref TextBox quantity, ref TextBox publisher, 
                                    ref ComboBox genre, ref ComboBox author, ref System.Windows.Controls.Image image, OpenFileDialog openFileDialog, ref bool added)
        {

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                xmlDoc.Load("output.xml");
            }
            else
            {
                MessageBox.Show("Wystąpił błąd odczytu pliku XML");
                return;
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList bookList = xmlDoc.SelectNodes("Ksiazki/Ksiazka");
            XmlNodeList authorList = xmlDoc.SelectNodes("Ksiazki/Autor");
            XmlNodeList genreList = xmlDoc.SelectNodes("Ksiazki/Gatunek");

            int max = 0;

            foreach (XmlNode book in bookList)
            {
                var x = book["ID_KSIAZKI"].InnerText;

                if (Convert.ToInt32(x) > max)
                {
                    max = Convert.ToInt32(x);
                }
            }

            //Zwiększenie o 1 w celu wyrównania numeracji od 0 w systemie i od 1 w bazie danych
            max += 1;

            // Przetwarzanie węzłów i dodawanie danych do listy
            XmlNode Nodes = bookList[bookList.Count + 1];

            string pricePattern = @"^\d{1,6},\d{2}$";

            bool isMatch = Regex.IsMatch(price.Text, pricePattern);

            string datePattern = @"^\d{1,4}$";

            bool isMatch2 = Regex.IsMatch(releaseDate.Text, datePattern);

            string quantityPattern = @"^\d{1,8}$";

            bool isMatch3 = Regex.IsMatch(quantity.Text, quantityPattern);

            if (title.Text != "" && price.Text != "" && publisher.Text != "" && releaseDate.Text != "" && isMatch && isMatch2 && isMatch3)
            {
                XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Ksiazka", null);

                XmlElement bookIdElement = xmlDoc.CreateElement("ID_KSIAZKI");
                bookIdElement.InnerText = max.ToString();
                newRecord.AppendChild(bookIdElement);

                XmlElement publisherElement = xmlDoc.CreateElement("WYDAWNICTWO");
                publisherElement.InnerText = publisher.Text;
                newRecord.AppendChild(publisherElement);

                XmlElement titleElement = xmlDoc.CreateElement("TYTUL");
                titleElement.InnerText = title.Text;
                newRecord.AppendChild(titleElement);

                XmlElement publishDateElement = xmlDoc.CreateElement("ROK_WYDANIA");
                publishDateElement.InnerText = releaseDate.Text;
                newRecord.AppendChild(publishDateElement);

                XmlElement priceElement = xmlDoc.CreateElement("CENA");
                priceElement.InnerText = price.Text;
                newRecord.AppendChild(priceElement);

                byte[] x = File.ReadAllBytes(openFileDialog.FileName);
                string base64Data = Convert.ToBase64String(x);

                XmlElement imageElement = xmlDoc.CreateElement("ZDJECIE");
                imageElement.InnerText = base64Data;
                newRecord.AppendChild(imageElement);

                // Dodawanie nowego rekordu do istniejącego pliku XML
                XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                rootNode.AppendChild(newRecord);

                AuthorXML.AddAuthorToBook(xmlDoc, authorList, max, author.Text);
                GenreXML.AddGenreToBook(xmlDoc, genreList, max, genre.Text);
                WarehouseXML.AddWarehouseToBook(xmlDoc, max, quantity.Text);

                xmlDoc.Save("output.xml");
                MessageBox.Show("Dodano wpis");

                title.Text = "";
                publisher.Text = "";
                quantity.Text = "";
                price.Text = "";
                releaseDate.Text = "";
                genre.Text = "";
                author.Text = "";
                image.Source = null;
                added = false;
            }
            else
            {
                MessageBox.Show("Wystąpił błąd formatu (rok, cena lub ilosc)");
            }
        }

        public static void EditXML(string bookId, bool change, OpenFileDialog openFileDialog, string price, string title, string releaseDate, 
                                    string quantity, string publisher, string author, string genre)
        {
            // Tworzenie obiektu XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                //Wczytanie dokumentu
                xmlDoc.Load("output.xml");
            }
            else
            {
                MessageBox.Show("Wystąpił błąd odczytu pliku XML");
                return;
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList bookList = xmlDoc.SelectNodes("Ksiazki/Ksiazka");
            XmlNodeList authorList = xmlDoc.SelectNodes("Ksiazki/Autor");
            XmlNodeList genreList = xmlDoc.SelectNodes("Ksiazki/Gatunek");
            XmlNodeList warehouseList = xmlDoc.SelectNodes("Ksiazki/Magazyn");

            // Przetwarzanie węzłów i dodawanie danych do listy

            for (int i = 0; i < bookList.Count; i++)
            {
                XmlNode Nodes = bookList[i];

                string pricePattern = @"^\d{1,6},\d{2}$";

                bool isMatch = Regex.IsMatch(price, pricePattern);

                string datePattern = @"^\d{1,4}$";

                bool isMatch2 = Regex.IsMatch(releaseDate, datePattern);

                string quantityPattern = @"^\d{1,8}$";

                bool isMatch3 = Regex.IsMatch(quantity, quantityPattern);

                if (Nodes["ID_KSIAZKI"].InnerText == bookId)
                {
                    if (title != "" && price != "" && publisher != "" && releaseDate != "" && isMatch && isMatch2 && isMatch3)
                    {
                        Nodes["TYTUL"].InnerText = title;
                        Nodes["CENA"].InnerText = price;
                        Nodes["WYDAWNICTWO"].InnerText = publisher;
                        Nodes["ROK_WYDANIA"].InnerText = releaseDate;

                        if (change)
                        {
                            byte[] x = File.ReadAllBytes(openFileDialog.FileName);
                            string base64Data = Convert.ToBase64String(x);

                            Nodes["ZDJECIE"].InnerText = base64Data;
                        }

                        AuthorXML.EditAuthor(xmlDoc, authorList, bookId, author);
                        GenreXML.EditGenre(xmlDoc, genreList, bookId, genre);
                        WarehouseXML.EditWarehouse(xmlDoc, warehouseList, bookId, quantity);

                        xmlDoc.Save("output.xml");

                        MessageBox.Show("Wyedytowano książke");
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił błąd formatu (rok lub cena)");
                    }
                }
            }
        }
    }
}

