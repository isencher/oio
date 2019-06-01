using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ting.lib.net
{
    public class withIdentity
    {
        /// <summary>
        /// to check id number whether is valid
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns>valid or invalid</returns>
        public static bool ValidateIdNumber(string idNumber)
        {
            bool withregularexpression = CheckIdCardWithRegularExpression(idNumber);
            if (withregularexpression)
            {
                if (idNumber.Length == 15)
                { return true; }
                else
                {
                    if (CheckIDCard18(idNumber))
                    {
                        return true;
                    }
                    else
                    { return false; }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// to get gender from id number
        /// </summary>
        /// <param name="idNumber">id number</param>
        /// <returns>gender</returns>
        public static bool GetGenderFromIdNumber(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                if (int.Parse(idNumber.Substring(14, 3)) % 2 == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (int.Parse(idNumber.Substring(12, 3)) % 2 == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        /// <summary>
        /// to get birthday from id number
        /// </summary>
        /// <param name="IdNumber">id number</param>
        /// <returns>birthday</returns>
        public static DateTime GetBirthdayFromIdNumber(string IdNumber)
        {
            string datenumber;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string format;

            if (ValidateIdNumber(IdNumber))
            {
                if (IdNumber.Length == 18)
                {
                    datenumber = IdNumber.Substring(6, 8);
                    format = "yyyyMMdd";
                    return DateTime.ParseExact(datenumber, format, provider);
                }
                else
                {
                    datenumber = IdNumber.Substring(6, 6);
                    format = "yyMMdd";
                    return DateTime.ParseExact(datenumber, format, provider);
                }
            }
            else
            {
                return DateTime.Today;
            }
        }
        /// <summary>
        /// to convert id number from 15 digit to 18 digit
        /// </summary>
        /// <param name="id">15 digit id number</param>
        /// <returns>18 digit id number</returns>
        public static string Id15To18(string id)
        {
            IDCard card = new IDCard();
            return card.Convert15To18(id);
        }

        private static bool CheckIdCardWithRegularExpression(string IdNumber)
        {

            if ((!Regex.IsMatch(IdNumber, @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
            {
                return false;
            }
            else
            { return true; }
        }
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }
    }

    //============比较完整的转换类（C#版）=============
    internal class IDCard
    {
        // wi =2(n-1)(mod 11) 
        int[] wi = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
        // verify digit 
        int[] vi = new int[] { 1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2 };
        private int[] ai = new int[18];

        public IDCard() { }

        public string Convert15To18(string idNumber)
        {
            if (!IsIDcard(idNumber) || (idNumber.Length != 15)) //正则表达式验证不是身份证
            {
                return idNumber;
            }
            else
            {
                return uptoeighteen(idNumber);
            }
        }

        //verify 
        private string Verify(string idcard)
        {
            string strCard = "";
            if (idcard.Length == 15)
            {
                strCard = uptoeighteen(idcard);
            }
            if (idcard.Length == 18)
            {
                strCard = idcard;
            }
            //string verify = idcard.Substring(17, 18); 
            //if (verify.Equals(getVerify(idcard))) { 
            //return true; 
            //} 
            return strCard;
        }
        //get verify 
        private string getVerify(string eightcardid)
        {
            int remaining = 0;
            if (eightcardid.Length == 18)
            {
                eightcardid = eightcardid.Substring(0, 17);
            }
            if (eightcardid.Length == 17)
            {
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    string k = eightcardid.Substring(i, 1);
                    ai[i] = int.Parse(k);
                }
                for (int i = 0; i < 17; i++)
                {
                    sum = sum + wi[i] * ai[i];
                }
                remaining = sum % 11;
            }
            return remaining == 2 ? "X" : (vi[remaining]).ToString();
        }
        //15 update to 18 
        private string uptoeighteen(string fifteencardid)
        {
            string eightcardid = fifteencardid.Substring(0, 6);
            eightcardid = eightcardid + "19";
            eightcardid = eightcardid + fifteencardid.Substring(6, 9);
            eightcardid = eightcardid + getVerify(eightcardid);
            return eightcardid;
        }

        // 正则表达式验证str_idcard是否符合身份证要求
        private bool IsIDcard(string str_idcard)
        {
            if (Regex.IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)")
                &&
               Regex.IsMatch(str_idcard, @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
