using System;
using System.Collections.Generic;

namespace Generator.DatabaseMapper
{
    public class DatabaseMapperFactory
    {
        private static DatabaseMapperFactory instance;
        private Dictionary<string, DatabaseMapper> databaseMapper;

        private DatabaseMapperFactory()
        {
            databaseMapper = new Dictionary<string, DatabaseMapper>();
            databaseMapper.Add("PostgreSQL", new PostgresMapper());
            databaseMapper.Add("MySQL", new MySQLMapper());
        }

        public static DatabaseMapperFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseMapperFactory();
            }
            return instance;
        }

        public DatabaseMapper GetDatabaseMapper(string type)
        {
            if (databaseMapper.ContainsKey(type))
            {
                return databaseMapper[type];
            }
            return null;
        }
    }
}
