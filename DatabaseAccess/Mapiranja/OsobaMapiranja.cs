using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    class OsobaMapiranja:ClassMap<Entiteti.Osoba>
    {

        public OsobaMapiranja()
        {
            Table("OSOBA");

            //proveriti da li je TriggerIdentity 
            //Id(x => x.MaticniBroj, "MATICNI_BROJ").GeneratedBy.TriggerIdentity();

            //nije triger nego je maticni broj assigned
            Id(x => x.MaticniBroj, "MATICNI_BROJ").GeneratedBy.Assigned();

           


            Map(x => x.Ime, "IME");
            Map(x => x.ImeRoditelja, "IME_RODITELJA");
            Map(x => x.Prezime, "PREZIME");
            Map(x => x.Adresa, "ADRESA");
            Map(x => x.BrojTelefona, "BROJ_TELEFONA");

            //mapiranje veza

        }

    }
    class VlasnikStanaMapiranja : SubclassMap<Entiteti.VlasnikStana>
    {
        public VlasnikStanaMapiranja()
        {
            Table("VLASNIK_STANA");
            KeyColumn("MATICNI_BROJ");
            Map(x => x.Funkcija, "FUNKCIJA");
            //mapiranje veza
            HasMany(x => x.Stanovi).KeyColumn("VLASNIK_ID").LazyLoad().Cascade.All().Inverse();
        }
    }
}
