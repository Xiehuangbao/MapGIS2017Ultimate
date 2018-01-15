using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
using WorkSpace;
using System.Threading;
namespace MapGIS2005
{
    public partial class UpdateAnnotationDlg : DevComponents.DotNetBar.Office2007Form
    {
        public UpdateAnnotationDlg()
        {
            InitializeComponent();
        }

        private void UpdateAnnotationDlg_Load(object sender, EventArgs e)
        {

            axMapXView1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxDocTreeView1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxEditControl1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxEditControl1.EditView = axMapXView1.ToInterface;
            axMxEditControl1.AddGroupTool("����", "SymEditTools.SymEditToolBar.1");
            axMxEditControl1.AddGroupTool("��༭�˵�", "MainEdit.EditGroup.1");

            axMapXViewNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxDocTreeViewNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxEditControlNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxEditControlNew.EditView = axMapXViewNew.ToInterface;
            axMxEditControlNew.AddGroupTool("����", "SymEditTools.SymEditToolBar.1");

        }

        private void btnOpenSourceMPJ_Click(object sender, EventArgs e)
        {
            try
            {
                axMxWorkSpace1.Open("C:\\����ɽ��\\��-S430424003_����ɽǦп��˲���-Ǧ��\\NewGisDoc.Map", WorkSpace.EnumOpenMode.OpenNormal);
                mcDataSrcMng dsm = axMxWorkSpace1.DataSrcMng;
                mcMapGISEnv env = new mcMapGISEnv();
                env.cur = "C:\\����ɽ��\\��-S430424003_����ɽǦп��˲���-Ǧ��";
                env.slib = "C:\\5wslib";
                env.clib = "C:\\MapGIS K9 SP3\\Clib";
                env.temp = "C:\\MapGIS K9 SP3\\Temp";
                dsm.SetEnv(env);
                axMapXView1.Restore();
            }
            catch
            {
            }
        }

        private void btnOpenNewMPJ_Click(object sender, EventArgs e)
        {
            try
            {
                axMxWorkSpaceNew.Open("C:\\����ɽ��\\T01_0060\\T01_0060.MPJ", WorkSpace.EnumOpenMode.OpenNormal);
                mcDataSrcMng dsm = axMxWorkSpaceNew.DataSrcMng;
                mcMapGISEnv env = new mcMapGISEnv();
                env.cur = "C:\\����ɽ��\\T01_0060";
                env.slib = "C:\\mapgisNewLib";
                env.clib = "C:\\MapGIS K9 SP3\\Clib";
                env.temp = "C:\\MapGIS K9 SP3\\Temp";
                dsm.SetEnv(env);
                axMapXViewNew.Restore();
            }
            catch
            {
            }
        }

        private void axMapXView1_OnLDbClick(object sender, AxMapXView._IXViewEvents_OnLDbClickEvent e)
        {

            IXTransformation tran = null;
            IXDisplay display = null;
            display = axMapXView1.Display;
            tran = display.Transformation;
            double pmx = 0;
            double pmy = 0;
            tran.WpToLp(e.x, e.y, ref pmx, ref pmy);
            mc_basObj7Lib.mcDot dot = new mc_basObj7Lib.mcDot();
            dot.x = pmx + 38000000;
            dot.y = pmy;
        }


        #region ʵ�ֹ���������
        private void btnSetContext_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReSetEnvir", 1, 1);

        }

        private void btnSetNewContext_Click(object sender, EventArgs e)
        {
            axMxEditControlNew.StartTool("ReSetEnvir", 1, 1);
        }

