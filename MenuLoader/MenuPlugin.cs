using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuLoader
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuPlugin
    {
        public const string PLUGIN = "Plugin";

        public ToolStripMenuItem PluginMenuItem;


        public ToolStripMenuItem GetPluginMenu(Form mainForm)
        {
            MenuStrip mainMenuStrip = mainForm.MainMenuStrip;
            ToolStripMenuItem menuPlugin = null;
            foreach (ToolStripMenuItem item in mainMenuStrip.Items)
            {
                if(item.Text.EndsWith(PLUGIN))
                {
                    menuPlugin = item;
                    break;
                }
            }
            if (menuPlugin == null)
            {
                menuPlugin = new ToolStripMenuItem(PLUGIN);
                mainMenuStrip.Items.Add(menuPlugin);
            }
            return menuPlugin;
        }

        public virtual void AddMenu(Form mainForm)
        {
            PluginMenuItem = GetPluginMenu(mainForm);
        }
    }
}
