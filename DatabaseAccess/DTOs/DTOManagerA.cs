using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping;
using UpravnikProjekat.Entiteti;

namespace UpravnikProjekat
{
    public class DTOManagerA
    {

        public static List<ZaposleniPregled> prikaziSveZaposlene()
        {
            List<ZaposleniPregled> zaposleni = new List<ZaposleniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Zaposleni> sviZaposleni = from z in s.Query<Zaposleni>() select z;

                foreach(Zaposleni zap in sviZaposleni) 
                {
                    zaposleni.Add(new ZaposleniPregled(zap.MaticniBroj, zap.Ime, zap.ImeRoditelja, zap.Prezime, zap.Adresa, zap.BrojTelefona, zap.DatumRodjenja, zap.BrojLicneKarte, zap.MestoIzdavanjaLK,zap.ImaFunkcijuUpravnika));
                }
                s.Close();

            }
            catch(Exception e) 
            {
            }
            return zaposleni;
        }
        public static List<UpravnikPregled> prikaziSveUpravnike()
        {
            List<UpravnikPregled> upravnici = new List<UpravnikPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Zaposleni> sviZaposleni = from z in s.Query<Zaposleni>() select z;

                foreach (Zaposleni zap in sviZaposleni)
                {
                    if(zap.ImaFunkcijuUpravnika=="TRUE")
                    upravnici.Add(new UpravnikPregled(zap.MaticniBroj, zap.Ime, zap.ImeRoditelja, zap.Prezime, zap.Adresa, zap.BrojTelefona, zap.DatumRodjenja, zap.BrojLicneKarte, zap.MestoIzdavanjaLK, zap.ImaFunkcijuUpravnika));
                }
                s.Close();

            }
            catch (Exception e)
            {
            }
            return upravnici;
        }
        public static void dodajZaposlenog(ZaposleniBasic zap)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                //Osoba o = new Osoba();
                Zaposleni z = new Zaposleni();

                z.Ime = zap.Ime;
                z.ImeRoditelja = zap.ImeRoditelja;
                z.Prezime = zap.Prezime;
                z.MaticniBroj=zap.MaticniBroj;
                z.Adresa=zap.Adresa;
                z.BrojTelefona=zap.BrojTelefona;
                z.DatumRodjenja=zap.DatumRodjenja;
                z.BrojLicneKarte = zap.BrojLicneKarte;
                z.MestoIzdavanjaLK = zap.MestoIzdavanjaLK;
                z.ImaFunkcijuUpravnika = zap.ImaFunkcijuUpravnika;

                s.Save(z);

                s.Flush();
                s.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public static void obrisiZaposlenog(string id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Zaposleni r = s.Load<Zaposleni>(id);

                s.Delete(r);
                s.Flush();

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static ZaposleniBasic vratiZaposlenog(string id)
        {
            ZaposleniBasic zaposleni=null;
            try 
            {
                ISession s = DataLayer.GetSession();

                Zaposleni zap = s.Load<Zaposleni>(id);

                
                LicencaPregled lic = vratiLicencuUpravnika(zap.MaticniBroj);
                zaposleni = new ZaposleniBasic(zap.MaticniBroj, zap.Ime, zap.ImeRoditelja, zap.Prezime, zap.Adresa, zap.BrojTelefona, zap.DatumRodjenja, zap.BrojLicneKarte, zap.MestoIzdavanjaLK, zap.ImaFunkcijuUpravnika,lic);
                s.Close();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
            return zaposleni;
        }
        public static void izmeniZaposlenog(ZaposleniBasic zap)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Zaposleni z = s.Load<Zaposleni>(zap.MaticniBroj);

                z.Ime = zap.Ime;
                z.ImeRoditelja = zap.ImeRoditelja;
                z.Prezime = zap.Prezime;
                z.MaticniBroj = zap.MaticniBroj;
                z.Adresa = zap.Adresa;
                z.BrojTelefona = zap.BrojTelefona;
                z.DatumRodjenja = zap.DatumRodjenja;
                z.BrojLicneKarte = zap.BrojLicneKarte;
                z.MestoIzdavanjaLK = zap.MestoIzdavanjaLK;
                z.ImaFunkcijuUpravnika = zap.ImaFunkcijuUpravnika;

                s.Update(z);
                s.Flush();

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static LicencaPregled vratiLicencuUpravnika(string maticniUpravnika)
        {
            LicencaPregled lic=null;
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Licenca> sveLicence = from l in s.Query<Licenca>() select l;

                foreach (Licenca l in sveLicence)
                {
                    if (l.Upravnik.MaticniBroj == maticniUpravnika)
                        lic = new LicencaPregled(l.Id,l.BrojLicence, l.NazivInstitucije, l.DatumSticanja);
                }
                s.Close();

            }
            catch (Exception e)
            {
            }
            return lic;

        }
        public static void izmeniLicencu(LicencaPregled l,string maticniUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                //LicencaBasic bl=vratiLicencuUpravnikaBasic(maticniUpravnika);

                Licenca lic = s.Load<Licenca>(l.Id);
                lic.BrojLicence = l.BrojLicence;
                lic.DatumSticanja = l.DatumSticanja;
                Zaposleni z = s.Load<Zaposleni>(maticniUpravnika);
                lic.Upravnik = z;
                lic.DatumSticanja = l.DatumSticanja;
                lic.NazivInstitucije = l.NazivInstitucije;

                s.Update(lic);
                s.Flush();

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void dodajLicencu(LicencaPregled licneca, string maticniBrojUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Licenca lic = new Licenca();
                lic.BrojLicence = licneca.BrojLicence;
                lic.DatumSticanja= licneca.DatumSticanja;
                Zaposleni z = s.Load<Zaposleni>(maticniBrojUpravnika);
                lic.Upravnik = z;
                lic.DatumSticanja = licneca.DatumSticanja;
                s.Save(lic);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void dodajObrazovanje(VidObrazovanjaPregled obrazovanje, string maticniBrojUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VidObrazovanja v= new VidObrazovanja();
                v.DatumSticanjaDiplome = obrazovanje.DatumSticanjaDiplome;
                v.Zvanje = obrazovanje.Zvanje;
                v.NazivInstitucije = obrazovanje.NazivInstitucije;
                Zaposleni z = s.Load<Zaposleni>(maticniBrojUpravnika);
                v.Upravnik = z;

                s.Save(v);
                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static LicencaBasic vratiLicencuUpravnikaBasic(string maticniUpravnika)
        {
            LicencaBasic lic = null;
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Licenca> sveLicence = from l in s.Query<Licenca>() select l;

                foreach (Licenca l in sveLicence)
                {
                    if (l.Upravnik.MaticniBroj == maticniUpravnika)
                        lic = new LicencaBasic(l.Id,l.BrojLicence, l.NazivInstitucije,(UpravnikBasic)vratiZaposlenog(maticniUpravnika), l.DatumSticanja);
                }
                s.Close();

            }
            catch (Exception e)
            {
            }
            return lic;

        }

        public static List<VidObrazovanjaPregled> prikaziSveVidoveObrazovanja(string maticniUpravnika)
        {
            List<VidObrazovanjaPregled> obrz = new List<VidObrazovanjaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<VidObrazovanja> sviVidoviObrazovanja = from vo in s.Query<VidObrazovanja>() select vo;

                foreach (VidObrazovanja vo in sviVidoviObrazovanja)
                {
                    if (vo.Upravnik.MaticniBroj == maticniUpravnika)
                    {
                        obrz.Add(new VidObrazovanjaPregled(vo.Id,vo.DatumSticanjaDiplome, vo.Zvanje, vo.NazivInstitucije));
                    }
                }
                s.Close();

            }
            catch (Exception e)
            {
            }
            return obrz;
        }

        public static void obrisiVidObrazovnja(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VidObrazovanja r = s.Load<VidObrazovanja>(id);

                s.Delete(r);
                s.Flush();

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void izmeniVidObrazovanja(VidObrazovanjaPregled vp,int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                VidObrazovanja vidObr = s.Load<VidObrazovanja>(id);

                vidObr.DatumSticanjaDiplome =vp.DatumSticanjaDiplome;
                vidObr.Zvanje = vp.Zvanje;
                vidObr.NazivInstitucije = vp.NazivInstitucije;

                s.Update(vidObr);
                s.Flush();

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static VidObrazovanjaPregled vratiVidObrazovanja(int id)
        {
            VidObrazovanjaPregled vidObr=null;
            try
            {
                ISession s = DataLayer.GetSession();

                VidObrazovanja vp = s.Load<VidObrazovanja>(id);
                vidObr = new VidObrazovanjaPregled(vp.Id, vp.DatumSticanjaDiplome, vp.Zvanje, vp.NazivInstitucije);
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return vidObr;
        }
    }
 
}
