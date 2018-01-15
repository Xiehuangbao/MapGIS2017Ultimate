using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
using WorkSpace;
namespace MapGIS2005
{
    public partial class AttrMappingDlg : DevComponents.DotNetBar.Office2007Form
    {
        public AttrMappingDlg()
        {
            InitializeComponent();
        }

        mcRecordSet sourceRecordSet = null;
        mcRecordSet newRecordSet = null;
        public List<string> sourceAttr = null;
        public List<string> newAttr = null;

        private ComboBox cmb_Temp = new ComboBox();
        private ComboBox newAnnoCmb = new ComboBox();
        private void AttrMappingDlg_Load(object sender, EventArgs e)
        {
            // 绑定注记下拉列表框
            BindSourceAttr(sourceAttr);
            BindNewAttr(newAttr);

            //绑定数据表
            BindData(sourceAttr,newAttr);

            // 设置下拉列表框不可见
            cmb_Temp.Visible = false;
            newAnnoCmb.Visible = false;
            // 添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            newAnnoCmb.SelectedIndexChanged += new EventHandler(newAnnoCmb_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dataGridView1.Controls.Add(cmb_Temp);
            this.dataGridView1.Controls.Add(newAnnoCmb);
            dataGridView1.Columns[0].Width = 160;
            dataGridView1.Columns[1].Width = 160;
        }

        /// <summary>
        /// 绑定原始注记下拉列表框
        /// </summary>
        Dictionary<int, string> sourceValue = new Dictionary<int, string>();
        private void BindSourceAttr(List<string> Attr)
        {
            DataTable dtAnno = new DataTable();
            dtAnno.Columns.Add("ID");
            dtAnno.Columns.Add("Anno");
            DataRow drSex;
            for (int i = 0; i < Attr.Count; i++)
            {
                string[] s = Attr[i].Split('、');
                drSex = dtAnno.NewRow();
                sourceValue.Add(int.Parse(s[0]),s[1]);
                drSex[0] = s[0];
                drSex[1] = Attr[i];
                dtAnno.Rows.Add(drSex);
            }
            cmb_Temp.ValueMember = "ID";
            cmb_Temp.DisplayMember = "Anno";
            cmb_Temp.DataSource = dtAnno;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
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
                NewValue.Add(int.Parse(s[0]),s[1]);
                drSex[0] = s[0];
                drSex[1] = Attr[i];
                dtAnno.Rows.Add(drSex);
            }
            newAnnoCmb.ValueMember = "ID";
            newAnnoCmb.DisplayMember = "Anno";
            newAnnoCmb.DataSource = dtAnno;
            newAnnoCmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BindData(List<string> SourceAttr,List<string> newAttr)
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("原始注记项");
            dtData.Columns.Add("新注记项");
            int index = 0;
            if(sourceAttr.Count > newAttr.Count)
                index = sourceAttr.Count;
            else
                index = newAttr.Count;

            DataRow drData;
            for (int i = 0; i < index;i++ )
            {
                string source = "";
                string newAtt = "";
                if(i < sourceAttr.Count)
                    source = sourceAttr[i];
                else
                    source = "";

                if (i < newAttr.Count)
                    newAtt = newAttr[i];
                else
                    newAtt = "";
                drData = dtData.NewRow();
                drData[0] = source;
                drData[1] = newAtt;
                dtData.Rows.Add(drData);
            }
            this.dataGridView1.DataSource = dtData;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
                    string Value = null;
                    if (isInt(sexValue))
                    {
                        sourceValue.TryGetValue(int.Parse(sexValue), out Value);
                    }
                    else
                    {
                        Value = sexValue;
                    }
                    
                    cmb_Temp.Text = Value;
                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;
                }
                else if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    string sexValue = dataGridView1.CurrentCell.Value.ToString();
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
                    cmb_Temp.Visible = false;
                    newAnnoCmb.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
            foreach(KeyValuePair<int,string> kvp in sourceValue)
            {
                if(kvp.Value.Equals(((ComboBox)sender).Text))
                {
                    dataGridView1.CurrentCell.Tag = kvp.Key;
                    break;
                }
            }
        }

        private void newAnnoCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).Text;
            foreach (KeyValuePair<int, string> kvp in NewValue)
            {
                if (kvp.Value.Equals(((ComboBox)sender).Text))
                {
                    dataGridView1.CurrentCell.Tag = kvp.Key;
                    break;
                }
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            this.newAnnoCmb.Visible = false;
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            this.newAnnoCmb.Visible = false;
        }

        private bool isInt(string value) {
            return Regex.IsMatch(value,@"^[+-]?\d*$");
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[0].ColumnIndex == 0)
                {
                    dataGridView1.Rows[i].Cells[0].Tag = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (isInt(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        string Value = null;
                        sourceValue.TryGetValue(int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()), out Value);
                        dataGridView1.Rows[i].Cells[0].Value = Value;
                    }
                }
                if (dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[1].ColumnIndex == 1)
                {
                    dataGridView1.Rows[i].Cells[1].Tag = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (isInt(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                    {
                        string Value = null;
                        sourceValue.TryGetValue(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()), out Value);
                        dataGridView1.Rows[i].Cells[1].Value = Value;
                    }
                }
            }
        }
        
        //开始更新注记信息
        public Dictionary<string, string> tableRealation = null;
        private void btnUpdateAttr_Click(object sender, EventArgs e)
        {
            tableRealation = new Dictionary<string, string>();
            int row = dataGridView1.Rows.Count;
            int cell = dataGridView1.Rows[0].Cells.Count;
            try
            {
                for (int i = 0; i < row; i++)
                {
                    string source = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string newA = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    tableRealation.Add(source.Split('、')[0], newA.Split('、')[0]);
                }
            }
            catch
            {
                MessageBox.Show("请检查对应关系是否有错");
            }
        }

        //校验对应关系
        private void btnCheckRelation_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sourceRealation = new Dictionary<string, string>();
            Dictionary<string, string> newRealation = new Dictionary<string, string>();
            int row = dataGridView1.Rows.Count - 1;
            int cell = dataGridView1.Rows[0].Cells.Count;
            try
            {
                for (int i = 0; i < row; i++)
                {
                    string source = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    sourceRealation.Add(source.Split('、')[0], source.Split('、')[1]);
                    string newA = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    newRealation.Add(newA.Split('、')[0], newA.Split('、')[1]);
                }
            }
            catch {
                MessageBox.Show("请检查对应关系是否有错");
            }
            MessageBox.Show("校验通过！");
        }



    }
}