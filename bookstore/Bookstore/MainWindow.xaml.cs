using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Bookstore.DTO;
using Bookstore.XML;
using Bookstore.Database;
using Bookstore.GlobalMethods;

namespace Bookstore
{

    public partial class MainWindow : Window
    {
        bool selected = false; //czy zaznaczono element (służy do przejścia do edycji)
        string bookId = ""; //Id zaznaczonego elementu
        private Popup popup;

        public MainWindow()
        {
            InitializeComponent();
            DBsync(); //inicjalizacja i wpisanie danych do ListView
        }
        public void DBsync()
        {
            var pList = BookXML.LoadBookShortData();
            lista.ItemsSource = pList;
        }

        private void NewBookClick(object sender, RoutedEventArgs e)
        {
            var window = new NewBook();
            window.Top = Top;
            window.Left = Left;
            window.Height = ActualHeight;
            window.Width = ActualWidth;
            Close();
            window.ShowDialog();
        }

        private void EditBookClick(object sender, RoutedEventArgs e)
        {
            if (selected == true)
            {
                var window = new EditBook(bookId);
                window.Top = Top;
                window.Left = Left;
                window.Height = ActualHeight;
                window.Width = ActualWidth;
                Close();
                window.ShowDialog();
            }
        }

        private async void SaveToDatabaseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                PopupMessage.ShowMessage(ref popup,"Trwa wykonywanie operacji na bazie danych", this);
                await Task.Run(() =>
                {
                    DB.UpdateDB();
                });
                PopupMessage.HideMessage(ref popup);
                MessageBox.Show("Operacja wykonana");
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd na bazie danych");
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = ActualHeight * 0.03;
            Height = Width * 0.5;
        }
        

        private void ListViewClick(object sender, MouseButtonEventArgs e)
        {

           
            if (((ListView)sender).SelectedItem == null) 
            { 
                    return; 
            }

            var selectedBook = (BookShortDTO)((ListView)sender).SelectedItem;
            //string k = book.Title;
            bookId = selectedBook.Id;

            //wybranie z bazy dla konkretnego ID
            var booksXML = BookXML.LoadBookData();

            try
            {
                BookDTO selectedBookData = booksXML.Where(c => c.Id == bookId).First();

                opis.Text = "Wydawca: " + selectedBookData.Publisher + "\n" + "Cena: " + selectedBookData.Price + "\n" + "Data wydania: " + selectedBookData.Pub_date;

                BitmapImage image = BitmapOperations.ByteArrayToImage(selectedBookData.Zdjecie);
                obraz.Source = image;
            }
            catch
            {
                opis.Text = "Wydawca: " + "Brak" + "\n" + "Cena: " + "Brak" + "\n" + "Data wydania: " + "Brak";
            }


            var genresXML = GenreXML.LoadGenreData();
            try
            {
                GenreDTO selectedBookGenreData = genresXML.Where(c => c.Book_id == bookId).First();
                kategoria.Text = selectedBookGenreData.Genre_name;
            }
            catch
            {
                kategoria.Text = "Brak";
            }

            var authorsXML = AuthorXML.LoadAuthorData();

            try
            {
                AuthorDTO selectedBookAuthorData = authorsXML.Where(c => c.Book_id == bookId).First();
                ilosc.Text = selectedBookAuthorData.Name + " " + selectedBookAuthorData.Surname;
            }
            catch
            {
                ilosc.Text = "Brak";
            }

            var warehouseXML = WarehouseXML.LoadWarehouseData();

            try
            {
                //Ponieważ może być kilka rekordów dotyczących tej samej książki to jest to lista
                List<WarehouseDTO> selectedBookWarehouseData = warehouseXML.Where(c => c.Book_id == bookId).ToList();
                int quantity = 0;

                foreach(var w in selectedBookWarehouseData)
                {
                    quantity += Convert.ToInt32(w.Quantity);
                }

                ilosc_sztuk.Text = Convert.ToString(quantity);

            }
            catch
            {
                ilosc_sztuk.Text = "Błąd";
            }

            selected = true; //jeżeli zaznaczono to znaczy ze będzie można edytować
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            selected = false;
            string searchCriterium = SearchBox.Text;
            if (searchCriterium != "")
            {
                var books = BookXML.LoadBookShortData();

                var pList = books.Select(p => new BookShortDTO { Id = p.Id, Title = p.Title });

                var search = from x in pList where x.Title.StartsWith(searchCriterium) select x;
                lista.ItemsSource = search.ToList();
            }
            else
            {
                var books = BookXML.LoadBookShortData();
                var pList = books.Select(p => new BookShortDTO { Id = p.Id, Title = p.Title });
                lista.ItemsSource = pList.ToList();
            }
        }

        private void AscendingIdClick(object sender, RoutedEventArgs e)
        {
            var pList = (IEnumerable<BookShortDTO>)lista.ItemsSource;
            var sortAscending = pList.OrderBy(x => Convert.ToInt32(x.Id));
            lista.ItemsSource = sortAscending;
        }

        private void DescendingIdClick(object sender, RoutedEventArgs e)
        {
            var pList = (IEnumerable<BookShortDTO>)lista.ItemsSource;
            var sortDescending = pList.OrderByDescending(x => Convert.ToInt32(x.Id));
            lista.ItemsSource = sortDescending;
        }

        private void AscendingTitleClick(object sender, RoutedEventArgs e)
        {
            var pList = (IEnumerable<BookShortDTO>)lista.ItemsSource;
            var sortAscending = pList.OrderBy(x => x.Title);
            lista.ItemsSource = sortAscending;
        }

        private void DescendingTitleClick(object sender, RoutedEventArgs e)
        {
            var pList = (IEnumerable<BookShortDTO>)lista.ItemsSource;
            var sortDescending = pList.OrderByDescending(x => x.Title);
            lista.ItemsSource = sortDescending;
        }

        

        private void DecreaseWarehouseStock(object sender, RoutedEventArgs e)
        {
            var warehouseXML = WarehouseXML.LoadWarehouseData();

            try
            {
                var selectedBookWarehouseData = warehouseXML.Where(c => c.Book_id == bookId).ToList();
                int quantity = 0;

                foreach (var w in selectedBookWarehouseData)
                {
                    quantity += Convert.ToInt32(w.Quantity);
                }

                if(quantity - Convert.ToInt32(ilosc_sztuk_zmniejsz.Text) > 0) 
                {
                    quantity -= Convert.ToInt32(ilosc_sztuk_zmniejsz.Text);
                    ilosc_sztuk.Text = Convert.ToString(quantity);
                    //update na XML
                    try
                    {
                        WarehouseXML.WarehouseUpdate(quantity, bookId);
                    }
                    catch
                    {
                        MessageBox.Show("Wystąpił błąd");
                    }
                }
                else
                {
                    MessageBox.Show("Za mała ilość w magazynie");
                }

            }
            catch
            {
                MessageBox.Show("Wystąpił błąd ładowania");
            }
        }
    }
}
