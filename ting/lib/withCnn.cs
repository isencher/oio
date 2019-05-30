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

        /// <summary>
        /// to update a cnnstring string of cnn name into set.config file
        /// </summary>
        /// <param name="name">cnn name</param>
        /// <param name="strcnn">cnnstring string</param>
        /// <returns>success or fail</returns>
        public static bool UpdateConnectionString(string name, string strcnn)
        {
            // encrypt cnnstring string
            strcnn = sec.Encrypt(strcnn, password);
            return rw.UpdateConnectionString(name, strcnn, true, file);
        }

        /// <summary>
        /// to get a cnnstring string by cnn name from set.config file
        /// </summary>
        /// <param name="name">cnn name</param>
        /// <returns>return a cnnstring string, return null when set.config is not exist or cnnstring string from file is invalid.</returns>
        public static string GetConnectionString(string name)
        {

            var result = rw.GetConnectionString(name, file);
            if (result != null)
            {
                // descrypt cnnstring string
                result = sec.Decrypt(result, password);
                if (rw.ValidateConnectionString(result))
                {
                    return result;
                }
                else { return null; }
            }
            else { return null; }
        }

        //public static Form SetCnnUI()
        //{
        //    return new setcnn();
        //}
    }
}
