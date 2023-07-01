using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpravnikProjekat.Entiteti;

namespace UpravnikProjekat
{

    #region StambenaZgrada

    public class StambenaZgradaBasic
    {
        public int Id { get; set; }
        public UpravnikBasic Upravnik { get; set; }
        public UgovorBasic Ugovor { get; set; }
        public IList<SpratBasic> Spratovi { get; set; }
        public IList<UlazBasic> Ulazi { get; set; }
        public IList<LiftBasic> Liftovi { get; set; }
        public IList<StanBasic> Stanovi { get; set; }
        public IList<ParkingMestoBasic> ParkingMesta { get; set; }
        public IList<LokalBasic> Lokali { get; set; }

      public StambenaZgradaBasic(int id, UpravnikBasic upravnik, UgovorBasic ugovor)
        {
            Id = id;
            Upravnik = upravnik;
            Ugovor = ugovor;
        }   
       public StambenaZgradaBasic()
       {
            Spratovi=new List<SpratBasic>();
            Ulazi = new List<UlazBasic>();
            Liftovi = new List<LiftBasic>();
            Stanovi=new List<StanBasic>();
            ParkingMesta=new List<ParkingMestoBasic>();
            Lokali=new List<LokalBasic>();

       }
    }
    public class StambenaZgradaPregled
    {
        public int Id { get; set; }
        public UpravnikPregled Upravnik { get; set; }
        public UgovorPregled Ugovor { get; set; }

        public StambenaZgradaPregled(int id, UpravnikPregled upravnik, UgovorPregled ugovor)
        {
            Id = id;
            Upravnik = upravnik;
            Ugovor = ugovor;
        }
    }


    #endregion

    #region Stan
    public class StanBasic
    {
        public int Id { get; protected set; }
        public int RedniBroj { get; set; }
        public StambenaZgradaBasic ZgradaId { get; set; }
        public VlasnikStanaBasic Vlasnik { get; set; }
        public VisiSpratBasic BrojSprat { get; set; }
        public IList<ImenaStanaraPregled> Stanari { get; set; }
        public StanBasic()
        {
            Stanari = new List<ImenaStanaraPregled>();
        }
        public StanBasic(int id, int redniBroj, StambenaZgradaBasic zgradaId, VlasnikStanaBasic vlasnik, VisiSpratBasic brojSprat,List<ImenaStanaraPregled>stanari)
        {
            Id = id;
            RedniBroj = redniBroj;
            ZgradaId = zgradaId;
            Vlasnik = vlasnik;
            BrojSprat = brojSprat;
            Stanari = stanari.ToList();
        }
    }

    public class StanPregled
    {
        public int Id { get; protected set; }
        public int RedniBroj { get; set; }

        public StanPregled(int Id, int RedniBroj)
        {
            this.Id = Id;
            this.RedniBroj = RedniBroj;
        }
    }


    #endregion

    #region Sprat
    public class SpratBasic
    {
        public int Id { get; set; }
        public int BrojSprata { get; set; }
        public string VrstaSprata { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }

        public SpratBasic(int id, int brojSprata, string vrstaSprata, StambenaZgradaBasic zgrada)
        {
            Id = id;
            BrojSprata = brojSprata;
            VrstaSprata = vrstaSprata;
            Zgrada = zgrada;
        }
        public SpratBasic()
        { }
    }
    public class PodzemniSpratBasic : SpratBasic
    {
        public IList<ParkingMestoBasic> ParkingMesta { get; set; }

