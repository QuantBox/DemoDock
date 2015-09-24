using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoDock
{
    class IntMenuItem : ToolStripMenuItem
    {
        public int Int { get; private set; }
        public IntMenuItem(int Int):base(Int.ToString())
        {
            this.Int = Int;
        }
    }
}
