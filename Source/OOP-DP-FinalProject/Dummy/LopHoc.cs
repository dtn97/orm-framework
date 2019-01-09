using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.DataAnnotations;
using OOPDPFinalProject.ORMFramework;
namespace OOPDPFinalProject.Dummy
{
    [Table("lophoc")]
    public class LopHoc : DomainObject
    {
        public LopHoc()
        {
        }

        private int id;
        [PrimaryKey]
        public int ID
        {
            set
            {
                id = value;
                key = id;
            }
            get
            {
                Load<LopHoc>();
                return id;
            }
        }

        private string ten;
        public string Ten
        {
            get
            {
                Load<LopHoc>();
                return ten;
            }
            set
            {
                ten = value;
            }
        }

        private string gvcn;
        public string GVCN
        {
            get
            {
                Load<LopHoc>();
                return gvcn;
            }
            set
            {
                gvcn = value;
            }
        }

        private List<Student> students;
        [HasMany(TableName = "hocsinh")]
        public List<Student> Students
        {
            get
            {
                Load<LopHoc>();
                return students;
            }
            set
            {
                students = value;
            }
        }

        public void Print()
        {
            Console.WriteLine("--- In thong tin lop hoc ---");
            Console.WriteLine("ID     = " + ID.ToString());
            Console.WriteLine("TenLop = " + Ten);
            Console.WriteLine("GVCN   = " + GVCN);
            Console.WriteLine("- DS Hoc sinh - ");
            if (Students != null)
            {
                foreach (Student student in Students)
                {
                    student.Print();
                    Console.WriteLine("\n");
                }
            }
            Console.WriteLine("\n\n");
        }
    }
}
