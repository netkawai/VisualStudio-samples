using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ContextMenuStripSample
{
    // This code example demonstrates how to set the check
    // and image margins for a ToolStripMenuItem.
    class Form5 : Form
    {
        public Form5()
        {
            // Size the form to show three wide menu items.
            this.Width = 500;
            this.Text = "ToolStripContextMenuStrip: Image and Check Margins";

            // Create a new MenuStrip control.
            MenuStrip ms = new MenuStrip();

            // Create the ToolStripMenuItems for the MenuStrip control.
            ToolStripMenuItem bothMargins = new ToolStripMenuItem("BothMargins");
            ToolStripMenuItem imageMarginOnly = new ToolStripMenuItem("ImageMargin");
            ToolStripMenuItem checkMarginOnly = new ToolStripMenuItem("CheckMargin");
            ToolStripMenuItem noMargins = new ToolStripMenuItem("NoMargins");

            // Customize the DropDowns menus.
            // This ToolStripMenuItem has an image margin
            // and a check margin.
            bothMargins.DropDown = CreateCheckImageContextMenuStrip();
            ((ContextMenuStrip)bothMargins.DropDown).ShowImageMargin = true;
            ((ContextMenuStrip)bothMargins.DropDown).ShowCheckMargin = true;

            // This ToolStripMenuItem has only an image margin.
            imageMarginOnly.DropDown = CreateCheckImageContextMenuStrip();
            ((ContextMenuStrip)imageMarginOnly.DropDown).ShowImageMargin = true;
            ((ContextMenuStrip)imageMarginOnly.DropDown).ShowCheckMargin = false;

            // This ToolStripMenuItem has only a check margin.
            checkMarginOnly.DropDown = CreateCheckImageContextMenuStrip();
            ((ContextMenuStrip)checkMarginOnly.DropDown).ShowImageMargin = false;
            ((ContextMenuStrip)checkMarginOnly.DropDown).ShowCheckMargin = true;

            // This ToolStripMenuItem has no image and no check margin.
            noMargins.DropDown = CreateCheckImageContextMenuStrip();
            ((ContextMenuStrip)noMargins.DropDown).ShowImageMargin = false;
            ((ContextMenuStrip)noMargins.DropDown).ShowCheckMargin = false;

            // Populate the MenuStrip control with the ToolStripMenuItems.
            ms.Items.Add(bothMargins);
            ms.Items.Add(imageMarginOnly);
            ms.Items.Add(checkMarginOnly);
            ms.Items.Add(noMargins);

            // Dock the MenuStrip control to the top of the form.
            ms.Dock = DockStyle.Top;

            // Add the MenuStrip control to the controls collection last.
            // This is important for correct placement in the z-order.
            this.Controls.Add(ms);
        }

        // This utility method creates a Bitmap for use in
        // a ToolStripMenuItem's image margin.
        internal Bitmap CreateSampleBitmap()
        {
            // The Bitmap is a smiley face.
            Bitmap sampleBitmap = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(sampleBitmap);

            using (Pen p = new Pen(ProfessionalColors.ButtonPressedBorder))
            {
                // Set the Pen width.
                p.Width = 4;

                // Set up the mouth geometry.
                Point[] curvePoints = new Point[]{
                new Point(4,14),
                new Point(16,24),
                new Point(28,14)};

                // Draw the mouth.
                g.DrawCurve(p, curvePoints);

                // Draw the eyes.
                g.DrawEllipse(p, new Rectangle(new Point(7, 4), new Size(3, 3)));
                g.DrawEllipse(p, new Rectangle(new Point(22, 4), new Size(3, 3)));
            }

            return sampleBitmap;
        }

        // This utility method creates a ContextMenuStrip control
        // that has four ToolStripMenuItems showing the four
        // possible combinations of image and check margins.
        internal ContextMenuStrip CreateCheckImageContextMenuStrip()
        {
            // Create a new ContextMenuStrip control.
            ContextMenuStrip checkImageContextMenuStrip = new ContextMenuStrip();

            // Create a ToolStripMenuItem with a
            // check margin and an image margin.
            ToolStripMenuItem yesCheckYesImage =
                new ToolStripMenuItem("Check, Image");
            yesCheckYesImage.Checked = true;
            yesCheckYesImage.Image = CreateSampleBitmap();

            // Create a ToolStripMenuItem with no
            // check margin and with an image margin.
            ToolStripMenuItem noCheckYesImage =
                new ToolStripMenuItem("No Check, Image");
            noCheckYesImage.Checked = false;
            noCheckYesImage.Image = CreateSampleBitmap();

            // Create a ToolStripMenuItem with a
            // check margin and without an image margin.
            ToolStripMenuItem yesCheckNoImage =
                new ToolStripMenuItem("Check, No Image");
            yesCheckNoImage.Checked = true;

            // Create a ToolStripMenuItem with no
            // check margin and no image margin.
            ToolStripMenuItem noCheckNoImage =
                new ToolStripMenuItem("No Check, No Image");
            noCheckNoImage.Checked = false;

            // Add the ToolStripMenuItems to the ContextMenuStrip control.
            checkImageContextMenuStrip.Items.Add(yesCheckYesImage);
            checkImageContextMenuStrip.Items.Add(noCheckYesImage);
            checkImageContextMenuStrip.Items.Add(yesCheckNoImage);
            checkImageContextMenuStrip.Items.Add(noCheckNoImage);

            return checkImageContextMenuStrip;
        }
    }
}
