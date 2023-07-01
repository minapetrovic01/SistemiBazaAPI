using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class Sprat
    {
        public virtual int Id { get; protected set; }
        public virtual int BrojSprata { get; set; }
        public virtual string VrstaSprata { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }

     
        public Sprat()
        {
            
        }
    }
    public class PodzemniSprat : Sprat
    {
        public virtual IList<ParkingMesto> ParkingMesta { get; set; }
        public PodzemniSprat()
        {
            ParkingMesta = new List<ParkingMesto>();
        }
    }
    public class NiziSprat:Sprat 
    {
        public virtual IList<Lokal> Lokali { get; set; }
        public NiziSprat()
        {
            Lokali = new List<Lokal>();
        }
    }
    public class VisiSprat:Sprat
    {
        public virtual IList<Stan> Stanovi { get; set; }
        public VisiSprat()
        {
            Stanovi = new List<Stan>();
        }
    }
}
