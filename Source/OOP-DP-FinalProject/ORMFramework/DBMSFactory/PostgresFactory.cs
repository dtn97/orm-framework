using System;
using OOPDPFinalProject.ORMFramework.SQLDriver;

namespace OOPDPFinalProject.ORMFramework.DBMSFactory
{
    public class PostgresFactory:DBMSAbstractFactory
    {
        public PostgresFactory()
        {
        }

        public override ISqlAdapter GetSqlAdapter()
        {
            return new ORMFramework.SqlPostgresAdapter();
        }

        public override ISQLDriver GetSQLDriver()
        {
            return new ORMFramework.SQLDriver.PostgresDriver();
        }
    }
}
