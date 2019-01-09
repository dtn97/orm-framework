using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class HasMany : DataAnnotation
    {
        public HasMany()
        {
        }
        public string TableName { get; set; }
        public HasMany(string tableName)
        {
            TableName = tableName;
        }
    }
}
