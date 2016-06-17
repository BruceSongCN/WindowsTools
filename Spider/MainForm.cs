using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spider
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // http://www.epianhong.com/web/gagmi?dzpid=22 河北列表-实时
            // http://www.epianhong.com/web/ggmi?id=11843 实时明细
            // http://www.epianhong.com/web/gad?gd=12647 30日

            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string strHtml = web.DownloadString("http://www.epianhong.com/web/gagmi?dzpid=22");

            JObject jo = JsonConvert.DeserializeObject<JObject>(strHtml);
            JToken[] jarray = jo["goods"].ToArray();

            foreach (var item in jarray)
            {
                MessageBox.Show(item["name"].ToString());
            }


            //MessageBox.Show();
            // double a = 1.3588827E7;
            // a = 4.02520125E7;
            // a = 4e10;
            // MessageBox.Show(a.ToString());
        }
    }
}
