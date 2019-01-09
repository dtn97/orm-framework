using System;
using System.Collections.Generic;
using Generator.SQLDriver;
namespace Generator.DatabaseMapper
{
    public class DatabaseGenerator
    {
        public DatabaseGenerator()
        {
        }

        public static bool Generate(string dbType, string connectionString, string folderPath)
        {
            ISQLDriver driver = SQLDriverFactory.GetInstance().GetDriver(dbType);
            if (driver == null)
            {
                return false;
            }
            if (!driver.Open(connectionString))
            {
                return false;
            }
            DatabaseMapper databaseMapper = DatabaseMapperFactory.GetInstance().GetDatabaseMapper(dbType);
            if (databaseMapper == null)
            {
                return false;
            }
            databaseMapper.Attach(driver);
            List<string> tables = databaseMapper.GetTables();
            foreach (string table in tables)
            {
                Console.WriteLine("Table name: " + table);
                Console.WriteLine("\tTable schema:");
                Dictionary<string, string> tableSchema = databaseMapper.GetTableSchema(table);
                foreach (KeyValuePair<string, string> tmp in tableSchema)
                {
                    Console.WriteLine("\t\t" + tmp.Key + ": " + tmp.Value);
                }
                List<string> primaryKeys = databaseMapper.GetTablePrimaryKey(table);
                if (primaryKeys != null && primaryKeys.Count > 0)
                {
                    Console.Write("\t\tPrimary key: ");
                    foreach (string primaryKey in primaryKeys)
                    {
                        Console.Write(primaryKey + "\t");
                    }
                    Console.WriteLine();
                }
                Dictionary<string, string> foreignKeys = databaseMapper.GetTableForeignKeys(table);
                if (foreignKeys != null && foreignKeys.Count > 0)
                {
                    Console.Write("\t\tForeign key: ");
                    foreach (KeyValuePair<string, string> foreignKey in foreignKeys)
                    {
                        Console.Write("[" + foreignKey.Key + ", " + foreignKey.Value + "]\t");
                    }
                    Console.WriteLine();
                }
                List<string> tablesHaveForeignKey = databaseMapper.GetTableHaveForeignKey(table);
                if (tablesHaveForeignKey != null && tablesHaveForeignKey.Count > 0)
                {
                    Console.Write("\t\tTable have Foreign key: ");
                    foreach (string tableHaveForeignKey in tablesHaveForeignKey)
                    {
                        Console.Write(tableHaveForeignKey + "\t");
                    }
                    Console.WriteLine();
                }
                ClassGenerator.Generate(table, tableSchema, primaryKeys, foreignKeys, tablesHaveForeignKey, folderPath);
            }

            driver.Close();

            return true;
        }
    }
}
