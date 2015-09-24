using System;
using System.Collections.Generic;
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
            ToolStripMenuItem menuPlugin_Demo = new ToolStripMenuItem("Demo");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo);
            menuPlugin_Demo.Click += menuPlugin_Demo_Click;

            // 添加分隔符
            ToolStripSeparator menu_Sepearator = new ToolStripSeparator();
            PluginMenuItem.DropDownItems.Add(menu_Sepearator);

            // 参考QuoteMonitor的方式，再挂二级菜单
            ToolStripMenuItem menuPlugin_Demo2 = new ToolStripMenuItem("Demo2");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo2);
            menuPlugin_Demo2.DropDownOpening += menuPlugin_Demo2_DropDownOpening;
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
    }
}
