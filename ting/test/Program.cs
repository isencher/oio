using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.lib.net;
using ting.model.net;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            var cnn = withCnn.GetConnectionString("tingSystem");
            while (cnn == null)
            {
                withCnn.cnnSetUI.ShowDialog();
                cnn = withCnn.GetConnectionString("tingSystem");
            }
            var mods = repo.GetAll<sysContext, Module>();
            Console.WriteLine(cnn);
            Console.ReadKey();
        }
    }
}
