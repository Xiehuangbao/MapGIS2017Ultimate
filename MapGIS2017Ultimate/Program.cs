using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MapGIS2017Ultimate
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
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                ///MessageBox.Show(e.Message);
                MessageBox.Show("MapGIS内部组件错误，工程所做修改已保存！");
            }
            
        }
    }
}