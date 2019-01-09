/* License: http://www.apache.org/licenses/LICENSE-2.0 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using LambdaSqlBuilder.Builder;

namespace LambdaSqlBuilder.Resolver
{
    public enum SelectFunction
    {
        COUNT,
        DISTINCT,
        SUM,
        MIN,
        MAX,
        AVG
    }
    partial class LambdaResolver
    {
        private Dictionary<ExpressionType, string> _operationDictionary = new Dictionary<ExpressionType, string>()
                                                                              {
                                                                                  { ExpressionType.Equal, "="},
                                                                                  { ExpressionType.NotEqual, "!="},
                                                                                  { ExpressionType.GreaterThan, ">"},
                                                                                  { ExpressionType.LessThan, "<"},
                                                                                  { ExpressionType.GreaterThanOrEqual, ">="},
                                                                                  { ExpressionType.LessThanOrEqual, "<="}
                                                                              };

        private SqlQueryBuilder _builder { get; set; }

        public LambdaResolver(SqlQueryBuilder builder)
        {
            _builder = builder;   
        }

        // Lay ten goi cua cac cot co trong table
        public static string GetColumnName<T>(Expression<Func<T, object>> selector)
        {
            return GetColumnName(GetMemberExpression(selector.Body));
        }

        public static string GetColumnName(Expression expression)
        {
            var member = GetMemberExpression(expression);
            var column = member.Member.GetCustomAttribute<OOPDPFinalProject.ORMFramework.DataAnnotations.Column>();
            if (column != null)
                return column.ColumnName;
            else

                return member.Member.Name;

        }


        // Lay ten goi cua Table quan ly ci kieu du lieu type
        public static string GetTableName<T>()
        {
            return GetTableName(typeof(T));
        }

        public static string GetTableName(Type type)
        {
            OOPDPFinalProject.ORMFramework.Session session = OOPDPFinalProject.ORMFramework.Session.getCurSession();
            MethodInfo method = session.GetType().GetMethod("Table").MakeGenericMethod(type);
            var table = method.Invoke(session, null);
            string tableName = (string)table.GetType().GetMethod("GetTableName").Invoke(table, new object[] { type });
            return tableName;
        }

        private static string GetTableName(MemberExpression expression)
        {
            return GetTableName(expression.Member.DeclaringType);
        }



        // Lay ten cua primary key cua Table quan ly kieu co du lieu type
        private static string GetPrimaryKeyName(MemberExpression expression)
        {
            return GetPrimaryKeyName(expression.Member.DeclaringType);
        }

        public static string GetPrimaryKeyName(Type type)
        {
            OOPDPFinalProject.ORMFramework.Session session = OOPDPFinalProject.ORMFramework.Session.getCurSession();
            MethodInfo method = session.GetType().GetMethod("Table").MakeGenericMethod(type);
            var table = method.Invoke(session, null);
            string tableName = (string)table.GetType().GetMethod("GetPrimaryKeyName").Invoke(table, new object[] {});
            return tableName;
        }

       


        private static BinaryExpression GetBinaryExpression(Expression expression)
        {
            if (expression is BinaryExpression)
                return expression as BinaryExpression;

            throw new ArgumentException("Binary expression expected");
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return expression as MemberExpression;
                case ExpressionType.Convert:
                    return GetMemberExpression((expression as UnaryExpression).Operand);
            }

            throw new ArgumentException("Member expression expected");
        }

    }
}
