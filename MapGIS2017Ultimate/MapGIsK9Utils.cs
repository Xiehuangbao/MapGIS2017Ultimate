using System;
using System.Collections.Generic;
using System.Text;
using mapXBase;
using mc_basObj7Lib;
using mc_basXcls7Lib;
using mc_Spc_Anly70Lib;
using System.Text.RegularExpressions;
using mx_MapLibCtrlLib;
using System.Data.OleDb;
using WorkSpace;
namespace MapGIS2017Ultimate
{
    class MapGIsK9Utils
    {
        //打开图层，获取RecordSet
        public static mcRecordSet GetLayerRecordSet(string layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            IXGroupLayer layer = null;
            mcRecordSet RecordSet = null;
            layer = WorkSpace.MapCollection.get_Item(0).get_Layer2(layername) as IXGroupLayer;
            IVectorCls vcls = layer.get_Layer(1).XClass as IVectorCls;
            vcls.Select(null, out RecordSet);
            return RecordSet;
        }


        //打开图层，获取IVecls
        public static IVectorCls GetVectorCls(string layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            IXGroupLayer layer = null;
            layer = WorkSpace.MapCollection.get_Item(0).get_Layer2(layername) as IXGroupLayer;
            IVectorCls vcls = layer.get_Layer(1).XClass as IVectorCls;
            return vcls;
        }
        //获取标注圆的圆心坐标
        public static mcDots GetCircleCoor(mcRecordSet recordset)
        {
            mcDots circleDots = new mcDots();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //找到图例中的注记圆
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //判断是否是注记圆
                if (width == heigth)
                {
                    //计算保留图例圆的中心点坐标
                    mcDot CircleDot = new mcDot();
                    CircleDot.x = (rect.xmax + rect.xmin) / 2;
                    CircleDot.y = (rect.ymin + rect.ymax) / 2;
                    circleDots.Add(CircleDot);
                }
                SfcID = recordset.MoveNext();
            }
            return circleDots;
        }

