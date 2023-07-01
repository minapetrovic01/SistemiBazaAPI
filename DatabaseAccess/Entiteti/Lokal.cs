using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Lokal
    {
        public virtual int Id { get; set; }
        public virtual int RedniBroj { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }
        public virtual string ImeFirme { get; set; }
        public virtual NiziSprat Sprat { get; set; }

        public Lokal()
        {
        }
    }
}
