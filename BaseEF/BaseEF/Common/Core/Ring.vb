Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Linq.Mapping

Namespace Core

    Public Class Ring

        <Column(Name:="OID", IsDbGenerated:=True, IsPrimaryKey:=True, DbType:="INTEGER")>
        <Key>
        Public Property OID As Integer

        <Column(Name:="Id", DbType:="VARCHAR")>
        Public Property Id As String

        Public Overridable Property Ringset As Ringset

        Public Overridable Property Holes As ICollection(Of Hole)

        Public Overridable Property Slot As Slot

    End Class

End Namespace