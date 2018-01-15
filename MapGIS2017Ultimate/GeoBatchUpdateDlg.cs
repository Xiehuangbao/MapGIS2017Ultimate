using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MapGIS2017Ultimate
{
    public partial class GeoBatchUpdateDlg : DevComponents.DotNetBar.Office2007Form
    {
        MainForm mf = new MainForm();
        public GeoBatchUpdateDlg(MainForm mf)
        {
            InitializeComponent();
            this.mf = mf;
        }


        String sProjPath = "";
        String nProjPath = "";
        private ComboBox cmb_Temp = new ComboBox();
        private ComboBox newAnnoCmb = new ComboBox();
        List<String> sProj = null;
        List<String> nProj = null;
        //导入原始工程
        private void btnImportSProj_Click(object sender, EventArgs e)
        {
            sProj = new List<string>();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择原始MPJ所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                DirectoryInfo folder = new DirectoryInfo(dialog.SelectedPath);
                foreach(FileInfo file in folder.GetFiles("*.MPJ"))
                {
                    sProj.Add(file.Name);
                }
                
            }
            sProjPath = dialog.SelectedPath;
            BindSourceAttr(sProj);
        }

        //导入新工程
        private void btnImportNProj_Click(object sender, EventArgs e)
        {
            nProj = new List<string>();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择新工程所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                string[] folders = Directory.GetDirectories(dialog.SelectedPath,"T01_*");
                for (int i = 0; i < folders.Length; i ++ )
                {
                    string[] str = folders[i].Split('\\');
                    nProj.Add(str[str.Length - 1] + ".MPJ");
                }

            }
            nProjPath = dialog.SelectedPath;
            BindNewAttr(nProj);
        }

        //批量更新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> projTable = new Dictionary<string, string>();
            try
            {
                for (int i = 0; i < dgvONProj.Rows.Count; i++)
                {
                    if (dgvONProj.Rows[i].Cells[0].Value != null && !"".Equals(dgvONProj.Rows[i].Cells[0].Value.ToString()))
                    {
                        projTable.Add(dgvONProj.Rows[i].Cells[0].Value.ToString(), dgvONProj.Rows[i].Cells[1].Value.ToString());
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mf.projTable = projTable;
            mf.sProjPath = sProjPath;
            mf.nProjPath = nProjPath;
        }

        /// <summary>
        /// 绑定原始注记下拉列表框
        /// </summary>
        private void BindSourceAttr(List<string> Attr)
        {
            DataTable dtAnno = new DataTable();
            dtAnno.Columns.Add("Anno");
            DataRow drSex;
            for (int i = 0; i < Attr.Count; i++)
            {
                drSex = dtAnno.NewRow();
                drSex[0] = Attr[i];
                dtAnno.Rows.Add(drSex);
            }
            cmb_Temp.DisplayMember = "Anno";
            cmb_Temp.DataSource = dtAnno;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 绑定新注记下拉列表框
        /// </summary>
        private void BindNewAttr(List<string> Attr)
        {
            DataTable dtAnno = new DataTable();
            dtAnno.Columns.Add("Anno");
            DataRow drSex;
            for (int i = 0; i < Attr.Count; i++)
            {
                drSex = dtAnno.NewRow();
                drSex[0] = Attr[i];
                dtAnno.Rows.Add(drSex);
            }
            newAnnoCmb.DisplayMember = "Anno";
            newAnnoCmb.DataSource = dtAnno;
            newAnnoCmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BindData(List<string> sourceAttr, List<string> newAttr)
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("原始工程");
            
            dtData.Columns.Add("新工程");
            int index = 0;
            if (sourceAttr.Count > newAttr.Count)
                index = sourceAttr.Count;
            else
                index = newAttr.Count;

            DataRow drData;
            for (int i = 0; i < index; i++)
            {
                string source = "";
                string newAtt = "";
                if (i < sourceAttr.Count)
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
            this.dgvONProj.DataSource = dtData;
        }

        //将新旧工程加载到映射表中
        private void btnProjMapping_Click(object sender, EventArgs e)
        {
            BindData(sProj, nProj);

            // 设置下拉列表框不可见
            cmb_Temp.Visible = false;
            newAnnoCmb.Visible = false;
            // 添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            newAnnoCmb.SelectedIndexChanged += new EventHandler(newAnnoCmb_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvONProj.Controls.Add(cmb_Temp);
            this.dgvONProj.Controls.Add(newAnnoCmb);
            dgvONProj.Columns[0].Width = 210;
            dgvONProj.Columns[1].Width = 160;
        }

        private void dgvONProj_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvONProj.CurrentCell.ColumnIndex == 0)
                {
                    Rectangle rect = dgvONProj.GetCellDisplayRectangle(dgvONProj.CurrentCell.ColumnIndex, dgvONProj.CurrentCell.RowIndex, false);
                    string sexValue = dgvONProj.CurrentCell.Value.ToString();
                    cmb_Temp.Text = sexValue;
                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;
                }
                else if (this.dgvONProj.CurrentCell.ColumnIndex == 1)
                {
                    Rectangle rect = dgvONProj.GetCellDisplayRectangle(dgvONProj.CurrentCell.ColumnIndex, dgvONProj.CurrentCell.RowIndex, false);
                    string sexValue = dgvONProj.CurrentCell.Value.ToString();

                    newAnnoCmb.Text = sexValue;
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

        private void dgvONProj_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dgvONProj.Rows.Count; i++)
            {
                if (dgvONProj.Rows[i].Cells[0].Value != null && dgvONProj.Rows[i].Cells[0].ColumnIndex == 0)
                {
                    dgvONProj.Rows[i].Cells[0].Tag = dgvONProj.Rows[i].Cells[0].Value.ToString();
                }
                if (dgvONProj.Rows[i].Cells[1].Value != null && dgvONProj.Rows[i].Cells[1].ColumnIndex == 1)
                {
                    dgvONProj.Rows[i].Cells[1].Tag = dgvONProj.Rows[i].Cells[1].Value.ToString();
                }
            }
        }


        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvONProj.CurrentCell.Value = ((ComboBox)sender).Text;
            
        }

        private void newAnnoCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvONProj.CurrentCell.Value = ((ComboBox)sender).Text;
          
        }

        private void dgvONProj_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            this.newAnnoCmb.Visible = false;
        }

        private void dgvONProj_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
            this.newAnnoCmb.Visible = false;
        }



    }
}