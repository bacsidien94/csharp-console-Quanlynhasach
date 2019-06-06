using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_Quanlynhasach
{
    class MyInvoice
    {
        public static List<Invoice> myinv { get; set; }
        public static List<InvoiceDetail> myinvdt { get; set; }
        //init both invoice & invoice detail
        public static void init_invoice()
        {
            //init
            MyInvoice.myinv = new List<Invoice>();         

            List<Invoice> init = new List<Invoice>();
            string filePath = @"../../myInvoice.txt";
            List<string> listInvoice = new List<string>();
            listInvoice = File.ReadAllLines(filePath).ToList();
            foreach (string record in listInvoice)
            {
                string[] inv_att = record.Split('#');
                Invoice inv = new Invoice() { };
                inv.invNo = inv_att[0];
                inv.invType = int.Parse(inv_att[1]);
                inv.invTotalAmount = double.Parse(inv_att[2]);
                inv.issueDate = DateTime.Parse(inv_att[3]).ToShortDateString();
                inv.invStatus = int.Parse(inv_att[4]);
                inv.Issuer = inv_att[5];
                init.Add(inv);
            }
            MyInvoice.myinv = init;

        }
        public static void init_invDetail()
        {
            MyInvoice.myinvdt = new List<InvoiceDetail>();

            List<InvoiceDetail> init = new List<InvoiceDetail>();
            string filePath = @"../../myInvoiceDetail.txt";
            List<string> listInvDetail = new List<string>();
            listInvDetail = File.ReadAllLines(filePath).ToList();
            foreach (string record in listInvDetail)
            {
                string[] invdt_att = record.Split('#');
                InvoiceDetail invdt = new InvoiceDetail() { };
                invdt.invNo = invdt_att[0];
                invdt.invUnit = invdt_att[1];
                invdt.invUnitQty = int.Parse(invdt_att[2]);
                invdt.invUnitPrice = double.Parse(invdt_att[3]);
                invdt.invUnitAmount = double.Parse(invdt_att[4]);
                init.Add(invdt);
            }
            MyInvoice.myinvdt = init;
        }
        public static int find_InvIndex(string invNo)
        {
            int index = 0;//init
            for (int i = 0; i < MyInvoice.myinv.Count; i++)
            {
                if (MyInvoice.myinv[i].invNo == invNo)
                {
                    index = i;
                    //Console.WriteLine(i); // test
                    break;
                }
            }
            return index;
        }
        public static void printInv(string invNo)
        {
            int index = 0;
            index = find_InvIndex(invNo);
            // MyInvoice.myinvdt[indexmatched[x]].invUnitQty
            Console.WriteLine("\nHoa Don {0} \n", GeneralCode.convertinvType(MyInvoice.myinv[index].invType));          
            Console.WriteLine("So Hoa Don : {0,-10} ", MyInvoice.myinv[index].invNo);
            Console.WriteLine("Trang Thai : {0,-15}", GeneralCode.convertinvStatus(MyInvoice.myinv[index].invStatus));
            Console.WriteLine("Ngay Phat Hanh : {0,-20} ", MyInvoice.myinv[index].issueDate);
            Console.WriteLine("Nguoi lap Hoa Don : {0,-20}", MyInvoice.myinv[index].Issuer);
            printInvDetail(invNo);
            Console.WriteLine("Tong Cong : {0,-15}", GeneralCode.converPricetag(MyInvoice.myinv[index].invTotalAmount));
            
        }
        public static void printInvDetail(string invNo)
        {
            //tim kiem
            List<int> indexmatched = new List<int>();

            for (int i = 0; i < MyInvoice.myinvdt.Count; i++)
            {
                if (MyInvoice.myinvdt[i].invNo.ToUpper() == invNo.ToUpper())
                {
                    indexmatched.Add(i);
                }
                else { }
            }
            //in ket qua
            Console.WriteLine("\nChi Tiet Hoa Don");
            //in thong tin chi tiet hoa don
            // phai xu ly cach tim index phu hop voi thong tin invoice no
            InvoiceDetail.print_header();
            //foreach (InvoiceDetail invdt in MyInvoice.myinvdt)
            for (int x = 0; x < indexmatched.Count; x++)
            {
                Console.WriteLine("{0,-5}{1,-35}{2,-15}{3,-15}{4,-15}",
                    x+1,
                    Book.inqBookbyID(MyInvoice.myinvdt[indexmatched[x]].invUnit).bookName,
                    MyInvoice.myinvdt[indexmatched[x]].invUnitQty,
                    GeneralCode.converPricetag(MyInvoice.myinvdt[indexmatched[x]].invUnitPrice),
                    GeneralCode.converPricetag(MyInvoice.myinvdt[indexmatched[x]].invUnitAmount));
            }
        }
    }
}
