Module Program
    <STAThread>
    Public Sub Main()

        Dim s As Settings = Settings.Instance
        s.Height = 2.0
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

End Module
