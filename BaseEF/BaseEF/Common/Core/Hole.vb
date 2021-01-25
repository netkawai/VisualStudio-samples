Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Linq.Mapping

Namespace Core

    Public Class Hole

        <Column(Name:="OID", IsDbGenerated:=True, IsPrimaryKey:=True, DbType:="INTEGER")>
        <Key>
        Public Property OID As Integer

        <Column(Name:="Id", DbType:="VARCHAR")>
        Public Property Id As String

        Public Overridable Property Slot As Slot

        Public Overridable Property Ring As Ring

        Public Overridable Property Charges As ICollection(Of Charge)


    End Class
End Namespace
