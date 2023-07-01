using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpravnikProjekat.Entiteti
{
    public class ParkingMestoId
    {
        public virtual int RedniBroj { get; set; }

        public virtual StambenaZgrada ZgradaId { get; set; }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
                return true;

            if (obj.GetType() != typeof(ParkingMestoId))
                return false;

            ParkingMestoId rObj = (ParkingMestoId)obj;

            if ((ZgradaId.Id == rObj.ZgradaId.Id) && (RedniBroj == rObj.RedniBroj))
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
