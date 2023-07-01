using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UpravnikProjekat.Entiteti;

namespace UpravnikProjekat
{
    #region Osoba

    public class OsobaBasic
    {
        public string MaticniBroj { get; set; }
        public string Ime { get; set; }
        public string ImeRoditelja { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }

        public OsobaBasic(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string
            BrojTelefona)
        {
            this.MaticniBroj = MaticniBroj;
            this.Ime = Ime;
            this.ImeRoditelja = ImeRoditelja;
            this.Prezime = Prezime;
            this.Adresa = Adresa;
            this.BrojTelefona = BrojTelefona;
        }
        public OsobaBasic()
        {

        }
    }

    #endregion

    #region Zaposleni

    public class ZaposleniPregled : OsobaBasic
    {

        public DateTime DatumRodjenja { get; set; }
        public string BrojLicneKarte { get; set; }
        public string MestoIzdavanjaLK { get; set; }
        public string ImaFunkcijuUpravnika { get; set; }

        public ZaposleniPregled() { }
        public ZaposleniPregled(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string
            BrojTelefona, DateTime DatumRodjenja, string BrojLicneKarte, string MestoIzdavanjaLK, string ImaFunkcijuUpravnika)
            : base(MaticniBroj, Ime, ImeRoditelja, Prezime, Adresa, BrojTelefona)

        {

            this.DatumRodjenja = DatumRodjenja;
            this.BrojLicneKarte = BrojLicneKarte;
            this.MestoIzdavanjaLK = MestoIzdavanjaLK;
            this.ImaFunkcijuUpravnika = ImaFunkcijuUpravnika;
        }
    }
    public class ZaposleniBasic : OsobaBasic
    {
        public DateTime DatumRodjenja { get; set; }
        public string BrojLicneKarte { get; set; }
        public string MestoIzdavanjaLK { get; set; }
        public string ImaFunkcijuUpravnika { get; set; }
        public LicencaPregled Licenca { get; set; }
        public IList<VidObrazovanjaBasic> Obrazovanje { get; set; }
        public IList<StambenaZgradaBasic> ZgradeZaUpravljanje { get; set; }
        public ZaposleniBasic()
        {
            Obrazovanje = new List<VidObrazovanjaBasic>();
            ZgradeZaUpravljanje = new List<StambenaZgradaBasic>();
        }
        public ZaposleniBasic(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string
            BrojTelefona, DateTime DatumRodjenja, string BrojLicneKarte, string MestoIzdavanjaLK, string ImaFunkcijuUpravnika, LicencaPregled Licenca)
            : base(MaticniBroj, Ime, ImeRoditelja, Prezime, Adresa, BrojTelefona)

        {

            this.DatumRodjenja = DatumRodjenja;
            this.BrojLicneKarte = BrojLicneKarte;
            this.MestoIzdavanjaLK = MestoIzdavanjaLK;
            this.ImaFunkcijuUpravnika = ImaFunkcijuUpravnika;
            this.Licenca = Licenca;

        }


    }

    #endregion

    #region Upravnik  

    public class UpravnikPregled : ZaposleniPregled
    {
        public UpravnikPregled(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string
            BrojTelefona, DateTime DatumRodjenja, string BrojLicneKarte, string MestoIzdavanjaLK, string ImaFunkcijuUpravnika)
            : base(MaticniBroj, Ime, ImeRoditelja, Prezime, Adresa, BrojTelefona, DatumRodjenja, BrojLicneKarte, MestoIzdavanjaLK, ImaFunkcijuUpravnika)
        {

        }
    }
    public class UpravnikBasic : ZaposleniBasic
    { }

    #endregion  

    #region VlasnikStana
    public class VlasnikStanaBasic : OsobaBasic
    {
        public string Funkcija { get; set; }

        public IList<StanBasic> Stanovi { get; set; }

        public VlasnikStanaBasic()
        {
            Stanovi = new List<StanBasic>();
        }

        public VlasnikStanaBasic(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string BrojTelefona, string Funkcija)
            : base(MaticniBroj, Ime, ImeRoditelja, Prezime, Adresa, BrojTelefona)
        {

            this.Funkcija = Funkcija;
        }
    }
    public class VlasnikStanaPregled : OsobaBasic
    {
        public string Funkcija { get; set; }

        public VlasnikStanaPregled(string MaticniBroj, string Ime, string ImeRoditelja, string Prezime, string Adresa, string BrojTelefona, string Funkcija)
            : base(MaticniBroj, Ime, ImeRoditelja, Prezime, Adresa, BrojTelefona)
        {

            this.Funkcija = Funkcija;
        }
    }

