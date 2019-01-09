using System;
using System.Collections.Generic;

namespace OOPDPFinalProject.ORMFramework.Datatype
{
    public class DataTypeMapper
    {
        protected Dictionary<Type, string> mapper;
        protected Dictionary<string, string> dbMapper;
        public DataTypeMapper()
        {
            mapper = new Dictionary<Type, string>();
            dbMapper = new Dictionary<string, string>();
        }
        virtual public string GetDataType(Type t)
        {
            if (mapper.ContainsKey(t))
            {
                return mapper[t];
            }
            string[] tmp = t.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (tmp.Length > 0)
            {
                return tmp[tmp.Length - 1].ToLower();
            }
            return null;
        }
        virtual public string dbTypeToClassType(string dbType)
        {
            if (dbMapper.ContainsKey(dbType))
            {
                return dbMapper[dbType];
            }
            return "Object";
        }
    }
}
