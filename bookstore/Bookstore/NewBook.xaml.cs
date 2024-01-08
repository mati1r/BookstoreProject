using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Bookstore.XML;


namespace Bookstore
{
    /// <summary>
    /// Logika interakcji dla klasy Nowy_wpis.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private bool added = false;
        public NewBook()
        {
            InitializeComponent();
            LoadXMLData();
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
        }

        public void LoadXMLData()
        {
            LoadGenereDataToWindowControls();

            LoadAuthorDataToWindowControls();
        }
        private void AddImage(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "JPG(.jpg)|*.jpg|PNG(.png)|*.png|JPEG(.jpeg)|*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                obraz.Source = new BitmapImage(fileUri);
                added = true;
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FontSize = ActualHeight * 0.03;
            Height = Width * 0.5;
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            var widnow = new MainWindow();
            widnow.Top = Top;
            widnow.Left = Left;
            widnow.Height = ActualHeight;
            widnow.Width = ActualWidth;
            Close();
            widnow.ShowDialog();
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            try
            {
                XML.XML.AddXML(ref nazwa, ref cena, ref data_wydania, ref ilosc_sztuk, ref wydawca, ref gatunek, ref autor, ref obraz, openFileDialog, ref added);
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd lub nie wprowadzono wszytskich danych");
            }
        }
    }
}