        private void btnSaveLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SaveRegLayer", 1, 1);
        }

        private void btnSaveAnnoLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SaveRegLayer", 1, 1);
        }

        private void btnSavePointLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SavePntLayer", 1, 1);
        }

        private void btnSaveLineLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SaveLinLayer", 1, 1);
        }

        private void btnModifyRegLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyRegLayer", 1, 1);
        }

        private void btnModifyAnnLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyAnnLayer", 1, 1);
        }

        private void btnModifyPntLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyPntLayer", 1, 1);
        }

        private void btnModifyLineLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyLinLayer", 1, 1);
        }

        private void btnDelRegLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("DelRegLayer", 1, 1);
        }

        private void btnDelAnnLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("DelAnnLayer", 1, 1);
        }

        private void btnDelPntLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("DelPntLayer", 1, 1);
        }

        private void btnDelLineLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("DelLinLayer", 1, 1);
        }

        private void btnRenameLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("RenameLayer", 1, 1);
        }

        private void btnModifyCurRegLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyCurRegLayer", 1, 1);
        }

        private void btnModifyCurAnnLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyCurAnnLayer", 1, 1);
        }

        private void btnModifyCurPntLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyCurPntLayer", 1, 1);
        }

        private void btnModifyCurLineLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyCurLinLayer", 1, 1);
        }

        private void btnReplaceRegLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReplaceRegLayer", 1, 1);
        }

        private void btnReplaceAnnLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReplaceAnnLayer", 1, 1);
        }

        private void btnReplacePntLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReplacePntLayer", 1, 1);
        }

        private void btnReplaceLineLayer_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReplaceLinLayer", 1, 1);
        }

        private void btnSelectLine_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SelectLin", 1, 1);
        }

        private void btnModifyLineAttribute_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ModifyLineAttribute", 1, 1);
        }
        #endregion

        #region �����ݲ���
        //��ȡͼ��Բ
        string New_Lengend_Line_Name = "";
        mcDot Global_Lengend_CircleCentreCoor = new mcDot();
        private void btnExtractLengendCircle_Click(object sender, EventArgs e)
        {
            //  1���ҵ�����ͼ��
            mcRecordSet AllFeatures_RecordSet = MapGIsK9Utils.GetLayerRecordSet("T01_0060.WL", axMxWorkSpaceNew);
            List<double> mpLengths = new List<double>();
            AllFeatures_RecordSet.MoveFirst();
            while (!AllFeatures_RecordSet.IsEOF())
            {
                mcRecord record = null;
                AllFeatures_RecordSet.GetAtt(out record);
                object per = null;
                record.GetFldVal("mpLength", out per);
                mpLengths.Add((double)per);
                AllFeatures_RecordSet.MoveNext();
            }
            mpLengths.Sort();
            mcQueryDef lengthDef = new mcQueryDef();
            lengthDef.Filter = "mpLength=" + mpLengths[mpLengths.Count - 1];
            mcRecordSet frameRecordSet = null;
            AllFeatures_RecordSet.Select(lengthDef, out frameRecordSet);
            frameRecordSet.MoveFirst();
            IGeometry geoFrame = null;
            frameRecordSet.GetGeometry(out geoFrame);

            // 2���ҵ�ͼ��Բ
            IXGroupLayer m_Layer = null;
            m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            mcQueryDef queryDef = new mcQueryDef();
            //ͼ����������
            mcRect FrameRect = null;
            geoFrame.CalRect(out FrameRect);

            //���㡢����ͼ��Բ�����ĵ�����
            mcDot CircleDot = new mcDot();
            mcRect CircleRect = null;//���½�ͼ��Բ���������

            mcGeoPolygon queryPolygon = MapGIsK9Utils.GetRectPolygon(FrameRect);
            queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            mcRecordSet containCircle = null;
            mcRecordSet allCircle = null;
            vcls.Select(null, out allCircle);
            vcls.Select(queryDef, out containCircle);//ͼ���ڵ�Բ
            allCircle.SubSet(containCircle);//ȥ��ͼ���ڵ�Բ��Ҳ����ֻʣͼ�����һ��Բ
            allCircle.MoveFirst();
            while (!allCircle.IsEOF())
            {
                mcObjectID id = null;
                allCircle.GetID(out id);
                IGeomInfo geoInfo = null;
                allCircle.GetInfo(out geoInfo);
                IPntInfo pntInfo = geoInfo as IPntInfo;
                if (pntInfo.height > 20 && pntInfo.width > 20)
                {
                    IGeometry circle = null;
                    allCircle.GetGeometry(out circle);
                    circle.CalRect(out CircleRect);

                    //ȷ�����½�ͼ��Բ�����ĵ�����
                    CircleDot.x = (CircleRect.xmin + CircleRect.xmax) / 2;
                    CircleDot.y = (CircleRect.ymin + CircleRect.ymax) / 2;
                    Global_Lengend_CircleCentreCoor = CircleDot;//����ͼ�������ĵ�����

                    mcGDBServer GDBSvr = null;
                    mcGDataBase GDB = null;
                    mcSFeatureCls vecCls = new mcSFeatureCls();
                    GDBSvr = new mcGDBServer();
                    GDBSvr.Connect("MapGislocal", "", "");
                    GDB = GDBSvr.get_gdb("TEMPDATABASE");
                    vecCls = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
                    New_Lengend_Line_Name = "New_Lengend_Line" + new Random().Next();
                    vecCls.Create(New_Lengend_Line_Name, meGeomConstrainType.mefLin, 0, 0, null);

                    IResource Resource = axMxWorkSpaceNew.Resource;
                    IXMapSymbolLib MapSymbolLib = Resource.MapSymLib;

                    mcMapSymbol psySymbol = null;
                    meUnitSymbolType symbolType = meUnitSymbolType.meVectLine;
                    MapSymbolLib.GetSymbol(pntInfo.symID, meSymbolType.mePntSymbol, out psySymbol, out symbolType);

                    //��ȡ��ͼ��ÿ��Item
                    IXSymbolHead SymbolHead = psySymbol.SymbolHead;

                    for (short i = 0; i < SymbolHead.ItemNum; i++)
                    {
                        mcSymbolItem SymbolItem = null;
                        mcDots Dots = null;
                        String str = null;
                        psySymbol.GetItem(i, out SymbolItem, out Dots, out str);
                        switch (SymbolItem.ItemType)
                        {
                            case meSymbolItemType.mePolyLine:
                                mcGeoVarLine line = new mcGeoVarLine();
                                int r = 0;
                                Dots.Del(Dots.count - 1, ref r);
                                line.SetDots2D(Dots);
                                IGeometry geoAddLine = line as IGeometry;
                                vecCls.Append((IGeometry)line, null, null);
                                break;
                        }
                    }

                    //�ҵ���ͼԲ���������
                    mcRecordSet LineRecordSet = null;
                    vecCls.Select(null, out LineRecordSet);
                    mcRect NewCircleRect = null;//��ͼԲ���������
                    LineRecordSet.MoveFirst();
                    while (!LineRecordSet.IsEOF())
                    {
                        IGeometry geoLine = null;
                        LineRecordSet.GetGeometry(out geoLine);
                        mcGeoVarLine varline = geoLine as mcGeoVarLine;
                        mcDots lindeDots = null;
                        varline.Get2Dots(out lindeDots);
                        mcRect isRect = null;
                        geoLine.CalRect(out isRect);
                        if (lindeDots.count > 10)
                        {
                            NewCircleRect = isRect;
                            break;
                        }
                        LineRecordSet.MoveNext();
                    }
                    //���߽������Ų���
                    mcDot NewCircleDot = new mcDot();
                    NewCircleDot.x = (NewCircleRect.xmax + NewCircleRect.xmin) / 2;
                    NewCircleDot.y = (NewCircleRect.ymax + NewCircleRect.ymin) / 2;

                    double NewCircleRectWidth = NewCircleRect.xmax - NewCircleRect.xmin;
                    double NewCircleRectHeight = NewCircleRect.ymax - NewCircleRect.ymin;

                    double ratio = (pntInfo.height / 2) / NewCircleRectHeight;//�¾�Բ�����ű���

                    LineRecordSet.MoveFirst();
                    while (!LineRecordSet.IsEOF())
                    {
                        IGeometry geoLine = null;
                        LineRecordSet.GetGeometry(out geoLine);
                        mcGeoVarLine varline = geoLine as mcGeoVarLine;
                        mcDots lineDotsNew = new mcDots();
                        mcDots lindeDots = null;
                        varline.Get2Dots(out lindeDots);

                        for (int k = 0; k < lindeDots.count; k++)//ѭ����ÿ����������
                        {
                            mcDot NewDot = new mcDot();
                            NewDot.x = ratio * (lindeDots.get_item(k).x - NewCircleDot.x) + CircleDot.x;
                            NewDot.y = ratio * (lindeDots.get_item(k).y - NewCircleDot.y) + CircleDot.y;
                            lineDotsNew.Add(NewDot);
                        }

                        mcGeoVarLine addLine = new mcGeoVarLine();
                        addLine.SetDots2D(lineDotsNew);
                        vecCls.Append((IGeometry)addLine, null, null);
                        mcObjectID delID = null;
                        LineRecordSet.GetID(out delID);
                        vecCls.Del(delID);

                        LineRecordSet.MoveNext();
                    }
                    //���ͼ����ӻ�����
                    mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
                    this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
                    _sfLayer.XClass = (IBasCls)vecCls;
                    _sfLayer.LayerName = _sfLayer.LayerName;
                    this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
                    this.axMxWorkSpaceNew.Save();
                    this.axMapXViewNew.Restore();

                    vecCls.Close();
                    GDB.Close();
                    GDBSvr.DisConnect();
                }
                allCircle.MoveNext();
            }
            
        }



        //�ӳ��߲���
        private void btnExpandLine_Click(object sender, EventArgs e)
        {
            IXMapLayer m_Layer = null;
            mcRecordSet RecordSet = null;
            mcRect Rect = new mcRect();
            mcRecordSet RecordSetLine = null;
            List<mcObjectID> objIDs = new List<mcObjectID>();//ע��Բ�ڵ��߼���
            m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(New_Lengend_Line_Name) as IXMapLayer;
            IVectorCls vcls = m_Layer.XClass as IVectorCls;
            vcls.Select(null, out RecordSet);

            int SfcID = 0;
            SfcID = RecordSet.MoveFirst();
            //�ҵ�ע��Բ
            while (!RecordSet.IsEOF())
            {
                IGeometry GeoResult = null;
                RecordSet.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 0);
                double heigth = Math.Round(rect.ymax - rect.ymin, 0);
                mcGeoCir NewCircle = new mcGeoCirClass();
                mcDot center = new mcDot();
                center.x = (rect.xmax + rect.xmin) / 2;
                center.y = (rect.ymax + rect.ymin) / 2;
                NewCircle.Set(center, (width + heigth) / 4);
                //�ж��Ƿ���ע��Բ
                if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
                {
                    mcObjectID delCircleID = null;
                    RecordSet.GetID(out delCircleID);
                    mcObjectIDs delCircleIDs = new mcObjectIDs();
                    delCircleIDs.Append(delCircleID);
                    vcls.Select(null, out RecordSetLine);
                    //���»��������Բ����Ϊ֮ǰ��Բ�Ƕϵ�
                    IGeometry geoOldCircle = null;
                    mcGeoVarLine OldCircle = null;
                    RecordSet.GetGeometry(out geoOldCircle);
                    OldCircle = geoOldCircle as mcGeoVarLine;
                    mcGeoVarLine geoVarLine = new mcGeoVarLine();
                    mcDots OldCircleDots = null;
                    OldCircle.Get2Dots(out OldCircleDots);
                    for (int i = 0; i < OldCircleDots.count; i++)
                    {
                        mcDot dot = OldCircleDots.get_item(i);
                        geoVarLine.Append2D(dot);
                    }
                    geoVarLine.Append2D(OldCircleDots.get_item(0));

                    vcls.Append(geoVarLine, null, null);
                    vcls.Del(delCircleID);
                    //ȥ�������Բ��
                    RecordSetLine.SubSet2(delCircleIDs);
                    RecordSetLine.MoveFirst();
                    while (!RecordSetLine.IsEOF())
                    {
                        MapGIsK9Utils.ExpandLine(RecordSetLine, vcls);
                        RecordSetLine.MoveNext();
                    }
                    break;
                }
                SfcID = RecordSet.MoveNext();
            }
        }

        //��������β���
        string TempRegName = "";
        private void btnBuildPolygon_Click(object sender, EventArgs e)
        {
            IXMapLayer m_Layer = null;
            mcRecordSet RecordSet = null;
            m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(New_Lengend_Line_Name) as IXMapLayer;
            IVectorCls vcls = m_Layer.XClass as IVectorCls;
            vcls.Select(null, out RecordSet);

            mcGDBServer GDBSvr = null;
            mcGDataBase GDB = null;
            IVectorCls vecCls = null;
            IVectorCls _vecCls = null;
            GDBSvr = new mcGDBServer();
            GDBSvr.Connect("MapGislocal", "", "");
            GDB = GDBSvr.get_gdb("TEMPDATABASE");
            vecCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            _vecCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            vecCls.Create("NewTempLines" + new Random().Next(), meGeomConstrainType.mefLin, 0, 0, null);
            TempRegName = "NewTempReg" + new Random().Next();
            _vecCls.Create(TempRegName, meGeomConstrainType.mefReg, 0, 0, null);
            mcFields Fields = null;
            mcField Field = new mcField();
            _vecCls.GetFields(out Fields);
            //����Ҫ��ӵ��ֶ�,�ֱ���ÿ��С���ε�������ε�����ֵ
            Field.fieldname = "Xmin";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Ymin";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Xmax";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Ymax";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            //���ע�Ƕ�Ӧ�ı��
            Field.fieldname = "AnnoID";
            Field.msk_leng = 15;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            //�Լ�Ҫ������������
            _vecCls.SetFields(Fields);

            int SfcID = 0;
            SfcID = RecordSet.MoveFirst();
            //�ҵ�ע��Բ
            mcRect circleRect = new mcRect();//ע��Բ���������

            while (!RecordSet.IsEOF())
            {
                IGeometry GeoResult = null;
                RecordSet.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ҵ�ע��Բ
                if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
                {
                    circleRect = rect;
                }
                IGeoLine line = null;
                IGeometry geo = null;
                RecordSet.GetGeometry(out geo);
                line = geo as IGeoLine;
                vecCls.Append(line, null, null);
                SfcID = RecordSet.MoveNext();
            }

            mcSFeatureLayer sfLayer = new mcSFeatureLayer();
            this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
            sfLayer.XClass = (IBasCls)vecCls;
            sfLayer.LayerName = sfLayer.LayerName;
            this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)sfLayer);
            sfLayer.Active = true;

            //��������
            mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
            SpatialAnalysis.ClipArc((mcSFeatureCls)vecCls, null, null, null);
            SpatialAnalysis.TopoRegion((mcSFeatureCls)vecCls, null, (mcSFeatureCls)_vecCls, null);

            ////��ȡͼ���еı�ע
            IVectorCls anno_vcls = null;
            mcRecordSet TextAnno_RecordSet = null;//ͼ����Բ�������ڵı�ע����

            IXGroupLayer layer = null;
            layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            anno_vcls = layer.get_Layer(1).XClass as IVectorCls;
            mcQueryDef anno_def = new mcQueryDef();

            //�������ݲ���׼����Ҫ���������(circleRect)����ȷ����ȡ�����б�ע
            mcRect ExpandRect = new mcRect();
            ExpandRect.xmin = circleRect.xmin - 5;
            ExpandRect.ymin = circleRect.ymin - 5;
            ExpandRect.xmax = circleRect.xmax + 5;
            ExpandRect.ymax = circleRect.ymax + 5;
            anno_def.set_rect(ExpandRect, meSpaQueryMode.meModeContain);
            anno_vcls.Select(anno_def, out TextAnno_RecordSet);

            //�����������ɫ
            mcRecordSet RecordSetReg = null;
            //_vecCls�����������ļ�
            _vecCls.Select(null, out RecordSetReg);
            int regInt = 0;
            regInt = RecordSetReg.MoveFirst();
            while (!RecordSetReg.IsEOF())
            {
                IGeometry geoReg = null;
                IGeomInfo regInfo = null;
                mcObjectID regID = null;
                mcRecord record = null;
                RecordSetReg.GetID(out regID);
                _vecCls.Get(regID, out geoReg, out record, out regInfo);
                //�������ֶ����Ը�ֵ,��ȡͼ����עԲ�еı�ע
                mcRect tempRect = null;//Բ��ÿ��С�����������
                geoReg.CalRect(out tempRect);

                TextAnno_RecordSet.MoveFirst();
                while (!TextAnno_RecordSet.IsEOF())
                {
                    mcTextAnno local_TextAnno = null;
                    IGeometry geo = null;
                    TextAnno_RecordSet.GetGeometry(out geo);
                    local_TextAnno = geo as mcTextAnno;
                    mcDot AnchorDot = new mcDot();
                    AnchorDot = local_TextAnno.AnchorDot;
                    if (MapGIsK9Utils.isDotInRect(AnchorDot, tempRect))
                    {
                        record.SetFldVal("Xmin", tempRect.xmin);
                        record.SetFldVal("Xmax", tempRect.xmax);
                        record.SetFldVal("Ymin", tempRect.ymin);
                        record.SetFldVal("Ymax", tempRect.ymax);
                        record.SetFldVal("AnnoID", local_TextAnno.Text);
                        //�������ɫ
                        mcRegInfo mRegInfo = (mcRegInfo)regInfo;
                        //mRegInfo.fillclr = 0;

                        _vecCls.Append(geoReg, record, mRegInfo);
                        _vecCls.Del(regID);
                    }

                    TextAnno_RecordSet.MoveNext();
                }

                regInt = RecordSetReg.MoveNext();
            }

            mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
            this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
            _sfLayer.XClass = (IBasCls)_vecCls;
            _sfLayer.LayerName = _sfLayer.LayerName;
            this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            this.axMapXViewNew.Restore();
            this.axMxWorkSpaceNew.Save();

            vecCls.Close();
            _vecCls.Close();
            GDB.Close();
            GDBSvr.DisConnect();
        }


        //��ȡע����Ϣ
        private void btnExtractNewAnno_Click(object sender, EventArgs e)
        {
            NewAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();
            IXGroupLayer layer = null;
            IXMapLayer mapLayer = null;
            IVectorCls vcls = null;
            mcRecordSet lengend_Circle_Record = null;
            mcRecordSet anno_Recordset = null;
            //��ȡ��עԲ
            mapLayer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(New_Lengend_Line_Name) as IXMapLayer;
            vcls = mapLayer.XClass as IVectorCls;
            vcls.Select(null, out lengend_Circle_Record);
            layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            vcls = layer.get_Layer(1).XClass as IVectorCls;
            vcls.Select(null, out anno_Recordset);

            mcDot LengendCircleCenterCoor = MapGIsK9Utils.GetCircleCoor_New(lengend_Circle_Record).get_item(0);//��ȡͼ��Բ�����ĵ�����
            mcDots AnnoCircleDots = MapGIsK9Utils.GetAnnoCircleDots("T01_0060.WL", axMxWorkSpaceNew);//��עԲ��Բ�����꼯��

            //��ȡͼ��ͼ����ÿ��С��
            IXMapLayer regLayer = null;
            regLayer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(TempRegName);
            IVectorCls reg_vcls = regLayer.XClass as IVectorCls;
            mcRecordSet reg_RecordSet = null;
            reg_vcls.Select(null, out reg_RecordSet);
            //��עԲ�ڵı�ע����
            List<IGeometry> geoCircelList = null;
            List<IGeomInfo> geoCircleInfoList = null;
            MapGIsK9Utils.GetAnnoCircleGeoList("T01_0060.WL", out geoCircelList, out geoCircleInfoList, axMxWorkSpaceNew);
            List<mcRecordSet> AnnoInCircels = MapGIsK9Utils.GetCircleAnnosNew(geoCircelList, geoCircleInfoList, anno_Recordset);
            //������ע�е�ÿһ��Բ����ȡ԰�еı�ע��Ϣ
            for (int i = 0; i < AnnoCircleDots.count; i++)
            {
                //�����עԲ��ͼ����עԲ������ƫ��
                Dictionary<string, string> annoDic = new Dictionary<string, string>();
                mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
                double offsetX = AnnoCircleDot.x - LengendCircleCenterCoor.x;
                double offsetY = AnnoCircleDot.y - LengendCircleCenterCoor.y;

                int id = reg_RecordSet.MoveFirst();
                while (!reg_RecordSet.IsEOF())
                {
                    mcRecord record = null;
                    reg_RecordSet.GetAtt(out record);
                    object Xmin = null;
                    object Xmax = null;
                    object Ymin = null;
                    object Ymax = null;
                    object AnnoID = null;
                    record.GetFldVal("Xmin", out Xmin);
                    record.GetFldVal("Xmax", out Xmax);
                    record.GetFldVal("Ymin", out Ymin);
                    record.GetFldVal("Ymax", out Ymax);
                    record.GetFldVal("AnnoID", out AnnoID);

                    //�����עԲ��С���η�Χ
                    mcRect rect = new mcRect();
                    rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                    rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                    rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                    rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

                    //�ӱ�עԲ��Χ����ȡ��ע��Ϣ
                    mcQueryDef def = new mcQueryDef();
                    def.set_rect(rect, meSpaQueryMode.meModeContain);

                    int sID = anno_Recordset.MoveFirst();
                    while (!anno_Recordset.IsEOF())
                    {
                        IGeometry geo = null;
                        mcTextAnno TextAnno = null;
                        anno_Recordset.GetGeometry(out geo);
                        TextAnno = geo as mcTextAnno;
                        mcDot AnchorDot = new mcDot();
                        AnchorDot = TextAnno.AnchorDot;
                        if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                        {
                            annoDic.Add(AnnoID.ToString(), TextAnno.Text);
                        }
                        sID = anno_Recordset.MoveNext();
                    }

                    id = reg_RecordSet.MoveNext();
                }
                NewAnnoDictionSet.Add(AnnoCircleDots.get_item(i), annoDic);
            }
        }

        #endregion

        #region ԭʼ����ע����ȡ����

        //ԭʼ����,��ȡͼ��Բ������������Բ�ڵ��߽������Ӳ�����
        List<mcObjectID> Line_In_LengendCircle_ObjectIDs = null;
        string LineInLengendCircleName = "";
        mcDot legendCircleCoor = new mcDot();//ͼ��Բ�����ĵ�����
        private void btnExtranLengendCircleSource_Click(object sender, EventArgs e)
        {
            mcGDBServer GDBSvr = null;
            mcGDataBase GDB = null;
            GDBSvr = new mcGDBServer();
            GDBSvr.Connect("MapGislocal", "", "");
            GDB = GDBSvr.get_gdb("TEMPDATABASE");

            //1����ȡͼ��Բ
            Line_In_LengendCircle_ObjectIDs = new List<mcObjectID>();//ͼ��Բ�����е���ID����
            IXGroupLayer Lengend_Circle_Layer = null;
            Lengend_Circle_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HYBBC00204201_ͼ��.WL") as IXGroupLayer;
            IVectorCls Lengend_vcls = Lengend_Circle_Layer.get_Layer(1).XClass as IVectorCls;

            //����һ����ͼ��洢��Ҫ��
            IVectorCls Line_In_LegendCircleCls = null;
            Line_In_LegendCircleCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            LineInLengendCircleName = "LineInLengendCircle" + new Random().Next();
            Line_In_LegendCircleCls.Create(LineInLengendCircleName, meGeomConstrainType.mefLin, 0, 0, null);

            mcRecordSet Lengend_Line_In_CircleSet = null;
            mcRecordSet Lengend_RecordSet = null;
            Lengend_vcls.Select(null, out Lengend_RecordSet);
            Lengend_RecordSet.MoveFirst();
            //�ҵ�ע��Բ,���ҽ�ͼ��Բ�Լ�ͼ��Բ�е��߸��Ƶ���ͼ����
            mcRect rect = null;
            while (!Lengend_RecordSet.IsEOF())
            {
                IGeometry GeoResult = null;
                Lengend_RecordSet.GetGeometry(out GeoResult);     
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ж��Ƿ���ע��Բ
                if (width == heigth)
                {
                    //����ͼ��Բ�����ĵ�����
                    legendCircleCoor.x = (rect.xmax + rect.xmin) / 2;
                    legendCircleCoor.y = (rect.ymax + rect.ymin) / 2;
                    break;
                }
                Lengend_RecordSet.MoveNext();
            }

            if (rect != null)
            {
                mcQueryDef QueryDef = new mcQueryDef();
                QueryDef.set_rect(rect, meSpaQueryMode.meModeContain);
                Lengend_vcls.Select(QueryDef, out Lengend_Line_In_CircleSet);
            }

            Lengend_Line_In_CircleSet.MoveFirst();
            while(!Lengend_Line_In_CircleSet.IsEOF()){
                IGeometry geo = null;
                Lengend_Line_In_CircleSet.GetGeometry(out geo);
                Line_In_LegendCircleCls.Append(geo, null, null);
                Lengend_Line_In_CircleSet.MoveNext();
            }


            mcSFeatureLayer _sfLayer = new mcSFeatureLayer(); 
            this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            _sfLayer.XClass = (IBasCls)Line_In_LegendCircleCls;
            this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            this.axMapXView1.Restore();

            //�ҵ�ͼ��Բ�����е���
            //Line_In_LegendCircleCls.Select(null, out Lengend_Line_In_CircleSet);
            //Lengend_Line_In_CircleSet.MoveFirst();
            //while (!Lengend_Line_In_CircleSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    Lengend_Line_In_CircleSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width != heigth)
            //    {
            //        mcObjectID lineID = null;
            //        Lengend_Line_In_CircleSet.GetID(out lineID);
            //        Line_In_LengendCircle_ObjectIDs.Add(lineID);
            //    }
            //    Lengend_Line_In_CircleSet.MoveNext();
            //}

            //ѭ���ж��ߣ����ߵ����Ӵ���
            //List<mcObjectID> delObjects = new List<mcObjectID>();
            //for (int i = 0; i < Line_In_LengendCircle_ObjectIDs.Count; i++)
            //{
            //    //ȥ���жϹ�����
            //    if (!delObjects.Contains(Line_In_LengendCircle_ObjectIDs[i]))
            //    {
            //        mcDots dots = null;
            //        IGeometry geoLineInCire = null;
            //        Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[i], out geoLineInCire);
            //        IGeoLine lineInCire = geoLineInCire as IGeoLine;
            //        lineInCire.Get2Dots(out dots);
            //        //ѭ���ж��Ƿ�������������������
            //        for (int k = 0; k < dots.count; k++)
            //        {
            //            mcDot dot = null;
            //            dot = dots.get_item(k);
            //            for (int j = 0; j < Line_In_LengendCircle_ObjectIDs.Count; j++)
            //            {
            //                if (i != j && !delObjects.Contains(Line_In_LengendCircle_ObjectIDs[j]))
            //                {
            //                    mcDots _dots = null;
            //                    IGeometry _geoLineInCire = null;
            //                    Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[j], out _geoLineInCire);
            //                    IGeoLine _lineInCire = _geoLineInCire as IGeoLine;
            //                    _lineInCire.Get2Dots(out _dots);
            //                    for (int _k = 0; _k < _dots.count; _k++)
            //                    {
            //                        mcDot _dot = null;
            //                        _dot = _dots.get_item(_k);
            //                        //�ж��Ƿ�������ĵ��������������
            //                        double _x = Math.Abs(dot.x - _dot.x);
            //                        double _y = Math.Abs(dot.y - _dot.y);
            //                        //����������Ӿ����ӣ�����ɾ�������ӵ���
            //                        if (_x < 0.2 && _y < 0.2)
            //                        {
            //                            IGeoVarLine geoVarLine = lineInCire as IGeoVarLine;
            //                            //�������һ���ߵ�����
            //                            for (int pN = 0; pN < _dots.count; pN++)
            //                            {
            //                                geoVarLine.Append2D(_dots.get_item(pN));
            //                            }
            //                            Line_In_LegendCircleCls.Append(geoVarLine, null, null);
            //                            delObjects.Add(Line_In_LengendCircle_ObjectIDs[i]);
            //                            delObjects.Add(Line_In_LengendCircle_ObjectIDs[j]);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            ////ɾ�����ϲ�����
            //for (int di = 0; di < delObjects.Count; di++)
            //{
            //    Line_In_LegendCircleCls.Del(delObjects[di]);
            //}

            //mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
            //this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            //_sfLayer.XClass = (IBasCls)Line_In_LegendCircleCls;
            //_sfLayer.LayerName = _sfLayer.LayerName;
            //this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            //this.axMapXView1.Restore();

            //Line_In_LegendCircleCls.Close();
            //Lengend_vcls.Close();
            //GDB.Close();
            //GDBSvr.DisConnect();

            #region  
            //IXGroupLayer m_Layer = null;
            //List<mcObjectID> lineObjectIDs = new List<mcObjectID>();//ͼ��Բ�����е���ID����
            //mcRecordSet RecordSet = null;
            //mcRecordSet RecordSetLine = null;
            //m_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HYBBC00204201_ͼ��.WL") as IXGroupLayer;
            //IVectorCls vcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            //vcls.Select(null, out RecordSet);

            //RecordSet.MoveFirst();
            ////�ҵ�ע��Բ
            //while (!RecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    RecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width == heigth)
            //    {
            //        IGeometry geoLine = null;
            //        mcQueryDef QueryDef = new mcQueryDef();
            //        QueryDef.set_rect(rect, meSpaQueryMode.meModeContain);
            //        vcls.Select(QueryDef, out RecordSetLine);
            //        RecordSetLine.MoveFirst();
            //        //�ҵ�ע��Բ�е���
            //        while (!RecordSetLine.IsEOF())
            //        {
            //            //ȥ��Բ����
            //            mcObjectID circleID = null;
            //            RecordSet.GetID(out circleID);
            //            mcObjectID lineID = null;
            //            RecordSetLine.GetID(out lineID);
            //            if (circleID.Int != lineID.Int)
            //            {
            //                //ȥ��Բ�Ժ����е��ߣ��洢��lineObjectIDs��
            //                RecordSetLine.GetGeometry(out geoLine);
            //                IGeoLine line = geoLine as IGeoLine;
            //                lineObjectIDs.Add(lineID);
            //            }
            //            RecordSetLine.MoveNext();
            //        }
            //    }
            //    RecordSet.MoveNext();
            //}

            ////ѭ���ж��ߣ����ߵ����Ӵ���
            //List<mcObjectID> delObjects = new List<mcObjectID>();
            //for (int i = 0; i < lineObjectIDs.Count; i++)
            //{
            //    //ȥ���жϹ�����
            //    if (!delObjects.Contains(lineObjectIDs[i]))
            //    {
            //        mcDots dots = null;
            //        IGeometry geoLineInCire = null;
            //        vcls.GetGeometry(lineObjectIDs[i], out geoLineInCire);
            //        IGeoLine lineInCire = geoLineInCire as IGeoLine;
            //        lineInCire.Get2Dots(out dots);
            //        //ѭ���ж��Ƿ�������������������
            //        for (int k = 0; k < dots.count; k++)
            //        {
            //            mcDot dot = null;
            //            dot = dots.get_item(k);
            //            for (int j = 0; j < lineObjectIDs.Count; j++)
            //            {
            //                if (i != j && !delObjects.Contains(lineObjectIDs[j]))
            //                {
            //                    mcDots _dots = null;
            //                    IGeometry _geoLineInCire = null;
            //                    vcls.GetGeometry(lineObjectIDs[j], out _geoLineInCire);
            //                    IGeoLine _lineInCire = _geoLineInCire as IGeoLine;
            //                    _lineInCire.Get2Dots(out _dots);
            //                    for (int _k = 0; _k < _dots.count; _k++)
            //                    {
            //                        mcDot _dot = null;
            //                        _dot = _dots.get_item(_k);
            //                        //�ж��Ƿ�������ĵ��������������
            //                        double _x = Math.Abs(dot.x - _dot.x);
            //                        double _y = Math.Abs(dot.y - _dot.y);
            //                        //����������Ӿ����ӣ�����ɾ�������ӵ���
            //                        if (_x < 0.2 && _y < 0.2)
            //                        {
            //                            IGeoVarLine geoVarLine = lineInCire as IGeoVarLine;
            //                            //�������һ���ߵ�����
            //                            for (int pN = 0; pN < _dots.count; pN++)
            //                            {
            //                                geoVarLine.Append2D(_dots.get_item(pN));
            //                            }
            //                            vcls.Append(geoVarLine, null, null);
            //                            delObjects.Add(lineObjectIDs[i]);
            //                            delObjects.Add(lineObjectIDs[j]);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            ////ɾ�����ϲ�����
            //for (int di = 0; di < delObjects.Count; di++)
            //{
            //    vcls.Del(delObjects[di]);
            //}
            #endregion
        }

        //ԭʼ�����ӳ��߲���
        mcRect circleRect = null;
        private void btnExpandLineSource_Click(object sender, EventArgs e)
        {
            IXMapLayer Lengend_Circle_Layer = null;
            Lengend_Circle_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(LineInLengendCircleName) as IXMapLayer;
            IVectorCls Line_In_LegendCircleCls = Lengend_Circle_Layer.XClass as IVectorCls;
            //�ҵ�ͼ��Բ�����е���
            Line_In_LengendCircle_ObjectIDs.Clear();
            mcRecordSet Lengend_Line_In_CircleSet = null;
            Line_In_LegendCircleCls.Select(null, out Lengend_Line_In_CircleSet);
            Lengend_Line_In_CircleSet.MoveFirst();
            while (!Lengend_Line_In_CircleSet.IsEOF())
            {
                IGeometry GeoResult = null;
                Lengend_Line_In_CircleSet.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ж��Ƿ���ע��Բ
                if (width != heigth)
                {
                    mcObjectID lineID = null;
                    Lengend_Line_In_CircleSet.GetID(out lineID);
                    Line_In_LengendCircle_ObjectIDs.Add(lineID);
                }
                else
                {
                    circleRect = rect;
                }
                Lengend_Line_In_CircleSet.MoveNext();
            }
            //�ӳ���
            for (int i = 0; i < Line_In_LengendCircle_ObjectIDs.Count; i++)
            {
                IGeometry l_geoLine = null;
                Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[i], out l_geoLine);
                IGeoVarLine l_Line = null;
                l_Line = l_geoLine as IGeoVarLine;
                mcDots l_dots = null;
                l_Line.Get2Dots(out l_dots);
                mcDot dot_1 = l_dots.get_item(0);
                mcDot dot_2 = l_dots.get_item(l_dots.count - 1);

                //��ֱ(��Ϊ���ݲ���׼����������Ϊ< 1)
                if (Math.Abs(dot_1.x - dot_2.x) < 1)
                {
                    mcDot NewDot1 = new mcDot();
                    mcDot NewDot2 = new mcDot();
                    NewDot1.x = dot_1.x;
                    NewDot1.y = dot_1.y + l_Line.CalLength(null) / 10;
                    NewDot2.x = dot_2.x;
                    NewDot2.y = dot_2.y - l_Line.CalLength(null) / 10;

                    mcGeoVarLine NewLine = new mcGeoVarLine();

                    NewLine.Append2D(NewDot1);
                    NewLine.Append2D(NewDot2);
                    Line_In_LegendCircleCls.Append(NewLine, null, null);

                    Line_In_LegendCircleCls.Del(Line_In_LengendCircle_ObjectIDs[i]);
                }
                else
                {
                    //����ֱ�ߵ�б��
                    double slope = Math.Abs(dot_2.y - dot_1.y) / Math.Abs(dot_2.x - dot_1.x);
                    mcDot NewDot1 = new mcDot();
                    mcDot NewDot2 = new mcDot();

                    NewDot1.x = dot_1.x - l_Line.CalLength(null) / 10;
                    NewDot1.y = slope * (NewDot1.x - dot_1.x) + dot_1.y;

                    NewDot2.x = dot_2.x + l_Line.CalLength(null) / 10;
                    NewDot2.y = slope * (NewDot2.x - dot_2.x) + dot_2.y;

                    mcGeoVarLine NewLine = new mcGeoVarLine();
                    NewLine.Append2D(NewDot1);
                    NewLine.Append2D(NewDot2);
                    Line_In_LegendCircleCls.Append(NewLine, null, null);
                    Line_In_LegendCircleCls.Del(Line_In_LengendCircle_ObjectIDs[i]);
                }
            }
            Line_In_LegendCircleCls.Close();

            #region
            //IXGroupLayer m_Layer = null;
            //mcRecordSet RecordSet = null;
            //mcQueryDef QueryDef = new mcQueryDef();
            //mcRecordSet RecordSetLine = null;
            //List<mcObjectID> objIDs = new List<mcObjectID>();//ע��Բ�ڵ��߼���

            //m_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HYBBC00204201_ͼ��.WL") as IXGroupLayer;
            //IVectorCls vcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            //vcls.Select(null, out RecordSet);

            //int SfcID = 0;
            //SfcID = RecordSet.MoveFirst();
            ////�ҵ�ע��Բ
            //while (!RecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    RecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (Math.Abs(width - heigth) < 5)
            //    {
            //        QueryDef.set_rect(rect, meSpaQueryMode.meModeContain);
            //        vcls.Select(QueryDef, out RecordSetLine);
            //        int id = 0;
            //        id = RecordSetLine.MoveFirst();
            //        //�ҵ�ע��Բ�е���
            //        while (!RecordSetLine.IsEOF())
            //        {
            //            //ȥ��Բ����
            //            mcObjectID circleID = null;
            //            RecordSet.GetID(out circleID);
            //            mcObjectID lineID = null;
            //            RecordSetLine.GetID(out lineID);
            //            if (circleID.Int != lineID.Int)
            //            {
            //                objIDs.Add(lineID);

            //            }
            //            id = RecordSetLine.MoveNext();
            //        }
            //    }
            //    SfcID = RecordSet.MoveNext();
            //}

            ////�ӳ���
            //for (int i = 0; i < objIDs.Count; i++)
            //{
            //    IGeometry l_geoLine = null;
            //    vcls.GetGeometry(objIDs[i], out l_geoLine);
            //    IGeoVarLine l_Line = null;
            //    l_Line = l_geoLine as IGeoVarLine;
            //    mcDots l_dots = null;
            //    l_Line.Get2Dots(out l_dots);
            //    mcDot dot_1 = l_dots.get_item(0);
            //    mcDot dot_2 = l_dots.get_item(l_dots.count - 1);

            //    //��ֱ(��Ϊ���ݲ���׼����������Ϊ< 1)
            //    if (Math.Abs(dot_1.x - dot_2.x) < 1)
            //    {
            //        mcDot NewDot1 = new mcDot();
            //        mcDot NewDot2 = new mcDot();
            //        NewDot1.x = dot_1.x;
            //        NewDot1.y = dot_1.y + l_Line.CalLength(null) / 10;
            //        NewDot2.x = dot_2.x;
            //        NewDot2.y = dot_2.y - l_Line.CalLength(null) / 10;

            //        mcGeoVarLine NewLine = new mcGeoVarLine();

            //        NewLine.Append2D(NewDot1);
            //        NewLine.Append2D(NewDot2);
            //        vcls.Append(NewLine, null, null);

            //        vcls.Del(objIDs[i]);
            //    }
            //    else
            //    {
            //        //����ֱ�ߵ�б��
            //        double slope = Math.Abs(dot_2.y - dot_1.y) / Math.Abs(dot_2.x - dot_1.x);
            //        mcDot NewDot1 = new mcDot();
            //        mcDot NewDot2 = new mcDot();

            //        NewDot1.x = dot_1.x - l_Line.CalLength(null) / 10;
            //        NewDot1.y = slope * (NewDot1.x - dot_1.x) + dot_1.y;

            //        NewDot2.x = dot_2.x + l_Line.CalLength(null) / 10;
            //        NewDot2.y = slope * (NewDot2.x - dot_2.x) + dot_2.y;

            //        mcGeoVarLine NewLine = new mcGeoVarLine();

            //        NewLine.Append2D(NewDot1);
            //        NewLine.Append2D(NewDot2);
            //        vcls.Append(NewLine, null, null);

            //        vcls.Del(objIDs[i]);
            //    }
            //}
            #endregion
        }

        //Դ�������˹������
        string NewRegName = "";
        private void btnBulidPolygonSource_Click(object sender, EventArgs e)
        {
            //3�����˹���

            mcGDBServer GDBSvr = null;
            mcGDataBase GDB = null;
            GDBSvr = new mcGDBServer();
            GDBSvr.Connect("MapGislocal", "", "");
            GDB = GDBSvr.get_gdb("TEMPDATABASE");

            IVectorCls NewRegCls = null;
            NewRegCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            NewRegName = "�¹���ͼ��" + new Random().Next();
            NewRegCls.Create(NewRegName, meGeomConstrainType.mefReg, 0, 0, null);

            mcFields Fields = null;
            mcField Field = new mcField();
            NewRegCls.GetFields(out Fields);
            //����Ҫ��ӵ��ֶ�,�ֱ���ÿ��С���ε�������ε�����ֵ
            Field.fieldname = "Xmin";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Ymin";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Xmax";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            Field.fieldname = "Ymax";
            Field.msk_leng = 255;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            //���ע�Ƕ�Ӧ�ı��
            Field.fieldname = "AnnoID";
            Field.msk_leng = 15;
            Field.fieldtype = meFieldType.meFldStr;
            Fields.AppendFld(Field);
            //�Լ�Ҫ������������
            NewRegCls.SetFields(Fields);

            IXMapLayer layer1 = null;
            layer1 = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(LineInLengendCircleName) as IXMapLayer;
            IVectorCls Line_vcls = layer1.XClass as IVectorCls;

            //��������
            mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
            SpatialAnalysis.ClipArc((mcSFeatureCls)Line_vcls, null, null, null);
            SpatialAnalysis.TopoRegion((mcSFeatureCls)Line_vcls, null, (mcSFeatureCls)NewRegCls, null);

            //��ȡͼ���еı�ע
            IVectorCls anno_vcls = null;
            mcRecordSet TextAnno_RecordSet = null;//ͼ����Բ�������ڵı�ע����
            anno_vcls = MapGIsK9Utils.GetVectorCls("HYBBC00104201_ͼ��.WT", axMxWorkSpace1);
            mcQueryDef anno_def = new mcQueryDef();
            anno_def.set_rect(circleRect, meSpaQueryMode.meModeContain);
            anno_vcls.Select(anno_def, out TextAnno_RecordSet);


            //�����������ɫ
            mcRecordSet RecordSetReg = null;
            NewRegCls.Select(null, out RecordSetReg);
            RecordSetReg.MoveFirst();
            while (!RecordSetReg.IsEOF())
            {
                IGeometry geoReg = null;
                IGeomInfo regInfo = null;
                mcObjectID regID = null;
                mcRecord record = null;
                RecordSetReg.GetID(out regID);
                NewRegCls.Get(regID, out geoReg, out record, out regInfo);
                //�������ֶ����Ը�ֵ,��ȡͼ����עԲ�еı�ע
                mcRect tempRect = null;//Բ��ÿ��С�����������
                geoReg.CalRect(out tempRect);
                mcQueryDef localRegDef = new mcQueryDef();
                localRegDef.set_rect(tempRect, meSpaQueryMode.meModeContain);
                mcRecordSet localAnnoRecordSet = null;
                TextAnno_RecordSet.Select(localRegDef, out localAnnoRecordSet);
                localAnnoRecordSet.MoveFirst();
                IGeometry localResult = null;
                mcTextAnno local_TextAnno = null;
                localAnnoRecordSet.GetGeometry(out localResult);
                local_TextAnno = localResult as mcTextAnno;

                record.SetFldVal("Xmin", tempRect.xmin);
                record.SetFldVal("Xmax", tempRect.xmax);
                record.SetFldVal("Ymin", tempRect.ymin);
                record.SetFldVal("Ymax", tempRect.ymax);
                record.SetFldVal("AnnoID", local_TextAnno.Text);
                //�������ɫ
                mcRegInfo mRegInfo = (mcRegInfo)regInfo;
                //mRegInfo.fillclr = 0;

                NewRegCls.Append(geoReg, record, mRegInfo);
                NewRegCls.Del(regID);
                RecordSetReg.MoveNext();
            }

            mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
            this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            _sfLayer.XClass = (IBasCls)NewRegCls;
            _sfLayer.LayerName = _sfLayer.LayerName;
            this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            this.axMapXView1.Restore();

            NewRegCls.Close();
            GDB.Close();
            GDBSvr.DisConnect();

            #region
            //IXGroupLayer m_Layer = null;
            //mcRecordSet RecordSet = null;
            //mcQueryDef QueryDef = new mcQueryDef();
            //mcRecordSet RecordSetLine = null;

            //m_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HYBBC00204201_ͼ��.WL") as IXGroupLayer;
            //IVectorCls vcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            //vcls.Select(null, out RecordSet);

            //mcGDBServer GDBSvr = null;
            //mcGDataBase GDB = null;
            //IVectorCls vecCls = null;
            //IVectorCls _vecCls = null;
            //GDBSvr = new mcGDBServer();
            //GDBSvr.Connect("MapGislocal", "", "");
            //GDB = GDBSvr.get_gdb("TEMPDATABASE");
            //vecCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //_vecCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //int flag_id = vecCls.Create("������ͼ��" + new Random().Next(), meGeomConstrainType.mefLin, 0, 0, null);
            //SourceLayerName = "�¹���ͼ��" + new Random().Next();
            //_vecCls.Create(SourceLayerName, meGeomConstrainType.mefReg, 0, 0, null);

            //mcFields Fields = null;
            //mcField Field = new mcField();
            //_vecCls.GetFields(out Fields);
            ////����Ҫ��ӵ��ֶ�,�ֱ���ÿ��С���ε�������ε�����ֵ
            //Field.fieldname = "Xmin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Xmax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////���ע�Ƕ�Ӧ�ı��
            //Field.fieldname = "AnnoID";
            //Field.msk_leng = 15;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////�Լ�Ҫ������������
            //_vecCls.SetFields(Fields);

            //int SfcID = 0;
            //SfcID = RecordSet.MoveFirst();
            ////�ҵ�ע��Բ
            //mcRect circleRect = new mcRect();
            //while (!RecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    RecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width == heigth)
            //    {
            //        circleRect = rect;
            //        QueryDef.set_rect(rect, meSpaQueryMode.meModeIntersect);
            //        vcls.Select(QueryDef, out RecordSetLine);
            //        int id = 0;
            //        id = RecordSetLine.MoveFirst();
            //        //�ҵ�ע��Բ�е���
            //        while (!RecordSetLine.IsEOF())
            //        {
            //            IGeoLine line = null;
            //            IGeometry geo = null;
            //            RecordSetLine.GetGeometry(out geo);
            //            line = geo as IGeoLine;
            //            vecCls.Append(line, null, null);
            //            id = RecordSetLine.MoveNext();
            //        }
            //    }
            //    SfcID = RecordSet.MoveNext();
            //}

            //mcSFeatureLayer sfLayer = new mcSFeatureLayer();
            //this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            //sfLayer.XClass = (IBasCls)vecCls;
            //sfLayer.LayerName = sfLayer.LayerName;
            //this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)sfLayer);
            //sfLayer.Active = true;

            ////��������
            //mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
            //SpatialAnalysis.ClipArc((mcSFeatureCls)vecCls, null, null, null);
            //SpatialAnalysis.TopoRegion((mcSFeatureCls)vecCls, null, (mcSFeatureCls)_vecCls, null);

            ////��ȡͼ���еı�ע
            //IVectorCls anno_vcls = null;
            //mcRecordSet TextAnno_RecordSet = null;//ͼ����Բ�������ڵı�ע����
            //anno_vcls = MapGIsK9Utils.GetVectorCls("HYBBC00104201_ͼ��.WT", axMxWorkSpace1);
            //mcQueryDef anno_def = new mcQueryDef();
            //anno_def.set_rect(circleRect, meSpaQueryMode.meModeContain);
            //anno_vcls.Select(anno_def, out TextAnno_RecordSet);


            ////�����������ɫ
            //mcRecordSet RecordSetReg = null;
            //_vecCls.Select(null, out RecordSetReg);
            //int regInt = 0;
            //regInt = RecordSetReg.MoveFirst();
            //while (!RecordSetReg.IsEOF())
            //{
            //    IGeometry geoReg = null;
            //    IGeomInfo regInfo = null;
            //    mcObjectID regID = null;
            //    mcRecord record = null;
            //    RecordSetReg.GetID(out regID);
            //    _vecCls.Get(regID, out geoReg, out record, out regInfo);
            //    //�������ֶ����Ը�ֵ,��ȡͼ����עԲ�еı�ע
            //    mcRect tempRect = null;//Բ��ÿ��С�����������
            //    geoReg.CalRect(out tempRect);
            //    mcQueryDef localRegDef = new mcQueryDef();
            //    localRegDef.set_rect(tempRect, meSpaQueryMode.meModeContain);
            //    mcRecordSet localAnnoRecordSet = null;
            //    TextAnno_RecordSet.Select(localRegDef, out localAnnoRecordSet);
            //    localAnnoRecordSet.MoveFirst();
            //    IGeometry localResult = null;
            //    mcTextAnno local_TextAnno = null;
            //    localAnnoRecordSet.GetGeometry(out localResult);
            //    local_TextAnno = localResult as mcTextAnno;

            //    record.SetFldVal("Xmin", tempRect.xmin);
            //    record.SetFldVal("Xmax", tempRect.xmax);
            //    record.SetFldVal("Ymin", tempRect.ymin);
            //    record.SetFldVal("Ymax", tempRect.ymax);
            //    record.SetFldVal("AnnoID", local_TextAnno.Text);
            //    //�������ɫ
            //    mcRegInfo mRegInfo = (mcRegInfo)regInfo;
            //    //mRegInfo.fillclr = 0;

            //    _vecCls.Append(geoReg, record, mRegInfo);
            //    _vecCls.Del(regID);
            //    regInt = RecordSetReg.MoveNext();
            //}

            //mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
            //this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            //_sfLayer.XClass = (IBasCls)_vecCls;
            //_sfLayer.LayerName = _sfLayer.LayerName;
            //this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            //this.axMapXView1.Restore();
#endregion
        }

        //��ȡԭʼ���ݵ�ע����Ϣ
        private void btnExtractAnnoSource_Click(object sender, EventArgs e)
        {
            SourceAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();

            mcRecordSet anno_Circle_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0904201_��עԲ.WL", axMxWorkSpace1);
            mcDots AnnoCircleDots = MapGIsK9Utils.GetCircleCoor(anno_Circle_Recordset);
            mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0804201_��עԲ.WT", axMxWorkSpace1);
            mcRecordSet reg_RecordSet = null;

            IXMapLayer layer1 = null;
            layer1 = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(NewRegName) as IXMapLayer;
            IVectorCls Reg_vcls = layer1.XClass as IVectorCls;

            Reg_vcls.Select(null, out reg_RecordSet);
            //������ע�е�ÿһ��Բ����ȡ԰�еı�ע��Ϣ
            for (int i = 0; i < AnnoCircleDots.count; i++)
            {
                mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
                Dictionary<string, string> annoDic = new Dictionary<string, string>();
                //�����עԲ��ͼ����עԲ������ƫ��
                double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
                double offsetY = AnnoCircleDot.y - legendCircleCoor.y;

                reg_RecordSet.MoveFirst();
                while (!reg_RecordSet.IsEOF())
                {
                    mcRecord record = null;
                    reg_RecordSet.GetAtt(out record);
                    object Xmin = null;
                    object Xmax = null;
                    object Ymin = null;
                    object Ymax = null;
                    object AnnoID = null;
                    record.GetFldVal("Xmin", out Xmin);
                    record.GetFldVal("Xmax", out Xmax);
                    record.GetFldVal("Ymin", out Ymin);
                    record.GetFldVal("Ymax", out Ymax);
                    record.GetFldVal("AnnoID", out AnnoID);

                    //�����עԲ��С���η�Χ
                    mcRect rect = new mcRect();
                    rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                    rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                    rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                    rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

                    anno_Recordset.MoveFirst();
                    while (!anno_Recordset.IsEOF())
                    {
                        IGeometry geo = null;
                        mcTextAnno TextAnno = null;
                        anno_Recordset.GetGeometry(out geo);
                        TextAnno = geo as mcTextAnno;
                        mcDot AnchorDot = new mcDot();
                        AnchorDot = TextAnno.AnchorDot;
                        if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                        {
                            annoDic.Add(AnnoID.ToString(), TextAnno.Text);
                            break;
                        }
                        anno_Recordset.MoveNext();
                    }

                    reg_RecordSet.MoveNext();
                }
                SourceAnnoDictionSet.Add(AnnoCircleDot, annoDic);
            }

            #region
            //SourceAnnoList = new List<Dictionary<string, string>>();
            //mcRecordSet legend_Recordset = MapGIsK9Utils.GetLayerRecordSet("HYBBC00204201_ͼ��.WL", axMxWorkSpace1);
            //mcRecordSet anno_Circle_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0904201_��עԲ.WL", axMxWorkSpace1);
            //mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0804201_��עԲ.WT", axMxWorkSpace1);

            //mcDots LegendCircleDots = MapGIsK9Utils.GetCircleCoor(legend_Recordset);
            //mcDots AnnoCircleDots = MapGIsK9Utils.GetCircleCoor(anno_Circle_Recordset);
            //mcDot legendCircleCoor = LegendCircleDots.get_item(0);

            ////��ȡͼ��ͼ����ÿ��С��
            //IXMapLayer regLayer = null;
            //regLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(SourceLayerName);
            //IVectorCls reg_vcls = regLayer.XClass as IVectorCls;
            //mcRecordSet reg_RecordSet = null;
            //reg_vcls.Select(null, out reg_RecordSet);
            ////������ע�е�ÿһ��Բ����ȡ԰�еı�ע��Ϣ
            //for (int i = 0; i < AnnoCircleDots.count; i++)
            //{
            //    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
            //    Dictionary<string, string> annoDic = new Dictionary<string, string>();
            //    //�����עԲ��ͼ����עԲ������ƫ��
            //    double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
            //    double offsetY = AnnoCircleDot.y - legendCircleCoor.y;

            //    int id = reg_RecordSet.MoveFirst();
            //    while (!reg_RecordSet.IsEOF())
            //    {
            //        mcRecord record = null;
            //        reg_RecordSet.GetAtt(out record);
            //        object Xmin = null;
            //        object Xmax = null;
            //        object Ymin = null;
            //        object Ymax = null;
            //        object AnnoID = null;
            //        record.GetFldVal("Xmin", out Xmin);
            //        record.GetFldVal("Xmax", out Xmax);
            //        record.GetFldVal("Ymin", out Ymin);
            //        record.GetFldVal("Ymax", out Ymax);
            //        record.GetFldVal("AnnoID", out AnnoID);

            //        //�����עԲ��С���η�Χ
            //        mcRect rect = new mcRect();
            //        rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
            //        rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
            //        rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
            //        rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

            //        //�ӱ�עԲ��Χ����ȡ��ע��Ϣ
            //        mcQueryDef def = new mcQueryDef();
            //        def.set_rect(rect, meSpaQueryMode.meModeContain);

            //        int sID = anno_Recordset.MoveFirst();
            //        while (!anno_Recordset.IsEOF())
            //        {
            //            IGeometry geo = null;
            //            mcTextAnno TextAnno = null;
            //            anno_Recordset.GetGeometry(out geo);
            //            TextAnno = geo as mcTextAnno;
            //            mcDot AnchorDot = new mcDot();
            //            AnchorDot = TextAnno.AnchorDot;
            //            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
            //            {
            //                annoDic.Add(AnnoID.ToString(), TextAnno.Text);
            //            }
            //            sID = anno_Recordset.MoveNext();
            //        }

            //        id = reg_RecordSet.MoveNext();
            //    }
            //    SourceAnnoList.Add(annoDic);
            //}
            #endregion

        }

        #endregion

        //��������֮��ľ���
        private double CalDistanceOfCircle(mcDot dot1,mcDot dot2) {
            double dis = 0;
            dis = Math.Sqrt(Math.Pow(dot2.y - dot1.y, 2) + Math.Pow(dot2.x - dot1.x, 2));
            return dis;
        }



        //��ȡ��ע������
        public List<string> GetLengendAnno(mcRecordSet recordset)
        {
            List<string> Attr = new List<string>();
            recordset.MoveFirst();
            while (!recordset.IsEOF())
            {
                IGeometry legendAnoo_Result = null;
                mcTextAnno legendAnoo_TextAnno = null;
                recordset.GetGeometry(out legendAnoo_Result);
                legendAnoo_TextAnno = legendAnoo_Result as mcTextAnno;
                string pattern = @"\d+��";
                if (Regex.IsMatch(legendAnoo_TextAnno.Text, pattern))
                {
                    Attr.Add(legendAnoo_TextAnno.Text);

                }
                recordset.MoveNext();
            }
            return Attr;

        }

        //����DataGridView���������
        private void LoadDataGrideView(Dictionary<string, string> relationtable, List<string> sourceAttr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("��עԲ���");
            //��ӱ�ͷ
            for (int i = 0; i < sourceAttr.Count; i++)
            {
                dt.Columns.Add(sourceAttr[i].Split('��')[1]);
            }
            //�������ĵ����������Բ
            int BlockID = 1;
            foreach(KeyValuePair<mcDot,Dictionary<string,string>> sourceKVP in SourceAnnoDictionSet)
            {
                mcDot sourceDot = sourceKVP.Key;
                double minDis = double.MaxValue;
                Dictionary<string, string> newCircleAnnoDic = null;//��ԭʼ����Բ����������ݵ�Բ
                //���������ݼ��ϣ��ҵ���ԭʼ����Բ����ĵ�
                foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                {
                    mcDot newDot = newKVP.Key;
                    newDot.x = newDot.x - 38000000;
                    double distance = CalDistanceOfCircle(sourceDot, newDot);
                    if(minDis > distance)
                    {
                        minDis = distance;
                        newCircleAnnoDic = newKVP.Value;
                    }
                }

                //�ٸ����¾����ݶ�Ӧ��ϵ�������ݼ��ص�DataGridView��
                List<string> sourceRow = new List<string>();
                sourceRow.Add(BlockID.ToString());
                List<string> newRow = new List<string>();
                newRow.Add(BlockID.ToString());
                Dictionary<string, string> sourceCircleAnnoDic = sourceKVP.Value;
                List<string> sourceKeyList = new List<string>();
                foreach (KeyValuePair<string, string> sourceValueKVP in sourceCircleAnnoDic)
                {
                    
                    sourceKeyList.Add(sourceValueKVP.Key);

                }
                //�Լ��������򣬰�1��2��3��4����13��˳��
                sourceKeyList.Sort();
                for (int i = 0; i < sourceKeyList.Count; i++)
                {
                    string value = "";
                    sourceCircleAnnoDic.TryGetValue(sourceKeyList[i], out value);
                    sourceRow.Add(value);
                    string newKey = "";
                    relationtable.TryGetValue(sourceKeyList[i], out newKey);
                    string newValue = "";
                    newCircleAnnoDic.TryGetValue(newKey, out newValue);
                    newRow.Add(newValue);
                }
                dt.Rows.Add(sourceRow.ToArray());
                dt.Rows.Add(newRow.ToArray());
                BlockID++;
            }

            this.dataGridViewListAnno.DataSource = dt;
            this.dataGridViewListAnno.ColumnHeadersHeight = 40;
            this.dataGridViewListAnno.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewListAnno.MergeColumnNames.Add("��עԲ���");
            //this.dataGridViewListAnno.AddSpanHeader(2, 2, "XXXX");
        }
        //�������ݸ���
        private void btnAttrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> sourceAttr = new List<string>();
                List<string> newAttr = new List<string>();
                mcRecordSet legend_Record = MapGIsK9Utils.GetLayerRecordSet("HYBBC00104201_ͼ��.WT", axMxWorkSpace1);
                mcRecordSet legend_RecordSetNew = MapGIsK9Utils.GetLayerRecordSet("T01_0060.WT", axMxWorkSpaceNew);
                sourceAttr = GetLengendAnno(legend_Record);
                newAttr = GetLengendAnno(legend_RecordSetNew);

                //ȷ��ÿһ��Ķ�Ӧ��ϵ
                AttrMappingDlg amdlg = new AttrMappingDlg();
                amdlg.sourceAttr = sourceAttr;
                amdlg.newAttr = newAttr;
                Dictionary<string, string> tableRealation = null;
                if (amdlg.ShowDialog() == DialogResult.OK)
                {
                    tableRealation = amdlg.tableRealation;
                }

                //����DataGridView����
                LoadDataGrideView(tableRealation, sourceAttr);
                //��ʼ���£����������е�ͼ�߸��¾������е�ͼ��

                //��ȡԭʼ���ݵ�Ҫ������
                //IXGroupLayer sourceLayer = null;
                //sourceLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HJGABD0804201_��עԲ.WT") as IXGroupLayer;
                //IVectorCls sourceVcls = sourceLayer.get_Layer(1).XClass as IVectorCls;

                //mcRecordSet legend_Recordset = MapGIsK9Utils.GetLayerRecordSet("HYBBC00204201_ͼ��.WL", axMxWorkSpace1);
                //mcRecordSet anno_Circle_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0904201_��עԲ.WL", axMxWorkSpace1);
                //mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0804201_��עԲ.WT", axMxWorkSpace1);

                //mcDots LegendCircleDots = MapGIsK9Utils.GetCircleCoor(legend_Recordset);
                //mcDots AnnoCircleDots = MapGIsK9Utils.GetCircleCoor(anno_Circle_Recordset);
                //mcDot legendCircleCoor = LegendCircleDots.get_item(0);

                ////��ȡͼ��ͼ����ÿ��С��
                //IXMapLayer regLayer = null;
                //regLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("�¹���ͼ��2042655448");
                //IVectorCls reg_vcls = regLayer.XClass as IVectorCls;
                //mcRecordSet reg_RecordSet = null;
                //reg_vcls.Select(null, out reg_RecordSet);
                ////��עԲ�ڵı�ע����
                ////List<mcRecordSet> AnnoInCircels = GetCircleAnnos(anno_Circle_Recordset, anno_Recordset);
                ////������ע�е�ÿһ��Բ����ȡ԰�еı�ע��Ϣ
                //for (int i = 0; i < AnnoCircleDots.count; i++)
                //{
                //    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
                //    //�����עԲ��ͼ����עԲ������ƫ��
                //    double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
                //    double offsetY = AnnoCircleDot.y - legendCircleCoor.y;
                //    //�洢����ԭʼ����Բ��Ӧ��������Բ�е�����
                //    Dictionary<string, string> NewCireclDic = new Dictionary<string, string>();
                //    int id = reg_RecordSet.MoveFirst();
                //    while (!reg_RecordSet.IsEOF())
                //    {
                //        mcRecord record = null;
                //        reg_RecordSet.GetAtt(out record);
                //        object Xmin = null;
                //        object Xmax = null;
                //        object Ymin = null;
                //        object Ymax = null;
                //        object AnnoID = null;
                //        record.GetFldVal("Xmin", out Xmin);
                //        record.GetFldVal("Xmax", out Xmax);
                //        record.GetFldVal("Ymin", out Ymin);
                //        record.GetFldVal("Ymax", out Ymax);
                //        record.GetFldVal("AnnoID", out AnnoID);

                //        //�����עԲ��С���η�Χ
                //        mcRect rect = new mcRect();
                //        rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                //        rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                //        rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                //        rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

                //        //���ҵ���Ҫ����Բ�ı�ţ�Ϊ�˽��¾����ݵ�Բ���Ӧ
                //        int CircleID = 0;
                //        anno_Recordset.MoveFirst();
                //        while (!anno_Recordset.IsEOF())
                //        {
                //            IGeometry geo = null;
                //            mcTextAnno TextAnno = null;
                //            anno_Recordset.GetGeometry(out geo);
                //            TextAnno = geo as mcTextAnno;
                //            mcDot AnchorDot = new mcDot();
                //            AnchorDot = TextAnno.AnchorDot;
                //            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                //            {
                //                if (AnnoID.ToString().Equals("1"))
                //                {
                //                    string[] NumberStr = TextAnno.Text.Split('-');
                //                    CircleID = int.Parse(NumberStr[1]);
                //                    break;
                //                }
                //            }
                //            anno_Recordset.MoveNext();
                //        }

                //        //�ҵ���ԭʼ����ԲID��Ŷ�Ӧ��������

                //        for (int i_new = 0; i_new < NewAnnoList.Count; i_new++)
                //        {
                //            string tempValue = null;
                //            NewAnnoList[i_new].TryGetValue("1", out tempValue);
                //            if (tempValue.Split('-')[tempValue.Split('-').Length - 1].Equals(CircleID.ToString()))
                //            {
                //                NewCireclDic = NewAnnoList[i_new];
                //                break;
                //            }
                //        }

                //        int sID = anno_Recordset.MoveFirst();
                //        while (!anno_Recordset.IsEOF())
                //        {
                //            IGeometry geo = null;
                //            mcTextAnno TextAnno = null;
                //            anno_Recordset.GetGeometry(out geo);
                //            mcObjectID updataID = null;
                //            anno_Recordset.GetID(out updataID);
                //            TextAnno = geo as mcTextAnno;
                //            mcDot AnchorDot = new mcDot();
                //            AnchorDot = TextAnno.AnchorDot;
                //            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                //            {
                //                //�ȸ��ݶ�Ӧ��ϵ�ҵ�ԭʼ���ݶ�Ӧ�������ݱ��
                //                string NewID = null;
                //                tableRealation.TryGetValue(AnnoID.ToString(), out NewID);
                //                string updataData = null;
                //                NewCireclDic.TryGetValue(NewID, out updataData);
                //                //�滻����
                //                TextAnno.Text = updataData;
                //                IGeometry updateGeo = TextAnno as IGeometry;
                //                int s = sourceVcls.Update(updataID, updateGeo, null, null);
                //            }
                //            sID = anno_Recordset.MoveNext();
                //        }

                //        id = reg_RecordSet.MoveNext();
                //    }
                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //ͼ�����ݸ���
        private void btnGeoUpdate_Click(object sender, EventArgs e)
        {

        }


        #region ������ע��һ����ȡ����
        //һ����ȡ������ע����Ϣ
        Dictionary<mcDot, Dictionary<string, string>> NewAnnoDictionSet = null;//�洢�����±�עԲ�������Լ�ÿ��Բ�ڵ����б�ע��Ϣ
        private void btnExtractAnnoOnce_New_Click(object sender, EventArgs e)
        {
            btnExtractLengendCircle_Click(sender,e);
            btnExpandLine_Click(sender,e);
            btnBuildPolygon_Click(sender,e);
            btnExtractNewAnno_Click(sender,e);


            #region  
            //mcGDBServer GDBSvr = null;
            //mcGDataBase GDB = null;
            //GDBSvr = new mcGDBServer();
            //GDBSvr.Connect("MapGislocal", "", "");
            //GDB = GDBSvr.get_gdb("TEMPDATABASE");
            ////1����ȡͼ��Բ

            ////1.1�ҵ�����ͼ��
            //mcRecordSet AllFeatures_RecordSet = MapGIsK9Utils.GetLayerRecordSet("T01_0060.WL", axMxWorkSpaceNew);
            //List<double> mpLengths = new List<double>();
            //int sfID = 0;
            //sfID = AllFeatures_RecordSet.MoveFirst();
            //while (!AllFeatures_RecordSet.IsEOF())
            //{
            //    mcRecord record = null;
            //    AllFeatures_RecordSet.GetAtt(out record);
            //    object per = null;
            //    record.GetFldVal("mpLength", out per);
            //    mpLengths.Add((double)per);
            //    sfID = AllFeatures_RecordSet.MoveNext();
            //}
            //mpLengths.Sort();
            //mcQueryDef lengthDef = new mcQueryDef();
            //lengthDef.Filter = "mpLength=" + mpLengths[mpLengths.Count - 1];
            //mcRecordSet frameRecordSet = null;
            //AllFeatures_RecordSet.Select(lengthDef, out frameRecordSet);
            //frameRecordSet.MoveFirst();
            //IGeometry geoFrame = null;
            //frameRecordSet.GetGeometry(out geoFrame);

            //// 1.2���ҵ�ͼ��Բ
            //IXGroupLayer m_Layer = null;
            //m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            //IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            //mcQueryDef queryDef = new mcQueryDef();
            ////ͼ����������
            //mcRect FrameRect = null;
            //geoFrame.CalRect(out FrameRect);

            ////���㡢����ͼ��Բ�����ĵ�����
            //mcDot LengendCircleDot = new mcDot();
            //mcRect CircleRect = null;//���½�ͼ��Բ���������

            //mcGeoPolygon queryPolygon = MapGIsK9Utils.GetRectPolygon(FrameRect);

            //queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            //mcRecordSet containCircle = null;
            //mcRecordSet allCircle = null;
            //IPntInfo pntInfo = null;
            //vcls.Select(null, out allCircle);
            //vcls.Select(queryDef, out containCircle);//ͼ���ڵ�Բ
            //allCircle.SubSet(containCircle);//ȥ��ͼ���ڵ�Բ��Ҳ����ֻʣͼ�����һ��Բ
            ////��ͼ��Բ����
            //allCircle.MoveFirst();
            //while (!allCircle.IsEOF())
            //{
            //    mcObjectID id = null;
            //    allCircle.GetID(out id);
            //    IGeomInfo geoInfo = null;
            //    allCircle.GetInfo(out geoInfo);
            //    pntInfo = geoInfo as IPntInfo;//ͼ��Բ��ͼ����Ϣ
            //    if (pntInfo.height > 20 && pntInfo.width > 20)
            //    {
            //        IGeometry circle = null;
            //        allCircle.GetGeometry(out circle);
            //        circle.CalRect(out CircleRect);
            //        //ȷ�����½�ͼ��Բ�����ĵ�����
            //        LengendCircleDot.x = (CircleRect.xmin + CircleRect.xmax) / 2;
            //        LengendCircleDot.y = (CircleRect.ymin + CircleRect.ymax) / 2;
            //        Global_Lengend_CircleCentreCoor = LengendCircleDot;//����ͼ�������ĵ�����
            //        break;
            //    }
            //    allCircle.MoveNext();
            //}
            ////����ȡ��ͼ��Բ�е��ߴ洢��һ���µ���ͼ����
            //mcSFeatureCls ExtractLengendCircleLineCls = new mcSFeatureCls();//��ȡ��ͼ��Բ��ͼ��Ҫ��
            //ExtractLengendCircleLineCls = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
            //string symboleCircleLine = "SymbolCircleLine" + new Random().Next();//��ȡͼ��Բ�Ժ���ͼ��Բ�����ͼ��
            //ExtractLengendCircleLineCls.Create(symboleCircleLine, meGeomConstrainType.mefLin, 0, 0, null);
            ////��ȡ��ͼ��Ϣ
            //IResource Resource = axMxWorkSpaceNew.Resource;
            //IXMapSymbolLib MapSymbolLib = Resource.MapSymLib;
            //mcMapSymbol psySymbol = null;
            //meUnitSymbolType symbolType = meUnitSymbolType.meVectLine;
            //MapSymbolLib.GetSymbol(pntInfo.symID, meSymbolType.mePntSymbol, out psySymbol, out symbolType);
            ////��ȡ��ͼ��ÿ��Item
            //IXSymbolHead SymbolHead = psySymbol.SymbolHead;
            //for (short i = 0; i < SymbolHead.ItemNum; i++)
            //{
            //    mcSymbolItem SymbolItem = null;
            //    mcDots Dots = null;
            //    String str = null;
            //    psySymbol.GetItem(i, out SymbolItem, out Dots, out str);
            //    switch (SymbolItem.ItemType)
            //    {
            //        case meSymbolItemType.mePolyLine:
            //            mcGeoVarLine line = new mcGeoVarLine();
            //            int r = 0;
            //            Dots.Del(Dots.count - 1, ref r);
            //            line.SetDots2D(Dots);
            //            IGeometry geoAddLine = line as IGeometry;
            //            //����ͼ�е�����ӵ��µ���ͼ����
            //            ExtractLengendCircleLineCls.Append((IGeometry)line, null, null);
            //            break;
            //    }
            //}

            ////�ҵ���ͼԲ���������
            //mcRecordSet newLengendLineRecordSet = null;//������ͼ���е���Ҫ��
            //ExtractLengendCircleLineCls.Select(null, out newLengendLineRecordSet);
            //mcRect NewCircleRect = null;//��ͼԲ���������
            //newLengendLineRecordSet.MoveFirst();
            //while (!newLengendLineRecordSet.IsEOF())
            //{
            //    IGeometry geoLine = null;
            //    newLengendLineRecordSet.GetGeometry(out geoLine);
            //    mcGeoVarLine varline = geoLine as mcGeoVarLine;
            //    mcDots lindeDots = null;
            //    varline.Get2Dots(out lindeDots);
            //    mcRect isRect = null;
            //    geoLine.CalRect(out isRect);
            //    //�������еĵ�������ȷ���ǲ���Բ
            //    if (lindeDots.count > 10)
            //    {
            //        NewCircleRect = isRect;
            //        break;
            //    }
            //    newLengendLineRecordSet.MoveNext();
            //}


            ////���߽������Ų���,���»���Բ������Բ����
            //mcDot NewCircleDot = new mcDot();
            //NewCircleDot.x = (NewCircleRect.xmax + NewCircleRect.xmin) / 2;
            //NewCircleDot.y = (NewCircleRect.ymax + NewCircleRect.ymin) / 2;

            //double NewCircleRectWidth = NewCircleRect.xmax - NewCircleRect.xmin;
            //double NewCircleRectHeight = NewCircleRect.ymax - NewCircleRect.ymin;

            //double ratio = (pntInfo.height / 2) / NewCircleRectHeight;//�¾�Բ�����ű���
            ////�������ɵ���ͼ���е�Ҫ��ƽ�Ƶ���ȷλ��
            //newLengendLineRecordSet.MoveFirst();
            //while (!newLengendLineRecordSet.IsEOF())
            //{
            //    IGeometry geoLine = null;
            //    newLengendLineRecordSet.GetGeometry(out geoLine);
            //    mcGeoVarLine varline = geoLine as mcGeoVarLine;
            //    mcDots lineDotsNew = new mcDots();
            //    mcDots lindeDots = null;
            //    varline.Get2Dots(out lindeDots);

            //    for (int k = 0; k < lindeDots.count; k++)//ѭ����ÿ����������
            //    {
            //        mcDot NewDot = new mcDot();
            //        NewDot.x = ratio * (lindeDots.get_item(k).x - NewCircleDot.x) + LengendCircleDot.x;
            //        NewDot.y = ratio * (lindeDots.get_item(k).y - NewCircleDot.y) + LengendCircleDot.y;
            //        lineDotsNew.Add(NewDot);
            //    }

            //    mcGeoVarLine addLine = new mcGeoVarLine();
            //    addLine.SetDots2D(lineDotsNew);
            //    ExtractLengendCircleLineCls.Append((IGeometry)addLine, null, null);
            //    mcObjectID delID = null;
            //    newLengendLineRecordSet.GetID(out delID);
            //    ExtractLengendCircleLineCls.Del(delID);

            //    newLengendLineRecordSet.MoveNext();
            //}
            ////�������ɵ�ͼ��Բͼ����ӵ���ͼ��
            //mcSFeatureLayer newLengendCircleLineLayer = new mcSFeatureLayer();
            //this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
            //newLengendCircleLineLayer.XClass = (IBasCls)ExtractLengendCircleLineCls;
            //newLengendCircleLineLayer.LayerName = newLengendCircleLineLayer.LayerName;
            //this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)newLengendCircleLineLayer);
            //this.axMapXViewNew.Restore();


            ////2���������ɵ�ͼ��Բ�е��߽����ӳ�����
            //mcRecordSet RecordSet = null;//����ע��Բ���߼���
            //mcRecordSet ExpandLineRecordSet = null;//���ӳ��߼���
            //ExtractLengendCircleLineCls.Select(null, out RecordSet);
            //RecordSet.MoveFirst();
            ////�ҵ�ע��Բ
            //while (!RecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    RecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 0);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 0);
            //    //�ж��Ƿ���ע��Բ
            //    if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
            //    {
            //        mcObjectID delCircleID = null;
            //        RecordSet.GetID(out delCircleID);
            //        mcObjectIDs delCircleIDs = new mcObjectIDs();
            //        delCircleIDs.Append(delCircleID);
            //        ExtractLengendCircleLineCls.Select(null, out ExpandLineRecordSet);
            //        //���»��������Բ����Ϊ֮ǰ��Բ�Ƕϵ�
            //        IGeometry geoOldCircle = null;
            //        mcGeoVarLine OldCircle = null;
            //        RecordSet.GetGeometry(out geoOldCircle);
            //        OldCircle = geoOldCircle as mcGeoVarLine;
            //        mcGeoVarLine geoVarLine = new mcGeoVarLine();
            //        mcDots OldCircleDots = null;
            //        OldCircle.Get2Dots(out OldCircleDots);
            //        for (int i = 0; i < OldCircleDots.count; i++)
            //        {
            //            mcDot dot = OldCircleDots.get_item(i);
            //            geoVarLine.Append2D(dot);
            //        }
            //        geoVarLine.Append2D(OldCircleDots.get_item(0));

            //        ExtractLengendCircleLineCls.Append(geoVarLine, null, null);
            //        ExtractLengendCircleLineCls.Del(delCircleID);
            //        //ȥ�������Բ��
            //        ExpandLineRecordSet.SubSet2(delCircleIDs);
            //        ExpandLineRecordSet.MoveFirst();
            //        while (!ExpandLineRecordSet.IsEOF())
            //        {
            //            MapGIsK9Utils.ExpandLine(ExpandLineRecordSet, ExtractLengendCircleLineCls);
            //            ExpandLineRecordSet.MoveNext();
            //        }
            //        break;
            //    }
            //    RecordSet.MoveNext();
            //}


            ////3���������߽��й������
            //mcRecordSet NewLineFeatureRecordSet = null;
            //ExtractLengendCircleLineCls.Select(null, out NewLineFeatureRecordSet);

            //IVectorCls BreakLineCls = null;//����Ժ���Ҫ��ͼ��
            //IVectorCls NewRegCls = null;//��������Ҫ��ͼ��
            //BreakLineCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //NewRegCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //BreakLineCls.Create("NewBreakLines" + new Random().Next(), meGeomConstrainType.mefLin, 0, 0, null);
            //NewRegCls.Create("NewTempReg" + new Random().Next(), meGeomConstrainType.mefReg, 0, 0, null);
            //mcFields Fields = null;
            //mcField Field = new mcField();
            //NewRegCls.GetFields(out Fields);
            ////����Ҫ��ӵ��ֶ�,�ֱ���ÿ��С���ε�������ε�����ֵ
            //Field.fieldname = "Xmin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Xmax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////���ע�Ƕ�Ӧ�ı��
            //Field.fieldname = "AnnoID";
            //Field.msk_leng = 15;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////�Լ�Ҫ������������
            //NewRegCls.SetFields(Fields);

            //NewLineFeatureRecordSet.MoveFirst();
            ////�ҵ�ע��Բ
            //mcRect circleRect = new mcRect();//ע��Բ���������
            //while (!NewLineFeatureRecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    NewLineFeatureRecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ҵ�ע��Բ
            //    if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
            //    {
            //        circleRect = rect;
            //    }
            //    IGeoLine line = null;
            //    IGeometry geo = null;
            //    NewLineFeatureRecordSet.GetGeometry(out geo);
            //    line = geo as IGeoLine;
            //    BreakLineCls.Append(line, null, null);
            //    NewLineFeatureRecordSet.MoveNext();
            //}

            //mcSFeatureLayer sfLayer = new mcSFeatureLayer();
            //this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
            //sfLayer.XClass = (IBasCls)BreakLineCls;
            //sfLayer.LayerName = sfLayer.LayerName;
            //this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)sfLayer);
            //sfLayer.Active = true;

            ////��������
            //mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
            //SpatialAnalysis.ClipArc((mcSFeatureCls)BreakLineCls, null, null, null);
            //SpatialAnalysis.TopoRegion((mcSFeatureCls)BreakLineCls, null, (mcSFeatureCls)NewRegCls, null);

            //////��ȡͼ���еı�ע
            //IVectorCls anno_vcls = null;
            //mcRecordSet TextAnno_RecordSet = null;//ͼ����Բ�������ڵı�ע����

            //IXGroupLayer layer = null;
            //layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            //anno_vcls = layer.get_Layer(1).XClass as IVectorCls;
            //mcQueryDef anno_def = new mcQueryDef();

            ////�������ݲ���׼����Ҫ���������(circleRect)����ȷ����ȡ�����б�ע
            //mcRect ExpandRect = new mcRect();
            //ExpandRect.xmin = circleRect.xmin - 5;
            //ExpandRect.ymin = circleRect.ymin - 5;
            //ExpandRect.xmax = circleRect.xmax + 5;
            //ExpandRect.ymax = circleRect.ymax + 5;
            //anno_def.set_rect(ExpandRect, meSpaQueryMode.meModeContain);
            //anno_vcls.Select(anno_def, out TextAnno_RecordSet);

            //mcRecordSet RecordSetReg = null;
            ////NewRegCls�����������ļ�
            //NewRegCls.Select(null, out RecordSetReg);
            //RecordSetReg.MoveFirst();
            //while (!RecordSetReg.IsEOF())
            //{
            //    IGeometry geoReg = null;
            //    IGeomInfo regInfo = null;
            //    mcObjectID regID = null;
            //    mcRecord record = null;
            //    RecordSetReg.GetID(out regID);
            //    NewRegCls.Get(regID, out geoReg, out record, out regInfo);
            //    //�������ֶ����Ը�ֵ,��ȡͼ����עԲ�еı�ע
            //    mcRect tempRect = null;//Բ��ÿ��С�����������
            //    geoReg.CalRect(out tempRect);

            //    TextAnno_RecordSet.MoveFirst();
            //    while (!TextAnno_RecordSet.IsEOF())
            //    {
            //        mcTextAnno local_TextAnno = null;
            //        IGeometry geo = null;
            //        TextAnno_RecordSet.GetGeometry(out geo);
            //        local_TextAnno = geo as mcTextAnno;
            //        mcDot AnchorDot = new mcDot();
            //        AnchorDot = local_TextAnno.AnchorDot;
            //        if (MapGIsK9Utils.isDotInRect(AnchorDot, tempRect))
            //        {
            //            record.SetFldVal("Xmin", tempRect.xmin);
            //            record.SetFldVal("Xmax", tempRect.xmax);
            //            record.SetFldVal("Ymin", tempRect.ymin);
            //            record.SetFldVal("Ymax", tempRect.ymax);
            //            record.SetFldVal("AnnoID", local_TextAnno.Text);
            //            NewRegCls.Append(geoReg, record, regInfo);
            //            NewRegCls.Del(regID);
            //        }

            //        TextAnno_RecordSet.MoveNext();
            //    }

            //    RecordSetReg.MoveNext();
            //}

            //mcSFeatureLayer _sfLayer = new mcSFeatureLayer();
            //this.axMxWorkSpaceNew.ActiveMap = this.axMxWorkSpaceNew.MapCollection.get_Item(0);
            //_sfLayer.XClass = (IBasCls)NewRegCls;
            //_sfLayer.LayerName = _sfLayer.LayerName;
            //this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer((IXMapLayer)_sfLayer);
            ////this.axMxWorkSpaceNew.Save();

            ////4����ȡע����Ϣ
            //NewAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();
            //mcRecordSet anno_Recordset = null;
            //anno_vcls.Select(null, out anno_Recordset);

            //mcDot LengendCircleCenterCoor = Global_Lengend_CircleCentreCoor;//��ȡͼ��Բ�����ĵ�����
            //mcDots AnnoCircleDots = MapGIsK9Utils.GetAnnoCircleDots("T01_0060.WL", axMxWorkSpaceNew);//��עԲ��Բ�����꼯��

            ////��ȡͼ��ͼ����ÿ��С��
            //mcRecordSet reg_RecordSet = null;
            //NewRegCls.Select(null, out reg_RecordSet);
            //for (int i = 0; i < AnnoCircleDots.count; i++)
            //{
            //    //�����עԲ��ͼ����עԲ������ƫ��
            //    Dictionary<string, string> annoDic = new Dictionary<string, string>();
            //    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
            //    double offsetX = AnnoCircleDot.x - LengendCircleCenterCoor.x;
            //    double offsetY = AnnoCircleDot.y - LengendCircleCenterCoor.y;

            //    int id = reg_RecordSet.MoveFirst();
            //    while (!reg_RecordSet.IsEOF())
            //    {
            //        mcRecord record = null;
            //        reg_RecordSet.GetAtt(out record);
            //        object Xmin = null;
            //        object Xmax = null;
            //        object Ymin = null;
            //        object Ymax = null;
            //        object AnnoID = null;
            //        record.GetFldVal("Xmin", out Xmin);
            //        record.GetFldVal("Xmax", out Xmax);
            //        record.GetFldVal("Ymin", out Ymin);
            //        record.GetFldVal("Ymax", out Ymax);
            //        record.GetFldVal("AnnoID", out AnnoID);

            //        //�����עԲ��С���η�Χ
            //        mcRect rect = new mcRect();
            //        try
            //        {
            //            rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
            //            rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
            //            rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
            //            rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

            //            //�ӱ�עԲ��Χ����ȡ��ע��Ϣ
            //            mcQueryDef def = new mcQueryDef();
            //            def.set_rect(rect, meSpaQueryMode.meModeContain);

            //            int sID = anno_Recordset.MoveFirst();
            //            while (!anno_Recordset.IsEOF())
            //            {
            //                IGeometry geo = null;
            //                mcTextAnno TextAnno = null;
            //                anno_Recordset.GetGeometry(out geo);
            //                TextAnno = geo as mcTextAnno;
            //                mcDot AnchorDot = new mcDot();
            //                AnchorDot = TextAnno.AnchorDot;
            //                if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
            //                {
            //                    annoDic.Add(AnnoID.ToString(), TextAnno.Text);
            //                }
            //                sID = anno_Recordset.MoveNext();
            //            }

            //            id = reg_RecordSet.MoveNext();
            //        }
            //        catch
            //        {
            //            MessageBox.Show("����ע��Ϣ�Ƿ�淶������ע�Ƿ���ȷ������ڣ�");
            //        }

            //    }
            //    NewAnnoDictionSet.Add(AnnoCircleDots.get_item(i), annoDic);
            //}
            #endregion  

        }

        #endregion

        #region ԭʼ����ע��һ����ȡ����
        //ԭʼ����ע��һ����ȡ����
        Dictionary<mcDot, Dictionary<string, string>> SourceAnnoDictionSet = null;//�洢�����±�עԲ�������Լ�ÿ��Բ�ڵ����б�ע��Ϣ
        private void btnExtractAnnoOnceSource_Click(object sender, EventArgs e)
        {
            btnExtranLengendCircleSource_Click(sender,e);
            //btnExpandLineSource_Click(sender,e);
            //btnBulidPolygonSource_Click(sender,e);
            //btnExtractAnnoSource_Click(sender,e);



            //mcGDBServer GDBSvr = null;
            //mcGDataBase GDB = null;
            //mcRect circleRect = null;
            //mcDot legendCircleCoor = new mcDot();//ͼ��Բ�����ĵ�����
            //GDBSvr = new mcGDBServer();
            //GDBSvr.Connect("MapGislocal", "", "");
            //GDB = GDBSvr.get_gdb("TEMPDATABASE");
            ////1����ȡͼ��Բ
            //List<mcObjectID> Line_In_LengendCircle_ObjectIDs = new List<mcObjectID>();//ͼ��Բ�����е���ID����
            //IXGroupLayer Lengend_Circle_Layer = null;
            //Lengend_Circle_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2("HYBBC00204201_ͼ��.WL") as IXGroupLayer;
            //IVectorCls Lengend_vcls = Lengend_Circle_Layer.get_Layer(1).XClass as IVectorCls;
            ////����һ����ͼ��洢��Ҫ��
            //IVectorCls Line_In_LegendCircleCls = null;
            //Line_In_LegendCircleCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //mcFields fields = null;
            //Lengend_vcls.GetFields(out fields);
            //Line_In_LegendCircleCls.Create("LineInLengendCircle" + new Random().Next(), meGeomConstrainType.mefLin, 0, Lengend_vcls.srID, fields);

            //mcRecordSet Lengend_Line_In_CircleSet = null;
            //mcRecordSet Lengend_RecordSet = null;
            //Lengend_vcls.Select(null, out Lengend_RecordSet);
            //Lengend_RecordSet.MoveFirst();
            ////�ҵ�ע��Բ,���ҽ�ͼ��Բ�Լ�ͼ��Բ�е��߸��Ƶ���ͼ����
            //while (!Lengend_RecordSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    Lengend_RecordSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width == heigth)
            //    {
            //        //����ͼ��Բ�����ĵ�����
            //        legendCircleCoor.x = (rect.xmax + rect.xmin) / 2;
            //        legendCircleCoor.y = (rect.ymax + rect.ymin) / 2;
            //        mcQueryDef QueryDef = new mcQueryDef();
            //        QueryDef.set_rect(rect, meSpaQueryMode.meModeContain);
            //        Lengend_vcls.Select(QueryDef, out Lengend_Line_In_CircleSet);
            //        Line_In_LegendCircleCls.AddSet(Lengend_Line_In_CircleSet);
            //        break;
            //    }
            //    Lengend_RecordSet.MoveNext();
            //}

            //this.axMxWorkSpace1.Save();

            ////�ҵ�ͼ��Բ�����е���
            //Line_In_LegendCircleCls.Select(null, out Lengend_Line_In_CircleSet);
            //Lengend_Line_In_CircleSet.MoveFirst();
            //while (!Lengend_Line_In_CircleSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    Lengend_Line_In_CircleSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width != heigth)
            //    {
            //        mcObjectID lineID = null;
            //        Lengend_Line_In_CircleSet.GetID(out lineID);
            //        Line_In_LengendCircle_ObjectIDs.Add(lineID);
            //    }
            //    Lengend_Line_In_CircleSet.MoveNext();
            //}

            ////ѭ���ж��ߣ����ߵ����Ӵ���
            //List<mcObjectID> delObjects = new List<mcObjectID>();
            //for (int i = 0; i < Line_In_LengendCircle_ObjectIDs.Count; i++)
            //{
            //    //ȥ���жϹ�����
            //    if (!delObjects.Contains(Line_In_LengendCircle_ObjectIDs[i]))
            //    {
            //        mcDots dots = null;
            //        IGeometry geoLineInCire = null;
            //        Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[i], out geoLineInCire);
            //        IGeoLine lineInCire = geoLineInCire as IGeoLine;
            //        lineInCire.Get2Dots(out dots);
            //        //ѭ���ж��Ƿ�������������������
            //        for (int k = 0; k < dots.count; k++)
            //        {
            //            mcDot dot = null;
            //            dot = dots.get_item(k);
            //            for (int j = 0; j < Line_In_LengendCircle_ObjectIDs.Count; j++)
            //            {
            //                if (i != j && !delObjects.Contains(Line_In_LengendCircle_ObjectIDs[j]))
            //                {
            //                    mcDots _dots = null;
            //                    IGeometry _geoLineInCire = null;
            //                    Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[j], out _geoLineInCire);
            //                    IGeoLine _lineInCire = _geoLineInCire as IGeoLine;
            //                    _lineInCire.Get2Dots(out _dots);
            //                    for (int _k = 0; _k < _dots.count; _k++)
            //                    {
            //                        mcDot _dot = null;
            //                        _dot = _dots.get_item(_k);
            //                        //�ж��Ƿ�������ĵ��������������
            //                        double _x = Math.Abs(dot.x - _dot.x);
            //                        double _y = Math.Abs(dot.y - _dot.y);
            //                        //����������Ӿ����ӣ�����ɾ�������ӵ���
            //                        if (_x < 0.2 && _y < 0.2)
            //                        {
            //                            IGeoVarLine geoVarLine = lineInCire as IGeoVarLine;
            //                            //�������һ���ߵ�����
            //                            for (int pN = 0; pN < _dots.count; pN++)
            //                            {
            //                                geoVarLine.Append2D(_dots.get_item(pN));
            //                            }
            //                            Line_In_LegendCircleCls.Append(geoVarLine, null, null);
            //                            delObjects.Add(Line_In_LengendCircle_ObjectIDs[i]);
            //                            delObjects.Add(Line_In_LengendCircle_ObjectIDs[j]);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            ////ɾ�����ϲ�����
            //for (int di = 0; di < delObjects.Count; di++)
            //{
            //    Line_In_LegendCircleCls.Del(delObjects[di]);
            //}

            ////2���ӳ���
            ////�ҵ�ͼ��Բ�����е���
            //Line_In_LengendCircle_ObjectIDs.Clear();
            //Line_In_LegendCircleCls.Select(null, out Lengend_Line_In_CircleSet);
            //Lengend_Line_In_CircleSet.MoveFirst();
            //while (!Lengend_Line_In_CircleSet.IsEOF())
            //{
            //    IGeometry GeoResult = null;
            //    Lengend_Line_In_CircleSet.GetGeometry(out GeoResult);
            //    mcRect rect = null;
            //    GeoResult.CalRect(out rect);
            //    double width = Math.Round(rect.xmax - rect.xmin, 2);
            //    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
            //    //�ж��Ƿ���ע��Բ
            //    if (width != heigth)
            //    {
            //        mcObjectID lineID = null;
            //        Lengend_Line_In_CircleSet.GetID(out lineID);
            //        Line_In_LengendCircle_ObjectIDs.Add(lineID);
            //    }
            //    else
            //    {
            //        circleRect = rect;
            //    }
            //    Lengend_Line_In_CircleSet.MoveNext();
            //}
            ////�ӳ���
            //for (int i = 0; i < Line_In_LengendCircle_ObjectIDs.Count; i++)
            //{
            //    IGeometry l_geoLine = null;
            //    Line_In_LegendCircleCls.GetGeometry(Line_In_LengendCircle_ObjectIDs[i], out l_geoLine);
            //    IGeoVarLine l_Line = null;
            //    l_Line = l_geoLine as IGeoVarLine;
            //    mcDots l_dots = null;
            //    l_Line.Get2Dots(out l_dots);
            //    mcDot dot_1 = l_dots.get_item(0);
            //    mcDot dot_2 = l_dots.get_item(l_dots.count - 1);

            //    //��ֱ(��Ϊ���ݲ���׼����������Ϊ< 1)
            //    if (Math.Abs(dot_1.x - dot_2.x) < 1)
            //    {
            //        mcDot NewDot1 = new mcDot();
            //        mcDot NewDot2 = new mcDot();
            //        NewDot1.x = dot_1.x;
            //        NewDot1.y = dot_1.y + l_Line.CalLength(null) / 10;
            //        NewDot2.x = dot_2.x;
            //        NewDot2.y = dot_2.y - l_Line.CalLength(null) / 10;

            //        mcGeoVarLine NewLine = new mcGeoVarLine();

            //        NewLine.Append2D(NewDot1);
            //        NewLine.Append2D(NewDot2);
            //        Line_In_LegendCircleCls.Append(NewLine, null, null);

            //        Line_In_LegendCircleCls.Del(Line_In_LengendCircle_ObjectIDs[i]);
            //    }
            //    else
            //    {
            //        //����ֱ�ߵ�б��
            //        double slope = Math.Abs(dot_2.y - dot_1.y) / Math.Abs(dot_2.x - dot_1.x);
            //        mcDot NewDot1 = new mcDot();
            //        mcDot NewDot2 = new mcDot();

            //        NewDot1.x = dot_1.x - l_Line.CalLength(null) / 10;
            //        NewDot1.y = slope * (NewDot1.x - dot_1.x) + dot_1.y;

            //        NewDot2.x = dot_2.x + l_Line.CalLength(null) / 10;
            //        NewDot2.y = slope * (NewDot2.x - dot_2.x) + dot_2.y;

            //        mcGeoVarLine NewLine = new mcGeoVarLine();
            //        NewLine.Append2D(NewDot1);
            //        NewLine.Append2D(NewDot2);
            //        Line_In_LegendCircleCls.Append(NewLine, null, null);
            //        Line_In_LegendCircleCls.Del(Line_In_LengendCircle_ObjectIDs[i]);
            //    }
            //}

            ////3�����˹���
            //IVectorCls NewRegCls = null;
            //NewRegCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            //string NewRegName = "�¹���ͼ��" + new Random().Next();
            //NewRegCls.Create(NewRegName, meGeomConstrainType.mefReg, 0, 0, null);

            //mcFields Fields = null;
            //mcField Field = new mcField();
            //NewRegCls.GetFields(out Fields);
            ////����Ҫ��ӵ��ֶ�,�ֱ���ÿ��С���ε�������ε�����ֵ
            //Field.fieldname = "Xmin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymin";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Xmax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            //Field.fieldname = "Ymax";
            //Field.msk_leng = 255;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////���ע�Ƕ�Ӧ�ı��
            //Field.fieldname = "AnnoID";
            //Field.msk_leng = 15;
            //Field.fieldtype = meFieldType.meFldStr;
            //Fields.AppendFld(Field);
            ////�Լ�Ҫ������������
            //NewRegCls.SetFields(Fields);

            //mcSFeatureLayer BreakLineLayer = new mcSFeatureLayer();
            //this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            //BreakLineLayer.XClass = (IBasCls)Line_In_LegendCircleCls;
            //BreakLineLayer.LayerName = BreakLineLayer.LayerName;
            //this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)BreakLineLayer);
            //BreakLineLayer.Active = true;


            //mcSFeatureLayer NewRegLayer = new mcSFeatureLayer();
            //this.axMxWorkSpace1.ActiveMap = this.axMxWorkSpace1.MapCollection.get_Item(0);
            //NewRegLayer.XClass = (IBasCls)NewRegCls;
            //NewRegLayer.LayerName = NewRegLayer.LayerName;
            //this.axMxWorkSpace1.MapCollection.get_Item(0).Appendlayer((IXMapLayer)NewRegLayer);
            //NewRegLayer.Active = true;

            //Line_In_LegendCircleCls.Close();
            //NewRegCls.Close();
            //GDB.Close();
            //GDBSvr.DisConnect();

            //IXMapLayer layer1 = null;
            //layer1 = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(BreakLineLayer.LayerName) as IXMapLayer;
            //IVectorCls Line_vcls = layer1.XClass as IVectorCls;

            //IXMapLayer layer2 = null;
            //layer2 = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(NewRegLayer.LayerName) as IXMapLayer;
            //IVectorCls Reg_vcls = layer2.XClass as IVectorCls;

            ////��������
            //mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
            //SpatialAnalysis.ClipArc((mcSFeatureCls)Line_vcls, null, null, null);
            //SpatialAnalysis.TopoRegion((mcSFeatureCls)Line_vcls, null, (mcSFeatureCls)Reg_vcls, null);

            ////��ȡͼ���еı�ע
            //IVectorCls anno_vcls = null;
            //mcRecordSet TextAnno_RecordSet = null;//ͼ����Բ�������ڵı�ע����
            //anno_vcls = MapGIsK9Utils.GetVectorCls("HYBBC00104201_ͼ��.WT", axMxWorkSpace1);
            //mcQueryDef anno_def = new mcQueryDef();
            //anno_def.set_rect(circleRect, meSpaQueryMode.meModeContain);
            //anno_vcls.Select(anno_def, out TextAnno_RecordSet);


            ////�����������ɫ
            //mcRecordSet RecordSetReg = null;
            //Reg_vcls.Select(null, out RecordSetReg);
            //RecordSetReg.MoveFirst();
            //while (!RecordSetReg.IsEOF())
            //{
            //    IGeometry geoReg = null;
            //    IGeomInfo regInfo = null;
            //    mcObjectID regID = null;
            //    mcRecord record = null;
            //    RecordSetReg.GetID(out regID);
            //    Reg_vcls.Get(regID, out geoReg, out record, out regInfo);
            //    //�������ֶ����Ը�ֵ,��ȡͼ����עԲ�еı�ע
            //    mcRect tempRect = null;//Բ��ÿ��С�����������
            //    geoReg.CalRect(out tempRect);
            //    mcQueryDef localRegDef = new mcQueryDef();
            //    localRegDef.set_rect(tempRect, meSpaQueryMode.meModeContain);
            //    mcRecordSet localAnnoRecordSet = null;
            //    TextAnno_RecordSet.Select(localRegDef, out localAnnoRecordSet);
            //    localAnnoRecordSet.MoveFirst();
            //    IGeometry localResult = null;
            //    mcTextAnno local_TextAnno = null;
            //    localAnnoRecordSet.GetGeometry(out localResult);
            //    local_TextAnno = localResult as mcTextAnno;

            //    record.SetFldVal("Xmin", tempRect.xmin);
            //    record.SetFldVal("Xmax", tempRect.xmax);
            //    record.SetFldVal("Ymin", tempRect.ymin);
            //    record.SetFldVal("Ymax", tempRect.ymax);
            //    record.SetFldVal("AnnoID", local_TextAnno.Text);
            //    //�������ɫ
            //    mcRegInfo mRegInfo = (mcRegInfo)regInfo;
            //    //mRegInfo.fillclr = 0;

            //    Reg_vcls.Append(geoReg, record, mRegInfo);
            //    Reg_vcls.Del(regID);
            //    RecordSetReg.MoveNext();
            //}

            ////4����ȡע����Ϣ

            //SourceAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();

            //mcRecordSet anno_Circle_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0904201_��עԲ.WL", axMxWorkSpace1);
            //mcDots AnnoCircleDots = MapGIsK9Utils.GetCircleCoor(anno_Circle_Recordset);
            //mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet("HJGABD0804201_��עԲ.WT", axMxWorkSpace1);
            //mcRecordSet reg_RecordSet = null;
            //Reg_vcls.Select(null, out reg_RecordSet);
            ////������ע�е�ÿһ��Բ����ȡ԰�еı�ע��Ϣ
            //for (int i = 0; i < AnnoCircleDots.count; i++)
            //{
            //    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
            //    Dictionary<string, string> annoDic = new Dictionary<string, string>();
            //    //�����עԲ��ͼ����עԲ������ƫ��
            //    double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
            //    double offsetY = AnnoCircleDot.y - legendCircleCoor.y;

            //    reg_RecordSet.MoveFirst();
            //    while (!reg_RecordSet.IsEOF())
            //    {
            //        mcRecord record = null;
            //        reg_RecordSet.GetAtt(out record);
            //        object Xmin = null;
            //        object Xmax = null;
            //        object Ymin = null;
            //        object Ymax = null;
            //        object AnnoID = null;
            //        record.GetFldVal("Xmin", out Xmin);
            //        record.GetFldVal("Xmax", out Xmax);
            //        record.GetFldVal("Ymin", out Ymin);
            //        record.GetFldVal("Ymax", out Ymax);
            //        record.GetFldVal("AnnoID", out AnnoID);

            //        //�����עԲ��С���η�Χ
            //        mcRect rect = new mcRect();
            //        rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
            //        rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
            //        rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
            //        rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

            //        //mcGeoPolygon poly = MapGIsK9Utils.GetRectPolygon(rect);
            //        //Reg_vcls.Append(poly, null, null);

            //        anno_Recordset.MoveFirst();
            //        while (!anno_Recordset.IsEOF())
            //        {
            //            IGeometry geo = null;
            //            mcTextAnno TextAnno = null;
            //            anno_Recordset.GetGeometry(out geo);
            //            TextAnno = geo as mcTextAnno;
            //            mcDot AnchorDot = new mcDot();
            //            AnchorDot = TextAnno.AnchorDot;
            //            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
            //            {
            //                annoDic.Add(AnnoID.ToString(), TextAnno.Text);
            //                break;
            //            }
            //            anno_Recordset.MoveNext();
            //        }

            //        reg_RecordSet.MoveNext();
            //    }
            //    SourceAnnoDictionSet.Add(AnnoCircleDot, annoDic);
            //}
            //anno_vcls.Close();
            //Lengend_vcls.Close();
            //Line_vcls.Close();
            //Reg_vcls.Close();
        }
        #endregion







        //end
    }

}