using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpravnikProjekat.Entiteti;
using UpravnikProjekat;

namespace DatabaseAccess
{
    public class DataProvider
    {
        #region StambenaZgrada
        public static List<StambenaZgradaPregled> IzlistajSveZgrade()
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
                    UpravnikPregled z = new UpravnikPregled(sZ.Upravnik.MaticniBroj, sZ.Upravnik.Ime, sZ.Upravnik.ImeRoditelja, sZ.Upravnik.Prezime,
                        sZ.Upravnik.Adresa, sZ.Upravnik.BrojTelefona, sZ.Upravnik.DatumRodjenja, sZ.Upravnik.BrojLicneKarte, sZ.Upravnik.MestoIzdavanjaLK, sZ.Upravnik.ImaFunkcijuUpravnika);
                    if (sZ.Ugovor != null)
                        u = new UgovorPregled(sZ.Ugovor.Sifra, sZ.Ugovor.DatumPotpisivanja, sZ.Ugovor.PeriodVazenja);
                    else
                        u = new UgovorPregled("Nema ugovora", DateTime.Now, 0);
                    zgrade.Add(new StambenaZgradaPregled(sZ.Id, z, u));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }

            return zgrade;
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
                throw;
            }
        }
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
                throw;
            }
        }
       
        public static int IzmeniUpravnikaZgrade(int idZgrade, string idUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada z = s.Load<StambenaZgrada>(idZgrade);
                Zaposleni u = s.Load<Zaposleni>(idUpravnika);
                if (z == null || u == null)
                    throw new Exception("Ne postoji zgrada ili upravnik");
                z.Upravnik = u;
                u.ZgradeZaUpravljanje.Add(z);
                s.SaveOrUpdate(z);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion StambenaZgrada
        #region Ugovor
        public static int DodajUgovor(UgovorPregled u, int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ugovor ugovor = new Ugovor()
                {
                    Sifra = u.Sifra,
                    DatumPotpisivanja = u.DatumPotpisivanja,
                    PeriodVazenja = u.PeriodVazenja

                };
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                if(zgrada.Ugovor!= null)
                    throw new Exception("Zgrada vec ima ugovor");
                zgrada.Ugovor = ugovor;
                ugovor.Zgrada = zgrada;
                s.Save(ugovor);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int ObrisiUgovor(int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada z = s.Load<StambenaZgrada>(idZgrade);
                Ugovor u= s.Load<Ugovor>(z.Ugovor.Sifra);
                if (u == null)
                    throw new Exception("Zgrada nema ugovor");
                z.Ugovor = null;
                s.Delete(u);
                
                s.SaveOrUpdate(z);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static List<UgovorPregled> IzlistajSveUgovore()
        {
            List<UgovorPregled> ugovori = new List<UgovorPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Ugovor> sviUgovori = from u in s.Query<Ugovor>()
                                                 select u;
                foreach (Ugovor u in sviUgovori)
                {
                    ugovori.Add(new UgovorPregled(u.Sifra,u.DatumPotpisivanja,u.PeriodVazenja));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return ugovori;
        }

        public static int IzmeniUgovor(UgovorPregled u,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada z = s.Load<StambenaZgrada>(idZgrade);
                z.Ugovor.Sifra =u.Sifra;
                z.Ugovor.DatumPotpisivanja = u.DatumPotpisivanja;
                z.Ugovor.PeriodVazenja = u.PeriodVazenja;
                s.SaveOrUpdate(z);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Ugovor
        #region Stan
        public static List<StanBasic> IzlistajSveStanoveZgrade(int idZgrade)
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
                                                                                  where st.Stan.Id == stan.Id
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
                throw;
            }

            return stanovi;
        }
        public static List<StanBasic> IzlistajSveStanove()
        {
            List<StanBasic> stanovi = new List<StanBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<UpravnikProjekat.Entiteti.Stan> sviStanovi = from st in s.Query<Stan>()
                                                                         select st;
              

                foreach (Stan stan in sviStanovi)
                {
                    List<ImenaStanaraPregled> imena=new List<ImenaStanaraPregled>();
                    IEnumerable<UpravnikProjekat.Entiteti.ImenaStanara> stanari = from st in s.Query<ImenaStanara>()
                                                                                  where st.Stan.Id == stan.Id
                                                                                  select st;
                    foreach (ImenaStanara stanar in stanari)
                    {
                        imena.Add(new ImenaStanaraPregled(stanar.Id,stanar.Ime,stanar.Prezime));
                    }
                    VlasnikStanaBasic vb = new VlasnikStanaBasic(stan.Vlasnik.MaticniBroj, stan.Vlasnik.Ime, stan.Vlasnik.ImeRoditelja, stan.Vlasnik.Prezime, stan.Vlasnik.Adresa, stan.Vlasnik.BrojTelefona, stan.Vlasnik.Funkcija);
                    VisiSpratBasic vsb = new VisiSpratBasic(stan.Id, stan.BrojSprat.BrojSprata, "VISI",new StambenaZgradaBasic(stan.ZgradaId.Id,null,null));
                    stanovi.Add(new StanBasic(stan.Id, stan.RedniBroj, null, vb, vsb,imena));
                }

                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }

            return stanovi;
        }
        public static int DodajStan(int idZgrade, string maticniVlasnika, int spratId, int rbr)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                VlasnikStana vlasnik = s.Load<VlasnikStana>(maticniVlasnika);
                VisiSprat sprat = s.Load<VisiSprat>(spratId);
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
                s.Save(sprat);
                //s.Save(stan);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int ObrisiStan(int idZgrade, int idStana)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                Stan stan = s.Load<Stan>(idStana);
                VlasnikStana vlasnik = s.Load<VlasnikStana>(stan.Vlasnik.MaticniBroj);
                VisiSprat sprat = s.Load<VisiSprat>(stan.BrojSprat.Id);
                zgrada.Stanovi.Remove(stan);
                vlasnik.Stanovi.Remove(stan);
                sprat.Stanovi.Remove(stan);
                s.Delete(stan);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        public static int IzmeniVlasnikaStana(int idStana,string idVlasnikaNovog)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Stan stan = s.Load<Stan>(idStana);
                VlasnikStana vlasnik = s.Load<VlasnikStana>(idVlasnikaNovog);
                stan.Vlasnik.Stanovi.Remove(stan);
                stan.Vlasnik = vlasnik;
                vlasnik.Stanovi.Add(stan);
                s.SaveOrUpdate(stan);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }


        #endregion Stan
        #region ImenaStanara
        public static List<ImenaStanaraPregled> IzlistajImenaStanara(int idStana)
        {
            List<ImenaStanaraPregled> imena = new List<ImenaStanaraPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ImenaStanara> stanari = from st in s.Query<ImenaStanara>()
                                                                              where st.Stan.Id == idStana
                                                                              select st;
                foreach (ImenaStanara stanar in stanari)
                {
                    imena.Add(new ImenaStanaraPregled(stanar.Id, stanar.Ime, stanar.Prezime));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return imena;
        }
        public static List<ImenaStanaraPregled> IzlistajImenaStanaraZgrade(int idZgrade)
        {
            List<ImenaStanaraPregled> imena = new List<ImenaStanaraPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ImenaStanara> stanari = from st in s.Query<ImenaStanara>()
                                                                              where st.Stan.ZgradaId.Id == idZgrade
                                                                              select st;
                foreach (ImenaStanara stanar in stanari)
                {
                    imena.Add(new ImenaStanaraPregled(stanar.Id, stanar.Ime, stanar.Prezime));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return imena;
        }
        public static int DodajImeStanara(int idStana, string ime, string prezime)
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
                stan.Stanari.Add(stanar);
                s.Save(stan);
                s.Save(stanar);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int ObrisiImeStanara(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ImenaStanara stanar = s.Load<ImenaStanara>(id);
                Stan stan = s.Load<Stan>(stanar.Stan.Id);
                stan.Stanari.Remove(stanar);
                s.Delete(stanar);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int IzmeniImeStanara(ImenaStanaraPregled st)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ImenaStanara stanar = s.Load<ImenaStanara>(st.Id);
                stanar.Ime = st.Ime;
                stanar.Prezime = st.Prezime;
                s.SaveOrUpdate(stanar);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion ImenaStanara
        #region Ulaz
        public static List<UlazPregled> IzlistajUlaze(int idZgrade)
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
                    ulazi.Add(new UlazPregled(u.Id, u.RedniBroj, u.VremeKadaJeOtvoren, u.InstaliranaKamera));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return ulazi;
        }
        public static int DodajUlaz(UlazPregled u,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                Ulaz ulaz = new Ulaz()
                {
                    RedniBroj = u.RedniBroj,
                    VremeKadaJeOtvoren = u.VremeKadaJeOtvoren,
                    InstaliranaKamera = u.InstaliranaKamera,
                    Zgrada = zgrada
                };
                zgrada.Ulazi.Add(ulaz);
                s.Save(zgrada);
                s.Save(ulaz);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int IzmeniUlaz(UlazPregled u)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ulaz ulaz = s.Load<Ulaz>(u.Id);
                ulaz.RedniBroj = u.RedniBroj;
                ulaz.VremeKadaJeOtvoren = u.VremeKadaJeOtvoren;
                ulaz.InstaliranaKamera = u.InstaliranaKamera;
                s.SaveOrUpdate(ulaz);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static int ObrisiUlaz(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Ulaz ulaz = s.Load<Ulaz>(id);
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(ulaz.Zgrada.Id);
                zgrada.Ulazi.Remove(ulaz);
                s.Delete(ulaz);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }

        }
        #endregion Ulaz
        #region Lift
        public static List<LiftTeretniPregled> IzlistajTeretneLiftove(int idZgrade)
        {
            List<LiftTeretniPregled> liftovi = new List<LiftTeretniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<TeretniLift> sviLiftovi = from z in s.Query<TeretniLift>()
                                                      where z.Zgrada.Id == idZgrade
                                                      select z;
                foreach (TeretniLift l in sviLiftovi)
                {
                    liftovi.Add(new LiftTeretniPregled(l.SerijskiBroj,l.NazivProizvodjaca,l.DatumServisiranja,l.BrojDanaVanUpotrebe,l.DatumPoslenjegKvara,"TERETNI",l.Nosivost));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return liftovi;
        }
        public static List<LiftZaPrevozLjudiPregled> IzlistajLiftoveZaPrevozLjudi(int idZgrade)
        {
            List<LiftZaPrevozLjudiPregled> liftovi = new List<LiftZaPrevozLjudiPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<LiftZaPrevozLjudi> sviLiftovi = from z in s.Query<LiftZaPrevozLjudi>()
                                                       where z.Zgrada.Id == idZgrade
                                                       select z;
                foreach (LiftZaPrevozLjudi l in sviLiftovi)
                {
                    liftovi.Add(new LiftZaPrevozLjudiPregled(l.SerijskiBroj, l.NazivProizvodjaca, l.DatumServisiranja, l.BrojDanaVanUpotrebe, l.DatumPoslenjegKvara, "PUTNICKI", l.MaksimalanBrojOsoba));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return liftovi;
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
                throw;
            }
        }
        public static void IzmeniLiftZaPrevozLjudi(LiftZaPrevozLjudiPregled lift)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                LiftZaPrevozLjudi l = s.Load<LiftZaPrevozLjudi>(lift.SerijskiBroj);
                l.NazivProizvodjaca = lift.NazivProizvodjaca;
                l.DatumServisiranja = lift.DatumServisiranja;
                l.BrojDanaVanUpotrebe = lift.BrojDanaVanUpotrebe;
                l.DatumPoslenjegKvara = lift.DatumPoslenjegKvara;
                l.MaksimalanBrojOsoba = lift.MaksimalanBrojOsoba;
                s.SaveOrUpdate(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniTeretniLift(LiftTeretniPregled lift)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                TeretniLift l = s.Load<TeretniLift>(lift.SerijskiBroj);
                l.NazivProizvodjaca = lift.NazivProizvodjaca;
                l.DatumServisiranja = lift.DatumServisiranja;
                l.BrojDanaVanUpotrebe = lift.BrojDanaVanUpotrebe;
                l.DatumPoslenjegKvara = lift.DatumPoslenjegKvara;
                l.Nosivost = lift.Nosivost;
                s.SaveOrUpdate(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajTeretniLift(LiftTeretniPregled lift,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                TeretniLift l = new TeretniLift();
                l.SerijskiBroj = lift.SerijskiBroj;
                l.NazivProizvodjaca = lift.NazivProizvodjaca;
                l.DatumServisiranja = lift.DatumServisiranja;
                l.BrojDanaVanUpotrebe = lift.BrojDanaVanUpotrebe;
                l.DatumPoslenjegKvara = lift.DatumPoslenjegKvara;
                l.Nosivost = lift.Nosivost;
                l.TipLifta = "Teretni";
                l.Zgrada = zgrada;
                zgrada.Liftovi.Add(l);
                s.Save(zgrada);
                //s.SaveOrUpdate(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }               
        }
        public static void DodajLiftZaPrevozLjudi(LiftZaPrevozLjudiPregled lift,int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada zgrada = s.Load<StambenaZgrada>(idZgrade);
                LiftZaPrevozLjudi l = new LiftZaPrevozLjudi();
                l.SerijskiBroj = lift.SerijskiBroj;
                l.NazivProizvodjaca = lift.NazivProizvodjaca;
                l.DatumServisiranja = lift.DatumServisiranja;
                l.BrojDanaVanUpotrebe = lift.BrojDanaVanUpotrebe;
                l.DatumPoslenjegKvara = lift.DatumPoslenjegKvara;
                l.MaksimalanBrojOsoba = lift.MaksimalanBrojOsoba;
                l.TipLifta = "Lift za prevoz ljudi";
                l.Zgrada = zgrada;
                zgrada.Liftovi.Add(l);
                // s.SaveOrUpdate(l);
                s.Save(zgrada);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Lift
        #region Sprat
        public static List<SpratPregled> IzlistajSpratove(int idZgrade)
        {
            List<SpratPregled> spratovi = new List<SpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Sprat> sviSpratovi = from z in s.Query<Sprat>()
                                                                           where z.Zgrada.Id == idZgrade
                                                                           select z;
                foreach (Sprat sp in sviSpratovi)
                {
                    spratovi.Add(new SpratPregled(sp.Id, sp.BrojSprata, sp.VrstaSprata));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return spratovi;
        }
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
                    spratovi.Add(new PodzemniSpratPregled(sp.Id, sp.BrojSprata, "PODZEMNI"));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
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
                    spratovi.Add(new NiziSpratPregled(sp.Id, sp.BrojSprata, "NIZI"));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return spratovi;
        }
        public static List<SpratPregled> IzlistajSveViseSpratove(int idZgrade)
        {
            List<SpratPregled> spratovi = new List<SpratPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.VisiSprat> sviSpratovi = from z in s.Query<VisiSprat>()
                                                                               where z.Zgrada.Id == idZgrade
                                                                               select z;
                foreach (VisiSprat sp in sviSpratovi)
                {
                    spratovi.Add(new SpratPregled(sp.Id, sp.BrojSprata, sp.VrstaSprata));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return spratovi;
        }
        public static int DodajSprat( SpratPregled spratNovi, int idZgrade)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                StambenaZgrada sz = s.Load<StambenaZgrada>(idZgrade);
                if (spratNovi.VrstaSprata == "VISI")
                {
                    VisiSprat sprat = new VisiSprat()
                    {
                        BrojSprata = spratNovi.BrojSprata,
                        VrstaSprata = spratNovi.VrstaSprata,
                        Zgrada = sz
                    };
                    sz.Spratovi.Add(sprat);
                }
                else if (spratNovi.VrstaSprata == "NIZI")
                {
                    NiziSprat sprat = new NiziSprat()
                    {
                        BrojSprata = spratNovi.BrojSprata,
                        VrstaSprata = spratNovi.VrstaSprata,
                        Zgrada = sz
                    };
                    sz.Spratovi.Add(sprat);
                }
                else
                {
                    PodzemniSprat podzemniSprat = new PodzemniSprat()
                    {
                        BrojSprata = spratNovi.BrojSprata,
                        VrstaSprata = spratNovi.VrstaSprata,
                        Zgrada = sz
                    };
                    sz.Spratovi.Add(podzemniSprat);
                }

                s.Save(sz);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void ObrisiSprat(int idSprata)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Sprat sprat = s.Load<Sprat>(idSprata);
                StambenaZgrada sz = s.Load<StambenaZgrada>(sprat.Zgrada.Id);
                sz.Spratovi.Remove(sprat);
                s.Delete(sprat);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Sprat
        #region Lokal
        public static List<LokalPregled> IzlistajSveLokale(int idZgrade)
        {
            List<LokalPregled> lokali = new List<LokalPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Lokal> sviLokali = from z in s.Query<Lokal>()
                                                                         where z.Zgrada.Id == idZgrade
                                                                         select z;
                foreach (Lokal l in sviLokali)
                {
                    lokali.Add(new LokalPregled(l.Id, l.RedniBroj, l.ImeFirme, new NiziSpratPregled(l.Sprat.Id,l.Sprat.BrojSprata,"NIZI")));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return lokali;
        }
        public static int DodajLokal(LokalPregled l)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                NiziSprat sprat = s.Load<NiziSprat>(l.Sprat.Id);
                StambenaZgrada zgrada = sprat.Zgrada;
                Lokal lokal = new Lokal()
                {
                    RedniBroj = l.RedniBroj,
                    ImeFirme = l.ImeFirme,
                    Sprat = sprat,
                    Zgrada = zgrada
                };

                zgrada.Lokali.Add(lokal);
                s.Save(zgrada);
                s.Flush();
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                throw;
            }
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
                throw;
            }
        }
        public static void IzmeniLokal(LokalPregled l)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Lokal lokal = s.Load<Lokal>(l.Id);
                lokal.RedniBroj = l.RedniBroj;
                lokal.ImeFirme = l.ImeFirme;
                s.Update(lokal);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Lokal
        #region Parking Mesto
        public static List<ParkingMestoPregled> IzlistajSvaParkingMesta(int idZgrade)
        {
            List<ParkingMestoPregled> parkingMesta = new List<ParkingMestoPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ParkingMesto> svaParkingMesta = from z in s.Query<ParkingMesto>()
                                                                                      where z.Zgrada.Id == idZgrade
                                                                                      select z;
                foreach (ParkingMesto pm in svaParkingMesta)
                {
                    parkingMesta.Add(new ParkingMestoPregled(pm.Id, pm.RedniBroj, pm.RegistarskiBrojRezervisanog, new PodzemniSpratPregled(pm.Sprat.Id,pm.Sprat.BrojSprata,"PODZEMNI")));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return parkingMesta;
        }
        public static List<ParkingMestoPregled> IzlistajSvaNerezervisanaParkingMesta(int idZgrade)
        {
            List<ParkingMestoPregled> parkingMesta = new List<ParkingMestoPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.ParkingMesto> svaParkingMesta = from z in s.Query<ParkingMesto>()
                                                                                      where z.Zgrada.Id == idZgrade &&
                                                                                        z.RegistarskiBrojRezervisanog == null
                                                                                      select z;
                foreach (ParkingMesto pm in svaParkingMesta)
                {
                    parkingMesta.Add(new ParkingMestoPregled(pm.Id, pm.RedniBroj, pm.RegistarskiBrojRezervisanog, new PodzemniSpratPregled(pm.Sprat.Id, pm.Sprat.BrojSprata, "PODZEMNI")));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return parkingMesta;
        }
        public static void DodajParkingMesto(ParkingMestoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                PodzemniSprat sprat = s.Load<PodzemniSprat>(p.Sprat.Id);
                StambenaZgrada zgrada = sprat.Zgrada;
                ParkingMesto parkingMesto = new ParkingMesto()
                {
                    RedniBroj =p.RedniBroj,
                    RegistarskiBrojRezervisanog = p.RegistarskiBrojRezervisanog,
                    Sprat = sprat,
                    Zgrada = zgrada
                };
                zgrada.ParkingMesta.Add(parkingMesto);
                sprat.ParkingMesta.Add(parkingMesto);
                s.Save(zgrada);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniParkingMesto(ParkingMestoPregled p)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ParkingMesto pm = s.Load<ParkingMesto>(p.Id);
                pm.RegistarskiBrojRezervisanog = p.RegistarskiBrojRezervisanog;
                s.Save(pm);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void ObrisiParkingMesto(int idParkingMesta)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                ParkingMesto pm = s.Load<ParkingMesto>(idParkingMesta);
                s.Delete(pm);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Parking Mesto
        #region Licenca
        public static LicencaPregled PreuzmiLicencu(string idUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni u = s.Load<Zaposleni>(idUpravnika);
                if(u.Licenca==null)
                    throw new Exception("Upravnik nema licencu!");
                LicencaPregled licenca = new LicencaPregled(u.Licenca.Id,u.Licenca.BrojLicence,u.Licenca.NazivInstitucije,u.Licenca.DatumSticanja);
                s.Close();
                return licenca;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void ObrisiLicencu(int idLicence)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Licenca l = s.Load<Licenca>(idLicence);
                s.Delete(l);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniLicencu(LicencaPregled l)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Licenca licenca = s.Load<Licenca>(l.Id);
                licenca.BrojLicence = l.BrojLicence;
                licenca.NazivInstitucije = l.NazivInstitucije;
                licenca.DatumSticanja = l.DatumSticanja;
                s.Update(licenca);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajLicencu(LicencaPregled l, string idUpravnika)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni u = s.Load<Zaposleni>(idUpravnika);
                if(u.Licenca != null)
                {
                    throw new Exception("Upravnik vec ima licencu!");
                }
                Licenca licenca = new Licenca()
                {
                    BrojLicence = l.BrojLicence,
                    NazivInstitucije = l.NazivInstitucije,
                    DatumSticanja = l.DatumSticanja,
                    Upravnik = u
                };
                u.Licenca = licenca;
                s.Save(licenca);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Licenca
        #region Zaposleni
        public static List<ZaposleniPregled> IzlistajZaposlene()
        {
            List<ZaposleniPregled> zaposleni = new List<ZaposleniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Zaposleni> sviZaposleni = from z in s.Query<Zaposleni>()
                                                                                select z;
                foreach (Zaposleni z in sviZaposleni)
                {
                    zaposleni.Add(new ZaposleniPregled(z.MaticniBroj,z.Ime,z.ImeRoditelja,z.Prezime,z.Adresa,z.BrojTelefona,z.DatumRodjenja,z.BrojLicneKarte,z.MestoIzdavanjaLK,z.ImaFunkcijuUpravnika));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return zaposleni;
        }
        public static ZaposleniPregled PreuzmiZaposlenog(string id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni z = s.Load<Zaposleni>(id);
                ZaposleniPregled zaposleni = new ZaposleniPregled(z.MaticniBroj, z.Ime, z.ImeRoditelja, z.Prezime, z.Adresa, z.BrojTelefona, z.DatumRodjenja, z.BrojLicneKarte, z.MestoIzdavanjaLK, z.ImaFunkcijuUpravnika);
                s.Close();
                return zaposleni;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static List<ZaposleniPregled>
            IzlistajUpravnike()
        {
            List<ZaposleniPregled> upravnici = new List<ZaposleniPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.Zaposleni> sviUpravnici = from u in s.Query<Zaposleni>()
                                                                                where u.ImaFunkcijuUpravnika == "TRUE"
                                                                                select u;
                foreach (Zaposleni u in sviUpravnici)
                {
                    upravnici.Add(new UpravnikPregled(u.MaticniBroj,u.Ime,u.ImeRoditelja,u.Prezime,u.Adresa,u.BrojTelefona,u.DatumRodjenja,u.BrojLicneKarte,u.MestoIzdavanjaLK,u.ImaFunkcijuUpravnika));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return upravnici;
        }
        public static void ObrisiZaposlenog(string id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni z = s.Load<Zaposleni>(id);
                s.Delete(z);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniZaposlenog(ZaposleniPregled z)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni zaposleni = s.Load<Zaposleni>(z.MaticniBroj);
                zaposleni.Ime = z.Ime;
                zaposleni.ImeRoditelja = z.ImeRoditelja;
                zaposleni.Prezime = z.Prezime;
                zaposleni.Adresa = z.Adresa;
                zaposleni.BrojTelefona = z.BrojTelefona;
                zaposleni.DatumRodjenja = z.DatumRodjenja;
                zaposleni.BrojLicneKarte = z.BrojLicneKarte;
                zaposleni.MestoIzdavanjaLK = z.MestoIzdavanjaLK;
                zaposleni.ImaFunkcijuUpravnika = z.ImaFunkcijuUpravnika;
                s.Update(zaposleni);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajZaposlenog(ZaposleniPregled z)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni zaposleni = new Zaposleni()
                {
                    MaticniBroj = z.MaticniBroj,
                    Ime = z.Ime,
                    ImeRoditelja = z.ImeRoditelja,
                    Prezime = z.Prezime,
                    Adresa = z.Adresa,
                    BrojTelefona = z.BrojTelefona,
                    DatumRodjenja = z.DatumRodjenja,
                    BrojLicneKarte = z.BrojLicneKarte,
                    MestoIzdavanjaLK = z.MestoIzdavanjaLK,
                    ImaFunkcijuUpravnika = z.ImaFunkcijuUpravnika
                };
                s.Save(zaposleni);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        #endregion Zaposleni
        #region VlasnikStana
        public static List<VlasnikStanaPregled> IzlistajVlasnike()
        {
            List<VlasnikStanaPregled> vlasnici = new List<VlasnikStanaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.VlasnikStana> sviVlasnici = from v in s.Query<VlasnikStana>()
                                                                                select v;
                foreach (VlasnikStana v in sviVlasnici)
                {
                    vlasnici.Add(new VlasnikStanaPregled(v.MaticniBroj,v.Ime,v.ImeRoditelja,v.Prezime,v.Adresa,v.BrojTelefona,v.Funkcija));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return vlasnici;
        }
        public static VlasnikStanaPregled PreuzmiVlasnika(string id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VlasnikStana v = s.Load<VlasnikStana>(id);
                VlasnikStanaPregled vlasnik = new VlasnikStanaPregled(v.MaticniBroj, v.Ime, v.ImeRoditelja, v.Prezime, v.Adresa, v.BrojTelefona, v.Funkcija);
                s.Close();
                return vlasnik;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void ObrisiVlasnika(string id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VlasnikStana v = s.Load<VlasnikStana>(id);
                s.Delete(v);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniVlasnika(VlasnikStanaPregled v)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VlasnikStana vlasnik = s.Load<VlasnikStana>(v.MaticniBroj);
                vlasnik.Ime = v.Ime;
                vlasnik.ImeRoditelja = v.ImeRoditelja;
                vlasnik.Prezime = v.Prezime;
                vlasnik.Adresa = v.Adresa;
                vlasnik.BrojTelefona = v.BrojTelefona;
                vlasnik.Funkcija = v.Funkcija;
                s.Update(vlasnik);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajVlasnika(VlasnikStanaPregled v)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VlasnikStana vlasnik = new VlasnikStana()
                {
                    MaticniBroj = v.MaticniBroj,
                    Ime = v.Ime,
                    ImeRoditelja = v.ImeRoditelja,
                    Prezime = v.Prezime,
                    Adresa = v.Adresa,
                    BrojTelefona = v.BrojTelefona,
                    Funkcija = v.Funkcija
                };
                s.Save(vlasnik);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        #endregion VlasnikStana
        #region VidObrazovanja
        public static List<VidObrazovanjaPregled> VratiVidoveObrazovanja(string id)
        {
            List<VidObrazovanjaPregled> vidovi = new List<VidObrazovanjaPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<UpravnikProjekat.Entiteti.VidObrazovanja> sviVidovi = from v in s.Query<VidObrazovanja>()
                                                                                  where v.Upravnik.MaticniBroj == id
                                                                                  select v;
                foreach (VidObrazovanja v in sviVidovi)
                {
                    vidovi.Add(new VidObrazovanjaPregled(v.Id,v.DatumSticanjaDiplome,v.Zvanje,v.NazivInstitucije));
                }
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
            return vidovi;
        }
        public static VidObrazovanjaPregled PreuzmiVidObrazovanja(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VidObrazovanja v = s.Load<VidObrazovanja>(id);
                VidObrazovanjaPregled vid = new VidObrazovanjaPregled(v.Id, v.DatumSticanjaDiplome, v.Zvanje, v.NazivInstitucije);
                s.Close();
                return vid;
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void ObrisiVidObrazovanja(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VidObrazovanja v = s.Load<VidObrazovanja>(id);
                s.Delete(v);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void IzmeniVidObrazovanja(VidObrazovanjaPregled v)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                VidObrazovanja vid = s.Load<VidObrazovanja>(v.Id);
                vid.DatumSticanjaDiplome = v.DatumSticanjaDiplome;
                vid.Zvanje = v.Zvanje;
                vid.NazivInstitucije = v.NazivInstitucije;
                s.Update(vid);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }
        public static void DodajVidObrazovanja(VidObrazovanjaPregled v, string idZaposleni)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Zaposleni z = s.Load<Zaposleni>(idZaposleni);
                VidObrazovanja vid = new VidObrazovanja()
                {
                    DatumSticanjaDiplome = v.DatumSticanjaDiplome,
                    Zvanje = v.Zvanje,
                    NazivInstitucije = v.NazivInstitucije,
                    Upravnik=z
                };
              
                z.Obrazovanje.Add(vid);
                s.Save(z);
                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                throw;
            }
        }

      
        #endregion VidObrazovanja
    }
}
