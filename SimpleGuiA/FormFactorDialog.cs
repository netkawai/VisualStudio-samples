using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWindowsForms
{
    public partial class FormFactorDialog : Form
    {
        public FormFactorDialog()
        {
            InitializeComponent();
        }

        public string getFormFactorName()
        {
            return formFactorTxtBox.Text.Trim();
        }
    }
}
