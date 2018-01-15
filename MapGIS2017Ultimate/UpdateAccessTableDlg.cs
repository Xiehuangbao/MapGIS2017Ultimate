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

        //���캯���������ݴ��䵽ҳ����
        public UpdateAccessTableDlg(Dictionary<Dictionary<string, string>, Dictionary<string, string>> annoMappingDic,
            List<string> sourceAttr, List<string> newAttr,string DBPath)
        {
            InitializeComponent();
            this.annoMappingDic = annoMappingDic;
            this.sourceAttr = sourceAttr;
            this.newAttr = newAttr;
            this.DBPath = DBPath;
        }

        //ӳ���ϵ���ݿ�
        OleDbConnection MapConn = null;
        OleDbDataReader MapAdr = null;
        OleDbDataReader Map_CL_Adr = null;
        OleDbDataReader _Map_CL_Adr = null;
        //ԭʼ�������ݿ�
        OleDbConnection Conn = null;
        OleDbDataReader Adr = null;
        OleDbDataReader CL_Adr = null;
        string DBPath = "";

        List<DataGridView> dgvList = new List<DataGridView>();//��¼�����е�DataGridView���Ը������ݿ�
        List<DataTable> TableList = new List<DataTable>();
        private void UpdateAccessTableDlg_Load(object sender, EventArgs e)
        {
            //���DataGridView����
            //if (dgvListTableView.RowCount > 1)
            //{
            //    dgvListTableView.DataSource = null;
            //}
            try
            {
                //�˲��α��ֶζ�Ӧ
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
                string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
                MapConn = AccessUtils.GetConn(MapDBPath);
 
                Conn = AccessUtils.GetConn(DBPath);

                //�������ĵ����������Բ
                foreach (KeyValuePair<Dictionary<string, string>, Dictionary<string, string>> annoKVP in annoMappingDic)
                {
                    DataGridView dgvListTableView = new DataGridView();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("����");
                    dt.Columns.Add("�ֶ�");
                    dt.Columns.Add("ԭ��ֵ");
                    dt.Columns.Add("ԭͼֵ");
                    dt.Columns.Add("��ͼֵ");
                    dt.Columns.Add("�ֶ�ֵ");

                    Dictionary<string, string> sourceCircleAnnoDic = annoKVP.Key;

                    Dictionary<string, string> newCircleAnnoDic = annoKVP.Value;
   
                    //ƥ��˲��α�
                    string MapSQL = "select FieldName,LegendAnno,FieldDesc from JGAB308_�˲���";
                    MapAdr = AccessUtils.GetDataReader(MapSQL, MapConn);
                    string sourceKDBH = "";
                    while (MapAdr.Read())
                    {
                        List<string> FieldRow = new List<string>();//ƥ���У�Ҳ����˵��Ҫ���µ��ֶ�
                        //����Ӧ��ϵ���ݱ��е�ӳ������Ϊ��ͬ��������ע�Ƚ�
                        bool flag = false;
                        if (!MapAdr["LegendAnno"].ToString().Equals(""))//����ֶζ�Ӧ�в�Ϊ����
                        {

                            string[] PatternStr = MapAdr["LegendAnno"].ToString().Split(',');
                            for (int i = 0; i < PatternStr.Length; i++)
                            {
                                bool Break = false;
                                //����ͼ����ע
                                for (int j = 0; j < newAttr.Count; j++)
                                {
                                    //�ж�ͼ����ע�Ƿ��кͺ˲��ζ�Ӧ��ϵ��ƥ����ֶ�
                                    string[] lengendAnno = newAttr[j].Split('��');
                                    Regex r = new Regex(PatternStr[i]);
                                    if (r.Match(lengendAnno[1]).Success)
                                    {
                                        //����
                                        FieldRow.Add("JGAB308_�˲���");
                                        //�ֶ���
                                        FieldRow.Add(MapAdr["FieldDesc"].ToString());
                                        //��ȡԭ��ֵ

                                        sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                        string SQL = "select " + MapAdr["FieldName"].ToString() + " from JGAB308_�˲��� where KDBH ='" + sourceKDBH + "'";
                                        Adr = AccessUtils.GetDataReader(SQL, Conn);
                                        while (Adr.Read())
                                        {
                                            FieldRow.Add(Adr[MapAdr["FieldName"].ToString()].ToString());
                                        }
                                        //ԭͼֵ
                                        string sourceValue = "";
                                        sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                        FieldRow.Add(sourceValue);

                                        //��ͼֵ
                                        string newValue = "";
                                        newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                        FieldRow.Add(newValue);

                                        //�ֶ�ֵ
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
                            if (flag)//���ƥ��ɹ��������
                            {
                                dt.Rows.Add(FieldRow.ToArray());
                            }
                        }
                    }

                    //�ͷ���Դ
                    MapAdr.Close();
                    Adr.Close();

                    //ƥ��˲��δ��������ÿ�������ͬ�Ʒλ������ʲô�ĵ�����ʾ���ֿ���
                    string _Map_CL_SQL = "select FieldName,LegendAnno,FieldDesc from JGAB309_�˲��δ���";
                    _Map_CL_Adr = AccessUtils.GetDataReader(_Map_CL_SQL, MapConn);
                    while (_Map_CL_Adr.Read())
                    {
                        List<string> FieldRow = new List<string>();//ƥ���У�Ҳ����˵��Ҫ���µ��ֶ�
                        //����Ӧ��ϵ���ݱ��е�ӳ������Ϊ��ͬ��������ע�Ƚ�
                        bool flag = false;
                        //����ֶζ�Ӧ�в�Ϊ����
                        if (!_Map_CL_Adr["LegendAnno"].ToString().Equals(""))
                        {
                            //�������жϿ���������
                            string[] PatternStr = _Map_CL_Adr["LegendAnno"].ToString().Split(',');
                            for (int i = 0; i < PatternStr.Length; i++)
                            {
                                bool Break = false;
                                //����ͼ����ע
                                for (int j = 0; j < newAttr.Count; j++)
                                {
                                    //�ж�ͼ����ע�Ƿ��кͺ˲��ζ�Ӧ��ϵ��ƥ����ֶ�
                                    string[] lengendAnno = newAttr[j].Split('��');
                                    Regex r = new Regex(PatternStr[i]);
                                    if (r.Match(lengendAnno[1]).Success)
                                    {
                                        //����ֶ���PW
                                        if ("PW".Equals(_Map_CL_Adr["FieldName"].ToString()) || "HCJSL".Equals(_Map_CL_Adr["FieldName"].ToString()))
                                        {

                                        }//����Ǻ˲�������ֶ�
                                        else
                                        {
                                            //����
                                            FieldRow.Add("JGAB309_�˲��δ���");
                                            //�ֶ���
                                            FieldRow.Add(_Map_CL_Adr["FieldDesc"].ToString());
                                            //��ȡԭ��ֵ

                                            sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                            string SQL = "select " + _Map_CL_Adr["FieldName"].ToString() + " from JGAB309_�˲��δ��� where KDBH ='" + sourceKDBH + "'";
                                            CL_Adr = AccessUtils.GetDataReader(SQL, Conn);
                                            if (CL_Adr.HasRows)
                                            {
                                                while (CL_Adr.Read())
                                                {

                                                    FieldRow.Add(CL_Adr[_Map_CL_Adr["FieldName"].ToString()].ToString());
                                                    break;//ֻ��ȡ�ÿ�εĵ�һ�����ݾͿ�����
                                                }
                                            }
                                            else
                                            {
                                                FieldRow.Add(" ");
                                            }
                                            //ԭͼֵ
                                            string sourceValue = "";
                                            sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                            FieldRow.Add(sourceValue);

                                            //��ͼֵ
                                            string newValue = "";
                                            newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                            FieldRow.Add(newValue);

                                            //�ֶ�ֵ
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
                            if (flag)//���ƥ��ɹ��������
                            {
                                dt.Rows.Add(FieldRow.ToArray());
                            }
                        }
                    }
                    _Map_CL_Adr.Close();
                    //��ƥ���ʯ����,�ҵ��ñ�ע��һ���м������͵Ŀ���

                    //�ӿ�ʯ���ͱ���ƥ���ʯ����
                    string Map_KSLX_SQL = "select KCType,KCTypeMapping from �������ӳ��";
                    OleDbDataReader KSLX_Adr = null;
                    KSLX_Adr = AccessUtils.GetDataReader(Map_KSLX_SQL, MapConn);
                    while (KSLX_Adr.Read())
                    {
                        if (!KSLX_Adr["KCTypeMapping"].ToString().Equals(""))//����ֶζ�Ӧ�в�Ϊ����
                        {
                            string[] PatternType = KSLX_Adr["KCTypeMapping"].ToString().Split(',');//ƥ��ģ��
                            for (int LX_i = 0; LX_i < PatternType.Length; LX_i++)
                            {   //����ͼ����ע
                                bool LX_Break = false;
                                for (int LX_j = 0; LX_j < newAttr.Count; LX_j++)
                                {
                                    //�ж�ͼ����ע�Ƿ��кͺ˲��ζ�Ӧ��ϵ��ƥ����ֶ�
                                    string[] LX_lengendAnno = newAttr[LX_j].Split('��');
                                    Regex LX_r = new Regex(PatternType[LX_i]);

                                    //�ҵ������������Ժ�
                                    if (LX_r.Match(LX_lengendAnno[1]).Success)
                                    {
                                        //KSLX_Adr["KCType"]�������
                                        string Map_CL_SQL = "select FieldName,LegendAnno,FieldDesc from JGAB309_�˲��δ���";
                                        Map_CL_Adr = AccessUtils.GetDataReader(Map_CL_SQL, MapConn);

                                        while (Map_CL_Adr.Read())
                                        {
                                            List<string> FieldRow = new List<string>();//ƥ���У�Ҳ����˵��Ҫ���µ��ֶ�
                                            //����Ӧ��ϵ���ݱ��е�ӳ������Ϊ��ͬ��������ע�Ƚ�
                                            bool flag = false;
                                            //����ֶζ�Ӧ�в�Ϊ����
                                            if (!Map_CL_Adr["LegendAnno"].ToString().Equals(""))
                                            {
                                                //�������жϿ���������
                                                string[] PatternStr = Map_CL_Adr["LegendAnno"].ToString().Split(',');
                                                for (int i = 0; i < PatternStr.Length; i++)
                                                {
                                                    bool Break = false;
                                                    bool _flag = true;
                                                    //����ͼ����ע
                                                    for (int j = 0; j < newAttr.Count; j++)
                                                    {
                                                        //�ж�ͼ����ע�Ƿ��кͺ˲��ζ�Ӧ��ϵ��ƥ����ֶ�
                                                        string[] lengendAnno = newAttr[j].Split('��');
                                                        Regex r = new Regex(PatternStr[i]);
                                                        if (r.Match(lengendAnno[1]).Success)
                                                        {
                                                            //����ֶ���PW
                                                            if ("PW".Equals(Map_CL_Adr["FieldName"].ToString()) || "HCJSL".Equals(Map_CL_Adr["FieldName"].ToString()))
                                                            {
                                                                //���ñ�ע�Ŀ������
                                                                string type = GetKCLX(newAttr[j], MapConn);
                                                                if (type.Equals(KSLX_Adr["KCType"].ToString()))
                                                                {
                                                                    //����
                                                                    FieldRow.Add("JGAB309_�˲��δ���-" + KSLX_Adr["KCType"].ToString());
                                                                    //�ֶ���
                                                                    FieldRow.Add(Map_CL_Adr["FieldDesc"].ToString());
                                                                    //��ȡԭ��ֵ

                                                                    sourceCircleAnnoDic.TryGetValue("1", out sourceKDBH);
                                                                    string SQL = "select " + Map_CL_Adr["FieldName"].ToString() + " from JGAB309_�˲��δ��� where KDBH ='" + sourceKDBH + "' and KCMC = '" + KSLX_Adr["KCType"].ToString() + "'";
                                                                    CL_Adr = AccessUtils.GetDataReader(SQL, Conn);
                                                                    //������ݿ���û��ֵ���Կո���棬��ֹ�����ֵǰ��
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
                                                                    //ԭͼֵ
                                                                    string sourceValue = "";
                                                                    sourceCircleAnnoDic.TryGetValue(lengendAnno[0], out sourceValue);
                                                                    FieldRow.Add(sourceValue);

                                                                    //��ͼֵ
                                                                    string newValue = "";
                                                                    newCircleAnnoDic.TryGetValue(lengendAnno[0], out newValue);
                                                                    FieldRow.Add(newValue);

                                                                    //�ֶ�ֵ
                                                                    FieldRow.Add(Map_CL_Adr["FieldName"].ToString());
                                                                    Break = true;
                                                                    flag = true;
                                                                    break;
                                                                }
                                                            }//����Ǻ˲�������ֶ�

                                                        }
                                                    }
                                                    if (Break)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (flag)//���ƥ��ɹ��������
                                                {
                                                    dt.Rows.Add(FieldRow.ToArray());
                                                }
                                            }
                                        }

                                        LX_Break = true;
                                        break;//������ǰ���ֵı�����������һ�ֿ��
                                    }

                                    //�ҵ������������Ժ�
                                }
                                //������ǰ���֣�ѡ����һ�ֿ�
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



                    //��̬���Tab��ʾ��ͬ��ε�����
                    DevComponents.DotNetBar.SuperTabControlPanel panel = new DevComponents.DotNetBar.SuperTabControlPanel();
                    DevComponents.DotNetBar.SuperTabItem tabItem = this.superTabControl1.CreateTab(sourceKDBH + "���");
                    tabItem.AttachedControl = panel;
                    panel.Controls.Add(dgvListTableView);
                    dgvListTableView.Dock = DockStyle.Fill;
                    this.superTabControl1.Controls.Add(panel);
                    //dgvList.Add(dgvListTableView);
                }
                //Ĭ����tabcontrolѡ�����һ��
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

        //�ӻ�ÿ����Ԫ�񣬺ϲ���Ԫ��
        private void dgv_CellPaint(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                DataGridView dgvListTableView = (DataGridView)sender;
                
                dgvListTableView.Columns[5].Visible = false;
                // �Ե�1����ͬ��Ԫ����кϲ�
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
                            // �����Ԫ��
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // �� Grid ���ߣ�������Ԫ��ĵױ��ߺ��ұ��ߣ�
                            //   �����һ�к͵�ǰ�е����ݲ�ͬ�����ڵ�ǰ�ĵ�Ԫ��һ���ױ���
                            if ((e.RowIndex < dgvListTableView.Rows.Count - 1) && (Convert.ToString(dgvListTableView.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value) != e.Value.ToString()))
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            }
                            //�����һ����
                            if ((e.RowIndex == dgvListTableView.Rows.Count - 1))
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom - 1);
                            }
                            // ���ұ���
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                e.CellBounds.Top, e.CellBounds.Right - 1,
                                e.CellBounds.Bottom);


                            // ������д����Ԫ�����ݣ���ͬ�����ݵĵ�Ԫ��ֻ��д��һ��
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

        //����ͼ����˵������ȡ�������
        public string GetKCLX(string Anno, OleDbConnection MapConn)
        {
            string Type = "";
            //�ӿ�ʯ���ͱ���ƥ���ʯ����
            string Map_KSLX_SQL = "select KCType,KCTypeMapping from �������ӳ��";
            OleDbDataReader KSLX_Adr = null;
            KSLX_Adr = AccessUtils.GetDataReader(Map_KSLX_SQL, MapConn);
            while (KSLX_Adr.Read())
            {
                if (!KSLX_Adr["KCTypeMapping"].ToString().Equals(""))//����ֶζ�Ӧ�в�Ϊ����
                {
                    //ƥ��ģ��
                    string[] PatternStr = KSLX_Adr["KCTypeMapping"].ToString().Split(',');
                    for (int i = 0; i < PatternStr.Length; i++)
                    {
                        //�ж�ͼ����ע�Ƿ��кͺ˲��ζ�Ӧ��ϵ��ƥ����ֶ�
                        string[] lengendAnno = Anno.Split('��');
                        Regex r = new Regex(PatternStr[i]);
                        //�ҵ������������Ժ�
                        if (r.Match(lengendAnno[1]).Success)
                        {
                            //ƥ�䵽��������Ժ����������д�������˵��ƥ�䵽Ǧ����ô����ֻ����Ǧ��
                            //KSLX_Adr["KCType"]�������
                            Type = KSLX_Adr["KCType"].ToString();
                        }
                    }
                }
            }
            KSLX_Adr.Close();
            return Type;
        }

        //ȷ������󣬿��Ը������
        private void btnBeginUpdateAccess_Click(object sender, EventArgs e)
        {
           

            //�������ݿ�
            OleDbConnection Conn = null;
            OleDbCommand cmd = null;
            string KTBH = "";
            string newKTBH = "";
            string DBPath = @"C:\����ɽ��\��-S430424003_����ɽǦп��˲���-Ǧ��\ACCESS���ݿ�\S430424003_����ɽ��Ǧпөʯ��.mdb";
            try
            {
                Conn = AccessUtils.GetConn(DBPath);
                //�������е�DataGridView
                for (int i = 0; i < TableList.Count; i++)
                {
                    DataTable rmv = TableList[i];
                    int rowCount = rmv.Rows.Count;
                    string Sql = "";//ƴ�Ӹ����ַ���
                    string HCKDBH = "";
                    string newHCKDBH = "";
                    for (int j = 0; j < rowCount; j++)
                    {
                        string cellValue = rmv.Rows[j][0].ToString();
                        
                        if (cellValue.Split('-').Length == 1)//�������Ϊ1�Ļ���˵����һ��ֻ�Ǳ���
                        {

                            int count = 0;//��¼һ���ж���
                            if ("�˲��α��".Equals(rmv.Rows[j][1].ToString()))
                            {
                                HCKDBH = rmv.Rows[j][2].ToString();
                                newHCKDBH = rmv.Rows[j][4].ToString();
                            }
                            Sql += rmv.Rows[j][5].ToString() + " = '" + rmv.Rows[j][4].ToString() + "', ";
                            for (int k = j + 1; k < rowCount; k++)
                            {
                                if (rmv.Rows[k][0].ToString().Equals(cellValue))
                                {
                                    if ("�˲��α��".Equals(rmv.Rows[k][0].ToString()))
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
                        else//�������> 2 ˵�����������ǿ���
                        {
                            int count = 0;
                            if ("�˲��α��".Equals(rmv.Rows[j][1].ToString()))
                            {
                                HCKDBH = rmv.Rows[j][2].ToString();
                                newHCKDBH = rmv.Rows[j][4].ToString();
                            }
                            Sql += rmv.Rows[j][5].ToString() + " = '" + rmv.Rows[j][4].ToString() + "', ";
                            for (int k = j + 1; k < rowCount; k++)
                            {
                                if (rmv.Rows[k][0].ToString().Equals(cellValue))
                                {
                                    if ("�˲��α��".Equals(rmv.Rows[k][0].ToString()))
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

                    //���Ƚ��˲��δ�������ؿ�α�Ÿ���
                    String KDBH_SQL = "update JGAB309_�˲��δ��� set KDBH ='" + newHCKDBH + "' where KDBH ='" + HCKDBH + "'";
                    cmd = new OleDbCommand(KDBH_SQL, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    KTBH = HCKDBH.Split('-')[0];
                    newKTBH = newHCKDBH.Split('-')[0];

                }

                //���¿�����
                String KT_SQL = "update JGAB306_���� set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
                cmd = new OleDbCommand(KT_SQL, Conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //������������û�м������¹�ϵ�ı�
                string JGAB319 = "update JGAB319_���ζ��ձ� set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
                cmd = new OleDbCommand(JGAB319, Conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                string JGAB312 = "update JGAB312_��ζ��ձ� set KTBH ='" + newKTBH + "' where KTBH ='" + KTBH + "'";
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
                //�������е�DataGridView
                for (int i = 0; i < dgvList.Count; i++)
                {
                    dgvList[i].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //������ÿ�����ݰ����Զ�����
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