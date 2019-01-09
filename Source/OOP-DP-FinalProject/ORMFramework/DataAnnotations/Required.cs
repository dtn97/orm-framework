using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class Required : ColumnAnnotation
    {
        public bool IsRequired { get; set; }
        public Required()
        {
            IsRequired = true;
        }
        public Required(bool required)
        {
            IsRequired = required;
        }
    }
}
