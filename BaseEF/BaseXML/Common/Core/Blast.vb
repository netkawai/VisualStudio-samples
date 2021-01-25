Imports System.Runtime.Serialization

Namespace Core

    <DataContract(Name:="Blast", [Namespace]:="https://iring.ca")>
    Public Class Blast
        <DataMember()>
        Public Property OID As Integer
        <DataMember()>
        Public Property Id As String
        <DataMember()>
        Public Overridable Property Ringset As Ringset

    End Class

End Namespace