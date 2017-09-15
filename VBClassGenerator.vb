Imports System.Collections.Generic
Imports System.Text
Imports System.Xml

Public Class VBClassGenerator
    Implements IGenerate

#Region "Declarations"

    Private _NameCollection As Dictionary(Of String, Integer)
    Private _PublicClassNameCollection As Dictionary(Of String, Integer)
    Private _Settings As Settings
    Private _SubClasses As List(Of String)

#End Region

#Region "Properties"

    Public Property Settings() As Settings Implements IGenerate.Settings
        Get
            Return _Settings
        End Get
        Set(ByVal Value As Settings)
            _Settings = Value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal oSettings As Settings)
        _PublicClassNameCollection = New Dictionary(Of String, Integer)
        _NameCollection = New Dictionary(Of String, Integer)
        _Settings = oSettings
    End Sub

#End Region

#Region "Public Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generates the Class file.
    ''' </summary>
    ''' <param name="xml">Source XML</param>
    ''' <returns>returns the generated class</returns>
    ''' -----------------------------------------------------------------------------
    Public Function GenerateClass(ByVal xml As String) As String Implements IGenerate.GenerateClass
        Dim xmlDoc As New XmlDocument
        Dim node As XmlNode
        Dim childNodes As XmlNodeList
        Dim nodeIndex As Integer
        Dim attribIndex As Integer
        Dim nodeCount As Integer
        Dim attribCount As Integer = 0
        Dim nameCollection As New List(Of String)
        Dim attribNameTable As New Dictionary(Of String, List(Of String))
        Dim generateChildren As Boolean = False
        Dim hasAttributes As Boolean = False
        Dim sClass As New StringBuilder
        _SubClasses = New List(Of String)

        Try
            xmlDoc.LoadXml(xml)
            childNodes = xmlDoc.DocumentElement.ChildNodes()
            _PublicClassNameCollection = New Dictionary(Of String, Integer)
            _NameCollection = New Dictionary(Of String, Integer)
            Dim attr As Integer = 0
            Dim counter As Integer = 0
            With sClass
                If Settings.MakeSerializable Then
                    .Append("<Serializable()> ")
                End If
                .Append("Public Class ")
                .Append(PublicClassNameMatchSetter(xmlDoc.DocumentElement.Name))
                .Append(vbCrLf)
                .Append("#Region "" Properties """)
                .Append(vbCrLf)

                If Settings.GenerateAttributes AndAlso Not xmlDoc.DocumentElement.Attributes Is Nothing AndAlso xmlDoc.DocumentElement.Attributes.Count > 0 Then
                    attr = xmlDoc.DocumentElement.Attributes.Count
                    For attribIndex = 0 To attr - 1
                        'Generate Property Declaration
                        .Append(GenerateDeclaration(String.Concat(xmlDoc.DocumentElement.Name, "_", xmlDoc.DocumentElement.Attributes(attribIndex).Name)))
                        .Append(vbCrLf)
                        .Append(GenerateProperty(String.Concat(xmlDoc.DocumentElement.Name, "_", xmlDoc.DocumentElement.Attributes(attribIndex).Name)))
                        .Append(vbCrLf)
                        AddAttributeTableForXMLDoc(xmlDoc, attribIndex, attribNameTable)
                    Next
                End If

                nodeCount = childNodes.Count
                For nodeIndex = 0 To nodeCount - 1
                    node = childNodes(nodeIndex)
                    generateChildren = False
                    hasAttributes = False
                    If node.HasChildNodes Then
                        CheckForChildren(node, generateChildren)
                    End If

                    If generateChildren Then
                        .Append(GeneratePublicDeclaration(node.Name))
                        .Append(vbCrLf)
                        _SubClasses.Add(GenerateInnerClass(node.OuterXml))
                        nameCollection.Add(String.Concat("%", node.Name, "Collection"))
                    Else
                        'Generate Property Declaration
                        .Append(GenerateDeclaration(node.Name))
                        .Append(vbCrLf)
                        .Append(GenerateProperty(node.Name))
                        nameCollection.Add(node.Name)
                        .Append(vbCrLf)
                        If Settings.GenerateAttributes AndAlso Not node.Attributes Is Nothing Then
                            attribCount = node.Attributes.Count
                        Else
                            attribCount = 0
                        End If

                        If attribCount > 0 Then
                            For attribIndex = 0 To attribCount - 1
                                'Generate Property Declaration
                                .Append(GenerateDeclaration(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                .Append(vbCrLf)
                                .Append(GenerateProperty(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                .Append(vbCrLf)

                                If attribNameTable.ContainsKey(node.Name) Then
                                    attribNameTable(node.Name).Add(node.Attributes(attribIndex).Name)
                                Else
                                    attribNameTable.Add(node.Name, New List(Of String))
                                    attribNameTable(node.Name).Add(node.Attributes(attribIndex).Name)
                                End If
                            Next
                        End If
                    End If
                Next

                .Append(vbCrLf)
                .Append("#End Region")
                .Append(vbCrLf)

                BuildMethods(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                .Append(" End Class ")
                .Append(vbCrLf)
                For counter = 0 To _SubClasses.Count - 1
                    .Append(_SubClasses.Item(counter))
                Next
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return sClass.ToString()
    End Function

    Public Function GenerateInnerClass(ByVal xml As String) As String Implements IGenerate.GenerateInnerClass
        Dim xmlDoc As New XmlDocument
        Dim node As XmlNode
        Dim childNodes As XmlNodeList
        Dim nodeIndex As Integer
        Dim attribIndex As Integer
        Dim nodeCount As Integer
        Dim attribCount As Integer = 0
        Dim nameCollection As New List(Of String)
        Dim attribNameTable As New Dictionary(Of String, List(Of String))
        Dim createSubNew As Boolean = False
        Dim hasAttributes As Boolean = False
        Dim sClass As New StringBuilder
        Dim generateChildren As Boolean = False

        Try
            xmlDoc.LoadXml(xml)
            childNodes = xmlDoc.DocumentElement.ChildNodes()
            Dim attr As Integer = 0
            With sClass
                .Append(vbCrLf)
                If Settings.MakeSerializable Then
                    .Append("<Serializable()> ")
                End If
                .Append("Public Class ")
                .Append(PublicClassNameMatchSetter(xmlDoc.DocumentElement.Name))
                .Append(vbCrLf)
                .Append("#Region "" Properties """)
                .Append(vbCrLf)

                If Settings.GenerateAttributes AndAlso Not xmlDoc.DocumentElement.Attributes Is Nothing AndAlso xmlDoc.DocumentElement.Attributes.Count > 0 Then
                    createSubNew = True
                    attr = xmlDoc.DocumentElement.Attributes.Count
                    If Not xmlDoc.DocumentElement.Attributes Is Nothing Then
                        attr = xmlDoc.DocumentElement.Attributes.Count
                        For attribIndex = 0 To attr - 1
                            .Append(GenerateDeclaration(String.Concat(xmlDoc.DocumentElement.Name, "_", xmlDoc.DocumentElement.Attributes(attribIndex).Name)))
                            .Append(vbCrLf)
                            .Append(GenerateProperty(String.Concat(xmlDoc.DocumentElement.Name, "_", xmlDoc.DocumentElement.Attributes(attribIndex).Name)))
                            .Append(vbCrLf)
                            AddAttributeTableForXMLDoc(xmlDoc, attribIndex, attribNameTable)
                        Next
                    End If
                End If

                nodeCount = childNodes.Count
                For nodeIndex = 0 To nodeCount - 1
                    node = childNodes(nodeIndex)

                    If node.NodeType = XmlNodeType.Element Then
                        createSubNew = True
                        generateChildren = False
                        hasAttributes = False
                        If node.HasChildNodes Then
                            CheckForChildren(node, generateChildren)
                        End If

                        If generateChildren Then
                            .Append(GeneratePublicDeclaration(node.Name))
                            .Append(vbCrLf)
                            _SubClasses.Add(GenerateInnerClass(node.OuterXml))
                            nameCollection.Add(String.Concat("%", node.Name, "Collection"))
                        Else
                            'Generate Property Declaration
                            .Append(GenerateDeclaration(node.Name))
                            .Append(vbCrLf)
                            .Append(GenerateProperty(node.Name))
                            nameCollection.Add(node.Name)
                            .Append(vbCrLf)
                            If Settings.GenerateAttributes AndAlso Not node.Attributes Is Nothing Then
                                attribCount = node.Attributes.Count
                            Else
                                attribCount = 0
                            End If

                            If attribCount > 0 Then
                                For attribIndex = 0 To attribCount - 1
                                    'Generate Property Declaration
                                    .Append(GenerateDeclaration(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                    .Append(vbCrLf)
                                    .Append(GenerateProperty(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                    .Append(vbCrLf)
                                    AddAttributeTableForNode(node, attribIndex, attribNameTable)
                                Next
                            End If
                        End If
                    End If
                Next

                .Append(vbCrLf)
                .Append("#End Region")
                .Append(vbCrLf)

                If Not createSubNew Then
                    sClass = New StringBuilder
                    Exit Try
                End If

                BuildMethods(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                .Append(" End Class ")
                .Append(vbCrLf)

            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return sClass.ToString()
    End Function

    Public Function GenerateInnerClass(ByVal xml As XmlNode) As String Implements IGenerate.GenerateInnerClass
        Dim node As XmlNode
        Dim childNodes As XmlNodeList
        Dim nodeIndex As Integer
        Dim attribIndex As Integer
        Dim nodeCount As Integer
        Dim attribCount As Integer = 0
        Dim nameCollection As New List(Of String)
        Dim attribNameTable As New Dictionary(Of String, List(Of String))
        Dim createSubNew As Boolean = False
        Dim hasAttributes As Boolean = False
        Dim sClass As New StringBuilder
        Dim generateChildren As Boolean = False
        Try

            Dim attr As Integer = 0
            With sClass
                .Append(vbCrLf)
                If Settings.MakeSerializable Then
                    .Append("<Serializable()> ")
                End If
                .Append("Public Class ")
                .Append(PublicClassNameMatchSetter(xml.Name))
                .Append(vbCrLf)
                .Append("#Region "" Properties """)
                .Append(vbCrLf)

                If Settings.GenerateAttributes AndAlso Not xml.Attributes Is Nothing Then
                    createSubNew = True
                    attr = xml.Attributes.Count
                    If Not xml.Attributes Is Nothing Then
                        attr = xml.Attributes.Count
                        For attribIndex = 0 To attr - 1
                            'Generate Property Declaration
                            .Append(GenerateDeclaration(String.Concat(xml.Name, "_", xml.Attributes(attribIndex).Name)))
                            .Append(vbCrLf)
                            .Append(GenerateProperty(String.Concat(xml.Name, "_", xml.Attributes(attribIndex).Name)))
                            .Append(vbCrLf)
                            AddAttributeTableForNode(xml, attribIndex, attribNameTable)
                        Next
                    End If
                End If
                childNodes = xml.ChildNodes()
                nodeCount = childNodes.Count
                For nodeIndex = 0 To nodeCount - 1
                    node = childNodes(nodeIndex)

                    If node.NodeType = XmlNodeType.Element Then
                        createSubNew = True
                        generateChildren = False
                        hasAttributes = False
                        If node.HasChildNodes Then
                            CheckForChildren(node, generateChildren)
                        End If
                        If generateChildren Then
                            .Append(GeneratePublicDeclaration(node.Name))
                            .Append(vbCrLf)
                            _SubClasses.Add(GenerateInnerClass(node.OuterXml))
                            nameCollection.Add(String.Concat("%", node.Name, "Collection"))
                        Else
                            'Generate Property Declaration
                            .Append(GenerateDeclaration(node.Name))
                            .Append(vbCrLf)
                            .Append(GenerateProperty(node.Name))
                            nameCollection.Add(node.Name)
                            .Append(vbCrLf)
                            If Settings.GenerateAttributes AndAlso Not node.Attributes Is Nothing Then
                                attribCount = node.Attributes.Count
                            Else
                                attribCount = 0
                            End If

                            If attribCount > 0 Then
                                For attribIndex = 0 To attribCount - 1
                                    'Generate Property Declaration
                                    .Append(GenerateDeclaration(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                    .Append(vbCrLf)
                                    .Append(GenerateProperty(String.Concat(node.Name, "_", node.Attributes(attribIndex).Name)))
                                    .Append(vbCrLf)

                                    AddAttributeTableForNode(node, attribIndex, attribNameTable)
                                Next
                            End If
                        End If
                    End If
                Next

                .Append(vbCrLf)
                .Append("#End Region")
                .Append(vbCrLf)

                If Not createSubNew Then
                    sClass = New StringBuilder
                    Exit Try
                End If
                BuildMethods(sClass, xml.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xml.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)
                .Append(" End Class ")
                .Append(vbCrLf)

            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return sClass.ToString()
    End Function

#End Region

#Region "Supporting Methods"

    Private Sub AddAttributeTableForNode(ByVal node As XmlNode, ByVal attribIndex As Integer, ByVal attribNameTable As Dictionary(Of String, List(Of String)))
        If attribNameTable.ContainsKey(node.Name) Then
            attribNameTable(node.Name).Add(node.Attributes(attribIndex).Name)
        Else
            attribNameTable.Add(node.Name, New List(Of String))
            attribNameTable(node.Name).Add(node.Attributes(attribIndex).Name)
        End If
    End Sub

    Private Sub AddAttributeTableForXMLDoc(ByVal xmlDoc As XmlDocument, ByVal attribIndex As Integer, ByVal attribNameTable As Dictionary(Of String, List(Of String)))
        If attribNameTable.ContainsKey(xmlDoc.DocumentElement.Name) Then
            attribNameTable(xmlDoc.DocumentElement.Name).Add(xmlDoc.DocumentElement.Attributes(attribIndex).Name)
        Else
            attribNameTable.Add(xmlDoc.DocumentElement.Name, New List(Of String))
            attribNameTable(xmlDoc.DocumentElement.Name).Add(xmlDoc.DocumentElement.Attributes(attribIndex).Name)
        End If
    End Sub

    Private Sub BuildConstructors(ByVal sClass As StringBuilder, ByVal name As String, ByVal nameCollection As List(Of String), ByVal attribNameDictionary As Dictionary(Of String, List(Of String)))
        With sClass
            .Append("#Region ""Constructors""")
            .Append(vbCrLf)

            .Append(" Public Sub New()")
            If nameCollection.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameCollection.FindAll(AddressOf Find_Collection)
                .Append(vbCrLf)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("    {0} = New List(Of {1}){2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            Else
                .Append(vbCrLf)
            End If

            .Append(" End Sub ")
            .Append(vbCrLf)
            .Append(vbCrLf)

            .AppendFormat(" Public Sub New(ByVal passedXml As String){0}", vbCrLf)

            If nameCollection.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameCollection.FindAll(AddressOf Find_Collection)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("      {0} = New List(Of {1}){2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            End If

            .AppendFormat("    Using sr As StringReader = New StringReader(passedXml){0}", vbCrLf)
            .AppendFormat("        Dim reader As XmlReader = XmlReader.Create(sr){0}", vbCrLf)
            .AppendFormat("        While reader.Read{0}", vbCrLf)
            .AppendFormat("            With reader{0}", vbCrLf)
            .AppendFormat("            If .NodeType = XmlNodeType.Element Then{0}", vbCrLf)
            .AppendFormat("                Select Case .LocalName{0}", vbCrLf)

            If attribNameDictionary.ContainsKey(name) Then
                .AppendFormat("            Case ""{0}""{1}", name, vbCrLf)
                For index As Integer = 0 To attribNameDictionary(name).Count - 1
                    .AppendFormat("                         Me._{0}_{1} = .GetAttribute(""{1}""){2}", name, attribNameDictionary(name)(index), vbCrLf)
                Next
            End If

            For index As Integer = 0 To nameCollection.Count - 1

                If nameCollection(index).Trim.Contains("%") AndAlso nameCollection(index).Trim.Contains("Collection") Then
                    Dim ClassName As String() = nameCollection(index).Substring(1).Split("Collection")
                    .AppendFormat("                 Case ""{0}""{1}", ClassName(0), vbCrLf)
                    .AppendFormat("                         Me.{0}.Add(New {1}(.ReadSubtree)){2}", nameCollection(index).Substring(1), ClassName(0), vbCrLf)
                    .Append(vbCrLf)
                Else
                    .AppendFormat("                 Case ""{1}""{0}", vbCrLf, nameCollection(index).Trim)

                    If attribNameDictionary.ContainsKey(nameCollection(index).Trim) Then
                        For attrIndex As Integer = 0 To attribNameDictionary(nameCollection(index).Trim).Count - 1
                            .AppendFormat("                         Me._{0}_{1} = .GetAttribute(""{1}""){2}", nameCollection(index).Trim, attribNameDictionary(nameCollection(index).Trim)(attrIndex), vbCrLf)
                        Next
                    End If
                    .AppendFormat("                         Me._{1} = .ReadString{0}", vbCrLf, nameCollection(index).Trim)
                    .Append(vbCrLf)
                End If
            Next
            .AppendFormat("                End Select{0}", vbCrLf)
            .AppendFormat("            End If{0}", vbCrLf)
            .AppendFormat("            End With{0}", vbCrLf)
            .AppendFormat("        End While{0}", vbCrLf)
            .AppendFormat("    End Using{0}", vbCrLf)
            .AppendFormat(" End Sub{0}", vbCrLf)
            .Append(vbCrLf)

            .AppendFormat(" Public Sub New(ByVal reader As XmlReader){0}", vbCrLf)
            If nameCollection.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameCollection.FindAll(AddressOf Find_Collection)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("      {0} = New List(Of {1}){2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            End If
            .AppendFormat("      While reader.Read(){0}", vbCrLf)
            .AppendFormat("         With reader{0}", vbCrLf)
            .AppendFormat("            If .NodeType = XmlNodeType.Element Then{0}", vbCrLf)
            .AppendFormat("                Select Case .LocalName{0}", vbCrLf)

            If attribNameDictionary.ContainsKey(name) Then
                .AppendFormat("            Case ""{0}""{1}", name, vbCrLf)
                For attrIndex As Integer = 0 To attribNameDictionary(name).Count - 1
                    .AppendFormat("                         Me._{0}_{1} = .GetAttribute(""{1}""){2}", name, attribNameDictionary(name)(attrIndex), vbCrLf)
                Next
            End If

            For index As Integer = 0 To nameCollection.Count - 1
                If nameCollection(index).Trim.Contains("%") AndAlso nameCollection(index).Trim.Contains("Collection") Then
                    Dim x As String() = nameCollection(index).Substring(1).Split("Collection")
                    .AppendFormat("                 Case ""{0}""{1}", x(0), vbCrLf)
                    .AppendFormat("                         Me.{0}.Add(New {1}(.ReadSubtree())){2}", nameCollection(index).Substring(1), x(0), vbCrLf)
                    .Append(vbCrLf)
                Else
                    .AppendFormat("                 Case ""{1}""{0}", vbCrLf, nameCollection(index).Trim)
                    If attribNameDictionary.ContainsKey(nameCollection(index).Trim) Then
                        For attrIndex As Integer = 0 To attribNameDictionary(nameCollection(index).Trim).Count - 1
                            .AppendFormat("                         Me._{0}_{1} = .GetAttribute(""{1}""){2}", nameCollection(index).Trim, attribNameDictionary(nameCollection(index).Trim)(attrIndex), vbCrLf)
                        Next
                    End If
                    .AppendFormat("                         Me._{1} = .ReadString{0}", vbCrLf, nameCollection(index).Trim)
                    .Append(vbCrLf)
                End If
            Next
            .AppendFormat("                End Select{0}", vbCrLf)
            .AppendFormat("            End If{0}", vbCrLf)
            .AppendFormat("            End With{0}", vbCrLf)
            .AppendFormat("        End While{0}", vbCrLf)
            .AppendFormat(" End Sub{0}", vbCrLf)
            .Append(vbCrLf)

            .Append("#End Region")

        End With
    End Sub

    Private Sub BuildMethods(ByVal sClass As StringBuilder, ByVal name As String, ByVal nameCollection As List(Of String), ByVal attribNameTable As Dictionary(Of String, List(Of String)))
        With sClass
            .Append("#Region ""Methods""")
            .Append(vbCrLf)
            .Append(vbCrLf)
            .AppendFormat(" Public Function ToXML() As String{0}", vbCrLf)
            .AppendFormat("    Dim sbBuilder As New StringBuilder{0}", vbCrLf)
            .AppendFormat("    Using s As New StringWriter(sbBuilder), writer As New XmlTextWriter(s){0}", vbCrLf)
            .AppendFormat("        With writer{0}", vbCrLf)
            .AppendFormat("            .WriteStartElement(""{1}""){0}", vbCrLf, name)

            If attribNameTable.ContainsKey(name) Then
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("            If Not String.IsNullOrEmpty(Me.{0}_{1}) Then{2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("                  .WriteAttributeString(""{1}"", Me.{0}_{1}){2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("            End If{0}", vbCrLf)
                Next
            End If

            For index As Integer = 0 To nameCollection.Count - 1
                If nameCollection(index).Trim.Contains("%") AndAlso nameCollection(index).Trim.Contains("Collection") Then
                    .AppendFormat("            For index As Integer = 0 To Me.{0}.Count - 1{1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendFormat("                With {0}(index){1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendFormat("                    .WriteXML(writer){0}", vbCrLf)
                    .AppendFormat("                End With{0}", vbCrLf)
                    .AppendFormat("            Next{0}", vbCrLf)
                Else
                    If attribNameTable.ContainsKey(nameCollection(index).Trim) Then
                        .AppendFormat("            .WriteStartElement(""{1}""){0}", vbCrLf, nameCollection(index).Trim)
                        For attrIndex As Integer = 0 To attribNameTable(nameCollection(index).Trim).Count - 1
                            .AppendFormat("            If Not String.IsNullOrEmpty(Me.{0}_{1}) Then{2}", nameCollection(index).Trim, attribNameTable(nameCollection(index).Trim)(attrIndex), vbCrLf)
                            .AppendFormat("                  .WriteAttributeString(""{1}"", Me.{0}_{1}){2}", nameCollection(index).Trim, attribNameTable(nameCollection(index).Trim)(attrIndex), vbCrLf)
                            .AppendFormat("            End If{0}", vbCrLf)
                        Next
                        .AppendFormat("            .WriteString(Me.{0}){1}", nameCollection(index).Trim, vbCrLf)
                        .AppendFormat("            .WriteEndElement(){0}", vbCrLf)
                    Else
                        .AppendFormat("            .WriteElementString(""{1}"", Me.{1}){0}", vbCrLf, nameCollection(index).Trim)
                    End If
                End If

            Next
            .AppendFormat("            .WriteEndElement(){0}", vbCrLf)
            .AppendFormat("        End With{0}", vbCrLf)
            .AppendFormat("  End Using{0}", vbCrLf)
            .AppendFormat("  Return sbBuilder.ToString{0}", vbCrLf)
            .AppendFormat("End Function{0}", vbCrLf)
            .Append(vbCrLf)

            .AppendFormat(" Public Sub WriteXML(ByVal writer As XmlTextWriter){0}", vbCrLf)
            .AppendFormat("     With writer{0}", vbCrLf)
            .AppendFormat("         .WriteStartElement(""{1}""){0}", vbCrLf, name)

            If attribNameTable.ContainsKey(name) Then
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("            If Not String.IsNullOrEmpty(Me.{0}_{1}) Then{2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("                  .WriteAttributeString(""{1}"", Me.{0}_{1}){2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("            End If{0}", vbCrLf)
                Next
            End If
            For index As Integer = 0 To nameCollection.Count - 1

                If nameCollection(index).Trim.Contains("%") AndAlso nameCollection(index).Trim.Contains("Collection") Then
                    .AppendFormat("          For index As Integer = 0 To Me.{0}.Count - 1{1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendFormat("              With {0}(index){1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendFormat("                  .WriteXML(writer){0}", vbCrLf)
                    .AppendFormat("              End With{0}", vbCrLf)
                    .AppendFormat("          Next{0}", vbCrLf)
                Else

                    If attribNameTable.ContainsKey(nameCollection(index).Trim) Then
                        .AppendFormat("            .WriteStartElement(""{1}""){0}", vbCrLf, nameCollection(index).Trim)
                        For x As Integer = 0 To attribNameTable(nameCollection(index).Trim).Count - 1
                            .AppendFormat("            If Not String.IsNullOrEmpty(Me.{0}_{1}) Then{2}", nameCollection(index).Trim, attribNameTable(nameCollection(index).Trim)(x), vbCrLf)
                            .AppendFormat("                  .WriteAttributeString(""{1}"", Me.{0}_{1}){2}", nameCollection(index).Trim, attribNameTable(nameCollection(index).Trim)(x), vbCrLf)
                            .AppendFormat("            End If{0}", vbCrLf)
                        Next
                        .AppendFormat("            .WriteString(Me.{0}){1}", nameCollection(index).Trim, vbCrLf)
                        .AppendFormat("            .WriteEndElement(){0}", vbCrLf)
                    Else
                        .AppendFormat("            .WriteElementString(""{1}"", Me.{1}){0}", vbCrLf, nameCollection(index).Trim)
                    End If
                End If
            Next
            .AppendFormat("         .WriteEndElement(){0}", vbCrLf)
            .AppendFormat("     End With{0}", vbCrLf)
            .AppendFormat(" End Sub{0}", vbCrLf)
            .Append(vbCrLf)
            .AppendLine("")
            .AppendLine(" Public Overrides Function ToString() As String")
            .AppendLine("     Dim sb As New StringBuilder ")
            .AppendLine("      With sb ")
            .AppendFormat("        .AppendLine(""{0} node ""){1}", name, vbCrLf)

            If attribNameTable.ContainsKey(name) Then
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("        .AppendLine(String.Concat(""{0}_{1} "", Me.{0}_{1})){2}", name, attribNameTable(name)(index), vbCrLf)
                Next
            End If

            For index As Integer = 0 To nameCollection.Count - 1

                If nameCollection(index).Trim.Contains("%") AndAlso nameCollection(index).Trim.Contains("Collection") Then
                    .AppendFormat("        For index As Integer = 0 To Me.{0}.Count - 1{1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendFormat("             .AppendLine(Me.{0}(index).ToString()){1}", nameCollection(index).Substring(1), vbCrLf)
                    .AppendLine("          Next")
                Else

                    If attribNameTable.ContainsKey(nameCollection(index).Trim) Then
                        .AppendFormat("       .AppendLine(String.Concat(""{0} "", {0})){1}", nameCollection(index).Trim, vbCrLf)

                        For attrIndex As Integer = 0 To attribNameTable(nameCollection(index).Trim).Count - 1
                            .AppendFormat("       .AppendLine(String.Concat(""{0}_{1} "", Me.{0}_{1})){2}", nameCollection(index).Trim, attribNameTable(nameCollection(index).Trim)(attrIndex), vbCrLf)
                        Next
                    Else
                        .AppendFormat("       .AppendLine(String.Concat(""{0} "", {0})){1}", nameCollection(index).Trim, vbCrLf)
                    End If
                End If
            Next
            .AppendLine("       End With ")
            .AppendLine("    Return sb.ToString")
            .AppendLine(" End Function")
            .AppendLine("")
            .Append("#End Region")

        End With
    End Sub

    Private Sub CheckForChildren(ByVal node As XmlNode, ByRef generateChildren As Boolean)
        For index As Integer = 0 To node.ChildNodes.Count - 1
            If node.ChildNodes(index).NodeType = XmlNodeType.Element Then
                generateChildren = True
            End If
        Next
    End Sub

    Private Function Find_Collection(ByVal s As String) As Boolean
        Return s.Trim.Contains("%") AndAlso s.Trim.Contains("Collection")
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generates the declaration systax for the property member variables.
    ''' </summary>
    ''' <param name="propertyName">Prperty Name</param>
    ''' <returns></returns>
    ''' -----------------------------------------------------------------------------
    Private Function GenerateDeclaration(ByVal propertyName As String) As String
        Dim sDeclaration As New StringBuilder
        With sDeclaration
            .Append(" Private ")
            .Append(String.Concat("_", propertyName, " As String"))
        End With
        Return sDeclaration.ToString()
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Generates Property
    ''' </summary>
    ''' <param name="propertyName">Property Name to be generated</param>
    ''' <returns>Property definition.</returns>
    ''' -----------------------------------------------------------------------------
    Private Function GenerateProperty(ByVal propertyName As String) As String
        Dim sProperty As New StringBuilder
        With sProperty
            .Append(" Public Property ")
            .Append(propertyName)
            .Append("() As String")
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(" Get")
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(vbTab)
            .Append(" Return _" + propertyName)
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(" End Get")
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(" Set(ByVal Value As String) ")
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(vbTab)
            .Append(" _" + propertyName)
            .Append(" = Value")
            .Append(vbCrLf)
            .Append(vbTab)
            .Append(" End Set")
            .Append(vbCrLf)
            .Append(" End Property ")
            .Append(vbCrLf)
        End With
        ' sProperty.Append(vbCrLf)
        Return sProperty.ToString
    End Function

    Private Function GeneratePublicDeclaration(ByVal propertyName As String) As String
        Dim sDeclaration As New StringBuilder
        With sDeclaration
            .Append(vbCrLf)
            .Append(" Public ")
            Dim name As String = NameMatchSetter(propertyName)
            .Append(String.Concat(name, "Collection As  List(Of ", name, ")"))
            .Append(vbCrLf)
        End With
        Return sDeclaration.ToString()
    End Function

    Private Function MakeNameSafe(ByVal s As String) As String
        s = s.Replace(":", "_Colon_")
        s = s.Replace(".", "_Dot_")
        Return s
    End Function

    Private Function NameMatchSetter(ByVal name As String) As String
        If _NameCollection Is Nothing Then
            _NameCollection = New Dictionary(Of String, Integer)
        End If
        If _NameCollection.ContainsKey(name) Then
            _NameCollection.Item(name) += 1
            Return String.Concat(name, _NameCollection.Item(name).ToString)
        Else
            _NameCollection.Add(name, 0)
            Return name
        End If
    End Function

    Private Function PublicClassNameMatchSetter(ByVal name As String) As String
        If _PublicClassNameCollection Is Nothing Then
            _PublicClassNameCollection = New Dictionary(Of String, Integer)
        End If
        If _PublicClassNameCollection.ContainsKey(name) Then
            _PublicClassNameCollection.Item(name) += 1
            Return String.Concat(name, _PublicClassNameCollection.Item(name).ToString)
        Else
            _PublicClassNameCollection.Add(name, 0)
            Return name
        End If
    End Function

#End Region

End Class