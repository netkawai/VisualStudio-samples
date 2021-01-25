using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StatCalc {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {
      string[] sVals = textBox1.Text.Split(' ');
      int[] iVals = new int[sVals.Length];
      for (int i = 0; i < iVals.Length; ++i)
        iVals[i] = int.Parse(sVals[i]);
      if (radioButton1.Checked) {
        double sum = 0.0;
        foreach (int v in iVals)
          sum += v;
        double result = (double)(sum / iVals.Length);
        textBox2.Text = result.ToString("F4");
      }
      else if (radioButton2.Checked) {
        double product = 1.0;
        foreach (int v in iVals)
        product *= (double)v;
        double result = NthRoot(product, iVals.Length);
        textBox2.Text = result.ToString("F4");
      }
      else if (radioButton3.Checked) {
        double sum = 0.0;
        foreach (int v in iVals)
          sum += (1/ (double)v);
        double result = (double)(iVals.Length / sum);
        textBox2.Text = result.ToString("F4");
      }
    } // button1_Click

    private void radioButton3_CheckedChanged(object sender, EventArgs e) {

    }

    private static double NthRoot(double x, int n) {
      return Math.Exp( Math.Log(x) / (double)n );
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      Application.Exit();
    }

  } // class
} // ns