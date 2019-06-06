using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_Quanlynhasach
{
    class Invoice
    {

        public string invNo { get; set; }              //1. Số Hoá Đơn
        public int invType { get; set; }               //2. Loại Hoá Đơn  1-Bán (Sales)/ 2-Nhập (Purchase)
        public double invTotalAmount { get; set; }     //3. Thành Tiền - auto
        public string issueDate { get; set; }          //4. Ngày Phát Hành - auto
        public int invStatus { get; set; }            //5. Trạng Thái: 1-da phat hanh /2-da xoa
        public string Issuer { get; set; }             //6. Nguoi lap hoa don


        public static bool chkinvNo(string invNo)
        {
            bool isfound = false;
            /*
            int temp;
            if (!int.TryParse(invNo, out temp) && invNo != "0")
            {
                Console.WriteLine("Vui long nhap lai So Hoa Don khac toi da 08 ky tu: ");
                isfound = true;
            }*/
            for (int i = 0; i < MyInvoice.myinv.Count; i++)
            {
                if (MyInvoice.myinv[i].invNo == invNo)
                {
                    isfound = true;
                    //Console.WriteLine("\n So Hoa Don {0} da ton tai. Vui long nhap So Hoa Don khac: ", invNo);                    
                    break;
                }
                else
                {
                    isfound = false;
                }
            }
            return isfound;
        }                      
        public static void issueSalesInvoice()
        {
            string fPathInv = @"../../myInvoice.txt";
            string fPathInvDetail = @"../../myInvoiceDetail.txt";
            //tam luu tat ca du lieu trong txt file vao List<string>
            List<string> all_inv_records = File.ReadAllLines(fPathInv).ToList();
            List<string> all_invDetail_records = File.ReadAllLines(fPathInvDetail).ToList();

            Invoice newInv = new Invoice();
            string invNo = string.Empty;
            string bookID = string.Empty;
            string Issuer = string.Empty;
            int bookQty; double bookPriceTag;

            Console.Write("Nhap So Hoa Don "); invNo = Console.ReadLine();
            while (chkinvNo(invNo)||invNo.Length > 8)
            {
                Console.Write("\n So Hoa Don {0} da ton tai. Vui long nhap So Hoa Don khac: ", invNo);
                invNo = Console.ReadLine();
            }
            
            newInv.invNo = invNo;
            newInv.invType = 1; //Ban
            newInv.issueDate = DateTime.Now.ToShortDateString(); // chi don gian la lay thoi gian hien hanh
            newInv.invTotalAmount = 0; // init      
            newInv.invStatus = 1;
            Console.Write("Nhap Ten Nguoi Lap Hoa Don: ");
            Issuer = Console.ReadLine();
            while (Issuer.Length > 20)
            {
                Console.Write("\nVui long nhap ten duoi 20 ky tu");
                Issuer = Console.ReadLine();
            }
            newInv.Issuer = Issuer;

            //nhap thong tin chi tiet hoa don
            int selection = 1;          
            while (selection == 1)          
            {
                InvoiceDetail newInvDetails = new InvoiceDetail();
                newInvDetails.invNo = newInv.invNo;
                Console.Write("Nhap thong tin Chi Tiet Hoa Don ");
                MyBookStore.printAllBook();

                //kiem tra ma sach can ban can phai co trong book store 
                Console.Write("Nhap Ma Sach can ban ");
                bookID =  Console.ReadLine();
                Book rs = Book.inqBookbyID(bookID);
                while (rs.bookName == "")
                {
                    Console.Write("Nhap Ma Sach can ban ");
                    bookID = Console.ReadLine();
                    rs = Book.inqBookbyID(bookID);
                }
                
                bookQty = Book.inqBookbyID(bookID).bookQty;
                bookPriceTag = Book.inqBookbyID(bookID).bookPriceTag;
                newInvDetails.invUnit = bookID;
                newInvDetails.invUnitPrice = bookPriceTag;
                Console.Write("\nGia 1 cuon sach la " + GeneralCode.converPricetag(newInvDetails.invUnitPrice));
                string invUnitQty = string.Empty;
                Console.Write("\nNhap So Luong ");
                invUnitQty = Console.ReadLine();
                while (!InvoiceDetail.chkQty(invUnitQty))
                {
                    invUnitQty = Console.ReadLine();
                }

                //cap nhat so luong ban ra so luong sach hien co
                int newQty = bookQty;
                
                    // validate so luong sach & - so luong sach  
                    if (bookQty < int.Parse(invUnitQty))
                    {
                        Console.Write("\nVuot qua so luong sach hien co : {0}", bookQty);
                        Console.Write("\nBan chi duoc ban toi da : {0} cuon sach", bookQty);
                        newInvDetails.invUnitQty = bookQty;
                        Book.updateBookQty(Book.inqBookbyID(bookID).bookID, 0);
                    }
                    else {
                        newInvDetails.invUnitQty = int.Parse(invUnitQty);
                        newQty -= int.Parse(invUnitQty);
                        Book.updateBookQty(Book.inqBookbyID(bookID).bookID, newQty);
                    }

               
                newInvDetails.invUnitAmount = InvoiceDetail.calculateAmount(newInvDetails.invUnitQty, newInvDetails.invUnitPrice);                
                newInv.invTotalAmount += newInvDetails.invUnitAmount;
                
                // luu vao MyInvoice
                MyInvoice.myinvdt.Add(newInvDetails);
                //luu vao file
                string invDetailrecord = $"{newInvDetails.invNo}#{newInvDetails.invUnit}#{newInvDetails.invUnitQty}#{newInvDetails.invUnitPrice}#{newInvDetails.invUnitAmount}";
                all_invDetail_records.Add(invDetailrecord);
                File.WriteAllLines(fPathInvDetail, all_invDetail_records);

                Console.Write("De tiep tuc - Nhap 1 | De hoan tat Hoa Don - Nhap 0 : ");
                selection = int.Parse(Console.ReadLine());
            }
           
            MyInvoice.myinv.Add(newInv);

            //tao ban ghi
            string invrecord = $"{newInv.invNo}#{newInv.invType}#{newInv.invTotalAmount}#{newInv.issueDate}#{newInv.invStatus}#{newInv.Issuer}";
            all_inv_records.Add(invrecord);
            File.WriteAllLines(fPathInv, all_inv_records);

            Console.WriteLine("\nHaon tat them thong tin Hoa Don Ban Sach");
            //in ket qua
            MyInvoice.printInv(newInv.invNo);
        }
        public static void issuePurchaseInvoice()
        {
            string fPathInv = @"../../myInvoice.txt";
            string fPathInvDetail = @"../../myInvoiceDetail.txt";
            //tam luu tat ca du lieu trong txt file vao List<string>
            List<string> all_inv_records = File.ReadAllLines(fPathInv).ToList();
            List<string> all_invDetail_records = File.ReadAllLines(fPathInvDetail).ToList();
            Invoice newInv = new Invoice();
            //InvoiceDetail newInvDetails = new InvoiceDetail();
            string invNo = string.Empty;
            string Issuer = string.Empty;
            Console.Write("Nhap So Hoa Don ");
            invNo = Console.ReadLine();
            while (chkinvNo(invNo) || invNo.Length > 8)
            {
                Console.Write("\n So Hoa Don {0} da ton tai. Vui long nhap So Hoa Don khac: ", invNo);
                invNo = Console.ReadLine();
            }
            newInv.invNo = invNo;
            newInv.invType = 2; // Nhap
            newInv.issueDate = DateTime.Now.ToShortDateString(); // chi don gian la lay thoi gian hien hanh   
            newInv.invTotalAmount = 0;
            newInv.invStatus = 1;
            Console.Write("Nhap Ten Nguoi Lap Hoa Don: ");
            Issuer = Console.ReadLine();
            while (Issuer.Length > 20)
            {
                Console.Write("\nVui long nhap ten duoi 20 ky tu");
                Issuer = Console.ReadLine();
            }
            newInv.Issuer = Issuer;
            int selection = 1;
            string bookID = string.Empty;
           // int bookQty; double bookPriceTag;
            while (selection == 1)
            {
                InvoiceDetail newInvDetails = new InvoiceDetail();
                newInvDetails.invNo = newInv.invNo;
                Console.WriteLine("Nhap thong tin Chi Tiet Hoa Don ");
                MyBookStore.printAllBook();
                
                Console.WriteLine("Nhap Ma Sach can nhap");
               // bookID = Console.ReadLine();
                Book rs = new Book();
                Console.WriteLine("Nhap Ma Sach");
                string input = Console.ReadLine();
                // tien hanh tim ma sach nay
                if (Book.chkbookID(input))
                {
                    rs = Book.inqBookbyID(input);
                    // neu ma sach da ton tai - tien hanh lay thong tin cua sach nay
                    newInvDetails.invUnit = rs.bookID;
                    newInvDetails.invUnitPrice = rs.bookPriceTag;
                }
                else
                {
                    //neu ma sach moi, thuc hien them sach nay vao bookstore luon
                    Book.addBook(input);
                    rs = Book.inqBookbyID(input);
                    newInvDetails.invUnit = rs.bookID;
                    newInvDetails.invUnitPrice = rs.bookPriceTag;
                    //we both know rs.bookQty = 0
                }

                string invUnitQty = string.Empty;
                //Console.WriteLine("\nNhap So Luong sach can nhap");
                invUnitQty = Console.ReadLine();
                while (!InvoiceDetail.chkQty(invUnitQty))
                {
                    invUnitQty = Console.ReadLine();
                }

                //cap nhat so luong mua vao va cap nhat so luong cho thong tin sach lien quan
                // + so luong sach mua vao MyBookstore
                rs.bookQty += int.Parse(invUnitQty);
                Book.updateBookQty(input, rs.bookQty);
                //hoan thanh viec cap nhat so luong mua vao va cap nhat so luong cho thong tin sach lien quan

                newInvDetails.invUnitQty = int.Parse(invUnitQty);
                newInvDetails.invUnitAmount = InvoiceDetail.calculateAmount(newInvDetails.invUnitQty, newInvDetails.invUnitPrice);
                newInv.invTotalAmount += newInvDetails.invUnitAmount;

                // luu vao MyInvoice
                MyInvoice.myinvdt.Add(newInvDetails);
                //luu vao file
                string invDetailrecord = $"{newInvDetails.invNo}#{newInvDetails.invUnit}#{newInvDetails.invUnitQty}#{newInvDetails.invUnitPrice}#{newInvDetails.invUnitAmount}";
                all_invDetail_records.Add(invDetailrecord);
                File.WriteAllLines(@"../../myInvoiceDetail.txt", all_invDetail_records);

                Console.Write("Tiep tuc - Nhap 1 | Hoan tat Hoa Don - Nhap 0 ");
                selection = int.Parse(Console.ReadLine());
            }
            // newInv.invTotalAmount = Invoices.calculateTotalAmount(newInv.invDetail);
            MyInvoice.myinv.Add(newInv);

            //tao ban ghi
            string invrecord = $"{newInv.invNo}#{newInv.invType}#{newInv.invTotalAmount}#{newInv.issueDate}#{newInv.invStatus}#{newInv.Issuer}";
            all_inv_records.Add(invrecord);
            File.WriteAllLines(@"../../myInvoice.txt", all_inv_records);

            Console.WriteLine("\nHoan tat them thong tin Hoa Don Nhap Sach");
            MyInvoice.printInv(newInv.invNo);
        }

        //tim kiem hoa don 
        public static Invoice inqInvoice(string invNo)
        {
            bool isFound = false;
            Invoice result = new Invoice();

            for (int i = 0; i < MyInvoice.myinv.Count; i++)
            {
                if (MyInvoice.myinv[i].invNo == invNo)
                {
                    isFound = true;
                    //lay thong tin
                    result = MyInvoice.myinv[i];
                    //in thong tin hoa don sau khi tim duoc 
                    MyInvoice.printInv(invNo);
                    break;
                }
                else
                {
                    isFound = false;
                }
            }
            if (isFound == false)
            {
                Console.WriteLine("\nKhong co thong tin Hoa Don tren");
            }
            return result;
        }

        public static Invoice inqInvoice(string invNo,int invType)
        {
            bool isFound = false;
            Invoice result = new Invoice();
            //1 - hoa don ban hang / 2 - hoa don mua hang
            if (invType == 1)
            {
                for (int i = 0; i < MyInvoice.myinv.Count; i++)
                {
                    if (MyInvoice.myinv[i].invNo == invNo && MyInvoice.myinv[i].invType == 1)
                    {
                        isFound = true;
                        //lay thong tin
                        result = MyInvoice.myinv[i];
                        //in thong tin hoa don sau khi tim duoc 
                        MyInvoice.printInv(invNo);
                        break;
                    }
                    else
                    {
                        isFound = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < MyInvoice.myinv.Count; i++)
                {
                    if (MyInvoice.myinv[i].invNo == invNo && MyInvoice.myinv[i].invType == 2)
                    {
                        isFound = true;
                        //lay thong tin
                        result = MyInvoice.myinv[i];
                        //in thong tin hoa don sau khi tim duoc 
                        MyInvoice.printInv(invNo);
                        break;
                    }
                    else
                    {
                        isFound = false;
                    }
                }
            }



            if (isFound == false)
            {
                Console.WriteLine("\nKhong co thong tin Hoa Don tren");
            }
            return result;
        }

        // chi cho sua ten nguoi phat hanh hoa don
        public static void updateInvoice(string invNo)
        {
            string filePath = @"../../myInvoice.txt";
            List<string> listInv = new List<string>();
            listInv = File.ReadAllLines(filePath).ToList();
            if (Invoice.chkinvNo(invNo))
            {
                Invoice rs = inqInvoice(invNo);
                //get lib index of the candidate book for modifying
                int inv_index = MyInvoice.find_InvIndex(invNo);
                Console.Write("Nhap Ten Nguoi Lap Hoa Don moi: ");
                string Issuer = Console.ReadLine();
                while (Issuer.Length > 20)
                {
                    Console.Write("\nVui long nhap ten duoi 20 ky tu");
                    Issuer = Console.ReadLine();
                }
                rs.Issuer = Issuer;
                listInv[inv_index] = $"{rs.invNo}#{rs.invType}#{rs.invTotalAmount}#{rs.issueDate}#{rs.invStatus}#{rs.Issuer}";
                File.WriteAllLines(filePath, listInv);

                Console.WriteLine(" Sua thong tin Hoa Don hoan tat");
            }
            else
            {
                Console.WriteLine("Khong co thong tin Hoa Don tuong ung");
            }
        }
        public static void deleteInvoice(string invNo)
        {
            string filePath = @"../../myInvoice.txt";
            List<string> listInv = new List<string>();
            listInv = File.ReadAllLines(filePath).ToList();


            if (Invoice.chkinvNo(invNo))
            {
                Invoice rs = inqInvoice(invNo);
                //get lib index of the candidate book for modifying
                int inv_index = MyInvoice.find_InvIndex(invNo);
                rs.invStatus = 2;
                MyInvoice.myinv[inv_index].invStatus = 2 ;

                listInv[inv_index] = $"{rs.invNo}#{rs.invType}#{rs.invTotalAmount}#{rs.issueDate}#{rs.invStatus}";
                File.WriteAllLines(filePath, listInv);
 
                Console.WriteLine("Da xoa thong tin Hoa Don");
            }
            else
            {
                Console.WriteLine("Khong co thong tin Hoa Don tuong ung");
            }
        }
        
    }
}
