Friend Class TemperatureField
    Public Key As String

    Public Property Count() As Integer

    Public Property IsDelete() As Boolean

    Public Property Comment() As String

    Public Overrides Function ToString() As String
        Return Comment
    End Function
End Class