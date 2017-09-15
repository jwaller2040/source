Public Interface IGenerate

#Region "Properties"

    Property Settings() As Settings

#End Region

#Region "Methods"

    Function GenerateClass(ByVal xml As String) As String

    Function GenerateInnerClass(ByVal xml As String) As String

    Function GenerateInnerClass(ByVal xml As System.Xml.XmlNode) As String

#End Region

End Interface