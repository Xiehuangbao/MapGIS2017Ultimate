using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop;
using System.Threading;

namespace MapGIS2005
{
    public partial class UpdateTableForm : DevComponents.DotNetBar.Office2007Form
    {
        string access_Path = "";
        string access_Path_New = "";
        string historyPath = "";
        DataTable source;//���Ƚϵ�ԭ����
        DataTable newData;//�Ƚϵ�������
        string strDBCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";

        //  //���½������б�
        //private delegate void SetPos(int ipos);

        ////���½�����������ʾ
        //private void SetTextMessage(int ipos)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        SetPos setpos = new SetPos(SetTextMessage);
        //        this.Invoke(setpos, new object[] { ipos });
        //    }
        //    else
        //    {
        //        this.toolStripStatusLabel1.Text = ipos.ToString() + "/100";
        //        this.toolStripProgressBar1.Value = Convert.ToInt32(ipos);
        //    }
        //    if (ipos == 100)
        //    {
        //        this.toolStripStatusLabel1.Text = "�������";
        //    }
        //}


        public UpdateTableForm(string access_Path,string access_Path_New,string historyPath)
        {
            InitializeComponent();
            this.access_Path = access_Path;
            this.access_Path_New = access_Path_New;
            this.historyPath = historyPath;
            string[] TableName = { "JGAB301_�˲����", "JGAB302_ԭ�ϱ����", "JGAB303_���鹤����", "JGAB304_�ɿ�Ȩ", "JGAB305_̽��Ȩ",
                "JGAB306_����", "JGAB307_�ɿ���", "JGAB308_�˲���", "JGAB309_�˲��δ���", "JGAB310_ԭ���", "JGAB311_ԭ��δ���",
                "JGAB312_��ζ��ձ�", "JGAB313_����Ŀ¼", "JGAB314_����Ŀ¼", "JGAB315_ר��ͼ��", "JGAB316_ר��ͼ��ͼ��",
                "JGAB317_ú������", "JGAB318_��������", "JGAB319_���ζ��ձ�", "JGAB320_�ϲ�ԭ���", "JGAB321_�ɿ�Ȩ����" };
            for (int i = 0; i < TableName.Length; i++)
            {
                cbxTableList.Items.Add(TableName[i]);
            }

            OleDbConnection conn = new OleDbConnection(strDBCon + access_Path);
            conn.Open();
            if (conn.State != ConnectionState.Open) return;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select CKZBH from  JGAB304_�ɿ�Ȩ";   //������ȡ C#����Access֮���ж�ȡmdb  
            OleDbDataReader odrReader = cmd.ExecuteReader();
            while (odrReader.Read())
            {
                this.cbxSelectKQuan.Items.Add(odrReader["CKZBH"].ToString());
                this.cbxUpdateKQuan.Items.Add(odrReader["CKZBH"].ToString());
            }
            odrReader.Close();
            cmd.CommandText = "select HCKQBH from  JGAB304_�ɿ�Ȩ";   //������ȡ C#����Access֮���ж�ȡmdb  
            odrReader = cmd.ExecuteReader();
            while (odrReader.Read())
            {
                this.cbxKQu.Items.Add(odrReader["HCKQBH"].ToString());
            }

            odrReader.Close();
            conn.Close();
        }

        private void cbxSelectKQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strDBCon + access_Path);
            conn.Open();
            if (conn.State != ConnectionState.Open) return;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select YKDBH ,KDBH from  JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";   //������ȡ C#����Access֮���ж�ȡmdb  
            OleDbDataReader odrReader = cmd.ExecuteReader();
            this.lstvKD.Clear();
            this.lstvKD.View = View.Details;
            this.lstvKD.GridLines = true;
            this.lstvKD.Columns.Add("ԭ��α��", 100, HorizontalAlignment.Left); //����ֶ�
            this.lstvKD.Columns.Add("�˲��α��", 100, HorizontalAlignment.Left); //����ֶ�
            this.lstvKD.BeginUpdate();
            while (odrReader.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = odrReader["YKDBH"].ToString();
                lvi.SubItems.Add(odrReader["KDBH"].ToString());
                this.lstvKD.Items.Add(lvi);

            }
            this.lstvKD.EndUpdate();


