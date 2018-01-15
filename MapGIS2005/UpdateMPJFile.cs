using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2005
{
    public partial class UpdateMPJFile : DevComponents.DotNetBar.Office2007Form
    {
        MainForm f;

        public UpdateMPJFile(MainForm f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void UpdateMPJFile_Load(object sender, EventArgs e)
        {
            this.labelX3.Visible = false;
            this.txtbSavePath.Visible = false;
            this.btnMPJSave.Visible = false;
        }

        private void sbSaveOrNo_ValueChanged(object sender, EventArgs e)
        {
            if (this.sbSaveOrNo.Value)
            {
                this.labelX3.Visible = true;
                this.txtbSavePath.Visible = true;
                this.btnMPJSave.Visible = true;
            }
            else {
                this.labelX3.Visible = false;
                this.txtbSavePath.Visible = false;
                this.btnMPJSave.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtNewMPJFilePath.Text == "" || !this.txtNewMPJFilePath.Text.Contains(".MPJ")  || this.txtbSavePath.Text == "")
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            f.NewMPJFilePath = this.txtNewMPJFilePath.Text;
            f.SaveMPJFilePath = this.txtbSavePath.Text;
            f.SwichBtnVale = this.sbSaveOrNo.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadNewMPJFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MPJ文件|*.MPJ";
            ofd.ShowDialog();
            this.txtNewMPJFilePath.Text = ofd.FileName;
            f.NewMPJFile = ofd.SafeFileName;
           
        }

        private void btnMPJSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            this.txtbSavePath.Text = fbd.SelectedPath;
        }
    }
}