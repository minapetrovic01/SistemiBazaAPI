using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using UpravnikProjekat.Entiteti;
using NHibernate;
using NHibernate.Mapping;
using DatabaseAccess;

namespace UpravnikProjekat
{
    public class DTOManagerM
    {
        #region StambenaZgrada

        public static int ObrisiZgradu(int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada z = s.Load<StambenaZgrada>(idZgrade);
                s.Delete(z);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return 0;
            }
        }

        public static int IzmeniUpravnikaZgrade(int idZgrade, string idUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada z = s.Load<StambenaZgrada>(idZgrade);
                Zaposleni u = s.Load<Zaposleni>(idUpravnika);
                //Upravnik prethodni=s.Load<Upravnik>(z.Upravnik.MaticniBroj);
               // prethodni.ZgradeZaUpravljanje.Remove(z);
                z.Upravnik = u;
                u.ZgradeZaUpravljanje.Add(z);
                s.SaveOrUpdate(z);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return 0;
            }
        }
        public static List<StambenaZgradaPregled> izlistajSveZgrade()
        {
            List<StambenaZgradaPregled> zgrade = new List<StambenaZgradaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.StambenaZgrada> sveZgrade = from z in s.Query<StambenaZgrada>()
                                                                            select z;

                foreach (StambenaZgrada sZ in sveZgrade)
                {
                    UgovorPregled u;
                    //UpravnikBasic z = new UpravnikBasic(sZ.Upravnik.)
                    UpravnikPregled z=new UpravnikPregled(sZ.Upravnik.MaticniBroj,sZ.Upravnik.Ime,sZ.Upravnik.ImeRoditelja,sZ.Upravnik.Prezime,
                        sZ.Upravnik.Adresa,sZ.Upravnik.BrojTelefona,sZ.Upravnik.DatumRodjenja,sZ.Upravnik.BrojLicneKarte,sZ.Upravnik.MestoIzdavanjaLK,sZ.Upravnik.ImaFunkcijuUpravnika);
                    if(sZ.Ugovor!=null)
                         u = new UgovorPregled(sZ.Ugovor.Sifra, sZ.Ugovor.DatumPotpisivanja, sZ.Ugovor.PeriodVazenja);
                    else
                         u = new UgovorPregled("Nema ugovora", DateTime.Now, 0);
                    zgrade.Add(new StambenaZgradaPregled(sZ.Id,z,u));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return zgrade;
        }
        #endregion
        # region Upravnik
        public static List<UpravnikPregled> izlistajSveUpravnike()
        {
            List<UpravnikPregled> upravnici = new List<UpravnikPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.Zaposleni> sviUpravnici = from u in s.Query<Zaposleni>()
                                                                                where u.ImaFunkcijuUpravnika == "TRUE"
                                                                                  select u;

                foreach (Zaposleni za in sviUpravnici)
                {
                    
                    UpravnikPregled z = new UpravnikPregled(za.MaticniBroj, za.Ime, za.ImeRoditelja, za.Prezime,
                        za.Adresa, za.BrojTelefona, za.DatumRodjenja, za.BrojLicneKarte, za.MestoIzdavanjaLK, za.ImaFunkcijuUpravnika);
                    //UgovorPregled u = new UgovorPregled(sZ.Ugovor.Sifra, sZ.Ugovor.DatumPotpisivanja, sZ.Ugovor.PeriodVazenja);
                    upravnici.Add(z);
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return upravnici;
        }
        public static int DodajZgradu(string idUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                UpravnikProjekat.Entiteti.Zaposleni upravnik = s.Load<UpravnikProjekat.Entiteti.Zaposleni>(idUpravnika);
                StambenaZgrada zgrada = new StambenaZgrada()
                {
                    Upravnik = upravnik
                };
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return zgrada.Id;
            }
            catch (Exception ec)
            {
                //handle exceptions
                return -1;
            }
        }

        #endregion
        #region Ugovor
        public static int DodajUgovor(string sifra, DateTime datumPotpisivanja, int periodVazenja,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ugovor ugovor = new Ugovor()
                {
                    Sifra = sifra,
                    DatumPotpisivanja = datumPotpisivanja,
                    PeriodVazenja = periodVazenja
                    
                };
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                zgrada.Ugovor = ugovor;
                ugovor.Zgrada = zgrada;
               // s.Save(zgrada);
                s.Save(ugovor);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                //handle exceptions
                return -1;
            }
        }
        #endregion
        #region Stan

        public static int DodajStan(int idZgrade, string maticniVlasnika, int spratId, int rbr)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                VlasnikStana vlasnik = s.Load<VlasnikStana>(maticniVlasnika);
                VisiSprat sprat=s.Load<VisiSprat>(spratId);
                Stan stan = new Stan()
                {
                    BrojSprat = sprat,
                    RedniBroj = rbr,
                    ZgradaId = zgrada,
                    Vlasnik = vlasnik
                };
                zgrada.Stanovi.Add(stan);
                vlasnik.Stanovi.Add(stan);
                sprat.Stanovi.Add(stan);

                s.Save(zgrada);
                s.Save(vlasnik);
                s.Save(stan);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }
        public static List<StanBasic> IzlistajSveStanove(int idZgrade)
        {
            List<StanBasic> stanovi = new List<StanBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.Stan> sviStanovi = from st in s.Query<Stan>()
                                                                         where st.ZgradaId.Id == idZgrade
                                                                         select st;

                foreach (Stan stan in sviStanovi)
                {
                    List<ImenaStanaraPregled> imena = new List<ImenaStanaraPregled>();
                    IEnumerable<UpravnikProjekat.Entiteti.ImenaStanara> stanari = from st in s.Query<ImenaStanara>()
                                                                                  where st.Stan.Id == st.Id
                                                                                  select st;
                    foreach (ImenaStanara stanar in stanari)
                    {
                        imena.Add(new ImenaStanaraPregled(stanar.Id, stanar.Ime, stanar.Prezime));
                    }
                    VlasnikStanaBasic vb = new VlasnikStanaBasic(stan.Vlasnik.MaticniBroj, stan.Vlasnik.Ime, stan.Vlasnik.ImeRoditelja, stan.Vlasnik.Prezime, stan.Vlasnik.Adresa, stan.Vlasnik.BrojTelefona, stan.Vlasnik.Funkcija);
                    VisiSpratBasic vsb = new VisiSpratBasic(stan.Id, stan.BrojSprat.BrojSprata, "VISI", null);
                    stanovi.Add(new StanBasic(stan.Id, stan.RedniBroj, null, vb, vsb, imena));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return stanovi;
        }

        #endregion
        #region Stanari
        public static List<ImenaStanaraPregled> IzlistajSveStanare(int idStana)
        {
            List<ImenaStanaraPregled> stanari = new List<ImenaStanaraPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ImenaStanara> sviStanari = from st in s.Query<ImenaStanara>()
                                                                         where st.Stan.Id == idStana
                                                                         select st;
                foreach (ImenaStanara ime in sviStanari)
                {
                    stanari.Add(new ImenaStanaraPregled(ime.Id,ime.Ime,ime.Prezime));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return stanari;

        }
        public static void ObrisiStanara(int idStanara)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ImenaStanara z = s.Load<ImenaStanara>(idStanara);
                s.Delete(z);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
            }
        }
        public static void DodajStanara(int idStana, string ime, string prezime)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Stan stan = s.Load<Stan>(idStana);
                ImenaStanara stanar = new ImenaStanara()
                {
                    Ime = ime,
                    Prezime = prezime,
                    Stan = stan
                };
                s.Save(stanar);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
            }
        }
        #endregion
        #region Vlasnici

        public static int DodajVlasnikaStana(string maticniBroj, string ime, string imeRoditelja, string prezime, string adresa, string brojTelefona, string funkcija)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VlasnikStana vlasnik = new VlasnikStana()
                {
                    MaticniBroj = maticniBroj,
                    Ime = ime,
                    ImeRoditelja = imeRoditelja,
                    Prezime = prezime,
                    Adresa = adresa,
                    BrojTelefona = brojTelefona,
                    Funkcija = funkcija
                };
                s.Save(vlasnik);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }
        public static List<VlasnikStanaPregled> IzlistajSveVlasnike()
        {
            List<VlasnikStanaPregled> vlasnici = new List<VlasnikStanaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.VlasnikStana> sviVlasnici = from z in s.Query<VlasnikStana>()
                                                                                  select z;

                foreach (VlasnikStana vs in sviVlasnici)
                {
                    
                    vlasnici.Add(new VlasnikStanaPregled(vs.MaticniBroj,vs.Ime, vs.ImeRoditelja,vs.Prezime,vs.Adresa,vs.BrojTelefona,vs.Funkcija));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return vlasnici;
        }

        #endregion
        #region Sprat

        public static List<PodzemniSpratPregled> IzlistajSvePodzemneSpratove(int idZgrade)
        {
            List<PodzemniSpratPregled> spratovi = new List<PodzemniSpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.PodzemniSprat> sviSpratovi = from z in s.Query<PodzemniSprat>()
                                                                               where z.Zgrada.Id == idZgrade
                                                                               select z;
                foreach (Sprat sp in sviSpratovi)
                {
                    spratovi.Add(new PodzemniSpratPregled(sp.Id, sp.BrojSprata,"PODZEMNI"));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return spratovi;
        }
        public static List<NiziSpratPregled> IzlistajSveNizeSpratove(int idZgrade)
        {
            List<NiziSpratPregled> spratovi = new List<NiziSpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.NiziSprat> sviSpratovi = from z in s.Query<NiziSprat>()
                                                                           where z.Zgrada.Id == idZgrade
                                                                           select z;
                foreach (Sprat sp in sviSpratovi)
                {
                    spratovi.Add(new NiziSpratPregled(sp.Id, sp.BrojSprata,"NIZI"));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return spratovi;
        }

        public static List<SpratPregled> IzlistajSveSpratoveSvihTIpova(int idZgrade)
        {
            List<SpratPregled> spratovi = new List<SpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Sprat> sviSpratovi = from z in s.Query<Sprat>()
                                                                           where z.Zgrada.Id==idZgrade 
                                                                           select z;
                foreach (Sprat sp in sviSpratovi)
                {
                    // spratovi.Add(new SpratPregled(sp.Id,sp.BrojSprata,sp.VrstaSprata));
                    spratovi.Add(new SpratPregled(sp.Id, sp.BrojSprata,sp.VrstaSprata));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return spratovi;
        }

        public static List<SpratPregled> IzlistajSveSpratove(int idZgrade)
        {
            List<SpratPregled> spratovi = new List<SpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.VisiSprat> sviSpratovi = from z in s.Query<VisiSprat>()
                                                                           where z.Zgrada.Id==idZgrade 
                                                                                  select z;

                foreach (VisiSprat sp in sviSpratovi)
                {

                    spratovi.Add(new SpratPregled(sp.Id,sp.BrojSprata,sp.VrstaSprata));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }

            return spratovi;
        }
        public static int DodajSprat(int idZgrade,string tip,int broj)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada sz = s.Load<StambenaZgrada>(idZgrade);
                if (tip == "VISI")
                {
                    VisiSprat sprat = new VisiSprat()
                    {
                        BrojSprata = broj,
                        VrstaSprata = tip,
                        Zgrada = sz
                    };
                    //s.Save(sprat);
                    sz.Spratovi.Add(sprat);
                }
                else if (tip == "NIZI")
                {
                    NiziSprat sprat = new NiziSprat()
                    {
                        BrojSprata = broj,
                        VrstaSprata = tip,
                        Zgrada = sz
                    };
                    //s.Save(sprat);
                    sz.Spratovi.Add(sprat);
                }
                else 
                {
                    PodzemniSprat podzemniSprat = new PodzemniSprat()
                    {
                        BrojSprata = broj,
                        VrstaSprata = tip,
                        Zgrada = sz
                    };
                    //s.Save(podzemniSprat);
                    sz.Spratovi.Add(podzemniSprat);
                }
                
                s.Save(sz);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }
        #endregion
        #region Lokal


        public static int DodajLokal(int idSprata,int broj,string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

               NiziSprat sprat = s.Load<NiziSprat>(idSprata);
                StambenaZgrada zgrada= sprat.Zgrada;
                Lokal lokal = new Lokal()
                {
                    RedniBroj = broj,
                    ImeFirme = ime,
                    Sprat = sprat,
                    Zgrada= zgrada
                };

              zgrada.Lokali.Add(lokal);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }
        public static List<LokalPregled> IzlistajSveLokale(int idZgrade)
        {
            List<LokalPregled> lokali = new List<LokalPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Lokal> sviLokali = from z in s.Query<Lokal>()
                                                                           where z.Zgrada.Id==idZgrade 
                                                                           select z;
                foreach (Lokal l in sviLokali)
                {
                    lokali.Add(new LokalPregled(l.Id,l.RedniBroj,l.ImeFirme,null));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return lokali;
        }
        public static void ObrisiLokal(int idLokala)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lokal l = s.Load<Lokal>(idLokala);
                s.Delete(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
            }
        }
        #endregion
        #region ParkingMesto

        public static bool DodajParkingMesto(int idZgrade,int idSprata,string redni, string registracioni)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                PodzemniSprat sprat = s.Load<PodzemniSprat>(idSprata);
                StambenaZgrada zgrada = sprat.Zgrada;
                ParkingMesto parkingMesto = new ParkingMesto()
                {
                    RedniBroj = Int32.Parse( redni),
                    RegistarskiBrojRezervisanog = registracioni,
                    Sprat = sprat,
                    Zgrada = zgrada
                };
                zgrada.ParkingMesta.Add(parkingMesto);
                sprat.ParkingMesta.Add(parkingMesto);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }
        public static List<ParkingMestoPregled> IzlistajSvaParkingMesta(int idZgrade)
        {
            List<ParkingMestoPregled> parkingMesta = new List<ParkingMestoPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ParkingMesto> svaParkingMesta = from z in s.Query<ParkingMesto>()
                                                                           where z.Zgrada.Id==idZgrade 
                                                                           select z;
                foreach (ParkingMesto pm in svaParkingMesta)
                {
                    parkingMesta.Add(new ParkingMestoPregled(pm.Id,pm.RedniBroj,pm.RegistarskiBrojRezervisanog,null));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return parkingMesta;
        }
        public static bool RezervisiParkingMesto(int idMesta,string regBr)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ParkingMesto pm = s.Load<ParkingMesto>(idMesta);
                pm.RegistarskiBrojRezervisanog = regBr;
                s.Save(pm);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        #endregion
        #region Lift

        public static bool DodajPutnickiLift(string tbSerijski, string proizvodjac, DateTime servisiran, string vanUpotrebe, DateTime kvar, string maxosobe, int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);

                LiftZaPrevozLjudi lift = new LiftZaPrevozLjudi()
                {
                    SerijskiBroj = tbSerijski,
                    NazivProizvodjaca = proizvodjac,
                    DatumServisiranja = servisiran,
                    BrojDanaVanUpotrebe = vanUpotrebe,
                    DatumPoslenjegKvara = kvar,
                    MaksimalanBrojOsoba = Int32.Parse(maxosobe),
                    Zgrada = zgrada,
                    TipLifta = "Lift za prevoz ljudi"
                };
                zgrada.Liftovi.Add(lift);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                //MessageBox.Show("Neuspelo dodavanje!");
                return false;
            }

           
        }
        public static bool DodajTeretniLift(string tbSerijski, string proizvodjac, DateTime servisiran, string vanUpotrebe, DateTime kvar, string nosivost, int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                TeretniLift lift = new TeretniLift()
                {
                    SerijskiBroj = tbSerijski,
                    NazivProizvodjaca = proizvodjac,
                    DatumServisiranja = servisiran,
                    BrojDanaVanUpotrebe = vanUpotrebe,
                    DatumPoslenjegKvara = kvar,
                    Nosivost = Int32.Parse(nosivost),
                    Zgrada = zgrada,
                    TipLifta = "Teretni"
                };
                zgrada.Liftovi.Add(lift);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
               // MessageBox.Show("Neuspelo dodavanje!");
                return false;
            }
        }

        public static bool IzmeniDatumServisiranja(string serijskiBrojLifta, DateTime newdate)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lift l = s.Load<Lift>(serijskiBrojLifta);
                l.DatumServisiranja = newdate;
                s.Save(l);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
               return false;
            }
        }
        public static bool IzmeniBrojDataUKvaru(string serijskiBrojLifta,string newDani)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lift l = s.Load<Lift>(serijskiBrojLifta);
                l.BrojDanaVanUpotrebe =newDani;
                s.Save(l);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public static bool IzmeniDatumPoslednjegKvara(string serijskiBrojLifta, DateTime newdate)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lift l = s.Load<Lift>(serijskiBrojLifta);
                l.DatumPoslenjegKvara = newdate;
                s.Save(l);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public static void ObrisiLift(string serijskiBrojLifta)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lift l = s.Load<Lift>(serijskiBrojLifta);
                s.Delete(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
            }
        }

        public static List<LiftPregled> IzlistajSveLiftove(int idZgrade)
        {
            List<LiftPregled> liftovi = new List<LiftPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Lift> sviLiftovi = from z in s.Query<Lift>()
                                                                           where z.Zgrada.Id==idZgrade 
                                                                           select z;
                foreach (Lift l in sviLiftovi)
                {
                    if(l.GetType()==typeof(TeretniLift))
                        liftovi.Add(new LiftPregled(l.SerijskiBroj,l.NazivProizvodjaca,l.DatumServisiranja,l.BrojDanaVanUpotrebe,l.DatumPoslenjegKvara,"TERETNI"));
                    else
                        liftovi.Add(new LiftPregled(l.SerijskiBroj, l.NazivProizvodjaca, l.DatumServisiranja, l.BrojDanaVanUpotrebe, l.DatumPoslenjegKvara, "PUTNICKI"));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return liftovi;
        }
        #endregion
        #region Ulaz

        public static bool DodajUlaz(string vreme,int redniBroj,bool imaKamere,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                Ulaz ulaz = new Ulaz()
                {
                    VremeKadaJeOtvoren = vreme,
                    RedniBroj = redniBroj,
                    InstaliranaKamera = imaKamere ? "TRUE" : "FALSE",
                    Zgrada = zgrada
                };
                zgrada.Ulazi.Add(ulaz);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                //MessageBox.Show("Neuspelo dodavanje!");
                return false;
            }
        }
        public static bool IzmeniVreme(int idUlaza,string vreme)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ulaz u = s.Load<Ulaz>(idUlaza);
                u.VremeKadaJeOtvoren = vreme;
                s.Save(u);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
                //MessageBox.Show("Neuspelo izmena vremena!");
                return false;
            }
        }
        public static bool InstalirajKameru(int idUlaz)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ulaz u = s.Load<Ulaz>(idUlaz);
                u.InstaliranaKamera = "TRUE";
                s.Save(u);
                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ec)
            {
               // MessageBox.Show("Neuspelo instaliranje kamere!");
                return false;
            }
        }
        public static List<UlazPregled> IzlistajSveUlaze(int idZgrade)
        {
            List<UlazPregled> ulazi = new List<UlazPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Ulaz> sviUlazi = from z in s.Query<Ulaz>()
                                             where z.Zgrada.Id == idZgrade
                                             select z;
                foreach (Ulaz u in sviUlazi)
                {
                    ulazi.Add(new UlazPregled(u.Id,u.RedniBroj,u.VremeKadaJeOtvoren,u.InstaliranaKamera));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                //handle exceptions
            }
            return ulazi;
        }

        #endregion
    }

}
