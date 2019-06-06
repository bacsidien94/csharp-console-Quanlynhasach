using System;
using System.IO;

namespace csharp_Quanlynhasach
{
    class InvoiceDetail
    {
        public string invNo { get; set; }              //1. Số Hoá Đơn from invNo
        public string invUnit { get; set; }         //2. Mã Sách - from BookID 
        public int invUnitQty { get; set; }             //3. Số Lượng
        public double invUnitPrice { get; set; }        //4. Đơn Giá - from Book att
        public double invUnitAmount { get; set; }       //5. Thành Tiền - auto

        public static double calculateAmount(int qty, double pricetag)
        {
            double result;
            result = qty * pricetag;
            Console.WriteLine("\ntong cong: " + GeneralCode.converPricetag(result));//
            return result;
        }
        public static bool chkQty(string input)
        {
            int temp;
            // bool result = false;
            if (!int.TryParse(input, out temp) && input != "0")
            {
                Console.WriteLine("Vui long nhap So Luong");
                return false;
            }
            //2019-05-22 added
            else if (input == "0")
            {
                Console.WriteLine("Vui long nhap So Luong > 0 ");
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void print_header()
        {
            Console.WriteLine("\n{0,-5}{1,-35}{2,-15}{3,-15}{4,-15}\n",
                "STT",
                "Ten Sach",
               "So Luong",
               "Don Gia",
               "Thanh Tien");
        }
    }
}
