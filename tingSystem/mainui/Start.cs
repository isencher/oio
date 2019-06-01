using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.lib;
using ting.log;
using ting.model;

namespace ting.pal
{
    public class Start
    {
        public static User User { get; set; }
        public static SetofBook Set { get; set; }

        public static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 确保从配置文件中能读取到ConnectionString
            string cnn = withCnn.GetConnectionString("tingSystem");
            if (cnn == null)
            {
                var dia = withCnn.SetCnnUI();
                var r = dia.ShowDialog();
            }
            if (withCnn.ValidateConnectionString(cnn) == false)
            {
                var dia = withCnn.SetCnnUI();
                var r = dia.ShowDialog();
            }

            FrmLogin login = null;
            DialogResult result;
            User user = null;
            SetofBook set = null;
            mainui main = null;
            do
            {
                login = new FrmLogin();
                result = login.ShowDialog();
                if (result == DialogResult.No) { return; }
                user = login.CurrentUser; User = user;
                set = login.CurrentBook; Set = set; login.Dispose();
                main = new mainui(user, set);
                Application.Run(main);

            } while (main.IsCancel == true);
            #region write log
            var log = new Log()
            {
                UserAcount = user.Account,
                Date = DateTime.Now,
                Action = "Exit",
                Entity = null,
                Content = null,
                Original = null
            };
            new sqlLogger().Write(log);
            #endregion
        }
    }
}
