using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bazzze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Db _db = new();

        public MainWindow()
        {
            InitializeComponent();
            dg.ItemsSource = _db.Osobas.Local.ToObservableCollection();
            _db.Osobas.Include(o => o.Adresa).ToList();
            listaZanimanja.ItemsSource = _db.Zanimanje.Local.ToObservableCollection();
            listaZanimanja.DisplayMemberPath = "Naziv";
            _db.Zanimanje.ToList();
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            Editor ed = new();
            ed.Owner = this;
            ed.ShowDialog();
            if (ed.DialogResult.HasValue && ed.DialogResult.Value)
            {
                _db.Add(ed.DataContext as Osoba);
                _db.SaveChanges();
            }
        }

        private void Izmena(object sender, RoutedEventArgs e)
        {
            Editor ed = new();
            ed.Owner = this;
            ed.DataContext = dg.SelectedItem;
            ed.ShowDialog();
            if (ed.DialogResult.HasValue && ed.DialogResult.Value)
            {
                _db.SaveChanges();
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            _db.Remove(dg.SelectedItem as Osoba);
            _db.SaveChanges();
        }
    }
}