        public PodzemniSpratBasic()
        {
            ParkingMesta = new List<ParkingMestoBasic>();
        }
        public PodzemniSpratBasic(int id, int brojSprata, string vrstaSprata, StambenaZgradaBasic zgrada)
            : base(id, brojSprata, vrstaSprata, zgrada)
        {
        }
    }
    public class NiziSpratBasic : SpratBasic
    {
        public virtual IList<LokalBasic> Lokali { get; set; }
        public NiziSpratBasic()
        {
            Lokali = new List<LokalBasic>();
        }
        public NiziSpratBasic(int id, int brojSprata, string vrstaSprata, StambenaZgradaBasic zgrada)
            : base(id, brojSprata, vrstaSprata, zgrada)
        {
        }

    }
    public class VisiSpratBasic : SpratBasic
    {
        public IList<StanBasic> Stanovi { get; set; }
        public VisiSpratBasic()
        {
            Stanovi = new List<StanBasic>();
        }

        public VisiSpratBasic(int id, int brojSprata, string vrstaSprata, StambenaZgradaBasic zgrada)
            : base(id, brojSprata, vrstaSprata, zgrada)
        {
        }
    }

    public class SpratPregled
    {
        public int Id { get; set; }
        public int BrojSprata { get; set; }
        public string VrstaSprata { get; set; }
        public SpratPregled(int id, int brojSprata, string vrstaSprata)
        {
            Id = id;
            BrojSprata = brojSprata;
            VrstaSprata = vrstaSprata;
        }
        public SpratPregled() { }
    }

    public class PodzemniSpratPregled : SpratPregled 
    {
        public PodzemniSpratPregled(int id, int brojSprata, string vrstaSprata)
            : base(id, brojSprata, vrstaSprata)
        {
        }
    }
    public class NiziSpratPregled : SpratPregled 
    {
        public NiziSpratPregled(int id, int brojSprata, string vrstaSprata)
            : base(id, brojSprata, vrstaSprata)
        {
        }
    }
    public class VisiSpratPregled : SpratPregled { }



    #endregion

    #region Lokal
    public class LokalBasic
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }
        public string ImeFirme { get; set; }
        public NiziSpratBasic Sprat { get; set; }

        public LokalBasic(int id, int redniBroj, StambenaZgradaBasic zgrada, string imeFirme, NiziSpratBasic sprat)
        {
            Id = id;
            RedniBroj = redniBroj;
            Zgrada = zgrada;
            ImeFirme = imeFirme;
            Sprat = sprat;
        }
    }

    public class LokalPregled
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public string ImeFirme { get; set; }
        public NiziSpratPregled Sprat { get; set; }

        public LokalPregled(int id, int redniBroj, string imeFirme, NiziSpratPregled sprat)
        {
            Id = id;
            RedniBroj = redniBroj;
            ImeFirme = imeFirme;
            Sprat = sprat;
        }
    }

    #endregion

    #region Ugovor
    public class UgovorBasic
    {
        public string Sifra { get; set; }
        public DateTime DatumPotpisivanja { get; set; }
        public int PeriodVazenja { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }

        public UgovorBasic(string sifra, DateTime datumPotpisivanja, int periodVazenja, StambenaZgradaBasic zgrada)
        {
            Sifra = sifra;
            DatumPotpisivanja = datumPotpisivanja;
            PeriodVazenja = periodVazenja;
            Zgrada = zgrada;
        }
    }

    public class UgovorPregled
    {
        public string Sifra { get; set; }
        public DateTime DatumPotpisivanja { get; set; }
        public int PeriodVazenja { get; set; }
        public UgovorPregled(string sifra, DateTime datumPotpisivanja, int periodVazenja)
        {
            Sifra = sifra;
            DatumPotpisivanja = datumPotpisivanja;
            PeriodVazenja = periodVazenja;
        }
    }


    #endregion

    #region ImenaStanra
    public class ImenaStanaraBasic
    {
        public  int Id { get; protected set; }
        public  string Ime { get; set; }
        public  string Prezime { get; set; }
        public  StanBasic Stan { get; set; }

        public ImenaStanaraBasic(int id, string ime, string prezime, StanBasic stan)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            Stan = stan;
        }
    }
    public class ImenaStanaraPregled
    {
        public int Id { get; protected set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public ImenaStanaraPregled(int id, string ime, string prezime)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
        }
    }

    #endregion

   

}
