using System;
using System.Collections.Generic;
using System.IO;

namespace OOPDPFinalProject.ORMFramework.DatabaseMapper
{
    public class ClassGenerator
    {
        private static string namespaceOpen = "\nnamespace {0}\n";
        private static string namespaceClose = "}";

        private static string classOpen = "\tpublic class {0} : ORMFramework.DomainObject\n";
        private static string classClose = "\t}\n";

        private static string annotation = "\t\t[{0}({1})]\n";

        private static string field = "\t\tprivate {1} {0};\n";

        private static string publicField = "\t\tpublic {1} {0}\n";
        
        public ClassGenerator()
        {
        }

        public static void Generate(string tableName, Dictionary<string, string> columns, List<string> primaryKeys, Dictionary<string, string> foreignKeys)
        {
            if (columns == null)
            {
                return;
            }
            string res = "using System;\n";
            tableName = toUpperCase(tableName);
            res += String.Format(namespaceOpen, "DAM");
            res += "{\n";
            res += String.Format(classOpen, tableName);
            res += "\t{\n";
            if (primaryKeys != null)
            {
                foreach (string primaryKey in primaryKeys)
                {
                    res += String.Format(field, "_" + primaryKey, columns[primaryKey]);
                    res += String.Format(annotation, "PrimaryKey", "true");
                    res += GetFieldPublic(primaryKey, columns[primaryKey], tableName);
                    columns.Remove(primaryKey);
                }
            }
            if (foreignKeys != null)
            {
                foreach (KeyValuePair<string, string> foreignKey in foreignKeys)
                {
                    res += String.Format(field, "_" + foreignKey.Key, columns[foreignKey.Key]);
                    res += String.Format(annotation, "ForeignKey", "\"" + foreignKey.Value + "\"");
                    res += GetFieldPublic(foreignKey.Key, columns[foreignKey.Key], tableName);
                    columns.Remove(foreignKey.Key);
                }
            }
            foreach (KeyValuePair<string, string> column in columns)
            {
                res += String.Format(field, "_" + column.Key, column.Value);
                res += GetFieldPublic(column.Key, column.Value, tableName);
            }
            res += classClose;
            res += namespaceClose;

            Console.WriteLine(res);

            File.WriteAllText("./" + tableName + ".cs", res);
        }

        private static string toUpperCase(string tableName)
        {
            char[] tmp = tableName.ToCharArray();
            tmp[0] = char.ToUpper(tmp[0]);
            return new string(tmp);
        }

        private static string GetFieldPublic(string key, string value, string tableName)
        {
            string res = "";
            res += String.Format(publicField, toUpperCase(key), value);
            res += "\t\t{\n";

            res += "\t\t\tget\n\t\t\t{\n";
            res += String.Format("\t\t\t\tLoad<{0}>();\n", tableName);
            res += String.Format("\t\t\t\treturn {0};\n", "_" + key);
            res += "\t\t\t}\n";

            res += "\t\t\tset\n\t\t\t{\n";
            res += String.Format("\t\t\t\tCheckDirty<{0}>();\n", tableName);
            res += String.Format("\t\t\t\t{0} = value;\n", "_" + key);
            res += "\t\t\t}\n";

            res += "\t\t}\n";

            return res;
        }
    }
}
