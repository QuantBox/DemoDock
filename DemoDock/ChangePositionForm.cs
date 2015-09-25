using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartQuant;
using SmartQuant.Shared;

namespace DemoDock
{
    public partial class ChangePositionForm : Form
    {
        public Instrument Instrument;
        public decimal Amount;
        public ChangePositionForm()
        {
            InitializeComponent();
            this.numericUpDown_Amout.Maximum = decimal.MaxValue;
            this.numericUpDown_Amout.Minimum = decimal.MinValue;
            //this.UpdateOKButtonStatus();
        }

        private void UpdateOKButtonStatus()
        {
            this.buttonOK.Enabled = !string.IsNullOrEmpty(this.textBox_Instrument.Text);
        }

        private void textBox_Instrument_Validating(object sender, CancelEventArgs e)
        {
            Instrument = SmartQuant.Shared.Global.Framework.InstrumentManager.Get((sender as TextBox).Text);
            errorProvider1.Clear();
            if (Instrument == null)
            {
                errorProvider1.SetError(textBox_Instrument,"合约不存在");
            }
        }

        private void numericUpDown_Amout_Validating(object sender, CancelEventArgs e)
        {
            Amount = numericUpDown_Amout.Value;
            errorProvider1.Clear();
            if (Amount == 0)
            {
                errorProvider1.SetError(numericUpDown_Amout, "数量为0，不进行操作");
            }
        }
    }
}
