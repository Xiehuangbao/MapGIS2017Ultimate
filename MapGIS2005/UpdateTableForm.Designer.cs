namespace MapGIS2005
{
    partial class UpdateTableForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbxSelectKQuan = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbxTableList = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCheck = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnInsert = new DevComponents.DotNetBar.ButtonX();
            this.btnBackUP = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.lstvKD = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewSource = new System.Windows.Forms.DataGridView();
            this.dataGridViewNew = new System.Windows.Forms.DataGridView();
            this.dateTimeInput1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtUpdateReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbxKQu = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbxUpdateKQuan = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.txtComment = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOperator = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtManager = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(9, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 23);
            this.labelX1.TabIndex = 19;
            this.labelX1.Text = "选择矿权";
            // 
            // cbxSelectKQuan
            // 
            this.cbxSelectKQuan.DisplayMember = "Text";
            this.cbxSelectKQuan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxSelectKQuan.FormattingEnabled = true;
            this.cbxSelectKQuan.ItemHeight = 21;
            this.cbxSelectKQuan.Location = new System.Drawing.Point(72, 12);
            this.cbxSelectKQuan.Name = "cbxSelectKQuan";
            this.cbxSelectKQuan.Size = new System.Drawing.Size(225, 27);
            this.cbxSelectKQuan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxSelectKQuan.TabIndex = 18;
            this.cbxSelectKQuan.SelectedIndexChanged += new System.EventHandler(this.cbxSelectKQuan_SelectedIndexChanged);
            // 
            // cbxTableList
            // 
            this.cbxTableList.DisplayMember = "Text";
            this.cbxTableList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxTableList.FormattingEnabled = true;
            this.cbxTableList.ItemHeight = 21;
            this.cbxTableList.Location = new System.Drawing.Point(584, 453);
            this.cbxTableList.Name = "cbxTableList";
            this.cbxTableList.Size = new System.Drawing.Size(185, 27);
            this.cbxTableList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxTableList.TabIndex = 34;
            this.cbxTableList.SelectedIndexChanged += new System.EventHandler(this.cbxTableList_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCheck.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCheck.Location = new System.Drawing.Point(775, 451);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(77, 31);
            this.btnCheck.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCheck.TabIndex = 33;
            this.btnCheck.Text = "检测";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(961, 451);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 31);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 32;
            this.btnDelete.Text = "删除数据";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInsert.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInsert.Location = new System.Drawing.Point(1059, 451);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(84, 31);
            this.btnInsert.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInsert.TabIndex = 31;
            this.btnInsert.Text = "插入数据";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnBackUP
            // 
            this.btnBackUP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackUP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBackUP.Location = new System.Drawing.Point(868, 451);
            this.btnBackUP.Name = "btnBackUP";
            this.btnBackUP.Size = new System.Drawing.Size(75, 31);
            this.btnBackUP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBackUP.TabIndex = 30;
            this.btnBackUP.Text = "备份数据";
            this.btnBackUP.Click += new System.EventHandler(this.btnBackUP_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(584, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 35;
            this.labelX2.Text = "原始数据表";
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(584, 225);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(75, 23);
            this.labelX13.TabIndex = 36;
            this.labelX13.Text = "新数据表";
            // 
            // lstvKD
            // 
            // 
            // 
            // 
            this.lstvKD.Border.Class = "ListViewBorder";
            this.lstvKD.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstvKD.Location = new System.Drawing.Point(70, 47);
            this.lstvKD.Name = "lstvKD";
            this.lstvKD.Size = new System.Drawing.Size(227, 117);
            this.lstvKD.TabIndex = 38;
            this.lstvKD.UseCompatibleStateImageBehavior = false;
            this.lstvKD.View = System.Windows.Forms.View.Details;
            // 
            // labelX14
            // 
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(9, 44);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(55, 23);
            this.labelX14.TabIndex = 37;
            this.labelX14.Text = "核查块段";
            // 
            // dataGridViewSource
            // 
            this.dataGridViewSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSource.Location = new System.Drawing.Point(580, 31);
            this.dataGridViewSource.Name = "dataGridViewSource";
            this.dataGridViewSource.RowTemplate.Height = 23;
            this.dataGridViewSource.Size = new System.Drawing.Size(563, 185);
            this.dataGridViewSource.TabIndex = 39;
            // 
            // dataGridViewNew
            // 
            this.dataGridViewNew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNew.Location = new System.Drawing.Point(584, 254);
            this.dataGridViewNew.Name = "dataGridViewNew";
            this.dataGridViewNew.RowTemplate.Height = 23;
            this.dataGridViewNew.Size = new System.Drawing.Size(563, 191);
            this.dataGridViewNew.TabIndex = 40;
            // 
            // dateTimeInput1
            // 
            // 
            // 
            // 
            this.dateTimeInput1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInput1.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateTimeInput1.ButtonDropDown.Visible = true;
            this.dateTimeInput1.IsPopupCalendarOpen = false;
            this.dateTimeInput1.Location = new System.Drawing.Point(65, 277);
            this.dateTimeInput1.Margin = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInput1.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dateTimeInput1.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInput1.MonthCalendar.DisplayMonth = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dateTimeInput1.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dateTimeInput1.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInput1.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInput1.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInput1.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimeInput1.Name = "dateTimeInput1";
            this.dateTimeInput1.Size = new System.Drawing.Size(231, 21);
            this.dateTimeInput1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateTimeInput1.TabIndex = 48;
            // 
            // txtUpdateReason
            // 
            // 
            // 
            // 
            this.txtUpdateReason.Border.Class = "TextBoxBorder";
            this.txtUpdateReason.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUpdateReason.Location = new System.Drawing.Point(65, 329);
            this.txtUpdateReason.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateReason.Multiline = true;
            this.txtUpdateReason.Name = "txtUpdateReason";
            this.txtUpdateReason.Size = new System.Drawing.Size(231, 147);
            this.txtUpdateReason.TabIndex = 47;
            // 
            // cbxKQu
            // 
            this.cbxKQu.DisplayMember = "Text";
            this.cbxKQu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxKQu.FormattingEnabled = true;
            this.cbxKQu.ItemHeight = 21;
            this.cbxKQu.Location = new System.Drawing.Point(65, 234);
            this.cbxKQu.Margin = new System.Windows.Forms.Padding(2);
            this.cbxKQu.Name = "cbxKQu";
            this.cbxKQu.Size = new System.Drawing.Size(232, 27);
            this.cbxKQu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxKQu.TabIndex = 46;
            // 
            // cbxUpdateKQuan
            // 
            this.cbxUpdateKQuan.DisplayMember = "Text";
            this.cbxUpdateKQuan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxUpdateKQuan.FormattingEnabled = true;
            this.cbxUpdateKQuan.ItemHeight = 21;
            this.cbxUpdateKQuan.Location = new System.Drawing.Point(65, 192);
            this.cbxUpdateKQuan.Margin = new System.Windows.Forms.Padding(2);
            this.cbxUpdateKQuan.Name = "cbxUpdateKQuan";
            this.cbxUpdateKQuan.Size = new System.Drawing.Size(232, 27);
            this.cbxUpdateKQuan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxUpdateKQuan.TabIndex = 45;
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(9, 327);
            this.labelX10.Margin = new System.Windows.Forms.Padding(2);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(107, 15);
            this.labelX10.TabIndex = 44;
            this.labelX10.Text = "更新原因";
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(9, 281);
            this.labelX11.Margin = new System.Windows.Forms.Padding(2);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(107, 15);
            this.labelX11.TabIndex = 43;
            this.labelX11.Text = "更新时间";
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(9, 241);
            this.labelX12.Margin = new System.Windows.Forms.Padding(2);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(107, 15);
            this.labelX12.TabIndex = 42;
            this.labelX12.Text = "所属矿区";
            // 
            // labelX15
            // 
            this.labelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(9, 199);
            this.labelX15.Margin = new System.Windows.Forms.Padding(2);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(107, 15);
            this.labelX15.TabIndex = 41;
            this.labelX15.Text = "更新矿权";
            // 
            // txtComment
            // 
            // 
            // 
            // 
            this.txtComment.Border.Class = "TextBoxBorder";
            this.txtComment.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtComment.Location = new System.Drawing.Point(302, 163);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(265, 313);
            this.txtComment.TabIndex = 54;
            // 
            // txtOperator
            // 
            // 
            // 
            // 
            this.txtOperator.Border.Class = "TextBoxBorder";
            this.txtOperator.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOperator.Location = new System.Drawing.Point(302, 107);
            this.txtOperator.Margin = new System.Windows.Forms.Padding(2);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(265, 21);
            this.txtOperator.TabIndex = 53;
            // 
            // txtManager
            // 
            // 
            // 
            // 
            this.txtManager.Border.Class = "TextBoxBorder";
            this.txtManager.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtManager.Location = new System.Drawing.Point(302, 40);
            this.txtManager.Margin = new System.Windows.Forms.Padding(2);
            this.txtManager.Name = "txtManager";
            this.txtManager.Size = new System.Drawing.Size(265, 21);
            this.txtManager.TabIndex = 52;
            // 
            // labelX16
            // 
            this.labelX16.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(302, 144);
            this.labelX16.Margin = new System.Windows.Forms.Padding(2);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(61, 15);
            this.labelX16.TabIndex = 51;
            this.labelX16.Text = "备 注";
            // 
            // labelX17
            // 
            this.labelX17.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(302, 87);
            this.labelX17.Margin = new System.Windows.Forms.Padding(2);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(61, 15);
            this.labelX17.TabIndex = 50;
            this.labelX17.Text = "操作员";
            // 
            // labelX18
            // 
            this.labelX18.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(302, 19);
            this.labelX18.Margin = new System.Windows.Forms.Padding(2);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(61, 15);
            this.labelX18.TabIndex = 49;
            this.labelX18.Text = "负责人";
            // 
            // UpdateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 483);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtOperator);
            this.Controls.Add(this.txtManager);
            this.Controls.Add(this.labelX16);
            this.Controls.Add(this.labelX17);
            this.Controls.Add(this.labelX18);
            this.Controls.Add(this.dateTimeInput1);
            this.Controls.Add(this.txtUpdateReason);
            this.Controls.Add(this.cbxKQu);
            this.Controls.Add(this.cbxUpdateKQuan);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX11);
            this.Controls.Add(this.labelX12);
            this.Controls.Add(this.labelX15);
            this.Controls.Add(this.dataGridViewNew);
            this.Controls.Add(this.dataGridViewSource);
            this.Controls.Add(this.lstvKD);
            this.Controls.Add(this.labelX14);
            this.Controls.Add(this.labelX13);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbxTableList);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnBackUP);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cbxSelectKQuan);
            this.MaximizeBox = false;
            this.Name = "UpdateTableForm";
            this.Text = "矿区数据表更新";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxSelectKQuan;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxTableList;
        private DevComponents.DotNetBar.ButtonX btnCheck;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnInsert;
        private DevComponents.DotNetBar.ButtonX btnBackUP;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.ListViewEx lstvKD;
        private DevComponents.DotNetBar.LabelX labelX14;
        private System.Windows.Forms.DataGridView dataGridViewSource;
        private System.Windows.Forms.DataGridView dataGridViewNew;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUpdateReason;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxKQu;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxUpdateKQuan;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.Controls.TextBoxX txtComment;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOperator;
        private DevComponents.DotNetBar.Controls.TextBoxX txtManager;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.LabelX labelX18;


    }
}