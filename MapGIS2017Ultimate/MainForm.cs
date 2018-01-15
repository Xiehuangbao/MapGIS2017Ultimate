using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkSpace;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
using System.Data.OleDb;
using MapGIS2005;
namespace MapGIS2017Ultimate
{
    public partial class MainForm : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //存储新旧数据中图层名称集合，用于后续打开图层时的名称匹配
        List<string> layerNameListSource;
        List<string> layerNameListNew;

        private void MainForm_Load(object sender, EventArgs e)
        {
            axMapXView1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxDocTreeView1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxEditControl1.WorkSpace = axMxWorkSpace1.ToInterface;
            axMxEditControl1.EditView = axMapXView1.ToInterface;
            axMxEditControl1.AddGroupTool("设置", "SymEditTools.SymEditToolBar.1");
            axMxEditControl1.AddGroupTool("层编辑菜单", "MainEdit.EditGroup.1");

            axMapXViewNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxDocTreeViewNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxEditControlNew.WorkSpace = axMxWorkSpaceNew.ToInterface;
            axMxEditControlNew.EditView = axMapXViewNew.ToInterface;
            axMxEditControlNew.AddGroupTool("设置", "SymEditTools.SymEditToolBar.1");
        }

        //打开源数据
        private void btnOpenSourceMPJ_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "MapGIS67文件|*.MPJ|MapGISK9文件|*.MAP";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    axMxWorkSpace1.Open(ofd.FileName, WorkSpace.EnumOpenMode.OpenNormal);
                    //获取图层名称集合
                    layerNameListSource = new List<string>();
                    int layerCount = axMxWorkSpace1.MapCollection.get_Item(0).LayerCount;
                    for (int i = 1; i <= layerCount; i++)
                    {
                        layerNameListSource.Add(axMxWorkSpace1.MapCollection.get_Item(0).get_Layer(i).LayerName);
                    }
                    axMapXView1.Restore();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //打开新数据
        private void btnOpenNewMPJ_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "MapGIS67文件|*.MPJ|MapGISK9文件|*.MAP";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    axMxWorkSpaceNew.Open(ofd.FileName, WorkSpace.EnumOpenMode.OpenNormal);
                    //获取图层名称集合
                    layerNameListNew = new List<string>();
                    int layerCount = axMxWorkSpaceNew.MapCollection.get_Item(0).LayerCount;
                    for (int i = 1; i <= layerCount; i++)
                    {
                        layerNameListNew.Add(axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer(i).LayerName);
                    }

                    axMapXViewNew.Restore();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //关闭新旧工程数据
        private void btnCloseProj_Click(object sender, EventArgs e)
        {
            axMxWorkSpace1.Close(EnumCloseMode.DlgSave);
            axMxWorkSpaceNew.Close(EnumCloseMode.DlgSave);

            if (this.dgvAnnoList.Rows.Count > 1)
            {
                this.dgvAnnoList.Rows.Clear();
            }
        }

