using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Npgsql;

namespace OOPDPFinalProject
{
    class MainClass
    {
        /*
        public static void Main(string[] args)
        {
            // ### (a) Ket noi voi CSDL ###
            // -- Postgre SQL Server --
            ORMFramework.Session.Open("Server = thevncore-lab.mooo.com; Port = 7000; Database = OOP-DP; User Id = postgres;Password = hthieu;");
            ORMFramework.Session session = ORMFramework.Session.getCurSession();

            // -- MySQL Server --
            //ORMFramework.Session.Open("Server = thevncore-lab.mooo.com; Port = 7001; Database = selabmysql; User Id = sa;Password = assembler;",ORMFramework.DBMS.MySQL);
            //ORMFramework.Session session = ORMFramework.Session.getCurSession();

            // (e_1) Lay thong tin ve lop hoc co ten la 15 CNTN - su dung where 
            LambdaSqlBuilder.SqlLam<Dummy.LopHoc> query = new LambdaSqlBuilder.SqlLam<Dummy.LopHoc>(c => c.Ten == "CNTN2015");
            Console.WriteLine(query.QueryString);
            Dummy.LopHoc Cntn2015 = session.Table<Dummy.LopHoc>().FindOne(query);
            Cntn2015.Print();

            //### (c) Sua doi ho ten cua sinh vien co MSSV 1512222 ###
            Dummy.Student sv1512222 = session.Table<Dummy.Student>().GetInstance(1512222);
            sv1512222.Name = "Ten moi cua sv 1512222";

            //### (d) Xoa mot hoc sinh co MSSV la 1512873 khoi danh sach ###
            Dummy.Student sv1512873 = session.Table<Dummy.Student>().GetInstance(1512873);
            sv1512873.Delete();

            //Luu toan thay doi len database
            session.CommitAll();

            //### (e_0) Lay toan bo danh sach hoc sinh ###
            List<Dummy.Student> students = session.Table<Dummy.Student>().FindAll();
            foreach (Dummy.Student std in students)
            {
                std.Print();
            }

            //### (c) Sua doi ho ten cua sinh vien MSSV 151222 ve binh thuong ###
            sv1512222.Name = "Do Minh Trieu";

            //### (b) Them moi mot sinh vien vao danh sach - them lai sinh vien vua xoa luc nay co mssv 1512873 ###
            sv1512873 = session.Table<Dummy.Student>().GetInstance(1512873);
            sv1512873.Name = "Tran Quang Mau";
            sv1512873.Points = 5.6f;
            sv1512873.classID = 2;
            session.CommitAll();


            //### (e_2) Lay DS Hoc sinh gioi: ###
            Console.WriteLine("List of good students ");
            LambdaSqlBuilder.SqlLam<Dummy.Student> query2 = new LambdaSqlBuilder.SqlLam<Dummy.Student>(c => c.Points >= 8.0);
            List<Dummy.Student> GoodStudents = session.Table<Dummy.Student>().FindAll(query2);
            foreach (Dummy.Student std in GoodStudents)
            {
                std.Print();
            }
            //### (f) Dong ket noi voi CSDL ###

            //### Query with JOIN ###
            var query3 = new LambdaSqlBuilder.SqlLam<Dummy.Student>().Join<Dummy.LopHoc>((s, c) => s.classID == c.ID)
            .Where(c => c.Ten == "CQ2015");

            List<Dummy.Student> cq2015 = session.Table<Dummy.Student>().FindAll(query3.QueryString, query3.QueryParameters);
            Console.WriteLine("List of studets styding in class name CQ2015");
            foreach (Dummy.Student std in cq2015)
            {
                std.Print();
            }
            //### Query with OFFSET AND LIMIT
            Console.WriteLine("\n\nDemo offset and limit ");
            List<Dummy.Student> student_limit_offset = session.Table<Dummy.Student>().Limit(2).Offset(3).FindAll();
            foreach (Dummy.Student std in student_limit_offset)
            {
                std.Print();
            }
            //###  Query with HAVING AND GROUPBY
            Console.WriteLine("\n\nDemo group by - having - list of class ID with more than 2 students");
            LambdaSqlBuilder.SqlLam<Dummy.Student> groupByQuery = new LambdaSqlBuilder.SqlLam<Dummy.Student>()
                .GroupBy(c => c.classID)
                .Select(c => c.classID).HavingCount(c => c.ID, p => p > 2);

            List<object[]> classMoreThan2students = session.Table<Dummy.Student>().ExecuteQuery(groupByQuery.QueryString, groupByQuery.QueryParameters);
            foreach (object[] obj in classMoreThan2students)
            {
                Console.WriteLine("--> " + obj[0]);
            }

            //###  Query with COUNT/SUM/AVG
            Console.WriteLine("\n\nDemo AVG - calculate GPA of sutdents in each class\n");
            LambdaSqlBuilder.SqlLam<Dummy.Student> avgGPAQuery = new LambdaSqlBuilder.SqlLam<Dummy.Student>()
                .GroupBy(c => c.classID)
                .SelectAverage(c=>c.Points);

            List<object[]> gpas = session.Table<Dummy.Student>().ExecuteQuery(avgGPAQuery);
            foreach (object[] obj in gpas)
            {
                Console.Write("--> ");
                Console.WriteLine(obj[0]);
            }
            session.Close();
        }
        */


