using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class LokalMapiranja:ClassMap<Entiteti.Lokal>
    {
        public LokalMapiranja() 
        { 
            Table("LOKAL");

            Id(x=> x.Id, "ID").GeneratedBy.TriggerIdentity();
            
            Map(x => x.RedniBroj, "REDNI_BROJ");
            Map(x => x.ImeFirme).Column("IME_FIRME");
            References(x => x.Sprat).Column("SPRAT_ID").LazyLoad();
            References(x=>x.Zgrada).Column("ZGRADA_ID").LazyLoad();

        }
    }
}
