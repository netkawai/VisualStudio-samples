using System;

using System.Windows.Automation;
using System.Diagnostics; // for Process
using System.Threading; // for Thread.Sleep()

namespace TestScenario {
  class Program {
    static void Main(string[] args) {
      try {
        Console.WriteLine("\nBegin WPF UIAutomation test run\n");
        Console.WriteLine("Launching StatCalc application");
        Process p = Process.Start("..\\..\\..\\StatCalc\\bin\\Debug\\StatCalc.exe");
        Thread.Sleep(2000);

        AutomationElement aeDesktop = AutomationElement.RootElement;
        
        AutomationElement aeForm = null;
        int numWaits = 0;
        do {
          aeForm = aeDesktop.FindFirst(TreeScope.Children,
            new PropertyCondition(AutomationElement.NameProperty, "StatCalc"));
          Console.WriteLine("Looking for StatCalc . . . ");
          ++numWaits;
          Thread.Sleep(100);
        } while (aeForm == null && numWaits < 50);
        if (aeForm == null)
          throw new Exception("Failed to find StatCalc");
        else
          Console.WriteLine("Found it!");


        Console.WriteLine("Finding all user controls");
        AutomationElement aeButton = aeForm.FindFirst(TreeScope.Children,
          new PropertyCondition(AutomationElement.NameProperty, "Calculate"));
        AutomationElementCollection aeAllTextBoxes = aeForm.FindAll(TreeScope.Children,
          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
        AutomationElement aeTextBox1 = aeAllTextBoxes[1]; // caution: implied index order
        AutomationElement aeTextBox2 = aeAllTextBoxes[0];

        AutomationElement aeRadioButton1 = aeForm.FindFirst(TreeScope.Descendants,
          new PropertyCondition(AutomationElement.NameProperty, "Arithmetic Mean"));
        AutomationElement aeRadioButton2 = aeForm.FindFirst(TreeScope.Descendants,
          new PropertyCondition(AutomationElement.NameProperty, "Geometric Mean"));
        AutomationElement aeRadioButton3 = aeForm.FindFirst(TreeScope.Descendants,
          new PropertyCondition(AutomationElement.NameProperty, "Harmonic Mean"));

        Console.WriteLine("\nSetting input to '30 60'");
        ValuePattern vpTextBox1 = (ValuePattern)aeTextBox1.GetCurrentPattern(ValuePattern.Pattern);
        vpTextBox1.SetValue("30 60");

        Console.WriteLine("Selecting 'Geometric Mean' ");
        SelectionItemPattern ipSelectRadioButton2 =
          (SelectionItemPattern)aeRadioButton2.GetCurrentPattern(SelectionItemPattern.Pattern);
        ipSelectRadioButton2.Select();

        Console.WriteLine("Clicking on Calculate button");
        InvokePattern ipClickButton1 =
          (InvokePattern)aeButton.GetCurrentPattern(InvokePattern.Pattern);
        ipClickButton1.Invoke();
        Thread.Sleep(1500);

        Console.WriteLine("\nChecking textBox2 for '42.4264'");
        string result =
          (string)aeTextBox2.GetCurrentPropertyValue(ValuePattern.ValueProperty);

        if (result == "42.4264") {
          Console.WriteLine("Found it");
          Console.WriteLine("\nTest scenario: Pass");
        }
        else {
          Console.WriteLine("Did not find it");
          Console.WriteLine("\nTest scenario: *FAIL*");
        }

        Console.WriteLine("\nClosing application in 5 seconds");
        Thread.Sleep(5000);
        WindowPattern wpCloseForm =
          (WindowPattern)aeForm.GetCurrentPattern(WindowPattern.Pattern);
        wpCloseForm.Close();
       
        Console.WriteLine("\nEnd test run\n");
        Console.ReadLine();
      }
      catch(Exception ex) {
        Console.WriteLine("Fatal error: " + ex.Message);
        Console.ReadLine();
      }
    } // Main()
 
  } // class
} // ns
