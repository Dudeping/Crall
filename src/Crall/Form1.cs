#region 引用
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
#endregion

namespace 杜德平的数据库编程
{
    public partial class Form1 : Form
    {
        #region 定义一些共有字段，方便数据传输和共用
        //学号
        private string stuId = "";
        //姓名
        private string stuName = "";
        //系别
        private string stuDepart = "";
        //总人数
        private int totalNum = 0;
        //标记是否是第一次点击
        private bool isFirst = true;
        //记录中的全部学生
        private List<StudentLog> listLog = new List<StudentLog>();
        //用于本环节点名且没被点的全部学生
        private List<StudentClick> listClick = new List<StudentClick>();
        //本次已点学生
        private List<ArealdyStudent> stuArealdy = new List<ArealdyStudent>();
        //文本文件保存路径
        private string tpath = string.Format("{0}\\log.txt", Environment.CurrentDirectory);
        //XML文件保存路径
        private string xpath = string.Format("{0}\\log.xml", Environment.CurrentDirectory);
        //链接字符串
        private string constr = "Data Source=10.2.130.244; Initial Catalog=xjgl; Persist Security Info=True; User ID=xuesheng;Password=123";
        #endregion
        private static Form1 _form1;
        /// <summary>
        /// 初始化窗口
        /// 并初始化一个基本数据
        /// </summary>
        private Form1()
        {
            InitializeComponent();

            #region 检查计算机是否联网
            try
            {
                using (WebRequest.Create("https://www.baidu.com").GetResponse()) { }
            }
            catch (Exception)
            {
                MessageBox.Show("计算机必须联网才能进行点名操作", "Error");
                return;
            }
            #endregion

            #region 初始化按钮-->将一些按钮禁用以符合处理逻辑
            labdeal.Hide();
            butTo.Enabled = false;
            txtLog.Enabled = false;
            butNoTo.Enabled = false;
            butView.Enabled = false;
            comboBoxType.Enabled = false;
            txtStatistic.Enabled = false;
            labFoot.Text = "Copyright (C) 2016-" + DateTime.Now.Year.ToString() + labFoot.Text;
            pic.Image = Image.FromStream(System.Net.WebRequest.Create("https://pic1.zhimg.com/bd406f6a8a37d6af642d9b0ebaccbc5c_xl.jpg").GetResponse().GetResponseStream());
            defultDepart();//初始化数据-->从数据库读取数据来初始化系别下拉框
            #endregion

            //初始化一份日志文件，如果有正式部署就直接维护一个日志数据表，毕竟文件共享性不高
            createTxtFile();

            //初始化一份已点名的日志文件
            createXmlFile();
        }

        /// <summary>
        /// 本来是想引入单例，但是失败了
        /// 时间紧迫，还有很多作业
        /// 还是留到以后碰到再解决
        /// </summary>
        /// <returns></returns>
        //TODO: 单例
        public static Form1 createForm1()
        {
            if (_form1 == null)
                _form1 = new Form1();
            return _form1;
        }

        /// <summary>
        /// 初始化类别
        /// </summary>
        private void defultDepart()
        {
            #region 初始化数据-->从数据库读取数据
            string conStr = constr;
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct 系别 from students;";
            SqlDataAdapter adpter_depart = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpter_depart.Fill(dt);
            con.Close();
            #endregion

            //初始化系别下拉框
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comBoxSelectDepart.Items.Add(dt.Rows[i]["系别"].ToString().Trim() == "" ? "NULL" : dt.Rows[i]["系别"].ToString().Trim());
            }
        }

        /// <summary>
        /// 维护一份本次系统已点名单
        /// </summary>
        private void createXmlFile()
        {
            XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Log"));
            xdoc.Save(xpath);
        }

