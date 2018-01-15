namespace MapGIS2005
{
    partial class GeoUpdateParameterSetDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeoUpdateParameterSetDlg));
            this.WorkSpace1 = new AxWorkSpace.AxMxWorkSpace();
            this.svcList = new System.Windows.Forms.ListBox();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.cbxNewField = new System.Windows.Forms.ComboBox();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cbxSourceField = new System.Windows.Forms.ComboBox();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cbxNewDB = new System.Windows.Forms.ComboBox();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cbsSourceDB = new System.Windows.Forms.ComboBox();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbxNewKQ = new System.Windows.Forms.ComboBox();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cbxSourceKQ = new System.Windows.Forms.ComboBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lstLayerMap = new System.Windows.Forms.ListView();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lstDataNew = new System.Windows.Forms.ListView();
            this.lstSourceData = new System.Windows.Forms.ListView();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.WorkSpace1)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkSpace1
            // 
            this.WorkSpace1.Enabled = true;
            this.WorkSpace1.Location = new System.Drawing.Point(23, 382);
            this.WorkSpace1.Name = "WorkSpace1";
            this.WorkSpace1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WorkSpace1.OcxState")));
            this.WorkSpace1.Size = new System.Drawing.Size(32, 32);
            this.WorkSpace1.TabIndex = 47;
            // 
            // svcList
            // 
            this.svcList.FormattingEnabled = true;
            this.svcList.ItemHeight = 12;
            this.svcList.Location = new System.Drawing.Point(3, 334);
            this.svcList.Name = "svcList";
            this.svcList.Size = new System.Drawing.Size(142, 88);
            this.svcList.TabIndex = 46;
            this.svcList.SelectedIndexChanged += new System.EventHandler(this.svcList_SelectedIndexChanged);
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(3, 305);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(143, 23);
            this.labelX10.TabIndex = 45;
            this.labelX10.Text = "数据库服务器";
            // 
            // cbxNewField
            // 
            this.cbxNewField.FormattingEnabled = true;
            this.cbxNewField.Location = new System.Drawing.Point(469, 399);
            this.cbxNewField.Name = "cbxNewField";
            this.cbxNewField.Size = new System.Drawing.Size(84, 20);
            this.cbxNewField.TabIndex = 42;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(469, 370);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(84, 23);
            this.labelX9.TabIndex = 41;
            this.labelX9.Text = "新判定字段";
            // 
            // cbxSourceField
            // 
            this.cbxSourceField.FormattingEnabled = true;
            this.cbxSourceField.Location = new System.Drawing.Point(469, 334);
            this.cbxSourceField.Name = "cbxSourceField";
            this.cbxSourceField.Size = new System.Drawing.Size(84, 20);
            this.cbxSourceField.TabIndex = 40;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(469, 305);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(84, 23);
            this.labelX8.TabIndex = 39;
            this.labelX8.Text = "原始判定字段";
            // 
            // cbxNewDB
            // 
            this.cbxNewDB.FormattingEnabled = true;
            this.cbxNewDB.Location = new System.Drawing.Point(151, 399);
            this.cbxNewDB.Name = "cbxNewDB";
            this.cbxNewDB.Size = new System.Drawing.Size(143, 20);
            this.cbxNewDB.TabIndex = 38;
            this.cbxNewDB.SelectedIndexChanged += new System.EventHandler(this.cbxNewDB_SelectedIndexChanged);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(151, 370);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(143, 23);
            this.labelX7.TabIndex = 37;
            this.labelX7.Text = "新数据库";
            // 
            // cbsSourceDB
            // 
            this.cbsSourceDB.FormattingEnabled = true;
            this.cbsSourceDB.Location = new System.Drawing.Point(151, 334);
            this.cbsSourceDB.Name = "cbsSourceDB";
            this.cbsSourceDB.Size = new System.Drawing.Size(143, 20);
            this.cbsSourceDB.TabIndex = 36;
            this.cbsSourceDB.SelectedIndexChanged += new System.EventHandler(this.cbsSourceDB_SelectedIndexChanged);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(151, 305);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(143, 23);
            this.labelX6.TabIndex = 35;
            this.labelX6.Text = "原始数据库";
            // 
            // cbxNewKQ
            // 
            this.cbxNewKQ.FormattingEnabled = true;
            this.cbxNewKQ.Location = new System.Drawing.Point(300, 399);
            this.cbxNewKQ.Name = "cbxNewKQ";
            this.cbxNewKQ.Size = new System.Drawing.Size(163, 20);
            this.cbxNewKQ.TabIndex = 34;
            this.cbxNewKQ.SelectedIndexChanged += new System.EventHandler(this.cbxNewKQ_SelectedIndexChanged);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(300, 370);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(143, 23);
            this.labelX5.TabIndex = 33;
            this.labelX5.Text = "新数据矿区范围";
            // 
            // cbxSourceKQ
            // 
            this.cbxSourceKQ.FormattingEnabled = true;
            this.cbxSourceKQ.Location = new System.Drawing.Point(300, 334);
            this.cbxSourceKQ.Name = "cbxSourceKQ";
            this.cbxSourceKQ.Size = new System.Drawing.Size(163, 20);
            this.cbxSourceKQ.TabIndex = 32;
            this.cbxSourceKQ.SelectedIndexChanged += new System.EventHandler(this.cbxSourceKQ_SelectedIndexChanged);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(300, 305);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(143, 23);
            this.labelX4.TabIndex = 31;
            this.labelX4.Text = "原始矿区范围";
            // 
            // lstLayerMap
            // 
            this.lstLayerMap.LabelEdit = true;
            this.lstLayerMap.Location = new System.Drawing.Point(152, 31);
            this.lstLayerMap.Name = "lstLayerMap";
            this.lstLayerMap.Size = new System.Drawing.Size(243, 268);
            this.lstLayerMap.TabIndex = 30;
            this.lstLayerMap.UseCompatibleStateImageBehavior = false;
            this.lstLayerMap.View = System.Windows.Forms.View.Details;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(173, 2);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(117, 23);
            this.labelX3.TabIndex = 29;
            this.labelX3.Text = "更新数据列表";
            // 
            // lstDataNew
            // 
            this.lstDataNew.Location = new System.Drawing.Point(401, 31);
            this.lstDataNew.Name = "lstDataNew";
            this.lstDataNew.Size = new System.Drawing.Size(152, 268);
            this.lstDataNew.TabIndex = 28;
            this.lstDataNew.UseCompatibleStateImageBehavior = false;
            this.lstDataNew.View = System.Windows.Forms.View.Details;
            this.lstDataNew.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDataNew_MouseDoubleClick);
            // 
            // lstSourceData
            // 
            this.lstSourceData.Location = new System.Drawing.Point(3, 31);
            this.lstSourceData.Name = "lstSourceData";
            this.lstSourceData.Size = new System.Drawing.Size(143, 268);
            this.lstSourceData.TabIndex = 27;
            this.lstSourceData.UseCompatibleStateImageBehavior = false;
            this.lstSourceData.View = System.Windows.Forms.View.Details;
            this.lstSourceData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSourceData_MouseDoubleClick);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(401, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 26;
            this.labelX2.Text = "新数据";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 25;
            this.labelX1.Text = "原始数据";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(362, 440);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 48;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(469, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 49;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GeoUpdateParameterSetDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 464);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.WorkSpace1);
            this.Controls.Add(this.svcList);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.cbxNewField);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.cbxSourceField);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.cbxNewDB);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.cbsSourceDB);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.cbxNewKQ);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.cbxSourceKQ);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lstLayerMap);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.lstDataNew);
            this.Controls.Add(this.lstSourceData);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Name = "GeoUpdateParameterSetDlg";
            this.Text = "几何数据更新参数设置";
            this.Load += new System.EventHandler(this.GeoUpdateParameterSetDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WorkSpace1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWorkSpace.AxMxWorkSpace WorkSpace1;
        private System.Windows.Forms.ListBox svcList;
        private DevComponents.DotNetBar.LabelX labelX10;
        private System.Windows.Forms.ComboBox cbxNewField;
        private DevComponents.DotNetBar.LabelX labelX9;
        private System.Windows.Forms.ComboBox cbxSourceField;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.ComboBox cbxNewDB;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.ComboBox cbsSourceDB;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.ComboBox cbxNewKQ;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.ComboBox cbxSourceKQ;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.ListView lstLayerMap;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.ListView lstDataNew;
        private System.Windows.Forms.ListView lstSourceData;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}