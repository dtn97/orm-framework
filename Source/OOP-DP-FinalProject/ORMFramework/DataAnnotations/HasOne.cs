using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class HasOne : DataAnnotation
    {
        public HasOne()
        {
        }
        public string TableName { get; set; }
        public HasOne(string tableName)
        {
            TableName = tableName;
        }
    }
}
