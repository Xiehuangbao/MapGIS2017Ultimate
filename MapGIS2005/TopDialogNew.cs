using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2005
{
    public partial class TopDialogNew : DevComponents.DotNetBar.Office2007Form
    {
        public TopDialogNew()
        {
            InitializeComponent();
        }

        private void TopDialogNew_Load(object sender, EventArgs e)
        {
            axMapXView1.WorkSpace = axMxWorkSpace1.ToInterface;
        }
    }
}