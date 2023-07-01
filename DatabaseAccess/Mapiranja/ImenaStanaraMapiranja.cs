using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class ImenaStanaraMapiranja:ClassMap<Entiteti.ImenaStanara>
    {
        public ImenaStanaraMapiranja() 
        {
            Table("IMENA_STANARA");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();
            Map(x=>x.Ime).Column("IME");
            Map(x => x.Prezime).Column("PREZIME");
            References(x => x.Stan).Column("STAN_ID").LazyLoad();
        }
    }
}
