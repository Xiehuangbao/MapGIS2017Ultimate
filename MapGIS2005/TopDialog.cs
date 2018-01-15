using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapGIS2005
{
    public partial class TopDialog : DevComponents.DotNetBar.Office2007Form
    {
        public TopDialog()
        {
            InitializeComponent();
            
        }

        private void TopDialog_Load(object sender, EventArgs e)
        {
            axMapXView1.WorkSpace = axMxWorkSpace1.ToInterface;
        }
    }
}