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
            this.cbxStatisticField.Items.Add("��ʯ����");
            this.cbxStatisticField.Items.Add("Ʒλ");
            this.cbxStatisticField.Items.Add("�ָ������");
            this.cbxStatisticField.Items.Add("�ָ��ʯ��");
            this.cbxStatisticField.Items.Add("�˲������");
            this.cbxStatisticField.Items.Add("�˲��ʯ��");

            this.cbxStatisticItem.Items.Add("�ܺ�");
            this.cbxStatisticItem.Items.Add("ƽ��ֵ");
        }

        private void cbxStatisticItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string field = "";
            switch (this.cbxStatisticField.Text)
            {
                case "��ʯ����": field = "KSTZ";
                    break;
                case "Ʒλ": field = "PW";
                    break;
                case "�ָ������": field = "FGJSL";
                    break;
                case "�ָ��ʯ��": field = "FGKSL";
                    break;
                case "�˲������": field = "HCJSL";
                    break;
                case "�˲��ʯ��": field = "HCKSL";
                    break;
            }
            DataTable dtNew = null;
            if (cbxStatisticItem.SelectedItem.ToString().Equals("�ܺ�"))
            {
                string SQL = "select sum(" + field + ") as result from " + tablename;
                dtNew = GetDataSet(con, SQL);
            }
            else if (cbxStatisticItem.SelectedItem.ToString().Equals("ƽ��ֵ"))
            {
                string SQL = "select avg(" + field + ") as result from " + tablename;
                dtNew = GetDataSet(con, SQL);
            }
            this.txtStatisticResult.Text = cbxStatisticItem.Text + "Ϊ��" + dtNew.Rows[0][0].ToString();

            string sql = "select KCMC ," + field + " from " + tablename ;
            DataTable dt = new DataTable();
            dt = GetDataSet(con, sql);
            if (this.chart1.Series.Count > 1)
            {
                chart1.Series.RemoveAt(chart1.Series.Count - 1);
            }
            
            chart1.DataSource = dt; //����chart������Դ
            chart1.Series[0].YValueMembers = field; //y���ֵ
            chart1.Series[0].XValueMember = "KCMC"; //x���ֵ
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
        /// ��ȡdataset
        /// </summary>
        /// <param name="conn">�����ַ���</param>
        /// <param name="SQL">��ȡ���ݱ��sql���</param>
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