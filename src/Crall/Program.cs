using System;
using System.Windows.Forms;

namespace 杜德平的数据库编程
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Form1.createForm1());
        }
    }
}
