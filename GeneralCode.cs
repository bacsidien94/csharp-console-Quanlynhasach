using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_Quanlynhasach
{
    class GeneralCode
    {       
        public static string converPricetag(double input)
        {
            //var pricetag = input.ToString("C0");          
            //var pricetag = input.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));// tien viet ko co decimal
            var pricetag = input.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
            return pricetag;
        }
        public static string convertinvType(int invType)
        {
            string result = String.Empty;
            if (invType == 1)
            {
                result = "Ban Hang";
            }
            else
            {
                result = "Nhap Hang";
            }
            return result;
        }

        public static string convertinvStatus(int invStatus)
        {
            string result = String.Empty;
            if (invStatus == 1)
            {
                result = "Da Phat Hanh";
            }
            else
            {
                result = "Da Xoa";
            }
            return result;
        }
    }
}
