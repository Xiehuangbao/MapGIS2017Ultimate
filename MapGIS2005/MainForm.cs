using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using DevComponents.AdvTree;
using AxMapXView;
using System.Data.OleDb;
using mapXBase;
using System.Threading;
using System.Text.RegularExpressions;

namespace MapGIS2005
{
    public partial class MainForm : DevComponents.DotNetBar.Office2007Form
    {
        //记录原始数据以及新数据的路径
        string sourcePath = "";
        string newPath = "";
        string historyPath = "";
        string strDBCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";

        /// <summary>
        /// MPJ_Path 是sourcePath加上选择的矿区以后的路径
        /// Access_Path 指定具体矿区后  该矿区的Access数据库路径
        /// Access_Path_New指定具体矿区后 矿区列表中新的矿区路径中的access路径
        /// </summary>
        string MPJ_Path = "";
        string Access_Path = "";
        string Access_Path_New = "";

        /// <summary>
        /// 在窗体加载时绑定相关Mapgis控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.axMapXView1.WorkSpace = this.axMxWorkSpace1.ToInterface;
            this.axMxEditControl1.WorkSpace = this.axMxWorkSpace1.ToInterface;
            this.axMxEditControl1.EditView = this.axMapXView1.ToInterface;
            this.axMxEditControl1.AddGroupTool("编辑扩展", "EditExtension.EditExtension.1");
            this.axMxEditControl1.AddGroupTool("通用扩展", "MainEdit.EditGroup.1");
            this.axMxEditControl1.AddGroupTool("窗口操作", "GisWndTool.WndTools.1");
            this.bar2.Enabled = false;
            this.bar3.Enabled = false;
            this.superTabControl1.SelectedTab = superTabItem1;

        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="ipos"></param>
        private delegate void SetPos(int ipos);

        public MainForm()
        {
            InitializeComponent();

        }

