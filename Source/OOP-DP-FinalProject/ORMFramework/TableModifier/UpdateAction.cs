using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.SQLDriver;
namespace OOPDPFinalProject.ORMFramework.TableModifier
{
    public class UpdateAction:TableModifier
    { 
        protected override string getSQL(Dictionary<string, string> fieldValues)
        {
            return _adapter.UpdateString(_tableName, _primaryKeyName, fieldValues);
        }

        public UpdateAction(ISQLDriver sQLDriver, Dictionary<string, TableSchema.Column> Columns, string TableName, string PrimaryKeyName)
        : base(sQLDriver, Columns, TableName, PrimaryKeyName)
        {
        }
    }
}
