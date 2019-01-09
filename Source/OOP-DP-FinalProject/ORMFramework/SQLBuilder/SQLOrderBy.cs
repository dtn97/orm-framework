using System;
namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLOrderBy:SQLBuilder
    {
        public SQLOrderBy()
        {
        }

        protected override string ComponentsToString()
        {
            return "ORDER BY " + string.Join(", ", _componentList);
        }
    }
}
