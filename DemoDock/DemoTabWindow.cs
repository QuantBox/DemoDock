using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartQuant.Shared;
using TD.SandDock;

namespace DemoDock
{
    // 注意：将UserControl改成DockWindow后界面设计器将不可用
    public partial class DemoTabWindow : DockWindow//  //UserControl
    {
        public DemoTabWindow()
        {
            InitializeComponent();

            base.Control = demoTabControl; // 此句必须要加，不然报空指针错误

            base.DefaultDockLocation = ContainerDockLocation.Center;
            base.Text = "DemoTab";
        }

        protected override void OnInit()
        {
            base.OnInit();
            int key = (int)base.Key;
            this.Text = string.Format("DemoTab[{0}]",key);
        }
    }
}
