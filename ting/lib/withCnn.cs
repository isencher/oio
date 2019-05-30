using System;
using System.Collections.Generic;
using System.Text;

namespace ting.lib
{
    public class withCnn
    {
        private static rwcnnTofile rw = new rwcnnTofile();
        //private static sec se = new sec();
        private static string password = "@*:'~`t/|]t[[2+-";
        private static string file = "set.config";
        private static cnnString cs = new cnnString();

        public static bool UpdateConnectionString(string name, string strcnn)
        {
            // encrypt cnnstring string
            strcnn = sec.Encrypt(strcnn, password);
            return rw.UpdateConnectionString(name, strcnn, true, file);
        }
        public static string GetConnectionString(string name)
        {

            var result = rw.GetConnectionString(name, file);
            if (result != null)
            {
                // descrypt cnnstring string
                result = sec.Decrypt(result, password);
                if (ValidateConnectionString(result))
                {
                    return result;
                }
                else { return null; }
            }
            else { return null; }
        }
        public static bool ValidateConnectionString(string strcnn)
        {
            var c = cs.ConvertcnnString(strcnn);

            if (c.DataSource == null && c.InitialCatalog == null && c.IntegratedSecurity == null
                && c.UserId == null && c.Password == null) { return false; }
            else { return true; }
        }
        //public static Form SetCnnUI()
        //{
        //    return new setcnn();
        //}
    }
}
