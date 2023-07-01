using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class UgovorMapiranja:ClassMap<Entiteti.Ugovor>
    {
        public UgovorMapiranja() 
        {
            Table("UGOVOR");


            Id(x => x.Sifra, "SIFRA").GeneratedBy.Assigned();

            Map(x => x.DatumPotpisivanja, "DATUM_POTPISIVANJA");
            Map(x => x.PeriodVazenja, "PERIOD_VAZENJA");

            References(x => x.Zgrada).Column("ZGRADA_ID").LazyLoad();
        }

    }
}