        //设置原始数据环境目录
        private void btnSetEnv_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("ReSetEnvir",0,0);
            //mcDataSrcMng dsm = axMxWorkSpace1.DataSrcMng;
            //mcMapGISEnv env = dsm.GetEnv();
            //env.cur = "C:\\东岗山矿\\旧-S430424003_东岗山铅锌矿核查区-铅矿";
            //env.slib = "C:\\5wslib";
            //env.clib = "C:\\MapGIS K9 SP3\\Clib";
            //env.temp = "C:\\MapGIS K9 SP3\\Temp";
            //dsm.SetEnv(env);
        }

        //设置新数据环境目录
        private void btnSetNewEnv_Click(object sender, EventArgs e)
        {
            axMxEditControlNew.StartTool("ReSetEnvir", 0, 0);
            //mcDataSrcMng dsm = axMxWorkSpaceNew.DataSrcMng;
            //mcMapGISEnv env = dsm.GetEnv();
            //env.cur = "C:\\东岗山矿\\旧-S430424003_东岗山铅锌矿核查区-铅矿";
            //env.slib = "C:\\C:\\mapgisNewLib";
            //env.clib = "C:\\MapGIS K9 SP3\\Clib";
            //env.temp = "C:\\MapGIS K9 SP3\\Temp";
            //dsm.SetEnv(env);
        }

        //提取原始数据注记信息
        Dictionary<mcDot, Dictionary<string, string>> SourceAnnoDictionSet = null;
        double RectWidth = 0;//记录标记圆的外包矩形宽，为了后面定位到标注圆用
        mcRecordSet NewRegRecordSetForUpdate = null;//后面更新标注时需要用到该小区集合数据
        mcRect circleRect = null;
        private void btnExtractAnnoSource_Click(object sender, EventArgs e)
        {
            try
            {
                NewRegRecordSetForUpdate = new mcRecordSet();
                mcDot legendCircleCoor = new mcDot();//图例圆的中心点坐标
                circleRect = new mcRect();    //图例圆的外包矩形

                List<mcObjectID> LineInLengendCircle_ObjectIDs = null;//图例圆中所有的线ID集合

                //连接创建的临时数据库
                mcGDBServer GDBSvr = null;
                mcGDataBase GDB = null;
                GDBSvr = new mcGDBServer();
                GDBSvr.Connect("MapGislocal", "", "");
                GDB = GDBSvr.get_gdb("TEMPDATABASE");

                //创建一个新图层存储线要素
                IVectorCls LineInLegendCircle_Cls = null;
                LineInLegendCircle_Cls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                string LineInLengendCircleName = "";//存储图例圆中的线的新图层的名字
                LineInLengendCircleName = "LineInLengendCircle" + new Random().Next();
                LineInLegendCircle_Cls.Create(LineInLengendCircleName, meGeomConstrainType.mefLin, 0, 0, null);

                //提取图例圆
                LineInLengendCircle_ObjectIDs = new List<mcObjectID>();
                IXGroupLayer Lengend_Circle_Layer = null;
                string LegendLayerWL = MapGIsK9Utils.getLayerName("_图例.WL", layerNameListSource);
                Lengend_Circle_Layer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(LegendLayerWL) as IXGroupLayer;
                IVectorCls Lengend_vcls = Lengend_Circle_Layer.get_Layer(1).XClass as IVectorCls;

                mcRecordSet LineInLengendCircle_RecordSet = null;
                mcRecordSet Lengend_RecordSet = null;
                Lengend_vcls.Select(null, out Lengend_RecordSet);
                Lengend_RecordSet.MoveFirst();
                //找到注记圆,并且将图例圆以及图例圆中的线复制到新图层中
                mcRect LengendCircleRect = null;
                while (!Lengend_RecordSet.IsEOF())
                {
                    IGeometry GeoResult = null;
                    Lengend_RecordSet.GetGeometry(out GeoResult);
                    GeoResult.CalRect(out LengendCircleRect);
                    double width = Math.Round(LengendCircleRect.xmax - LengendCircleRect.xmin, 2);
                    double heigth = Math.Round(LengendCircleRect.ymax - LengendCircleRect.ymin, 2);
                    //判断是否是注记圆
                    if (width == heigth)
                    {
                        RectWidth = width;
                        //保留图例圆的中心点坐标
                        legendCircleCoor.x = (LengendCircleRect.xmax + LengendCircleRect.xmin) / 2;
                        legendCircleCoor.y = (LengendCircleRect.ymax + LengendCircleRect.ymin) / 2;
                        break;
                    }
                    Lengend_RecordSet.MoveNext();
                }

                if (LengendCircleRect != null)
                {
                    mcQueryDef QueryDef = new mcQueryDef();
                    QueryDef.set_rect(LengendCircleRect, meSpaQueryMode.meModeContain);
                    Lengend_vcls.Select(QueryDef, out LineInLengendCircle_RecordSet);
                }
                //将图例圆中的所有线复制到新建的线图层中
                LineInLengendCircle_RecordSet.MoveFirst();
                while (!LineInLengendCircle_RecordSet.IsEOF())
                {
                    IGeometry geo = null;
                    LineInLengendCircle_RecordSet.GetGeometry(out geo);
                    LineInLegendCircle_Cls.Append(geo, null, null);
                    LineInLengendCircle_RecordSet.MoveNext();
                }

                //找到图例圆中所有的线
                LineInLengendCircle_RecordSet = null;
                LineInLegendCircle_Cls.Select(null, out LineInLengendCircle_RecordSet);
                LineInLengendCircle_RecordSet.MoveFirst();
                while (!LineInLengendCircle_RecordSet.IsEOF())
                {
                    IGeometry GeoResult = null;
                    LineInLengendCircle_RecordSet.GetGeometry(out GeoResult);
                    mcRect rect = null;
                    GeoResult.CalRect(out rect);
                    double width = Math.Round(rect.xmax - rect.xmin, 2);
                    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                    //判断是否是注记圆
                    if (width != heigth)
                    {
                        mcObjectID lineID = null;
                        LineInLengendCircle_RecordSet.GetID(out lineID);
                        //将除了圆以外的线加入到list集合中，用以后面线的连接用
                        LineInLengendCircle_ObjectIDs.Add(lineID);
                    }
                    else
                    {
                        //重新绘制外面的圆，因为之前的圆是断的
                        IGeometry geoOldCircle = null;
                        mcGeoVarLine OldCircle = null;
                        LineInLengendCircle_RecordSet.GetGeometry(out geoOldCircle);
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

                        LineInLegendCircle_Cls.Append(geoVarLine, null, null);
                        mcObjectID delCircleID = null;
                        LineInLengendCircle_RecordSet.GetID(out delCircleID);
                        LineInLegendCircle_Cls.Del(delCircleID);
                    }
                    LineInLengendCircle_RecordSet.MoveNext();
                }

                //循环判断线，做线的连接处理
                List<mcObjectID> delObjects = new List<mcObjectID>();//连接后需要删除的线集合
                for (int i = 0; i < LineInLengendCircle_ObjectIDs.Count; i++)
                {
                    //去除判断过的线
                    if (!delObjects.Contains(LineInLengendCircle_ObjectIDs[i]))
                    {
                        mcDots dots = null;
                        IGeometry geoLineInCire = null;
                        LineInLegendCircle_Cls.GetGeometry(LineInLengendCircle_ObjectIDs[i], out geoLineInCire);
                        IGeoLine lineInCire = geoLineInCire as IGeoLine;
                        lineInCire.Get2Dots(out dots);
                        //循环判断是否与两点有最近距离的线
                        for (int k = 0; k < dots.count; k++)
                        {
                            mcDot dot = null;
                            dot = dots.get_item(k);
                            for (int j = 0; j < LineInLengendCircle_ObjectIDs.Count; j++)
                            {
                                if (i != j && !delObjects.Contains(LineInLengendCircle_ObjectIDs[j]))
                                {
                                    mcDots _dots = null;
                                    IGeometry _geoLineInCire = null;
                                    LineInLegendCircle_Cls.GetGeometry(LineInLengendCircle_ObjectIDs[j], out _geoLineInCire);
                                    IGeoLine _lineInCire = _geoLineInCire as IGeoLine;
                                    _lineInCire.Get2Dots(out _dots);
                                    for (int _k = 0; _k < _dots.count; _k++)
                                    {
                                        mcDot _dot = null;
                                        _dot = _dots.get_item(_k);
                                        //判断是否有相近的点可以连接两条线
                                        double _x = Math.Abs(dot.x - _dot.x);
                                        double _y = Math.Abs(dot.y - _dot.y);
                                        //如果可以连接就连接，并且删除被连接的线
                                        if (_x < 0.2 && _y < 0.2)
                                        {
                                            IGeoVarLine geoVarLine = lineInCire as IGeoVarLine;
                                            //添加另外一条线的两点
                                            for (int pN = 0; pN < _dots.count; pN++)
                                            {
                                                geoVarLine.Append2D(_dots.get_item(pN));
                                            }
                                            LineInLegendCircle_Cls.Append(geoVarLine, null, null);
                                            delObjects.Add(LineInLengendCircle_ObjectIDs[i]);
                                            delObjects.Add(LineInLengendCircle_ObjectIDs[j]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //删除被合并的线
                for (int di = 0; di < delObjects.Count; di++)
                {
                    LineInLegendCircle_Cls.Del(delObjects[di]);
                }

                //2、延长线
                //从新建的图例图层中找到图例圆中所有的线
                LineInLengendCircle_ObjectIDs.Clear();
                LineInLengendCircle_RecordSet = null;
                LineInLegendCircle_Cls.Select(null, out LineInLengendCircle_RecordSet);
                LineInLengendCircle_RecordSet.MoveFirst();
                while (!LineInLengendCircle_RecordSet.IsEOF())
                {
                    IGeometry GeoResult = null;
                    LineInLengendCircle_RecordSet.GetGeometry(out GeoResult);
                    mcRect rect = null;
                    GeoResult.CalRect(out rect);
                    double width = Math.Round(rect.xmax - rect.xmin, 2);
                    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                    //判断是否是注记圆
                    if (width != heigth)
                    {
                        mcObjectID lineID = null;
                        LineInLengendCircle_RecordSet.GetID(out lineID);
                        LineInLengendCircle_ObjectIDs.Add(lineID);
                    }
                    else
                    {
                        circleRect = rect;//保存图例中圆的外包矩形
                    }
                    LineInLengendCircle_RecordSet.MoveNext();
                }
                //延长线
                for (int i = 0; i < LineInLengendCircle_ObjectIDs.Count; i++)
                {
                    IGeometry l_geoLine = null;
                    LineInLegendCircle_Cls.GetGeometry(LineInLengendCircle_ObjectIDs[i], out l_geoLine);
                    IGeoVarLine l_Line = null;
                    l_Line = l_geoLine as IGeoVarLine;
                    mcDots l_dots = null;
                    l_Line.Get2Dots(out l_dots);
                    mcDot dot_1 = l_dots.get_item(0);
                    mcDot dot_2 = l_dots.get_item(l_dots.count - 1);

                    //垂直情况(因为数据不标准，所以设置为< 1)
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
                        LineInLegendCircle_Cls.Append(NewLine, null, null);
                    }
                    else//非垂直情况
                    {
                        //计算直线的斜率
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
                        LineInLegendCircle_Cls.Append(NewLine, null, null);
                    }
                }

                //删除被延长的线
                for (int i = 0; i < LineInLengendCircle_ObjectIDs.Count; i++)
                {
                    LineInLegendCircle_Cls.Del(LineInLengendCircle_ObjectIDs[i]);
                }

                //3、拓扑构面
                IVectorCls NewRegCls = null;
                NewRegCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                string NewRegName = "新构面图层" + new Random().Next();
                NewRegCls.Create(NewRegName, meGeomConstrainType.mefReg, 0, 0, null);

                mcFields Fields = null;
                mcField Field = new mcField();
                NewRegCls.GetFields(out Fields);
                //设置要添加的字段,分别是每个小矩形的外包矩形的坐标值
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
                //添加注记对应的编号
                Field.fieldname = "AnnoID";
                Field.msk_leng = 15;
                Field.fieldtype = meFieldType.meFldStr;
                Fields.AppendFld(Field);
                //对简单要素类设置属性
                NewRegCls.SetFields(Fields);

                //线剪断
                mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
                SpatialAnalysis.ClipArc((mcSFeatureCls)LineInLegendCircle_Cls, null, null, null);

                //拓扑检查，删除悬挂弧段
                mcObjectIDs dangleLineIDs = new mcObjectIDs();
                SpatialAnalysis.DangleLineCheck2((mcSFeatureCls)LineInLegendCircle_Cls, dangleLineIDs);
                for (int dangleID = 0; dangleID < dangleLineIDs.count; dangleID++)
                {
                    LineInLegendCircle_Cls.Del(dangleLineIDs.get_item(dangleID));
                }

                //重叠线检查
                mcObjectIDs OverLapLineIDs = new mcObjectIDs();
                SpatialAnalysis.OverlapCheck2((mcSFeatureCls)LineInLegendCircle_Cls, OverLapLineIDs);
                for (int OverLapID = 0; OverLapID < OverLapLineIDs.count; OverLapID++)
                {
                    LineInLegendCircle_Cls.Del(OverLapLineIDs.get_item(OverLapID));
                }

                //获取要构面线的ID
                mcRecordSet lineForTuoBuildRegSet = null;
                LineInLegendCircle_Cls.Select(null, out lineForTuoBuildRegSet);
                mcObjectIDs LineIDsForBuildReg = new mcObjectIDs();
                lineForTuoBuildRegSet.MoveFirst();
                while (!lineForTuoBuildRegSet.IsEOF())
                {
                    mcObjectID lineIDForBuildReg = null;
                    lineForTuoBuildRegSet.GetID(out lineIDForBuildReg);
                    LineIDsForBuildReg.Append(lineIDForBuildReg);
                    lineForTuoBuildRegSet.MoveNext();
                }

                mcObjectIDs RegIDS = new mcObjectIDs();
                //拓扑造区
                SpatialAnalysis.TopoRegion((mcSFeatureCls)LineInLegendCircle_Cls, LineIDsForBuildReg, (mcSFeatureCls)NewRegCls, RegIDS);

                //获取图例中的标注
                IVectorCls anno_vcls = null;
                mcRecordSet TextAnno_RecordSet = null;//图例中圆形区域内的标注集合
                string LegendLayerWT = MapGIsK9Utils.getLayerName("_图例.WT", layerNameListSource);
                anno_vcls = MapGIsK9Utils.GetVectorCls(LegendLayerWT, axMxWorkSpace1);
                mcQueryDef anno_def = new mcQueryDef();
                anno_def.set_rect(circleRect, meSpaQueryMode.meModeContain);
                anno_vcls.Select(anno_def, out TextAnno_RecordSet);

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
                    //给坐标字段属性赋值,获取图例标注圆中的标注
                    mcRect tempRect = null;//圆内每个小区的外包矩形
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
                    //设置填充色
                    mcRegInfo mRegInfo = (mcRegInfo)regInfo;
                    //mRegInfo.fillclr = 0;

                    NewRegCls.Append(geoReg, record, mRegInfo);
                    NewRegCls.Del(regID);
                    RecordSetReg.MoveNext();
                }

                //4、提取注记信息

                SourceAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();

                string AnnoCircleWL = MapGIsK9Utils.getLayerName("_标注圆.WL", layerNameListSource);
                mcRecordSet anno_Circle_Recordset = MapGIsK9Utils.GetLayerRecordSet(AnnoCircleWL, axMxWorkSpace1);
                mcDots AnnoCircleDots = MapGIsK9Utils.GetCircleCoor(anno_Circle_Recordset);
                string AnnoCircleWT = MapGIsK9Utils.getLayerName("_标注圆.WT", layerNameListSource);
                mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet(AnnoCircleWT, axMxWorkSpace1);
                mcRecordSet reg_RecordSet = null;
                NewRegCls.Select(null, out reg_RecordSet);
                NewRegRecordSetForUpdate = reg_RecordSet;//保存原始数据的每个小区
                //遍历标注中的每一个圆，获取园中的标注信息
                for (int i = 0; i < AnnoCircleDots.count; i++)
                {
                    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
                    Dictionary<string, string> annoDic = new Dictionary<string, string>();
                    //计算标注圆与图例标注圆的坐标偏移
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

                        //计算标注圆的小矩形范围
                        mcRect rect = new mcRect();
                        rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                        rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                        rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                        rect.ymax = double.Parse(Ymax.ToString()) + offsetY;
                        //将注记信息添加到集合中
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
                    //将每个标注圆内的注记信息存储到列表中
                    SourceAnnoDictionSet.Add(AnnoCircleDot, annoDic);
                }

                //读取标注信息

                mcRecordSet legend_Record = MapGIsK9Utils.GetLayerRecordSet(LegendLayerWT, axMxWorkSpace1);
                sourceAttr = MapGIsK9Utils.GetLengendAnno(legend_Record);

                //检测图例标注是否存在于数据字典中，不存在的话，新添字典
                List<string> attrNotInMapDic = null;
                bool isContain = MapGIsK9Utils.isInDictionary(sourceAttr, "sourceAnno", out attrNotInMapDic);
                if (!isContain)
                {
                    AddDataDicDlg adddlg = new AddDataDicDlg(attrNotInMapDic, "sourceAnno");
                    adddlg.Show();
                }

                LineInLegendCircle_Cls.Close();
                NewRegCls.Close();

                GDB.Close();
                GDBSvr.DisConnect();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.ToString());
            }
           
        }


        //提取新数据注记信息
        Dictionary<mcDot, Dictionary<string, string>> NewAnnoDictionSet = null;//存储待更新标注圆的坐标以及每个圆内的所有标注信息
        double newRectWidth = 0;
        string lineforupdate = "";//后面需要更新标注圆图形信息的名字
        private void btnExtractAnnoNew_Click(object sender, EventArgs e)
        {
            try
            {
                mcGDBServer GDBSvr = null;
                mcGDataBase GDB = null;
                GDBSvr = new mcGDBServer();
                GDBSvr.Connect("MapGislocal", "", "");
                GDB = GDBSvr.get_gdb("TEMPDATABASE");
                //1、提取图例圆

                //计算、保留图例圆的中心点坐标
                mcDot LengendCircleDot = new mcDot();
                mcRect CircleRect = new mcRect();//右下角图例圆的外包矩形

                //找到图例圆
                IPntInfo pntInfo = getLegendCircle(out LengendCircleDot, out CircleRect);
                //将提取的图例圆中的线存储在一个新的线图层中
                mcSFeatureCls ExtractLengendCircleLineCls = new mcSFeatureCls();//提取的图例圆线图层要素
                ExtractLengendCircleLineCls = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
                string symboleCircleLine = "SymbolCircleLine" + new Random().Next();//提取图例圆以后新图例圆组成线图层
                ExtractLengendCircleLineCls.Create(symboleCircleLine, meGeomConstrainType.mefLin, 0, 0, null);
                //提取子图信息
                IResource Resource = axMxWorkSpaceNew.Resource;
                IXMapSymbolLib MapSymbolLib = Resource.MapSymLib;
                mcMapSymbol psySymbol = null;
                meUnitSymbolType symbolType = meUnitSymbolType.meVectLine;
                MapSymbolLib.GetSymbol(pntInfo.symID, meSymbolType.mePntSymbol, out psySymbol, out symbolType);
                //获取子图的每个Item
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
                            //将子图中的线添加到新的线图层中
                            ExtractLengendCircleLineCls.Append((IGeometry)line, null, null);
                            break;
                    }
                }

                //找到新生成图圆的外包矩形
                mcRecordSet newLengendLineRecordSet = null;//新生成图层中的线要素
                ExtractLengendCircleLineCls.Select(null, out newLengendLineRecordSet);
                mcRect NewCircleRect = new mcRect();//新图圆的外包矩形
                newLengendLineRecordSet.MoveFirst();
                while (!newLengendLineRecordSet.IsEOF())
                {
                    IGeometry geoLine = null;
                    newLengendLineRecordSet.GetGeometry(out geoLine);
                    mcGeoVarLine varline = geoLine as mcGeoVarLine;
                    mcDots lindeDots = null;
                    varline.Get2Dots(out lindeDots);
                    mcRect isRect = null;
                    geoLine.CalRect(out isRect);
                    //根据线中的点数个数确定是不是圆
                    if (lindeDots.count > 10)
                    {
                        NewCircleRect = isRect;
                        break;
                    }
                    newLengendLineRecordSet.MoveNext();
                }


                //对线进行缩放操作,将新画的圆与数据圆对齐
                mcDot NewCircleDot = new mcDot();
                NewCircleDot.x = (NewCircleRect.xmax + NewCircleRect.xmin) / 2;
                NewCircleDot.y = (NewCircleRect.ymax + NewCircleRect.ymin) / 2;

                double NewCircleRectWidth = NewCircleRect.xmax - NewCircleRect.xmin;
                double NewCircleRectHeight = NewCircleRect.ymax - NewCircleRect.ymin;


                //T60新数据需要/2但是其他数据不需要/2   Why????
                double ratio = (pntInfo.height / NewCircleRectHeight) / 2;//新旧圆的缩放比例
                //将新生成的线图层中的要素平移到正确位置
                newLengendLineRecordSet.MoveFirst();
                while (!newLengendLineRecordSet.IsEOF())
                {
                    IGeometry geoLine = null;
                    newLengendLineRecordSet.GetGeometry(out geoLine);
                    mcGeoVarLine varline = geoLine as mcGeoVarLine;
                    mcDots lineDotsNew = new mcDots();
                    mcDots lindeDots = null;
                    varline.Get2Dots(out lindeDots);

                    for (int k = 0; k < lindeDots.count; k++)//循环对每个点做缩放
                    {
                        mcDot NewDot = new mcDot();
                        NewDot.x = ratio * (lindeDots.get_item(k).x - NewCircleDot.x) + LengendCircleDot.x;
                        NewDot.y = ratio * (lindeDots.get_item(k).y - NewCircleDot.y) + LengendCircleDot.y;
                        lineDotsNew.Add(NewDot);
                    }

                    mcGeoVarLine addLine = new mcGeoVarLine();
                    addLine.SetDots2D(lineDotsNew);
                    ExtractLengendCircleLineCls.Append((IGeometry)addLine, null, null);
                    mcObjectID delID = null;
                    newLengendLineRecordSet.GetID(out delID);
                    ExtractLengendCircleLineCls.Del(delID);

                    newLengendLineRecordSet.MoveNext();
                }


                //将新的图例圆保存在GDB中，为了后面更新注记图形用
                mcSFeatureCls LengendCircleLineForUpdateCls = new mcSFeatureCls();
                LengendCircleLineForUpdateCls = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
                lineforupdate = "LineForUpdate" + new Random().Next();//提取图例圆以后新图例圆组成线图层
                LengendCircleLineForUpdateCls.Create(lineforupdate, meGeomConstrainType.mefLin, 0, 0, null);
                mcRecordSet addForUpdateSet = null;
                ExtractLengendCircleLineCls.Select(null, out addForUpdateSet);
                addForUpdateSet.MoveFirst();
                while (!addForUpdateSet.IsEOF())
                {
                    IGeometry updateGeo = null;
                    addForUpdateSet.GetGeometry(out updateGeo);
                    IGeoVarLine updateLine = updateGeo as IGeoVarLine;
                    LengendCircleLineForUpdateCls.Append(updateLine, null, null);
                    addForUpdateSet.MoveNext();
                }

                //2、将新生成的图例圆中的线进行延长操作
                mcRecordSet RecordSet = null;//包含注记圆的线集合
                mcRecordSet ExpandLineRecordSet = null;//待延长线集合
                ExtractLengendCircleLineCls.Select(null, out RecordSet);
                RecordSet.MoveFirst();
                //找到注记圆
                while (!RecordSet.IsEOF())
                {
                    IGeometry GeoResult = null;
                    RecordSet.GetGeometry(out GeoResult);
                    mcRect rect = null;
                    GeoResult.CalRect(out rect);
                    double width = Math.Round(rect.xmax - rect.xmin, 0);
                    double heigth = Math.Round(rect.ymax - rect.ymin, 0);
                    //判断是否是注记圆
                    if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
                    {
                        newRectWidth = width;
                        mcObjectID delCircleID = null;
                        RecordSet.GetID(out delCircleID);
                        mcObjectIDs delCircleIDs = new mcObjectIDs();
                        delCircleIDs.Append(delCircleID);
                        ExtractLengendCircleLineCls.Select(null, out ExpandLineRecordSet);
                        //重新绘制外面的圆，因为之前的圆是断的
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

                        ExtractLengendCircleLineCls.Append(geoVarLine, null, null);
                        ExtractLengendCircleLineCls.Del(delCircleID);
                        //去除外面的圆线
                        ExpandLineRecordSet.SubSet2(delCircleIDs);
                        ExpandLineRecordSet.MoveFirst();
                        while (!ExpandLineRecordSet.IsEOF())
                        {
                            MapGIsK9Utils.ExpandLine(ExpandLineRecordSet, ExtractLengendCircleLineCls);
                            ExpandLineRecordSet.MoveNext();
                        }
                        break;
                    }
                    RecordSet.MoveNext();
                }


                //3、根据新线进行构面操作
                mcRecordSet NewLineFeatureRecordSet = null;
                ExtractLengendCircleLineCls.Select(null, out NewLineFeatureRecordSet);

                IVectorCls BreakLineCls = null;//打断以后线要素图层
                IVectorCls NewRegCls = null;//新生成区要素图层
                BreakLineCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                NewRegCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
                BreakLineCls.Create("NewBreakLines" + new Random().Next(), meGeomConstrainType.mefLin, 0, 0, null);
                NewRegCls.Create("NewTempReg" + new Random().Next(), meGeomConstrainType.mefReg, 0, 0, null);
                mcFields Fields = null;
                mcField Field = new mcField();
                NewRegCls.GetFields(out Fields);
                //设置要添加的字段,分别是每个小矩形的外包矩形的坐标值
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
                //添加注记对应的编号
                Field.fieldname = "AnnoID";
                Field.msk_leng = 15;
                Field.fieldtype = meFieldType.meFldStr;
                Fields.AppendFld(Field);
                //对简单要素类设置属性
                NewRegCls.SetFields(Fields);

                NewLineFeatureRecordSet.MoveFirst();
                //找到注记圆
                mcRect circleRect = new mcRect();//注记圆的外包矩形
                while (!NewLineFeatureRecordSet.IsEOF())
                {
                    IGeometry GeoResult = null;
                    NewLineFeatureRecordSet.GetGeometry(out GeoResult);
                    mcRect rect = null;
                    GeoResult.CalRect(out rect);
                    double width = Math.Round(rect.xmax - rect.xmin, 2);
                    double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                    //找到注记圆
                    if (Math.Abs(width - heigth) == 0 || Math.Abs(width - heigth) < 5)
                    {
                        circleRect = rect;
                    }
                    IGeoLine line = null;
                    IGeometry geo = null;
                    NewLineFeatureRecordSet.GetGeometry(out geo);
                    line = geo as IGeoLine;
                    BreakLineCls.Append(line, null, null);
                    NewLineFeatureRecordSet.MoveNext();
                }

                //3、拓扑造区
                mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();
                SpatialAnalysis.ClipArc((mcSFeatureCls)BreakLineCls, null, null, null);

                //拓扑检查，删除悬挂弧段
                mcObjectIDs dangleLineIDs = new mcObjectIDs();
                SpatialAnalysis.DangleLineCheck2((mcSFeatureCls)BreakLineCls, dangleLineIDs);
                for (int dangleID = 0; dangleID < dangleLineIDs.count; dangleID++)
                {
                    BreakLineCls.Del(dangleLineIDs.get_item(dangleID));
                }

                //重叠线检查
                mcObjectIDs OverLapLineIDs = new mcObjectIDs();
                SpatialAnalysis.OverlapCheck2((mcSFeatureCls)BreakLineCls, OverLapLineIDs);
                for (int OverLapID = 0; OverLapID < OverLapLineIDs.count; OverLapID++)
                {
                    BreakLineCls.Del(OverLapLineIDs.get_item(OverLapID));
                }

                //获取要构面线的ID
                mcRecordSet lineForTuoBuildRegSet = null;
                BreakLineCls.Select(null, out lineForTuoBuildRegSet);
                mcObjectIDs LineIDsForBuildReg = new mcObjectIDs();
                lineForTuoBuildRegSet.MoveFirst();
                while (!lineForTuoBuildRegSet.IsEOF())
                {
                    mcObjectID lineIDForBuildReg = null;
                    lineForTuoBuildRegSet.GetID(out lineIDForBuildReg);
                    LineIDsForBuildReg.Append(lineIDForBuildReg);
                    lineForTuoBuildRegSet.MoveNext();
                }

                mcObjectIDs RegIDS = new mcObjectIDs();
                //造区
                SpatialAnalysis.TopoRegion((mcSFeatureCls)BreakLineCls, LineIDsForBuildReg, (mcSFeatureCls)NewRegCls, RegIDS);

                ////获取图例中的标注
                IVectorCls anno_vcls = null;
                mcRecordSet TextAnno_RecordSet = null;//图例中圆形区域内的标注集合

                IXGroupLayer layer = null;
                string WT = MapGIsK9Utils.getLayerName(".WT", layerNameListNew);
                layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(WT) as IXGroupLayer;
                anno_vcls = layer.get_Layer(1).XClass as IVectorCls;
                mcQueryDef anno_def = new mcQueryDef();

                //由于数据不标准，需要将外包矩形(circleRect)扩大，确保获取到所有标注
                mcRect ExpandRect = new mcRect();
                ExpandRect.xmin = CircleRect.xmin - 2;
                ExpandRect.ymin = CircleRect.ymin - 2;
                ExpandRect.xmax = CircleRect.xmax + 2;
                ExpandRect.ymax = CircleRect.ymax + 2;
                mcGeoPolygon queryPolygon = MapGIsK9Utils.GetRectPolygon(ExpandRect);
                anno_def.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
                anno_vcls.Select(anno_def, out TextAnno_RecordSet);

                //将新构面图层加载到地图中，比较差异
                //this.axMxWorkSpaceNew.MapCollection.get_Item(0).Appendlayer2(NewRegCls);

                mcRecordSet RecordSetReg = null;
                //NewRegCls是新生成区文件
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
                    //给坐标字段属性赋值,获取图例标注圆中的标注
                    mcRect tempRect = null;//圆内每个小区的外包矩形
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
                            NewRegCls.Append(geoReg, record, regInfo);
                            NewRegCls.Del(regID);
                        }

                        TextAnno_RecordSet.MoveNext();
                    }

                    RecordSetReg.MoveNext();
                }


                //4、提取注记信息
                NewAnnoDictionSet = new Dictionary<mcDot, Dictionary<string, string>>();

                mcRecordSet anno_Recordset = null;
                anno_vcls.Select(null, out anno_Recordset);

                mcDot LengendCircleCenterCoor = LengendCircleDot;//获取图例圆的中心点坐标

                //获取标注圆的中心点坐标集合
                mcDots AnnoCircleDots = new mcDots();
                getAnnoCircleDots(AnnoCircleDots);
                //获取图例图层中每个小区
                mcRecordSet reg_RecordSet = null;
                NewRegCls.Select(null, out reg_RecordSet);
                for (int i = 0; i < AnnoCircleDots.count; i++)
                {
                    //计算标注圆与图例标注圆的坐标偏移
                    Dictionary<string, string> annoDic = new Dictionary<string, string>();
                    mcDot AnnoCircleDot = AnnoCircleDots.get_item(i);
                    double offsetX = AnnoCircleDot.x - LengendCircleCenterCoor.x;
                    double offsetY = AnnoCircleDot.y - LengendCircleCenterCoor.y;

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

                        //计算标注圆的小矩形范围
                        mcRect rect = new mcRect();
                        try
                        {
                            rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                            rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                            rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                            rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

                            //从标注圆范围内提取标注信息
                            mcQueryDef def = new mcQueryDef();
                            def.set_rect(rect, meSpaQueryMode.meModeContain);

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
                                }
                                anno_Recordset.MoveNext();
                            }

                            reg_RecordSet.MoveNext();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    NewAnnoDictionSet.Add(AnnoCircleDots.get_item(i), annoDic);
                }

                //获取图例标注信息
                mcRecordSet legend_RecordSetNew = MapGIsK9Utils.GetLayerRecordSet(WT, axMxWorkSpaceNew);
                newAttr = MapGIsK9Utils.GetLengendAnno(legend_RecordSetNew);


                //检测图例标注是否存在于数据字典中，不存在的话，新添字典
                List<string> attrNotInMapDic = null;
                bool isContain = MapGIsK9Utils.isInDictionary(newAttr, "newAnno", out attrNotInMapDic);
                if (!isContain)
                {
                    AddDataDicDlg adddlg = new AddDataDicDlg(attrNotInMapDic, "newAnno");
                    adddlg.Show();
                }

                LengendCircleLineForUpdateCls.Close();
                ExtractLengendCircleLineCls.Close();
                BreakLineCls.Close();//释放资源  
                NewRegCls.Close();
                GDB.Close();
                GDBSvr.DisConnect();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //加载DataGridView里面的数据
        List<mcDot> CircleDotInRow = null; //每行对应的圆的坐标
        private void LoadDataGrideView(Dictionary<string, string> relationtable, List<string> sourceAttr)
        {
            try
            {
                //清空DataGridView数据
                if (dgvAnnoList.RowCount > 1)
                {
                    dgvAnnoList.DataSource = null;
                }

                CircleDotInRow = new List<mcDot>();
                DataTable dt = new DataTable();
                dt.Columns.Add("标注圆编号");
                //添加表头
                for (int i = 0; i < sourceAttr.Count; i++)
                {
                    //dt.Columns.Add(sourceAttr[i].Split('、')[1]);
                    dt.Columns.Add((i + 1).ToString());
                }
                //计算中心点坐标最近的圆
                int BlockID = 1;
                foreach (KeyValuePair<mcDot, Dictionary<string, string>> sourceKVP in SourceAnnoDictionSet)
                {
                    mcDot sourceDot = sourceKVP.Key;
                    CircleDotInRow.Add(sourceDot);
                    double minDis = double.MaxValue;
                    Dictionary<string, string> newCircleAnnoDic = null;//与原始数据圆最近的新数据的圆
                    //遍历新数据集合，找到与原始数据圆最近的点
                    mcDot rowDot = new mcDot();
                    foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                    {

                        mcDot newDot = newKVP.Key;
                        //数据截取，去除y值的38000000
                        string valueStr = newDot.x.ToString().Substring(2);
                        //处理过后，去掉带号的坐标
                        mcDot exceptPreDot = new mcDot();
                        exceptPreDot.x = double.Parse(valueStr);
                        exceptPreDot.y = newDot.y;
                        double distance = MapGIsK9Utils.CalDistanceOfCircle(sourceDot, exceptPreDot);
                        if (minDis > distance)
                        {
                            minDis = distance;
                            newCircleAnnoDic = newKVP.Value;
                            rowDot = newDot;
                        }
                    }
                    CircleDotInRow.Add(rowDot);
                    //再根据新旧数据对应关系，将数据加载到DataGridView中
                    List<string> sourceRow = new List<string>();
                    sourceRow.Add(BlockID.ToString());
                    List<string> newRow = new List<string>();
                    newRow.Add(BlockID.ToString());

                    Dictionary<string, string> sourceCircleAnnoDic = sourceKVP.Value;
                    List<int> sourceKeyList = new List<int>();
                    foreach (KeyValuePair<string, string> sourceValueKVP in sourceCircleAnnoDic)
                    {

                        sourceKeyList.Add(int.Parse(sourceValueKVP.Key));

                    }
                    //对键进行排序，按1、2、3、4……13的顺序
                    sourceKeyList.Sort();
                    //重新处理，为了读取没有注记信息的点
                    List<int> _sourceKeyList = new List<int>();
                    for (int i = 0; i < sourceKeyList[sourceKeyList.Count - 1]; i++)
                    {
                        _sourceKeyList.Add(i + 1);
                    }

                    for (int i = 0; i < _sourceKeyList.Count; i++)
                    {
                        string value = "";
                        sourceCircleAnnoDic.TryGetValue(_sourceKeyList[i].ToString(), out value);
                        sourceRow.Add(value);
                        string newKey = "";
                        relationtable.TryGetValue(_sourceKeyList[i].ToString(), out newKey);
                        string newValue = "";
                        newCircleAnnoDic.TryGetValue(newKey, out newValue);
                        newRow.Add(newValue);
                    }
                    dt.Rows.Add(sourceRow.ToArray());
                    dt.Rows.Add(newRow.ToArray());
                    BlockID++;
                }

                this.dgvAnnoList.DataSource = dt;
                this.dgvAnnoList.ColumnHeadersHeight = 40;
                this.dgvAnnoList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                this.dgvAnnoList.MergeColumnNames.Add("标注圆编号");
                //不能让每行数据按列自动排序
                for (int i = 0; i < this.dgvAnnoList.Columns.Count; i++)
                {
                    this.dgvAnnoList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (i > 1)
                    {
                        this.dgvAnnoList.Columns[i].Width = 55;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //双击GridView实现自动定位到标注圆
        private void dgvAnnoList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvAnnoList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                int sourceRowIndex = 0;
                int newRowIndex = 0;
                if (e.RowIndex % 2 != 0)
                {
                    sourceRowIndex = e.RowIndex - 1;
                    newRowIndex = e.RowIndex;
                }
                else
                {
                    sourceRowIndex = e.RowIndex;
                    newRowIndex = e.RowIndex + 1;
                }

                mcRect sourceRect = new mcRect();
                sourceRect.xmin = CircleDotInRow[sourceRowIndex].x - (RectWidth);
                sourceRect.xmax = CircleDotInRow[sourceRowIndex].x + (RectWidth);
                sourceRect.ymin = CircleDotInRow[sourceRowIndex].y - (RectWidth);
                sourceRect.ymax = CircleDotInRow[sourceRowIndex].y + (RectWidth);
                mcRect newRect = new mcRect();
                newRect.xmin = CircleDotInRow[newRowIndex].x - (newRectWidth);
                newRect.xmax = CircleDotInRow[newRowIndex].x + (newRectWidth);
                newRect.ymin = CircleDotInRow[newRowIndex].y - (newRectWidth);
                newRect.ymax = CircleDotInRow[newRowIndex].y + (newRectWidth);
                this.axMapXViewNew.Jump(newRect);
                this.axMapXView1.Jump(sourceRect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //更新标注信息
        private void btnAnnoUpdate_Click(object sender, EventArgs e)
        {
            //获取原始数据的要素数据
            try
            {
                IXGroupLayer sourceLayer = null;
                string AnnoCircleWT = MapGIsK9Utils.getLayerName("_标注圆.WT", layerNameListSource);
                sourceLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(AnnoCircleWT) as IXGroupLayer;
                IVectorCls sourceVcls = sourceLayer.get_Layer(1).XClass as IVectorCls;
                string LegendWL = MapGIsK9Utils.getLayerName("_图例.WL", layerNameListSource);
                mcRecordSet legend_Recordset = MapGIsK9Utils.GetLayerRecordSet(LegendWL, axMxWorkSpace1);
                mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet(AnnoCircleWT, axMxWorkSpace1);

                //找到图例圆的中心点坐标
                mcDots LegendCircleDots = MapGIsK9Utils.GetCircleCoor(legend_Recordset);
                mcDot legendCircleCoor = LegendCircleDots.get_item(0);

                foreach (KeyValuePair<mcDot, Dictionary<string, string>> sourceKVP in SourceAnnoDictionSet)
                {
                    mcDot sourceDot = sourceKVP.Key;
                    double minDis = double.MaxValue;
                    Dictionary<string, string> newCircleAnnoDic = null;//与原始数据圆最近的新数据的圆
                    //遍历新数据集合，找到与原始数据圆最近的点
                    mcDot rowDot = null;
                    foreach (KeyValuePair<mcDot, Dictionary<string, string>> newKVP in NewAnnoDictionSet)
                    {

                        mcDot newDot = newKVP.Key;
                        //数据截取，去除y值的38000000
                        string valueStr = newDot.x.ToString().Substring(2);
                        //处理过后，去掉带号的坐标
                        mcDot exceptPreDot = new mcDot();
                        exceptPreDot.x = double.Parse(valueStr);
                        exceptPreDot.y = newDot.y;
                        double distance = MapGIsK9Utils.CalDistanceOfCircle(sourceDot, exceptPreDot);
                        if (minDis > distance)
                        {
                            minDis = distance;
                            newCircleAnnoDic = newKVP.Value;
                            rowDot = newDot;
                        }
                    }


                    mcDot AnnoCircleDot = sourceDot;
                    //计算标注圆与图例标注圆的坐标偏移
                    double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
                    double offsetY = AnnoCircleDot.y - legendCircleCoor.y;
                    //存储了与原始数据圆对应的新数据圆中的数据
                    Dictionary<string, string> NewCireclDic = newCircleAnnoDic;
                    int id = NewRegRecordSetForUpdate.MoveFirst();
                    while (!NewRegRecordSetForUpdate.IsEOF())
                    {
                        mcRecord record = null;
                        NewRegRecordSetForUpdate.GetAtt(out record);
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

                        //计算标注圆的小矩形范围
                        mcRect rect = new mcRect();
                        rect.xmin = double.Parse(Xmin.ToString()) + offsetX;
                        rect.ymin = double.Parse(Ymin.ToString()) + offsetY;
                        rect.xmax = double.Parse(Xmax.ToString()) + offsetX;
                        rect.ymax = double.Parse(Ymax.ToString()) + offsetY;

                        int sID = anno_Recordset.MoveFirst();
                        while (!anno_Recordset.IsEOF())
                        {
                            IGeometry geo = null;
                            mcTextAnno TextAnno = null;
                            anno_Recordset.GetGeometry(out geo);
                            mcObjectID updataID = null;
                            anno_Recordset.GetID(out updataID);
                            TextAnno = geo as mcTextAnno;
                            mcDot AnchorDot = new mcDot();
                            AnchorDot = TextAnno.AnchorDot;
                            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                            {
                                //先根据对应关系找到原始数据对应的新数据编号
                                string NewID = null;
                                tableRealation.TryGetValue(AnnoID.ToString(), out NewID);
                                string updataData = null;
                                NewCireclDic.TryGetValue(NewID, out updataData);
                                //替换数据
                                TextAnno.Text = updataData;
                                IGeometry updateGeo = TextAnno as IGeometry;
                                int s = sourceVcls.Update(updataID, updateGeo, null, null);
                            }
                            sID = anno_Recordset.MoveNext();
                        }

                        id = NewRegRecordSetForUpdate.MoveNext();
                    }
                }
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                //string TempMPJPath = AppPath.Replace("bin\\x86\\Debug\\", "TempMPJ\\");
                string TempMPJPath = AppPath+ "TempMPJ\\";
                axMxWorkSpace1.SaveAs(TempMPJPath + axMxWorkSpace1.MapCollection.get_Item(0).Name + "-AnnoU.MPJ");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("标注信息更新成功!");
        }

        //注记校验
        List<string> sourceAttr = null;
        List<string> newAttr = null;
        Dictionary<string, string> tableRealation = null;//对应关系表
        private void btnDataVerify_Click(object sender, EventArgs e)
        {
            if (sourceAttr != null && newAttr != null)
            {
                tableRealation = new Dictionary<string, string>();
                tableRealation = getAnnoRelationTable();
                //加载DataGridView数据
                LoadDataGrideView(tableRealation, sourceAttr);
            }
        }

        //Access表更新
        private void btnAccessUpdate_Click(object sender, EventArgs e)
        {
            AccessUpdateDlg audlg = new AccessUpdateDlg(SourceAnnoDictionSet,NewAnnoDictionSet);
            audlg.newAttr = newAttr;
            audlg.ShowDialog();
        }

        //标注图形更新
        private void btnGeoUpdate_Click(object sender, EventArgs e)
        {
            mcGDBServer GDBSvr = null;
            mcGDataBase GDB = null;
            IVectorCls LengendCircleLineForUpdateCls = null;
            GDBSvr = new mcGDBServer();

            //连接数据源，打开数据库
            GDBSvr.Connect("MapGislocal", "", "");
            GDB = GDBSvr.get_gdb("TEMPDATABASE");

            //打开简单要素类
            LengendCircleLineForUpdateCls = GDB.get_XClass(meXClsType.meXSFCls) as IVectorCls;
            LengendCircleLineForUpdateCls.Open(lineforupdate, 0);

            try
            {
                IXGroupLayer sourceLayer = null;
                string LegendLayerWL = MapGIsK9Utils.getLayerName("_图例.WL", layerNameListSource);
                sourceLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(LegendLayerWL) as IXGroupLayer;
                IVectorCls sourceVcls = sourceLayer.get_Layer(1).XClass as IVectorCls;
                mcQueryDef def = new mcQueryDef();
                def.set_rect(circleRect, meSpaQueryMode.meModeContain);

                IXGroupLayer sourceLengendAnnoLayer = null;
                //删除原始数据中图例圆中的标注
                string LegendLayerWT = MapGIsK9Utils.getLayerName("_图例.WT", layerNameListSource);
                sourceLengendAnnoLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(LegendLayerWT) as IXGroupLayer;
                IVectorCls sourceLengendAnnoVcls = sourceLengendAnnoLayer.get_Layer(1).XClass as IVectorCls;
                mcRecordSet LegendAnnoSet = new mcRecordSet();
                sourceLengendAnnoVcls.Select(null, out LegendAnnoSet);
                LegendAnnoSet.MoveFirst();
                while (!LegendAnnoSet.IsEOF())
                {
                    IGeometry legendAnoo_Result = null;
                    mcTextAnno legendAnoo_TextAnno = null;
                    LegendAnnoSet.GetGeometry(out legendAnoo_Result);
                    legendAnoo_TextAnno = legendAnoo_Result as mcTextAnno;
                    string pattern = @"\d+、";
                    if (Regex.IsMatch(legendAnoo_TextAnno.Text, pattern))
                    {
                        mcObjectID delAnnoID = new mcObjectID();
                        LegendAnnoSet.GetID(out delAnnoID);
                        sourceLengendAnnoVcls.Del(delAnnoID);
                    }
                    LegendAnnoSet.MoveNext();
                }

                mcRecordSet legendAnnoSetInCircle = new mcRecordSet();
                sourceLengendAnnoVcls.Select(def, out legendAnnoSetInCircle);
                legendAnnoSetInCircle.MoveFirst();
                while (!legendAnnoSetInCircle.IsEOF())
                {
                    mcObjectID delAnnoID = new mcObjectID();
                    legendAnnoSetInCircle.GetID(out delAnnoID);
                    sourceLengendAnnoVcls.Del(delAnnoID);
                    legendAnnoSetInCircle.MoveNext();
                }

                mcRecordSet lineInCircleSet = new mcRecordSet();
                sourceVcls.Select(def, out lineInCircleSet);
                lineInCircleSet.MoveFirst();
                while(!lineInCircleSet.IsEOF()){

                    mcObjectID updateID = null;
                    lineInCircleSet.GetID(out updateID);
                    sourceVcls.Del(updateID);
                    lineInCircleSet.MoveNext();
                }
                sourceVcls.SubSet(lineInCircleSet);
                mcDot sourceCircleDot = new mcDot();
                sourceCircleDot.x = (circleRect.xmin + circleRect.xmax) / 2;
                sourceCircleDot.y = (circleRect.ymin + circleRect.ymax) / 2;

                //新标注圆
                mcRecordSet newCircleSet = null;
                LengendCircleLineForUpdateCls.Select(null, out newCircleSet);
                mcRect newCircleRect = null;
                newCircleSet.MoveFirst();
                while (!newCircleSet.IsEOF())
                {
                    IGeometry geo = null;
                    newCircleSet.GetGeometry(out geo);
                    IGeoVarLine varLine = geo as IGeoVarLine;
                    mcDots dots = null;
                    varLine.Get2Dots(out dots);
                    if (dots.count > 10)
                    {
                        geo.CalRect(out newCircleRect);//找到新标注圆的外包矩形
                        break;
                    }
                    newCircleSet.MoveNext();
                }


                mcDot newCircleDot = new mcDot();
                newCircleDot.x = (newCircleRect.xmin + newCircleRect.xmax) / 2;
                newCircleDot.y = (newCircleRect.ymin + newCircleRect.ymax) / 2;
                //处理新数据的X坐标去掉38
                string str = newCircleDot.x.ToString().Substring(2);
                newCircleDot.x = double.Parse(str);
                double offsetX = newCircleDot.x - sourceCircleDot.x;
                double offsetY = newCircleDot.y - sourceCircleDot.y;
                //将新标注圆复制到原始标注圆上
                newCircleSet.MoveFirst();
                while (!newCircleSet.IsEOF())
                {
                    IGeometry geo = null;
                    newCircleSet.GetGeometry(out geo);
                    IGeoVarLine varLine = geo as IGeoVarLine;
                    mcGeoVarLine newVarLine = new mcGeoVarLine();
                    mcDots dots = null;
                    varLine.Get2Dots(out dots);
                    for (int i = 0; i < dots.count; i++)
                    {
                        mcDot dot = dots.get_item(i);
                        string strDot = dot.x.ToString().Substring(2);
                        dot.x = double.Parse(strDot);
                        dot.x -= offsetX;
                        dot.y -= offsetY;
                        newVarLine.Append2D(dot);
                    }
                    sourceVcls.Append(newVarLine, null, null);
                    newCircleSet.MoveNext();
                }


                //找到图例外包矩形内的标注,并将标注复制到原始数据图层中

                IXGroupLayer AnnoLayer = null;
                string WT = MapGIsK9Utils.getLayerName(".WT", layerNameListNew);
                AnnoLayer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(WT) as IXGroupLayer;
                IVectorCls AnnoVcls = AnnoLayer.get_Layer(1).XClass as IVectorCls;
                mcRecordSet newLegendAnnoSet = new mcRecordSet();
                AnnoVcls.Select(null, out newLegendAnnoSet);
                newLegendAnnoSet.MoveFirst();
                while (!newLegendAnnoSet.IsEOF())
                {
                    IGeometry legendAnoo_Result = null;
                    mcTextAnno legendAnoo_TextAnno = null;
                    newLegendAnnoSet.GetGeometry(out legendAnoo_Result);
                    legendAnoo_TextAnno = legendAnoo_Result as mcTextAnno;
                    string pattern = @"\d+、";
                    if (Regex.IsMatch(legendAnoo_TextAnno.Text, pattern))
                    {
                        string strDot = legendAnoo_TextAnno.AnchorDot.x.ToString().Substring(2);//坐标变化，去除带号38
                        legendAnoo_TextAnno.AnchorDot.x = double.Parse(strDot);
                        legendAnoo_TextAnno.AnchorDot.x -= offsetX;
                        legendAnoo_TextAnno.AnchorDot.y -= offsetY;
                        sourceLengendAnnoVcls.Append(legendAnoo_TextAnno, null, null);
                    }
                    newLegendAnnoSet.MoveNext();
                }

                mcQueryDef Annodef = new mcQueryDef();
                mcGeoPolygon RectPolygn = MapGIsK9Utils.GetRectPolygon(newCircleRect);
                Annodef.set_Spatial(RectPolygn, meSpaQueryMode.meModeContain);
                mcRecordSet AnnoInCircleSet = new mcRecordSet();
                AnnoVcls.Select(Annodef, out AnnoInCircleSet);
                //将新数据的标注点复制到原始数据中来
                AnnoInCircleSet.MoveFirst();
                while (!AnnoInCircleSet.IsEOF())
                {
                    IGeometry PntGeo = null;
                    AnnoInCircleSet.GetGeometry(out PntGeo);
                    mcTextAnno textAnno = PntGeo as mcTextAnno;
                    string strDot = textAnno.AnchorDot.x.ToString().Substring(2);//坐标变化，去除带号38
                    textAnno.AnchorDot.x = double.Parse(strDot);
                    textAnno.AnchorDot.x -= offsetX;
                    textAnno.AnchorDot.y -= offsetY;
                    sourceLengendAnnoVcls.Append(textAnno, null, null);
                    AnnoInCircleSet.MoveNext();
                }

            LengendCircleLineForUpdateCls.Close();

            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string TempMPJPath = AppPath.Replace("bin\\x86\\Debug\\", "TempMPJ\\");
            string TempMPJPath = AppPath + "TempMPJ\\";
            axMxWorkSpace1.SaveAs(TempMPJPath + axMxWorkSpace1.MapCollection.get_Item(0).Name + "-AnnoCircleU.MPJ");

            GDB.Close();
            GDBSvr.DisConnect();
            MessageBox.Show("图形注记更新完成！");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        }

        private void btnUpdateAccessTable_Click(object sender, EventArgs e)
        {
            if (DBPath.Equals(""))
            {
                MessageBox.Show("请选择需要更新的Access文件！");
                return;
            }
            UpdateAccessTableDlg uatdlg = new UpdateAccessTableDlg(annoMappingDic, sourceAttr, newAttr, DBPath);
            uatdlg.ShowDialog();
        }

        //利用新数据图形部分更新原始数据图形部分
        Dictionary<Dictionary<string, string>, Dictionary<string, string>> annoMappingDic = null;
        private void btnSourceAnno_Click(object sender, EventArgs e)
        {
            //初始化要传入Access更新的页面中
            annoMappingDic = new Dictionary<Dictionary<string, string>, Dictionary<string, string>>();
            try
            {
                mcGDBServer GDBSvr = null;
                mcGDataBase GDB = null;
                mcGeoLines CKLines = new mcGeoLines();
                GDBSvr = new mcGDBServer();
                GDBSvr.Connect("MapGislocal", "", "");
                GDB = GDBSvr.get_gdb("TEMPDATABASE");
                //获取原始数据块段图层
                IXGroupLayer sourceKDLayer = null;
                string HCKD_WP = MapGIsK9Utils.getLayerName("_核查块段.WP", layerNameListSource);
                sourceKDLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(HCKD_WP) as IXGroupLayer;

                IVectorCls sourceKDVCLS = sourceKDLayer.get_Layer(1).XClass as IVectorCls;
                //获取待删除边的ID集合
                List<int> idList = new List<int>();
                IVectorCls sourceKDVCLS_Line = sourceKDLayer.get_Layer(2).XClass as IVectorCls;
                mcRecordSet lineSetForDelete = null;
                sourceKDVCLS_Line.Select(null, out lineSetForDelete);
                lineSetForDelete.MoveFirst();
                while (!lineSetForDelete.IsEOF())
                {
                    mcObjectID oID = null;
                    lineSetForDelete.GetID(out oID);
                    idList.Add(oID.Int);
                    lineSetForDelete.MoveNext();
                }
                //获取新数据块段图层
                IXGroupLayer newKDLayer = null;
                string WP = MapGIsK9Utils.getLayerName(".WP", layerNameListNew);
                newKDLayer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(WP) as IXGroupLayer;
                IVectorCls newKDVCLS = newKDLayer.get_Layer(1).XClass as IVectorCls;

                //对新数据块段进行过滤，去除非块段数据
                mcQueryDef newKDQuerDef = new mcQueryDef();
                newKDQuerDef.Filter = "mpArea > 10";
                mcRecordSet newKDSet = new mcRecordSet();
                newKDVCLS.Select(newKDQuerDef, out newKDSet);

                //循环用原始数据的每一个块段与新数据块段进行叠加，利用叠加后的面积判断块段变化情况
                mcSFeatureCls sourceKDForOverlayVCLS = null;
                mcSFeatureCls newKDForOverLayVCLS = null;
                mcSFeatureCls destOverlayVCLS = null;
                mcSpatialAnalysis2 SpatialAnalysis = new mcSpatialAnalysis2();

                sourceKDForOverlayVCLS = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
                newKDForOverLayVCLS = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;

                //打开需要更新标注圆图层
                IXGroupLayer sourceKDAnnoLayer = null;
                string AnnoCircleWT = MapGIsK9Utils.getLayerName("_标注圆.WT", layerNameListSource);
                sourceKDAnnoLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(AnnoCircleWT) as IXGroupLayer;
                IVectorCls sourceKDAnnoVCLS = sourceKDAnnoLayer.get_Layer(1).XClass as IVectorCls;

                IXGroupLayer sourceCLLXLayer = null;
                string CLLX_WL = MapGIsK9Utils.getLayerName("_储量类型.WL", layerNameListSource);
                sourceCLLXLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(CLLX_WL) as IXGroupLayer;
                IVectorCls sourceCLLXVCLS = sourceCLLXLayer.get_Layer(1).XClass as IVectorCls;

                IXGroupLayer sourceCKQLayer = null;
                string CK_WL = MapGIsK9Utils.getLayerName("_采空区?.WL", layerNameListSource);
                sourceCKQLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(CK_WL) as IXGroupLayer;
                IVectorCls sourceCKQVCLS = sourceCKQLayer.get_Layer(1).XClass as IVectorCls;

                IXGroupLayer sourceHCKDLayer = null;
                string HCKD_WL = MapGIsK9Utils.getLayerName("_核查块段.WL", layerNameListSource);
                sourceHCKDLayer = axMxWorkSpace1.MapCollection.get_Item(0).get_Layer2(HCKD_WL) as IXGroupLayer;
                IVectorCls sourceHCKDVCLS = sourceHCKDLayer.get_Layer(1).XClass as IVectorCls;

                //获取新旧标注圆中标注项间的对应关系
                Dictionary<string, string> tableRealation = getAnnoRelationTable();

                mcRecordSet sourceKDSet = new mcRecordSet();
                sourceKDVCLS.Select(null, out sourceKDSet);
                sourceKDSet.MoveFirst();
                while (!sourceKDSet.IsEOF())
                {
                    sourceKDForOverlayVCLS.Create("sourceKDForOverlay" + new Random().Next(), meGeomConstrainType.mefReg, 0, 0, null);
                    IGeometry sourceGeo = null;
                    mcObjectID sourceID = null;
                    sourceKDSet.GetGeometry(out sourceGeo);
                    sourceKDSet.GetID(out sourceID);
                    mcGeoPolygon sourcePolygon = sourceGeo as mcGeoPolygon;
                    sourceKDForOverlayVCLS.Append(sourcePolygon, null, null);

                    //用于记录相交部分最大面积块段的ID
                    double maxArea = Double.MinValue;
                    mcObjectID newMaxAreaID = new mcObjectID();
                    newKDSet.MoveFirst();
                    while (!newKDSet.IsEOF())
                    {
                        newKDForOverLayVCLS.Create("newKDForOverLay" + new Random().Next(), meGeomConstrainType.mefReg, 0, 0, null);
                        IGeometry newGeo = null;
                        newKDSet.GetGeometry(out newGeo);
                        mcObjectID newID = null;
                        newKDSet.GetID(out newID);
                        mcGeoPolygon newPolygon = newGeo as mcGeoPolygon;
                        newKDForOverLayVCLS.Append(newPolygon, null, null);

                        //需要先对新数据做全图平移变换
                        mcTransParams TransParams = null;
                        TransParams = new mcTransParams();
                        TransParams.AddTransType(meTransType.meMove);
                        TransParams.dx = -38000000;
                        newKDForOverLayVCLS.Transfrom(TransParams);

                        destOverlayVCLS = GDB.get_XClass(meXClsType.meXSFCls) as mcSFeatureCls;
                        destOverlayVCLS.Create("destOverlayVCLS" + new Random().Next(), meGeomConstrainType.mefReg, 0, 0, null);
                        //原始数据与新数据的叠加操作
                        SpatialAnalysis.OverLay(sourceKDForOverlayVCLS, newKDForOverLayVCLS, destOverlayVCLS, meOverlayType.OVLY_INTER, null);

                        mcRecordSet overLayRecordSet = new mcRecordSet();
                        destOverlayVCLS.Select(null, out overLayRecordSet);
                        //根据叠加后的面积，找到与原始块段匹配的新块段
                        overLayRecordSet.MoveFirst();
                        while (!overLayRecordSet.IsEOF())
                        {
                            IGeometry restGeo = null;
                            overLayRecordSet.GetGeometry(out restGeo);
                            mcGeoPolygon resultPoly = restGeo as mcGeoPolygon;
                            double area_INTER = resultPoly.CalArea(null);
                            double area_Source = sourcePolygon.CalArea(null);
                            double areaRatio = area_INTER / area_Source;
                            //求相交部分面积与原始面积的比例，小于10%就认为没有相交，或者是边界误差产生的
                            if ((area_INTER > maxArea) && (areaRatio > 0.1))
                            {
                                maxArea = area_INTER;
                                newMaxAreaID = newID;
                            }
                            overLayRecordSet.MoveNext();
                        }
                        destOverlayVCLS.Close();
                        newKDForOverLayVCLS.Close();
                        newKDSet.MoveNext();
                    }

                    //获取原始数据中块段对应的块段标注圆
                    KeyValuePair<mcDot, Dictionary<string, string>> sourceAnnoCircleSet = new KeyValuePair<mcDot, Dictionary<string, string>>();
                    IGeometry sourceAnnoGeo = null;
                    mcObjectID sourceAnnoID = null;
                    sourceAnnoCircleSet = KDAnnoMapping(axMxWorkSpace1, SourceAnnoDictionSet, AnnoCircleWT, sourceGeo, out sourceAnnoGeo, out sourceAnnoID);
                    //获取新数据中块段对应的块段标注圆
                    KeyValuePair<mcDot, Dictionary<string, string>> newAnnoCircleSet = new KeyValuePair<mcDot, Dictionary<string, string>>();
                    IGeometry newAnnoGeo = null;
                    mcObjectID newAnnoID = null;
                    IGeometry newMappingGeo = null;
                    newKDVCLS.GetGeometry(newMaxAreaID, out newMappingGeo);
                    string WT = MapGIsK9Utils.getLayerName(".WT", layerNameListNew);
                    newAnnoCircleSet = KDAnnoMapping(axMxWorkSpaceNew, NewAnnoDictionSet, WT, newMappingGeo, out newAnnoGeo, out newAnnoID);

                    //将对应好关系的标注圆存储起来
                    annoMappingDic.Add(sourceAnnoCircleSet.Value, newAnnoCircleSet.Value);

                    //更新相对应块段里面的块段编号
                    mcTextAnno newText = (mcTextAnno)newAnnoGeo;
                    mcTextAnno sourceText = (mcTextAnno)sourceAnnoGeo;
                    sourceText.Text = newText.Text;
                    sourceKDAnnoVCLS.Update(sourceAnnoID, sourceText, null, null);

                    //更新对应的块段图形信息
                    mcRecord newRd = null;
                    IGeometry newGe = null;
                    IGeomInfo newGeInfo = null;
                    mcDots newDots = new mcDots();
                    mcGeoVarLine newGeoLine = new mcGeoVarLine();
                    mcGeoLines newGeoLines = new mcGeoLines();
                    mcGeoPolygon newGeoPolygon = new mcGeoPolygon();
                    newKDVCLS.Get(newMaxAreaID, out newGe, out newRd, out newGeInfo);
                    //坐标平移操作
                    mcGeoPolygon mgp = (mcGeoPolygon)newGe;
                    int circleNum = mgp.GetCircleNum();
                    for (int i = 0; i < circleNum; i++)
                    {
                        mcGeoLines geoLines = null;
                        mgp.Get(i, out geoLines);
                        int lineNum = geoLines.GetNum();
                        for (int j = 0; j < lineNum; j++)
                        {
                            IGeoLine geoLine = null;
                            geoLines.GetLine(j, out geoLine);

                            mcDots pDots = null;
                            geoLine.Get2Dots(out pDots);
                            int dotCount = pDots.count;
                            for (int d = 0; d < dotCount; d++)
                            {
                                mcDot dot = pDots.get_item(d);
                                dot.x = dot.x - 38000000;
                                dot.y = dot.y;
                                newDots.Add(dot);
                            }
                            newGeoLine.SetDots2D(newDots);
                        }
                        newGeoLines.Append(newGeoLine);
                        //如果新数据块段属于采空块段，则记录相应块段边更新原始数据
                        if (newAnnoCircleSet.Value.ContainsValue("采空"))
                        {
                            CKLines.Append(newGeoLine);
                        }
                    }
                    newGeoPolygon.Append(newGeoLines);
                    sourceKDVCLS.Update(sourceID, (IGeometry)newGeoPolygon, newRd, newGeInfo);

                    //找到图例圆的中心点坐标
                    string LegendWL = MapGIsK9Utils.getLayerName("_图例.WL", layerNameListSource);
                    mcRecordSet legend_Recordset = MapGIsK9Utils.GetLayerRecordSet(LegendWL, axMxWorkSpace1);
                    mcDots LegendCircleDots = MapGIsK9Utils.GetCircleCoor(legend_Recordset);
                    mcDot legendCircleCoor = LegendCircleDots.get_item(0);

                    mcDot AnnoCircleDot = sourceAnnoCircleSet.Key;
                    //计算标注圆与图例标注圆的坐标偏移
                    double offsetX = AnnoCircleDot.x - legendCircleCoor.x;
                    double offsetY = AnnoCircleDot.y - legendCircleCoor.y;
                    //存储了与原始数据圆对应的新数据圆中的数据
                    Dictionary<string, string> NewCireclDic = newAnnoCircleSet.Value;

                    mcRecordSet anno_Recordset = MapGIsK9Utils.GetLayerRecordSet(AnnoCircleWT, axMxWorkSpace1);
                    NewRegRecordSetForUpdate.MoveFirst();
                    while (!NewRegRecordSetForUpdate.IsEOF())
                    {
                        mcRecord record = null;
                        NewRegRecordSetForUpdate.GetAtt(out record);
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

                        //计算标注圆的小矩形范围
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
                            mcObjectID updataID = null;
                            anno_Recordset.GetID(out updataID);
                            TextAnno = geo as mcTextAnno;
                            mcDot AnchorDot = new mcDot();
                            AnchorDot = TextAnno.AnchorDot;
                            if (MapGIsK9Utils.isDotInRect(AnchorDot, rect))
                            {
                                //先根据对应关系找到原始数据对应的新数据编号
                                string NewID = null;
                                tableRealation.TryGetValue(AnnoID.ToString(), out NewID);
                                string updataData = null;
                                NewCireclDic.TryGetValue(NewID, out updataData);
                                //替换数据
                                TextAnno.Text = updataData;
                                IGeometry updateGeo = TextAnno as IGeometry;
                                int s = sourceKDAnnoVCLS.Update(updataID, updateGeo, null, null);
                            }
                            anno_Recordset.MoveNext();
                        }
                        NewRegRecordSetForUpdate.MoveNext();
                    }
                    sourceKDForOverlayVCLS.Close();
                    sourceKDSet.MoveNext();
                }

                //删除原始块段中未更新的块段边
                IVectorCls sourceKDVCLS_Line_After = (IVectorCls)sourceKDLayer.get_Layer(2).XClass;
                mcRecordSet recordSet_After = new mcRecordSet();
                sourceKDVCLS_Line_After.Select(null, out recordSet_After);
                recordSet_After.MoveFirst();
                while (!recordSet_After.IsEOF())
                {
                    mcObjectID dID = null;
                    recordSet_After.GetID(out dID);
                    if (idList.Contains(dID.Int))
                    {
                        sourceKDVCLS_Line_After.Del(dID);
                    }
                    recordSet_After.MoveNext();
                }


                //更新资源储量图层
                sourceCLLXVCLS.Clear();
                mcRecordSet addToCLLXSet = null;
                sourceKDVCLS_Line_After.Select(null, out addToCLLXSet);
                sourceCLLXVCLS.AddSet(addToCLLXSet);

                //更新采空区图层
                sourceCKQVCLS.Clear();
                for (int CK = 0; CK < CKLines.GetNum(); CK++)
                {
                    IGeoLine CKLine = null;
                    CKLines.GetLine(CK, out CKLine);
                    sourceCKQVCLS.Append((IGeometry)CKLine, null, null);
                }

                //更新核查块段图层
                sourceHCKDVCLS.Clear();
                sourceHCKDVCLS.AddSet(addToCLLXSet);

                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                //string TempMPJPath = AppPath.Replace("bin\\x86\\Debug\\", "TempMPJ\\");
                string TempMPJPath = AppPath + "TempMPJ\\";
                axMxWorkSpace1.SaveAs(TempMPJPath + axMxWorkSpace1.MapCollection.get_Item(0).Name + "-GeoU.MPJ");

                GDB.Close();
                GDBSvr.DisConnect();
                MessageBox.Show("更新完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        //获取新旧标注圆间标注项之间的对应关系
        private Dictionary<string, string> getAnnoRelationTable()
        {
            //打开新旧标注对应关系表，找到新旧标注对应关系
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
            string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
            OleDbConnection MapConn = AccessUtils.GetConn(MapDBPath);
            Dictionary<string, string> tableRealation = new Dictionary<string, string>();//对应关系表
            for (int i = 0; i < sourceAttr.Count; i++)
            {
                string value = sourceAttr[i].Split('、')[1];
                //找到对应关系
                string Anno_Mapping = "select sourceAnno,newAnno from 标注映射表";
                OleDbDataReader mapping_Adr = null;
                mapping_Adr = AccessUtils.GetDataReader(Anno_Mapping, MapConn);
                while (mapping_Adr.Read())
                {
                    bool flag = false;
                    List<string> sourceList = new List<string>(mapping_Adr["sourceAnno"].ToString().Split('、'));
                    if ( sourceList.Contains(value))//匹配上以后，将对应ID存起来
                    {
                        string newValue = mapping_Adr["newAnno"].ToString();
                        List<string> newList = new List<string>(newValue.Split('、'));
                        for (int j = 0; j < newAttr.Count; j++)
                        {
                            string newAttrValue = newAttr[j].Split('、')[1];
                            if (newList.Contains(newAttrValue))
                            {
                                tableRealation.Add(sourceAttr[i].Split('、')[0], newAttr[j].Split('、')[0]);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                    }
                }

            }
            MapConn.Close();
            return tableRealation;
        }

        //数据块段与块段标注圆对应,返回对应的标注圆
        private KeyValuePair<mcDot, Dictionary<string, string>> KDAnnoMapping(AxWorkSpace.AxMxWorkSpace workSpace, Dictionary<mcDot, Dictionary<string, string>> AnnoDicSet, 
            string LayerName, IGeometry queryGeo,out IGeometry annoGeo,out mcObjectID annoID)
        {
            KeyValuePair<mcDot, Dictionary<string, string>> Pair = new KeyValuePair<mcDot,Dictionary<string,string>>();
            //获取该块段内的所有标注数据，利用块段编号找到本块段对应的标注
            IXGroupLayer sourceKDAnnoLayer = null;
            sourceKDAnnoLayer = workSpace.MapCollection.get_Item(0).get_Layer2(LayerName) as IXGroupLayer;
            IVectorCls sourceKDAnnoVCLS = sourceKDAnnoLayer.get_Layer(1).XClass as IVectorCls;
            mcQueryDef queryKDAnnoDef = new mcQueryDef();
            queryKDAnnoDef.set_Spatial(queryGeo, meSpaQueryMode.meModeMBRIntersect);
            mcRecordSet annoInKDSet = null;
            sourceKDAnnoVCLS.Select(queryKDAnnoDef, out annoInKDSet);
            IGeometry outGeo = null;
            mcObjectID textInKDID = null;
            annoInKDSet.MoveFirst();
            while (!annoInKDSet.IsEOF())
            {
                bool flag = false;
                IGeometry pntAnnoGeo = null;
                annoInKDSet.GetGeometry(out pntAnnoGeo);
                
                annoInKDSet.GetID(out textInKDID);
                mcTextAnno textAnno = pntAnnoGeo as mcTextAnno;

                //获取对应块段的标注圆数据
                foreach (KeyValuePair<mcDot, Dictionary<string, string>> kvp in AnnoDicSet)
                {
                    string KDBH = "";
                    kvp.Value.TryGetValue("1", out KDBH);
                    //判断块段内标注是否和标注圆一样，找到块段对应的标注圆
                    if (textAnno.Text.Equals(KDBH))
                    {
                        outGeo = pntAnnoGeo;
                        Pair = kvp;
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }

                annoInKDSet.MoveNext();
            }
            annoID = textInKDID;
            annoGeo = outGeo;
            return Pair;
        }

        private void btnNewAnno_Click(object sender, EventArgs e)
        {
            axMxEditControl1.StartTool("SaveRegLayer", 1, 1);
            
        }


        //提取图例圆
        private IPntInfo getLegendCircle(out mcDot LengendCircleDot, out mcRect CircleRect)
        {
            // 1.2、找到图例圆
            LengendCircleDot = new mcDot();
            CircleRect = new mcRect();
            IPntInfo pntInfo = null;
            IXGroupLayer m_Layer = null;
            string WT = MapGIsK9Utils.getLayerName(".WT", layerNameListNew);
            m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(WT) as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            if (vcls == null)
            {
                vcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            }
            IVectorCls AnnoVcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            mcRecordSet allCircle = null;
            vcls.Select(null, out allCircle);
            //找图例圆操作
            allCircle.MoveFirst();
            while (!allCircle.IsEOF())
            {
                mcObjectID id = null;
                allCircle.GetID(out id);
                IGeomInfo geoInfo = null;
                allCircle.GetInfo(out geoInfo);
                if (geoInfo != null)
                {
                    mcDot _LengendCircleDot = new mcDot();
                    mcRect _CircleRect = new mcRect(); 
                    IPntInfo _pntInfo = geoInfo as IPntInfo;//图例圆的图形信息
                    if (_pntInfo.height > 20 && _pntInfo.width > 20)
                    {
                        IGeometry circle = null;
                        allCircle.GetGeometry(out circle);
                        circle.CalRect(out _CircleRect);
                        _CircleRect.xmin -= (_pntInfo.width / 2);
                        _CircleRect.xmax += (_pntInfo.width / 2);
                        _CircleRect.ymin -= (_pntInfo.height / 2);
                        _CircleRect.ymax += (_pntInfo.height / 2);

                        mcRecordSet innerLegendAnno = null;
                        mcQueryDef innerAnnoDef = new mcQueryDef();
                        mcGeoPolygon queryPolygon = MapGIsK9Utils.GetRectPolygon(_CircleRect);
                        innerAnnoDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
                        AnnoVcls.Select(innerAnnoDef, out innerLegendAnno);
                        List<string> innerAnnoList = new List<string>();
                        innerLegendAnno.MoveFirst();
                        while (!innerLegendAnno.IsEOF())
                        {
                            IGeometry innerGeo = null;
                            innerLegendAnno.GetGeometry(out innerGeo);
                            mcTextAnno innerAnno = (mcTextAnno)innerGeo;
                            innerAnnoList.Add(innerAnno.Text);
                            innerLegendAnno.MoveNext();
                        }
                        if (innerAnnoList.Contains("1") && innerAnnoList.Contains("2") && innerAnnoList.Contains("3"))
                        {
                            //确定右下角图例圆的中心点坐标
                            _LengendCircleDot.x = (_CircleRect.xmin + _CircleRect.xmax) / 2;
                            _LengendCircleDot.y = (_CircleRect.ymin + _CircleRect.ymax) / 2;
                            CircleRect = _CircleRect;
                            LengendCircleDot = _LengendCircleDot;
                            pntInfo = _pntInfo;
                            break;
                        }

                    }
                }
                allCircle.MoveNext();
            }
            return pntInfo;
        }


        //提取标注圆
        private void getAnnoCircleDots(mcDots AnnoCircleDots)
        {
            // 1.2、找到图例圆
            IPntInfo pntInfo = null;
            IXGroupLayer m_Layer = null;
            string WT = MapGIsK9Utils.getLayerName(".WT", layerNameListNew);
            m_Layer = axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer2(WT) as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            IVectorCls AnnoVcls = m_Layer.get_Layer(1).XClass as IVectorCls;
            mcRecordSet allCircle = null;
            vcls.Select(null, out allCircle);
            //找图例圆操作
            allCircle.MoveFirst();
            while (!allCircle.IsEOF())
            {
                mcObjectID id = null;
                allCircle.GetID(out id);
                IGeomInfo geoInfo = null;
                allCircle.GetInfo(out geoInfo);
                if (geoInfo != null)
                {

                    pntInfo = geoInfo as IPntInfo;//图例圆的图形信息
                    if (pntInfo.height > 20 && pntInfo.width > 20)
                    {
                        IGeometry circle = null;
                        mcRect CircleRect = null;
                        allCircle.GetGeometry(out circle);
                        circle.CalRect(out CircleRect);
                        CircleRect.xmin -= (pntInfo.width / 2);
                        CircleRect.xmax += (pntInfo.width / 2);
                        CircleRect.ymin -= (pntInfo.height / 2);
                        CircleRect.ymax += (pntInfo.height / 2);

                        mcRecordSet innerLegendAnno = null;
                        mcQueryDef innerAnnoDef = new mcQueryDef();
                        mcGeoPolygon queryPolygon = MapGIsK9Utils.GetRectPolygon(CircleRect);
                        innerAnnoDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
                        AnnoVcls.Select(innerAnnoDef, out innerLegendAnno);
                        List<string> innerAnnoList = new List<string>();
                        innerLegendAnno.MoveFirst();
                        while (!innerLegendAnno.IsEOF())
                        {
                            IGeometry innerGeo = null;
                            innerLegendAnno.GetGeometry(out innerGeo);
                            mcTextAnno innerAnno = (mcTextAnno)innerGeo;
                            innerAnnoList.Add(innerAnno.Text);
                            innerLegendAnno.MoveNext();
                        }
                        if (!innerAnnoList.Contains("1") && !innerAnnoList.Contains("2") && !innerAnnoList.Contains("3"))
                        {
                            //确定标注圆的中心点坐标
                            mcDot CircleDot = new mcDot();
                            CircleDot.x = (CircleRect.xmin + CircleRect.xmax) / 2;
                            CircleDot.y = (CircleRect.ymin + CircleRect.ymax) / 2;
                            AnnoCircleDots.Add(CircleDot);
                        }

                    }
                }
                allCircle.MoveNext();
            }
        }

        //文件更新子系统模块
        private void btnFileUpdateSystem_Click(object sender, EventArgs e)
        {
            MapGIS2005.MainForm mf = new MapGIS2005.MainForm();
            mf.ShowDialog();
        }

        string DBPath = "";
        private void btnSetAPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access文件|*.mdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DBPath = ofd.FileName;
            }
        }

        //帮助提示信息
        private void btnInforMessage_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("湖南省地质科学研究院版权所有！\r\n 联系电话： \r\n 联系人：\r\n CopyRight 2015-2017", "湖南省地质科学研究院", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //图形数据批量更新
        public Dictionary<String, String> projTable = null;
        public String sProjPath = "";
        public String nProjPath = "";
        private void btnGeoBatch_Click(object sender, EventArgs e)
        {
            GeoBatchUpdateDlg gbuDlg = new GeoBatchUpdateDlg(this);
            if (gbuDlg.ShowDialog() == DialogResult.OK)
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
                string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
                OleDbConnection logConn = AccessUtils.GetConn(MapDBPath);
                try
                {

                    foreach (string key in projTable.Keys)
                    {
                        //打开原始工程
                        axMxWorkSpace1.Open(sProjPath +"\\" + key, WorkSpace.EnumOpenMode.OpenNormal);
                        //获取图层名称集合
                        layerNameListSource = new List<string>();
                        int layerCount = axMxWorkSpace1.MapCollection.get_Item(0).LayerCount;
                        for (int i = 1; i <= layerCount; i++)
                        {
                            layerNameListSource.Add(axMxWorkSpace1.MapCollection.get_Item(0).get_Layer(i).LayerName);
                        }
                        axMapXView1.Restore();

                        //打开新工程
                        string nProjName = "";
                        projTable.TryGetValue(key, out nProjName);
                        string dirction = nProjName.Split('.')[0];
                        string nPath = nProjPath +"\\" +  dirction + "\\" + nProjName;
                        axMxWorkSpaceNew.Open(nPath, WorkSpace.EnumOpenMode.OpenNormal);
                        //获取图层名称集合
                        layerNameListNew = new List<string>();
                        int nlayerCount = axMxWorkSpaceNew.MapCollection.get_Item(0).LayerCount;
                        for (int i = 1; i <= nlayerCount; i++)
                        {
                            layerNameListNew.Add(axMxWorkSpaceNew.MapCollection.get_Item(0).get_Layer(i).LayerName);
                        }
                        axMapXViewNew.Restore();

                        this.btnExtractAnnoSource_Click(null, null);
                        this.btnExtractAnnoNew_Click(null, null);
                        this.btnSourceAnno_Click(null, null);

                        //this.axMxWorkSpace1.Close(EnumCloseMode.NoDlgSave);
                        //this.axMxWorkSpaceNew.Close(EnumCloseMode.NoDlgSave);

                        //匹配核查块段表
                        string MapSQL = "insert into 更新工程记录(`Time`,`SourceProj`,`NewProj`) values('" + DateTime.Now.ToLocalTime().ToString() + "','" +
                            key + "','" + nProjName + "')";
                        OleDbCommand insert = new OleDbCommand(MapSQL, logConn);
                        insert.ExecuteNonQuery();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    logConn.Close();
                }
                logConn.Close();
            }
        }

        /// <summary>
        /// 通用编辑功能区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string ProjFlag = "";
        private void btnModifyString_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn","");
            startTool(ProjFlag,tool);
        }

        private void btnMovePnt_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        //打开工具函数
        private void startTool(string flag,string tool)
        {
            SelectSourceNewProj ssnp = new SelectSourceNewProj(this);
            if (ssnp.ShowDialog() == DialogResult.OK)
            {
                if ("Source".Equals(flag))
                {
                    axMxEditControl1.StartTool(tool, 0, 0);
                }
                else
                {
                    axMxEditControlNew.StartTool(tool, 0, 0);
                }
            }
        }

        private void btnInputAnnoKeyb_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnDeletePnt_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnModifyAnnAtrru_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnSelectLin_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnMoveLine_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnInputBrokenLine_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnCopyLine_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnDeleteLine_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnInputRegParamSet_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnRegUnion_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnCopyReg_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnEditRegAtt_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        private void btnDeleteReg_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btn = (DevComponents.DotNetBar.ButtonItem)sender;
            string tool = btn.Name.Replace("btn", "");
            startTool(ProjFlag, tool);
        }

        //浏览更新记录
        private void btnUpdateLog_Click(object sender, EventArgs e)
        {
            UpdateLogDlg uld = new UpdateLogDlg();
            uld.ShowDialog();

        }

















  



    }
}