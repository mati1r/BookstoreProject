﻿#pragma checksum "..\..\..\NewBook.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8EA19ED7ECF27F693AD31D1B5348F65B2FBE1861"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Bookstore {
    
    
    /// <summary>
    /// NewBook
    /// </summary>
    public partial class NewBook : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 73 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Dodaj_ksiazke;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Dodaj_zdjecie;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Powrot;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nazwa;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox autor;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox gatunek;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image obraz;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox data_wydania;
        
        #line default
        #line hidden
        
        
        #line 253 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cena;
        
        #line default
        #line hidden
        
        
        #line 266 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox wydawca;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\NewBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ilosc_sztuk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bookstore;component/newbook.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\NewBook.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\NewBook.xaml"
            ((Bookstore.NewBook)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.WindowSizeChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Dodaj_ksiazke = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\NewBook.xaml"
            this.Dodaj_ksiazke.Click += new System.Windows.RoutedEventHandler(this.AddBook);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Dodaj_zdjecie = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\NewBook.xaml"
            this.Dodaj_zdjecie.Click += new System.Windows.RoutedEventHandler(this.AddImage);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Powrot = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\NewBook.xaml"
            this.Powrot.Click += new System.Windows.RoutedEventHandler(this.GoBackClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.nazwa = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.autor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.gatunek = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.obraz = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.data_wydania = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.cena = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.wydawca = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.ilosc_sztuk = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

