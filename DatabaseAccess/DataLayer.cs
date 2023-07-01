using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpravnikProjekat.Mapiranja;

namespace DatabaseAccess
{
    class DataLayer
    {
        private static ISessionFactory _factory = null;
        private static object _lock = new object();
        public static ISession GetSession()
        {
            if (_factory == null)
            {
                lock (_lock)
                {
                    if (_factory == null)
                    {
                        // _factory = new NHibernate.Cfg.Configuration().Configure().BuildSessionFactory();
                        _factory = CreateSessionFactory();
                    }
                }
            }
            return _factory.OpenSession();
        }
        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var cfg = OracleManagedDataClientConfiguration.Oracle10
                    .ConnectionString(c =>
                    c.Is("DATA SOURCE=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;PERSIST SECURITY INFO=True;USER ID=S18263;Password=23410"));
                    //c.Is("DATA SOURCE=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;PERSIST SECURITY INFO=True;USER ID=S18332;Password=m1m1c4#b4ze"));
                return Fluently.Configure()
                    .Database(cfg.ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OsobaMapiranja>())
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

        }
    }
}
