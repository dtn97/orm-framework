using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.SQLDriver;
namespace OOPDPFinalProject.ORMFramework.TableModifier
{
    public class InsertAction:TableModifier
    {
        public InsertAction(ISQLDriver sQLDriver, Dictionary<string, TableSchema.Column> Columns, string TableName, string PrimaryKeyName)
        : base(sQLDriver, Columns, TableName, PrimaryKeyName)
        {
        }

        protected override string getSQL(Dictionary<string, string> fieldValues)
        {
            
            return _adapter.InsertString(_tableName, fieldValues);
        }
    }
}