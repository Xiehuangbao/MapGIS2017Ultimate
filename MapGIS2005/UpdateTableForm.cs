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
        DataTable source;//待比较的原数据
        DataTable newData;//比较的新数据
        string strDBCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";

        //  //更新进度条列表
        //private delegate void SetPos(int ipos);

        ////更新进度条进度显示
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
        //        this.toolStripStatusLabel1.Text = "加载完成";
        //    }
        //}


        public UpdateTableForm(string access_Path,string access_Path_New,string historyPath)
        {
            InitializeComponent();
            this.access_Path = access_Path;
            this.access_Path_New = access_Path_New;
            this.historyPath = historyPath;
            string[] TableName = { "JGAB301_核查矿区", "JGAB302_原上表矿区", "JGAB303_勘查工作区", "JGAB304_采矿权", "JGAB305_探矿权",
                "JGAB306_矿体", "JGAB307_采空区", "JGAB308_核查块段", "JGAB309_核查块段储量", "JGAB310_原块段", "JGAB311_原块段储量",
                "JGAB312_块段对照表", "JGAB313_资料目录", "JGAB314_附件目录", "JGAB315_专题图件", "JGAB316_专题图件图层",
                "JGAB317_煤质特征", "JGAB318_储量利用", "JGAB319_大块段对照表", "JGAB320_合并原块段", "JGAB321_采矿权三率" };
            for (int i = 0; i < TableName.Length; i++)
            {
                cbxTableList.Items.Add(TableName[i]);
            }

            OleDbConnection conn = new OleDbConnection(strDBCon + access_Path);
            conn.Open();
            if (conn.State != ConnectionState.Open) return;
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select CKZBH from  JGAB304_采矿权";   //建立读取 C#操作Access之按列读取mdb  
            OleDbDataReader odrReader = cmd.ExecuteReader();
            while (odrReader.Read())
            {
                this.cbxSelectKQuan.Items.Add(odrReader["CKZBH"].ToString());
                this.cbxUpdateKQuan.Items.Add(odrReader["CKZBH"].ToString());
            }
            odrReader.Close();
            cmd.CommandText = "select HCKQBH from  JGAB304_采矿权";   //建立读取 C#操作Access之按列读取mdb  
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
            cmd.CommandText = "select YKDBH ,KDBH from  JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";   //建立读取 C#操作Access之按列读取mdb  
            OleDbDataReader odrReader = cmd.ExecuteReader();
            this.lstvKD.Clear();
            this.lstvKD.View = View.Details;
            this.lstvKD.GridLines = true;
            this.lstvKD.Columns.Add("原块段编号", 100, HorizontalAlignment.Left); //添加字段
            this.lstvKD.Columns.Add("核查块段编号", 100, HorizontalAlignment.Left); //添加字段
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
                MessageBox.Show("请选择矿区！");
                return;
            }
            string P_str_Sql_04 = "select *  from JGAB304_采矿权 where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句  
            string P_str_Sql_21 = "select * from JGAB321_采矿权三率 where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句    
            string P_str_Sql_08 = "select * from JGAB308_核查块段 where CKZBH = '"
             + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句  
            string P_str_Sql_06 = "select * from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_09 = "select * from JGAB309_核查块段储量 where (TYBH IN (select DISTINCT TYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_12 = "select * from JGAB312_块段对照表 where (HCTYBH IN (select DISTINCT TYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_10 = "select * from JGAB310_原块段 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_11 = "select * from JGAB311_原块段储量 where (KDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_18 = "select * from JGAB318_储量利用 where (TYBH IN (select DISTINCT CLLYTYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_17 = "select * from JGAB317_煤质特征 where (TYBH IN (select DISTINCT MCBH from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_19 = "select * from JGAB319_大块段对照表 where (YKDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_20 = "select * from JGAB320_合并原块段 where (HBTYBH IN (select DISTINCT YTYBH from JGAB319_大块段对照表 where (YKDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_01 = "select * from JGAB301_核查矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_02 = "select * from JGAB302_原上表矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_03 = "select * from JGAB303_勘查工作区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "select * from JGAB305_探矿权 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_07 = "select * from JGAB307_采空区 where  CKQBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_13 = "select * from JGAB313_资料目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "select * from JGAB314_附件目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "select * from JGAB315_专题图件 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "select * from JGAB316_专题图件图层 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            try
            {
                OleDbConnection conn = new OleDbConnection(strDBCon + access_Path);
                conn.Open();
                if (conn.State != ConnectionState.Open) return;
                string SQL = "";
                switch (cbxTableList.SelectedItem.ToString())
                {
                    case "JGAB301_核查矿区":
                        SQL = P_str_Sql_01;
                        break;
                    case "JGAB302_原上表矿区":
                        SQL = P_str_Sql_02;
                        break;
                    case "JGAB303_勘查工作区":
                        SQL = P_str_Sql_03;
                        break;
                    case "JGAB304_采矿权":
                        SQL = P_str_Sql_04;
                        break;
                    case "JGAB305_探矿权":
                        SQL = P_str_Sql_05;
                        break;
                    case "JGAB306_矿体":
                        SQL = P_str_Sql_06;
                        break;
                    case "JGAB307_采空区":
                        SQL = P_str_Sql_07;
                        break;
                    case "JGAB308_核查块段":
                        SQL = P_str_Sql_08;
                        break;
                    case "JGAB309_核查块段储量":
                        SQL = P_str_Sql_09;
                        break;
                    case "JGAB310_原块段":
                        SQL = P_str_Sql_10;
                        break;
                    case "JGAB311_原块段储量":
                        SQL = P_str_Sql_11;
                        break;
                    case "JGAB312_块段对照表":
                        SQL = P_str_Sql_12;
                        break;
                    case "JGAB313_资料目录":
                        SQL = P_str_Sql_13;
                        break;
                    case "JGAB314_附件目录":
                        SQL = P_str_Sql_14;
                        break;
                    case "JGAB315_专题图件":
                        SQL = P_str_Sql_15;
                        break;
                    case "JGAB316_专题图件图层":
                        SQL = P_str_Sql_16;
                        break;
                    case "JGAB317_煤质特征":
                        SQL = P_str_Sql_17;
                        break;
                    case "JGAB318_储量利用":
                        SQL = P_str_Sql_18;
                        break;
                    case "JGAB319_大块段对照表":
                        SQL = P_str_Sql_19;
                        break;
                    case "JGAB320_合并原块段":
                        SQL = P_str_Sql_20;
                        break;
                    case "JGAB321_采矿权三率":
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

        //获取dataset
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
        /// 比较两个DataTable数据（结构相同）
        /// </summary>
        /// <param name="dt1">来自数据库的DataTable</param>
        /// <param name="dt2">来自文件的DataTable</param>
        /// <param name="keyField">关键字段名</param>
        /// <param name="dtRetAdd">新增数据（dt2中的数据）</param>
        /// <param name="dtRetDif1">不同的数据（数据库中的数据）</param>
        /// <param name="dtRetDif2">不同的数据（图2中的数据）</param>
        /// <param name="dtRetDel">删除的数据（dt2中的数据）</param>
        public static void CompareDt(DataTable dt1, DataTable dt2, string keyField,
            out DataTable dtRetAdd, out DataTable dtRetDif1, out DataTable dtRetDif2,
            out DataTable dtRetDel)
        {
            //为三个表拷贝表结构
            dtRetDel = dt1.Clone();
            dtRetAdd = dtRetDel.Clone();
            dtRetDif1 = dtRetDel.Clone();
            dtRetDif2 = dtRetDel.Clone();

            int colCount = dt1.Columns.Count;

            DataView dv1 = dt1.DefaultView;
            DataView dv2 = dt2.DefaultView;

            //先以第一个表为参照，看第二个表是修改了还是删除了
            foreach (DataRowView dr1 in dv1)
            {
                dv2.RowFilter = keyField + " = '" + dr1[keyField].ToString() + "'";
                if (dv2.Count > 0)
                {
                    if (!CompareUpdate(dr1, dv2[0]))//比较是否有不同的
                    {
                        dtRetDif1.Rows.Add(dr1.Row.ItemArray);//修改前
                        dtRetDif2.Rows.Add(dv2[0].Row.ItemArray);//修改后
                        dtRetDif2.Rows[dtRetDif2.Rows.Count - 1]["TYBH"] = dr1.Row["TYBH"];//将ID赋给来自文件的表，因为它的ID全部==0
                        continue;
                    }
                }
                else
                {
                    //已经被删除的
                    dtRetDel.Rows.Add(dr1.Row.ItemArray);
                }
            }

            //以第一个表为参照，看记录是否是新增的
            dv2.RowFilter = "";//清空条件
            foreach (DataRowView dr2 in dv2)
            {
                dv1.RowFilter = keyField + " = '" + dr2[keyField].ToString() + "'";
                if (dv1.Count == 0)
                {
                    //新增的
                    dtRetAdd.Rows.Add(dr2.Row.ItemArray);
                }
            }
        }

        //比较是否有不同的
        private static bool CompareUpdate(DataRowView dr1, DataRowView dr2)
        {
            //行里只要有一项不一样，整个行就不一样,无需比较其它
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
                MessageBox.Show("请选择矿权！");
                return;
            }
            if (this.cbxUpdateKQuan.SelectedItem == null || this.cbxKQu.SelectedItem == null || this.dateTimeInput1 == null
                || this.txtUpdateReason == null || this.txtOperator == null || this.txtManager == null || this.txtComment == null)
            {
                MessageBox.Show("请完善记录信息！");
                return;
            }
            string[] TableName = { "JGAB301_核查矿区", "JGAB302_原上表矿区", "JGAB303_勘查工作区", "JGAB304_采矿权", "JGAB305_探矿权",
                "JGAB306_矿体", "JGAB307_采空区", "JGAB308_核查块段", "JGAB309_核查块段储量", "JGAB310_原块段", "JGAB311_原块段储量",
                "JGAB312_块段对照表", "JGAB313_资料目录", "JGAB314_附件目录", "JGAB315_专题图件", "JGAB316_专题图件图层",
                "JGAB317_煤质特征", "JGAB318_储量利用", "JGAB319_大块段对照表", "JGAB320_合并原块段", "JGAB321_采矿权三率" };
            string[] tableField = { "TZYSBH", "TYBH", "HCKQBH", "CKZBH", "CKQR", "CKQFW", "DZ", "KSBH", "KSMC", "FZJG", "YXQQ", "YXQZ", "XKCSS", "XKCSX", "KCZKZ", "ZKZMC", "ZYJSL", "ZYKSL", "BYJSL", "BYKSL", "DKSNL", "DJSNL", "SKSNL", "SJSNL", "NDKSL", "NDJSL", "KCFSM", "KCFS", "XKFSM", "XKFS", "RXKSL", "KQBH", "JJLXM", "JJLX", "CYRY", "NCZ", "JSSCCB", "KCPLX", "CXSBSL" };
            // 文件保存路径及名称


            // 创建Excel文档
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;
            string sheetName = "";
            //删除自己生成的两个sheet
            for (int i = 2; i < 4; i++)
            {
                sheetName = "Sheet" + i;
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelBook.Sheets.get_Item(sheetName);
                sheet.Delete();
            }
            for (int i = 0; i < 1; i++)
            {
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Name = "更新日志表";

                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 1] = "更新矿权";//也可以这样赋值
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 2] = "所属矿区";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 3] = "更新时间";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 4] = "更新原因";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 5] = "负责人";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 6] = "操作员";
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).Cells[2, 7] = "备注";

                //合并 单元格 设置表头
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F2").MergeCells);
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G2").Merge(((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G2").MergeCells);
                //得到  Range 范围   域对象
                Microsoft.Office.Interop.Excel.Range range = ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G69");
                //设置 该range内的  样式   颜色  边框 

                ////设置Excel表格的  列宽
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "A69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("B1", "B69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("C1", "C69").ColumnWidth = 30;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("D1", "D69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("E1", "E69").ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("F1", "F69").ColumnWidth = 30;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("G1", "G69").ColumnWidth = 30;
                //设置  域 Range  的颜色   从 A1到W1
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G1").Interior.ColorIndex = 15;
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A2", "G2").Interior.ColorIndex = 15;

                //设置某个域range被选中  
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A3", "G3").Select();

                //左右   设置 选中域内的  Excel单元格从C 到W  是活动的     前面的A B  为固定的
                //但是 上下 方向 表头（这里表头合并两行 ）没有固定  选C3 到W3（表示从C的第三行开始 为 活动 的   上面两行为固定的）
                ExcelApp.ActiveWindow.FreezePanes = true;

                //设置 某个域range内 单元格里的字体颜色
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A1", "G2").Font.Color = -16744448;//（搜索Excel颜色对照表）
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[i + 1]).get_Range("A3", "G24").Font.Color = -16776961;
                //文字 居中   
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.Font.Size = 10;
                range.Borders.LineStyle = 1;
                //设置边框
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());
                range.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;
                range.WrapText = true;
                //赋值    就
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
            // 文件保存
            string excelPath = historyPath + "\\矿权-" + this.cbxSelectKQuan.SelectedItem.ToString() + "-" + DateTime.Today.Year.ToString() + "年" + DateTime.Today.Month.ToString() + "月.xls";
            ExcelBook.SaveAs(excelPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            ExcelBook.Close(Type.Missing, excelPath, Type.Missing);
            ExcelApp.Quit();
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//实例化OLEDB连接对象

            //使用事务保持数据的一致性与完整性

            string P_str_Sql_04 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB304_采矿权] from JGAB304_采矿权 where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句  
            string P_str_Sql_21 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB321_采矿权三率] from JGAB321_采矿权三率 where CKZBH = '"
                + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句    
            string P_str_Sql_08 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB308_核查块段] from JGAB308_核查块段 where CKZBH = '"
             + this.cbxSelectKQuan.SelectedItem.ToString() + "'";//记录连接Excel的语句  
            string P_str_Sql_06 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB306_矿体] from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_09 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB309_核查块段储量] from JGAB309_核查块段储量 where (TYBH IN (select DISTINCT TYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_12 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB312_块段对照表] from JGAB312_块段对照表 where (HCTYBH IN (select DISTINCT TYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_10 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB310_原块段] from JGAB310_原块段 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_11 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB311_原块段储量] from JGAB311_原块段储量 where (KDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_18 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB318_储量利用] from JGAB318_储量利用 where (TYBH IN (select DISTINCT CLLYTYBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_17 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB317_煤质特征] from JGAB317_煤质特征 where (TYBH IN (select DISTINCT MCBH from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_19 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB319_大块段对照表] from JGAB319_大块段对照表 where (YKDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_20 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB320_合并原块段] from JGAB320_合并原块段 where (HBTYBH IN (select DISTINCT YTYBH from JGAB319_大块段对照表 where (YKDBH IN (select DISTINCT YKDBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_01 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB301_核查矿区] from JGAB301_核查矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_02 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB302_原上表矿区] from JGAB302_原上表矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_03 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB303_勘查工作区] from JGAB303_勘查工作区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB305_探矿权] from JGAB305_探矿权 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_07 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB307_采空区] from JGAB307_采空区 where  CKQBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_13 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB313_资料目录] from JGAB313_资料目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB314_附件目录] from JGAB314_附件目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB315_专题图件] from JGAB315_专题图件 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "select * into [Excel 8.0;database=" + excelPath + "]." + "[JGAB316_专题图件图层] from JGAB316_专题图件图层 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";

            string[] SQLList = { P_str_Sql_04 , P_str_Sql_21 , P_str_Sql_08 , P_str_Sql_06 , P_str_Sql_09 , P_str_Sql_12 , P_str_Sql_10 , P_str_Sql_11
            ,P_str_Sql_18,P_str_Sql_17,P_str_Sql_19,P_str_Sql_20,P_str_Sql_01,P_str_Sql_02,P_str_Sql_03,P_str_Sql_05,P_str_Sql_07,P_str_Sql_13,P_str_Sql_14
            ,P_str_Sql_15,P_str_Sql_16};

            oledbcon.Open();//打开数据库连接
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//开始事务
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
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作遇到问题，已撤销所做操作！");
                oledbcom.Transaction.Rollback();//回滚数据，保证数据的完整性
            }
            finally
            {
                oledbcon.Close();//关闭数据库连接
                oledbcon.Dispose();//释放资源
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.cbxSelectKQuan.SelectedItem == null)
            {
                MessageBox.Show("请选择矿权！");
                return;
            }
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//实例化OLEDB连接对象

            string P_str_Sql_17 = "delete from JGAB317_煤质特征 where (TYBH IN (select DISTINCT MCBH from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB308_核查块段 where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))))";
            string P_str_Sql_07 = "delete from JGAB307_采空区 where SSCKQ = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_03 = "delete from JGAB303_勘查工作区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_05 = "delete from JGAB305_探矿权 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_13 = "delete from JGAB313_资料目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_14 = "delete from JGAB314_附件目录 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_15 = "delete from JGAB315_专题图件 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_16 = "delete from JGAB316_专题图件图层 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_06 = "delete from JGAB306_矿体 where (KTBH IN (select DISTINCT KTBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_04 = "delete from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'";
            string P_str_Sql_02 = "delete from JGAB302_原上表矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string P_str_Sql_01 = "delete from JGAB301_核查矿区 where (HCKQBH IN (select DISTINCT HCKQBH from JGAB304_采矿权  where CKZBH = '" + this.cbxSelectKQuan.SelectedItem.ToString() + "'))";
            string[] SQLList = { P_str_Sql_17, P_str_Sql_07 , P_str_Sql_03 , P_str_Sql_05 , P_str_Sql_13 , P_str_Sql_14 , P_str_Sql_15 , P_str_Sql_16 ,
            P_str_Sql_06,P_str_Sql_02,P_str_Sql_01,P_str_Sql_04};

            oledbcon.Open();//打开数据库连接
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//开始事务
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
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作遇到问题，已撤销所做操作！");
                oledbcom.Transaction.Rollback();//回滚数据，保证数据的完整性
            }
            finally
            {
                oledbcon.Close();//关闭数据库连接
                oledbcon.Dispose();//释放资源
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string P_str_Con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + access_Path + ";Persist Security Info=False";
            OleDbConnection oledbcon = new OleDbConnection(P_str_Con);//实例化OLEDB连接对象

            string insert_sql01 = "insert into JGAB301_核查矿区 select * from [;database=" + access_Path_New + ";].JGAB301_核查矿区";
            string insert_sql02 = "insert into JGAB302_原上表矿区 select * from [;database=" + access_Path_New + ";].JGAB302_原上表矿区";
            string insert_sql03 = "insert into JGAB303_勘查工作区 select * from [;database=" + access_Path_New + ";].JGAB303_勘查工作区";
            string insert_sql04 = "insert into JGAB304_采矿权 select * from [;database=" + access_Path_New + ";].JGAB304_采矿权";
            string insert_sql05 = "insert into JGAB305_探矿权 select * from [;database=" + access_Path_New + ";].JGAB305_探矿权";
            string insert_sql06 = "insert into JGAB306_矿体 select * from [;database=" + access_Path_New + ";].JGAB306_矿体";
            string insert_sql07 = "insert into JGAB307_采空区 select * from [;database=" + access_Path_New + ";].JGAB307_采空区";
            string insert_sql18 = "insert into JGAB318_储量利用 select * from [;database=" + access_Path_New + ";].JGAB318_储量利用";
            string insert_sql08 = "insert into JGAB308_核查块段 select * from [;database=" + access_Path_New + ";].JGAB308_核查块段";
            string insert_sql09 = "insert into JGAB309_核查块段储量 select * from [;database=" + access_Path_New + ";].JGAB309_核查块段储量";
            string insert_sql10 = "insert into JGAB310_原块段 select * from [;database=" + access_Path_New + ";].JGAB310_原块段";
            string insert_sql11 = "insert into JGAB311_原块段储量 select * from [;database=" + access_Path_New + ";].JGAB311_原块段储量";
            string insert_sql12 = "insert into JGAB312_块段对照表 select * from [;database=" + access_Path_New + ";].JGAB312_块段对照表";
            string insert_sql13 = "insert into JGAB313_资料目录 select * from [;database=" + access_Path_New + ";].JGAB313_资料目录";
            string insert_sql14 = "insert into JGAB314_附件目录 select * from [;database=" + access_Path_New + ";].JGAB314_附件目录";
            string insert_sql15 = "insert into JGAB315_专题图件 select * from [;database=" + access_Path_New + ";].JGAB315_专题图件";
            string insert_sql16 = "insert into JGAB316_专题图件图层 select * from [;database=" + access_Path_New + ";].JGAB316_专题图件图层";
            string insert_sql17 = "insert into JGAB317_煤质特征 select * from [;database=" + access_Path_New + ";].JGAB317_煤质特征";
            string insert_sql19 = "insert into JGAB319_大块段对照表 select * from [;database=" + access_Path_New + ";].JGAB319_大块段对照表";
            string insert_sql20 = "insert into JGAB320_合并原块段 select * from [;database=" + access_Path_New + ";].JGAB320_合并原块段";
            string insert_sql21 = "insert into JGAB321_采矿权三率 select * from [;database=" + access_Path_New + ";].JGAB321_采矿权三率";

            string[] SQLList = { insert_sql01 , insert_sql02 ,insert_sql03, insert_sql04 , insert_sql05, insert_sql06, insert_sql07, insert_sql18, insert_sql08, insert_sql09,
            insert_sql10 , insert_sql11 ,insert_sql12, insert_sql13 , insert_sql14,insert_sql15 , insert_sql16 ,insert_sql17, insert_sql19 , insert_sql20,insert_sql21};

            oledbcon.Open();//打开数据库连接
            OleDbCommand oledbcom = new OleDbCommand();
            oledbcom.Connection = oledbcon;
            oledbcom.Transaction = oledbcon.BeginTransaction();//开始事务
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
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作遇到问题，已撤销所做操作！/br" + ex.Message);
                oledbcom.Transaction.Rollback();//回滚数据，保证数据的完整性
            }
            finally
            {
                oledbcon.Close();//关闭数据库连接
                oledbcon.Dispose();//释放资源
            }
        }


    }
}