using System;
using OOPDPFinalProject.ORMFramework.SQLDriver;

namespace OOPDPFinalProject.ORMFramework.DBMSFactory
{
    public class MySQLFactory:DBMSAbstractFactory
    {

        public override ISqlAdapter GetSqlAdapter()
        {
            return new ORMFramework.SqlAdapter.SqlMySQLAdapter();
        }

        public override ISQLDriver GetSQLDriver()
        {
            return new ORMFramework.SQLDriver.MySqlDriver();
        }
    }
}
