using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using WorkSpace;
using AxWorkSpace;

namespace MapGIS2005
{
    public partial class GeoUpdateParameterSetDlg : DevComponents.DotNetBar.Office2007Form
    {
        GeoUpdateDlg f;
        mcDataSrcMng m_svcMng = null;
        mcGDBServer m_svcObj = null;
        mcGDataBase m_gdbObj = null;
        public GeoUpdateParameterSetDlg(GeoUpdateDlg f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void GeoUpdateParameterSetDlg_Load(object sender, EventArgs e)
        {
            this.lstLayerMap.View = View.Details;
            this.lstLayerMap.Columns.Add("原始数据", lstLayerMap.Width / 2, HorizontalAlignment.Center);
            this.lstLayerMap.Columns.Add("新数据", lstLayerMap.Width / 2, HorizontalAlignment.Center);
            this.lstLayerMap.GridLines = true;
            int s_layerCount = f.sourceWorkSpace.MapCollection.get_Item(0).LayerCount;
            int n_layerCount = f.newWorkSpace.MapCollection.get_Item(0).LayerCount;
            this.lstSourceData.Columns.Add("原始数据项", lstSourceData.Width, HorizontalAlignment.Center);
            this.lstSourceData.View = View.Details;
            this.lstSourceData.BeginUpdate();
            //layer图层从序列1开始
            for (int i = 1; i <= s_layerCount; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = f.sourceWorkSpace.MapCollection.get_Item(0).get_Layer(i).LayerName;
                this.lstSourceData.Items.Add(lvi);
            }
            this.lstSourceData.EndUpdate();
            this.lstDataNew.Columns.Add("新数据项", lstDataNew.Width, HorizontalAlignment.Center);
            this.lstDataNew.View = View.Details;
            this.lstDataNew.BeginUpdate();
            for (int i = 1; i <= n_layerCount; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = f.newWorkSpace.MapCollection.get_Item(0).get_Layer(i).LayerName;
                this.lstDataNew.Items.Add(lvi);
            }
            this.lstDataNew.EndUpdate();

            int j = 0, cnt = 0;
            //1.获取数据源管理对象
            m_svcMng = WorkSpace1.DataSrcMng;
            //2.获取数据源连接个数
            cnt = m_svcMng.Count;
            //3.获取数据源名称并添加到列表中
            for (j = 0; j < cnt; j++)
            {
                svcList.Items.Add(m_svcMng.Get(j).SvcName);
            }
        }

        private void lstSourceData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = this.lstSourceData.SelectedItems[0].Text;
            lvi.SubItems.Add(" ");
            this.lstLayerMap.Items.Add(lvi);
        }

