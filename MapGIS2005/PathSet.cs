using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MapGIS2005
{
    public partial class PathSet : DevComponents.DotNetBar.Office2007Form
    {
        public string sourcePath = "C:\\�����Ŀ\\MapGIS2005\\����ʡ�����Դ��״����ɹ�";
        public string newPath = "";
        public string historyPath = "";

        public PathSet()
        {
            InitializeComponent();
        }

        private void btnSourcePathOK_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            this.txtSourcePath.Text = folderDlg.SelectedPath;
            //sourcePath = "C:\\�����Ŀ\\MapGIS2005\\����ʡ�����Դ��״����ɹ�"; 
            sourcePath = folderDlg.SelectedPath;
        }

        private void btnNewPathOK_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            this.txtNewPath.Text = folderDlg.SelectedPath;
            newPath = folderDlg.SelectedPath;
        }

        private void btnHisPathOK_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();
            this.txtHisPath.Text = folderDlg.SelectedPath;
            historyPath = folderDlg.SelectedPath;          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (sourcePath.Equals("") || newPath.Equals("") || historyPath.Equals(""))
            {
                MessageBox.Show("��������·����");
                this.DialogResult = DialogResult.None;                
            }
            else 
            {
                this.Close();
            }           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}