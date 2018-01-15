namespace MapGIS2017Ultimate
{
    partial class AccessUpdateDlg
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessUpdateDlg));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtBxAccessPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnImportAccess = new DevComponents.DotNetBar.ButtonX();
            this.advTreeTableList = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReSetKT = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdateKT = new DevComponents.DotNetBar.ButtonX();
            this.txtNewKTBH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbxKTBH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtKDBH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnResetDatagridView = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdateKD = new DevComponents.DotNetBar.ButtonX();
            this.cbxTableName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxKCName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnReSetKDCL = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdateKDCL = new DevComponents.DotNetBar.ButtonX();
            this.dgvFieldsUpdate = new System.Windows.Forms.DataGridView();
            this.ColField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.advTreeTableList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldsUpdate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "数据表导入";
            // 
            // txtBxAccessPath
            // 
            // 
            // 
            // 
            this.txtBxAccessPath.Border.Class = "TextBoxBorder";
            this.txtBxAccessPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBxAccessPath.Location = new System.Drawing.Point(12, 34);
            this.txtBxAccessPath.Name = "txtBxAccessPath";
            this.txtBxAccessPath.Size = new System.Drawing.Size(156, 21);
            this.txtBxAccessPath.TabIndex = 1;
            // 
            // btnImportAccess
            // 
            this.btnImportAccess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportAccess.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportAccess.Location = new System.Drawing.Point(174, 34);
            this.btnImportAccess.Name = "btnImportAccess";
            this.btnImportAccess.Size = new System.Drawing.Size(48, 23);
            this.btnImportAccess.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportAccess.TabIndex = 2;
            this.btnImportAccess.Text = "导入";
            this.btnImportAccess.Click += new System.EventHandler(this.btnImportAccess_Click);
            // 
            // advTreeTableList
            // 
            this.advTreeTableList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTreeTableList.AllowDrop = true;
            this.advTreeTableList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTreeTableList.BackgroundStyle.Class = "TreeBorderKey";
            this.advTreeTableList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTreeTableList.Location = new System.Drawing.Point(12, 63);
            this.advTreeTableList.Name = "advTreeTableList";
            this.advTreeTableList.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.advTreeTableList.NodesConnector = this.nodeConnector1;
            this.advTreeTableList.NodeStyle = this.elementStyle1;
            this.advTreeTableList.PathSeparator = ";";
            this.advTreeTableList.Size = new System.Drawing.Size(210, 554);
            this.advTreeTableList.Styles.Add(this.elementStyle1);
            this.advTreeTableList.TabIndex = 3;
            this.advTreeTableList.Text = "advTree1";
            this.advTreeTableList.NodeDoubleClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.advTreeTableList_NodeDoubleClick);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(228, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(106, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "待更新表名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReSetKT);
            this.groupBox1.Controls.Add(this.btnUpdateKT);
            this.groupBox1.Controls.Add(this.txtNewKTBH);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.cbxKTBH);
            this.groupBox1.Location = new System.Drawing.Point(490, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 120);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1、矿体更新";
            // 
            // btnReSetKT
            // 
            this.btnReSetKT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReSetKT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReSetKT.Location = new System.Drawing.Point(175, 91);
            this.btnReSetKT.Name = "btnReSetKT";
            this.btnReSetKT.Size = new System.Drawing.Size(69, 23);
            this.btnReSetKT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReSetKT.TabIndex = 10;
            this.btnReSetKT.Text = "重置";
            this.btnReSetKT.Click += new System.EventHandler(this.btnReSetKT_Click);
            // 
            // btnUpdateKT
            // 
            this.btnUpdateKT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdateKT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdateKT.Location = new System.Drawing.Point(102, 91);
            this.btnUpdateKT.Name = "btnUpdateKT";
            this.btnUpdateKT.Size = new System.Drawing.Size(70, 23);
            this.btnUpdateKT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdateKT.TabIndex = 4;
            this.btnUpdateKT.Text = "更新";
            this.btnUpdateKT.Click += new System.EventHandler(this.btnUpdateKTBH_Click);
            // 
            // txtNewKTBH
            // 
            // 
            // 
            // 
            this.txtNewKTBH.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewKTBH.Location = new System.Drawing.Point(65, 47);
            this.txtNewKTBH.Name = "txtNewKTBH";
            this.txtNewKTBH.Size = new System.Drawing.Size(179, 21);
            this.txtNewKTBH.TabIndex = 3;
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(6, 47);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "新编号";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(3, 20);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(58, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "矿体编号";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbxKTBH
            // 
            this.cbxKTBH.DisplayMember = "Text";
            this.cbxKTBH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxKTBH.FormattingEnabled = true;
            this.cbxKTBH.ItemHeight = 15;
            this.cbxKTBH.Location = new System.Drawing.Point(65, 20);
            this.cbxKTBH.Name = "cbxKTBH";
            this.cbxKTBH.Size = new System.Drawing.Size(179, 21);
            this.cbxKTBH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxKTBH.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtKDBH);
            this.groupBox2.Controls.Add(this.labelX6);
            this.groupBox2.Controls.Add(this.btnResetDatagridView);
            this.groupBox2.Controls.Add(this.btnUpdateKD);
            this.groupBox2.Location = new System.Drawing.Point(490, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 114);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2、核查块段更新";
            // 
            // txtKDBH
            // 
            // 
            // 
            // 
            this.txtKDBH.Border.Class = "TextBoxBorder";
            this.txtKDBH.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtKDBH.Location = new System.Drawing.Point(81, 34);
            this.txtKDBH.Name = "txtKDBH";
            this.txtKDBH.Size = new System.Drawing.Size(155, 21);
            this.txtKDBH.TabIndex = 11;
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(6, 34);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(69, 23);
            this.labelX6.TabIndex = 10;
            this.labelX6.Text = "新块段编号";
            // 
            // btnResetDatagridView
            // 
            this.btnResetDatagridView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnResetDatagridView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnResetDatagridView.Location = new System.Drawing.Point(175, 74);
            this.btnResetDatagridView.Name = "btnResetDatagridView";
            this.btnResetDatagridView.Size = new System.Drawing.Size(69, 23);
            this.btnResetDatagridView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnResetDatagridView.TabIndex = 9;
            this.btnResetDatagridView.Text = "重置";
            this.btnResetDatagridView.Click += new System.EventHandler(this.btnResetDatagridView_Click);
            // 
            // btnUpdateKD
            // 
            this.btnUpdateKD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdateKD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdateKD.Location = new System.Drawing.Point(99, 74);
            this.btnUpdateKD.Name = "btnUpdateKD";
            this.btnUpdateKD.Size = new System.Drawing.Size(70, 23);
            this.btnUpdateKD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdateKD.TabIndex = 8;
            this.btnUpdateKD.Text = "更新";
            this.btnUpdateKD.Click += new System.EventHandler(this.btnUpdateKD_Click);
            // 
            // cbxTableName
            // 
            this.cbxTableName.DisplayMember = "Text";
            this.cbxTableName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxTableName.FormattingEnabled = true;
            this.cbxTableName.ItemHeight = 15;
            this.cbxTableName.Location = new System.Drawing.Point(228, 33);
            this.cbxTableName.Name = "cbxTableName";
            this.cbxTableName.Size = new System.Drawing.Size(256, 21);
            this.cbxTableName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxTableName.TabIndex = 10;
            this.cbxTableName.SelectedIndexChanged += new System.EventHandler(this.cbxTableName_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxKCName);
            this.groupBox3.Controls.Add(this.labelX7);
            this.groupBox3.Controls.Add(this.btnReSetKDCL);
            this.groupBox3.Controls.Add(this.btnUpdateKDCL);
            this.groupBox3.Location = new System.Drawing.Point(490, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 134);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3、核查块段储量更新";
            // 
            // cbxKCName
            // 
            this.cbxKCName.DisplayMember = "Text";
            this.cbxKCName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxKCName.FormattingEnabled = true;
            this.cbxKCName.ItemHeight = 15;
            this.cbxKCName.Location = new System.Drawing.Point(12, 49);
            this.cbxKCName.Name = "cbxKCName";
            this.cbxKCName.Size = new System.Drawing.Size(224, 21);
            this.cbxKCName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxKCName.TabIndex = 20;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(12, 25);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(75, 23);
            this.labelX7.TabIndex = 19;
            this.labelX7.Text = "矿产名称";
            // 
            // btnReSetKDCL
            // 
            this.btnReSetKDCL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReSetKDCL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReSetKDCL.Location = new System.Drawing.Point(175, 89);
            this.btnReSetKDCL.Name = "btnReSetKDCL";
            this.btnReSetKDCL.Size = new System.Drawing.Size(69, 23);
            this.btnReSetKDCL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReSetKDCL.TabIndex = 18;
            this.btnReSetKDCL.Text = "重置";
            this.btnReSetKDCL.Click += new System.EventHandler(this.btnReSetKDCL_Click);
            // 
            // btnUpdateKDCL
            // 
            this.btnUpdateKDCL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdateKDCL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdateKDCL.Location = new System.Drawing.Point(99, 89);
            this.btnUpdateKDCL.Name = "btnUpdateKDCL";
            this.btnUpdateKDCL.Size = new System.Drawing.Size(70, 23);
            this.btnUpdateKDCL.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdateKDCL.TabIndex = 17;
            this.btnUpdateKDCL.Text = "更新";
            this.btnUpdateKDCL.Click += new System.EventHandler(this.btnUpdateKDCL_Click);
            // 
            // dgvFieldsUpdate
            // 
            this.dgvFieldsUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFieldsUpdate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColField,
            this.ColValue});
            this.dgvFieldsUpdate.Location = new System.Drawing.Point(228, 87);
            this.dgvFieldsUpdate.Name = "dgvFieldsUpdate";
            this.dgvFieldsUpdate.RowTemplate.Height = 23;
            this.dgvFieldsUpdate.Size = new System.Drawing.Size(256, 530);
            this.dgvFieldsUpdate.TabIndex = 12;
            this.dgvFieldsUpdate.CurrentCellChanged += new System.EventHandler(this.dgvFieldsUpdate_CurrentCellChanged);
            // 
            // ColField
            // 
            this.ColField.DataPropertyName = "Fields";
            this.ColField.HeaderText = "更新字段";
            this.ColField.Name = "ColField";
            this.ColField.Width = 85;
            // 
            // ColValue
            // 
            this.ColValue.DataPropertyName = "NewValue";
            this.ColValue.HeaderText = "更新值";
            this.ColValue.Name = "ColValue";
            this.ColValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColValue.Width = 105;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(228, 63);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(147, 23);
            this.labelX3.TabIndex = 11;
            this.labelX3.Text = "待更新字段设置";
            // 
            // AccessUpdateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 619);
            this.Controls.Add(this.cbxTableName);
            this.Controls.Add(this.dgvFieldsUpdate);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.advTreeTableList);
            this.Controls.Add(this.btnImportAccess);
            this.Controls.Add(this.txtBxAccessPath);
            this.Controls.Add(this.labelX1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AccessUpdateDlg";
            this.Text = "Access数据表更新";
            this.Load += new System.EventHandler(this.AccessUpdateDlg_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccessUpdateDlg_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.advTreeTableList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldsUpdate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBxAccessPath;
        private DevComponents.DotNetBar.ButtonX btnImportAccess;
        private DevComponents.AdvTree.AdvTree advTreeTableList;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxKTBH;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewKTBH;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnUpdateKT;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX btnResetDatagridView;
        private DevComponents.DotNetBar.ButtonX btnUpdateKD;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxTableName;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX btnReSetKDCL;
        private DevComponents.DotNetBar.ButtonX btnUpdateKDCL;
        private System.Windows.Forms.DataGridView dgvFieldsUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColField;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColValue;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtKDBH;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxKCName;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnReSetKT;
    }
}