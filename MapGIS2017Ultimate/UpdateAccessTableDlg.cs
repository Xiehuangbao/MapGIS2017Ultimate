using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkSpace;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
using System.Data.OleDb;
namespace MapGIS2017Ultimate
{
    public partial class UpdateAccessTableDlg : DevComponents.DotNetBar.Office2007Form
    {
        Dictionary<Dictionary<string, string>, Dictionary<string, string>> annoMappingDic = null;
        List<string> sourceAttr = null;
        List<string> newAttr = null;

        //构造函数，将数据传输到页面中
        public UpdateAccessTableDlg(Dictionary<Dictionary<string, string>, Dictionary<string, string>> annoMappingDic,
            List<string> sourceAttr, List<string> newAttr,string DBPath)
        {
            InitializeComponent();
            this.annoMappingDic = annoMappingDic;
            this.sourceAttr = sourceAttr;
            this.newAttr = newAttr;
            this.DBPath = DBPath;
        }

        //映射关系数据库
        OleDbConnection MapConn = null;
        OleDbDataReader MapAdr = null;
        OleDbDataReader Map_CL_Adr = null;
        OleDbDataReader _Map_CL_Adr = null;
        //原始数据数据库
        OleDbConnection Conn = null;
        OleDbDataReader Adr = null;
        OleDbDataReader CL_Adr = null;
        string DBPath = "";

