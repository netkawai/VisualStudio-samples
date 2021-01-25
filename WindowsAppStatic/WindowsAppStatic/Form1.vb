Public Class Form1

    Private _settings As Settings = Settings.Instance
    Private _backupSettings As Settings
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _backupSettings = Me._settings.Clone(True)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Console.WriteLine(Me._settings.Height)
        Console.WriteLine(Me._settings.IsShowDisplay)
        Me._settings.IsShowDisplay = True
        Me._settings.Height = 0.01
        Console.WriteLine(Me._settings.IsShowDisplay)
        Console.WriteLine(Me._settings.Height)


        Me._settings.Load(_backupSettings)

        Console.WriteLine(Me._settings.IsShowDisplay)
        Console.WriteLine(Me._settings.Height)
    End Sub

End Class
