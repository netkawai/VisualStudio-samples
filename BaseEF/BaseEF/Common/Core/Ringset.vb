Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.Linq.Mapping

Namespace Core
    <Table(Name:="Ringset")>
    Public Class Ringset

        <Column(Name:="OID", IsDbGenerated:=True, IsPrimaryKey:=True, DbType:="INTEGER")>
        <Key>
        Public Property OID As Integer

        <Column(Name:="Id", DbType:="VARCHAR")>
        Public Property Id As String


        Public Overridable Property Scenario As Scenario
#If False Then


        Public Overridable Property Rings As ICollection(Of Ring)

        Public Overridable Property Blasts As ICollection(Of Blast)

#End If

    End Class
End Namespace
