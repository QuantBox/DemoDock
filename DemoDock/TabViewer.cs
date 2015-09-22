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

namespace DemoDock
{
    public partial class TabViewer : UserControl
    {
        public TabViewer()
        {
            InitializeComponent();
        }

        public void OnTrade(object sender, Trade trade)
        {
            this.label1.Text = trade.ToString();
        }
    }
}
