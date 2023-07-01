using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Zaposleni:Osoba
    {
        public virtual DateTime DatumRodjenja { get; set; }
        public virtual string BrojLicneKarte { get; set; }
        public virtual string MestoIzdavanjaLK { get; set; }
        public virtual string ImaFunkcijuUpravnika { get; set; }
        public virtual Licenca Licenca { get; set; }
        public virtual IList<VidObrazovanja> Obrazovanje { get; set; }

        public virtual IList<StambenaZgrada> ZgradeZaUpravljanje { get; set; }

        public Zaposleni()
        {
             ZgradeZaUpravljanje = new List<StambenaZgrada>();
             Obrazovanje = new List<VidObrazovanja>();
        }
    }
    public class Upravnik : Zaposleni
    {

    }

}
