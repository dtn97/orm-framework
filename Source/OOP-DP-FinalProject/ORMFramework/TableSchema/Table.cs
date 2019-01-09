using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace OOPDPFinalProject.ORMFramework.TableSchema
{
    public class Table<T> where T : class, new()
    {
        private string tableName; //table Name on database
        public UnitOfWork unitOfWork = new UnitOfWork();
        private ISqlAdapter _sqlAdapter
        {
            get
            {
                return Session.getCurSession().SQLAdapter;
            }
        }
        private Dictionary<string, Column> columns;
        private Dictionary<string, string> hasOne;
        private Dictionary<string, string> hasMany;
        private string primaryKeyField = "";

        private SQLDriver.ISQLDriver _sqlDriver {
            get
            {
                return Session.getCurSession().SQLDriver;
            }
        }
       

        private SQLBuilder.SQLOffsetLimit offsetLimit = new SQLBuilder.SQLOffsetLimit();
        public Table(string name)
        {
            tableName = name;
            TableMapper();
        }

        public void Print()
        {
            Console.WriteLine("Table name: " + tableName);
            foreach (KeyValuePair<string, Column> column in columns)
            {
                column.Value.Print();
            }
        }

        public Table()
        {
            TableMapper();
        }

        private void TableMapper()
        {
            Type type = typeof(T);

            columns = new Dictionary<string, Column>();
            this.tableName = GetTableName(type);

            Datatype.DataTypeMapper mapper = new Datatype.PostgresDatatypeMapper();
            hasOne = new Dictionary<string, string>();
            hasMany = new Dictionary<string, string>();
            PropertyInfo[] propertyInfos = type.GetProperties();

           
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                // not mapper
                var attrColNotMapper = propertyInfo.GetCustomAttribute<DataAnnotations.NotMapper>();
                if (attrColNotMapper != null)
                {
                    if (attrColNotMapper.IsNotMapper)
                    {
                        continue;
                    }
                }

                var attriHasOne = propertyInfo.GetCustomAttribute<DataAnnotations.HasOne>();
                if (attriHasOne != null)
                {
                    hasOne.Add(propertyInfo.Name, attriHasOne.TableName);
                    continue;
                }

                var attriHasMany = propertyInfo.GetCustomAttribute<DataAnnotations.HasMany>();
                if (attriHasMany != null)
                {
                    hasMany.Add(propertyInfo.Name, attriHasMany.TableName);
                    continue;
                }

                string columnName;
                string columnType;
                bool isPK = false;
                bool isRequired = false;

                // field name
                columnName = propertyInfo.Name;

                // field type
                columnType = mapper.GetDataType(propertyInfo.PropertyType);

                // column name
                var attrColName = propertyInfo.GetCustomAttribute<DataAnnotations.Column>();
                if (attrColName != null)
                {
                    columnName = attrColName.ColumnName;
                }

                // column PK
                var attrPK = propertyInfo.GetCustomAttribute<DataAnnotations.PrimaryKey>();
                if (attrPK != null)
                {
                    isPK = attrPK.IsPrimaryKey;
                }

                // column required
                var attrRequired = propertyInfo.GetCustomAttribute<DataAnnotations.Required>();
                if (attrRequired != null)
                {
                    isPK = attrRequired.IsRequired;
                }

                // column FK
                var attrFK = propertyInfo.GetCustomAttribute<DataAnnotations.ForeignKey>();
                if (attrFK != null)
                {
                    columns.Add(propertyInfo.Name, new Column(columnName, columnType, isPK, isRequired, attrFK.RefTable));
                }
                else
                {
                    columns.Add(propertyInfo.Name, new Column(columnName, columnType, isPK, isRequired));
                }

                foreach (KeyValuePair<string, Column> column in columns)
                {
                    if (column.Value.IsPrimaryKey())
                    {
                        primaryKeyField = column.Key;
                    }
                }
            }
        }

        public string GetTableName(Type type)
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

        public string GetColumnName(string AttrName)
        {
            return columns[AttrName].GetName();
        }

        private string GetDataSQL()
        {
            string res = "";
            foreach (Column column in columns.Values)
            {
                res += (column.GetName() + ",");
            }
            return res.Substring(0, res.Length - 1);
        }

        public string GetPrimaryKeyName()
        {
            foreach (Column col in columns.Values)
            {
                if (col.IsPrimaryKey())
                {
                    return col.GetName();
                }
            }
            return null;
        }

        public string GetRefColumnName(string TargetTable)
        {
            TargetTable = TargetTable.ToLower();
            foreach (Column col in columns.Values)
            {
                if (col.IsForeignKey())
                {
                    if (col.GetRefTable() == TargetTable)
                        return col.GetName();
                }
            }
            return null;
        }

        public void DoLoadLine(ORMFramework.DomainObject obj)
        {
            // Neu da co san thi khong can load lai lan nua
            if (unitOfWork.IsExisted(obj.getKey()))
            {
                return;
            }
            // Tim trong database
            Dictionary<string, object> sqlParams = new Dictionary<string, object>();
            sqlParams.Add(GetPrimaryKeyName(), obj.getKey());
            string sqlQuery = _sqlAdapter.QueryString(GetDataSQL(), tableName, sqlParams);
            _sqlDriver.ExecuteReader(sqlQuery, sqlParams);

            bool foundInDB = false;
            while (_sqlDriver.Read())
            {
                int i = 0;
                object[] reader = _sqlDriver.GetReaderValue();
                foreach (string fieldName in columns.Keys)
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(fieldName);
                    propertyInfo.SetValue(obj, Convert.ChangeType(reader[i++], propertyInfo.PropertyType));
                }
                foundInDB = true;
            }

            if (foundInDB) //Neu tim thay trong DB, them vao DS nhung object da co
                unitOfWork.RegisterFromDatabase(obj);
            else //Neu khong co trong DB, object moi, them vao DS nhung object moi
                unitOfWork.RegisterNew(obj);

            // HasOne
            if (hasOne.Count > 0)
            {
                foreach (KeyValuePair<string, string> tmp in hasOne)
                {
                    Session session = Session.getCurSession();
                    MethodInfo method = session.GetType().GetMethod("Table").MakeGenericMethod(obj.GetType().GetProperty(tmp.Key).PropertyType);
                    var table = method.Invoke(session, null);

                    method = table.GetType().GetMethod("GetPrimaryKeyName");
                    var priCol = method.Invoke(table,null);

                    // get foreignKey

                    string fieldName = this.GetForeignKeyFieldName(tmp.Value);

                    object fieldValue = obj.GetType().GetProperty(fieldName).GetValue(obj);

                    method = table.GetType().GetMethod("FindOneHaveValue");
                    var list = method.Invoke(table, new object[] { priCol, fieldValue });

                    PropertyInfo propertyInfo = obj.GetType().GetProperty(tmp.Key);
                    propertyInfo.SetValue(obj, Convert.ChangeType(list, propertyInfo.PropertyType));
                }
            }

            // has many
            if (hasMany.Count > 0)
            {
                foreach (KeyValuePair<string, string> tmp in hasMany)
                {
                    Session session = Session.getCurSession();
                    MethodInfo method = session.GetType().GetMethod("Table").MakeGenericMethod(obj.GetType().GetProperty(tmp.Key).PropertyType.GetGenericArguments()[0]);
                    var table = method.Invoke(session, null);
                    method = table.GetType().GetMethod("GetRefColumnName");
                    var refCol = method.Invoke(table, new object[] { tableName });
                    method = table.GetType().GetMethod("FindAllHaveValue");
                    var list = method.Invoke(table, new object[] { refCol, obj.getKey() });

                    PropertyInfo propertyInfo = obj.GetType().GetProperty(tmp.Key);
                    propertyInfo.SetValue(obj, Convert.ChangeType(list, propertyInfo.PropertyType));
                }
            }
        }

        public string GetForeignKeyFieldName(string refTableName)
        {
            foreach (KeyValuePair<string, Column> column in columns)
            {
                if (column.Value.IsForeignKey())
                {
                    if (column.Value.GetRefTable().Equals(refTableName))
                    {
                        return column.Key;
                    }
                }
            }
            return null;
        }

        private List<T> FindAllWithSQLAndParams(string sqlQuery, Dictionary<string, object> sqlParams)
        {
            _sqlDriver.ExecuteReader(sqlQuery, sqlParams);
            List<T> all = new List<T>();
            while (_sqlDriver.Read())
            {
                T res = GetInstance(_sqlDriver.GetReaderValue()[0]);
                object primaryKey = (object)((DomainObject)(object)res).getKey();
                if (unitOfWork.IsExisted(primaryKey))
                {
                    all.Add((T)unitOfWork.GetObjectByKey(primaryKey));
                }
                else
                {
                    all.Add(res);
                }
            }
            return all;

        }
        public List<T> FindAll()
        {
            string sqlQuery = _sqlAdapter.QueryString(GetPrimaryKeyName(), tableName, "", "", "", "");
            return FindByQuery(sqlQuery);

        }
        public List<T> FindAllHaveValue(string field, object value)
        {

            Dictionary<string, object> sqlParams = new Dictionary<string, object>();
            sqlParams.Add(field, value);
            string sqlQuery = _sqlAdapter.QueryString(GetPrimaryKeyName(), tableName, sqlParams);
            return FindAllWithSQLAndParams(sqlQuery, sqlParams);
        }
        public T FindOneHaveValue(object field, object value)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>();

            sqlParams.Add((string)field, value);

            string sqlQuery = _sqlAdapter.QueryString(GetPrimaryKeyName(), tableName, sqlParams);
            List<T> tmp = FindAllWithSQLAndParams(sqlQuery, sqlParams);
            if (tmp != null && tmp.Count > 0)
            {
                return tmp[0];
            }
            return null;

        }
        private List<T> FindByQuery(string queryString, IDictionary<string, object> parameters)
        {
            List<T> all = new List<T>();
            _sqlDriver.ExecuteReader(SQLBuilder.SQLBuilder.AddOffsetLimit(queryString, offsetLimit), parameters);
            while (_sqlDriver.Read())
            {
                object[] reader = _sqlDriver.GetReaderValue();
                T res = GetInstance(reader[0]);
                all.Add(res);
            }
            offsetLimit.GetAll();
            return all;
        }

        private List<T> FindByQuery(string sqlQuery)
        {
            _sqlDriver.ExecuteReader(SQLBuilder.SQLBuilder.AddOffsetLimit(sqlQuery, offsetLimit));
            List<T> all = new List<T>();
            while (_sqlDriver.Read())
            {
                T res = GetInstance(_sqlDriver.GetReaderValue()[0]);
                all.Add(res);
            }
            offsetLimit.GetAll();
            return all;
        }

        public Table<T> Offset(int Offset)
        {
            offsetLimit.SetOffSet(Offset);
            return this;
        }

        public Table<T> Limit(int Limit)
        {
            offsetLimit.SetLimit(Limit);
            return this;
        }
        public List<T> FindAll(LambdaSqlBuilder.SqlLam<T> sqlLam)
        {
            return FindByQuery(sqlLam.QueryString, sqlLam.QueryParameters);
        }

        public List<T> FindAll(string queryString, IDictionary<string, object> parameters)
        {
            return FindByQuery(queryString, parameters);
        }

        public T FindOne(LambdaSqlBuilder.SqlLam<T> sqlLam)
        {
            return (FindAll(sqlLam)[0]);
        }

        public T GetInstance(object key)
        {
            if (unitOfWork.IsExisted(key))
            {
                return (T)unitOfWork.GetObjectByKey(key);
            }
            T res = new T();
            PropertyInfo propertyInfo = res.GetType().GetProperty(primaryKeyField); // Cho nay can xem lai, HARD CODE!!!
            propertyInfo.SetValue(res, key);
            return res;
        }

        public List<object[]> ExecuteQuery(string queryString, IDictionary<string, object> queryParameters)
        {
            List<object[]> all = new List<object[]>();
            _sqlDriver.ExecuteReader(queryString, queryParameters);

            while (_sqlDriver.Read())
            {
                all.Add(_sqlDriver.GetReaderValue());
            }
            return all;
        }

        public List<object[]> ExecuteQuery(LambdaSqlBuilder.SqlLam<T> sqlLam)
        {
            return ExecuteQuery(sqlLam.QueryString, sqlLam.QueryParameters);
        }
        public void printUnitOfWork()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("####  Print Unit of work of table " + tableName + " ###");
            unitOfWork.Print();
            Console.WriteLine("=================================================");
        }

        TableModifier.UpdateAction updateAction = null;
        TableModifier.InsertAction insertAction = null;
        TableModifier.DeleteAction deleteAction = null;

        private void doWork(List<object> objs, TableModifier.TableModifier modifier)
        {
            foreach (object obj in objs)
            {
                modifier.Execute(obj);
            }
        }

        public void commit()
        {
            printUnitOfWork();
            // Update
            if (updateAction == null)
            {
                updateAction = new TableModifier.UpdateAction(_sqlDriver, columns, tableName, GetPrimaryKeyName());
            }
            doWork(unitOfWork.getDirtyList(), updateAction);

            // Insert
            if (insertAction == null)
            {
                insertAction = new TableModifier.InsertAction(_sqlDriver, columns, tableName, GetPrimaryKeyName());
            }
            doWork(unitOfWork.getNewList(), insertAction);

            // Delete
            if (deleteAction == null)
            {
                deleteAction = new TableModifier.DeleteAction(_sqlDriver, columns, tableName, GetPrimaryKeyName());
            }
            doWork(unitOfWork.getRemovedList(), deleteAction);

            unitOfWork.ClearAll();
        }
    }
}
