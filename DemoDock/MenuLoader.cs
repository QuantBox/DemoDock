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
            PluginMenuItem = GetPluginMenu(mainForm);

            ToolStripMenuItem menuPlugin_Demo = new ToolStripMenuItem("Demo");
            PluginMenuItem.DropDownItems.Add(menuPlugin_Demo);

            menuPlugin_Demo.Click += menuPlugin_Demo_Click;
        }

        private void menuPlugin_Demo_Click(object sender, EventArgs e)
        {
            //Global.DockManager.Open(typeof(DemoTabWindow));
            Global.DockManager.Open(typeof(DemoTabWindow), new Random().Next());
        }
    }
}
