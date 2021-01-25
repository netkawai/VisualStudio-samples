using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContextMenuStripSample
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                var myMenu = new System.Windows.Controls.ContextMenu();
                myMenu.Items.Add("One");
                myMenu.Items.Add("Two");
                myMenu.Items.Add("Three");
                myMenu.IsOpen = true;
            }
        }
    }
}
