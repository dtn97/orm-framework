using System;
namespace OOPDPFinalProject.ORMFramework.SqlAdapter
{
    public class SqlMySQLAdapter:SqlPostgresAdapter
    {
        public override string OffSetAndLimit(int offset, int limit)
        {
            string tmp = "";
            if (offset >= 0) tmp += " LIMIT " + offset.ToString();
            if (limit >= 0) tmp += " , " + limit.ToString();
            return tmp;
        }
        public SqlMySQLAdapter()
        {
        }
    }
}