        public static void Main(string[] args)
        {
            // ### (a) Ket noi voi CSDL ###
            // -- Postgre SQL Server --
            //ORMFramework.Session.Open("Server = thevncore-lab.mooo.com; Port = 7000; Database = OOP-DP; User Id = postgres;Password = hthieu;");
            //ORMFramework.Session session = ORMFramework.Session.getCurSession();

            // -- MySQL Server --
            ORMFramework.Session.Open("Server = thevncore-lab.mooo.com; Port = 7001; Database = selabmysql; User Id = sa;Password = assembler;", ORMFramework.DBMS.MySQL);
            ORMFramework.Session session = ORMFramework.Session.getCurSession();

            // (e_1) Lay thong tin ve lop hoc co ten la 15 CNTN - su dung where 
            LambdaSqlBuilder.SqlLam<ClassGeneration.Lophoc> query = new LambdaSqlBuilder.SqlLam<ClassGeneration.Lophoc>(c => c.Ten == "CNTN2015");
            Console.WriteLine(query.QueryString);
            ClassGeneration.Lophoc Cntn2015 = session.Table<ClassGeneration.Lophoc>().FindOne(query);
            Cntn2015.Print();

            //### (c) Sua doi ho ten cua sinh vien co MSSV 1512222 ###
            ClassGeneration.Hocsinh sv1512222 = session.Table<ClassGeneration.Hocsinh>().GetInstance(1512222);
            sv1512222.Ten = "Ten moi cua sv 1512222";

            //### (d) Xoa mot hoc sinh co MSSV la 1512873 khoi danh sach ###
            ClassGeneration.Hocsinh sv1512873 = session.Table<ClassGeneration.Hocsinh>().GetInstance(1512873);
            sv1512873.Delete();

            //Luu toan thay doi len database
            session.CommitAll();

            //### (e_0) Lay toan bo danh sach hoc sinh ###
            List<ClassGeneration.Hocsinh> students = session.Table<ClassGeneration.Hocsinh>().FindAll();
            foreach (ClassGeneration.Hocsinh std in students)
            {
                std.Print();
            }

            //### (c) Sua doi ho ten cua sinh vien MSSV 151222 ve binh thuong ###
            sv1512222.Ten = "Do Minh Trieu";

            //### (b) Them moi mot sinh vien vao danh sach - them lai sinh vien vua xoa luc nay co mssv 1512873 ###
            sv1512873 = session.Table<ClassGeneration.Hocsinh>().GetInstance(1512873);
            sv1512873.Ten = "Tran Quang Mau";
            sv1512873.Dtb = 5.6f;
            sv1512873.Hoclop = 2;
            session.CommitAll();


            //### (e_2) Lay DS Hoc sinh gioi: ###
            Console.WriteLine("List of good students ");
            LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh> query2 = new LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh>(c => c.Dtb >= 8.0);
            List<ClassGeneration.Hocsinh> GoodStudents = session.Table<ClassGeneration.Hocsinh>().FindAll(query2);
            foreach (ClassGeneration.Hocsinh std in GoodStudents)
            {
                std.Print();
            }
            //### (f) Dong ket noi voi CSDL ###

            //### Query with JOIN ###
            var query3 = new LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh>().Join<ClassGeneration.Lophoc>((s, c) => s.Hoclop == c.Id)
            .Where(c => c.Ten == "CQ2015");

            List<ClassGeneration.Hocsinh> cq2015 = session.Table<ClassGeneration.Hocsinh>().FindAll(query3.QueryString, query3.QueryParameters);
            Console.WriteLine("List of studets styding in class name CQ2015");
            foreach (ClassGeneration.Hocsinh std in cq2015)
            {
                std.Print();
            }
            //### Query with OFFSET AND LIMIT
            Console.WriteLine("\n\nDemo offset and limit ");
            List<ClassGeneration.Hocsinh> student_limit_offset = session.Table<ClassGeneration.Hocsinh>().Limit(2).Offset(3).FindAll();
            foreach (ClassGeneration.Hocsinh std in student_limit_offset)
            {
                std.Print();
            }
            //###  Query with HAVING AND GROUPBY
            Console.WriteLine("\n\nDemo group by - having - list of class ID with more than 2 students");
            LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh> groupByQuery = new LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh>()
                .GroupBy(c => c.Hoclop)
                .Select(c => c.Hoclop).HavingCount(c => c.Id, p => p > 2);

            List<object[]> classMoreThan2students = session.Table<ClassGeneration.Hocsinh>().ExecuteQuery(groupByQuery.QueryString, groupByQuery.QueryParameters);
            foreach (object[] obj in classMoreThan2students)
            {
                Console.WriteLine("--> " + obj[0]);
            }

            //###  Query with COUNT/SUM/AVG
            Console.WriteLine("\n\nDemo AVG - calculate GPA of sutdents in each class\n");
            LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh> avgGPAQuery = new LambdaSqlBuilder.SqlLam<ClassGeneration.Hocsinh>()
                .GroupBy(c => c.Hoclop)
                .SelectAverage(c => c.Dtb);

            List<object[]> gpas = session.Table<ClassGeneration.Hocsinh>().ExecuteQuery(avgGPAQuery);
            foreach (object[] obj in gpas)
            {
                Console.Write("--> ");
                Console.WriteLine(obj[0]);
            }
            session.Close();
        }

    }
}
