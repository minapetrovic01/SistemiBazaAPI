using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class ParkingMestoMapiranja:ClassMap<Entiteti.ParkingMesto>
    {
        public ParkingMestoMapiranja() 
        {
            Table("PARKING_MESTO");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.RedniBroj, "REDNI_BROJ");
            

            Map(x => x.RegistarskiBrojRezervisanog, "REGISTARSKI_BROJ_REZERVISANOG");
            References(x => x.Sprat).Column("SPRAT_ID").LazyLoad();
            References(x => x.Zgrada).Column("ZGRADA_ID").LazyLoad();

        }
    }
}
