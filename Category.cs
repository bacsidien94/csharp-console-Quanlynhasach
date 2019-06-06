using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace csharp_Quanlynhasach
{
    class Category
    {
        public int catID { get; set; }              //1. Mã Thể Loại Sách
        public string catName { get; set; }         //2. Thể Loại Sách

        public static void print_header()
        {
            Console.WriteLine("\n{0,-10}{1,-35}\n",
                "Ma",
                "Ten The Loai Sach");
        }
        //2019-05-25 added
        public static bool chkcatID(string bookCat)
        {
            bool isok = false;

            int temp;
            if (!int.TryParse(bookCat, out temp) && bookCat != "0")
            {
                Console.WriteLine("Vui long nhap Ma The Loai Sach bang chu so. ");
                isok = false;                            
            }
            for (int i = 0; i < MyCategory.mybctgr.Count; i++)
            {
                if (MyCategory.mybctgr[i].catID == int.Parse(bookCat))
                {
                    Console.WriteLine("\n Ma The Loai Sach {0} da ton tai: {1}. ", bookCat, MyCategory.mybctgr[i].catName);
                    isok = false;
                    break;
                }
                else
                {
                    isok = true;
                }
            }              
            return isok;
        }
        public static void addBookCategories()
        {
            Category newCat = new Category();
            string bookCat = string.Empty;
            Console.Write("Nhap Ma The Loai Sach: ");
            bookCat = Console.ReadLine();
            //validation
            while (!chkcatID(bookCat))
            {
                //Console.Write("Vui long nhap Ma Sach khac bang chu so: ");
                bookCat = Console.ReadLine();
            }
            newCat.catID = int.Parse(bookCat);

            string bookCatName = string.Empty;
            Console.Write("Nhap Ten The Loai Sach: ");
            newCat.catName = Console.ReadLine();
            //validation
            while (newCat.catName == String.Empty||newCat.catName.Length > 30)
            {
                Console.Write("Vui long nhap Ten The Loai Sach toi da 30 ky tu: ");
                newCat.catName = Console.ReadLine();
            }
            string newrecord = $"{newCat.catID}#{newCat.catName}";
            List<string> allrecords = File.ReadAllLines(@"../../myBookCategories.txt").ToList();
            allrecords.Add(newrecord);
            File.WriteAllLines(@"../../myBookCategories.txt", allrecords);
            MyCategory.mybctgr.Add(newCat);
            Console.WriteLine("\nThem The Loai Sach da hoan tat!\n");
            Category.print_header();
            MyCategory.printCategory(MyCategory.mybctgr.Count - 1);
        }
        public static void inqCategorybyName(string inputhere)
        {
            List<int> indexmatched = new List<int>();

            for (int i = 0; i < MyCategory.mybctgr.Count; i++)
            {
                if (MyCategory.mybctgr[i].catName.ToUpper().Contains(inputhere.ToUpper()))
                {
                    indexmatched.Add(i);
                }
                else { }
            }
            if (indexmatched.Count == 0) { Console.WriteLine("\nKhong co thong tin The Loai Sach tren\n"); }
            else
            {              
                Console.WriteLine("\nDanh sach ket qua:");
                Category.print_header();
                //chạy một vòng lặp, lấy các thông tin sách theo list các index khớp với điều kiện và đã được lưu vào indexmatched
                for (int x = 0; x < indexmatched.Count; x++)
                {
                    Console.WriteLine("{0,-20}{1,-35}",
                    MyCategory.mybctgr[indexmatched[x]].catID,
                    MyCategory.mybctgr[indexmatched[x]].catName);
                }
            }
        }
        public static Category inqCategorybyID(int catID)
        {
            bool flag = false;
            Category result = new Category();
            for (int i = 0; i < MyCategory.mybctgr.Count; i++)
            {
                if (MyCategory.mybctgr[i].catID == catID)
                {
                    flag = true;
                    result.catName = MyCategory.mybctgr[i].catName;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            if (flag == false)
            {
                Console.WriteLine("\nKhong co Ma The Loai Sach tren");
                result.catName = "";
            }
            return result;
        }
        public static void updateCategory(int catID)
        {
            string filePath = @"../../myBookCategories.txt";
            List<string> listcategories = new List<string>();
            listcategories = File.ReadAllLines(filePath).ToList();

            Category input = Category.inqCategorybyID(catID);
            if (input.catName != "")
            {
                //get lib index of the candidate book for modifying
                int cat_index = MyCategory.findIndex(catID);
                //chi cho phep sua: ten the loai sach
                Console.WriteLine("Thuc hien viec sua thong tin The Loai Sach co Ma {0}", catID);

                string newbookCat;
                Console.Write("Nhap Ten The Loai Sach moi: "); newbookCat = Console.ReadLine();
                while (newbookCat == String.Empty || newbookCat.Length > 30)
                {
                    Console.Write("Vui long nhap Ten The Loai Sach toi da 30 ky tu: ");
                    newbookCat = Console.ReadLine();
                }
                MyCategory.mybctgr[cat_index].catName = newbookCat;


                listcategories[cat_index] = $"{MyCategory.mybctgr[cat_index].catID}#{MyCategory.mybctgr[cat_index].catName}";
                File.WriteAllLines(filePath, listcategories);

                Console.WriteLine("\nCap nhat thong tin The Loai Sach da hoan tat!\n");
                Console.WriteLine("Cap nhat The Loai Sach moi nhat\n");
                
                MyCategory.printAllCategory();
            }
            else
            {
               // Console.WriteLine("Khong co thong tin The Loai Sach voi Ma {0}", catID);
            }
        }
        public static void deleteCategory(int catID)
        {
            string filePath = @"../../myBookCategories.txt";
            List<string> listcategories = new List<string>();
            listcategories = File.ReadAllLines(filePath).ToList();

            Category input = Category.inqCategorybyID(catID);
            if (input.catName != "")
            {
                //get lib index of the candidate book for modifying
                int cat_index = MyCategory.findIndex(catID);
                //tien hanh tim sach theo thong tin The Loai Sach
                //neu co thi khong cho xoa
                //neu ko co thi tien hanh xoa
                if (Book.inqBookbybookCat(catID) == true)
                {
                    Console.WriteLine("\nKhong the xoa The Loai Sach Nay. Co ton tai thong tin sach voi The Loai Sach nay");
                }
                else
                {
                    MyCategory.mybctgr.Remove(MyCategory.mybctgr[cat_index]);
                    string cat_record = listcategories[cat_index];
                    listcategories.Remove(cat_record);
                    File.WriteAllLines(filePath, listcategories);
                    Console.WriteLine("Cap nhat The Loai Sach moi nhat\n");
                    
                    MyCategory.printAllCategory();
                    Console.WriteLine("Da xoa xong");
                }
            }
            else
            {
              //  Console.WriteLine("Khong co thong tin The Loai Sach voi Ma {0}", catID);
            }

        }
    }
}
