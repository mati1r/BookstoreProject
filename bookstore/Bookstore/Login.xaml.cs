using Bookstore.DTO;
using Bookstore.XML;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bookstore
{
    public partial class Login : Window
    {
        List<EmployeeDTO> pList;
        public Login()
        {
            InitializeComponent();
            XML.XML.WriteToXML();
            LoadXMLData();
        }
        public void LoadXMLData()
        {
            pList = EmployeeXML.LoadEmployeeData();
        }

        private void LoginToApp(object sender, RoutedEventArgs e)
        {
            string Name = name.Text;
            string Surname = surname.Text;

            int login = pList.Count(c => c.Name == Name && c.Surname == Surname);

            if(login > 0)
            {
                var window = new MainWindow();
                window.Top = Top;
                window.Left = Left;
                Close();
                window.ShowDialog();
            }
            else
            {
                blad.Visibility = Visibility.Visible;
            }
        }
        private void OK(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.Enter)
            {
                LoginToApp(sender, e);
            }
        }
    }
}
