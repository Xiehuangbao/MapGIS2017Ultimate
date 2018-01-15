using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2005
{
    public partial class CheckResult : DevComponents.DotNetBar.Office2007Form
    {
        DataTable Add;
        DataTable Dif1;
        DataTable Dif2;
        DataTable Del;
        public CheckResult(DataTable Add, DataTable Dif1, DataTable Dif2, DataTable Del)
        {
            InitializeComponent();
            this.Add = Add;
            this.Dif1 = Dif1;
            this.Dif2 = Dif2;
            this.Del = Del;
        }

        private void CheckResult_Load(object sender, EventArgs e)
        {
            this.dgv_dtRetAdd.DataSource = Add;
            this.dgv_dtRetDel.DataSource = Del;
            this.dgv_dtRetDif1.DataSource = Dif1;
            this.dgv_dtRetDif2.DataSource = Dif2;
        }
    }
}