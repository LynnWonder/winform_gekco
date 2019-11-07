using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gecko;


namespace WindowsFormsApplication1
{
    public partial class FormTest : Form
    {
        static private string xulrunnerPath = Application.StartupPath + "\\xulrunner";
        static private string testUrl = Application.StartupPath + "\\index.html";
        GeckoWebBrowser gecko;
        private string strTitle;
        public FormTest()
        {
            InitializeComponent();
           
            InitAll();
        }
        public void InitAll()
        {
            
            Xpcom.Initialize(xulrunnerPath);
            gecko = new GeckoWebBrowser();
            gecko.CreateControl();
            gecko.NoDefaultContextMenu = true; //禁用右键菜单
            gecko.Dock = DockStyle.Fill;
            panel1.Controls.Add(gecko); 
            gecko.ProgressChanged += Gecko_ProgressChanged;
            gecko.CreateWindow += Gecko_CreateWindow;
            gecko.DocumentCompleted += Gecko_DocumentCompleted;
            //gecko.Navigate("http://echarts.baidu.com");
            gecko.Navigate(testUrl);   
        }
        
        private void Gecko_DocumentCompleted(object sender, EventArgs e)
        {
            //调JQuery
            var executor = new Gecko.JQuery.JQueryExecutor(gecko.Window);  //先获取到jquery对象
            executor.ExecuteJQuery("$('#btn').click()");    //然后执行jquery的代码

            //调JS
            using (AutoJSContext context = new AutoJSContext(gecko.Window.JSContext))
            {
                //string str = this.label.Text.ToString();
                string title = "(winform向js传值)winform与gecko互动测试TESTTESTTESTTESTTEST!!!!!!";
                JsVal jv = context.EvaluateScript(@"print('" + title + "')", (nsIDOMWindow)gecko.Window.DomWindow);
                strTitle = jv.ToString();
                this.label.Text = strTitle;
                int a = 5;
                JsVal jv2 = context.EvaluateScript(@"getTotal(" + a + ")", (nsIDOMWindow)gecko.Window.DomWindow);
                int tt = jv2.ToInteger();
              //string result;
              //context.EvaluateScript("3 + 2;", out result);
              //context.EvaluateScript("'hello' + ' ' + 'world';", out result);
              //context.EvaluateScript("document.write('f');", out result);
              //context.EvaluateScript(@"window.alert('alert')", out result);
              //context.EvaluateScript(@"window.alert('alert')", (nsISupports)gecko.Window.DomWindow, out result);
            }
            progressBar1.Value = 0;
        }
        private void Gecko_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            //e.WebBrowser.Height = 500;
             e.InitialHeight = 500;
           // e.WebBrowser.Width = 500;
            e.InitialWidth = 500;

        }

        private void Gecko_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            if (e.MaximumProgress == 0)
                return;

            var value = (int)Math.Min(100, (e.CurrentProgress * 100) / e.MaximumProgress);
            if (value == 100)
                return;
            progressBar1.Value = value;
        }
    }
}
