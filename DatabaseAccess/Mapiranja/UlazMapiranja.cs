using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class UlazMapiranja:ClassMap<Entiteti.Ulaz>
    {
        public UlazMapiranja()
        {
            Table("ULAZ");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            Map(x => x.RedniBroj).Column("REDNI_BROJ");
            Map(x => x.VremeKadaJeOtvoren).Column("VREME_KADA_JE_OTVOREN");
            Map(x => x.InstaliranaKamera).Column("INSTALIRANA_KAMERA");
            References(x => x.Zgrada).Column("ZGRADA_ID").LazyLoad();
        }
    }
}
