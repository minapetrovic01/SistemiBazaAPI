
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class StambenaZgradaMapiranja:ClassMap<Entiteti.StambenaZgrada>
    {
        public StambenaZgradaMapiranja()
        {
            Table("STAMBENA_ZGRADA");

            Id(x => x.Id, "ID").GeneratedBy.SequenceIdentity("STAMBENA_ZGRADA_ID_SEQ");

            References(x => x.Upravnik).Column("UPRAVNIK_ID");
           // References(x => x.Ugovor).Column("SIFRA").LazyLoad();
            HasOne(x=> x.Ugovor).PropertyRef(x => x.Zgrada).Cascade.All();
            HasMany(x => x.Spratovi).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
            HasMany(x=> x.Ulazi).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
            HasMany(x => x.Liftovi).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
            HasMany(x => x.Stanovi).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
            HasMany(x => x.ParkingMesta).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
            HasMany(x=> x.Lokali).KeyColumn("ZGRADA_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
}
