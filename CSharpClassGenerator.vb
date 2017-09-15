Imports System.Collections.Generic
Imports System.Text
Imports System.Xml

Public Class CSharpClassGenerator
    Implements IGenerate

#Region "Fields"

    Private _Settings As Settings
    Private _NameCollection As Dictionary(Of String, Integer)
    Private _PublicClassNameCollection As Dictionary(Of String, Integer)
    Private _SubClasses As List(Of String)

#End Region

#Region "Constructors"

    Public Sub New(ByVal oSettings As Settings)
        _PublicClassNameCollection = New Dictionary(Of String, Integer)
        _NameCollection = New Dictionary(Of String, Integer)
        _Settings = oSettings
    End Sub

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

#Region "Methods"

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
        Dim nameList As New List(Of String)
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
                '.AppendLine("using System.Collections.Generic;")
                '.AppendLine("using System.IO;")
                '.AppendLine("using System.Text;")
                '.AppendLine("using System.Xml;")
                '.AppendLine("")
                If Settings.MakeSerializable Then
                    .AppendLine("[Serializable()] ")
                End If
                .Append("public class ")
                .Append(String.Concat(PublicClassNameMatchSetter(xmlDoc.DocumentElement.Name), " {"))
                .Append(vbCrLf)
                .Append("#region "" Properties """)
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
                        nameList.Add(String.Concat("%", node.Name, "Collection"))
                    Else
                        'Generate Property Declaration
                        .Append(GenerateDeclaration(node.Name))
                        .Append(vbCrLf)
                        .Append(GenerateProperty(node.Name))
                        nameList.Add(node.Name)
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
                .Append("#endregion")
                .Append(vbCrLf)

                BuildMethods(sClass, xmlDoc.DocumentElement.Name, nameList, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xmlDoc.DocumentElement.Name, nameList, attribNameTable)

                .Append(vbCrLf)

                .Append("} ")
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
                    .AppendLine("[Serializable()] ")
                End If
                .Append("public class ")
                .Append(String.Concat(PublicClassNameMatchSetter(xmlDoc.DocumentElement.Name), " {"))
                .Append(vbCrLf)
                .Append("#region "" Properties """)
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
                .Append("#endregion")
                .Append(vbCrLf)

                If Not createSubNew Then
                    sClass = New StringBuilder
                    Exit Try
                End If

                BuildMethods(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xmlDoc.DocumentElement.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                .Append("} ")
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
                    .AppendLine("[Serializable()] ")
                End If
                .Append("public class ")
                .Append(String.Concat(PublicClassNameMatchSetter(xml.Name), " {"))
                .Append(vbCrLf)
                .Append("#region "" Properties """)
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
                .Append("#endregion")
                .Append(vbCrLf)

                If Not createSubNew Then
                    sClass = New StringBuilder
                    Exit Try
                End If
                BuildMethods(sClass, xml.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)

                BuildConstructors(sClass, xml.Name, nameCollection, attribNameTable)

                .Append(vbCrLf)
                .Append("} ")
                .Append(vbCrLf)

            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return sClass.ToString()
    End Function

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

    Private Sub BuildConstructors(ByVal sClass As StringBuilder, ByVal name As String, ByVal nameList As List(Of String), ByVal attribNameTable As Dictionary(Of String, List(Of String)))

        With sClass
            .Append("#region ""Constructors""")
            .Append(vbCrLf)

            .AppendFormat(" public {0}()", name)
            .Append("{ ")
            If nameList.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameList.FindAll(AddressOf Find_Collection)
                .Append(vbCrLf)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("    {0} = new List<{1}>();{2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            Else
                .Append(vbCrLf)
            End If

            .Append("} ")
            .Append(vbCrLf)
            .Append(vbCrLf)

            .AppendFormat(" public {0}(string passedXML){1}", name, vbCrLf)
            .Append("{ ")
            If nameList.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameList.FindAll(AddressOf Find_Collection)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("      {0} = new List<{1}>();{2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            End If

            .AppendFormat("    using (StringReader sr = new StringReader(passedXML)) {0}{1}", "{", vbCrLf)
            .AppendFormat("         XmlReader reader = XmlReader.Create(sr);{0}", vbCrLf)
            .AppendFormat("         while (reader.Read()) {0}{1}", "{", vbCrLf)
            .AppendFormat("             {0}{1}", "{", vbCrLf)
            .AppendFormat("            if (reader.NodeType == XmlNodeType.Element) {0}{1}", "{", vbCrLf)
            .AppendFormat("                switch (reader.LocalName) {0}{1}", "{", vbCrLf)

            If attribNameTable.ContainsKey(name) Then
                .AppendFormat("            case ""{0}"":{1}", name, vbCrLf)
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("                         this._{0}_{1} = reader.GetAttribute(""{1}"");{2}", name, attribNameTable(name)(index), vbCrLf)
                Next
                .AppendFormat("                         break;{0}", vbCrLf)
            End If

            For index As Integer = 0 To nameList.Count - 1

                If nameList(index).Trim.Contains("%") AndAlso nameList(index).Trim.Contains("Collection") Then
                    Dim className As String() = nameList(index).Substring(1).Split("Collection")
                    .AppendFormat("                 case ""{0}"":{1}", className(0), vbCrLf)
                    .AppendFormat("                         this.{0}.Add(new {1}(reader.ReadSubtree()));{2}", nameList(index).Substring(1), className(0), vbCrLf)
                    .AppendFormat("                         break;{0}", vbCrLf)
                    .Append(vbCrLf)
                Else
                    .AppendFormat("                 case ""{1}"":{0}", vbCrLf, nameList(index).Trim)

                    If attribNameTable.ContainsKey(nameList(index).Trim) Then
                        For x As Integer = 0 To attribNameTable(nameList(index).Trim).Count - 1
                            .AppendFormat("                         this._{0}_{1} = reader.GetAttribute(""{1}"");{2}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), vbCrLf)
                        Next
                    End If
                    .AppendFormat("                         this._{1} = reader.ReadString();{0}", vbCrLf, nameList(index).Trim)
                    .AppendFormat("                         break;{0}", vbCrLf)
                    .Append(vbCrLf)
                End If
            Next
            .AppendFormat("                     {0}{1}", "}", vbCrLf)
            .AppendFormat("                 {0}{1}", "}", vbCrLf)
            .AppendFormat("             {0}{1}", "}", vbCrLf)
            .AppendFormat("         {0}{1}", "}", vbCrLf)
            .AppendFormat("     {0}{1}", "}", vbCrLf)
            .AppendFormat(" {0}{1}", "}", vbCrLf)

            .AppendFormat(" public {0}(XmlReader reader){1}", name, vbCrLf)
            .AppendFormat("{0}{1}", "{", vbCrLf)
            If nameList.FindIndex(AddressOf Find_Collection) <> -1 Then
                Dim tempList As List(Of String) = nameList.FindAll(AddressOf Find_Collection)
                For index As Integer = 0 To tempList.Count - 1
                    Dim x As String() = tempList(index).Substring(1).Split("Collection")
                    .AppendFormat("      {0} = new List<{1}>();{2}", tempList(index).Substring(1), x(0), vbCrLf)
                Next
            End If

            .AppendFormat("      while (reader.Read()) {0}{1}", "{", vbCrLf)
            .AppendFormat("          {0}{1}", "{", vbCrLf)
            .AppendFormat("            if (reader.NodeType == XmlNodeType.Element) {0}{1}", "{", vbCrLf)
            .AppendFormat("                switch (reader.LocalName) {0}{1}", "{", vbCrLf)

            If attribNameTable.ContainsKey(name) Then
                .AppendFormat("            case ""{0}"":{1}", name, vbCrLf)
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("                         this._{0}_{1} = reader.GetAttribute(""{1}"");{2}", name, attribNameTable(name)(index), vbCrLf)
                Next
                .AppendFormat("                         break;{0}", vbCrLf)
            End If

            For index As Integer = 0 To nameList.Count - 1
                If nameList(index).Trim.Contains("%") AndAlso nameList(index).Trim.Contains("Collection") Then
                    Dim x As String() = nameList(index).Substring(1).Split("Collection")
                    .AppendFormat("                 case ""{0}"":{1}", x(0), vbCrLf)
                    .AppendFormat("                         this.{0}.Add(new {1}(reader.ReadSubtree()));{2}", nameList(index).Substring(1), x(0), vbCrLf)
                    .AppendFormat("                         break;{0}", vbCrLf)
                    .Append(vbCrLf)
                Else
                    .AppendFormat("                 case ""{0}"":{1}", nameList(index).Trim, vbCrLf)
                    If attribNameTable.ContainsKey(nameList(index).Trim) Then
                        For x As Integer = 0 To attribNameTable(nameList(index).Trim).Count - 1
                            .AppendFormat("                         this._{0}_{1} = reader.GetAttribute(""{1}"");{2}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), vbCrLf)
                        Next
                    End If
                    .AppendFormat("                         this._{0} = reader.ReadString();{1}", nameList(index).Trim, vbCrLf)
                    .AppendFormat("                         break;{0}", vbCrLf)
                    .Append(vbCrLf)
                End If
            Next
            .AppendFormat("                 {0}{1}", "}", vbCrLf)
            .AppendFormat("             {0}{1}", "}", vbCrLf)
            .AppendFormat("         {0}{1}", "}", vbCrLf)
            .AppendFormat("     {0}{1}", "}", vbCrLf)
            .AppendFormat(" {0}{1}", "}", vbCrLf)
            .Append(vbCrLf)

            .Append("#endregion")

        End With
    End Sub

    Private Sub BuildMethods(ByVal sClass As StringBuilder, ByVal name As String, ByVal nameList As List(Of String), ByVal attribNameTable As Dictionary(Of String, List(Of String)))
        With sClass

            .Append("#region ""Methods""")
            .Append(vbCrLf)
            .Append(vbCrLf)
            .AppendFormat(" public string ToXML(){0}", vbCrLf)
            .AppendFormat(" {0}{1}", "{", vbCrLf)
            .AppendFormat("    StringBuilder sbBuilder = new StringBuilder();{0}", vbCrLf)
            .AppendFormat("    using (StringWriter s = new StringWriter(sbBuilder)) {0}{1}", "{", vbCrLf)
            .AppendFormat("       using (XmlTextWriter writer = new XmlTextWriter(s)) {0}{1}", "{", vbCrLf)
            .AppendFormat("           {0} {1}", "{", vbCrLf)
            .AppendFormat("            writer.WriteStartElement(""{1}"");{0}", vbCrLf, name)

            If attribNameTable.ContainsKey(name) Then
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("            if (!string.IsNullOrEmpty(this.{0}_{1})) {2}{3}", name, attribNameTable(name)(index), "{", vbCrLf)
                    .AppendFormat("                  writer.WriteAttributeString(""{1}"", this.{0}_{1});{2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("            {0}{1}", "}", vbCrLf)
                Next
            End If

            For index As Integer = 0 To nameList.Count - 1
                If nameList(index).Trim.Contains("%") AndAlso nameList(index).Trim.Contains("Collection") Then
                    .AppendFormat("            for (int i = 0; i <= this.{0}.Count - 1; i++) {1}{2}", nameList(index).Substring(1), "{", vbCrLf)
                    .AppendFormat("                {0}{1}", "{", vbCrLf)
                    .AppendFormat("                    {0}[i].WriteXML(writer);{1}", nameList(index).Substring(1), vbCrLf)
                    .AppendFormat("                {0}{1}", "}", vbCrLf)
                    .AppendFormat("            {0}{1}", "}", vbCrLf)
                Else
                    If attribNameTable.ContainsKey(nameList(index).Trim) Then
                        .AppendFormat("            writer.WriteStartElement(""{1}"");{0}", vbCrLf, nameList(index).Trim)
                        For x As Integer = 0 To attribNameTable(nameList(index).Trim).Count - 1
                            .AppendFormat("            if (!string.IsNullOrEmpty(this.{0}_{1})) {2}{3}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), "{", vbCrLf)
                            .AppendFormat("                  writer.WriteAttributeString(""{1}"", this.{0}_{1});{2}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), vbCrLf)
                            .AppendFormat("            {0}{1}", "}", vbCrLf)
                        Next
                        .AppendFormat("            writer.WriteString(this.{0});{1}", nameList(index).Trim, vbCrLf)
                        .AppendFormat("            writer.WriteEndElement();{0}", vbCrLf)
                    Else
                        .AppendFormat("            writer.WriteElementString(""{1}"", this.{1});{0}", vbCrLf, nameList(index).Trim)
                    End If
                End If

            Next
            .AppendFormat("            writer.WriteEndElement();{0}", vbCrLf)
            .AppendFormat("        {0}{1}", "}", vbCrLf)
            .AppendFormat("     {0}{1}", "}", vbCrLf)
            .AppendFormat("    return sbBuilder.ToString();{0}", vbCrLf)
            .AppendFormat("  {0}{1}", "}", vbCrLf)
            .AppendFormat("{0}{1}", "}", vbCrLf)
            .Append(vbCrLf)

            .AppendFormat(" public void WriteXML(XmlTextWriter writer){0}", vbCrLf)
            .AppendFormat("  {0}{1}", "{", vbCrLf)
            .AppendFormat("     {0}{1}", "{", vbCrLf)
            .AppendFormat("         writer.WriteStartElement(""{0}"");{1}", name, vbCrLf)

            If attribNameTable.ContainsKey(name) Then
                For index As Integer = 0 To attribNameTable(name).Count - 1
                    .AppendFormat("            if (!string.IsNullOrEmpty(this.{0}_{1})) {2}{3}", name, attribNameTable(name)(index), "{", vbCrLf)
                    .AppendFormat("                writer.WriteAttributeString(""{1}"", this.{0}_{1});{2}", name, attribNameTable(name)(index), vbCrLf)
                    .AppendFormat("            {0}{1}", "}", vbCrLf)
                Next
            End If
            For index As Integer = 0 To nameList.Count - 1

                If nameList(index).Trim.Contains("%") AndAlso nameList(index).Trim.Contains("Collection") Then
                    .AppendFormat("          for (int i = 0; i <= this.{0}.Count - 1; i++) {1}{2}", nameList(index).Substring(1), "{", vbCrLf)
                    .AppendFormat("              {0}{1}", "{", vbCrLf)
                    .AppendFormat("                  {0}[i].WriteXML(writer);{1}", nameList(index).Substring(1), vbCrLf)
                    .AppendFormat("              {0}{1}", "}", vbCrLf)
                    .AppendFormat("          {0}{1}", "}", vbCrLf)
                Else

                    If attribNameTable.ContainsKey(nameList(index).Trim) Then
                        .AppendFormat("            writer.WriteStartElement(""{1}"");{0}", vbCrLf, nameList(index).Trim)
                        For x As Integer = 0 To attribNameTable(nameList(index).Trim).Count - 1
                            .AppendFormat("            if (!string.IsNullOrEmpty(this.{0}_{1})) {2}{3}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), "{", vbCrLf)
                            .AppendFormat("                  writer.WriteAttributeString(""{1}"", this.{0}_{1});{2}", nameList(index).Trim, attribNameTable(nameList(index).Trim)(x), vbCrLf)
                            .AppendFormat("            {0}{1}", "}", vbCrLf)
                        Next
                        .AppendFormat("            writer.WriteString(this.{0});{1}", nameList(index).Trim, vbCrLf)
                        .AppendFormat("            writer.WriteEndElement();{0}", vbCrLf)
                    Else
                        .AppendFormat("            writer.WriteElementString(""{0}"", this.{0});{1}", nameList(index).Trim, vbCrLf)
                    End If
                End If
            Next
            .AppendFormat("         writer.WriteEndElement();{0}", vbCrLf)
            .AppendFormat("     {0}{1}", "}", vbCrLf)
            .AppendFormat(" {0}{1}", "}", vbCrLf)
            .Append(vbCrLf)
            .Append("#endregion")

        End With
    End Sub

    Private Sub CheckForChildren(ByVal node As XmlNode, ByRef generateChildren As Boolean)
        For i As Integer = 0 To node.ChildNodes.Count - 1
            If node.ChildNodes(i).NodeType = XmlNodeType.Element Then
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
            '    private string _ID;
            .AppendFormat(" private string _{0};{1}", propertyName, vbCrLf)
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
            .AppendFormat(" public string {0} {1}{2}", propertyName, "{", vbCrLf)
            .AppendFormat("{0} get {1} return _{2}; {3}{4}", vbTab, "{", propertyName, "}", vbCrLf)
            .AppendFormat("{0} set {1} _{2} = value; {3}{4}", vbTab, "{", propertyName, "}", vbCrLf)
            .AppendFormat(" {0} {1}", "}", vbCrLf)
        End With

        Return sProperty.ToString
    End Function

    Private Function GeneratePublicDeclaration(ByVal propertyName As String) As String
        Dim sDeclaration As New StringBuilder
        '    public List<LIRS> LIRS_Parent;
        Dim Name As String = NameMatchSetter(propertyName)
        With sDeclaration
            .AppendFormat("  public List<{0}> {0}Collection ;{1}", Name, vbCrLf)
        End With
        Return sDeclaration.ToString()
    End Function

    Private Function MakeNameSafe(ByVal s As String) As String
        s = s.Replace(":", "_Colon_")
        s = s.Replace(".", "_Dot_")
        Return s
    End Function

    Private Function NameMatchSetter(ByVal Name As String) As String
        If _NameCollection Is Nothing Then
            _NameCollection = New Dictionary(Of String, Integer)
        End If
        If _NameCollection.ContainsKey(Name) Then
            _NameCollection.Item(Name) += 1
            Return String.Concat(Name, _NameCollection.Item(Name).ToString)
        Else
            _NameCollection.Add(Name, 0)
            Return Name
        End If
    End Function

    Private Function PublicClassNameMatchSetter(ByVal Name As String) As String
        If _PublicClassNameCollection Is Nothing Then
            _PublicClassNameCollection = New Dictionary(Of String, Integer)
        End If
        If _PublicClassNameCollection.ContainsKey(Name) Then
            _PublicClassNameCollection.Item(Name) += 1
            Return String.Concat(Name, _PublicClassNameCollection.Item(Name).ToString)
        Else
            _PublicClassNameCollection.Add(Name, 0)
            Return Name
        End If
    End Function

#End Region

End Class