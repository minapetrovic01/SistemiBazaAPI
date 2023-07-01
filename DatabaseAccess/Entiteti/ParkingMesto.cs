using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class ParkingMesto
    {
        public virtual int Id { get; set; }
        public virtual int RedniBroj { get; set; }
        public virtual string RegistarskiBrojRezervisanog { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }
        public virtual PodzemniSprat Sprat { get; set; }

        public ParkingMesto()
        {
        }
    }
}
