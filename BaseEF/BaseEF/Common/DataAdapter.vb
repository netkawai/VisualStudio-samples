Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.SQLite

Namespace Common
    Public Class DataAdapter

        Private Shared _instance As DataAdapter
        Private _ctx As Internal.DatabaseContext

        ' Signleton pattern
        Private Sub New()
            _ctx = New Internal.DatabaseContext
        End Sub


        Public Shared Function GetInstance() As DataAdapter
            If _instance Is Nothing Then
                _instance = New DataAdapter
            End If
            Return _instance
        End Function

        Public Sub Add(s As Core.Scenario)
            _ctx.ProjectMaster.Add(s)
        End Sub

        Public Sub Save()
            _ctx.SaveChanges()
        End Sub

    End Class

End Namespace
