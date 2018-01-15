using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization.Charting;

namespace MapGIS2005
{
    public partial class StatisticTable : DevComponents.DotNetBar.Office2007Form
    {
        OleDbConnection con;
        string tablename;
        public StatisticTable(OleDbConnection con,string tablename)
        {
            InitializeComponent();
            this.con = con;
            this.tablename = tablename;
        }

        private void StatisticTable_Load(object sender, EventArgs e)
        {
            this.cbxStatisticField.Items.Add("矿石体重");
            this.cbxStatisticField.Items.Add("品位");
            this.cbxStatisticField.Items.Add("分割金属量");
            this.cbxStatisticField.Items.Add("分割矿石量");
            this.cbxStatisticField.Items.Add("核查金属量");
            this.cbxStatisticField.Items.Add("核查矿石量");

            this.cbxStatisticItem.Items.Add("总和");
            this.cbxStatisticItem.Items.Add("平均值");
        }

        private void cbxStatisticItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string field = "";
            switch (this.cbxStatisticField.Text)
            {
                case "矿石体重": field = "KSTZ";
                    break;
                case "品位": field = "PW";
                    break;
                case "分割金属量": field = "FGJSL";
                    break;
                case "分割矿石量": field = "FGKSL";
                    break;
                case "核查金属量": field = "HCJSL";
                    break;
                case "核查矿石量": field = "HCKSL";
                    break;
            }
            DataTable dtNew = null;
            if (cbxStatisticItem.SelectedItem.ToString().Equals("总和"))
            {
                string SQL = "select sum(" + field + ") as result from " + tablename;
                dtNew = GetDataSet(con, SQL);
            }
            else if (cbxStatisticItem.SelectedItem.ToString().Equals("平均值"))
            {
                string SQL = "select avg(" + field + ") as result from " + tablename;
                dtNew = GetDataSet(con, SQL);
            }
            this.txtStatisticResult.Text = cbxStatisticItem.Text + "为：" + dtNew.Rows[0][0].ToString();

            string sql = "select KCMC ," + field + " from " + tablename ;
            DataTable dt = new DataTable();
            dt = GetDataSet(con, sql);
            if (this.chart1.Series.Count > 1)
            {
                chart1.Series.RemoveAt(chart1.Series.Count - 1);
            }
            
            chart1.DataSource = dt; //设置chart的数据源
            chart1.Series[0].YValueMembers = field; //y轴的值
            chart1.Series[0].XValueMember = "KCMC"; //x轴的值
            chart1.Series[0].LegendText = field;
            chart1.DataBind();    
            Series ser = new Series();
            ser.ChartType = SeriesChartType.Line;
            ser.BorderWidth = 3;
            ser.BorderColor = Color.Yellow;
            ser.LegendText = cbxStatisticItem.Text;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ser.Points.AddY(Double.Parse(dtNew.Rows[0][0].ToString()));
            }
            this.chart1.Series.Add(ser);
            
            con.Close();
        }

        /// <summary>
        /// 获取dataset
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="SQL">获取数据表的sql语句</param>
        /// <returns></returns>
        public DataTable GetDataSet(OleDbConnection conn, string SQL)
        {  
            try
            {
                OleDbDataAdapter odda = new OleDbDataAdapter();
                OleDbCommand odc = new OleDbCommand(SQL, conn);
                odc.CommandType = CommandType.Text;
                odda.SelectCommand = odc;
                DataTable dt = new DataTable();
                odda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void cbxStatisticField_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxStatisticItem.Text = "";
        }
    }
}