using System;
namespace OOPDPFinalProject.ORMFramework.TableSchema
{
    public class Column
    {
        private string columnName;
        private string columnType;
        private bool isPrimaryKey;
        private bool isForeignKey;
        private bool isRequired;
        private string refTable;

        public string Value { get; set; }

        public Column()
        {
        }

        public Column(string cName, string cType)
        {
            columnName = cName;
            columnType = cType;
            isPrimaryKey = false;
            isForeignKey = false;
            isRequired = false;
        }

        public Column(string cName, string cType, bool required)
        {
            columnName = cName;
            columnType = cType;
            isPrimaryKey = false;
            isForeignKey = false;
            isRequired = required;
        }

        public Column(string cName, string cType, bool isPK, bool required)
        {
            columnName = cName;
            columnType = cType;
            isPrimaryKey = isPK;
            if (isPK)
            {
                isRequired = true;
            }
            else
            {
                isRequired = required;
            }
            isForeignKey = false;
        }

        public Column(string cName, string cType, string table)
        {
            columnName = cName;
            columnType = cType;
            isPrimaryKey = false;
            isForeignKey = true;
            refTable = table;
            isRequired = true;
        }

        public Column(string cName, string cType, bool isPK, bool required, string table)
        {
            columnName = cName;
            columnType = cType;
            isPrimaryKey = isPK;
            isForeignKey = true;
            refTable = table;
            isRequired = true;
        }

        public string GetName()
        {
            return columnName;
        }

        public bool IsRequired()
        {
            return isRequired;
        }

        public string GetDataType()
        {
            return columnType;
        }

        public bool IsForeignKey()
        {
            return isForeignKey;
        }

        public bool IsPrimaryKey()
        {
            return isPrimaryKey;
        }

        public string GetRefTable()
        {
            if (isForeignKey)
                return refTable;
            return null;
        }

        public void Print()
        {
            Console.WriteLine("\tColumn name: " + columnName);
            Console.WriteLine("\tColumn type: " + columnType);
            Console.WriteLine("\tRequired: " + (isRequired ? "yes" : "no"));
            Console.WriteLine("\tPrimary Key: " + (isPrimaryKey ? "yes" : "no"));
            Console.WriteLine("\tForeign key: " + (isForeignKey ? ("yes, ref table: " + refTable) : "no"));
            Console.WriteLine("\n\n");
        }
    }
}
