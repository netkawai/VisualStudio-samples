Imports System.IO
Imports System.Xml
Imports System.Runtime.Serialization

Module Program

#If False Then

    ReadOnly s_Terms As String() = New String() {"myLongRoot", "myLongChild", "myLongText", "http://www.w3.org/2001/XMLSchema-instance", "Items"}

    Public Class CustomXmlBinaryWriterSession
        Inherits XmlBinaryWriterSession

        Private m_Lock As Boolean

        Public Sub Lock()
            m_Lock = True
        End Sub

        Public Overrides Function TryAdd(ByVal value As XmlDictionaryString, ByRef key As Integer) As Boolean
            If m_Lock Then
                key = -1
                Return False
            End If

            Return MyBase.TryAdd(value, key)
        End Function
    End Class
    Private Sub InitializeWriter(ByRef dict As XmlDictionary, ByRef session As XmlBinaryWriterSession)
        dict = New XmlDictionary()
        Dim result = New CustomXmlBinaryWriterSession()
        Dim key = 0

        For Each term In s_Terms
            result.TryAdd(dict.Add(term), key)
        Next

        result.Lock()
        session = result
    End Sub
#End If

    Sub Main()

        Using fs As New FileStream("scenario.xml", FileMode.Create, FileAccess.Write, FileShare.None)

            Dim newScenario = New Core.Scenario
            newScenario.Id = "Scenario 1"
            Dim ringset1 = New Core.Ringset()
            ringset1.Id = "Ringset 1 - Scenario 1"
            Dim ringset2 = New Core.Ringset()
            ringset2.Id = "Ringset 2 - Scenario 1"
            newScenario.Ringsets = New List(Of Core.Ringset)
            newScenario.Ringsets.Add(ringset1)
            newScenario.Ringsets.Add(ringset2)

            Dim newScenario2 = New Core.Scenario

            newScenario2.Id = "Scenario 2"
            ringset1 = New Core.Ringset()
            ringset1.Id = "Ringset 1 - Scenario 2"
            ringset2 = New Core.Ringset()
            ringset2.Id = "Ringset 2 - Scenario 2"
            newScenario2.Ringsets = New List(Of Core.Ringset)
            newScenario2.Ringsets.Add(ringset1)
            newScenario2.Ringsets.Add(ringset2)


            Dim ser As DataContractSerializer = New DataContractSerializer(GetType(Core.Scenario))
            ser.WriteObject(fs, newScenario2)
        End Using

    End Sub

End Module
