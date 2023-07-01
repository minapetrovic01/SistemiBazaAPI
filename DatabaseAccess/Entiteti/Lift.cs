using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public abstract class Lift
    {
        public virtual string SerijskiBroj { get; set; }
        public virtual string NazivProizvodjaca { get; set; }
        public virtual DateTime DatumServisiranja { get; set; }
        public virtual string BrojDanaVanUpotrebe { get; set; }
        public virtual DateTime DatumPoslenjegKvara { get; set; }
        public virtual string TipLifta { get; set; }
        public virtual StambenaZgrada Zgrada { get; set; }

    }
    public class TeretniLift : Lift
    {
        public virtual int Nosivost { get; set; }
        public TeretniLift() { }
    }
    public class LiftZaPrevozLjudi : Lift
    {
        public virtual int MaksimalanBrojOsoba { get; set; }
        public LiftZaPrevozLjudi() { }
    }
}
