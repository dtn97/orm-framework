/* License: http://www.apache.org/licenses/LICENSE-2.0 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using LambdaSqlBuilder.Resolver;
using OOPDPFinalProject.ORMFramework;

namespace LambdaSqlBuilder.Builder
{
    public class SqlQueryBuilder
    {
        private const string PARAMETER_PREFIX = "Param";

        private readonly List<string> _tableNames = new List<string>();
        private readonly List<string> _splitColumns = new List<string>();
        private int _paramIndex;

        public List<string> TableNames { get { return _tableNames; } }
        public List<string> SplitColumns { get { return _splitColumns; } }
        public int CurrentParamIndex { get { return _paramIndex; } }

        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLCondition _conditionBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLCondition();
        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLSelection _selectionBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLSelection();
        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLGrouping _groupByBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLGrouping();
        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLHaving _havingBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLHaving();
        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLOrderBy _orderByBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLOrderBy();
        private OOPDPFinalProject.ORMFramework.SQLBuilder.SQLJoin _joinBuilder = new OOPDPFinalProject.ORMFramework.SQLBuilder.SQLJoin();

        public void Select(string tableName)
        {
            _selectionBuilder.AddTableName(tableName);
        }

        public void Select(string tableName, string fieldName)
        {
            _selectionBuilder.AddSelect(tableName, fieldName);
        }
        public void Select(string tableName, string fieldName, LambdaSqlBuilder.Resolver.SelectFunction selectFunction)
        {
            _selectionBuilder.AddSelect(tableName, fieldName, selectFunction);
        }

        public void GroupBy(string tableName, string fieldName)
        {
            _groupByBuilder.AddGroupBy(tableName, fieldName);
        }

        public void Join(string originalTableName, string joinTableName, string leftField, string rightField)
        {
            _tableNames.Add(joinTableName);
            _joinBuilder.AddJoin(originalTableName, joinTableName, leftField, rightField);
            _splitColumns.Add(rightField);
        }

        public IDictionary<string, object> Parameters { get; private set; }

        public string QueryString
        {
            get
            {
                return OOPDPFinalProject.ORMFramework.SQLBuilder.SQLBuilder.GetQueryString(
                _selectionBuilder, _conditionBuilder, _groupByBuilder, _havingBuilder, _orderByBuilder, _joinBuilder, _tableNames.First());
            }
        }

        public void Having(string tableName, string fieldName, SelectFunction selectFunction, string opr, object value)
        {
            var paramId = _havingBuilder.AddHaving(tableName, fieldName, selectFunction, opr, NextParamId());
            AddParameter(paramId, value);
        }
        //public string QueryStringPage(int pageSize, int? pageNumber = null)
        //{
        //    if (pageNumber.HasValue)
        //    {
        //        return Adapter.QueryStringPage(Source, Selection, Conditions, Order, pageSize, pageNumber.Value);
        //    }

        //    return Adapter.QueryStringPage(Source, Selection, Conditions, Order, pageSize);
        //}


        internal SqlQueryBuilder(string tableName)
        {
            _tableNames.Add(tableName);
            Parameters = new ExpandoObject();
            _paramIndex = 0;
        }

        private string NextParamId()
        {
            ++_paramIndex;
            return PARAMETER_PREFIX + _paramIndex.ToString(CultureInfo.InvariantCulture);
        }

        private void AddParameter(string key, object value)
        {
            if (!Parameters.ContainsKey(key))
                Parameters.Add(key, value);
        }
        public void BeginExpression()
        {
            _conditionBuilder.AddBeginExp();
        }

        public void EndExpression()
        {
            _conditionBuilder.AddEndExp();
        }

        public void And()
        {
            _conditionBuilder.AddAndExp();
        }

        public void Or()
        {
            _conditionBuilder.AddOrExp();
        }

        public void Not()
        {
            _conditionBuilder.AddNotExp();
        }

        public void QueryByField(string tableName, string fieldName, string op, object fieldValue)
        {
            var paramId = _conditionBuilder.AddFieldComparison(tableName, fieldName, op, fieldValue, NextParamId());
            AddParameter(paramId, fieldValue);
        }

        public void QueryByFieldLike(string tableName, string fieldName, string fieldValue)
        {
            var paramId = _conditionBuilder.AddFieldComarisionWithLike(tableName, fieldName, fieldValue, NextParamId());
            AddParameter(paramId, fieldValue);
        }

        public void QueryByFieldNull(string tableName, string fieldName)
        {
            _conditionBuilder.AddFieldIsNull(tableName, fieldName);
        }

        public void QueryByFieldNotNull(string tableName, string fieldName)
        {
            _conditionBuilder.AddFieldIsNotNull(tableName, fieldName);
        }

        public void QueryByFieldComparison(string leftTableName, string leftFieldName, string op,
            string rightTableName, string rightFieldName)
        {
            _conditionBuilder.AddTwoFieldComparision(leftTableName, leftFieldName, op, rightTableName, rightFieldName);
        }
    }
}
