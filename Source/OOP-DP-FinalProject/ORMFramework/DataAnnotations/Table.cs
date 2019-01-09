using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class Table : DataAnnotation
    {
        public string TableName { get; set; }

        public Table()
        {
        }

        public Table(string name)
        {
            TableName = name;
        }
    }
}
