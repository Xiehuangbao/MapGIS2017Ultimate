using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using mc_basObj7Lib;
using mc_basXcls7Lib;

namespace MapGIS2005
{
    public partial class GeoUpdateDlg : DevComponents.DotNetBar.Office2007Form
    {
        public AxWorkSpace.AxMxWorkSpace sourceWorkSpace;
        public AxWorkSpace.AxMxWorkSpace newWorkSpace;
        public GeoUpdateDlg()
        {
            InitializeComponent();
        }

        private void GeoUpdateDlg_Load(object sender, EventArgs e)
        {
            this.axMapXView1.WorkSpace = this.axMxWorkSpace1.ToInterface;
            this.axMapXView2.WorkSpace = this.axMxWorkSpace2.ToInterface;
        }


        private void btnOpenSorcePrj_Click(object sender, EventArgs e)
        {
            this.axMxWorkSpace1.Open("C:\\Users\\Kylin\\Desktop\\���Կ�������\\ԭʼ��������\\Testsource.Map", WorkSpace.EnumOpenMode.OpenNormal);
            this.axMapXView1.Restore();
            sourceWorkSpace = axMxWorkSpace1;
        }

        private void btnOpenNewPrj_Click(object sender, EventArgs e)
        {
            this.axMxWorkSpace2.Open("C:\\Users\\Kylin\\Desktop\\���Կ�������\\������\\Testsource.Map", WorkSpace.EnumOpenMode.OpenNormal);
            this.axMapXView2.Restore();
            newWorkSpace = axMxWorkSpace2;
        }
        public Dictionary<string, string> dict = new Dictionary<string, string>();
        public string DB;
        public string DB_source;
        public string KQ;
        public string KQ_source;
        public string Field;
        public string Field_source;

