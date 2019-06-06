using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_Quanlynhasach
{
    class MyCategory
    {
        public static List<Category> mybctgr { get; set; }
        //init book Categories from txt file
        public static void init_MyCategory()
        {        
            List<Category> init = new List<Category>();
            string filePath = @"../../myBookCategories.txt";
            List<string> listbook = new List<string>();
            listbook = File.ReadAllLines(filePath).ToList();
            foreach (string record in listbook)
            {
                string[] book_att = record.Split('#');
                Category a = new Category() { };
                a.catID = int.Parse(book_att[0]);
                a.catName = book_att[1];
                init.Add(a);
            }
            MyCategory.mybctgr = init;
            // print to test, comment before submitting
            // Console.WriteLine("Có tổng cộng {0} thể loại sách.\n", MyCategories.mybctgr.Count); 
        }
        public static void printAllCategory()
        {
            Category.print_header();
            for (int i = 0; i < MyCategory.mybctgr.Count; i++)
            {
                printCategory(i);
            }
        }
        public static void printCategory(int index)
        {
            int i = index;
            // (mã thể loại sách, thể loại sách)
            Console.WriteLine("{0,-10}{1,-35}",
                MyCategory.mybctgr[i].catID,
                MyCategory.mybctgr[i].catName);
        }
        public static int findIndex(int catID)
        {
            int index = 0;//init
            for (int i = 0; i < MyCategory.mybctgr.Count; i++)
            {
                if (MyCategory.mybctgr[i].catID == catID)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
