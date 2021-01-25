
Namespace Core

    Public Class Ring

        Public Property OID As Integer

        Public Property Id As String

        Public Overridable Property Ringset As Ringset

        Public Overridable Property Holes As ICollection(Of Hole)

        Public Overridable Property Slot As Slot

    End Class

End Namespace