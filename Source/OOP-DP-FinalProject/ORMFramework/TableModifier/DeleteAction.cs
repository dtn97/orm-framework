using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.SQLDriver;
namespace OOPDPFinalProject.ORMFramework.TableModifier
{
    public class DeleteAction:TableModifier
    {
        public DeleteAction(ISQLDriver sQLDriver, Dictionary<string, TableSchema.Column> Columns, string TableName, string PrimaryKeyName)
        : base(sQLDriver, Columns, TableName, PrimaryKeyName)
        {
        }

        protected override string getSQL(Dictionary<string, string> fieldValues)
        {
            return _adapter.DeleteString(_tableName, _primaryKeyName, fieldValues);
        }
    }
}
