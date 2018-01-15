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
    public partial class AddDataDicDlg : DevComponents.DotNetBar.Office2007Form
    {
        List<string> annoNotInDic = null;
        string Field = "";
        public AddDataDicDlg(List<string> annoNotInDic,string Field)
        {
            InitializeComponent();
            this.annoNotInDic = annoNotInDic;
            this.Field = Field;
            
        }

        private void AddDataDicDlg_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = getAnnoDataTable( Field);
            
            //分别设置不需要更新的列为只读状态
            try
            {
                if ("sourceAnno".Equals(Field))
                {
                    this.dataGridView1.Columns[2].ReadOnly = true;
                    this.dataGridView1.Columns[0].ReadOnly = true;
                    this.groupBox2.Text = "原始数据待添加标注";
                }
                if ("newAnno".Equals(Field))
                {
                    this.dataGridView1.Columns[1].ReadOnly = true;
                    this.dataGridView1.Columns[0].ReadOnly = true;
                    this.groupBox2.Text = "新数据待添加标注";
                }

                //添加需要添加到数据字典中的标注数据
                for (int i = 0; i < annoNotInDic.Count; i++)
                {
                    this.txtAddAnno.AppendText(annoNotInDic[i] + "\r\n");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private DataTable getAnnoDataTable(string Field)
        {
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
            string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
            OleDbConnection MapConn = AccessUtils.GetConn(MapDBPath);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select ID, sourceAnno,newAnno from 标注映射表";
                OleDbCommand cmd = new OleDbCommand(sql, MapConn);
                OleDbDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MapConn.Close();
            }
            return dt;
           
        }

        private bool UpdateAccess(DataTable dt, string Field)
        {
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
            string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
            OleDbConnection MapConn = AccessUtils.GetConn(MapDBPath);

            try
            {
                OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ID, sourceAnno,newAnno  FROM 标注映射表", MapConn);//建立一个DataAdapter对象
                OleDbCommandBuilder cb = new OleDbCommandBuilder(oda);//这里的CommandBuilder对象一定不要忘了,一般就是写在DataAdapter定义的后面
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                DataTable changesDT = dt.GetChanges();
                oda.Update(changesDT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MapConn.Close();
            }

            return true;
        }

        private void updateToAccess_Click(object sender, EventArgs e)
        {
            bool flag = UpdateAccess((DataTable)this.dataGridView1.DataSource,Field);
            if(flag)
            {
                MessageBox.Show("修改成功！");
            }
        }

        //添加数据时，自动添加ID编号
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }





    }
}