        private void SetTextMessage(int ipos)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos });
            }
            else
            {
                this.toolStripStatusLabel1.Text = ipos.ToString() + "/100";
                this.ProgressBar.Value = Convert.ToInt32(ipos);
            }
            if (ipos == 100)
            {
                this.toolStripStatusLabel1.Text = "加载完成";
            }
        }
        /// <summary>
        /// 加载MPJ文件到列表中
        /// 获取MPJ文件
        /// get_MPJ_File(MPJ_TheFolder);
        /// //获取Access文件
        /// get_Access_File(Access_TheFolder);
        /// //递归获取附件文件
        /// get_Accessory_File(new DirectoryInfo(MPJ_Path), null);
        /// </summary>
        private void LoadMPJFileToListBar()
        {
            try
            {
                MPJ_Path = sourcePath + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString();
                Access_Path = sourcePath + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString() + "\\ACCESS数据库";
                Access_Path_New = this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[2].Value.ToString() + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString() + "\\ACCESS数据库";
                DirectoryInfo MPJ_TheFolder = new DirectoryInfo(MPJ_Path);
                DirectoryInfo Access_TheFolder = new DirectoryInfo(Access_Path);
                get_MPJ_File(MPJ_TheFolder);
                get_Access_File(Access_TheFolder);
                advTreeAccessory.Nodes.Clear();
                get_Accessory_File(new DirectoryInfo(MPJ_Path), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请检查路径设置！");
                return;
            }
        }

        /// <summary>
        ///  获取某个路径下的MPJ文件
        /// </summary>
        /// <param name="dirInfo"></param>
        private void get_MPJ_File(DirectoryInfo dirInfo)
        {
            advTreeMPJ.Nodes.Clear();
            int count = 0;
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Extension.Equals(".MPJ"))
                {
                    count++;//用于显示进度条数据
                    Node pNode = new Node();
                    pNode.Text = file.Name;
                    advTreeMPJ.Nodes.Add(pNode);

                    //打开工程文件 加载工程下的图层文件
                    WorkSpace.IMxWorkSpace ws = new WorkSpace.MxWorkSpaceClass();
                    ws.Open(file.DirectoryName + "\\" + file.Name, WorkSpace.EnumOpenMode.OpenReadOnly);
                    XMap map = ws.GetMapByName(file.Name.Split('.')[0]);
                    ws.ActiveMap = map;
                    //遍历工程文件下的所有图层文件
                    for (int i = 1; i <= map.LayerCount; i++)
                    {
                        IXMapLayer mapLayer = map.get_Layer(i);
                        Node childNode = new Node();
                        childNode.Text = mapLayer.LayerName;
                        pNode.Nodes.Add(childNode);
                    }
                    ws.Close(WorkSpace.EnumCloseMode.NoDlgDiscard);
                    SetTextMessage(count * 100 / dirInfo.GetFiles().Length);
                }
            }
        }

        /// <summary>
        /// 获取某个路径下的access数据表文件
        /// </summary>
        private void get_Access_File(DirectoryInfo dirInfo)
        {
            string[] TableName = { "JGAB301_核查矿区", "JGAB302_原上表矿区", "JGAB303_勘查工作区", "JGAB304_采矿权", "JGAB305_探矿权",
                "JGAB306_矿体", "JGAB307_采空区", "JGAB308_核查块段", "JGAB309_核查块段储量", "JGAB310_原块段", "JGAB311_原块段储量",
                "JGAB312_块段对照表", "JGAB313_资料目录", "JGAB314_附件目录", "JGAB315_专题图件", "JGAB316_专题图件图层",
                "JGAB317_煤质特征", "JGAB318_储量利用", "JGAB319_大块段对照表", "JGAB320_合并原块段", "JGAB321_采矿权三率" };
            advTreeTableList.Nodes.Clear();
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Extension.Equals(".mdb"))
                {
                    Node pNode = new Node();
                    pNode.Text = file.Name;
                    pNode.ImageIndex = 0;
                    advTreeTableList.Nodes.Add(pNode);

                    for (int i = 0; i < TableName.Length; i++)
                    {
                        Node childNode = new Node();
                        childNode.Text = TableName[i];
                        childNode.ImageIndex = 1;
                        pNode.Nodes.Add(childNode);
                    }
                }
            }
        }

        /// <summary>
        /// 获取某个路径下的附件
        /// </summary>
        private void get_Accessory_File(FileSystemInfo dirInfo, Node parent)
        {
            DirectoryInfo dir = dirInfo as DirectoryInfo;
            //不是目录 
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                {
                    if (!file.Extension.Equals(".MPJ") && !file.Extension.Equals(".mdb")
                    && !file.Extension.Equals(".WT") && !file.Extension.Equals(".WL") && !file.Extension.Equals(".WP"))
                    {
                        Node pNode = new Node();
                        pNode.Text = file.Name;
                        parent.Nodes.Add(pNode);
                    }

                }
                //对于子目录，进行递归调用 
                else
                {
                    DirectoryInfo dir_root = files[i] as DirectoryInfo;
                    FileSystemInfo[] files_root = dir_root.GetFileSystemInfos();
                    if (files_root.Length != 0 && !dir_root.Name.Equals("ACCESS数据库") && !dir_root.Name.Equals("地理地质图层类") && !dir_root.Name.Equals("资源储量图层类")
                    && !dir_root.Name.Equals("核查矿区套合图层类") && !dir_root.Name.Equals("探采工程图层类") && !dir_root.Name.Equals("图件整饰图层类"))
                    {
                        Node root = new Node();
                        root.Text = files[i].Name;
                        advTreeAccessory.Nodes.Add(root);
                        get_Accessory_File(files[i], root);
                    }

                }

            }
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
                MessageBox.Show("数据库被占用！");
                return null;
            }
        }
        /// <summary>
        /// 设置工具条的可见性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            this.bar2.Enabled = false;
            this.bar3.Enabled = false;
            if (superTabControl1.SelectedTab == superTabItem2) this.bar2.Enabled = true;
            if (superTabControl1.SelectedTab == superTabItem1) this.bar3.Enabled = true;
        }
        /// <summary>
        /// 对地图的相关放大缩小操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoonIn_Click(object sender, EventArgs e)
        {
            this.axMxEditControl1.StartTool("zoomin", 0, 0);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            this.axMxEditControl1.StartTool("zoomout", 0, 0);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            this.axMxEditControl1.StartTool("restore", 0, 0);
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            this.axMxEditControl1.StartTool("movewindow", 0, 0);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.axMxEditControl1.StartTool("refresh", 0, 0);
        }

        /// <summary>
        /// 在视图中打开MPJ文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenVIew_Click(object sender, EventArgs e)
        {
            try
            {
                superTabControl1.SelectedTab = superTabItem2;
                if (advTreeMPJ.SelectedNode == null || advTreeMPJ.SelectedNode.Text.Equals("")) return;
                string mpj_Path = MPJ_Path + "\\" + advTreeMPJ.SelectedNode.Text;
                this.axMxWorkSpace1.Open(mpj_Path, WorkSpace.EnumOpenMode.OpenNormal);
                this.axMapXView1.Restore();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //button列表 记录需要重绘的button
        List<Button> lb = new List<Button>();

        //设置原始数据与新数据路径
        private void btnPath_Click(object sender, EventArgs e)
        {
            PathSet ps = new PathSet();
            if (ps.ShowDialog() == DialogResult.OK)
            {
                this.sourcePath = ps.sourcePath;
                this.newPath = ps.newPath;
                this.historyPath = ps.historyPath;

                //加载矿区列表
                DirectoryInfo TheFolder = new DirectoryInfo(sourcePath);
                DirectoryInfo[] nextFolder = TheFolder.GetDirectories();
                dataGridViewKQList.RowCount = nextFolder.Length;
                for (int i = 0; i < nextFolder.Length; i++)
                {
                    dataGridViewKQList.Rows[i].Cells[0].Value = nextFolder[i].Name;
                    dataGridViewKQList.Rows[i].Cells[1].Value = sourcePath;
                    dataGridViewKQList.Rows[i].Cells[2].Value = newPath;
                    dataGridViewKQList.Rows[i].Cells[3].Value = nextFolder[i].CreationTime.ToString();
                    Button btnSelectPath = new Button();
                    btnSelectPath.Text = "...";
                    btnSelectPath.Width = 30;
                    btnSelectPath.Location = new System.Drawing.Point(this.dataGridViewKQList.GetCellDisplayRectangle(2, i, true).Right - btnSelectPath.Width,
                                                                       this.dataGridViewKQList.GetCellDisplayRectangle(2, i, true).Y);
                    lb.Add(btnSelectPath);
                    btnSelectPath.Click += new EventHandler(btnSelectPath_Click);
                    this.dataGridViewKQList.Controls.Add(btnSelectPath);
                }
                superTabControl1.SelectedTab = superTabItem1;
            }
        }
        /// <summary>
        /// 矿区列表中的路径选择，通过该按钮设置新的数据路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowDialog();

            //x含义 单击button后 计算当前选择的是哪一行  用button的y值除以单元格的高度 计算
            Button b = (Button)sender;
            int x = b.Location.Y / this.dataGridViewKQList.GetCellDisplayRectangle(2, 0, true).Height;
            this.dataGridViewKQList.Rows[x - 1].Cells[2].Value = folderDlg.SelectedPath;
        }

        //重绘button的位置
        private void dataGridViewKQList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            for (int i = 0; i < lb.Count; i++)
            {
                lb[i].Location = new System.Drawing.Point(this.dataGridViewKQList.GetCellDisplayRectangle(2, i, true).Right - lb[i].Width,
                    this.dataGridViewKQList.GetCellDisplayRectangle(2, i, true).Y);
            }

        }

        // 获取鼠标点击的位置
        //用以记录右键点击时的坐标  打开矿区列表时使用
        int xt;
        int yt;
        private void dataGridViewKQList_MouseDown(object sender, MouseEventArgs e)
        {
            xt = e.X;
            yt = e.Y;
        }

        //弹出右键菜单 打开矿区里的工程文件
        private void dataGridViewKQList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.dataGridViewKQList.SelectedCells[0].ColumnIndex == 0)
            {
                cmsOpenKQ.Show(this.dataGridViewKQList, xt, yt);

            }
        }
        //启动新线程 加载MPJ文件到列表中
        private void LoadMPJFileToList_Click(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(LoadMPJFileToListBar));
            fThread.Start();
        }

        /// <summary>
        /// 更新矿区工程文件  以整个矿区文件夹更新的方式  更新整个矿区中的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiUpdateKQ_Click(object sender, EventArgs e)
        {
            try
            {
                //先将原始数据备份一下
                CopyFolder(this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString(),
                    this.historyPath + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString());
                //先删除文件夹下的所有文件，包括文件夹然后再新建一个和以前名字一样的文件夹
                Directory.Delete(this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString(), true);
                Directory.CreateDirectory(this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString());
                //更新原始数据
                CopyFolder(this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[2].Value.ToString(),
                    this.dataGridViewKQList.Rows[this.dataGridViewKQList.SelectedCells[0].RowIndex].Cells[1].Value.ToString() + "\\" + this.dataGridViewKQList.SelectedCells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("请确认新数据路径正确！");
                return;
            }
            MessageBox.Show("更新完成！");
        }

        //拷贝文件夹
        private bool CopyFolder(string sPath, string dPath)
        {
            try
            {
                // 创建目的文件夹
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }
                // 拷贝文件
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    //如果文件存在，则删除原始文件
                    if (File.Exists(dPath + "\\" + file.Name))
                    {
                        File.Delete(dPath + "\\" + file.Name);
                    }
                    else
                    {
                        file.CopyTo(dPath + "\\" + file.Name, true);
                    }

                }
                // 循环子文件夹
                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    CopyFolder(subDir.FullName, dPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("拷贝中出现问题！");
                return false;
            }
            return true;
        }


        //备份文件
        private void BackupFile(string FileName, string sPath, string dPath)
        {
            try
            {
                // 拷贝文件
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    //如果文件匹配  就复制文件
                    if (file.Name.Equals(FileName))
                    {
                        file.CopyTo(dPath + "\\" + file.Name, true);
                        return;
                    }
                }
                // 循环子文件夹
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    BackupFile(FileName, subDir.FullName, dPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份" + FileName + "文件出现问题！");
            }
        }


        //更新文件
        private void UpdateFile(string FileName, string sPath, string dPath)
        {
            try
            {
                // 拷贝文件
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    //如果文件匹配  就复制文件
                    if (file.Name.Equals(FileName))
                    {
                        file.CopyTo(dPath + "\\" + file.Name, true);
                        return;
                    }
                }
                // 循环子文件夹
                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    UpdateFile(FileName, subDir.FullName, dPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新" + FileName + "文件出现问题！");
            }
        }

        //删除文件
        private void DeleteFile(string FileName, string sPath)
        {
            try
            {
                // 拷贝文件
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    //如果文件匹配  就删除文件
                    if (file.Name.Equals(FileName))
                    {
                        File.Delete(sPath + "\\" + file.Name);
                        return;
                    }
                }
                // 循环子文件夹
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    DeleteFile(FileName, sPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除" + FileName + "文件出现问题！");
            }
        }

        //替换MPJ文件
        public string NewMPJFilePath = "";
        public string SaveMPJFilePath = "";
        public string NewMPJFile = "";
        public bool SwichBtnVale = false;
        //更新工程文件以及该工程文件的图层文件
        private void tsmiMPJFileUpdate_Click(object sender, EventArgs e)
        {
            UpdateMPJFile umpjdlg = new UpdateMPJFile(this);
            if (umpjdlg.ShowDialog() == DialogResult.OK)
            {
                //如果需要备份 先备份MPJ文件
                if (SwichBtnVale)
                {
                    //备份文件
                    BackupFile(this.advTreeMPJ.SelectedNode.Text, MPJ_Path, SaveMPJFilePath);
                    foreach (Node n in this.advTreeMPJ.SelectedNode.Nodes)
                    {
                        BackupFile(n.Text, MPJ_Path, SaveMPJFilePath);
                    }
                    //先删除MPJ文件以及该工程文件下的图层文件
                    DeleteFile(this.advTreeMPJ.SelectedNode.Text, MPJ_Path);
                    foreach (Node n in this.advTreeMPJ.SelectedNode.Nodes)
                    {
                        DeleteFile(n.Text, MPJ_Path);
                    }

                    //更新MPJ文件
                    UpdateFile(NewMPJFile, NewMPJFilePath.Replace("\\" + NewMPJFile, ""), MPJ_Path);
                    //打开工程文件 更新工程下的图层文件
                    WorkSpace.IMxWorkSpace ws = new WorkSpace.MxWorkSpaceClass();
                    ws.Open(NewMPJFilePath, WorkSpace.EnumOpenMode.OpenReadOnly);
                    XMap map = ws.GetMapByName(NewMPJFile.Replace(".MPJ", ""));
                    ws.ActiveMap = map;

                    for (int i = 1; i <= map.LayerCount; i++)
                    {
                        IXMapLayer mapLayer = map.get_Layer(i);
                        UpdateFile(mapLayer.LayerName, NewMPJFilePath.Replace("\\" + NewMPJFile, ""), MPJ_Path);
                    }
                    ws.Close(WorkSpace.EnumCloseMode.NoDlgDiscard);
                }
                else//不备份直接更新
                {
                    //先删除MPJ文件以及该工程文件下的图层文件
                    DeleteFile(this.advTreeMPJ.SelectedNode.Text, MPJ_Path);
                    foreach (Node n in this.advTreeMPJ.SelectedNode.Nodes)
                    {
                        DeleteFile(n.Text, MPJ_Path);
                    }

                    //更新MPJ文件
                    UpdateFile(NewMPJFile, NewMPJFilePath.Replace("\\" + NewMPJFile, ""), MPJ_Path);
                    //打开工程文件 更新工程下的图层文件
                    WorkSpace.IMxWorkSpace ws = new WorkSpace.MxWorkSpaceClass();
                    ws.Open(NewMPJFilePath, WorkSpace.EnumOpenMode.OpenReadOnly);
                    XMap map = ws.GetMapByName(NewMPJFile.Replace(".MPJ", ""));
                    ws.ActiveMap = map;

                    for (int i = 1; i <= map.LayerCount; i++)
                    {
                        IXMapLayer mapLayer = map.get_Layer(i);
                        UpdateFile(mapLayer.LayerName, NewMPJFilePath.Replace("\\" + NewMPJFile, ""), MPJ_Path);
                    }
                    ws.Close(WorkSpace.EnumCloseMode.NoDlgDiscard);
                }
            }


        }

        private void tsmiUpdateLayer_Click(object sender, EventArgs e)
        {

        }


        private void advTreeMPJ_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickPoint = new Point(e.X, e.Y);
                Node CurrentNode = advTreeMPJ.GetNodeAt(clickPoint);
                if (CurrentNode != null && CurrentNode.Parent != null)
                {
                    tsmiOpenVIew.Enabled = false;
                    tsmiMPJFileUpdate.Enabled = false;
                    tsmiLocalUpdate.Enabled = false;
                    tsmiUpdateLayer.Enabled = true;
                    CurrentNode.ContextMenu = cmsOpenMPJ;
                }
                else if (CurrentNode != null && CurrentNode.Parent == null)
                {
                    tsmiOpenVIew.Enabled = true;
                    tsmiMPJFileUpdate.Enabled = true;
                    tsmiLocalUpdate.Enabled = true;
                    tsmiUpdateLayer.Enabled = false;

                    CurrentNode.ContextMenu = cmsOpenMPJ;
                    //advTreeMPJ.SelectedNode = CurrentNode;//选中这个节点 
                }
            }
        }

        private void tsmiOpenAccessory_Click(object sender, EventArgs e)
        {
            string filePath = MPJ_Path + "\\" + advTreeAccessory.SelectedNode.FullPath.Replace(';', '\\');
            System.Diagnostics.Process.Start(filePath);
        }

        private void advTreeAccessory_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickPoint = new Point(e.X, e.Y);
                Node CurrentNode = advTreeAccessory.GetNodeAt(clickPoint);
                if (CurrentNode != null && CurrentNode.Parent != null)
                {
                    CurrentNode.ContextMenu = cmsOpenAccesory;
                }
            }
        }

        //打开access数据表
        private void tsmiOpenTable_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem3;

            if (advTreeTableList.SelectedNode == null || advTreeTableList.SelectedNode.Text.Equals("") || advTreeTableList.SelectedNode.Text.Equals(advTreeTableList.Nodes[0].Text)) return;
            string access_Path = Access_Path + "\\" + advTreeTableList.Nodes[0].Text;
            OleDbConnection connNew = new OleDbConnection(strDBCon + access_Path);
            connNew.Open();
            string SQL = "select * from " + advTreeTableList.SelectedNode.Text;
            DataTable dtNew = GetDataSet(connNew, SQL);
            this.dataGridViewXTable.DataSource = dtNew;
            //静止列排序功能
            for (int i = 0; i < this.dataGridViewXTable.Columns.Count; i++)
            {
                this.dataGridViewXTable.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            connNew.Close();
        }

        //在表中添加数据
        private void btnAddData_Click(object sender, EventArgs e)
        {
            ((DataTable)this.dataGridViewXTable.DataSource).Rows.Add();

        }
        //在表中删除数据
        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridViewXTable.SelectedRows)
            {
                if (dr.IsNewRow == false)//如果不是已提交的行，默认情况下在添加一行数据成功后，DataGridView为新建一行作为新数据的插入位置
                    dataGridViewXTable.Rows.Remove(dr);
            }

        }
        //在表中更新数据
        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridViewXTable.DataSource as DataTable;
            if (dt != null)
            {
                try
                {
                    using (OleDbConnection connNew = new OleDbConnection(strDBCon + Access_Path + "\\" + advTreeTableList.Nodes[0].Text))
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter("select * from " + advTreeTableList.SelectedNode.Text, connNew);
                        OleDbCommandBuilder ocb = new OleDbCommandBuilder(da);
                        DataSet ds = new DataSet();
                        da.Fill(ds, advTreeTableList.SelectedNode.Text);
                        da.Update(dt);
                    }
                }
                catch (Exception de)
                {
                    MessageBox.Show(de.Message);
                }
            }

        }
        //在表中查询数据
        private void btnSerachData_Click(object sender, EventArgs e)
        {
            int row = dataGridViewXTable.Rows.Count;//得到总行数
            int cell = dataGridViewXTable.Rows[1].Cells.Count;//得到总列数
            Regex r = new Regex(this.txtSearchData.Text); // 定义一个Regex对象实例
            for (int i = 0; i < row; i++)//得到总行数并在之内循环
            {
                for (int j = 0; j < cell; j++)//得到总列数并在之内循环
                {
                    Match m = r.Match(dataGridViewXTable.Rows[i].Cells[j].Value.ToString());
                    if (m.Success)
                    {   //对比TexBox中的值是否与dataGridView中的值相同（上面这句）  
                        dataGridViewXTable.CurrentCell = dataGridViewXTable[j, i];//定位到相同的单元格  
                        return;//返回  
                    }

                }
            }
        }

        private void advTreeTableList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point clickPoint = new Point(e.X, e.Y);
                Node CurrentNode = advTreeTableList.GetNodeAt(clickPoint);
                if (CurrentNode.Parent != null && CurrentNode != null)
                {
                    tsmiOpenTable.Enabled = true;
                    tsmiUpdateTable.Enabled = false;
                    CurrentNode.ContextMenu = cmsOpenDataTable;
                    this.advTreeTableList.Refresh();
                }
                else if (CurrentNode.Parent == null && CurrentNode != null)
                {
                    tsmiOpenTable.Enabled = false;
                    tsmiUpdateTable.Enabled = true;
                    CurrentNode.ContextMenu = cmsOpenDataTable;
                    this.advTreeTableList.Refresh();
                }
            }
        }

        private void tsmiUpdateTable_Click(object sender, EventArgs e)
        {
            UpdateTableForm utf = new UpdateTableForm(Access_Path + "\\" + advTreeTableList.Nodes[0].Text, Access_Path_New + "\\"
                + advTreeTableList.Nodes[0].Text, historyPath);
            utf.ShowDialog();
        }

        //备份附件文件
        public string NewAccessoryFile = "";
        public string NewAccessoryFilePath = "";
        public string SaveAccessoryFilePath = "";
        public bool SwichBtnValeAcc = false;

        private void tsmiAccessoryUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccessoryFile uafdlg = new UpdateAccessoryFile(this);
            if (uafdlg.ShowDialog() == DialogResult.OK)
            {
                //如果需要备份 先备份MPJ文件
                if (SwichBtnValeAcc)
                {
                    //备份文件
                    BackupFile(this.advTreeAccessory.SelectedNode.Text, MPJ_Path + "\\" + this.advTreeAccessory.SelectedNode.Parent.Text, SaveAccessoryFilePath);
                    //先删除附件文件以及该工程文件下的图层文件
                    DeleteFile(this.advTreeAccessory.SelectedNode.Text, MPJ_Path + "\\" + this.advTreeAccessory.SelectedNode.Parent.Text);
                    //更新MPJ文件
                    UpdateFile(NewAccessoryFile, NewAccessoryFilePath.Replace("\\" + NewAccessoryFile, ""), MPJ_Path + "\\" + this.advTreeAccessory.SelectedNode.Parent.Text);
                }
                else//不备份直接更新
                {
                    //先删除MPJ文件以及该工程文件下的图层文件
                    DeleteFile(this.advTreeAccessory.SelectedNode.Text, MPJ_Path + "\\" + this.advTreeAccessory.SelectedNode.Parent.Text);
                    //更新MPJ文件
                    UpdateFile(NewAccessoryFile, NewAccessoryFilePath.Replace("\\" + NewAccessoryFile, ""), MPJ_Path + "\\" + this.advTreeAccessory.SelectedNode.Parent.Text);
                }
                MessageBox.Show(this.advTreeAccessory.SelectedNode.Text + "附件备份完成");
            }

        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            string access_Path = Access_Path + "\\" + advTreeTableList.Nodes[0].Text;
            OleDbConnection connNew = new OleDbConnection(strDBCon + access_Path);

            StatisticTable stdlg = new StatisticTable(connNew, advTreeTableList.SelectedNode.Text);
            stdlg.ShowDialog();
        }

        private void btnGeoUpdate_Click(object sender, EventArgs e)
        {
            GeoUpdateDlg gudlg = new GeoUpdateDlg();
            gudlg.ShowDialog();
        }

        private void updateAnnotation_Click(object sender, EventArgs e)
        {
            //try
            //{
                UpdateAnnotationDlg uadlg = new UpdateAnnotationDlg();
                uadlg.Show();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }


    }
}