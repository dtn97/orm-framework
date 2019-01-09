using System;
namespace OOPDPFinalProject.ORMFramework.DBMSFactory
{
    public abstract class DBMSAbstractFactory
    {
        public static DBMSAbstractFactory GetFactoryByDBMS(DBMS name)
        {
            switch (name)
            {
                case DBMS.MySQL:
                    return new MySQLFactory();
                case DBMS.Postgres:
                    return new PostgresFactory();
            }
            return null;
        }

        public abstract ISqlAdapter GetSqlAdapter();

        public abstract SQLDriver.ISQLDriver GetSQLDriver();
    }
}
