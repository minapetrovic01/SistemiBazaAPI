using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Licenca
    {
        public virtual int Id { get;  set; }
        public virtual string BrojLicence { get; set; }
        public virtual string NazivInstitucije { get; set; }
        public virtual Zaposleni Upravnik { get; set; }
        public virtual DateTime DatumSticanja { get; set; }

        public Licenca()
        {
            
        }

    }
}
