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

        DataTable dtData = null; //��Ҫ�����ֶεı�
        private void AccessUpdateDlg_Load(object sender, EventArgs e)
        {
            BindNewAttr(newAttr);
            //��ʼ����Ҫ���� �ı�
            dtData = new DataTable();
            initDataTable();
            newAnnoCmb.Visible = false;
            //�������б���¼�
            newAnnoCmb.SelectedIndexChanged += new EventHandler(newAnnoCmb_SelectedIndexChanged);

            this.dgvFieldsUpdate.Controls.Add(newAnnoCmb);
        }


        public List<string> newAttr = null;
        private ComboBox newAnnoCmb = new ComboBox();
        OleDbConnection Conn = null;
        OleDbCommand comm = null;
        OleDbDataReader adr = null;
        //����Access���Ҽ���Access���еı�
        private void btnImportAccess_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "��ѡ���ļ�";
            fileDialog.Filter = "�����ļ�(*.mdb)|*.*";
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
                        //��������ӵ������б���
                        this.cbxTableName.Items.Add(dr["TABLE_NAME"].ToString());
                        //ÿ�������������
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

                //�о����еĿ���
                string SQL = "select KTBH from JGAB306_����";
                comm = new OleDbCommand(SQL, Conn);                
                adr = comm.ExecuteReader();
                while (adr.Read())
                {
                    this.cbxKTBH.Items.Add(adr["KTBH"]);
                }

                adr.Close();
                comm.Dispose();

                //�о����п������
                SQL = "select KCMC from JGAB301_�˲����";
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
        /// ����ע�������б��
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
                string[] s = Attr[i].Split('��');
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

        //�ֶ������Ҫ���µ��ֶ�


        private void initDataTable()
        {
            dtData.Columns.Add("Fields");
            dtData.Columns.Add("NewValue");
        }

        //˫�����ڵ������Ҫ���µ��ֶ�
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

        //�ж��Ƿ������ֺ���
        private bool isInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }


        //��ʾ�����б�
        private void dgvFieldsUpdate_CurrentCellChanged(object sender, EventArgs e)
        {
            //�����Ҫ��ʾ�����б���еĻ�
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

        //���������б��ɼ�
        private void dgvFieldsUpdate_Scroll(object sender, ScrollEventArgs e)
        {
            newAnnoCmb.Visible = false;
        }

        private void dgvFieldsUpdate_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            newAnnoCmb.Visible = false;
        }

        //���ѡ��ı����仯�����������б����
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


        //���¿�����
        private void btnUpdateKTBH_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    String SQL = "update JGAB306_���� set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(SQL, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    //������������û�м������¹�ϵ�ı�
                    string JGAB319 = "update JGAB319_���ζ��ձ� set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    cmd = new OleDbCommand(JGAB319, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    string JGAB312 = "update JGAB312_��ζ��ձ� set KTBH ='" + this.txtNewKTBH.Text + "' where KTBH ='" + this.cbxKTBH.Text + "'";
                    cmd = new OleDbCommand(JGAB312, Conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("�����Ÿ�����ɣ�");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //���¿�ε�����
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
                        Dictionary<string, string> newCircleAnnoDic = null;//��ԭʼ����Բ����������ݵ�Բ
                        //���������ݼ��ϣ��ҵ���ԭʼ����Բ����ĵ�
                        mcDot rowDot = null;
                        foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                        {
                            mcDot newDot = newKVP.Key;
                            //���ݽ�ȡ��ȥ��yֵ��38000000
                            string valueStr = newDot.x.ToString().Substring(2);
                            //�������ȥ�����ŵ�����
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
                        //�ҵ�ԭʼ��α��
                        Dictionary<string,string> sourceValueDic = sourceKVP.Value;                        
                        string KDBH = "";
                        string newKDBH = "";
                        sourceValueDic.TryGetValue("1", out KDBH);
                        newCircleAnnoDic.TryGetValue("1", out newKDBH);
                        //��DataGridView�ж�ȡ��Ҫ���µ��ֶ�
                        string sqlStr = "";
                        for (int i = 0; i < dgvFieldsUpdate.Rows.Count - 1;i++ )
                        {
                            if (i < dgvFieldsUpdate.Rows.Count - 2)
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('��')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "',";
                            }
                            else {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('��')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "'";
                            }

                        }
                        //���º˲���
                        string JGAB308SQL = "update " + this.cbxTableName.Text + " set " + sqlStr + " where KDBH = '" + KDBH + "'";
                        OleDbCommand cmd = new OleDbCommand(JGAB308SQL, Conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        //���º˲��δ�����Ŀ�α��
                        string JGAB309SQL = "update JGAB309_�˲��δ��� set KDBH = '" + newKDBH + "'  where KDBH = '" + KDBH + "'";
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


        //���¿�δ���
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
                        Dictionary<string, string> newCircleAnnoDic = null;//��ԭʼ����Բ����������ݵ�Բ
                        foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                        {
                            mcDot newDot = newKVP.Key;
                            //���ݽ�ȡ��ȥ��yֵ��38000000
                            string valueStr = newDot.x.ToString().Substring(2);
                            //�������ȥ�����ŵ�����
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
                        //�ҵ�ԭʼ��α��
                        Dictionary<string, string> sourceValueDic = sourceKVP.Value;
                        string KDBH = "";
                        string newKDBH = "";
                        sourceValueDic.TryGetValue("1", out KDBH);
                        newCircleAnnoDic.TryGetValue("1", out newKDBH);
                        //��DataGridView�ж�ȡ��Ҫ���µ��ֶ�
                        string sqlStr = "";
                        for (int i = 0; i < dgvFieldsUpdate.Rows.Count - 1; i++)
                        {
                            if (i < dgvFieldsUpdate.Rows.Count - 2)
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('��')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "',";
                            }
                            else
                            {
                                string id = dgvFieldsUpdate.Rows[i].Cells[1].Value.ToString().Split('��')[0];
                                string value = "";
                                newCircleAnnoDic.TryGetValue(id, out value);
                                sqlStr += dgvFieldsUpdate.Rows[i].Cells[0].Value.ToString() + " = '" + value + "'";
                            }

                        }
                        //���º˲���
                        string JGAB309SQL = "update " + this.cbxTableName.SelectedItem.ToString() + " set " + sqlStr + " where KDBH = '" + newKDBH +
                            "' and  KCMC = '" + this.cbxKCName.SelectedItem.ToString() + "��'";
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


        //��������
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
        //����رգ��ͷ����ݿ���Դ
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