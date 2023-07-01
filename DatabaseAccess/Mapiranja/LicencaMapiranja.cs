using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Mapiranja
{
    public class LicencaMapiranja:ClassMap<Entiteti.Licenca>
    {
        public LicencaMapiranja() 
        {
            Table("LICENCA");

            Id(x=>x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x=>x.BrojLicence, "BROJ_LICENCE");
            Map(x=>x.NazivInstitucije, "NAZIV_INSTITUCIJE");
            Map(x => x.DatumSticanja, "DATUM_STICANJA_ILI_OBNAVLJANJA");
            //References(x => x.Id.UpravnikId).Column("UPRAVNIK_ID");
            References(x => x.Upravnik).Column("UPRAVNIK_ID");

        }
    }
}
