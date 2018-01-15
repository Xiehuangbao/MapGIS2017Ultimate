using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MapGIS2017Ultimate
{
    public partial class UpdateLogDlg : DevComponents.DotNetBar.Office2007Form
    {
        public UpdateLogDlg()
        {
            InitializeComponent();
        }

        private void UpdateLogDlg_Load(object sender, EventArgs e)
        {
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
            string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
            OleDbConnection logConn = AccessUtils.GetConn(MapDBPath);
            string MapSQL = "select * from  更新工程记录";
            OleDbDataAdapter da = new OleDbDataAdapter(MapSQL, logConn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    dgvLog.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                logConn.Close();
                logConn.Dispose();
                da.Dispose();
            }
        }
    }
}