        /// <summary>
        /// 维护一份点名记录
        /// </summary>
        private void createTxtFile()
        {
            using (FileStream fs = new FileStream(tpath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length >= 10)
                {
                    //将查询记录设置为可用
                    butView.Enabled = true;
                    txtLog.Enabled = true;
                    comboBoxType.Enabled = true;
                    txtStatistic.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 改变图片
        /// </summary>
        /// <param name="name">学生姓名</param>
        private void changePic(string name)
        {
            //先尝试根据姓名从百度下载图片，失败了再从教务网随机搞一张2003级的照片
            try
            {
                #region 尝试从百度下载图片
                HttpWebRequest requestHtml = (HttpWebRequest)WebRequest.Create("http://image.baidu.com/search/avatarjson?tn=resultjsonavatarnew&ie=utf-8&word=" + Uri.EscapeDataString(name) + "&cg=girl&pn=30&rn=30&itg=0&z=0&fr=&lm=-1&ic=0&s=0&st=-1&gsm=f000000000f0");
                using (HttpWebResponse responseHtml = (HttpWebResponse)requestHtml.GetResponse())
                {
                    if (responseHtml.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = responseHtml.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string json = reader.ReadToEnd();
                                JObject objRoot = (JObject)JsonConvert.DeserializeObject(json);
                                JArray imgs = (JArray)objRoot["imgs"];
                                JObject img = (JObject)imgs[0];
                                string thumbURL = (string)img["middleURL"];

                                HttpWebRequest requestPic = (HttpWebRequest)WebRequest.Create(thumbURL);
                                requestPic.Referer = "http://image.baidu.com";
                                using (HttpWebResponse responsePic = (HttpWebResponse)requestPic.GetResponse())
                                {
                                    if (responsePic.StatusCode == HttpStatusCode.OK)
                                        pic.Image = Image.FromStream(responsePic.GetResponseStream());
                                    else throw new Exception();
                                }
                            }
                        }
                    }
                    else throw new Exception();
                }
                #endregion
            }
            catch (Exception)
            {
                #region 从教务网2003级中随机下载一张图片
                while (true)
                {
                    try
                    {
                        var en = Enumerable.Range(20030001, 7069);
                        int picIndex = en.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
                        var img = Image.FromStream(WebRequest.Create("http://jiaowu.sicau.edu.cn/photo/" + picIndex.ToString() + ".jpg").GetResponse().GetResponseStream());
                        pic.Image = img;
                        return;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 换下一位同学的处理逻辑
        /// </summary>
        /// <param name="isTo">是否到勤</param>
        private void nextTo(bool isTo)
        {
            labdeal.Show();
            //这里绝大多数情况下是会成功的，所以就不做过多的处理
            //记得加上院系
            using (StreamWriter sw = new StreamWriter(tpath, true))
            {
                sw.WriteLine(labStuId.Text + " " + labName.Text + " " + comBoxSelectDepart.SelectedItem.ToString() + (isTo == true ? " 到勤" : " 缺席"));

                #region 维护本次已点同学记录
                //Log
                //系别
                //--系别名称
                //--已点 
                //--总人数 
                //--student 
                //----学号 
                //----姓名
                XDocument xd = XDocument.Load(xpath);
                XElement log = xd.Element("Log");
                var depart = log.Elements("Depart");
                var departItem = from x in depart
                                 where x.Element("Name").Value == comBoxSelectDepart.SelectedItem.ToString()
                                 select x;
                if (departItem.Count() != 0)
                {
                    XElement xNum = departItem.Elements("Num").FirstOrDefault();
                    int xnum;
                    bool b = Int32.TryParse(xNum.Value, out xnum);
                    if (b)
                    {
                        xNum.ReplaceWith(new XElement("Num", xnum + 1));
                    }
                    //将此学生添加在本次已点名的记录中
                    departItem.FirstOrDefault().Add(new XElement("Student", new XElement("StuId", stuId), new XElement("StuName", stuName)));
                    xd.Save(xpath);
                }
                else
                {
                    XElement xe = new XElement("Depart",
                    new XElement("Name", comBoxSelectDepart.SelectedItem.ToString()),
                    new XElement("Num", 1),
                    new XElement("Total", totalNum),
                    new XElement("Student",
                        new XElement("StuId", stuId),
                        new XElement("StuName", stuName)
                        ));
                    log.Add(xe);
                    xd.Save(xpath);
                }
                #endregion

                butView.Enabled = true;
                txtLog.Enabled = true;
                comboBoxType.Enabled = true;
                txtStatistic.Enabled = true;
            }
            //移除该成员
            listClick.RemoveAll((StudentClick studentClick) => { return studentClick.stuId == stuId && studentClick.stuName == stuName && studentClick.stuDepart.Equals(stuDepart); });

            if (listClick.Count == 0)
            {
                //禁用一切点名按钮
                butTo.Enabled = false;
                butNoTo.Enabled = false;
                labNum.Text = totalNum.ToString() + " / " + totalNum.ToString();
                totalNum = 0;
                MessageBox.Show("已点完！", "提示");
            }
            else
            {
                //还原默认设置
                isTo = true;
                //初始化一个学生的信息
                defultOneStu();
            }
            labdeal.Hide();
        }

        /// <summary>
        /// 初始化一个学生
        /// </summary>
        private void defultOneStu()
        {
            if (isFirst)
            {
                //设置标志isFirst
                isFirst = false;

                //设置本次点名的时间
                using (StreamWriter sw = new StreamWriter(tpath, true))
                {
                    sw.WriteLine("* " + DateTime.Now);
                }
            }

            //数据抽取一位同学
            var stu = listClick.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
            //设置页面内容和共有变量
            labStuId.Text = stuId = stu.stuId;
            labName.Text = stuName = stu.stuName;
            stuDepart = stu.stuDepart;
            changePic(stuName);//设置学生照片
            labNum.Text = (totalNum - listClick.Count).ToString() + " / " + totalNum.ToString();

            //标记属性
            butTo.Enabled = true;
            butNoTo.Enabled = true;
        }

        /// <summary>
        /// 查看历史记录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butView_Click(object sender, EventArgs e)
        {

            using (StreamReader sr = new StreamReader(tpath))
            {
                txtLog.Text = "";
                string rel = sr.ReadToEnd();
                string[] splitstr = rel.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in splitstr)
                {
                    string[] itemSplit = item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (itemSplit[0] == "*")
                    {
                        if (txtLog.Text != "")
                            txtLog.AppendText(Environment.NewLine);
                        txtLog.AppendText(itemSplit[1] + " " + itemSplit[2] + Environment.NewLine);
                    }
                    else
                        txtLog.AppendText(itemSplit[0] + "(姓名：" + itemSplit[1] + ", 系别：" + itemSplit[2] + ")" + " ==> " + itemSplit[3] + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// 标记为到了按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butTo_Click(object sender, EventArgs e)
        {
            //禁用按钮
            butTo.Enabled = false;
            butNoTo.Enabled = false;
            //换下一位
            nextTo(true);
        }

        /// <summary>
        /// 标记为缺席按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butNoTo_Click(object sender, EventArgs e)
        {
            //禁用按钮
            butTo.Enabled = false;
            butNoTo.Enabled = false;
            //换下一位
            nextTo(false);
        }

        /// <summary>
        /// 当类型选项变动的时候触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBoxSelectDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            labdeal.Show();
            butNoTo.Enabled = false;
            butTo.Enabled = false;

            //判断该系别此次点名是否已点完并初始化已点的学生列表
            XDocument sd = XDocument.Load(xpath);
            XElement log = sd.Element("Log");
            var depart = log.Elements("Depart");
            var departItem = from x in depart
                             where x.Element("Name").Value == comBoxSelectDepart.SelectedItem.ToString()
                             select x;
            
            //检查到日志文件中存在此系的信息
            if(departItem.Count() == 1)
            {
                //是否点完，点完便提示，没点完就维护记录
                if (departItem.FirstOrDefault().Element("Total").Value == departItem.FirstOrDefault().Element("Num").Value)
                {
                    labNum.Text = departItem.FirstOrDefault().Element("Total").Value + " / " + departItem.FirstOrDefault().Element("Num").Value;
                    labStuId.Text = "";
                    labName.Text = "";
                    pic.Image = Image.FromStream(WebRequest.Create("http://ticket.ydath.cn/Content/pic.jpg").GetResponse().GetResponseStream());
                    MessageBox.Show("本次点名已完成此系的点名！", "Info");
                    labdeal.Hide();
                    return;
                }

                var student = departItem.FirstOrDefault().Elements("Student");
                foreach (var item in student)
                {
                    stuArealdy.Add(new ArealdyStudent() { stuId = item.Element("StuId").Value, stuName = item.Element("StuName").Value, stuDepart = comBoxSelectDepart.SelectedItem.ToString() });
                }
            }

            #region 从数据库下载此系的学生信息并初始化列表
            try
            {
                //测试过程中确实出过错，所以加了一个try-catch
                //猜测可能是因为UDP交互不安全导致
                string conStr = "Data Source=10.2.130.244; Initial Catalog=xjgl; Persist Security Info=True; User ID=xuesheng;Password=123";
                SqlConnection con = new SqlConnection(conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                if (comBoxSelectDepart.SelectedItem.ToString() == "NULL")
                    cmd.CommandText = "select 学号, 姓名 from students where 系别 is NULL;";
                else
                    cmd.CommandText = "select 学号, 姓名 from students where 系别 = '" + comBoxSelectDepart.SelectedItem.ToString() + "';";
                SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpter.Fill(ds);

                //先移除已有的元素
                listClick.RemoveAll((StudentClick s) => { return 1 == 1; });

                //初始化数据-->将数据存入一个列表用于本次点名使用
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        listClick.Add(new StudentClick() { stuId = ds.Tables[i].Rows[j]["学号"].ToString().Trim(), stuName = ds.Tables[i].Rows[j]["姓名"].ToString().Trim(), stuDepart = comBoxSelectDepart.SelectedItem.ToString().Trim() });
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("程序出错", "Error");
                labdeal.Hide();
                return;
            }
            #endregion
            if (listClick.Count < 1)
            {
                MessageBox.Show("数据初始化失败或者数据源没有数据！", "提示");
                labdeal.Hide();
                return;
            }

            //设置总人数
            totalNum = listClick.Count;
            //移除已点的学生
            if(stuArealdy.Count > 0)
            {
                foreach (var item in stuArealdy)
                {
                    listClick.RemoveAll((StudentClick stuClick) => { return stuClick.stuId == item.stuId && stuClick.stuName == item.stuName && stuClick.stuDepart == item.stuDepart; });
                }
                labNum.Text = stuArealdy.Count.ToString() + " / " + totalNum.ToString();
                stuArealdy.RemoveAll((ArealdyStudent srealdyStudent) => { return 1 == 1; });
            }
            else
                labNum.Text = "0 / " + totalNum.ToString();
            //初始化一个学生信息
            defultOneStu();
            //设置按钮属性
            labdeal.Hide();
            butNoTo.Enabled = true;
            butTo.Enabled = true;
        }

        /// <summary>
        /// 统计按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butStatistic_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedItem == null)
            {
                MessageBox.Show("请选择类别", "Warning");
                return;
            }
            
        }

        /// <summary>
        /// 当选择类别发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(tpath))
            {
                //移除全部
                txtStatistic.Text = "";
                txtStatistic.ForeColor = Color.Black;
                if (listLog.Count != 0)
                    listLog.RemoveAll((StudentLog studentLog) => { return 1 == 1; });

                //重新初始化
                string rel = sr.ReadToEnd();
                string[] splitstr = rel.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in splitstr)
                {
                    string[] itemSplit = item.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (itemSplit[0] != "*")
                    {
                        StudentLog stuLog = new StudentLog() { stuId = itemSplit[0], stuName = itemSplit[1], departName = itemSplit[2], toNum = itemSplit[3] == "到勤" ? 1 : 0, totalNum = 1 };
                        var stu = from x in listLog
                                  where x.stuId == stuLog.stuId && x.stuName == stuLog.stuName && x.departName == stuLog.departName
                                  select x;

                        //如果列表中没有该同学，则添加，有，则更新到勤次数和总被点名次数
                        if (stu.Count() == 1)
                        {
                            var stuOld = stu.FirstOrDefault();
                            stuLog.toNum += stuOld.toNum;
                            stuLog.totalNum += stuOld.totalNum;
                            listLog.Remove(stuOld);
                            listLog.Add(stuLog);
                        }
                        else if (stu.Count() == 0)
                        {
                            listLog.Add(stuLog);
                        }
                        else
                        {
                            MessageBox.Show("程序出错", "Error");
                            return;
                        }
                    }
                }

                #region 显示
                var depart = (
                        from x in listLog
                        select x.departName
                        ).Distinct().ToList();//强制执行
                foreach (var item in depart)
                {
                    var stuList = (
                        from x in listLog
                        where (x.departName == item &&
                        ((comboBoxType.SelectedItem == null || comboBoxType.SelectedItem.ToString() == "全部") ? x.totalNum - x.toNum >= 0
                        : comboBoxType.SelectedItem.ToString() == "全勤" ? x.totalNum - x.toNum == 0
                        : comboBoxType.SelectedItem.ToString() == "缺席一次" ? x.totalNum - x.toNum == 1
                        : comboBoxType.SelectedItem.ToString() == "缺席两次" ? x.totalNum - x.toNum == 2
                        : x.totalNum - x.toNum >= 3
                        ))
                        select x
                        ).ToList();

                    if (stuList.Count() != 0)
                    {
                        if (txtStatistic.Text != "")
                            txtStatistic.AppendText(Environment.NewLine);
                        txtStatistic.AppendText(item + Environment.NewLine);

                    }
                    foreach (var stuItem in stuList)
                    {
                        txtStatistic.AppendText(stuItem.stuId + "(姓名：" + stuItem.stuName + ", 记录：" + stuItem.toNum.ToString() + "/" + stuItem.totalNum.ToString() + ")" + Environment.NewLine);
                    }
                }

                if (txtStatistic.Text == "")
                {
                    txtStatistic.Text = "没有数据";
                    txtStatistic.ForeColor = Color.Red;
                }
                #endregion
            }
        }
    }

    #region 用于操作的数据结构
    /// <summary>
    /// 查看学生到勤情况的数据结构
    /// 这里不涉及用户操作，所以没有做数据约束
    /// </summary>
    public class StudentLog
    {
        //学号
        public string stuId { get; set; }

        //姓名
        public string stuName { get; set; }

        //系别
        public string departName { get; set; }

        //被点且到勤的次数
        public int toNum { get; set; }

        //被点总次数
        public int totalNum { get; set; }
    }

    /// <summary>
    /// 用于点名的学生列表的数据结构
    /// 这里不涉及用户操作，所以没有做数据约束
    /// </summary>
    public class StudentClick
    {
        public string stuId { get; set; }

        public string stuName { get; set; }

        public string stuDepart { get; set; }
    }

    /// <summary>
    /// 用于存放已点名的列表的数据结构
    /// </summary>
    public class ArealdyStudent : StudentClick { }
    #endregion
}
