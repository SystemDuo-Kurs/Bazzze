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

        private string _pretragaOsoba;

        public string PretragaOsoba
        {
            get => _pretragaOsoba;
            set
            {
                _pretragaOsoba = value;
                lbLevo.ItemsSource = _db.Osobas.Where(osoba =>
                    osoba.Ime.Contains(_pretragaOsoba) || osoba.Prezime.Contains(_pretragaOsoba))
                    .ToList();
            }
        }

        public string PretragaZanimanja
        {
            get; set;
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _db.Osobas.Include(o => o.Adresa).ToList();
            _db.Zanimanja.ToList();

            dg.ItemsSource = _db.Osobas.Local.ToObservableCollection();

            listaZanimanja.ItemsSource = _db.Zanimanja.Local.ToObservableCollection();
            listaZanimanja.DisplayMemberPath = "Naziv";

            lbLevo.ItemsSource = _db.Osobas.ToList();
            lbLevo.DisplayMemberPath = "ImeIPrezime";
            lbDesno.ItemsSource = _db.Zanimanja.Local.ToObservableCollection();
            lbDesno.DisplayMemberPath = "Naziv";

            cmbZam.ItemsSource = _db.Zanimanja.Local.ToObservableCollection();
            cmbZam.DisplayMemberPath = "Naziv";

            lbUhljebi.DisplayMemberPath = "ImeIPrezime";
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
                lbLevo.ItemsSource = _db.Osobas.ToList();
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
                lbLevo.ItemsSource = _db.Osobas.ToList();
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            _db.Remove(dg.SelectedItem as Osoba);
            _db.SaveChanges();
            lbLevo.ItemsSource = _db.Osobas.ToList();
        }

        private void DodajZam(object sender, RoutedEventArgs e)
        {
            EditorZam ed = new();
            ed.Owner = this;
            ed.ShowDialog();
            if (ed.DialogResult.HasValue && ed.DialogResult.Value)
            {
                _db.Add(ed.DataContext as Zanimanje);
                _db.SaveChanges();
            }
        }

        private void IzmenaZam(object sender, RoutedEventArgs e)
        {
            EditorZam ed = new();
            ed.Owner = this;
            ed.DataContext = listaZanimanja.SelectedItem;
            ed.ShowDialog();
            if (ed.DialogResult.HasValue && ed.DialogResult.Value)
            {
                _db.SaveChanges();
            }
        }

        private void BrisanjeZam(object sender, RoutedEventArgs e)
        {
            _db.Remove(listaZanimanja.SelectedItem as Zanimanje);
            _db.SaveChanges();
        }

        private void Zaposli(object sender, RoutedEventArgs e)
        {
            if (lbLevo.SelectedItem is null || lbDesno.SelectedItem is null)
            {
                MessageBox.Show("Joook");
            }
            else
            {
                (lbLevo.SelectedItem as Osoba).Zanimanja
                    .Add(lbDesno.SelectedItem as Zanimanje);
                _db.SaveChanges();
            }
        }

        private void Otposli(object sender, RoutedEventArgs e)
        {
            if (lbLevo.SelectedItem is null || lbDesno.SelectedItem is null)
            {
                MessageBox.Show("Joook");
            }
            else
            {
                (lbLevo.SelectedItem as Osoba).Zanimanja
                    .Remove(lbDesno.SelectedItem as Zanimanje);
                _db.SaveChanges();
            }
        }

        private void PromenaCmb(object sender, SelectionChangedEventArgs e)
        {
            if (cmbZam.SelectedItem is not null)
            {
                lbUhljebi.ItemsSource = _db.Osobas.Include(o => o.Zanimanja)
                    .Where(o => o.Zanimanja.Contains(cmbZam.SelectedItem as Zanimanje))
                    .ToList();
            }
        }
    }
}