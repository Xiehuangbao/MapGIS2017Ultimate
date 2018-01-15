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
            
            //�ֱ����ò���Ҫ���µ���Ϊֻ��״̬
            try
            {
                if ("sourceAnno".Equals(Field))
                {
                    this.dataGridView1.Columns[2].ReadOnly = true;
                    this.dataGridView1.Columns[0].ReadOnly = true;
                    this.groupBox2.Text = "ԭʼ���ݴ���ӱ�ע";
                }
                if ("newAnno".Equals(Field))
                {
                    this.dataGridView1.Columns[1].ReadOnly = true;
                    this.dataGridView1.Columns[0].ReadOnly = true;
                    this.groupBox2.Text = "�����ݴ���ӱ�ע";
                }

                //�����Ҫ��ӵ������ֵ��еı�ע����
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
                string sql = "select ID, sourceAnno,newAnno from ��עӳ���";
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
                OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ID, sourceAnno,newAnno  FROM ��עӳ���", MapConn);//����һ��DataAdapter����
                OleDbCommandBuilder cb = new OleDbCommandBuilder(oda);//�����CommandBuilder����һ����Ҫ����,һ�����д��DataAdapter����ĺ���
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
                MessageBox.Show("�޸ĳɹ���");
            }
        }

        //�������ʱ���Զ����ID���
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }





    }
}