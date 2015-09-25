using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartQuant.Shared;

using MenuLoader;

namespace DemoDock
{
    /// <summary>
    /// 菜单加载器
    /// </summary>
    public class MenuLoader : MenuPlugin
    {
        public override void AddMenu(Form mainForm)
        {
            // 创建一个Plugin菜单
            PluginMenuItem = GetPluginMenu(mainForm);

            // 在Plugin菜单下挂一下Demo菜单
            ToolStripMenuItem menuPlugin_Demo = new ToolStripMenuItem("弹出自己的Dock窗口");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo);
            menuPlugin_Demo.Click += menuPlugin_Demo_Click;

            // 添加分隔符
            ToolStripSeparator menu_Sepearator = new ToolStripSeparator();
            PluginMenuItem.DropDownItems.Add(menu_Sepearator);

            // 参考QuoteMonitor的方式，再挂二级菜单
            ToolStripMenuItem menuPlugin_Demo2 = new ToolStripMenuItem("显示二级菜单");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo2);
            menuPlugin_Demo2.DropDownOpening += menuPlugin_Demo2_DropDownOpening;

            // 在Plugin菜单下挂一下Demo菜单
            ToolStripMenuItem menuPlugin_Demo3 = new ToolStripMenuItem("弹出IDE的窗口并对其进行修改");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo3);
            menuPlugin_Demo3.Click += menuPlugin_Demo3_Click;
        }

        void menuPlugin_Demo2_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem menuPlugin_Demo2 = sender as ToolStripMenuItem;
            menuPlugin_Demo2.DropDownItems.Clear();
            for (int i = 0; i < 10; ++i)
            {
                // 定义了一个子类用来传参数
                IntMenuItem menuPlugin_Demo2_N = new IntMenuItem(i);
                menuPlugin_Demo2.DropDownItems.Add(menuPlugin_Demo2_N);
                menuPlugin_Demo2_N.Click += menuPlugin_Demo2_N_Click;
            }
        }

        void menuPlugin_Demo2_N_Click(object sender, EventArgs e)
        {
            // 此处示范传参数到窗口中去，用户可以自己实现QuoteMonitor那种传一个IDataProvder
            Global.DockManager.Open(typeof(DemoTabWindow), (sender as IntMenuItem).Int);
        }

        private void menuPlugin_Demo_Click(object sender, EventArgs e)
        {
            Global.DockManager.Open(typeof(DemoTabWindow));
        }

        private void menuPlugin_Demo3_Click(object sender, EventArgs e)
        {
            var window = Global.DockManager.Open(typeof(AccountDataWindow));

            // 通过不同颜色来区分不同区域
            window.BackColor = Color.Red;
            // 从反编译的代码可以知道AccountDataWindow中只放了一个AccountData
            window.Controls[0].BackColor = Color.Blue;
            // AccountData上放了TabControl
            window.Controls[0].Controls[0].BackColor = Color.Black;
            // 由于TabControl中为空，所以自己添加一下
            TabControl tagControl = window.Controls[0].Controls[0] as TabControl;

            // 添加一页
            TabPage page = new TabPage();
            page.Text = "DEMO3";
            tagControl.TabPages.Add(page);

            // 添加按钮
            Button btn = new Button();
            btn.Text = "Click Me!";
            btn.Click += btn_Click;
            page.Controls.Add(btn);
        }

        void btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }
    }
}
