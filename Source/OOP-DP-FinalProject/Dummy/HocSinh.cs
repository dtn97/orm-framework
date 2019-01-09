using System;
using OOPDPFinalProject.ORMFramework.DataAnnotations;
using OOPDPFinalProject.ORMFramework;
namespace OOPDPFinalProject.Dummy
{
    [Table("hocsinh")]
    public class Student : DomainObject
    {
        private int _id;
        [Column("id")]
        [PrimaryKey]
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                key = value;
            }

        }


        private string _name = null;
        [Column("ten")]
        public string Name
        {
            get
            {
                Load<Student>();
                return _name;
            }
            set
            {
                CheckDirty<Student>();
                _name = value;
            }
        }

        private float _points;
        [Column("dtb")]
        public float Points
        {
            get
            {
                Load<Student>();
                return _points;
            }
            set
            {
                CheckDirty<Student>();
                _points = value; ;
            }
        }

        private int _classId;

        [ForeignKey("lophoc")]
        [Column("hoclop")]
        public int classID
        {
            get
            {
                Load<Student>();
                return _classId;
            }
            set
            {
                CheckDirty<Student>();
                _classId = value; ;
            }
        }

        private LopHoc lophoc;
        [HasOne("lophoc")]
        public LopHoc Lophoc
        {
            get
            {
                Load<Student>();
                return lophoc;
            }
            set
            {
                CheckDirty<Student>();
                lophoc = value; ;
            }
        }

        public void Delete()
        {
            SetRemoved<Student>();
        }

        public Student(int key)
        {
            this.ID = key;
        }

        public Student()
        {
        }

        public void Print()
        {
            Console.WriteLine("Student ID: " + ID);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Points: " + Points.ToString());
            Console.WriteLine("Class ID: " + classID.ToString());
            Console.WriteLine("Hoc lop: " + Lophoc.Ten);
            Console.WriteLine("\n");
        }
    }
}