            odrReader.Close();
            conn.Close();
        }

        private void cbxTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxSelectKQuan.SelectedItem.ToString().Equals(""))
            {
                MessageBox.Show("��ѡ�������");
                return;
            }
            string P_str_Sql_04 = "select *  from JGAB304_�ɿ�Ȩ where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����  
            string P_str_Sql_21 = "select * from JGAB321_�ɿ�Ȩ���� where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����    
            string P_str_Sql_08 = "select * from JGAB308_�˲��� where CKZBH = '"
             + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����  
            string P_str_Sql_06 = "select * from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_09 = "select * from JGAB309_�˲��δ��� where (TYBH IN (select DISTINCT TYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_12 = "select * from JGAB312_��ζ��ձ� where (HCTYBH IN (select DISTINCT TYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_10 = "select * from JGAB310_ԭ��� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_11 = "select * from JGAB311_ԭ��δ��� where (KDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_18 = "select * from JGAB318_�������� where (TYBH IN (select DISTINCT CLLYTYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_17 = "select * from JGAB317_ú������ where (TYBH IN (select DISTINCT MCBH from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_19 = "select * from JGAB319_���ζ��ձ� where (YKDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_20 = "select * from JGAB320_�ϲ�ԭ��� where (HBTYBH IN (select DISTINCT YTYBH from JGAB319_���ζ��ձ� where (YKDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_01 = "select * from JGAB301_�˲���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_02 = "select * from JGAB302_ԭ�ϱ���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_03 = "select * from JGAB303_���鹤���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "select * from JGAB305_̽��Ȩ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_07 = "select * from JGAB307_�ɿ��� where  CKQBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_13 = "select * from JGAB313_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "select * from JGAB314_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "select * from JGAB315_ר��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "select * from JGAB316_ר��ͼ��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            try
            {
                OleDbConnection conn = new OleDbConnection(strDBCon + access_Path);
                conn.Open();
                if (conn.State != ConnectionState.Open) return;
                string SQL = "";
                switch (cbxTableList.SelectedItem.ToString())
                {
                    case "JGAB301_�˲����":
                        SQL = P_str_Sql_01;
                        break;
                    case "JGAB302_ԭ�ϱ����":
                        SQL = P_str_Sql_02;
                        break;
                    case "JGAB303_���鹤����":
                        SQL = P_str_Sql_03;
                        break;
                    case "JGAB304_�ɿ�Ȩ":
                        SQL = P_str_Sql_04;
                        break;
                    case "JGAB305_̽��Ȩ":
                        SQL = P_str_Sql_05;
                        break;
                    case "JGAB306_����":
                        SQL = P_str_Sql_06;
                        break;
                    case "JGAB307_�ɿ���":
                        SQL = P_str_Sql_07;
                        break;
                    case "JGAB308_�˲���":
                        SQL = P_str_Sql_08;
                        break;
                    case "JGAB309_�˲��δ���":
                        SQL = P_str_Sql_09;
                        break;
                    case "JGAB310_ԭ���":
                        SQL = P_str_Sql_10;
                        break;
                    case "JGAB311_ԭ��δ���":
                        SQL = P_str_Sql_11;
                        break;
                    case "JGAB312_��ζ��ձ�":
                        SQL = P_str_Sql_12;
                        break;
                    case "JGAB313_����Ŀ¼":
                        SQL = P_str_Sql_13;
                        break;
                    case "JGAB314_����Ŀ¼":
                        SQL = P_str_Sql_14;
                        break;
                    case "JGAB315_ר��ͼ��":
                        SQL = P_str_Sql_15;
                        break;
                    case "JGAB316_ר��ͼ��ͼ��":
                        SQL = P_str_Sql_16;
                        break;
                    case "JGAB317_ú������":
                        SQL = P_str_Sql_17;
                        break;
                    case "JGAB318_��������":
                        SQL = P_str_Sql_18;
                        break;
                    case "JGAB319_���ζ��ձ�":
                        SQL = P_str_Sql_19;
                        break;
                    case "JGAB320_�ϲ�ԭ���":
                        SQL = P_str_Sql_20;
                        break;
                    case "JGAB321_�ɿ�Ȩ����":
                        SQL = P_str_Sql_21;
                        break;
                }
                DataTable dt = GetDataSet(conn, SQL);
                dataGridViewSource.DataSource = dt;
                source = dt;

                conn.Close();
                OleDbConnection connNew = new OleDbConnection(strDBCon + access_Path_New);
                connNew.Open();
                DataTable dtNew = GetDataSet(connNew, SQL);
                dataGridViewNew.DataSource = dtNew;
                newData = dtNew;
                connNew.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string isNull(string str)
        {
            if (str == "") return "null";
            else return str;
        }


        public OleDbConnection GetConn(string connStr)
        {
            OleDbConnection conn = new OleDbConnection(connStr);
            return conn;
        }

        //��ȡdataset
        public DataTable GetDataSet(OleDbConnection conn, string SQL)
        {
            OleDbDataAdapter odda = new OleDbDataAdapter();
            OleDbCommand odc = new OleDbCommand(SQL, conn);
            odc.CommandType = CommandType.Text;
            odda.SelectCommand = odc;
            DataTable dt = new DataTable();
            odda.Fill(dt);
            return dt;
        }

        public List<string> CompareONField(DataSet oldDs, DataSet newDs)
        {
            List<string> lstValue = new List<string>();
            foreach (DataRow dr in newDs.Tables[0].Rows)
            {
                for (int i = 0; i < oldDs.Tables[0].Rows.Count; i++)
                {
                    if (oldDs.Tables[0].Rows[i][0] == dr[0]) break;
                    if (i == oldDs.Tables[0].Rows.Count - 1) lstValue.Add(dr[0].ToString());
                }
            }
            return lstValue;
        }

        public void ExecuteNonQuery(OleDbConnection conn, string SQL)
        {
            conn.Open();
            OleDbCommand odc = new OleDbCommand(SQL, conn);
            odc.CommandType = CommandType.Text;
            odc.ExecuteNonQuery();
            conn.Close();
        }

        DataTable dtRetAdd;
        DataTable dtRetDif1;
        DataTable dtRetDif2;
        DataTable dtRetDel;

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CompareDt(source, newData, "TYBH", out dtRetAdd, out dtRetDif1, out dtRetDif2, out dtRetDel);
            CheckResult cr = new CheckResult(dtRetAdd, dtRetDel, dtRetDif1, dtRetDif2);
            cr.ShowDialog();
        }

        /// <summary>
        /// �Ƚ�����DataTable���ݣ��ṹ��ͬ��
        /// </summary>
        /// <param name="dt1">�������ݿ��DataTable</param>
        /// <param name="dt2">�����ļ���DataTable</param>
        /// <param name="keyField">�ؼ��ֶ���</param>
        /// <param name="dtRetAdd">�������ݣ�dt2�е����ݣ�</param>
        /// <param name="dtRetDif1">��ͬ�����ݣ����ݿ��е����ݣ�</param>
        /// <param name="dtRetDif2">��ͬ�����ݣ�ͼ2�е����ݣ�</param>
        /// <param name="dtRetDel">ɾ�������ݣ�dt2�е����ݣ�</param>
        public static void CompareDt(DataTable dt1, DataTable dt2, string keyField,
            out DataTable dtRetAdd, out DataTable dtRetDif1, out DataTable dtRetDif2,
            out DataTable dtRetDel)
        {
            //Ϊ����������ṹ
            dtRetDel = dt1.Clone();
            dtRetAdd = dtRetDel.Clone();
            dtRetDif1 = dtRetDel.Clone();
            dtRetDif2 = dtRetDel.Clone();

            int colCount = dt1.Columns.Count;

            DataView dv1 = dt1.DefaultView;
            DataView dv2 = dt2.DefaultView;

            //���Ե�һ����Ϊ���գ����ڶ��������޸��˻���ɾ����
            foreach (DataRowView dr1 in dv1)
            {
                dv2.RowFilter = keyField + " = '" + dr1[keyField].ToString() + "'";
                if (dv2.Count > 0)
                {
                    if (!CompareUpdate(dr1, dv2[0]))//�Ƚ��Ƿ��в�ͬ��
                    {
                        dtRetDif1.Rows.Add(dr1.Row.ItemArray);//�޸�ǰ
                        dtRetDif2.Rows.Add(dv2[0].Row.ItemArray);//�޸ĺ�
                        dtRetDif2.Rows[dtRetDif2.Rows.Count - 1]["TYBH"] = dr1.Row["TYBH"];//��ID���������ļ��ı���Ϊ����IDȫ��==0
                        continue;
                    }
                }
                else
                {
                    //�Ѿ���ɾ����
                    dtRetDel.Rows.Add(dr1.Row.ItemArray);
                }
            }

            //�Ե�һ����Ϊ���գ�����¼�Ƿ���������
            dv2.RowFilter = "";//�������
            foreach (DataRowView dr2 in dv2)
            {
                dv1.RowFilter = keyField + " = '" + dr2[keyField].ToString() + "'";
                if (dv1.Count == 0)
                {
                    //������
                    dtRetAdd.Rows.Add(dr2.Row.ItemArray);
                }
            }
        }

        //�Ƚ��Ƿ��в�ͬ��
        private static bool CompareUpdate(DataRowView dr1, DataRowView dr2)
        {
            //����ֻҪ��һ�һ���������оͲ�һ��,����Ƚ�����
            object val1;
            object val2;
            for (int i = 1; i < dr1.Row.ItemArray.Length; i++)
            {
                val1 = dr1[i];
                val2 = dr2[i];
                if (!val1.Equals(val2))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnBackUP_Click(object sender, EventArgs e)
        {
            if (this.cbxSelectKQuan.SelectedItem == null)
            {
                MessageBox.Show("��ѡ���Ȩ��");
                return;
            }
            if (this.cbxUpdateKQuan.SelectedItem == null || this.cbxKQu.SelectedItem == null || this.dateTimeInput1 == null
                || this.txtUpdateReason == null || this.txtOperator == null || this.txtManager == null || this.txtComment == null)
            {
                MessageBox.Show("�����Ƽ�¼��Ϣ��");
                return;
            }
            string[] TableName = { "JGAB301_�˲����", "JGAB302_ԭ�ϱ����", "JGAB303_���鹤����", "JGAB304_�ɿ�Ȩ", "JGAB305_̽��Ȩ",
                "JGAB306_����", "JGAB307_�ɿ���", "JGAB308_�˲���", "JGAB309_�˲��δ���", "JGAB310_ԭ���", "JGAB311_ԭ��δ���",
                "JGAB312_��ζ��ձ�", "JGAB313_����Ŀ¼", "JGAB314_����Ŀ¼", "JGAB315_ר��ͼ��", "JGAB316_ר��ͼ��ͼ��",
                "JGAB317_ú������", "JGAB318_��������", "JGAB319_���ζ��ձ�", "JGAB320_�ϲ�ԭ���", "JGAB321_�ɿ�Ȩ����" };
            string[] tableField = { "TZYSBH", "TYBH", "HCKQBH", "CKZBH", "CKQR", "CKQFW", "DZ", "KSBH", "KSMC", "FZJG", "YXQQ", "YXQZ", "XKCSS", "XKCSX", "KCZKZ", "ZKZMC", "ZYJSL", "ZYKSL", "BYJSL", "BYKSL", "DKSNL", "DJSNL", "SKSNL", "SJSNL", "NDKSL", "NDJSL", "KCFSM", "KCFS", "XKFSM", "XKFS", "RXKSL", "KQBH", "JJLXM", "JJLX", "CYRY", "NCZ", "JSSCCB", "KCPLX", "CXSBSL" };
            // �ļ�����·��������


            // ����Excel�ĵ�
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;
            string sheetName = "";
            //ɾ���Լ����ɵ�����sheet
            for (int i = 2; i < 4; i++)
            {
                sheetName = "Sheet" + i;
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelBook.Sheets.get_Item(sheetName);
                sheet.Delete();
            }
            for (int i = 0; i < 1; i++)
            {
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Name = "������־��";

                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 1] = "���¿�Ȩ";//Ҳ����������ֵ
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 2] = "��������";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 3] = "����ʱ��";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 4] = "����ԭ��";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 5] = "������";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 6] = "����Ա";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 7] = "��ע";

                //�ϲ� ��Ԫ�� ���ñ�ͷ
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G2").MergeCells);
                //�õ�  Range ��Χ   �����
                Microsoft.Office.Interop.Excel.Range range = ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G69");
                //���� ��range�ڵ�  ��ʽ   ��ɫ  �߿� 

                ////����Excel����  �п�
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C69").ColumnWidth = 30;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F69").ColumnWidth = 30;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G69").ColumnWidth = 30;
                //����  �� Range  ����ɫ   �� A1��W1
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G1").Interior.ColorIndex = 15;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A2", "G2").Interior.ColorIndex = 15;

                //����ĳ����range��ѡ��  
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A3", "G3").Select();

                //����   ���� ѡ�����ڵ�  Excel��Ԫ���C ��W  �ǻ��     ǰ���A B  Ϊ�̶���
                //���� ���� ���� ��ͷ�������ͷ�ϲ����� ��û�й̶�  ѡC3 ��W3����ʾ��C�ĵ����п�ʼ Ϊ � ��   ��������Ϊ�̶��ģ�
                ExcelApp.ActiveWindow.FreezePanes = true;

                //���� ĳ����range�� ��Ԫ�����������ɫ
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G2").Font.Color = -16744448;//������Excel��ɫ���ձ�
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A3", "G24").Font.Color = -16776961;
                //���� ����   
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.Font.Size = 10;
                range.Borders.LineStyle = 1;
                //���ñ߿�
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                range.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;
                range.WrapText = true;
                //��ֵ    ��
                for (int j = 0; j < 1; j++)
                {
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 1] = this.cbxSelectKQuan.SelectedItem.ToString();
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 2] = this.cbxKQu.SelectedItem.ToString();
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 3] = this.dateTimeInput1.Text.ToString();
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 4] = this.txtUpdateReason.Text;
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 5] = this.txtManager.Text;
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 6] = this.txtOperator.Text;
                    ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[j + 3, 7] = this.txtComment.Text;
                }
            }
            ExcelApp.DisplayAlerts = true;
            object missing = System.Reflection.Missing.Value;
            // �ļ�����
            string excelPath = historyPath + "\\��Ȩ-" + this.cbxSelectKQuan.SelectedItem.ToString() + "-" + DateTime.Today.Year.ToString() + "��" + DateTime.Today.Month.ToString() + "��.xls";
            ExcelBook.SaveAs(excelPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            ExcelBook.Close(Type.Missing, excelPath, Type.Missing);
            ExcelApp.Quit();
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//ʵ����OLEDB���Ӷ���

            //ʹ�����񱣳����ݵ�һ������������

            string P_str_Sql_04 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB304_�ɿ�Ȩ] from JGAB304_�ɿ�Ȩ where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����  
            string P_str_Sql_21 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB321_�ɿ�Ȩ����] from JGAB321_�ɿ�Ȩ���� where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����    
            string P_str_Sql_08 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB308_�˲���] from JGAB308_�˲��� where CKZBH = '"
             + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//��¼����Excel�����  
            string P_str_Sql_06 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB306_����] from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_09 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB309_�˲��δ���] from JGAB309_�˲��δ��� where (TYBH IN (select DISTINCT TYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_12 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB312_��ζ��ձ�] from JGAB312_��ζ��ձ� where (HCTYBH IN (select DISTINCT TYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_10 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB310_ԭ���] from JGAB310_ԭ��� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_11 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB311_ԭ��δ���] from JGAB311_ԭ��δ��� where (KDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_18 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB318_��������] from JGAB318_�������� where (TYBH IN (select DISTINCT CLLYTYBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_17 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB317_ú������] from JGAB317_ú������ where (TYBH IN (select DISTINCT MCBH from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_19 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB319_���ζ��ձ�] from JGAB319_���ζ��ձ� where (YKDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_20 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB320_�ϲ�ԭ���] from JGAB320_�ϲ�ԭ��� where (HBTYBH IN (select DISTINCT YTYBH from JGAB319_���ζ��ձ� where (YKDBH IN (select DISTINCT YKDBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_01 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB301_�˲����] from JGAB301_�˲���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_02 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB302_ԭ�ϱ����] from JGAB302_ԭ�ϱ���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_03 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB303_���鹤����] from JGAB303_���鹤���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB305_̽��Ȩ] from JGAB305_̽��Ȩ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_07 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB307_�ɿ���] from JGAB307_�ɿ��� where  CKQBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_13 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB313_����Ŀ¼] from JGAB313_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB314_����Ŀ¼] from JGAB314_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB315_ר��ͼ��] from JGAB315_ר��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB316_ר��ͼ��ͼ��] from JGAB316_ר��ͼ��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";

            string[] SQLList = { P_str_Sql_04 , P_str_Sql_21 , P_str_Sql_08 , P_str_Sql_06 , P_str_Sql_09 , P_str_Sql_12 , P_str_Sql_10 , P_str_Sql_11
            ,P_str_Sql_18,P_str_Sql_17,P_str_Sql_19,P_str_Sql_20,P_str_Sql_01,P_str_Sql_02,P_str_Sql_03,P_str_Sql_05,P_str_Sql_07,P_str_Sql_13,P_str_Sql_14
            ,P_str_Sql_15,P_str_Sql_16};

            oledbcon.Open();//�����ݿ�����
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//��ʼ����
            try
            {
                for (int i = 0; i < SQLList.Length; i++)
                {
                    string strsql = SQLList[i].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        oledbcom.CommandText = strsql;
                        oledbcom.ExecuteNonQuery();
                    }
                    //SetTextMessage(i * 100 / SQLList.Length);
                }
                oledbcom.Transaction.Commit();
                MessageBox.Show("�����ɹ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����������⣬�ѳ�������������");
                oledbcom.Transaction.Rollback();//�ع����ݣ���֤���ݵ�������
            }
            finally
            {
                oledbcon.Close();//�ر����ݿ�����
                oledbcon.Dispose();//�ͷ���Դ
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.cbxSelectKQuan.SelectedItem == null)
            {
                MessageBox.Show("��ѡ���Ȩ��");
                return;
            }
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//ʵ����OLEDB���Ӷ���

            string P_str_Sql_17 = "delete from JGAB317_ú������ where (TYBH IN (select DISTINCT MCBH from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB308_�˲��� where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_07 = "delete from JGAB307_�ɿ��� where SSCKQ = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_03 = "delete from JGAB303_���鹤���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "delete from JGAB305_̽��Ȩ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_13 = "delete from JGAB313_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "delete from JGAB314_����Ŀ¼ where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "delete from JGAB315_ר��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "delete from JGAB316_ר��ͼ��ͼ�� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_06 = "delete from JGAB306_���� where (KTBH IN (select DISTINCT KTBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_04 = "delete from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_02 = "delete from JGAB302_ԭ�ϱ���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_01 = "delete from JGAB301_�˲���� where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_�ɿ�Ȩ  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string[] SQLList = { P_str_Sql_17, P_str_Sql_07 , P_str_Sql_03 , P_str_Sql_05 , P_str_Sql_13 , P_str_Sql_14 , P_str_Sql_15 , P_str_Sql_16 ,
            P_str_Sql_06,P_str_Sql_02,P_str_Sql_01,P_str_Sql_04};

            oledbcon.Open();//�����ݿ�����
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//��ʼ����
            try
            {
                for (int i = 0; i < SQLList.Length; i++)
                {
                    string strsql = SQLList[i].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        oledbcom.CommandText = strsql;
                        oledbcom.ExecuteNonQuery();
                    }
                }
                oledbcom.Transaction.Commit();
                MessageBox.Show("�����ɹ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����������⣬�ѳ�������������");
                oledbcom.Transaction.Rollback();//�ع����ݣ���֤���ݵ�������
            }
            finally
            {
                oledbcon.Close();//�ر����ݿ�����
                oledbcon.Dispose();//�ͷ���Դ
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//ʵ����OLEDB���Ӷ���

            string insert_sql01 = "insert into JGAB301_�˲���� select * from [;database=" + access_Path_New + ";].JGAB301_�˲����";
            string insert_sql02 = "insert into JGAB302_ԭ�ϱ���� select * from [;database=" + access_Path_New + ";].JGAB302_ԭ�ϱ����";
            string insert_sql03 = "insert into JGAB303_���鹤���� select * from [;database=" + access_Path_New + ";].JGAB303_���鹤����";
            string insert_sql04 = "insert into JGAB304_�ɿ�Ȩ select * from [;database=" + access_Path_New + ";].JGAB304_�ɿ�Ȩ";
            string insert_sql05 = "insert into JGAB305_̽��Ȩ select * from [;database=" + access_Path_New + ";].JGAB305_̽��Ȩ";
            string insert_sql06 = "insert into JGAB306_���� select * from [;database=" + access_Path_New + ";].JGAB306_����";
            string insert_sql07 = "insert into JGAB307_�ɿ��� select * from [;database=" + access_Path_New + ";].JGAB307_�ɿ���";
            string insert_sql18 = "insert into JGAB318_�������� select * from [;database=" + access_Path_New + ";].JGAB318_��������";
            string insert_sql08 = "insert into JGAB308_�˲��� select * from [;database=" + access_Path_New + ";].JGAB308_�˲���";
            string insert_sql09 = "insert into JGAB309_�˲��δ��� select * from [;database=" + access_Path_New + ";].JGAB309_�˲��δ���";
            string insert_sql10 = "insert into JGAB310_ԭ��� select * from [;database=" + access_Path_New + ";].JGAB310_ԭ���";
            string insert_sql11 = "insert into JGAB311_ԭ��δ��� select * from [;database=" + access_Path_New + ";].JGAB311_ԭ��δ���";
            string insert_sql12 = "insert into JGAB312_��ζ��ձ� select * from [;database=" + access_Path_New + ";].JGAB312_��ζ��ձ�";
            string insert_sql13 = "insert into JGAB313_����Ŀ¼ select * from [;database=" + access_Path_New + ";].JGAB313_����Ŀ¼";
            string insert_sql14 = "insert into JGAB314_����Ŀ¼ select * from [;database=" + access_Path_New + ";].JGAB314_����Ŀ¼";
            string insert_sql15 = "insert into JGAB315_ר��ͼ�� select * from [;database=" + access_Path_New + ";].JGAB315_ר��ͼ��";
            string insert_sql16 = "insert into JGAB316_ר��ͼ��ͼ�� select * from [;database=" + access_Path_New + ";].JGAB316_ר��ͼ��ͼ��";
            string insert_sql17 = "insert into JGAB317_ú������ select * from [;database=" + access_Path_New + ";].JGAB317_ú������";
            string insert_sql19 = "insert into JGAB319_���ζ��ձ� select * from [;database=" + access_Path_New + ";].JGAB319_���ζ��ձ�";
            string insert_sql20 = "insert into JGAB320_�ϲ�ԭ��� select * from [;database=" + access_Path_New + ";].JGAB320_�ϲ�ԭ���";
            string insert_sql21 = "insert into JGAB321_�ɿ�Ȩ���� select * from [;database=" + access_Path_New + ";].JGAB321_�ɿ�Ȩ����";

            string[] SQLList = { insert_sql01 , insert_sql02 ,insert_sql03, insert_sql04 , insert_sql05, insert_sql06, insert_sql07, insert_sql18, insert_sql08, insert_sql09,
            insert_sql10 , insert_sql11 ,insert_sql12, insert_sql13 , insert_sql14,insert_sql15 , insert_sql16 ,insert_sql17, insert_sql19 , insert_sql20,insert_sql21};

            oledbcon.Open();//�����ݿ�����
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//��ʼ����
            try
            {
                for (int i = 0; i < SQLList.Length; i++)
                {
                    string strsql = SQLList[i].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        oledbcom.CommandText = strsql;
                        oledbcom.ExecuteNonQuery();
                    }
                }
                oledbcom.Transaction.Commit();
                MessageBox.Show("�����ɹ���");
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����������⣬�ѳ�������������/br" + ex.Message);
                oledbcom.Transaction.Rollback();//�ع����ݣ���֤���ݵ�������
            }
            finally
            {
                oledbcon.Close();//�ر����ݿ�����
                oledbcon.Dispose();//�ͷ���Դ
            }
        }


    }
}