Imports System
Imports Microsoft.Win32

Friend Class Reg
    Public Shared Sub Main()

        Get45PlusFromRegistry()
        Console.ReadKey()

        Dim rk As RegistryKey = Registry.CurrentUser
        PrintKeys(rk)

    End Sub

    Private Shared Sub Get45PlusFromRegistry()
        Const subkey As String = "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"

        Using ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey)

            If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                Console.WriteLine($".NET Framework Version: {CheckFor45PlusVersion(ndpKey.GetValue("Release"))}")
            Else
                Console.WriteLine(".NET Framework Version 4.5 or later is not detected.")
            End If
        End Using
    
    End Sub

    Private Shared Function CheckFor45PlusVersion(ByVal releaseKey As Integer) As String
        If releaseKey >= 528040 Then Return "4.8 or later"
        If releaseKey >= 461808 Then Return "4.7.2"
        If releaseKey >= 461308 Then Return "4.7.1"
        If releaseKey >= 460798 Then Return "4.7"
        If releaseKey >= 394802 Then Return "4.6.2"
        If releaseKey >= 394254 Then Return "4.6.1"
        If releaseKey >= 393295 Then Return "4.6"
        If releaseKey >= 379893 Then Return "4.5.2"
        If releaseKey >= 378675 Then Return "4.5.1"
        If releaseKey >= 378389 Then Return "4.5"
        ' This code should never execute. A non-null release key should mean
        ' that 4.5 or later is installed.
        Return "No 4.5 or later version detected"
    End Function

    Private Shared Sub PrintKeys(ByVal rkey As RegistryKey)
        Dim names As String() = rkey.GetSubKeyNames
        Dim icount = 0
        Console.WriteLine("Subkeys of " & rkey.Name)
        Console.WriteLine("-----------------------------------------------")

        For Each s In names
            Console.WriteLine(s)
            icount += 1
            If icount >= 10 Then Exit For
        Next

        Console.ReadKey()
    End Sub
End Class