    #endregion

    #region ParkingMesto
    public class ParkingMestoBasic
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public string RegistarskiBrojRezervisanog { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }
        public PodzemniSpratBasic Sprat { get; set; }

        public ParkingMestoBasic(int id, int redniBroj, string registarskiBrojRezervisanog, StambenaZgradaBasic zgrada, PodzemniSpratBasic sprat)
        {
            Id = id;
            RedniBroj = redniBroj;
            RegistarskiBrojRezervisanog = registarskiBrojRezervisanog;
            Zgrada = zgrada;
            Sprat = sprat;
        }
    }

    public class ParkingMestoPregled
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public string RegistarskiBrojRezervisanog { get; set; }
        public PodzemniSpratPregled Sprat { get; set; }

        public ParkingMestoPregled(int id, int redniBroj, string registarskiBrojRezervisanog, PodzemniSpratPregled sprat)
        {
            Id = id;
            RedniBroj = redniBroj;
            RegistarskiBrojRezervisanog = registarskiBrojRezervisanog;
            Sprat = sprat;
        }


    }


    #endregion

    #region Ulaz
    public class UlazBasic
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public string VremeKadaJeOtvoren { get; set; }
        public string InstaliranaKamera { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }

        public UlazBasic(int id, int redniBroj, string vremeKadaJeOtvoren, string instaliranaKamera, StambenaZgradaBasic zgrada)
        {
            Id = id;
            RedniBroj = redniBroj;
            VremeKadaJeOtvoren = vremeKadaJeOtvoren;
            InstaliranaKamera = instaliranaKamera;
            Zgrada = zgrada;
        }
    }
    public class UlazPregled
    {
        public int Id { get; set; }
        public int RedniBroj { get; set; }
        public string VremeKadaJeOtvoren { get; set; }
        public string InstaliranaKamera { get; set; }

        public UlazPregled(int id, int redniBroj, string vremeKadaJeOtvoren, string instaliranaKamera)
        {
            Id = id;
            RedniBroj = redniBroj;
            VremeKadaJeOtvoren = vremeKadaJeOtvoren;
            InstaliranaKamera = instaliranaKamera;
        }
    }

    #endregion

    #region Lift
    public class LiftBasic
    {
        public string SerijskiBroj { get; set; }
        public string NazivProizvodjaca { get; set; }
        public DateTime DatumServisiranja { get; set; }
        public string BrojDanaVanUpotrebe { get; set; }
        public DateTime DatumPoslenjegKvara { get; set; }
        public string TipLifta { get; set; }
        public StambenaZgradaBasic Zgrada { get; set; }

        public LiftBasic(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta, StambenaZgradaBasic zgrada)
        {
            SerijskiBroj = serijskiBroj;
            NazivProizvodjaca = nazivProizvodjaca;
            DatumServisiranja = datumServisiranja;
            BrojDanaVanUpotrebe = brojDanaVanUpotrebe;
            DatumPoslenjegKvara = datumPoslenjegKvara;
            TipLifta = tipLifta;
            Zgrada = zgrada;
        }
    }
    public class LiftTeretniBasic : LiftBasic
    {
        public int Nosivost { get; set; }
        public LiftTeretniBasic(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta, StambenaZgradaBasic zgrada, int nosivost)
            : base(serijskiBroj, nazivProizvodjaca, datumServisiranja, brojDanaVanUpotrebe, datumPoslenjegKvara, tipLifta, zgrada)
        {
            Nosivost = nosivost;
        }
    }
    public class LiftZaPrevozLjudiBasic : LiftBasic
    {
        public int MaksimalanBrojOsoba { get; set; }

        public LiftZaPrevozLjudiBasic(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta, StambenaZgradaBasic zgrada, int MaksimalanBrojOsoba)
           : base(serijskiBroj, nazivProizvodjaca, datumServisiranja, brojDanaVanUpotrebe, datumPoslenjegKvara, tipLifta, zgrada)
        {
            this.MaksimalanBrojOsoba = MaksimalanBrojOsoba;
        }
    }

    public class LiftPregled
    {
        public string SerijskiBroj { get; set; }
        public string NazivProizvodjaca { get; set; }
        public DateTime DatumServisiranja { get; set; }
        public string BrojDanaVanUpotrebe { get; set; }
        public DateTime DatumPoslenjegKvara { get; set; }
        public string TipLifta { get; set; }
        public LiftPregled()
        {
        }
        public LiftPregled(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta)
        {
            SerijskiBroj = serijskiBroj;
            NazivProizvodjaca = nazivProizvodjaca;
            DatumServisiranja = datumServisiranja;
            if(brojDanaVanUpotrebe == null)
            {
                brojDanaVanUpotrebe = "0";
            }
            BrojDanaVanUpotrebe = brojDanaVanUpotrebe;
            DatumPoslenjegKvara = datumPoslenjegKvara;
            TipLifta = tipLifta;
        }
    }
    public class LiftTeretniPregled : LiftPregled
    {
        public int Nosivost { get; set; }
        public LiftTeretniPregled(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta, int nosivost)
            : base(serijskiBroj, nazivProizvodjaca, datumServisiranja, brojDanaVanUpotrebe, datumPoslenjegKvara, tipLifta)
        {
            this.Nosivost = nosivost;
        }
    }
    public class LiftZaPrevozLjudiPregled : LiftPregled
    {
        public int MaksimalanBrojOsoba { get; set; }

        public LiftZaPrevozLjudiPregled(string serijskiBroj, string nazivProizvodjaca, DateTime datumServisiranja, string brojDanaVanUpotrebe, DateTime datumPoslenjegKvara, string tipLifta, int MaksimalanBrojOsoba)
    : base(serijskiBroj, nazivProizvodjaca, datumServisiranja, brojDanaVanUpotrebe, datumPoslenjegKvara, tipLifta)
        {
            this.MaksimalanBrojOsoba = MaksimalanBrojOsoba;
        }
    }

    #endregion

    #region VidObrazovanja
    public class VidObrazovanjaBasic
    {
        public int Id { get; set; }
        public DateTime DatumSticanjaDiplome { get; set; }
        public string Zvanje { get; set; }
        public string NazivInstitucije { get; set; }
        public Zaposleni Upravnik { get; set; }

        public VidObrazovanjaBasic(int id, DateTime datumSticanjaDiplome, string zvanje, string nazivInstitucije, Zaposleni upravnik)
        {
            Id = id;
            DatumSticanjaDiplome = datumSticanjaDiplome;
            Zvanje = zvanje;
            NazivInstitucije = nazivInstitucije;
            Upravnik = upravnik;
        }
    }
    public class VidObrazovanjaPregled
    {
        public int Id { get; set; }
        public DateTime DatumSticanjaDiplome { get; set; }
        public string Zvanje { get; set; }
        public string NazivInstitucije { get; set; }
        //public VidObrazovanjaPregled(DateTime datumSticanjaDiplome, string zvanje, string nazivInstitucije)
        //{
        //    DatumSticanjaDiplome = datumSticanjaDiplome;
        //    Zvanje = zvanje;
        //    NazivInstitucije = nazivInstitucije;
        //}
        public VidObrazovanjaPregled(int id,DateTime datumSticanjaDiplome, string zvanje, string nazivInstitucije)
        {
            this.Id = id;
            DatumSticanjaDiplome = datumSticanjaDiplome;
            Zvanje = zvanje;
            NazivInstitucije = nazivInstitucije;
        }
    }

    #endregion

    #region Licenca
    public class LicencaBasic
    {
        public int Id { get; protected set; }
        public string BrojLicence { get; set; }
        public string NazivInstitucije { get; set; }
        public UpravnikBasic Upravnik { get; set; }
        public DateTime DatumSticanja { get; set; }

        public LicencaBasic(int id, string brojLicence, string nazivInstitucije, UpravnikBasic upravnik, DateTime datumSticanja)
        {
            Id = id;
            BrojLicence = brojLicence;
            NazivInstitucije = nazivInstitucije;
            Upravnik = upravnik;
            DatumSticanja = datumSticanja;
        }
    }

    public class LicencaPregled
    {
        public int Id { get; protected set; }
        public string BrojLicence { get; set; }
        public string NazivInstitucije { get; set; }
        public DateTime DatumSticanja { get; set; }

        public LicencaPregled(int id, string brojLicence, string nazivInstitucije, DateTime datumSticanja)
        {
            Id = id;
            BrojLicence = brojLicence;
            NazivInstitucije = nazivInstitucije;
            DatumSticanja = datumSticanja;
        }
        //public LicencaPregled(string brojLicence, string nazivInstitucije, DateTime datumSticanja)
        //{
        //    BrojLicence = brojLicence;
        //    NazivInstitucije = nazivInstitucije;
        //    DatumSticanja = datumSticanja;
        //}
    }

    #endregion

}

