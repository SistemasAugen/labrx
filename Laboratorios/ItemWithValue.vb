Public Class ItemWithValue
    Private sName As String
    ' You can also declare this as String,bitmap or almost anything. 
    ' If you change this delcaration you will also need to change the Sub New 
    ' to reflect any change. Also the ItemData Property will need to be updated. 
    Private iID As String

    ' Default empty constructor. 
    Public Sub New()
        sName = ""
        ' This would need to be changed if you modified the declaration above. 
        iID = 0
    End Sub

    Public Sub New(ByVal Name As String, ByVal Value As String)
        sName = Name
        iID = Value
    End Sub

    Public Property Name() As String
        Get
            Return sName
        End Get
        Set(ByVal sValue As String)
            sName = sValue
        End Set
    End Property

    ' This is the property that holds the extra data. 
    Public Property Value() As String
        Get
            Return iID
        End Get
        Set(ByVal iValue As String)
            iID = iValue
        End Set
    End Property

    ' This is neccessary because the ListBox and ComboBox rely 
    ' on this method when determining the text to display. 
    Public Overrides Function ToString() As String
        Return sName
    End Function

End Class
