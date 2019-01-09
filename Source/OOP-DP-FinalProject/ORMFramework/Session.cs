using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.SQLDriver;
using OOPDPFinalProject.ORMFramework.TableSchema;

namespace OOPDPFinalProject.ORMFramework
{
    public class Session
    {
        private string connectionString;
        private static Session sess = null;
        private Dictionary<Type, object> tables;
  
        private ISqlAdapter _sqlAdapter;
        private ORMFramework.SQLDriver.ISQLDriver _sqlDriver;


        public ISQLDriver SQLDriver
        {
            get
            {
                return _sqlDriver;
            }
        }

        public ISqlAdapter SQLAdapter
        {
            get
            {
                return _sqlAdapter;
            }
        }
        private Session(string conStr, DBMS dbms)
        {
            DBMSFactory.DBMSAbstractFactory DBMSfactory= DBMSFactory.DBMSAbstractFactory.GetFactoryByDBMS(dbms);
            this._sqlDriver = DBMSfactory.GetSQLDriver();
            this._sqlAdapter = DBMSfactory.GetSqlAdapter();
            this.connectionString = conStr;
            _sqlDriver.Open(conStr);
            tables = new Dictionary<Type, object>();
        }

        public Table<T> Table<T>() where T : class, new()
        {
            if (tables.ContainsKey(typeof(T))) {
                return (Table<T>) tables[typeof(T)];
            }
            Table<T> newTable = new Table<T>();
            tables.Add(typeof(T), newTable);
            return newTable;
        }

        public static void Open(string conStr)
        {
            Open(conStr, DBMS.Postgres);
        }

        public static void Open(string conStr,DBMS DBManagementSystem)
        {
            if (sess == null)
            {
                sess = new Session(conStr, DBManagementSystem);
                return;
            }
            sess.Close();
            sess = new Session(conStr, DBManagementSystem);
        }

        public static Session getCurSession()
        {
            return sess;
        }

        public void Close()
        {
            _sqlDriver.Close();
        }

        public void CommitAll()
        {
            foreach (object table in tables.Values)
            {
                table.GetType().GetMethod("commit").Invoke(table, new object[] { });
            }
        }

        public object GetTableByType(Type type)
        {
            if (tables.ContainsKey(type))
            {
                return tables[type];
            }
            return null;
        }
    }
}
