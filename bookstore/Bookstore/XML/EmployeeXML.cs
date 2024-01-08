using Bookstore.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bookstore.XML
{
    public static class EmployeeXML
    {
        public static void DatabaseToXMLEmployee(OracleConnection connection, XmlDocument xmlDocument, XmlElement rootElement)
        {
            string sqlQuery = "SELECT id_pracownika, imie, nazwisko FROM pracownik";
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
                        XmlElement rowElement = xmlDocument.CreateElement("Pracownik");

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

        public static List<EmployeeDTO> LoadEmployeeData()
        {
            // Tworzenie listy, do której będą dodawane dane z pliku XML
            List<EmployeeDTO> employeeList = new List<EmployeeDTO>();

            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("output.xml"))
            {
                //Wczytanie dokumentu
                xmlDoc.Load("output.xml");
            }

            // Pobieranie węzłów zawierających dane
            XmlNodeList employeeNodeList = xmlDoc.SelectNodes("Ksiazki/Pracownik");

            // Przetwarzanie węzłów i dodawanie danych do listy
            for (int i = 0; i < employeeNodeList.Count; i++)
            {
                XmlNode Nodes = employeeNodeList[i];

                employeeList.Add(new EmployeeDTO
                {
                    Employee_id = Nodes["ID_PRACOWNIKA"].InnerText,
                    Name = Nodes["IMIE"].InnerText,
                    Surname = Nodes["NAZWISKO"].InnerText
                });
            }

            return employeeList;
        }
    }
}
