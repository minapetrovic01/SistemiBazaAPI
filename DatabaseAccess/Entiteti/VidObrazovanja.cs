using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class VidObrazovanja
    {
        public virtual int Id { get; set; }
        public virtual DateTime DatumSticanjaDiplome { get; set; }
         public virtual string Zvanje { get; set; }
        public virtual string NazivInstitucije { get; set; }
        public virtual Zaposleni Upravnik { get; set; }
        public VidObrazovanja()
        {
           
        }
    }
}
