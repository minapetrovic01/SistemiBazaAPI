using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class LiftMapiranja:ClassMap<Entiteti.Lift>
    {
        public LiftMapiranja()
        {
            Table("LIFT");

            Id(x => x.SerijskiBroj, "SERIJSKI_BROJ").GeneratedBy.Assigned();

            //DiscriminateSubClassesOnColumn("TIP_LIFTA");

            Map(x => x.NazivProizvodjaca, "NAZIV_PROIZVODJACA");
            Map(x => x.DatumServisiranja, "DATUM_SERVISIRANJA");
            Map(x => x.BrojDanaVanUpotrebe, "BROJ_DANA_VAN_UPOTREBE");
            Map(x => x.DatumPoslenjegKvara, "DATUM_POSLEDNJEG_KVARA");
            Map(x => x.TipLifta).Column("TIP_LIFTA");
            References(x => x.Zgrada).Column("ZGRADA_ID").LazyLoad();
        }
    }

    public class TeretniLiftMapiranja : SubclassMap<Entiteti.TeretniLift>
    {
        public TeretniLiftMapiranja()
        {
            Table("TERETNI_LIFT");
            KeyColumn("SERIJSKI_BROJ");
            //DiscriminatorValue("Teretni");
            Map(x => x.Nosivost, "NOSIVOST");
        }
    }
    public class LiftZaPrevozLjudiMapiranja : SubclassMap<Entiteti.LiftZaPrevozLjudi>
    {
        public LiftZaPrevozLjudiMapiranja()
        {
            Table("LIFT_ZA_PREVOZ_LJUDI");
            KeyColumn("SERIJSKI_BROJ");
            //DiscriminatorValue("Lift za prevoz ljudi");
            //ne moze preko diskriminisuce kolone jer je u pitanju nova tabela
            Map(x => x.MaksimalanBrojOsoba, "MAKISMALAN_BROJ_OSOBA");
        }
    }

}
