using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2017Ultimate
{
    public partial class SelectSourceNewProj : DevComponents.DotNetBar.Office2007Form
    {
        MainForm mf;
        public SelectSourceNewProj(MainForm mf)
        {
            InitializeComponent();
            this.mf = mf;
        }

        String Flag = "";
        private void rbSource_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSource.Checked)
            {
                this.Flag = "Source";
            }
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNew.Checked)
            {
                this.Flag = "New";
            }
        }

        private void btnSelectType_Click(object sender, EventArgs e)
        {
            mf.ProjFlag = Flag;
        }
    }
}