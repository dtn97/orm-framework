using System;
using System.Collections.Generic;
using OOPDPFinalProject.ORMFramework.DataAnnotations;

namespace OOPDPFinalProject.ClassGeneration
{
    public class Lophoc : ORMFramework.DomainObject
    {
        public Lophoc() { }
        private int _id;
        [PrimaryKey(true)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                key = value;
                _id = value;
            }
        }
        private string _ten;
        public string Ten
        {
            get
            {
                Load<Lophoc>();
                return _ten;
            }
            set
            {
                CheckDirty<Lophoc>();
                _ten = value;
            }
        }
        private string _gvcn;
        public string Gvcn
        {
            get
            {
                Load<Lophoc>();
                return _gvcn;
            }
            set
            {
                CheckDirty<Lophoc>();
                _gvcn = value;
            }
        }
        private List<Hocsinh> hocsinhs;
        [HasMany("hocsinh")]
        public List<Hocsinh> Hocsinhs
        {
            get
            {
                Load<Lophoc>();
                return hocsinhs;
            }
            set
            {
                CheckDirty<Lophoc>();
                hocsinhs = value;
            }
        }
        public void Delete()
        {
            SetRemoved<Lophoc>();
        }

        public void Print()
        {
            Console.WriteLine("--- In thong tin lop hoc ---");
            Console.WriteLine("ID     = " + Id.ToString());
            Console.WriteLine("TenLop = " + Ten);
            Console.WriteLine("GVCN   = " + Gvcn);
            Console.WriteLine("- DS Hoc sinh - ");
            if (Hocsinhs != null)
            {
                foreach (Hocsinh student in Hocsinhs)
                {
                    student.Print();
                    Console.WriteLine("\n");
                }
            }
            Console.WriteLine("\n\n");
        }
    }
}