        private void btnGeoUpdate_Click(object sender, EventArgs e)
        {
            GeoUpdateParameterSetDlg aupDlg = new GeoUpdateParameterSetDlg(this);
            if (aupDlg.ShowDialog() == DialogResult.OK)
            {
                mcGDBServer GDBSvr = null;
                mcGDataBase GDB = null;
                mcGDataBase GDB_source = null;
                mcQueryDef QueryDef = null;
                mcQueryDef QueryDef_source = null;
                mcGeoPolygon GeoPolygon = null;
                mcGeoPolygon GeoPolygon_source = null;
                IGeometry pgeo = null;
                IGeometry pgeo_source = null;
                IVectorCls VecCls = null;
                IVectorCls VecCls_source = null;
                mcObjectID ID = null;
                mcObjectID ID_Source = null;
                IVectorCls VecCls_Add = null;

                GDBSvr = new mcGDBServer();
                QueryDef = new mcQueryDef();
                QueryDef_source = new mcQueryDef();
                GeoPolygon = new mcGeoPolygon();
                GeoPolygon_source = new mcGeoPolygon();
                //������ʼ��               
                ID = new mcObjectID();
                ID_Source = new mcObjectID();
                //��������Դ�������ݿ�
                try
                {
                    GDBSvr.Connect("MapGislocal", "", "");
                    GDB = GDBSvr.get_gdb(DB);
                    GDB_source = GDBSvr.get_gdb(DB_source);
                    VecCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                    VecCls_source = GDB_source.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                    VecCls.Open(KQ, 0);
                    VecCls_source.Open(KQ_source, 0);

                    ID.Int = 1;
                    VecCls.GetGeometry(ID, out pgeo);
                    GeoPolygon = (mcGeoPolygon)pgeo;
                    //�ռ��ѯģʽ����Ҫ������ ���ཻ����������ཻ����ȡ����룬������˼������������ĵ�
                    QueryDef.set_Spatial(GeoPolygon, meSpaQueryMode.meModeIntersect);

                    ID_Source.Int = 1;
                    VecCls_source.GetGeometry(ID_Source, out pgeo_source);
                    GeoPolygon_source = (mcGeoPolygon)pgeo_source;
                    //�ռ��ѯģʽ����Ҫ������ ���ཻ����������ཻ����ȡ����룬������˼������������ĵ�
                    QueryDef_source.set_Spatial(GeoPolygon_source, meSpaQueryMode.meModeIntersect);
                    string[] keys = new string[dict.Count];
                    dict.Keys.CopyTo(keys, 0);
                    foreach (string key in keys)
                    {
                        if ("".Equals(key))
                        {
                            mcQueryDef QDef = null;
                            mcRecordSet RecordSet = null;
                            QueryDef = new mcQueryDef();
                            IVectorCls VecCls_data = null;
                            //�򿪴���������
                            VecCls_data = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                            VecCls_data.Open(dict[key], 0);
                            VecCls_Add = GDB_source.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                            VecCls_Add.Create(dict[key], meGeomConstrainType.mefReg, 0, 0, null);
                            //�������Բ�ѯ����
                            QueryDef.Filter = "";
                            VecCls_data.Select(QDef, out RecordSet);
                            int rtn = VecCls_Add.CopySet(RecordSet);
                            VecCls_Add.Close();
                            VecCls_data.Close();
                            break;
                        }
                        SpatialDataAutoUpdate(GDBSvr, GDB, GDB_source, QueryDef, QueryDef_source, dict[key], key, Field, Field_source);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    VecCls.Close();
                    VecCls_source.Close();
                    GDBSvr.DisConnect();
                    GDB.Close();
                    GDB_source.Close();
                }
            }
        }

        private static void SpatialDataAutoUpdate(mcGDBServer GDBSvr, mcGDataBase GDB, mcGDataBase GDB_source,
    mcQueryDef QueryDef, mcQueryDef QueryDef_source, string data, string data_source, string field, string field_source)
        {
            //��������


            mcRecordSet RecordSet = null;
            mcRecordSet RecordSet_source = null;
            IVectorCls VecCls_data = null;
            IVectorCls VecCls_source_data = null;


            try
            {
                //�򿪴���������
                VecCls_data = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                VecCls_data.Open(data, 0);

                VecCls_source_data = GDB_source.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                VecCls_source_data.Open(data_source, 0);

                VecCls_data.Select(QueryDef, out RecordSet);

                VecCls_source_data.Select(QueryDef_source, out RecordSet_source);

                int SfcID = 0;
                SfcID = RecordSet.MoveFirst();
                while (!RecordSet.IsEOF())
                {
                    mcRecord record = null;
                    mcFields Fields = null;
                    IGeometry geo = null;
                    IGeomInfo geoInfo = null;
                    object val = null;

                    mcObjectID mID = new mcObjectID();

                    RecordSet.Get(out geo, out record, out geoInfo);
                    record.GetFields(out Fields);
                    record.GetFldVal(field, out val);

                    int SfcID_source = 0;
                    bool flag = true;
                    SfcID_source = RecordSet_source.MoveFirst();
                    while (!RecordSet_source.IsEOF())
                    {
                        mcObjectID id_source = new mcObjectID();
                        mcRecord record_source = null;
                        mcFields Fields_source = null;
                        IGeometry geo_source = null;
                        IGeomInfo geoInfo_source = null;
                        object val_source = null;

                        RecordSet_source.Get(out geo_source, out record_source, out geoInfo_source);
                        RecordSet_source.GetID(out id_source);
                        record_source.GetFields(out Fields_source);
                        record_source.GetFldVal(field_source, out val_source);
                        //�ҵ��¾������ж�Ӧ�Ŀ���
                        if (val.ToString().Equals(val_source.ToString()))
                        {
                            flag = false;
                            VecCls_source_data.Del(id_source);
                            VecCls_source_data.Append(geo, record, geoInfo);
                            //����ԭʼ���ݽ����  Ҫ���ᱨ��ָ�����
                            VecCls_source_data.Select(QueryDef, out RecordSet_source);
                            break;
                        }
                        SfcID_source = RecordSet_source.MoveNext();
                    }
                    //�ж��Ƿ����������� ��������ֱ�����
                    if (flag)
                    {
                        VecCls_source_data.Append(geo, record, geoInfo);
                        //����ԭʼ���ݽ����  Ҫ���ᱨ��ָ�����
                        VecCls_source_data.Select(QueryDef, out RecordSet_source);
                    }

                    SfcID = RecordSet.MoveNext();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("���³�������");
            }
            finally
            {
                //�ر��ࡢ���ݿ⡢�Ͽ�����Դ
                VecCls_data.Close();
                VecCls_source_data.Close();
            }
        }

    }
}