using System;
using OOPDPFinalProject.ORMFramework.DataAnnotations;

namespace OOPDPFinalProject.ClassGeneration
{
    public class Hocsinh : ORMFramework.DomainObject
    {
        public Hocsinh() { }
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
        private int _hoclop;
        [ForeignKey("lophoc")]
        public int Hoclop
        {
            get
            {
                Load<Hocsinh>();
                return _hoclop;
            }
            set
            {
                CheckDirty<Hocsinh>();
                _hoclop = value;
            }
        }
        private Lophoc refLophoc;
        [HasOne("lophoc")]
        public Lophoc RefLophoc
        {
            get
            {
                Load<Hocsinh>();
                return refLophoc;
            }
            set
            {
                CheckDirty<Hocsinh>();
                refLophoc = value;
            }
        }
        private string _ten;
        public string Ten
        {
            get
            {
                Load<Hocsinh>();
                return _ten;
            }
            set
            {
                CheckDirty<Hocsinh>();
                _ten = value;
            }
        }
        private float _dtb;
        public float Dtb
        {
            get
            {
                Load<Hocsinh>();
                return _dtb;
            }
            set
            {
                CheckDirty<Hocsinh>();
                _dtb = value;
            }
        }
        public void Delete()
        {
            SetRemoved<Hocsinh>();
        }

        public void Print()
        {
            Console.WriteLine("Student ID: " + Id);
            Console.WriteLine("Name: " + Ten);
            Console.WriteLine("Points: " + Dtb.ToString());
            Console.WriteLine("Class ID: " + Hoclop.ToString());
            Console.WriteLine("Hoc lop: " + RefLophoc.Ten);
            Console.WriteLine("\n");
        }
    }
}