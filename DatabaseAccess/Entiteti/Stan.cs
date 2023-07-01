using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Stan
    {
        public virtual int Id { get; protected set; }
        public virtual int RedniBroj { get; set; }
        public virtual StambenaZgrada ZgradaId { get; set; }
        public virtual VlasnikStana Vlasnik { get; set; }
        public virtual VisiSprat BrojSprat { get; set; }
        public virtual IList<ImenaStanara> Stanari { get; set; }
        public Stan()
        {
            Stanari = new List<ImenaStanara>();
        }
    }
}
