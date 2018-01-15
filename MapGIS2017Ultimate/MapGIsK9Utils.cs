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
        //��ͼ�㣬��ȡRecordSet
        public static mcRecordSet GetLayerRecordSet(string layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            IXGroupLayer layer = null;
            mcRecordSet RecordSet = null;
            layer = WorkSpace.MapCollection.get_Item(0).get_Layer2(layername) as IXGroupLayer;
            IVectorCls vcls = layer.get_Layer(1).XClass as IVectorCls;
            vcls.Select(null, out RecordSet);
            return RecordSet;
        }


        //��ͼ�㣬��ȡIVecls
        public static IVectorCls GetVectorCls(string layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            IXGroupLayer layer = null;
            layer = WorkSpace.MapCollection.get_Item(0).get_Layer2(layername) as IXGroupLayer;
            IVectorCls vcls = layer.get_Layer(1).XClass as IVectorCls;
            return vcls;
        }
        //��ȡ��עԲ��Բ������
        public static mcDots GetCircleCoor(mcRecordSet recordset)
        {
            mcDots circleDots = new mcDots();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //�ҵ�ͼ���е�ע��Բ
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ж��Ƿ���ע��Բ
                if (width == heigth)
                {
                    //���㱣��ͼ��Բ�����ĵ�����
                    mcDot CircleDot = new mcDot();
                    CircleDot.x = (rect.xmax + rect.xmin) / 2;
                    CircleDot.y = (rect.ymin + rect.ymax) / 2;
                    circleDots.Add(CircleDot);
                }
                SfcID = recordset.MoveNext();
            }
            return circleDots;
        }

        //��ȡ��עԲ�ڵ�ע�Ǽ���
        public static List<mcRecordSet> GetCircleAnnos(mcRecordSet recordset, mcRecordSet destRecordSet)
        {
            List<mcRecordSet> AnnoInCircles = new List<mcRecordSet>();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //�ҵ�ͼ���е�ע��Բ
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ж��Ƿ���ע��Բ
                if (Math.Abs(width - heigth) < 5)
                {
                    //��ȡ������עԲ�ڵ�ע��
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

        //��ȡ������εĶ����
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
        //�ӳ��ߺ���
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
                vcls.Append(NewLine, null, null);

                mcObjectID delID = null;
                LineSet.GetID(out delID);
                vcls.Del(delID);
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
                vcls.Append(NewLine, null, null);
                mcObjectID delID = null;
                LineSet.GetID(out delID);
                vcls.Del(delID);
            }
        }

        //�жϵ��Ƿ��ھ���
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

        //��ȡ��������ͼ�����ĵ�����
        public static mcDots GetCircleCoor_New(mcRecordSet recordset)
        {
            mcDots circleDots = new mcDots();
            int SfcID = 0;
            SfcID = recordset.MoveFirst();
            //�ҵ�ͼ���е�ע��Բ
            while (!recordset.IsEOF())
            {
                IGeometry GeoResult = null;
                recordset.GetGeometry(out GeoResult);
                mcRect rect = null;
                GeoResult.CalRect(out rect);
                double width = Math.Round(rect.xmax - rect.xmin, 2);
                double heigth = Math.Round(rect.ymax - rect.ymin, 2);
                //�ж��Ƿ���ע��Բ
                if (Math.Abs(width - heigth) < 5)
                {
                    //���㱣��ͼ��Բ�����ĵ�����
                    mcDot CircleDot = new mcDot();
                    CircleDot.x = rect.xmin + (rect.xmax - rect.xmin) / 2;
                    CircleDot.y = rect.ymin + (rect.ymax - rect.ymin) / 2;
                    circleDots.Add(CircleDot);
                }
                SfcID = recordset.MoveNext();
            }
            return circleDots;
        }

        //��ȡ�������ڱ�עԲ�����꼯��
        public static mcDots GetAnnoCircleDots(string frame_layername, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            mcDots dots = new mcDots();
            //  1���ҵ�����ͼ��
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

            // 2���ҵ�ͼ��Բ
            IXGroupLayer m_Layer = null;
            m_Layer = WorkSpace.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            mcQueryDef queryDef = new mcQueryDef();
            //ͼ����������
            mcRect FrameRect = null;
            geoFrame.CalRect(out FrameRect);

            //���㡢����ͼ��Բ�����ĵ�����
            mcDot CircleDot = new mcDot();
            mcRect CircleRect = null;//���½�ͼ��Բ���������

            mcGeoPolygon queryPolygon = GetRectPolygon(FrameRect);

            queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            mcRecordSet containCircle = null;
            vcls.Select(queryDef, out containCircle);//ͼ���ڵ�Բ
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
                    //ȷ�����½�ͼ��Բ�����ĵ�����
                    CircleDot.x = (CircleRect.xmin + CircleRect.xmax) / 2;
                    CircleDot.y = (CircleRect.ymin + CircleRect.ymax) / 2;
                    dots.Add(CircleDot);
                }
                containCircle.MoveNext();
            }
            return dots;
        }

        //��ȡ�������ڱ�עԲ�ļ���
        public static void GetAnnoCircleGeoList(string frame_layername, out List<IGeometry> Anno_CircleGeoList, out List<IGeomInfo> Anno_CircleGeoInfoList, AxWorkSpace.AxMxWorkSpace WorkSpace)
        {
            List<IGeometry> _Anno_CircleGeoList = new List<IGeometry>();
            List<IGeomInfo> _Anno_CircleGeoInfoList = new List<IGeomInfo>();
            //  1���ҵ�����ͼ��
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

            // 2���ҵ�ͼ��Բ
            IXGroupLayer m_Layer = null;
            m_Layer = WorkSpace.MapCollection.get_Item(0).get_Layer2("T01_0060.WT") as IXGroupLayer;
            IVectorCls vcls = m_Layer.get_Layer(2).XClass as IVectorCls;
            mcQueryDef queryDef = new mcQueryDef();
            //ͼ����������
            mcRect FrameRect = null;
            geoFrame.CalRect(out FrameRect);

            //���㡢����ͼ��Բ�����ĵ�����
            mcDot CircleDot = new mcDot();

            mcGeoPolygon queryPolygon = GetRectPolygon(FrameRect);

            queryDef.set_Spatial(queryPolygon, meSpaQueryMode.meModeContain);
            mcRecordSet containCircle = null;
            vcls.Select(queryDef, out containCircle);//ͼ���ڵ�Բ
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

        //��ȡ��עԲ�ڵ�ע�Ǽ���
        public static List<mcRecordSet> GetCircleAnnosNew(List<IGeometry> geoCircelList, List<IGeomInfo> geoCircelInfoList, mcRecordSet AnnoRecordSet)
        {
            List<mcRecordSet> AnnoInCircles = new List<mcRecordSet>();
            for (int i = 0; i < geoCircelList.Count; i++)
            {
                //��ȡ������עԲ�ڵ�ע��
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


        //��������֮��ľ���
        public static double CalDistanceOfCircle(mcDot dot1, mcDot dot2)
        {
            double dis = 0;
            dis = Math.Sqrt(Math.Pow(dot2.y - dot1.y, 2) + Math.Pow(dot2.x - dot1.x, 2));
            return dis;
        }



        //��ȡ��ע������
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
                string pattern = @"\d+��";
                if (Regex.IsMatch(legendAnoo_TextAnno.Text, pattern))
                {
                    Attr.Add(legendAnoo_TextAnno.Text);

                }
                recordset.MoveNext();
            }
            return Attr;

        }


        //��ȡ��Ӧͼ��
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


        //�жϱ�ע�ֵ����ǰ�����ǰͼ����ע
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
                string value = annoAttr[i].Split('��')[1];
                //�ҵ���Ӧ��ϵ
                string Anno_Mapping = "select "+ accessField +" from ��עӳ���";
                OleDbDataReader mapping_Adr = null;
                mapping_Adr = AccessUtils.GetDataReader(Anno_Mapping, MapConn);
                bool isContain = false;
                while (mapping_Adr.Read())
                {

                    List<string> mapDic = new List<string>(mapping_Adr[accessField].ToString().Split('��'));
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
