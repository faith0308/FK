using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Singleton;


namespace WFApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbltxt.Text = "测试单例设计模式：";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ShowIt();
        }

        void ShowIt()
        {
            for (int i = 0; i < 10; i++)
            {
                var singleton = Singleton.Singleton.CreateInstance().GetString("我被执行了！");
                lbltxt.Text += singleton;
            }

        }
    }
}
