using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2005
{
    public partial class UpdateAccessoryFile : DevComponents.DotNetBar.Office2007Form
    {
        MainForm f;
        public UpdateAccessoryFile(MainForm f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void btnLoadNewAccessoryFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文件|*.*";
            ofd.ShowDialog();
            this.txtNewAccessoryFilePath.Text = ofd.FileName;
            f.NewAccessoryFile = ofd.SafeFileName;
        }

        private void UpdateAccessoryFile_Load(object sender, EventArgs e)
        {
            this.labelX3.Visible = false;
            this.txtbSavePath.Visible = false;
            this.btnAccessorySave.Visible = false;
        }

        private void sbSaveOrNo_ValueChanged(object sender, EventArgs e)
        {
            if (this.sbSaveOrNo.Value)
            {
                this.labelX3.Visible = true;
                this.txtbSavePath.Visible = true;
                this.btnAccessorySave.Visible = true;
            }
            else
            {
                this.labelX3.Visible = false;
                this.txtbSavePath.Visible = false;
                this.btnAccessorySave.Visible = false;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccessorySave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            this.txtbSavePath.Text = fbd.SelectedPath;
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (this.txtNewAccessoryFilePath.Text == "" || this.txtbSavePath.Text == "")
            {
                MessageBox.Show("请选择文件！");
                return;
            }
            f.NewAccessoryFilePath = this.txtNewAccessoryFilePath.Text;
            f.SaveAccessoryFilePath = this.txtbSavePath.Text;
            f.SwichBtnValeAcc = this.sbSaveOrNo.Value;
            this.Close();
        }
    }
}