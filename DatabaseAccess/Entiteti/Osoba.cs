using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Osoba
    {
        public virtual string MaticniBroj { get; /*protected*/ set; }
        public virtual string Ime { get; set; }
        public virtual string ImeRoditelja { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Adresa { get; set; }
        public virtual string BrojTelefona { get; set; }
    }
    public class VlasnikStana : Osoba
    {
        public virtual string Funkcija { get; set; }

        public virtual IList<Stan> Stanovi { get; set; }

        public VlasnikStana()
        {
            Stanovi = new List<Stan>();
        }
    }
}