        List<DataGridView> dgvList = new List<DataGridView>();//记录下所有的DataGridView用以更新数据库
        List<DataTable> TableList = new List<DataTable>();
        private void UpdateAccessTableDlg_Load(object sender, EventArgs e)
        {
            //清空DataGridView数据
            //if (dgvListTableView.RowCount > 1)
            //{
            //    dgvListTableView.DataSource = null;
            //}
            try
            {
                //核查块段表字段对应
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
                string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
                MapConn = AccessUtils.GetConn(MapDBPath);
 
                Conn = AccessUtils.GetConn(DBPath);

                //计算中心点坐标最近的圆
                foreach (KeyValuePair<Dictionary<string, string>, Dictionary<string, string>> annoKVP in annoMappingDic)
                {
                    DataGridView dgvListTableView = new DataGridView();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("表名");
                    dt.Columns.Add("字段");
                    dt.Columns.Add("原表值");
                    dt.Columns.Add("原图值");
                    dt.Columns.Add("新图值");
                    dt.Columns.Add("字段值");

                    Dictionary<string, string> sourceCircleAnnoDic = annoKVP.Key;

                    Dictionary<string, string> newCircleAnnoDic = annoKVP.Value;
   
                    //匹配核查块段表
                    string MapSQL = "select FieldName,LegendAnno,FieldDesc from JGAB308_核查块段";
                    MapAdr = AccessUtils.GetDataReader(MapSQL, MapConn);
                    string sourceKDBH = "";
                    while (MapAdr.Read())
                    {
                        List<string> FieldRow = new List<string>();//匹配列，也就是说需要更新的字段
                        //将对应关系数据表中的映射项拆分为不同项，依次与标注比较
                        bool flag = false;
                        if (!MapAdr["LegendAnno"].ToString().Equals(""))//如果字段对应列不为“”
                        {

                            string[] PatternStr = MapAdr["LegendAnno"].ToString().Split(',');
                            for (int i = 0; i < PatternStr.Length; i++)
                            {
                                bool Break = false;
                                //遍历图例标注
                                for (int j = 0; j < newAttr.Count; j++)
                                {
                                    //判断图例标注是否有和核查块段对应关系表匹配的字段
                                    string[] lengendAnno = newAttr[j].Split('、');
                                    Regex r = new Regex(PatternStr[i]);
                                    if (r.Match(lengendAnno[1]).Success)
                                    {
                                        //表名
                                        FieldRow.Add("JGAB308_核查块段");
                                        //字段名
                                        FieldRow.Add(MapAdr["FieldDesc"].ToString());
                                        //获取原表值

                                        sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                        string SQL = "select " + MapAdr["FieldName"].ToString() + " from JGAB308_核查块段 where KDBH ='" + sourceKDBH + "'";
                                        Adr = AccessUtils.GetDataReader(SQL, Conn);
                                        while (Adr.Read())
                                        {
                                            FieldRow.Add(Adr[MapAdr["FieldName"].ToString()].ToString());
                                        }
                                        //原图值
                                        string sourceValue = "";
                                        sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                        FieldRow.Add(sourceValue);

                                        //新图值
                                        string newValue = "";
                                        newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                        FieldRow.Add(newValue);

                                        //字段值
                                        FieldRow.Add(MapAdr["FieldName"].ToString());
                                        Break = true;
                                        flag = true;
                                        break;
                                    }
                                }
                                if (Break)
                                {
                                    break;
                                }
                            }
                            if (flag)//如果匹配成功，再添加
                            {
                                dt.Rows.Add(FieldRow.ToArray());
                            }
                        }
                    }

                    //释放资源
                    MapAdr.Close();
                    Adr.Close();

                    //匹配核查块段储量表，添加每个块段相同项，品位，储量什么的单独显示，分矿种
                    string _Map_CL_SQL = "select FieldName,LegendAnno,FieldDesc from JGAB309_核查块段储量";
                    _Map_CL_Adr = AccessUtils.GetDataReader(_Map_CL_SQL, MapConn);
                    while (_Map_CL_Adr.Read())
                    {
                        List<string> FieldRow = new List<string>();//匹配列，也就是说需要更新的字段
                        //将对应关系数据表中的映射项拆分为不同项，依次与标注比较
                        bool flag = false;
                        //如果字段对应列不为“”
                        if (!_Map_CL_Adr["LegendAnno"].ToString().Equals(""))
                        {
                            //以上是判断矿产种类代码
                            string[] PatternStr = _Map_CL_Adr["LegendAnno"].ToString().Split(',');
                            for (int i = 0; i < PatternStr.Length; i++)
                            {
                                bool Break = false;
                                //遍历图例标注
                                for (int j = 0; j < newAttr.Count; j++)
                                {
                                    //判断图例标注是否有和核查块段对应关系表匹配的字段
                                    string[] lengendAnno = newAttr[j].Split('、');
                                    Regex r = new Regex(PatternStr[i]);
                                    if (r.Match(lengendAnno[1]).Success)
                                    {
                                        //如果字段是PW
                                        if ("PW".Equals(_Map_CL_Adr["FieldName"].ToString()) || "HCJSL".Equals(_Map_CL_Adr["FieldName"].ToString()))
                                        {

                                        }//如果是核查金属量字段
                                        else
                                        {
                                            //表名
                                            FieldRow.Add("JGAB309_核查块段储量");
                                            //字段名
                                            FieldRow.Add(_Map_CL_Adr["FieldDesc"].ToString());
                                            //获取原表值

                                            sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                            string SQL = "select " + _Map_CL_Adr["FieldName"].ToString() + " from JGAB309_核查块段储量 where KDBH ='" + sourceKDBH + "'";
                                            CL_Adr = AccessUtils.GetDataReader(SQL, Conn);
                                            if (CL_Adr.HasRows)
                                            {
                                                while (CL_Adr.Read())
                                                {

                                                    FieldRow.Add(CL_Adr[_Map_CL_Adr["FieldName"].ToString()].ToString());
                                                    break;//只获取该块段的第一个数据就可以了
                                                }
                                            }
                                            else
                                            {
                                                FieldRow.Add(" ");
                                            }
                                            //原图值
                                            string sourceValue = "";
                                            sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                            FieldRow.Add(sourceValue);

                                            //新图值
                                            string newValue = "";
                                            newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                            FieldRow.Add(newValue);

                                            //字段值
                                            FieldRow.Add(_Map_CL_Adr["FieldName"].ToString());
                                            Break = true;
                                            flag = true;
                                            break;
                                        }

                                    }
                                }
                                if (Break)
                                {
                                    break;
                                }
                            }
                            if (flag)//如果匹配成功，再添加
                            {
                                dt.Rows.Add(FieldRow.ToArray());
                            }
                        }
                    }
                    _Map_CL_Adr.Close();
                    //先匹配矿石种类,找到该标注中一共有几种类型的矿种

                    //从矿石类型表中匹配矿石类型
                    string Map_KSLX_SQL = "select KCType,KCTypeMapping from 矿产类型映射";
                    OleDbDataReader KSLX_Adr = null;
                    KSLX_Adr = AccessUtils.GetDataReader(Map_KSLX_SQL, MapConn);
                    while (KSLX_Adr.Read())
                    {
                        if (!KSLX_Adr["KCTypeMapping"].ToString().Equals(""))//如果字段对应列不为“”
                        {
                            string[] PatternType = KSLX_Adr["KCTypeMapping"].ToString().Split(',');//匹配模板
                            for (int LX_i = 0; LX_i < PatternType.Length; LX_i++)
                            {   //遍历图例标注
                                bool LX_Break = false;
                                for (int LX_j = 0; LX_j < newAttr.Count; LX_j++)
                                {
                                    //判断图例标注是否有和核查块段对应关系表匹配的字段
                                    string[] LX_lengendAnno = newAttr[LX_j].Split('、');
                                    Regex LX_r = new Regex(PatternType[LX_i]);

                                    //找到具体矿产种类以后
                                    if (LX_r.Match(LX_lengendAnno[1]).Success)
                                    {
                                        //KSLX_Adr["KCType"]矿产种类
                                        string Map_CL_SQL = "select FieldName,LegendAnno,FieldDesc from JGAB309_核查块段储量";
                                        Map_CL_Adr = AccessUtils.GetDataReader(Map_CL_SQL, MapConn);

                                        while (Map_CL_Adr.Read())
                                        {
                                            List<string> FieldRow = new List<string>();//匹配列，也就是说需要更新的字段
                                            //将对应关系数据表中的映射项拆分为不同项，依次与标注比较
                                            bool flag = false;
                                            //如果字段对应列不为“”
                                            if (!Map_CL_Adr["LegendAnno"].ToString().Equals(""))
                                            {
                                                //以上是判断矿产种类代码
                                                string[] PatternStr = Map_CL_Adr["LegendAnno"].ToString().Split(',');
                                                for (int i = 0; i < PatternStr.Length; i++)
                                                {
                                                    bool Break = false;
                                                    bool _flag = true;
                                                    //遍历图例标注
                                                    for (int j = 0; j < newAttr.Count; j++)
                                                    {
                                                        //判断图例标注是否有和核查块段对应关系表匹配的字段
                                                        string[] lengendAnno = newAttr[j].Split('、');
                                                        Regex r = new Regex(PatternStr[i]);
                                                        if (r.Match(lengendAnno[1]).Success)
                                                        {
                                                            //如果字段是PW
                                                            if ("PW".Equals(Map_CL_Adr["FieldName"].ToString()) || "HCJSL".Equals(Map_CL_Adr["FieldName"].ToString()))
                                                            {
                                                                //检查该标注的矿产种类
                                                                string type = GetKCLX(newAttr[j], MapConn);
                                                                if (type.Equals(KSLX_Adr["KCType"].ToString()))
                                                                {
                                                                    //表名
                                                                    FieldRow.Add("JGAB309_核查块段储量-" + KSLX_Adr["KCType"].ToString());
                                                                    //字段名
                                                                    FieldRow.Add(Map_CL_Adr["FieldDesc"].ToString());
                                                                    //获取原表值

                                                                    sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                                                    string SQL = "select " + Map_CL_Adr["FieldName"].ToString() + " from JGAB309_核查块段储量 where KDBH ='" + sourceKDBH + "' and KCMC = '" + KSLX_Adr["KCType"].ToString() + "'";
                                                                    CL_Adr = AccessUtils.GetDataReader(SQL, Conn);
                                                                    //如果数据库中没有值，以空格代替，防止后面的值前移
                                                                    if (CL_Adr.HasRows)
                                                                    {
                                                                        while (CL_Adr.Read())
                                                                        {

                                                                            FieldRow.Add(CL_Adr[Map_CL_Adr["FieldName"].ToString()].ToString());
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        FieldRow.Add(" ");
                                                                    }
                                                                    //原图值
                                                                    string sourceValue = "";
                                                                    sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                                                    FieldRow.Add(sourceValue);

                                                                    //新图值
                                                                    string newValue = "";
                                                                    newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                                                    FieldRow.Add(newValue);

                                                                    //字段值
                                                                    FieldRow.Add(Map_CL_Adr["FieldName"].ToString());
                                                                    Break = true;
                                                                    flag = true;
                                                                    break;
                                                                }
                                                            }//如果是核查金属量字段

                                                        }
                                                    }
                                                    if (Break)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (flag)//如果匹配成功，再添加
                                                {
                                                    dt.Rows.Add(FieldRow.ToArray());
                                                }
                                            }
                                        }

                                        LX_Break = true;
                                        break;//跳出当前矿种的遍历，遍历下一种矿产
                                    }

                                    //找到具体矿产种类以后
                                }
                                //跳出当前矿种，选择下一种矿
                                if (LX_Break)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    TableList.Add(dt);
                    dgvListTableView.DataSource = dt;
                    dgvListTableView.ColumnHeadersHeight = 40;
                    dgvListTableView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dgvListTableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvListTableView.AllowUserToAddRows = false;
                    dgvListTableView.CellPainting += new DataGridViewCellPaintingEventHandler(dgv_CellPaint);



                    //动态添加Tab显示不同块段的数据
                    DevComponents.DotNetBar.SuperTabControlPanel panel = new DevComponents.DotNetBar.SuperTabControlPanel();
                    DevComponents.DotNetBar.SuperTabItem tabItem = this.superTabControl1.CreateTab(sourceKDBH + "块段");
                    tabItem.AttachedControl = panel;
                    panel.Controls.Add(dgvListTableView);
                    dgvListTableView.Dock = DockStyle.Fill;
                    this.superTabControl1.Controls.Add(panel);
                    //dgvList.Add(dgvListTableView);
                }
                //默认让tabcontrol选中最后一个
                //this.superTabControl1.SelectedTabIndex = this.superTabControl1.Tabs.Count - 1;
                Map_CL_Adr.Close();
                CL_Adr.Close();
                MapConn.Close();
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //从绘每个单元格，合并单元格
        private void dgv_CellPaint(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                DataGridView dgvListTableView = (DataGridView)sender;
                
                dgvListTableView.Columns[5].Visible = false;
                // 对第1列相同单元格进行合并
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    using
                        (
                        Brush gridBrush = new SolidBrush(dgvListTableView.GridColor),
                        backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                        )
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // 清除单元格
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // 画 Grid 边线（仅画单元格的底边线和右边线）
                            //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                            if ((e.RowIndex < dgvListTableView.Rows.Count - 1) && (Convert.ToString(dgvListTableView.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value) != e.Value.ToString()))
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            }
                            //画最后一行线
                            if ((e.RowIndex == dgvListTableView.Rows.Count - 1))
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            }
                            // 画右边线
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom);


                            // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                            if (e.Value != null)
                            {
                                if ((e.RowIndex > 0) && (Convert.ToString(dgvListTableView.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value) == e.Value.ToString()))
                                {
                                    dgvListTableView.Rows[e.RowIndex].Cells[0].Tag = e.Value;
                                }
                                else
                                {
                                    e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                        Brushes.Black, e.CellBounds.X + 2,
                                        e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                    dgvListTableView.Rows[e.RowIndex].Cells[0].Tag = e.Value;
                                }
                            }
                            e.Handled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        //根据图例的说明，获取矿产类型
        public string GetKCLX(string Anno, OleDbConnection MapConn)
        {
            string Type = "";
            //从矿石类型表中匹配矿石类型
            string Map_KSLX_SQL = "select KCType,KCTypeMapping from 矿产类型映射";
            OleDbDataReader KSLX_Adr = null;
            KSLX_Adr = AccessUtils.GetDataReader(Map_KSLX_SQL, MapConn);
            while (KSLX_Adr.Read())
            {
                if (!KSLX_Adr["KCTypeMapping"].ToString().Equals(""))//如果字段对应列不为“”
                {
                    //匹配模板
                    string[] PatternStr = KSLX_Adr["KCTypeMapping"].ToString().Split(',');
                    for (int i = 0; i < PatternStr.Length; i++)
                    {
                        //判断图例标注是否有和核查块段对应关系表匹配的字段
                        string[] lengendAnno = Anno.Split('、');
                        Regex r = new Regex(PatternStr[i]);
                        //找到具体矿产种类以后
                        if (r.Match(lengendAnno[1]).Success)
                        {
                            //匹配到矿产种类以后，逐个矿产进行处理，比如说先匹配到铅矿，那么下面只处理铅矿
                            //KSLX_Adr["KCType"]矿产种类
                            Type = KSLX_Adr["KCType"].ToString();
                        }
                    }
                }
            }
            KSLX_Adr.Close();
            return Type;
        }

        //确认无误后，可以更新入库
        private void btnBeginUpdateAccess_Click(object sender, EventArgs e)
        {
           

            //连接数据库
            OleDbConnection Conn = null;
            OleDbCommand cmd = null;
            string KTBH = "";
            string newKTBH = "";
            string DBPath = @"C:\东岗山矿\旧-S430424003_东岗山铅锌矿核查区-铅矿\ACCESS数据库\S430424003_东岗山银铅锌萤石矿.mdb";
            try
            {
                Conn = AccessUtils.GetConn(DBPath);
                //遍历所有的DataGridView
                for (int i = 0; i < TableList.Count; i++)
                {
                    DataTable rmv = TableList[i];
                    int rowCount = rmv.Rows.Count;
                    string Sql = "";//拼接更新字符串
                    string HCKDBH = "";
                    string newHCKDBH = "";
                    for (int j = 0; j < rowCount; j++)
                    {
                        string cellValue = rmv.Rows[j][0].ToString();
                        
                        if (cellValue.Split('-').Length == 1)//如果长度为1的话，说明第一列只是表名
                        {

                            int count = 0;//记录一组有多少
                            if ("核查块段编号".Equals(rmv.Rows[j][1].ToString()))
                            {
                                HCKDBH = rmv.Rows[j][2].ToString();
                                newHCKDBH = rmv.Rows[j][4].ToString();
                            }
                            Sql += rmv.Rows[j][5].ToString() + " = '" + rmv.Rows[j][4].ToString() + "', ";
                            for (int k = j + 1; k < rowCount; k++)
                            {
                                if (rmv.Rows[k][0].ToString().Equals(cellValue))
                                {
                                    if ("核查块段编号".Equals(rmv.Rows[k][0].ToString()))
                                    {
                                        HCKDBH = rmv.Rows[k][2].ToString();
                                        newHCKDBH = rmv.Rows[j][4].ToString();
                                    }
                                    Sql += rmv.Rows[k][5].ToString() + " = '" + rmv.Rows[k][4].ToString() + "', ";
                                    count++;
                                }
                            }
                            string updateSQL = "update " + cellValue + " set " + Sql.Substring(0,Sql.Length-2) + " where KDBH ='" + HCKDBH + "'";
                            cmd = new OleDbCommand(updateSQL, Conn);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            Sql = "";
                            j += count;

                        }
                        else//如果长度> 2 说明表名后面是矿种
                        {
                            int count = 0;
                            if ("核查块段编号".Equals(rmv.Rows[j][1].ToString()))
                            {
                                HCKDBH = rmv.Rows[j][2].ToString();
                                newHCKDBH = rmv.Rows[j][4].ToString();
                            }
                            Sql += rmv.Rows[j][5].ToString() + " = '" + rmv.Rows[j][4].ToString() + "', ";
                            for (int k = j + 1; k < rowCount; k++)
                            {
                                if (rmv.Rows[k][0].ToString().Equals(cellValue))
                                {
                                    if ("核查块段编号".Equals(rmv.Rows[k][0].ToString()))
                                    {
                                        HCKDBH = rmv.Rows[k][2].ToString();
                                        newHCKDBH = rmv.Rows[j][4].ToString();
                                    }
                                    Sql += rmv.Rows[k][5].ToString() + " = '" + rmv.Rows[k][4].ToString() + "', ";
                                    count++;
                                }

                            }

                            string updateSQL = "update " + cellValue.Split('-')[0] + " set " + Sql.Substring(0, Sql.Length - 2) + " where KDBH ='" + newHCKDBH + "' and KCMC = '" + cellValue.Split('-')[1] + "'";
                            cmd = new OleDbCommand(updateSQL, Conn);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            Sql = "";
                            j += count;
                        }
                    }

                    //首先将核查块段储量的相关块段编号更新
                    String KDBH_SQL = "update JGAB309_核查块段储量 set KDBH ='" + newHCKDBH + "' where KDBH ='" + HCKDBH + "'";
                    cmd = new OleDbCommand(KDBH_SQL, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    KTBH = HCKDBH.Split('-')[0];
                    newKTBH = newHCKDBH.Split('-')[0];

                }

                //更新矿体编号
                String KT_SQL = "update JGAB306_矿体 set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
                cmd = new OleDbCommand(KT_SQL, Conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //继续更新其他没有级联更新关系的表
                string JGAB319 = "update JGAB319_大块段对照表 set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
                cmd = new OleDbCommand(JGAB319, Conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                string JGAB312 = "update JGAB312_块段对照表 set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
                cmd = new OleDbCommand(JGAB312, Conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conn.Close();

            }
        }

        //
        private void UpdateAccessTableDlg_Shown(object sender, EventArgs e)
        {
            try
            {
                //遍历所有的DataGridView
                for (int i = 0; i < dgvList.Count; i++)
                {
                    dgvList[i].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //不能让每行数据按列自动排序
                    for (int j = 0; j < dgvList[i].Columns.Count; j++)
                    {
                        dgvList[i].Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }

}