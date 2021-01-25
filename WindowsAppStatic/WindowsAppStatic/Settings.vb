Public Class Settings
    Implements ICloneable

    Private Shared _instance As New Settings

    Public Shared ReadOnly Property Instance As Settings
        Get
            Return _instance
        End Get
    End Property

    Private _isShowDisplay As Boolean
    Public Property IsShowDisplay As Boolean
        Get
            Return _isShowDisplay
        End Get
        Set(value As Boolean)
            If value = _isShowDisplay Then Return
            _isShowDisplay = value
        End Set
    End Property
    Private _height As Double
    Public Property Height As Double
        Get
            Return _height
        End Get
        Set(value As Double)
            If value = _height Then Return
            _height = value
        End Set
    End Property

    Public Sub New()
        Initialize()
    End Sub

    Private Sub Initialize()
        _isShowDisplay = False
        _height = 0.1
    End Sub

    <Obsolete>
    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function

    Public Sub Load(item As Settings)
        Me._isShowDisplay = item._isShowDisplay
        Me._height = item._height
    End Sub

    Public Function Clone(isDeep As Boolean) As Settings
        If isDeep Then
            Dim new_item As Settings = New Settings
            new_item._isShowDisplay = Me.IsShowDisplay
            new_item._height = Me.Height
            Return new_item
        Else
            Return Me
        End If
    End Function

End Class
