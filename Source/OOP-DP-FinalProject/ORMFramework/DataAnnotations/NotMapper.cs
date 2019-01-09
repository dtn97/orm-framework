using System;
namespace OOPDPFinalProject.ORMFramework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class NotMapper : ColumnAnnotation
    {
        public bool IsNotMapper { get; set; }

        public NotMapper()
        {
            IsNotMapper = true;
        }

        public NotMapper(bool isNotMapper)
        {
            IsNotMapper = isNotMapper;
        }
    }
}