        //获取标注圆内的注记集合
        public static List<mcRecordSet> GetCircleAnnos(mcRecordSet recordset, mcRecordSet destRecordSet)
        {
            List<mcRecordSet> AnnoInCircles = new List<mcRecordSet>();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //找到图例中的注记圆
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //判断是否是注记圆
                if (Math.Abs(width - heigth) < 5)
                {
                    //获取单个标注圆内的注记
                    mcRecordSet AnnoInCircle = null;
                    mcQueryDef queryDef = new mcQueryDef();
                    queryDef.set_rect(rect, meSpaQueryMode.meModeContain);
                    destRecordSet.Select(queryDef, out AnnoInCircle);
                    AnnoInCircles.Add(AnnoInCircle);
                }
                SfcID = recordset.MoveNext();
            }
            return AnnoInCircles;
        }

        //获取外包矩形的多边形
        public static mcGeoPolygon GetRectPolygon(mcRect rect)
        {
            mcGeoPolygon GeoPolygon = new mcGeoPolygon();
            mcLongList LongList = new mcLongList();
            mcDots regDots = new mcDots();
            mcDot dot1 = new mcDot();
            mcDot dot2 = new mcDot();
            mcDot dot3 = new mcDot();
            mcDot dot4 = new mcDot();
            dot1.x = rect.xmin;
            dot1.y = rect.ymin;
            dot2.x = rect.xmin;
            dot2.y = rect.ymax;
            dot3.x = rect.xmax;
            dot3.y = rect.ymax;
            dot4.x = rect.xmax;
            dot4.y = rect.ymin;
            regDots.Add(dot1);
            regDots.Add(dot2);
            regDots.Add(dot3);
            regDots.Add(dot4);
            LongList.Append(4);
            GeoPolygon.SetDots(regDots, LongList);
            return GeoPolygon;
        }
        //延长线函数
        public static void ExpandLine(mcRecordSet LineSet, IVectorCls vcls)
        {
            IGeometry l_geoLine = null;
            LineSet.GetGeometry(out l_geoLine);
            IGeoVarLine l_Line = null;
            l_Line = l_geoLine as IGeoVarLine;
            mcDots l_dots = null;
            l_Line.Get2Dots(out l_dots);
            mcDot dot_1 = null;
            mcDot dot_2 = null;

            if (l_dots.get_item(0).x < l_dots.get_item(l_dots.count - 1).x || l_dots.get_item(0).y > l_dots.get_item(l_dots.count - 1).y)
            {
                dot_1 = l_dots.get_item(0);
                dot_2 = l_dots.get_item(l_dots.count - 1);
            }
            else
            {
                dot_2 = l_dots.get_item(0);
                dot_1 = l_dots.get_item(l_dots.count - 1);
            }

            //垂直(因为数据不标准，所以设置为< 1)
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
                vcls.Append(NewLine, null, null);

                mcObjectID delID = null;
                LineSet.GetID(out delID);
                vcls.Del(delID);
            }
            else
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
                vcls.Append(NewLine, null, null);
                mcObjectID delID = null;
                LineSet.GetID(out delID);
                vcls.Del(delID);
            }
        }

        //判断点是否在矩形
        public static bool isDotInRect(mcDot dot, mcRect rect)
        {
            if (dot.x >= rect.xmin && dot.x <= rect.xmax && dot.y >= rect.ymin && dot.y <= rect.ymax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取新数据子图的中心点坐标
        public static mcDots GetCircleCoor_New(mcRecordSet recordset)
        {
            mcDots circleDots = new mcDots();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //找到图例中的注记圆
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //判断是否是注记圆
                if (Math.Abs(width - heigth) < 5)
                {
                    //计算保留图例圆的中心点坐标
                    mcDot CircleDot = new mcDot();
                    CircleDot.x = rect.xmin + (rect.xmax - rect.xmin) / 2;
                    CircleDot.y = rect.ymin + (rect.ymax - rect.ymin) / 2;
                    circleDots.Add(CircleDot);
                }
                SfcID = recordset.MoveNext();
            }
            return circleDots;
        }

        //获取新数据内标注圆的坐标集合
        public static mcDots GetAnnoCircleDots(string frame_layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            mcDots dots = new mcDots();
            //  1、找到最大的图框
            mcRecordSet AllFeatures_RecordSet = GetLayerRecordSet(frame_layername, WorkSpace);
            List<double> mpLengths = new List<double>();
            int sfID = 0;
            sfID = AllFeatures_RecordSet.MoveFirst();
            while (!AllFeatures_RecordSet.IsEOF())
            {
                mcRecord record = null;
                AllFeatures_RecordSet.GetAtt(out record);
                object per = null;
                record.GetFldVal("mpLength", out per);
                mpLengths.Add((double)per);
                sfID = AllFeatures_RecordSet.MoveNext();
            }
            mpLengths.Sort();
            mcQueryDef lengthDef = new mcQueryDef();
            lengthDef.Filter = "mpLength=" + mpLengths[mpLengths.Count - 1];
            mcRecordSet frameRecordSet = null;
            AllFeatures_RecordSet.Select(lengthDef, out frameRecordSet);
            frameRecordSet.MoveFirst();
            IGeometry geoFrame = null;
            frameRecordSet.GetGeometry(out geoFrame);

            // 2、找到图例圆
            IXGroupLayer m_Layer = null;
            m_Layer = WorkSpace.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            mcQueryDef queryDef = new mcQueryDef();
            //图框的外包矩形
            mcRect FrameRect = null;
            geoFrame.CalRect(out FrameRect);

            //计算、保留图例圆的中心点坐标
            mcDot CircleDot = new mcDot();
            mcRect CircleRect = null;//右下角图例圆的外包矩形

            mcGeoPolygon queryPolygon = GetRectPolygon(FrameRect);

            queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            mcRecordSet containCircle = null;
            vcls.Select(queryDef, out containCircle);//图框内的圆
            containCircle.MoveFirst();
            while (!containCircle.IsEOF())
            {
                mcObjectID id = null;
                containCircle.GetID(out id);
                IGeomInfo geoInfo = null;
                containCircle.GetInfo(out geoInfo);
                IPntInfo pntInfo = geoInfo as IPntInfo;
                if (pntInfo.height > 20 && pntInfo.width > 20)
                {
                    IGeometry circle = null;
                    containCircle.GetGeometry(out circle);
                    circle.CalRect(out CircleRect);
                    //确定右下角图例圆的中心点坐标
                    CircleDot.x = (CircleRect.xmin + CircleRect.xmax) / 2;
                    CircleDot.y = (CircleRect.ymin + CircleRect.ymax) / 2;
                    dots.Add(CircleDot);
                }
                containCircle.MoveNext();
            }
            return dots;
        }

        //获取新数据内标注圆的集合
        public static void GetAnnoCircleGeoList(string frame_layername, out List<IGeometry> Anno_CircleGeoList, out List<IGeomInfo> Anno_CircleGeoInfoList, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            List<IGeometry> _Anno_CircleGeoList = new List<IGeometry>();
            List<IGeomInfo> _Anno_CircleGeoInfoList = new List<IGeomInfo>();
            //  1、找到最大的图框
            mcRecordSet AllFeatures_RecordSet = GetLayerRecordSet(frame_layername, WorkSpace);
            List<double> mpLengths = new List<double>();
            int sfID = 0;
            sfID = AllFeatures_RecordSet.MoveFirst();
            while (!AllFeatures_RecordSet.IsEOF())
            {
                mcRecord record = null;
                AllFeatures_RecordSet.GetAtt(out record);
                object per = null;
                record.GetFldVal("mpLength", out per);
                mpLengths.Add((double)per);
                sfID = AllFeatures_RecordSet.MoveNext();
            }
            mpLengths.Sort();
            mcQueryDef lengthDef = new mcQueryDef();
            lengthDef.Filter = "mpLength=" + mpLengths[mpLengths.Count - 1];
            mcRecordSet frameRecordSet = null;
            AllFeatures_RecordSet.Select(lengthDef, out frameRecordSet);
            frameRecordSet.MoveFirst();
            IGeometry geoFrame = null;
            frameRecordSet.GetGeometry(out geoFrame);

            // 2、找到图例圆
            IXGroupLayer m_Layer = null;
            m_Layer = WorkSpace.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            mcQueryDef queryDef = new mcQueryDef();
            //图框的外包矩形
            mcRect FrameRect = null;
            geoFrame.CalRect(out FrameRect);

            //计算、保留图例圆的中心点坐标
            mcDot CircleDot = new mcDot();

            mcGeoPolygon queryPolygon = GetRectPolygon(FrameRect);

            queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            mcRecordSet containCircle = null;
            vcls.Select(queryDef, out containCircle);//图框内的圆
            containCircle.MoveFirst();
            while (!containCircle.IsEOF())
            {
                mcObjectID id = null;
                containCircle.GetID(out id);
                IGeomInfo geoInfo = null;
                containCircle.GetInfo(out geoInfo);
                IPntInfo pntInfo = geoInfo as IPntInfo;
                if (pntInfo.height > 20 && pntInfo.width > 20)
                {
                    IGeometry geoCircle = null;
                    IGeomInfo geoCircleInfo = null;
                    containCircle.GetGeometry(out geoCircle);
                    _Anno_CircleGeoList.Add(geoCircle);
                    containCircle.GetInfo(out geoCircleInfo);
                    _Anno_CircleGeoInfoList.Add(geoCircleInfo);
                }
                containCircle.MoveNext();
            }
            Anno_CircleGeoList = _Anno_CircleGeoList;
            Anno_CircleGeoInfoList = _Anno_CircleGeoInfoList;
        }

        //获取标注圆内的注记集合
        public static List<mcRecordSet> GetCircleAnnosNew(List<IGeometry> geoCircelList, List<IGeomInfo> geoCircelInfoList, mcRecordSet AnnoRecordSet)
        {
            List<mcRecordSet> AnnoInCircles = new List<mcRecordSet>();
            for (int i = 0; i < geoCircelList.Count; i++)
            {
                //获取单个标注圆内的注记
                mcRect circleRect = null;
                geoCircelList[i].CalRect(out circleRect);
                IPntInfo pntInfo = geoCircelInfoList[i] as IPntInfo;
                mcRect ExpandRect = new mcRect();
                ExpandRect.xmin = circleRect.xmin - (pntInfo.width / 2) - 2;
                ExpandRect.ymin = circleRect.ymin - (pntInfo.height / 2) - 2;
                ExpandRect.xmax = circleRect.xmax + (pntInfo.width / 2) + 2;
                ExpandRect.ymax = circleRect.ymax + (pntInfo.height / 2) + 2;
                mcRecordSet AnnoInCircle = null;
                mcQueryDef queryDef = new mcQueryDef();
                queryDef.set_rect(ExpandRect, meSpaQueryMode.meModeContain);
                AnnoRecordSet.Select(queryDef, out AnnoInCircle);
                AnnoInCircles.Add(AnnoInCircle);
            }
            return AnnoInCircles;
        }


        //计算两点之间的距离
        public static double CalDistanceOfCircle(mcDot dot1, mcDot dot2)
        {
            double dis = 0;
            dis = Math.Sqrt(Math.Pow(dot2.y - dot1.y, 2) + Math.Pow(dot2.x - dot1.x, 2));
            return dis;
        }



        //获取标注的属性
        public static List<string> GetLengendAnno(mcRecordSet recordset)
        {
            List<string> Attr = new List<string>();
            recordset.MoveFirst();
            while (!recordset.IsEOF())
            {
                IGeometry legendAnoo_Result = null;
                mcTextAnno legendAnoo_TextAnno = null;
                recordset.GetGeometry(out legendAnoo_Result);
                legendAnoo_TextAnno = legendAnoo_Result as mcTextAnno;
                string pattern = @"\d+、";
                if (Regex.IsMatch(legendAnoo_TextAnno.Text, pattern))
                {
                    Attr.Add(legendAnoo_TextAnno.Text);

                }
                recordset.MoveNext();
            }
            return Attr;

        }


        //获取对应图层
        public static String getLayerName(string pattern,List<String> layerList)
        {
            for (int i = 0; i < layerList.Count;i ++ )
            {
                Regex reg = new Regex(pattern,RegexOptions.IgnoreCase);
                //string str = layerList[i].ToUpper();
                Match match = reg.Match(layerList[i]);
                if (match.Success)
                {
                    return layerList[i];
                }
            }
            return "";
        
        }


        //判断标注字典中是包含当前图例标注
        public static bool isInDictionary(List<string> annoAttr,string accessField,out List<string> addDic) 
        {
            bool flag = true;
            addDic = new List<string>();
            string AppPath = AppDomain.CurrentDomain.BaseDirectory;
            //string MapDBPath = AppPath.Replace("bin\\x86\\Debug\\", "FieldsDictionary\\FieldsDictionary.accdb");
            string MapDBPath = AppPath + "FieldsDictionary\\FieldsDictionary.accdb";
            OleDbConnection MapConn = AccessUtils.GetConn(MapDBPath);
            for (int i = 0; i < annoAttr.Count; i++)
            {
                string value = annoAttr[i].Split('、')[1];
                //找到对应关系
                string Anno_Mapping = "select "+ accessField +" from 标注映射表";
                OleDbDataReader mapping_Adr = null;
                mapping_Adr = AccessUtils.GetDataReader(Anno_Mapping, MapConn);
                bool isContain = false;
                while (mapping_Adr.Read())
                {

                    List<string> mapDic = new List<string>(mapping_Adr[accessField].ToString().Split('、'));
                    if (mapDic.Contains(value))
                    {
                        isContain = true;
                        break;
                    }
                }
                if (!isContain)
                {
                    addDic.Add(value);
                    flag = false;
                }
            }

            return flag;
        
        }





    }
}
