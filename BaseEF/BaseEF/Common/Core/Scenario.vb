Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Linq.Mapping

Namespace Core

    <Table(Name:="Scenario")>
    Public Class Scenario

        <Column(Name:="OID", IsDbGenerated:=True, IsPrimaryKey:=True, DbType:="INTEGER")>
        <Key>
        Public Property OID As Integer

        <Column(Name:="Id", DbType:="VARCHAR")>
        Public Property Id As String


        Public Overridable Property Ringsets As ICollection(Of Ringset)

    End Class

End Namespace
