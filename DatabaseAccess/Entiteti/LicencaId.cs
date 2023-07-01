using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class LicencaId
    {
        public virtual string BrojLicence { get; set; }
        public virtual Zaposleni UpravnikId { get; set; }
        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
                return true;
            if (obj.GetType() != typeof(LicencaId))
                return false;
            LicencaId rObj = (LicencaId)obj;
            if ((BrojLicence == rObj.BrojLicence) && (UpravnikId.MaticniBroj == rObj.UpravnikId.MaticniBroj))
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
}
