using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Ugovor
    {
        public virtual string Sifra { get; set; }
        public virtual DateTime DatumPotpisivanja { get; set; }
        public virtual int PeriodVazenja { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }
    }
}
