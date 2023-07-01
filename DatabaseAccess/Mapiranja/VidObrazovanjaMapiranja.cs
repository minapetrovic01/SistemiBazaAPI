using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class VidObrazovanjaMapiranja:ClassMap<Entiteti.VidObrazovanja>
    {
        public VidObrazovanjaMapiranja()
        {
            Table("VID_OBRAZOVANJA");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            Map(x => x.DatumSticanjaDiplome).Column("DATUM_STICANJA_DIPLOME");
            Map(x => x.Zvanje).Column("ZVANJE");
            Map(x => x.NazivInstitucije).Column("NAZIV_INSTITUCIJE");
            References(x => x.Upravnik).Column("UPRAVNIK_ID").LazyLoad();
        }
    }
}
