using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazzze
{
    public class Zanimanje
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public List<Osoba> Osobe { get; set; } = new();
    }

    public class Osoba
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;

        //get => $"{Ime} {Prezime}" isto kao get {return $"{Ime} {Prezime}";}
        public string ImeIPrezime { get => $"{Ime} {Prezime}"; }

        public string Email { get; set; } = string.Empty;

        public Adresa Adresa { get; set; } = new();
        public List<Zanimanje> Zanimanja { get; set; } = new();
    }

    public class Adresa
    {
        public int Id { get; set; }
        public string Grad { get; set; } = string.Empty;
        public string PO { get; set; } = string.Empty;
        public string Ulica { get; set; } = string.Empty;
        public string Broj { get; set; } = string.Empty;
        public Osoba? Osoba { get; set; }

        public override string ToString()
        {
            return $"{PO} {Grad}";
        }
    }
}