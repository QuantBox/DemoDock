using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartQuant;
using SmartQuant.Controls;

namespace DemoDock
{
    // 此处将UserControl改成FrameworkControl设计器不会出错
    public partial class DemoTabControl : FrameworkControl
    {
        private Dictionary<int, TabViewer> viewers;

        public DemoTabControl()
        {
            InitializeComponent();

            viewers = new Dictionary<int, TabViewer>();
        }

        protected override void OnInit()
        {
            base.framework.EventManager.Dispatcher.Trade += Dispatcher_Trade;
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.framework.EventManager.Dispatcher.Trade -= Dispatcher_Trade;
        }

        void Dispatcher_Trade(object sender, SmartQuant.Trade trade)
        {
            base.InvokeAction(() => { OnTrade(sender,trade); });
        }

        private void OnTrade(object sender,Trade trade)
        {
            TabViewer viewer;
            int key = trade.InstrumentId;
            if (!this.viewers.TryGetValue(key, out viewer))
            {
                viewer = new TabViewer(){Dock = DockStyle.Fill};
                this.viewers.Add(key,viewer);
                TabPage page = new TabPage();
                var instrument = base.framework.InstrumentManager.GetById(key);
                page.Text = instrument.Symbol;
                page.Controls.Add(viewer);
                this.tabViewers.TabPages.Add(page);
            }
            viewer.OnTrade(sender,trade);
        }

        public void UpdateGUI()
        {
            // 这里可以参考PortfolioList.UpdateGUI()的写法
            // 它使用了一个消息队列，用于实际刷新时进行要做的事情的判断
            // 我们也可以使用这种方法解决全部刷新可能占用大量资源的问题
        }
    }
}
