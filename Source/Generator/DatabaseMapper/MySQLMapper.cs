using System;
using Generator.DataTypeMapper;
using Generator.SQLDriver;

namespace Generator.DatabaseMapper
{
    public class MySQLMapper : DatabaseMapper
    {
        private DataTypeMapper.DataTypeMapper mySQLDataTypeMapper = new MySQLDataTypeMapper();
        public MySQLMapper()
        {
        }

        public MySQLMapper(ISQLDriver driver)
        {
            base.driver = driver;
        }

        protected override string GetTablesSQL()
        {
            return "show tables";
        }

        protected override string GetTableSchemaSQL(string tableName)
        {
            return "describe " + tableName;
        }

        protected override string GetPrimaryKeySQL(string tableName)
        {
            return "show columns from " + tableName + " where `Key` = \"PRI\";";
        }

        protected override string GetTableForeignKeysSQL(string tableName)
        {
            return "SELECT k.COLUMN_NAME,  k.REFERENCED_TABLE_NAME FROM information_schema.TABLE_CONSTRAINTS i LEFT JOIN information_schema.KEY_COLUMN_USAGE k ON i.CONSTRAINT_NAME = k.CONSTRAINT_NAME  WHERE i.CONSTRAINT_TYPE = 'FOREIGN KEY'  AND i.TABLE_SCHEMA = DATABASE() AND i.TABLE_NAME = '" + tableName + "'";
        }

        protected override string DataMapper(object obj)
        {
            return mySQLDataTypeMapper.dbTypeToClassType((string)obj);
        }

        protected override string GetTableHaveForeignKeySQL(string tableName)
        {
            return "SELECT k.TABLE_NAME FROM information_schema.TABLE_CONSTRAINTS i LEFT JOIN information_schema.KEY_COLUMN_USAGE k ON i.CONSTRAINT_NAME = k.CONSTRAINT_NAME  WHERE i.CONSTRAINT_TYPE = 'FOREIGN KEY'  AND i.TABLE_SCHEMA = DATABASE() and k.REFERENCED_TABLE_NAME = '" + tableName + "'";
        }

    }
}
