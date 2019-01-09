using System;
using System.Collections.Generic;
using System.IO;

namespace Generator.DatabaseMapper
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

        private static string publicFieldList = "\t\tpublic List<{1}> {0}\n";

        public ClassGenerator()
        {
        }

        public static void Generate(string tableName, Dictionary<string, string> columns, List<string> primaryKeys, Dictionary<string, string> foreignKeys, List<string> tablesHaveForeignKey, string folderPath)
        {
            if (columns == null)
            {
                return;
            }
            string res = "using System;\n";
            if (tablesHaveForeignKey != null && tablesHaveForeignKey.Count > 0)
            {
                res += "using System.Collections.Generic;\n";
            }
            tableName = toUpperCase(tableName);
            res += String.Format(namespaceOpen, "YOUR_NAME_SPACE");
            res += "{\n";
            res += String.Format(classOpen, tableName);
            res += "\t{\n";
            res += GetConstructor(tableName);
            if (primaryKeys != null)
            {
                foreach (string primaryKey in primaryKeys)
                {
                    res += String.Format(field, "_" + primaryKey, columns[primaryKey]);
                    res += String.Format(annotation, "PrimaryKey", "true");
                    res += GetFieldPrimaryKey(primaryKey, columns[primaryKey], tableName);
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
                    res += GetFieldForeignKey(foreignKey.Value, tableName);
                    columns.Remove(foreignKey.Key);
                }
            }
            foreach (KeyValuePair<string, string> column in columns)
            {
                res += String.Format(field, "_" + column.Key, column.Value);
                res += GetFieldPublic(column.Key, column.Value, tableName);
            }

            if (tablesHaveForeignKey != null && tablesHaveForeignKey.Count > 0)
            {
                foreach (string tableHaveForeignKey in tablesHaveForeignKey)
                {
                    res += GetFieldTableForeignKey(tableHaveForeignKey, tableName);
                }
            }

            res += GetDelete(tableName);

            res += classClose;
            res += namespaceClose;


            Console.WriteLine(res);
            if (folderPath[folderPath.Length - 1] != '/')
            {
                folderPath += "/";
            }
            File.WriteAllText(folderPath + tableName + ".cs", res);
        }

        private static string GetDelete(string tableName)
        {
            string res = "\t\tpublic void Delete()\n\t\t{\n";
            res += String.Format("\t\t\tSetRemoved<{0}>();\n", toUpperCase(tableName));
            res += "\t\t}\n";
            return res;
        }

        private static string GetConstructor(string tableName)
        {
            return "\t\tpublic " + toUpperCase(tableName) + "() {}\n";
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

        private static string GetFieldPrimaryKey(string key, string value, string tableName)
        {
            string res = "";

            res += String.Format(publicField, toUpperCase(key), value);
            res += "\t\t{\n";

            res += "\t\t\tget\n\t\t\t{\n";
            res += String.Format("\t\t\t\treturn {0};\n", "_" + key);
            res += "\t\t\t}\n";

            res += "\t\t\tset\n\t\t\t{\n";
            res += "\t\t\t\tkey = value;\n";
            res += String.Format("\t\t\t\t{0} = value;\n", "_" + key);
            res += "\t\t\t}\n";

            res += "\t\t}\n";

            return res;
        }

        private static string GetFieldForeignKey(string refTableName, string className)
        {
            string res = "";
            string tableName = toUpperCase(refTableName);
            res += String.Format(field, "ref" + tableName, tableName);
            res += String.Format(annotation, "HasOne", "\"" + refTableName + "\"");
            res += String.Format(publicField, "Ref" + tableName, tableName);
            res += "\t\t{\n";
            res += "\t\t\tget\n\t\t\t{\n";
            res += String.Format("\t\t\t\tLoad<{0}>();\n", className);
            res += String.Format("\t\t\t\treturn {0};\n", "ref" + tableName);
            res += "\t\t\t}\n";

            res += "\t\t\tset\n\t\t\t{\n";
            res += String.Format("\t\t\t\tCheckDirty<{0}>();\n", className);
            res += String.Format("\t\t\t\t{0} = value;\n", "ref" + tableName);
            res += "\t\t\t}\n";

            res += "\t\t}\n";

            return res;
        }

        private static string GetFieldTableForeignKey(string tableName, string className)
        {
            string res = "";
            string type = toUpperCase(tableName);

            res += String.Format(field, tableName + "s", "List<" + type + ">");

            res += String.Format(annotation, "HasMany", "\"" + tableName + "\"");

            res += String.Format(publicFieldList, type + "s", type);
            res += "\t\t{\n";

            res += "\t\t\tget\n\t\t\t{\n";
            res += String.Format("\t\t\t\tLoad<{0}>();\n", className);
            res += String.Format("\t\t\t\treturn {0};\n", tableName + "s");
            res += "\t\t\t}\n";

            res += "\t\t\tset\n\t\t\t{\n";
            res += String.Format("\t\t\t\tCheckDirty<{0}>();\n", className);
            res += String.Format("\t\t\t\t{0} = value;\n", tableName + "s");
            res += "\t\t\t}\n";

            res += "\t\t}\n";

            return res;
        }
    }
}
