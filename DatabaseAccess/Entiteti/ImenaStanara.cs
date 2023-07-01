using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class ImenaStanara
    {
        /*
         ID INTEGER PRIMARY KEY,
STAN_ID INTEGER NOT NULL REFERENCES STAN(ID) ON DELETE CASCADE,
IME VARCHAR(25) NOT NULL,
PREZIME VARCHAR(25) NOT NULL
         */
        public virtual int Id { get; protected set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual Stan Stan { get; set; }
    }
}
