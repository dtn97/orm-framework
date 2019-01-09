using System;
using LambdaSqlBuilder.Resolver;

namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLHaving:SQLBuilder
    {
        public SQLHaving()
        {
        }

        protected override string ComponentsToString()
        {
            return "HAVING " + string.Join(" ", _componentList);
        }

        public string AddHaving(string tableName, string fieldName, SelectFunction selectFunction, string opr, string parameter)
        {
            var func = selectFunction.ToString();
            _componentList.Add(string.Format("{2}({0}.{1}) {3} {4}", tableName, fieldName,func,opr, _adapter.Parameter(parameter)));
            return parameter;
        }
    }
}
