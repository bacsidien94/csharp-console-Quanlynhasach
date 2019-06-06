using System;

namespace csharp_Quanlynhasach
{
    class Program
    {
        static void Main(string[] args)
        {
            initial();
            //testing new function here
            //test();
           
            //chương trình bắt đầu 
            menuScreen();
        }
        static void initial()
        {
            Console.SetWindowSize(180, 30);
            //khoi gan thong tin nha sach: sach / the loai sach / hoa don ban sach / hoa don nhap sach tu file txt file
            MyBookStore.init_MyBookstore();
            MyCategory.init_MyCategory();
            MyInvoice.init_invoice();
            MyInvoice.init_invDetail();
        }
        static void menuScreen()
        {
            string selection = string.Empty;
            int value = 99;
            Console.WriteLine("---CHUONG TRINH QUAN LY NHA SACH---\n\n"
                         + "Danh Sach Cac Chuc Nang:\n"
                         + "\t1. Them Sach\n"
                         + "\t2. Xoa Sach\n"
                         + "\t3. Sua Sach\n"
                         + "\t4. Tim Kiem Sach\n"

                         + "\t5. Them The Loai Sach\n"
                         + "\t6. Xoa The Loai Sach\n"
                         + "\t7. Sua The Loai Sach\n"
                         + "\t8. Tim Kiem The Loai Sach\n"

                         + "\t9. Them Hoa Don Ban Sach\n"
                         + "\t10. Xoa Hoa Don Ban Sach\n"
                         + "\t11. Sua Hoa Don Ban Sach\n"
                         + "\t12. Tim Kiem Hoa Don Ban Sach\n"

                         + "\t13. Them Hoa Don Nhap Sach\n"
                         + "\t14. Xoa Hoa Don Nhap Sach\n"
                         + "\t15. Sua Hoa Don Nhap Sach\n"
                         + "\t16. Tim Kiem Hoa Don Nhap Sach\n"

                         + "\t17. Thong Ke Sach Theo The Loai\n"
                         + "\t18. Thong Ke Sach Cu Xuat Ban Tren 3 Nam\n"
                         + "\t0. Thoat\n");
            do
            {
                // kiểm tra dữ liệu đầu vào
                // nếu thoả thì cho phép truyền giá trị này vào hàm tương ứng (1-11)
                // nên gán lại giá trị selection trong mỗi lần chạy vòng lặp, nếu để bên ngoài, vòng lặp sẽ chạy infinite sẽ không đạt yêu cầu
                Console.Write("\nVui long chon chuc nang can thuc hien: ");
                selection = Console.ReadLine();
                if (selection == "0")
                { // nếu là 0 thì kết thúc chương trình
                    Console.Write("\nKet thuc chuong trinh");
                    Console.ReadLine();
                }
                else if (!int.TryParse(selection, out value))
                {// nếu không phải số thì yêu cầu nhập lại
                    Console.Write("\nVui long chon chuc nang can thuc hien: ");
                    Console.ReadLine();
                }
                else if (int.Parse(selection) < 0 || int.Parse(selection) > 19)
                {// nếu là số nhưng không có chức năng cũng yêu cầu nhập lại
                    Console.Write("\nVui long chon chuc nang can thuc hien: ");
                    Console.ReadLine();
                }
                else
                {
                    begins(int.Parse(selection));
                }
            } while (selection != "0");
        }
        //hàm gọi 18 chức năng của console
        static void begins(int slt)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            switch (slt)
            {
                // 2019-05-24: done
                case 1:
                    {
                        Console.WriteLine("\nChuc nang them sach");
                        Book.addBook();
                        break;
                    }
                // 2019-05-24: done
                case 2:
                    {
                        Console.WriteLine("\nChuc nang xoa sach");                                              
                        MyBookStore.printAllBook();
                        Console.Write("\nNhap Ma sach tuong ung voi Ten Sach can xoa: ");                      
                        string inputvar = Console.ReadLine();
                        Book.deleteBook(inputvar);
                        break;
                    }
                // 2019-05-24: done
                case 3:
                    {
                        Console.WriteLine("\nChuc nang sua sach");                                          
                        MyBookStore.printAllBook();
                        Console.Write("\nNhap Ma Sach tuong ung voi Ten Sach can sua: ");
                        string inputvar = Console.ReadLine();
                        Book.updateBook(inputvar);
                        break;
                    }
                // 2019-05-24: done
                case 4:
                    {
                        Console.WriteLine("\nChuc nang tim kiem Sach");                                             
                        //MyBookStore.inquire_all_book(); // in ra truoc de test, remove truoc khi commit
                        Console.Write("\nNhap Ten Sach can tim: ");
                        string inputvar = Console.ReadLine();
                        Book.inqBookbyName(inputvar);
                        break;
                    }

                case 5:
                    {
                        Console.WriteLine("\nChuc nang them The Loai Sach");
                        Category.addBookCategories();
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("\nChuc nang xoa The Loai Sach");
                        MyCategory.printAllCategory();
                        Console.Write("Nhap Ma The Loai Sach can xoa: ");
                        int inputvar = int.Parse(Console.ReadLine());
                        Category.deleteCategory(inputvar);
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("\nChuc nang sua The Loai Sach"); 
                        MyCategory.printAllCategory();
                        Console.Write("Nhap Ma The Loai Sach can sua: ");
                        int inputvar = int.Parse(Console.ReadLine());
                        Category.updateCategory(inputvar);
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("\nChuc nang tim kiem The Loai Sach");
                        Console.Write("Nhap The Loai Sach can tim: ");
                        string inputvar = Console.ReadLine();
                        Category.inqCategorybyName(inputvar);
                        break;
                    }

                case 9:
                    {
                        Console.WriteLine("\nChuc nang them Hoa Don Ban Sach");
                        Invoice.issueSalesInvoice();
                        break;
                    }
                case 10:
                    {
                        Console.WriteLine("\nChuc nang xoa Hoa Don Ban Sach");
                        Console.Write("Nhap Hoa Don Ban Sach can xoa: ");
                        string inputvar = Console.ReadLine();
                        Invoice.deleteInvoice(inputvar);
                        break;
                    }
                case 11:
                    {
                        Console.WriteLine("\nChuc nang sua Hoa Don Ban Sach");
                        Console.Write("Nhap Hoa Don Ban Sach can sua: ");
                        string inputvar = Console.ReadLine();
                        Invoice.updateInvoice(inputvar);
                        break;
                    }
                case 12:
                    {
                        Console.WriteLine("\nChuc nang tim kiem Hoa Don Ban Sach");
                        Console.Write("Nhap Hoa Don Ban Sach can tim: ");
                        string inputvar = Console.ReadLine();
                        Invoice.inqInvoice(inputvar);
                        break;
                    }

                case 13:
                    {
                        Console.WriteLine("\nChuc nang them Hoa Don Nhap Sach");
                        Invoice.issuePurchaseInvoice();
                        break;
                    }
                case 14:
                    {
                        Console.WriteLine("\nChuc nang xoa Hoa Don Nhap Sach");
                        Console.Write("Nhap Hoa Don Nhap Sach can xoa: ");
                        string inputvar = Console.ReadLine();
                        Invoice.deleteInvoice(inputvar);
                        break;
                    }
                case 15:
                    {
                        Console.WriteLine("\nChuc nang sua Hoa Don Nhap Sach");
                        Console.Write("Nhap Hoa Don Nhap Sach can sua: ");
                        string inputvar = Console.ReadLine();
                        Invoice.updateInvoice(inputvar);
                        break;
                    }
                case 16:
                    {
                        Console.WriteLine("\nChuc nang tim kiem Hoa Don Nhap Sach");
                        Console.Write("Nhap Hoa Don Nhap Sach can tim: ");
                        string inputvar = Console.ReadLine();
                        Invoice.inqInvoice(inputvar);
                        break;
                    }

                case 17:
                    {
                        Console.WriteLine("\nChuc nang thong ke so sach con lai trong nha sach theo the loai");
                        MyBookStore.printRemainBook();
                        break;
                    }
                case 18:
                    {
                        Console.WriteLine("\nChuc nang thong ke so sach cu da xuat ban tren 3 nam");
                        Book.inqBookbybookYear(3);
                        break;
                    }
                    // default: break;
            }
        }
        //self test befor submit
        static void test()
        {
            /*
             * Console.Write("Nhap Hoa Don Ban Sach can tim: ");
            string inputvar = Console.ReadLine();
            Invoice.inqInvoice(inputvar);
            */
            //Invoice.inqInvoice("0005");
            Book.inqBookbybookYear(3);
            //Book.calculatebookYear(2014);
            // MyInvoice.find_InvIndex("00005");
            //Invoice.deleteInvoice("0005");

            //Book.deleteBook("00005");
            //Book.updateBookQty("00002", 199);
            //Invoice.issuePurchaseInvoice();
            //MyCategory.printAllCategory();
            //MyBookStore.printAllBook();
            //Book.deleteBook("9999");
            //Book.inqBookbybookCat(5);
            //Category.updateCategory(88);
            //Category.updateCategory(88);
            //Category.deleteCategory(25);
            //Category.deleteCategory(5);
            //Console.Write("Nhap The Loai Sach can tim: ");
            //string inputvar = Console.ReadLine();
            //BookCategory.inqbookCatbyName(inputvar);
            //Console.WriteLine(DateTime.Now.ToShortDateString());
            //InvoiceDetail.calculateAmount(10, 300000d);
            //Invoice.issueSalesInvoice();
            //MyInvoice.printInv("0006");
            //MyInvoice.inquireInv("00001");
            // MyInvoice.inquireInv("0040"); 
            //Invoice.issuePurchaseInvoice();
            //Invoices.printInvoice(MyInvoices.myinv[0]); 
            /*
            Console.Write("check book year: ");string inputby = Console.ReadLine();
            while (!Book.validatebookYear(inputby))
            {
                inputby = Console.ReadLine();
            }
            */
            /*
            Console.Write("check book qty: "); string inputbqty = Console.ReadLine();
            while (!Book.validatebookQty(inputbqty))
            {
                inputbqty = Console.ReadLine();
            }
            */
            /*
             if (!BookCategories.validateCatID("15"))
            {
                Console.WriteLine("false");
                Console.ReadLine();
            }
            else { Console.WriteLine("true"); Console.ReadLine(); }
            */
            //MyBookStore.inquire_all_book();
            //MyCategories.inquire_all_book_cat();
        }
    }
}

