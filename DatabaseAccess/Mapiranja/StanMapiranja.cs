using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class StanMapiranja:ClassMap<Entiteti.Stan>
    {
        public StanMapiranja() 
        {
            Table("STAN");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.RedniBroj, "REDNI_BROJ");
            References(x => x.ZgradaId).Column("ZGRADA_ID").LazyLoad();
            References(x => x.Vlasnik).Column("VLASNIK_ID").LazyLoad();
            References(x => x.BrojSprat).Column("SPRAT_ID").LazyLoad();
            HasMany(x => x.Stanari).KeyColumn("STAN_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
}
