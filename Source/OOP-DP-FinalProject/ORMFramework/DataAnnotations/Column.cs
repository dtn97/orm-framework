using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class Column : ColumnAnnotation
    {
        public string ColumnName { get; set; }

        public Column()
        {
        }

        public Column(string name)
        {
            ColumnName = name;
        }
    }
}
