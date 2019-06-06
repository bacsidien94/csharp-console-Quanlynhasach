using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace csharp_Quanlynhasach
{
    class Book
    {
        /* class sách  thể có 07 attributes
        (mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)       
        */
        // 07 thuộc tính của sách 
        public string bookID { get; set; }              //1. Mã sách
        public string bookName { get; set; }            //2. Tên sách
        public string bookPublisher { get; set; }       //3. Nhà xuất bản     
        public double bookPriceTag { get; set; }        //4. Giá sách
        public int bookYear { get; set; }               //5. Năm xuất bản
        public int bookCat { get; set; }                //6. Thể loại
        /* Tuấn Phạm Minh May 27 +giaptien.nbros@gmail.com Em nên xử lý là nhiều cuốn sách :)*/
        public int bookQty {get; set;}                  //7. Book Quantity in stock
    
        //(mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)
        public static void print_header()
        {
            Console.WriteLine("\n{0,-8}{1,-35}{2,-20}{3,-15}{4,-15}{5,-20}{6,-15}\n",
                "Ma Sach",
                "Ten Sach",
                "Nha Xuat Ban",
                "Gia",
                "Nam Xuat Ban",
                "The Loai",
                "So Luong");
        }
        //hàm validate giá tiền sách
        public static bool chkbookPriceTag(string input)
        {
            double temp;
            if (!double.TryParse(input, out temp) && input != "0")
            {
                Console.WriteLine("Vui long nhap Gia Tien bang chu so toi da 13 ky tu");
                return false;
            }
            //2019-05-22 added
            else if (input == "0")
            {
                Console.WriteLine("Vui long nhap Gia Tien bang chu so toi da 13 ky tu");
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool chkbookQty(string bookQty)
        {
           
            if (Regex.IsMatch(bookQty, "^[0-9]*$"))
            {
                return true;
            }
            else
            {
                Console.Write("Vui long nhap So Luong sach bang chu so ");
                return false;
            }
        }
        public static bool chkbookYear(string input)
        {          
            //2019-05-28 testing
            //if (!Regex.IsMatch(input, "^(19|20)[0-9][0-9]"))
            if (!Regex.IsMatch(input, "^(199|201)[0-9]"))
            {
                Console.Write("Gia tri Nam Xuat Ban khong hop le ");
                return false;
            }
            else
            {
                return true;
            }
        }
        //hàm truy vấn sách theo ID sách để kiểm tra mã sách đã tồn tại hay chưa
        public static bool chkbookID(string bookID)
        {
            bool isfound = false;//init

            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if (MyBookStore.mybs[i].bookID == bookID)
                {
                    isfound = true;
                    Console.WriteLine("\nMa Sach {0} da ton tai", bookID);
                    break;
                }
                else
                {
                    isfound = false;
                }
            }
            return isfound;
        }
        //hàm thêm thông tin sách
        //tinh tuoi sach
        public static int calculatebookYear(int bookYear)
        {
            int rs;
            rs = DateTime.Now.Year - bookYear;
            //Console.WriteLine(rs); test
            return rs;
        }
        public static void addBook()
        {
            string filePath = @"../../myBookstore.txt";
            Book newbook = new Book();
            string inputprice;

            Console.Write("Nhap Ma Sach: "); newbook.bookID = Console.ReadLine();
            //validation
            while (chkbookID(newbook.bookID) || newbook.bookID.Length > 8)
            {
                //Console.Write("Vui lòng nhập mã sách tối đa 08 ký tự hoặc mã sách đã tồn tại ");
                Console.Write("Vui long nhap Ma Sach khac toi da 08 ky tu: ");
                newbook.bookID = Console.ReadLine();
            }

            Console.Write("Nhap Ten Sach: "); newbook.bookName = Console.ReadLine();
            //validation
            while (newbook.bookName.Length > 30)
            {
                Console.Write("Vui long nhap Ten Sach toi da 35 ky tu: "); newbook.bookName = Console.ReadLine();
            }

            Console.Write("Nhap Nha Xuat Ban: "); newbook.bookPublisher = Console.ReadLine();
            // validation
            while (newbook.bookPublisher.Length > 20)
            {
                Console.Write("Vui long nhap Nha Xuat Ban toi da 20 ky tu"); newbook.bookPublisher = Console.ReadLine();
            }

            Console.Write("Nhap Gia Sach: "); inputprice = Console.ReadLine();
            //validation
            // kiểm tra giá trị nhập phải là giá trị số
            // 2019-05-22 nho kiem tra lai truong hop nhap 0
            while (!chkbookPriceTag(inputprice) || inputprice.Length > 13)
            {
                //nếu không phải là số thì yêu cầu nhập lại giá trị hợp lệ
                inputprice = Console.ReadLine();
            }
            newbook.bookPriceTag = double.Parse(inputprice);

            //2019-05-22 them moi nam xuat ban + gia sach + so luong
            // nho kiem tra lai truong hop nhap 0
            Console.Write("Nhap Nam Xuat Ban: "); string inputyear = Console.ReadLine();
            
            while (!chkbookYear(inputyear))
            {
                //Console.WriteLine("Gia tri Nam Xuat Ban khong hop le"); 
                inputyear = Console.ReadLine();
            }
            newbook.bookYear = int.Parse(inputyear);
            MyCategory.printAllCategory();
            Console.Write("Nhap Ma The Loai Sach tuong ung: "); int inputcatID = int.Parse(Console.ReadLine());

            while (Category.inqCategorybyID(inputcatID).catName=="")
            {
              //  Console.WriteLine("Nhap Ma Sach tuong ung: ");
                inputcatID = int.Parse(Console.ReadLine());
            }
            newbook.bookCat = inputcatID;

            Console.Write("Nhap So Luong: "); string bookQty = Console.ReadLine();
            while (!Book.chkbookQty(bookQty))
            {
                bookQty = Console.ReadLine();
            }
            newbook.bookQty = int.Parse(bookQty);
            //(mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)
            string newrecord = $"{newbook.bookID}#{newbook.bookName}#{newbook.bookPublisher}#{newbook.bookPriceTag}#{newbook.bookYear}#{newbook.bookCat}#{newbook.bookQty}";

            List<string> allrecords = File.ReadAllLines(filePath).ToList();
            //thêm bản ghi mới nhất vào lines
            allrecords.Add(newrecord);
            File.WriteAllLines(filePath, allrecords);
            //lưu thông tin sách mới vào nha sach MyBookStore
            MyBookStore.mybs.Add(newbook);

            Console.WriteLine("\nThem thong tin sach da hoan tat!\n");
            //Book.print_header();
            //MyBookStore.printBook(MyBookStore.mybs.Count - 1);
            MyBookStore.printBook(MyBookStore.findIndex(newbook.bookID));
        }
        //ham them sach tu hoa don nhap hang
        public static void addBook(string bookID)
        {
            string filePath = @"../../myBookstore.txt";
            Book newbook = new Book();
            string inputprice;

            while ( newbook.bookID.Length > 8)
            {
                //Console.Write("Vui lòng nhập mã sách tối đa 08 ký tự hoặc mã sách đã tồn tại ");
                Console.Write("Vui long nhap Ma Sach khac toi da 08 ky tu: ");
                newbook.bookID = Console.ReadLine();
            }

            Console.Write("Nhap Ten Sach: "); newbook.bookName = Console.ReadLine();
            //validation
            while (newbook.bookName.Length > 30)
            {
                Console.Write("Vui long nhap Ten Sach toi da 35 ky tu: "); newbook.bookName = Console.ReadLine();
            }

            Console.Write("Nhap Nha Xuat Ban: "); newbook.bookPublisher = Console.ReadLine();
            // validation
            while (newbook.bookPublisher.Length > 20)
            {
                Console.Write("Vui long nhap Nha Xuat Ban toi da 20 ky tu"); newbook.bookPublisher = Console.ReadLine();
            }

            Console.Write("Nhap Gia Sach: "); inputprice = Console.ReadLine();
            //validation
            // kiểm tra giá trị nhập phải là giá trị số
            // 2019-05-22 nho kiem tra lai truong hop nhap 0
            while (!chkbookPriceTag(inputprice) || inputprice.Length > 13)
            {
                //nếu không phải là số thì yêu cầu nhập lại giá trị hợp lệ
                inputprice = Console.ReadLine();
            }
            newbook.bookPriceTag = double.Parse(inputprice);

            //2019-05-22 them moi nam xuat ban + gia sach + so luong
            // nho kiem tra lai truong hop nhap 0
            Console.Write("Nhap Nam Xuat Ban: "); string inputyear = Console.ReadLine();

            while (!chkbookYear(inputyear))
            {
                //Console.WriteLine("Gia tri Nam Xuat Ban khong hop le"); 
                inputyear = Console.ReadLine();
            }
            newbook.bookYear = int.Parse(inputyear);
            MyCategory.printAllCategory();
            Console.Write("Nhap Ma The Loai Sach tuong ung: "); int inputcatID = int.Parse(Console.ReadLine());

            while (Category.inqCategorybyID(inputcatID).catName == "")
            {
                //  Console.WriteLine("Nhap Ma Sach tuong ung: ");
                inputcatID = int.Parse(Console.ReadLine());
            }
            newbook.bookCat = inputcatID;

            newbook.bookQty = 0;
            //(mã, tên sách, nxb, giá, năm xuất bản, thể loại, số lượng)
            string newrecord = $"{newbook.bookID}#{newbook.bookName}#{newbook.bookPublisher}#{newbook.bookPriceTag}#{newbook.bookYear}#{newbook.bookCat}#{newbook.bookQty}";
            
            List<string> allrecords = File.ReadAllLines(filePath).ToList();
            //thêm bản ghi mới nhất vào lines
            allrecords.Add(newrecord);
            File.WriteAllLines(filePath, allrecords);
            //lưu thông tin sách mới vào nha sach MyBookStore
            MyBookStore.mybs.Add(newbook);

            //Console.WriteLine("\nThem thong tin sach da hoan tat!\n");
            //Book.print_header();
            //MyBookStore.printBook(MyBookStore.mybs.Count - 1);
            //return newbook;
        }
        //hàm truy vấn sách theo tên sách cho phép tìm kiem thông tin sach có tên gần đúng với nhập liệu       
        public static void inqBookbyName(string inputhere)
        {
            List<int> indexmatched = new List<int>();

            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if (MyBookStore.mybs[i].bookName.ToUpper().Contains(inputhere.ToUpper()))
                {
                    indexmatched.Add(i);
                }
                else { }
            }
            //nếu không có kết quả sách nào khớp với từ khoá tìm kiếm, in ra dòng kết quả như sau
            if (indexmatched.Count == 0) { Console.WriteLine("\nKhong co thong tin sach tren\n"); }
            //nếu có kết quả sách khớp với từ khoá tìm kiếm, lần lượt in thông tin sách ra
            else
            {
                //nếu có kết quả thì in ra thông tin sách có thứ tự như sau
                Console.WriteLine("\nDanh sach ket qua:");
                Book.print_header();
                //chạy một vòng lặp, lấy các thông tin sách theo list các index khớp với điều kiện và đã được lưu vào indexmatched
                for (int x = 0; x < indexmatched.Count; x++)
                {                
                    Console.WriteLine("{0,-8}{1,-35}{2,-20}{3,-15}{4,-15}{5,-20}{6,-15}",
                       MyBookStore.mybs[indexmatched[x]].bookID,
                       MyBookStore.mybs[indexmatched[x]].bookName,
                       MyBookStore.mybs[indexmatched[x]].bookPublisher,
                       GeneralCode.converPricetag(MyBookStore.mybs[indexmatched[x]].bookPriceTag),
                       MyBookStore.mybs[indexmatched[x]].bookYear,
                       Category.inqCategorybyID(MyBookStore.mybs[x].bookCat).catName,
                       MyBookStore.mybs[x].bookQty
                       );
                }
            }
        }
        //tim sach da xuat ban tren 3 nam
        public static void inqBookbybookYear(int bookYear)
        {
            List<int> indexmatched = new List<int>();

            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if ( Book.calculatebookYear(MyBookStore.mybs[i].bookYear) > 3)
                {
                    indexmatched.Add(i);
                }
                else { }
            }
            //nếu không có kết quả sách nào khớp với từ khoá tìm kiếm, in ra dòng kết quả như sau
            if (indexmatched.Count == 0) { Console.WriteLine("\nKhong co thong tin sach cu da xuat ban tren 3 nam\n"); }
            //nếu có kết quả sách khớp với từ khoá tìm kiếm, lần lượt in thông tin sách ra
            else
            {
                //nếu có kết quả thì in ra thông tin sách có thứ tự như sau
                Console.WriteLine("\nDanh sach ket qua sach cu da xuat ban tren 3 nam:");
                Book.print_header();
                //chạy một vòng lặp, lấy các thông tin sách theo list các index khớp với điều kiện và đã được lưu vào indexmatched
                for (int x = 0; x < indexmatched.Count; x++)
                {
                    Console.WriteLine("{0,-8}{1,-35}{2,-20}{3,-15}{4,-15}{5,-20}{6,-15}",
                       MyBookStore.mybs[indexmatched[x]].bookID,
                       MyBookStore.mybs[indexmatched[x]].bookName,
                       MyBookStore.mybs[indexmatched[x]].bookPublisher,
                       GeneralCode.converPricetag(MyBookStore.mybs[indexmatched[x]].bookPriceTag),
                       MyBookStore.mybs[indexmatched[x]].bookYear,
                       Category.inqCategorybyID(MyBookStore.mybs[x].bookCat).catName,
                       MyBookStore.mybs[x].bookQty
                       );
                }
            }
        }
        //hàm truy vấn sách theo ID sách kq trả về là một class object dung cho chuc nang sua / xoa       
        public static Book inqBookbyID(string bookID)
        {
            bool apply_chk = false;
            Book result = new Book();
            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if (MyBookStore.mybs[i].bookID == bookID)
                {
                    apply_chk = true;
                    //lấy thêm các thông tin còn lại                   
                    result.bookID = bookID;
                    result.bookName = MyBookStore.mybs[i].bookName;
                    result.bookPublisher = MyBookStore.mybs[i].bookPublisher;
                    result.bookPriceTag = MyBookStore.mybs[i].bookPriceTag;
                    result.bookYear = MyBookStore.mybs[i].bookYear;
                    result.bookCat = MyBookStore.mybs[i].bookCat;
                    result.bookQty = MyBookStore.mybs[i].bookQty;
                    break;
                }
                else
                {
                    apply_chk = false;
                }
            }
            if (apply_chk == false)
            {
                Console.WriteLine("\nKhong co Ma Sach tren");
                result.bookName = "";
            }
            return result;
        }
        //hàm cho phép chỉnh sửa thông tin sách theo mã sách
        public static void updateBook(string bookID)
        {
            string filePath = @"../../myBookstore.txt";
            List<string> listbook = new List<string>();
            listbook = File.ReadAllLines(filePath).ToList();

            Book input = Book.inqBookbyID(bookID);
            if (input.bookName != "")
            {
                //get lib index of the candidate book for modifying
                int lib_index = MyBookStore.findIndex(input.bookID);
                //chi cho phep sua: gia sach / so luong
                Console.WriteLine("Thuc hien viec sua thong tin sach co Ma Sach {0}", bookID);

                string inputprice;
                Console.Write("Nhap Gia Sach: "); inputprice = Console.ReadLine();
                while (!chkbookPriceTag(inputprice) || inputprice.Length > 13)
                {
                    inputprice = Console.ReadLine();
                }
                MyBookStore.mybs[lib_index].bookPriceTag = double.Parse(inputprice);

                Console.Write("Nhap So Luong: "); string bookQty = Console.ReadLine();
                while (!Book.chkbookQty(bookQty))
                {
                    bookQty = Console.ReadLine();
                }
                MyBookStore.mybs[lib_index].bookQty = int.Parse(bookQty);

                listbook[lib_index] = $"{MyBookStore.mybs[lib_index].bookID}#{MyBookStore.mybs[lib_index].bookName}#{MyBookStore.mybs[lib_index].bookPublisher}#{MyBookStore.mybs[lib_index].bookPriceTag}#{MyBookStore.mybs[lib_index].bookYear}#{ MyBookStore.mybs[lib_index].bookCat}#{MyBookStore.mybs[lib_index].bookQty}";
                File.WriteAllLines(filePath, listbook);

                Console.WriteLine("\nCap nhat thong tin sach da hoan tat!\n");
                Console.WriteLine("Cap nhat Thu Vien Sach moi nhat\n");
               
                MyBookStore.printAllBook();
            }
            else
            {
                Console.WriteLine("Khong co thong tin sach voi Ma Sach {0}", bookID);
            }
        }     
        //hàm chi cho phép update so luong sach = 0    
        //vi thong tin sach con duoc dung trong thong tin hoa don
        public static void deleteBook(string bookID)
        {
            string filePath = @"../../myBookstore.txt";
            List<string> listbook = new List<string>();
            listbook = File.ReadAllLines(filePath).ToList();

            Book input = Book.inqBookbyID(bookID);
            if (input.bookName != "")
            {
                //get lib index of the candidate book for modifying
                int lib_index = MyBookStore.findIndex(input.bookID);
                MyBookStore.mybs[lib_index].bookQty = 0;

                listbook[lib_index] = $"{MyBookStore.mybs[lib_index].bookID}#{MyBookStore.mybs[lib_index].bookName}#{MyBookStore.mybs[lib_index].bookPublisher}#{MyBookStore.mybs[lib_index].bookPriceTag}#{MyBookStore.mybs[lib_index].bookYear}#{ MyBookStore.mybs[lib_index].bookCat}#{MyBookStore.mybs[lib_index].bookQty}";
                File.WriteAllLines(filePath, listbook);
                //MyBookStore.mybs.Remove(MyBookStore.mybs[lib_index]);
                //string book_record = listbook[lib_index];
                //listbook.Remove(book_record);
                //File.WriteAllLines(filePath, listbook);

                Console.WriteLine("Da xoa thong tin sach voi Ma Sach {0} ", input.bookID);
                Console.WriteLine("Cap nhat Thu Vien Sach moi nhat\n");
                
                MyBookStore.printAllBook();
            }
            else { Console.WriteLine("Khong co thong tin sach voi Ma Sach {0}", bookID); }
        }
        //test
        public static void updateBookQty(string bookID, int newQty)
        {
            string filePath = @"../../myBookstore.txt";
            List<string> listbook = new List<string>();
            listbook = File.ReadAllLines(filePath).ToList();
            Book input = Book.inqBookbyID(bookID);          
            //get lib index of the candidate book for modifying
            int lib_index = MyBookStore.findIndex(input.bookID);
            //chi cho phep sua: so luong
            MyBookStore.mybs[lib_index].bookQty = newQty;
            listbook[lib_index] = $"{MyBookStore.mybs[lib_index].bookID}#{MyBookStore.mybs[lib_index].bookName}#{MyBookStore.mybs[lib_index].bookPublisher}#{MyBookStore.mybs[lib_index].bookPriceTag}#{MyBookStore.mybs[lib_index].bookYear}#{ MyBookStore.mybs[lib_index].bookCat}#{MyBookStore.mybs[lib_index].bookQty}";
            File.WriteAllLines(filePath, listbook);
            //Console.WriteLine("Cap nhat so luong sach thanh cong"); // test
        }
        //tested
        public static bool inqBookbybookCat(int bookCat)
        {
            bool is_found = false;
            Book result = new Book();
            for (int i = 0; i < MyBookStore.mybs.Count; i++)
            {
                if (MyBookStore.mybs[i].bookCat == bookCat)
                {
                    is_found = true;
                    /*
                    lấy thêm các thông tin còn lại                   
                    result.bookID = bookID;
                    result.bookName = MyBookStore.mybs[i].bookName;
                    result.bookPublisher = MyBookStore.mybs[i].bookPublisher;
                    result.bookPriceTag = MyBookStore.mybs[i].bookPriceTag;
                    result.bookYear = MyBookStore.mybs[i].bookYear;
                    result.bookCat = MyBookStore.mybs[i].bookCat;
                    result.bookQty = MyBookStore.mybs[i].bookQty;
                    */
                    //Console.WriteLine("\nCo thong tin sach voi The Loai Sach nay");//to test
                    break;
                }
                else
                {
                    is_found = false;
                    //Console.WriteLine("\nKhong coo thong tin sach voi The Loai Sach nay");//to test
                }
            }
            
            return is_found;
        }
    }

}
