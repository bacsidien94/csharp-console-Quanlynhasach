using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_Quanlynhasach
{
    class MyBookStore
    {
        public static List<Book> mybs { get; set; }

        //init a library of books from myBookstore.txt file
        public static void init_MyBookstore()
        {
            List<Book> init = new List<Book>();
            string filePath = @"../../myBookstore.txt";
            List<string> listbook = new List<string>();
            listbook = File.ReadAllLines(filePath).ToList();
            foreach (string record in listbook)
            {
                //(mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)
                // print to test, comment before submitting
                // Console.WriteLine(record);
                string[] book_att = record.Split('#');

                Book a = new Book() { };
                //nho kiem tra du 7 thuoc tinh cua Book
                a.bookID = book_att[0];
                a.bookName = book_att[1];
                a.bookPublisher = book_att[2];
                a.bookPriceTag = double.Parse(book_att[3]);
                a.bookYear = int.Parse(book_att[4]);
                a.bookCat =  int.Parse(book_att[5]);
                a.bookQty = int.Parse(book_att[6]);
                init.Add(a);
            }
            MyBookStore.mybs = init;
            // print to test, comment before submitting
            // Console.WriteLine("Total {0} book(s).\n", MyBookStore.mybs.Count); 
        }
        //print all books 
        public static void printAllBook()
        {
            Book.print_header();
            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                printBook(i);
                /*
                if (MyBookStore.mybs[i].bookQty==0)
                {
                    //loai tru nhung cuon sach khong con trong kho
                }
                else
                {
                    printBook(i);
                }*/
            }
        }
         public static void printRemainBook()
        {
            Book.print_header();
            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                
                if (MyBookStore.mybs[i].bookQty==0)
                {
                    //loai tru nhung cuon sach khong con trong kho
                }
                else
                {
                    printBook(i);
                }
            }
        }
        //print a specific book
        public static void printBook(int index)
        {
            int i = index;
            // (mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)
            Console.WriteLine("{0,-8}{1,-35}{2,-20}{3,-15}{4,-15}{5,-20}{6,-15}",
                MyBookStore.mybs[i].bookID,
                MyBookStore.mybs[i].bookName,
                MyBookStore.mybs[i].bookPublisher,
                GeneralCode.converPricetag(MyBookStore.mybs[i].bookPriceTag),
                MyBookStore.mybs[i].bookYear,
                Category.inqCategorybyID(MyBookStore.mybs[i].bookCat).catName,
                MyBookStore.mybs[i].bookQty
                );          
        }
        //get index
        public static int findIndex(string bookID)
        {
            int index = 0;//init
            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if (MyBookStore.mybs[i].bookID == bookID)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
