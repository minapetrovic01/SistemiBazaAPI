using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class StambenaZgrada
    {
        public virtual int Id { get; set; }
        public virtual Zaposleni Upravnik { get; set; }
        public virtual Ugovor Ugovor { get; set; }
        public virtual IList<Sprat> Spratovi { get; set; }
        public virtual IList<Ulaz> Ulazi { get; set; }
        public virtual IList<Lift> Liftovi { get; set; }
        public virtual IList<Stan> Stanovi { get; set; }
        public virtual IList<ParkingMesto> ParkingMesta { get; set; }
        public virtual IList<Lokal> Lokali { get; set; }

        public StambenaZgrada()
        {
            Spratovi = new List<Sprat>();
            Ulazi = new List<Ulaz>();
            Liftovi = new List<Lift>();
            Stanovi = new List<Stan>();
            ParkingMesta = new List<ParkingMesto>();
            Lokali = new List<Lokal>();
        }
    }
}
