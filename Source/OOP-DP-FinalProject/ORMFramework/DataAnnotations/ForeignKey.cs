using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class ForeignKey : ColumnAnnotation
    {
        public string RefTable { get; set; }

        public ForeignKey(string name)
        {
            RefTable = name;
        }
    }
}
