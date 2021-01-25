Imports System.Runtime.Serialization

Namespace Core

    <DataContract(Name:="Scenario", [Namespace]:="https://iring.ca")>
    Public Class Scenario

        Public Property OID As Integer

        Public Property Id As String


        Public Overridable Property Ringsets As ICollection(Of Ringset)

    End Class

End Namespace