        private void lstDataNew_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstLayerMap.SelectedItems.Count > 0)
            {
                int index = this.lstLayerMap.SelectedItems[0].Index;
                this.lstLayerMap.Items[index].SubItems[1].Text = this.lstDataNew.SelectedItems[0].Text;
            }
            else
                MessageBox.Show("请选择相应行");
        }

        private void svcList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化数据库选项
            meConnectType connType;
            int rtn = 0;
            mcEnumIDs gdbIDs = null;
            int gdbID = 0;
            ListViewItem item = null;

            if (svcList.SelectedItem != null)
            {
                m_svcObj = new mcGDBServer();

                //1.判断数据源连接类型,若为非本地连接,需要输入用户名和密码
                connType = m_svcMng.Get(svcList.SelectedIndex).SvcType;
                if (connType == meConnectType.meConLocal || connType == meConnectType.meCon6xLocal)
                    rtn = m_svcObj.Connect(m_svcMng.Get(svcList.SelectedIndex).SvcName, "", "");
                //else
                //{
                //    logFrm logInfo = new logFrm();
                //    if (logInfo.ShowDialog() != DialogResult.OK)
                //        return;
                //    rtn = m_svcObj.Connect(m_svcMng.Get(svcList.SelectedIndex).SvcName, logInfo.m_user, logInfo.m_pswd);
                //}

                if (rtn < 1) return;

                //2.获取当前数据源连接包含的所有数据库
                gdbIDs = m_svcObj.gdbs;
                if (gdbIDs == null) return;

                //gdbList.Items.Clear();
                //2.1.取第一条记录
                gdbID = gdbIDs.Reset();
                while (gdbID > 0)
                {
                    m_gdbObj = m_svcObj.get_gdb0(gdbID);
                    this.cbxNewDB.Items.Add(m_gdbObj.name);
                    this.cbsSourceDB.Items.Add(m_gdbObj.name);
                    //2.2取下一条记录
                    gdbID = gdbIDs.Next();
                }
            }
        }

        private void cbsSourceDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxSourceKQ.Items.Clear();
            int fclsID;
            mcEnumIDs fclsEnum;
            mcFClsInfo fclsInfo;//要素类的信息
            m_gdbObj = m_svcObj.get_gdb(this.cbsSourceDB.SelectedItem.ToString());
            if (m_gdbObj != null)
            {
                fclsEnum = m_gdbObj.get_xclses(meXClsType.meXSFCls, 0);
                //光标复位
                fclsID = fclsEnum.Reset();
                //将fclsID中的ID对应的要素名逐一加到myXFClsList的listbox中。
                while (fclsID > 0)
                {
                    fclsInfo = (mcFClsInfo)m_gdbObj.get_xclsInfo(meXClsType.meXSFCls, fclsID);
                    if (fclsInfo != null)
                    {
                        this.cbxSourceKQ.Items.Add(fclsInfo.name);
                        fclsInfo = null;
                    }
                    fclsID = fclsEnum.Next();
                }
            }
        }

        private void cbxNewDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxNewKQ.Items.Clear();
            int fclsID;
            mcEnumIDs fclsEnum;
            mcFClsInfo fclsInfo;//要素类的信息
            m_gdbObj = m_svcObj.get_gdb(this.cbxNewDB.SelectedItem.ToString());
            if (m_gdbObj != null)
            {
                fclsEnum = m_gdbObj.get_xclses(meXClsType.meXSFCls, 0);
                //光标复位
                fclsID = fclsEnum.Reset();
                //将fclsID中的ID对应的要素名逐一加到myXFClsList的listbox中。
                while (fclsID > 0)
                {
                    fclsInfo = (mcFClsInfo)m_gdbObj.get_xclsInfo(meXClsType.meXSFCls, fclsID);
                    if (fclsInfo != null)
                    {
                        this.cbxNewKQ.Items.Add(fclsInfo.name);
                        fclsInfo = null;
                    }
                    fclsID = fclsEnum.Next();
                }
            }
        }

        private void cbxSourceKQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //变量声明
            mcFields Fields = null;
            IVectorCls VecCls = null;

            //打开简单要素类
            VecCls = m_gdbObj.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            VecCls.Open(this.cbxSourceKQ.SelectedItem.ToString(), 0);

            //直接取它的属性结构
            VecCls.GetFields(out Fields);
            for (short i = 0; i < Fields.numbfield; i++)
            {
                this.cbxSourceField.Items.Add(Fields.get_fldEntry(i).fieldname);
            }
        }

        private void cbxNewKQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //变量声明
            mcFields Fields = null;
            IVectorCls VecCls = null;

            //打开简单要素类
            VecCls = m_gdbObj.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            VecCls.Open(this.cbxNewKQ.SelectedItem.ToString(), 0);

            //直接取它的属性结构
            VecCls.GetFields(out Fields);
            for (short i = 0; i < Fields.numbfield; i++)
            {
                this.cbxNewField.Items.Add(Fields.get_fldEntry(i).fieldname);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            f.DB = this.cbxNewDB.SelectedItem.ToString();
            f.DB_source = this.cbsSourceDB.SelectedItem.ToString();
            f.KQ = this.cbxNewKQ.SelectedItem.ToString();
            f.KQ_source = this.cbxSourceKQ.SelectedItem.ToString();
            f.Field = this.cbxNewField.SelectedItem.ToString();
            f.Field_source = this.cbxSourceField.SelectedItem.ToString();
            if (f.dict.Count > 0)
            {
                f.dict.Clear();
            }
            foreach (ListViewItem item in this.lstLayerMap.Items)
            {
                string key = item.Text;
                string value = "";
                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    value = subitem.Text;
                }

                f.dict.Add(key, value);
            }
            this.m_svcObj.DisConnect();
            this.m_gdbObj.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.m_svcObj.DisConnect();
            this.m_gdbObj.Close();
            this.Close();
        }








    }
}