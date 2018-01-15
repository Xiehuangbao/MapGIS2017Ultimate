namespace MapGIS2005
{
    partial class StatisticTable
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbxStatisticField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbxStatisticItem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtStatisticResult = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.labelX1.Text = "统计字段";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 61);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "统计项";
            // 
            // cbxStatisticField
            // 
            this.cbxStatisticField.DisplayMember = "Text";
            this.cbxStatisticField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxStatisticField.FormattingEnabled = true;
            this.cbxStatisticField.ItemHeight = 15;
            this.cbxStatisticField.Location = new System.Drawing.Point(70, 14);
            this.cbxStatisticField.Name = "cbxStatisticField";
            this.cbxStatisticField.Size = new System.Drawing.Size(195, 21);
            this.cbxStatisticField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxStatisticField.TabIndex = 2;
            this.cbxStatisticField.SelectedIndexChanged += new System.EventHandler(this.cbxStatisticField_SelectedIndexChanged);
            // 
            // cbxStatisticItem
            // 
            this.cbxStatisticItem.DisplayMember = "Text";
            this.cbxStatisticItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxStatisticItem.FormattingEnabled = true;
            this.cbxStatisticItem.ItemHeight = 15;
            this.cbxStatisticItem.Location = new System.Drawing.Point(70, 61);
            this.cbxStatisticItem.Name = "cbxStatisticItem";
            this.cbxStatisticItem.Size = new System.Drawing.Size(195, 21);
            this.cbxStatisticItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxStatisticItem.TabIndex = 3;
            this.cbxStatisticItem.SelectedIndexChanged += new System.EventHandler(this.cbxStatisticItem_SelectedIndexChanged);
            // 
            // txtStatisticResult
            // 
            // 
            // 
            // 
            this.txtStatisticResult.Border.Class = "TextBoxBorder";
            this.txtStatisticResult.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStatisticResult.Location = new System.Drawing.Point(271, 14);
            this.txtStatisticResult.Multiline = true;
            this.txtStatisticResult.Name = "txtStatisticResult";
            this.txtStatisticResult.Size = new System.Drawing.Size(270, 68);
            this.txtStatisticResult.TabIndex = 4;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 99);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "统计图表";
            // 
            // chart1
            // 
            chartArea1.AxisY.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.No;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 128);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(529, 255);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            // 
            // StatisticTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 395);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtStatisticResult);
            this.Controls.Add(this.cbxStatisticItem);
            this.Controls.Add(this.cbxStatisticField);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "StatisticTable";
            this.Text = "统计窗口";
            this.Load += new System.EventHandler(this.StatisticTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxStatisticField;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxStatisticItem;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStatisticResult;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}