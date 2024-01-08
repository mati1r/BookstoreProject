using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Bookstore.GlobalMethods
{
    public static class PopupMessage
    {
        public static void HideMessage(ref Popup popup)
        {
            // Zamknij okno dialogowe MessageBox
            if (popup != null)
            {
                popup.IsOpen = false;
                popup = null;
            }
        }

        public static void ShowMessage(ref Popup popup, string message, UIElement placementTarget)
        {
            // Utwórz nowe Popup
            popup = new Popup
            {
                Placement = PlacementMode.Center,
                PlacementTarget = placementTarget,
                AllowsTransparency = true,
                PopupAnimation = PopupAnimation.Fade
            };

            // Utwórz kontrolkę TextBlock do wyświetlania komunikatu
            TextBlock textBlock = new TextBlock();
            textBlock.Text = message;
            textBlock.FontSize = 20;
            textBlock.Foreground = Brushes.White;
            textBlock.Background = Brushes.DarkBlue;
            textBlock.Padding = new Thickness(20);

            // Dodaj TextBlock do zawartości Popup
            popup.Child = textBlock;

            // Wyświetl Popup
            popup.IsOpen = true;
        }
    }
}
