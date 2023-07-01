using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class SpratMapiranja:ClassMap<Entiteti.Sprat>
    {
        public SpratMapiranja()
        {
            Table("SPRAT");
            Id(x => x.Id, "ID").GeneratedBy.SequenceIdentity("SPRAT_ID_SEQ");

            DiscriminateSubClassesOnColumn("VRSTA_SPRATA");

            Map(x => x.BrojSprata, "BROJ_SPRATA");
            //Map(x => x.VrstaSprata, "VRSTA_SPRATA");
            References(x => x.Zgrada).Column("ZGRADA_ID").LazyLoad();
           
            
        }
        
    }
    public class PodzemniSpratMapiranja:SubclassMap<Entiteti.PodzemniSprat>
    {
        public PodzemniSpratMapiranja()
        {
            DiscriminatorValue("PODZEMNI");
            HasMany(x => x.ParkingMesta).KeyColumn("SPRAT_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
    public class NiziSprat:SubclassMap<Entiteti.NiziSprat>
    {
        public NiziSprat()
        {
            DiscriminatorValue("NIZI");
            HasMany(x => x.Lokali).KeyColumn("SPRAT_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
    public class VisiSprat:SubclassMap<Entiteti.VisiSprat>
    {
        public VisiSprat()
        {
            DiscriminatorValue("VISI");
            HasMany(x => x.Stanovi).KeyColumn("SPRAT_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
}
