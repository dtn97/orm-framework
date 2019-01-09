using System;
namespace OOPDPFinalProject.ORMFramework.SQLBuilder
{
    public class SQLCondition:SQLBuilder
    {
        public SQLCondition()
        {
        }
        public override void Add(string component)
        {
            if (_componentList.Count != 0)
                base.Add(component);
        }

        public void AddBeginExp()
        {
            Add("(");
        }

        public void AddEndExp()
        {
            Add(")");
        }

        public void AddAndExp()
        {
            Add(" AND ");
        }

        public void AddOrExp()
        {
            Add(" OR ");
        }

        public void AddNotExp()
        {
            Add(" NOT ");
        }

        public string AddFieldComparison(string tableName, string fieldName, string op, object fieldValue, string paramId)
        {
            string newCondition = string.Format("{0} {1} {2}",
                _adapter.Field(tableName, fieldName),
                op,
                _adapter.Parameter(paramId));

            _componentList.Add(newCondition);
            return paramId;
        }

        public void AddTwoFieldComparision(string leftTableName, string leftFieldName, string op,
            string rightTableName, string rightFieldName)
        {
            string newCondition = string.Format("{0} {1} {2}",
           _adapter.Field(leftTableName, leftFieldName),
           op,
           _adapter.Field(rightTableName, rightFieldName));

           _componentList.Add(newCondition);
        }

        public string AddFieldComarisionWithLike(string tableName, string fieldName, string fieldValue, string paramId)
        {
            string newCondition = string.Format("{0} LIKE {1}",
                _adapter.Field(tableName, fieldName),
                _adapter.Parameter(paramId));
            _componentList.Add(newCondition);
            return paramId;
        }

        public void AddFieldIsNull(string tableName, string fieldName)
        {
            _componentList.Add(string.Format("{0} IS NULL", _adapter.Field(tableName, fieldName)));
        }
        public void AddFieldIsNotNull(string tableName, string fieldName)
        {
            _componentList.Add(string.Format("{0} IS NOT NULL", _adapter.Field(tableName, fieldName)));
        }

        protected override string ComponentsToString()
        {
            return "WHERE "+ string.Join("", _componentList);
        }
    }
}
