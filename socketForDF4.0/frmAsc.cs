using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace socketForDF4._0
{
    public partial class frmAsc : Form
    {
        public frmAsc()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAsc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtChar.Text = Convert.ToChar(int.Parse(txtAsc.Text)).ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
