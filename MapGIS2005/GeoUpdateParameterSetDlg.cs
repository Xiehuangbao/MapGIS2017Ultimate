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
            this.lstLayerMap.Columns.Add("ԭʼ����", lstLayerMap.Width / 2, HorizontalAlignment.Center);
            this.lstLayerMap.Columns.Add("������", lstLayerMap.Width / 2, HorizontalAlignment.Center);
            this.lstLayerMap.GridLines = true;
            int s_layerCount = f.sourceWorkSpace.MapCollection.get_Item(0).LayerCount;
            int n_layerCount = f.newWorkSpace.MapCollection.get_Item(0).LayerCount;
            this.lstSourceData.Columns.Add("ԭʼ������", lstSourceData.Width, HorizontalAlignment.Center);
            this.lstSourceData.View = View.Details;
            this.lstSourceData.BeginUpdate();
            //layerͼ�������1��ʼ
            for (int i = 1; i <= s_layerCount; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = f.sourceWorkSpace.MapCollection.get_Item(0).get_Layer(i).LayerName;
                this.lstSourceData.Items.Add(lvi);
            }
            this.lstSourceData.EndUpdate();
            this.lstDataNew.Columns.Add("��������", lstDataNew.Width, HorizontalAlignment.Center);
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
            //1.��ȡ����Դ�������
            m_svcMng = WorkSpace1.DataSrcMng;
            //2.��ȡ����Դ���Ӹ���
            cnt = m_svcMng.Count;
            //3.��ȡ����Դ���Ʋ���ӵ��б���
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
                MessageBox.Show("��ѡ����Ӧ��");
        }

        private void svcList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��ʼ�����ݿ�ѡ��
            meConnectType connType;
            int rtn = 0;
            mcEnumIDs gdbIDs = null;
            int gdbID = 0;
            ListViewItem item = null;

            if (svcList.SelectedItem != null)
            {
                m_svcObj = new mcGDBServer();

                //1.�ж�����Դ��������,��Ϊ�Ǳ�������,��Ҫ�����û���������
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

                //2.��ȡ��ǰ����Դ���Ӱ������������ݿ�
                gdbIDs = m_svcObj.gdbs;
                if (gdbIDs == null) return;

                //gdbList.Items.Clear();
                //2.1.ȡ��һ����¼
                gdbID = gdbIDs.Reset();
                while (gdbID > 0)
                {
                    m_gdbObj = m_svcObj.get_gdb0(gdbID);
                    this.cbxNewDB.Items.Add(m_gdbObj.name);
                    this.cbsSourceDB.Items.Add(m_gdbObj.name);
                    //2.2ȡ��һ����¼
                    gdbID = gdbIDs.Next();
                }
            }
        }

        private void cbsSourceDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxSourceKQ.Items.Clear();
            int fclsID;
            mcEnumIDs fclsEnum;
            mcFClsInfo fclsInfo;//Ҫ�������Ϣ
            m_gdbObj = m_svcObj.get_gdb(this.cbsSourceDB.SelectedItem.ToString());
            if (m_gdbObj != null)
            {
                fclsEnum = m_gdbObj.get_xclses(meXClsType.meXSFCls, 0);
                //��긴λ
                fclsID = fclsEnum.Reset();
                //��fclsID�е�ID��Ӧ��Ҫ������һ�ӵ�myXFClsList��listbox�С�
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
            mcFClsInfo fclsInfo;//Ҫ�������Ϣ
            m_gdbObj = m_svcObj.get_gdb(this.cbxNewDB.SelectedItem.ToString());
            if (m_gdbObj != null)
            {
                fclsEnum = m_gdbObj.get_xclses(meXClsType.meXSFCls, 0);
                //��긴λ
                fclsID = fclsEnum.Reset();
                //��fclsID�е�ID��Ӧ��Ҫ������һ�ӵ�myXFClsList��listbox�С�
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
            //��������
            mcFields Fields = null;
            IVectorCls VecCls = null;

            //�򿪼�Ҫ����
            VecCls = m_gdbObj.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            VecCls.Open(this.cbxSourceKQ.SelectedItem.ToString(), 0);

            //ֱ��ȡ�������Խṹ
            VecCls.GetFields(out Fields);
            for (short i = 0; i < Fields.numbfield; i++)
            {
                this.cbxSourceField.Items.Add(Fields.get_fldEntry(i).fieldname);
            }
        }

        private void cbxNewKQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��������
            mcFields Fields = null;
            IVectorCls VecCls = null;

            //�򿪼�Ҫ����
            VecCls = m_gdbObj.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            VecCls.Open(this.cbxNewKQ.SelectedItem.ToString(), 0);

            //ֱ��ȡ�������Խṹ
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