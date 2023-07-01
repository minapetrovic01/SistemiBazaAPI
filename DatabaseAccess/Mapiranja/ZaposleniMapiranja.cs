using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class ZaposleniMapiranja:SubclassMap<Entiteti.Zaposleni>
    {
        public ZaposleniMapiranja()
        {
            Table("ZAPOSLENI");

            KeyColumn("MATICNI_BROJ");

            //DiscriminateSubClassesOnColumn("FUPRAVNIK", "FALSE");

            Map(x => x.DatumRodjenja).Column("DATUM_RODJENJA");
            Map(x => x.BrojLicneKarte).Column("BROJ_LK");
            Map(x => x.MestoIzdavanjaLK).Column("MESTO_IZDAVANJA_LK");
            Map(x => x.ImaFunkcijuUpravnika).Column("FUPRAVNIK");
            //References(x => x.ZgradeZaUpravljanje).Column("ID").LazyLoad();
            HasMany(x=>x.ZgradeZaUpravljanje).KeyColumn("UPRAVNIK_ID").LazyLoad().Inverse();
            //References(x => x.Licenca).Unique();
            //HasOne(x => x.Licenca).PropertyRef(x => x.Upravnik).Cascade.All().LazyLoad();
            HasOne(x => x.Licenca).PropertyRef(x=>x.Upravnik).Cascade.All();
            HasMany(x=> x.Obrazovanje).KeyColumn("UPRAVNIK_ID").LazyLoad().Cascade.All().Inverse();
            //mapiranje veza
        }
    }   

    //public class UpravnikMapiranja : SubclassMap<Entiteti.Upravnik>
    //{
    //    public UpravnikMapiranja()
    //    {
    //        DiscriminatorValue("TRUE");
    //    }
    //}
    
}

