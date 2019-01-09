using System;
using Generator.DataTypeMapper;
using Generator.SQLDriver;

namespace Generator.DatabaseMapper
{
    public class PostgresMapper : DatabaseMapper
    {
        private DataTypeMapper.DataTypeMapper postgresDataTypeMapper = new PostgresDataTypeMapper();
        public PostgresMapper()
        {
        }

        public PostgresMapper(ISQLDriver driver)
        {
            base.driver = driver;
        }

        protected override string GetTablesSQL()
        {
            return "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";
        }

        protected override string GetTableSchemaSQL(string tableName)
        {
            return "select column_name, data_type from INFORMATION_SCHEMA.COLUMNS where table_name = '" + tableName + "'";
        }

        protected override string GetPrimaryKeySQL(string tableName)
        {
            return "SELECT c.column_name FROM information_schema.table_constraints tc JOIN information_schema.constraint_column_usage AS ccu USING (constraint_schema, constraint_name) JOIN information_schema.columns AS c ON c.table_schema = tc.constraint_schema AND tc.table_name = c.table_name AND ccu.column_name = c.column_name where constraint_type = 'PRIMARY KEY' and tc.table_name = '" + tableName + "'";
        }

        protected override string GetTableForeignKeysSQL(string tableName)
        {
            return "SELECT kcu.column_name, ccu.table_name AS foreign_table_name FROM information_schema.table_constraints AS tc JOIN information_schema.key_column_usage AS kcu ON tc.constraint_name = kcu.constraint_name AND tc.table_schema = kcu.table_schema JOIN information_schema.constraint_column_usage AS ccu ON ccu.constraint_name = tc.constraint_name AND ccu.table_schema = tc.table_schema WHERE constraint_type = 'FOREIGN KEY' AND tc.table_name='" + tableName + "'";
        }

        protected override string DataMapper(object obj)
        {
            return postgresDataTypeMapper.dbTypeToClassType((string)obj);
        }

        protected override string GetTableHaveForeignKeySQL(string tableName)
        {
            return "SELECT tc.table_name FROM information_schema.table_constraints AS tc JOIN information_schema.key_column_usage AS kcu ON tc.constraint_name = kcu.constraint_name AND tc.table_schema = kcu.table_schema JOIN information_schema.constraint_column_usage AS ccu ON ccu.constraint_name = tc.constraint_name AND ccu.table_schema = tc.table_schema WHERE constraint_type = 'FOREIGN KEY' and ccu.table_name = '" + tableName + "'";
        }
    }
}
