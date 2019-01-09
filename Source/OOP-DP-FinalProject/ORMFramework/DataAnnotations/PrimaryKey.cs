using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class PrimaryKey : ColumnAnnotation
    {
        public bool IsPrimaryKey { get; set; }

        public PrimaryKey()
        {
            IsPrimaryKey = true;
        }

        public PrimaryKey(bool isPrimaryKey)
        {
            IsPrimaryKey = isPrimaryKey;
        }
    }
}
