using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Layout;
using CoreUtilities;

namespace WriteThinker
{
    public partial class fStatPanel : Form
    {
        public fStatPanel()
        {
			this.Icon = LayoutDetails.Instance.MainFormIcon;
			FormUtils.SizeFormsForAccessibility(this, LayoutDetails.Instance.MainFormFontSize);
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
    }
}