using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using WorkSpace;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
namespace MapGIS2017Ultimate
{
    public partial class AccessUpdateDlg : DevComponents.DotNetBar.Office2007Form
    {
        public AccessUpdateDlg(Dictionary<mcDot, Dictionary<string, string>> SourceAnnoDictionSet, Dictionary<mcDot, Dictionary<string, string>> NewAnnoDictionSet)
        {
            InitializeComponent();
            this.SourceAnnoDictionSet = SourceAnnoDictionSet;
            this.NewAnnoDictionSet = NewAnnoDictionSet;
        }

        DataTable dtData = null; //需要更新字段的表
        private void AccessUpdateDlg_Load(object sender, EventArgs e)
        {
            BindNewAttr(newAttr);
            //初始化需要更新 的表
            dtData = new DataTable();
            initDataTable();
            newAnnoCmb.Visible = false;
            //绑定下拉列表的事件
            newAnnoCmb.SelectedIndexChanged += new EventHandler(newAnnoCmb_SelectedIndexChanged);

            this.dgvFieldsUpdate.Controls.Add(newAnnoCmb);
        }


        public List<string> newAttr = null;
        private ComboBox newAnnoCmb = new ComboBox();
        OleDbConnection Conn = null;
        OleDbCommand comm = null;
        OleDbDataReader adr = null;
        //导入Access并且加载Access所有的表
        private void btnImportAccess_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.mdb)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtBxAccessPath.Text = fileDialog.FileName;

            }

            string AccessPath = this.txtBxAccessPath.Text;
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source=" + AccessPath;
            Conn = new OleDbConnection(ConnectionString);
            try
            {
                Conn.Open();
                DataTable shemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                node1.Text = fileDialog.SafeFileName;
                foreach (DataRow dr in shemaTable.Rows)
                {
                    Regex reg = new Regex("^KCL");
                    if (!reg.IsMatch(dr["TABLE_NAME"].ToString()))
                    {
                        DevComponents.AdvTree.Node node2 = new DevComponents.AdvTree.Node();
                        node2.Text = dr["TABLE_NAME"].ToString();
                        node1.Nodes.Add(node2);
                        //将表名添加到表名列表中
                        this.cbxTableName.Items.Add(dr["TABLE_NAME"].ToString());
                        //每个表的所有列名
                        DataTable columnTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, dr["TABLE_NAME"].ToString(), null });
                        foreach (DataRow dr2 in columnTable.Rows)
                        {
                            DevComponents.AdvTree.Node node3 = new DevComponents.AdvTree.Node();
                            node3.Text = dr2["DESCRIPTION"].ToString();
                            node3.Tag = dr2["COLUMN_NAME"].ToString();
                            node2.Nodes.Add(node3);
                        }
                    }

                }

                //列举所有的矿体
                string SQL = "select KTBH from JGAB306_矿体";
                comm = new OleDbCommand(SQL, Conn);                
                adr = comm.ExecuteReader();
                while (adr.Read())
                {
                    this.cbxKTBH.Items.Add(adr["KTBH"]);
                }

                adr.Close();
                comm.Dispose();

                //列举所有矿产名称
                SQL = "select KCMC from JGAB301_核查矿区";
                comm = new OleDbCommand(SQL, Conn);
                adr = comm.ExecuteReader();
                while (adr.Read())
                {
                    string[] str = adr["KCMC"].ToString().Split(',');
                    for (int i = 0; i < str.Length;i++ )
                    {
                        this.cbxKCName.Items.Add(str[i]);
                    }
                }

                adr.Close();
                comm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Conn.Close();
            }
        }

        /// <summary>
        /// 绑定新注记下拉列表框
        /// </summary>
        Dictionary<int, string> NewValue = new Dictionary<int, string>();
        private void BindNewAttr(List<string> Attr)
        {
            DataTable dtAnno = new DataTable();
            dtAnno.Columns.Add("ID");
            dtAnno.Columns.Add("Anno");
            DataRow drSex;
            for (int i = 0; i < Attr.Count; i++)
            {
                string[] s = Attr[i].Split('、');
                drSex = dtAnno.NewRow();
                NewValue.Add(int.Parse(s[0]), s[1]);
                drSex[0] = s[0];
                drSex[1] = Attr[i];
                dtAnno.Rows.Add(drSex);
            }
            newAnnoCmb.ValueMember = "ID";
            newAnnoCmb.DisplayMember = "Anno";
            newAnnoCmb.DataSource = dtAnno;
            newAnnoCmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //手动添加需要更新的字段


        private void initDataTable()
        {
            dtData.Columns.Add("Fields");
            dtData.Columns.Add("NewValue");
        }

        //双击树节点添加需要更新的字段
        private void advTreeTableList_NodeDoubleClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            try
            {
                DataRow drData = dtData.NewRow();
                drData[0] = e.Node.Tag.ToString();
                drData[1] = newAttr[(int)Math.Round((double)(newAttr.Count - 1))];
                dtData.Rows.Add(drData);
                this.dgvFieldsUpdate.DataSource = dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //判断是否是数字函数
        private bool isInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }


        //显示下拉列表
        private void dgvFieldsUpdate_CurrentCellChanged(object sender, EventArgs e)
        {
            //如果是要显示下拉列表的列的话
            try
            {
                if (this.dgvFieldsUpdate.CurrentCell != null && this.dgvFieldsUpdate.CurrentCell.RowIndex != this.dgvFieldsUpdate.Rows.Count - 1 && this.dgvFieldsUpdate.CurrentCell.ColumnIndex == 1)
                {
                    Rectangle rect = dgvFieldsUpdate.GetCellDisplayRectangle(dgvFieldsUpdate.CurrentCell.ColumnIndex, dgvFieldsUpdate.CurrentCell.RowIndex, false);
                    string sexValue = dgvFieldsUpdate.CurrentCell.Value.ToString();
                    string Value = null;
                    if (isInt(sexValue))
                    {
                        NewValue.TryGetValue(int.Parse(sexValue), out Value);
                    }
                    else
                    {
                        Value = sexValue;
                    }

                    newAnnoCmb.Text = Value;
                    newAnnoCmb.Left = rect.Left;
                    newAnnoCmb.Top = rect.Top;
                    newAnnoCmb.Width = rect.Width;
                    newAnnoCmb.Height = rect.Height;
                    newAnnoCmb.Visible = true;
                }
                else
                {
                    newAnnoCmb.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //
        private void newAnnoCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

            dgvFieldsUpdate.CurrentCell.Value = ((ComboBox)sender).Text;
            foreach (KeyValuePair<int, string> kvp in NewValue)
            {
                if (kvp.Value.Equals(((ComboBox)sender).Text))
                {
                    dgvFieldsUpdate.CurrentCell.Tag = kvp.Key;
                    break;
                }
            }
        }

        //设置下拉列表不可见
        private void dgvFieldsUpdate_Scroll(object sender, ScrollEventArgs e)
        {
            newAnnoCmb.Visible = false;
        }

        private void dgvFieldsUpdate_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            newAnnoCmb.Visible = false;
        }

        //如果选择的表名变化，将待更新列表清空
        private void cbxTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFieldsUpdate.Rows.Count > 1)
                {
                    dtData.Rows.Clear();
                    this.dgvFieldsUpdate.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //更新矿体编号
        private void btnUpdateKTBH_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    String SQL = "update JGAB306_矿体 set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(SQL, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    //继续更新其他没有级联更新关系的表
                    string JGAB319 = "update JGAB319_大块段对照表 set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    cmd = new OleDbCommand(JGAB319, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    string JGAB312 = "update JGAB312_块段对照表 set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    cmd = new OleDbCommand(JGAB312, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("矿体编号更新完成！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //更新块段的数据
        Dictionary<mcDot, Dictionary<string, string>> SourceAnnoDictionSet = null;
        Dictionary<mcDot, Dictionary<string, string>> NewAnnoDictionSet = null;
        private void btnUpdateKD_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    foreach (KeyValuePair<mcDot, Dictionary<string, string>> sourceKVP in SourceAnnoDictionSet)
                    {
                        mcDot sourceDot = sourceKVP.Key;
                        double minDis = double.MaxValue;
                        Dictionary<string, string> newCircleAnnoDic = null;//与原始数据圆最近的新数据的圆
                        //遍历新数据集合，找到与原始数据圆最近的点
                        mcDot rowDot = null;
                        foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                        {
                            mcDot newDot = newKVP.Key;
                            //数据截取，去除y值的38000000
                            string valueStr = newDot.x.ToString().Substring(2);
                            //处理过后，去掉带号的坐标
                            mcDot exceptPreDot = new mcDot();
                            exceptPreDot.x = double.Parse(valueStr);
                            exceptPreDot.y = newDot.y;
                            double distance = MapGIsK9Utils.CalDistanceOfCircle(sourceDot, exceptPreDot);
                            if (minDis > distance)
                            {
                                minDis = distance;
                                newCircleAnnoDic = newKVP.Value;
                                rowDot = newDot;
                            }
                        }
                        //找到原始块段编号
                        Dictionary<string,string> sourceValueDic = sourceKVP.Value;                        
                        string KDBH = "";
                        string newKDBH = "";
                        sourceValueDic.TryGetValue("1", out KDBH);
                        newCircleAnnoDic.TryGetValue("1", out newKDBH);
                        //从DataGridView中读取需要更新的字段
                        string sqlStr = "";
                        for (int i = 0; i < dgvFieldsUpdate.Rows.Count - 1;i++ )
                        {
                            if (i < dgvFieldsUpdate.Rows.Count - 2)
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('、')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "',";
                            }
                            else {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('、')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "'";
                            }

                        }
                        //更新核查块段
                        string JGAB308SQL = "update " + this.cbxTableName.Text + " set " + sqlStr + " where KDBH = '" + KDBH + "'";
                        OleDbCommand cmd = new OleDbCommand(JGAB308SQL, Conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        //更新核查块段储量表的块段编号
                        string JGAB309SQL = "update JGAB309_核查块段储量 set KDBH = '" + newKDBH + "'  where KDBH = '" + KDBH + "'";
                        cmd = new OleDbCommand(JGAB309SQL, Conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //更新块段储量
        private void btnUpdateKDCL_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    foreach (KeyValuePair<mcDot, Dictionary<string, string>> sourceKVP in SourceAnnoDictionSet)
                    {
                        mcDot sourceDot = sourceKVP.Key;
                        double minDis = double.MaxValue;
                        Dictionary<string, string> newCircleAnnoDic = null;//与原始数据圆最近的新数据的圆
                        foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                        {
                            mcDot newDot = newKVP.Key;
                            //数据截取，去除y值的38000000
                            string valueStr = newDot.x.ToString().Substring(2);
                            //处理过后，去掉带号的坐标
                            mcDot exceptPreDot = new mcDot();
                            exceptPreDot.x = double.Parse(valueStr);
                            exceptPreDot.y = newDot.y;
                            double distance = MapGIsK9Utils.CalDistanceOfCircle(sourceDot, exceptPreDot);
                            if (minDis > distance)
                            {
                                minDis = distance;
                                newCircleAnnoDic = newKVP.Value;
                            }
                        }
                        //找到原始块段编号
                        Dictionary<string, string> sourceValueDic = sourceKVP.Value;
                        string KDBH = "";
                        string newKDBH = "";
                        sourceValueDic.TryGetValue("1", out KDBH);
                        newCircleAnnoDic.TryGetValue("1", out newKDBH);
                        //从DataGridView中读取需要更新的字段
                        string sqlStr = "";
                        for (int i = 0; i < dgvFieldsUpdate.Rows.Count - 1; i++)
                        {
                            if (i < dgvFieldsUpdate.Rows.Count - 2)
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('、')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "',";
                            }
                            else
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('、')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "'";
                            }

                        }
                        //更新核查块段
                        string JGAB309SQL = "update " + this.cbxTableName.SelectedItem.ToString() + " set " + sqlStr + " where KDBH = '" + newKDBH +
                            "' and  KCMC = '" + this.cbxKCName.SelectedItem.ToString() + "矿'";
                        OleDbCommand cmd = new OleDbCommand(JGAB309SQL, Conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //重置数据
        private void btnResetDatagridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFieldsUpdate.Rows.Count > 1)
                {
                    dtData.Rows.Clear();
                    this.dgvFieldsUpdate.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReSetKT_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFieldsUpdate.Rows.Count > 1)
                {
                    dtData.Rows.Clear();
                    this.dgvFieldsUpdate.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReSetKDCL_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFieldsUpdate.Rows.Count > 1)
                {
                    dtData.Rows.Clear();
                    this.dgvFieldsUpdate.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //窗体关闭，释放数据库资源
        private void AccessUpdateDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            try
            {
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }













    }
}