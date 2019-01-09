using System;
using System.Collections.Generic;

namespace Generator.SQLDriver
{
    public class SQLDriverFactory
    {
        static private SQLDriverFactory instance;
        private Dictionary<string, ISQLDriver> drivers;

        private SQLDriverFactory()
        {
            drivers = new Dictionary<string, ISQLDriver>();
            drivers.Add("PostgreSQL", new PostgresDriver());
            drivers.Add("MySQL", new MySqlDriver());
        }

        static public SQLDriverFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SQLDriverFactory();
            }
            return instance;
        }
        public ISQLDriver GetDriver(string type)
        {
            if (drivers.ContainsKey(type))
            {
                return drivers[type];
            }
            return null;
        }
    }
}
