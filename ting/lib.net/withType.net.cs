using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ting.lib.net
{
    public partial class withType
    {
       
        /// <summary>
        /// to check a containercontrol class whether exist
        /// </summary>
        /// <typeparam name="T">containercontrol class</typeparam>
        /// <param name="instance">containercontrol instance</param>
        /// <param name="tag">identity tag</param>
        /// <returns>containercontrol instance</returns>
        public static T IsExist<T>(T instance, int tag)
            where T : ContainerControl, new()
        {
            // 1. instance == null   --> new a new instance
            // 2. instance != null, but isdisposed == true    --> new a new instance
            // 3. instance != null, and isdisposed == false, but tag is not same -->disposed,and new a new instance
            // 4. instance != null, and isdisposed == false, and tag is same  --> no way
            if (instance == null) { instance = new T(); instance.Tag = tag; }
            else if (instance != null && instance.IsDisposed == true) { instance = new T(); instance.Tag = tag; }
            else if (instance != null && instance.IsDisposed == false && Convert.ToInt32(instance.Tag) != tag)
            { instance.Dispose(); instance = new T(); instance.Tag = tag; }
            return instance;
        }

    }
}
