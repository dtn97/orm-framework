using System;
using System.Collections.Generic;
using System.Reflection;
using OOPDPFinalProject.ORMFramework.DataAnnotations;
namespace OOPDPFinalProject.ORMFramework.TableSchema
{
    public class TableMapper
    {
        private static TableMapper tableMapper;
        private TableMapper()
        {
        }

        static TableMapper GetInstance()
        {
            if (tableMapper == null)
            {
                tableMapper = new TableMapper();
            }
            return tableMapper;
        }

        static string GetTableName(Type type)
        {
            var dnAttribute = type.GetCustomAttribute<DataAnnotations.Table>();
            if (dnAttribute != null)
            {
                return dnAttribute.TableName;
            }
            string[] tmp = type.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (tmp.Length > 0)
            {
                return tmp[tmp.Length - 1];
            }
            return null;
        }

        static Dictionary<string, string> GetTableColumn(Type type)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            return res;
        }
    }
}
