using Bookstore.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace Bookstore.XML
{
    public static class WarehouseXML
    {
        public static void DatabaseToXMLWarehouse(OracleConnection connection, XmlDocument xmlDocument, XmlElement rootElement)
        {
            string sqlQuery = "SELECT id_ksiazki, ilosc_sztuk FROM magazyn";
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
                        XmlElement rowElement = xmlDocument.CreateElement("Magazyn");

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

        public static List<WarehouseDTO> LoadWarehouseData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<WarehouseDTO> warehouseList = new List<WarehouseDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                //Wczytanie dokumentu
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList warehouseNodeList = xmlDoc.SelectNodes("Ksiazki/Magazyn");

            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < warehouseNodeList.Count; i++)
            {
                XmlNode Nodes = warehouseNodeList[i];

                warehouseList.Add(new WarehouseDTO
                {
                    Book_id = Nodes["ID_KSIAZKI"].InnerText,
                    Quantity = Nodes["ILOSC_SZTUK"].InnerText
                });
            }

            return warehouseList;
        }

        public static void WarehouseUpdate(int quantity, string bookId)
        {

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                xmlDoc.Load("output.xml");
            }
            else
            {
                MessageBox.Show("Wystąpił błąd odczytu pliku XML");
            }

            var recordNode = xmlDoc.SelectNodes("//Magazyn[ID_KSIAZKI = '" + bookId + "']");

            foreach (XmlNode a in recordNode)
            {
                if (a != null)
                {
                    // Usunięcie rekordu
                    a.ParentNode.RemoveChild(a);

                }
            }

            XmlElement rootElement = xmlDoc.DocumentElement;
            xmlDoc.AppendChild(rootElement);
            XmlElement rowElement = xmlDoc.CreateElement("Magazyn");

            XmlElement columnElementBookId = xmlDoc.CreateElement("ID_KSIAZKI");
            columnElementBookId.InnerText = bookId.ToString();

            rowElement.AppendChild(columnElementBookId);

            XmlElement columnElementQuantity = xmlDoc.CreateElement("ILOSC_SZTUK");
            columnElementQuantity.InnerText = quantity.ToString();

            // Dodawanie elementu kolumny do wiersza
            rowElement.AppendChild(columnElementQuantity);

            rootElement.AppendChild(rowElement);

            xmlDoc.Save("output.xml");

        }

        public static void AddWarehouseToBook(XmlDocument xmlDoc, int bookId, string quantity)
        {
            XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Magazyn", null);


            XmlElement book_id_element = xmlDoc.CreateElement("ID_KSIAZKI");
            book_id_element.InnerText = bookId.ToString();
            newRecord.AppendChild(book_id_element);

            XmlElement author_name_element = xmlDoc.CreateElement("ILOSC_SZTUK");
            author_name_element.InnerText = quantity;
            newRecord.AppendChild(author_name_element);

            // Dodawanie nowego rekordu do istniejącego pliku XML
            XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
            rootNode.AppendChild(newRecord);

        }

        public static void EditWarehouse(XmlDocument xmlDoc, XmlNodeList warehouseList, string bookId, string quantity)
        {

            int counter = 0;

            //Podmiana danych jeżeli istnieje już rekord
            for (int j = 0; j < warehouseList.Count; j++)
            {
                XmlNode warehouseNodes = warehouseList[j];

                if (warehouseNodes["ID_KSIAZKI"].InnerText == bookId && counter < 1)
                {
                    warehouseNodes["ILOSC_SZTUK"].InnerText = quantity;
                    counter++;
                }
            }
            //Insert jezeli nie istnieje rekord
            if (counter == 0)
            {
                //Tutaj insert
                XmlNode newRecord = xmlDoc.CreateNode(XmlNodeType.Element, "Magazyn", null);


                XmlElement bookIdElement = xmlDoc.CreateElement("ID_KSIAZKI");
                bookIdElement.InnerText = bookId;
                newRecord.AppendChild(bookIdElement);

                XmlElement quantityElement = xmlDoc.CreateElement("ILOSC_SZTUK");
                quantityElement.InnerText = quantity;
                newRecord.AppendChild(quantityElement);

                // Dodawanie nowego rekordu do istniejącego pliku XML
                XmlNode rootNode = xmlDoc.SelectSingleNode("Ksiazki");
                rootNode.AppendChild(newRecord);
            }
        }
    }
}
