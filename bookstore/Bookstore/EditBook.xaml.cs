using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using Bookstore.DTO;
using Bookstore.XML;
using Bookstore.GlobalMethods;

namespace Bookstore
{
    /// <summary>
    /// Logika interakcji dla klasy Edycja.xaml
    /// </summary>
    public partial class EditBook : Window
    {
        string bookId;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        bool change = false;

        public EditBook(string book_Id)
        {
            this.bookId = book_Id;
            InitializeComponent();
            LoadXMLData();
        }

        private void LoadBookDataToWindowControls()
        {
            var booksXML = BookXML.LoadBookData();
            var booksShortXML = BookXML.LoadBookShortData();

            BookShortDTO bookTitle = booksShortXML.Where(c => c.Id == bookId).First();
            BookDTO bookData = booksXML.Where(c => c.Id == bookId).First();

            //Przypisanie wartości do kontrolek
            nazwa.Text = bookTitle.Title;
            cena.Text = bookData.Price;
            wydawca.Text = bookData.Publisher;
            data_wydania.Text = bookData.Pub_date;
            BitmapImage image = BitmapOperations.ByteArrayToImage(bookData.Zdjecie);
            obraz.Source = image;
        }

        private void LoadGenereDataToWindowControls()
        {
            var genresXML = GenreXML.LoadGenreData();

            List<string> genreName = new List<string>();
            for (int i = 0; i < genresXML.Count(); i++)
            {
                genreName.Add(genresXML[i].Genre_name);
            }
            genreName.Add("Brak");

            List<string> distinctGenreName = genreName.Select(o => o).Distinct().ToList();

            //Przypisanie wartości do listy wartości gatunków
            gatunek.ItemsSource = distinctGenreName;

            try
            {
                GenreDTO genreData = genresXML.Where(c => c.Book_id == bookId).First();
                gatunek.Text = genreData.Genre_name;
            }
            catch
            {
                gatunek.Text = "Brak";
            }
        }

        private void LoadAuthorDataToWindowControls()
        {
            var authorsXML = AuthorXML.LoadAuthorData();

            List<string> authorName = new List<string>();

            for (int i = 0; i < authorsXML.Count(); i++)
            {
                authorName.Add(authorsXML[i].Name + " " + authorsXML[i].Surname);
            }

            authorName.Add("Brak");
            List<string> distinctAuthorName = authorName.Select(o => o).Distinct().ToList();
            autor.ItemsSource = distinctAuthorName;

            try
            {
                AuthorDTO authorData = authorsXML.Where(c => c.Book_id == bookId).First();
                autor.Text = authorData.Name + " " + authorData.Surname;
            }
            catch
            {
                autor.Text = "Brak";
            }
        }

        private void LoadWarehouseDataToWindowControls()
        {
            var warehouseXML = WarehouseXML.LoadWarehouseData();

            try
            {
                var warehouseData = warehouseXML.Where(c => c.Book_id == bookId).ToList();
                int quantity = 0;

                foreach (var w in warehouseData)
                {
                    quantity += Convert.ToInt32(w.Quantity);
                }

                ilosc_sztuk.Text = Convert.ToString(quantity);

            }
            catch
            {
                ilosc_sztuk.Text = "Błąd";
            }
        }

        public void LoadXMLData()
        {
            LoadBookDataToWindowControls();

            LoadGenereDataToWindowControls();

            LoadAuthorDataToWindowControls();

            LoadWarehouseDataToWindowControls();
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Top = Top;
            window.Left = Left;
            window.Height = ActualHeight;
            window.Width = ActualWidth;
            Close();
            window.ShowDialog();
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = ActualHeight * 0.03;
            Height = Width * 0.5;
        }

        private void PreviewText(object sender, TextCompositionEventArgs e)
        {
            //regex do sprawdzania czy wprowadzana do ilosc jest liczba
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteBookClick(object sender, RoutedEventArgs e)
        {

            XML.XML.DeleteFromXML(bookId);
            MessageBox.Show("Usunięto wpis");

            var window = new MainWindow();
            window.Top = Top;
            window.Left = Left;
            window.Height = ActualHeight;
            window.Width = ActualWidth;
            Close();
            window.ShowDialog();
        }


        private void EditBookClick(object sender, RoutedEventArgs e)
        {
            string price = cena.Text;
            string releaseDate = data_wydania.Text;
            string quantity = ilosc_sztuk.Text;
            string title = nazwa.Text;
            string publisher = wydawca.Text;
            string author = autor.Text;
            string genre = gatunek.Text;

            try
            {
                XML.XML.EditXML(bookId, change, openFileDialog, price, title, releaseDate, quantity, publisher, author, genre);
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd edycji");
            }
        }

        private void ChangeImageClick(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "JPG(.jpg)|*.jpg|PNG(.png)|*.png|JPEG(.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                obraz.Source = new BitmapImage(fileUri);
            }
            if(openFileDialog.FileName != "")
            {
                change = true;
            }
            else
            {
                change = false;
            }
        }
    }
}
