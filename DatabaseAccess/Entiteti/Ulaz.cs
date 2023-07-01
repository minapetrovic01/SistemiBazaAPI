using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Ulaz
    {
        
        public virtual int Id { get; set; }
        public virtual int RedniBroj { get; set; }
        public virtual string VremeKadaJeOtvoren { get; set; }
        public virtual string InstaliranaKamera { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }
    }
}
