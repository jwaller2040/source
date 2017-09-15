Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml

''' -----------------------------------------------------------------------------
''' Project	 : ClassGenerator
''' Class	 : ClassGenerator
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Generates the vb.net class from single or multi rooted xml.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	Srinivas Miriyala	6/29/2006	Created
'''     James Waller        3/20/2009   multi level
'''     James Waller        7/23/2010   
''' </history>
''' -----------------------------------------------------------------------------
Public Class frmClassGenerator
    Inherits System.Windows.Forms.Form

#Region "Declarations"

    Friend WithEvents ClassGenMainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents dlgopnflXML As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgsvflClass As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuItemExit As System.Windows.Forms.MenuItem

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents mnuItemFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuItemGenerate As System.Windows.Forms.MenuItem
    Friend WithEvents mnuItemOpen As System.Windows.Forms.MenuItem
    Friend WithEvents mnuItemOptions As System.Windows.Forms.MenuItem
    Friend WithEvents mnuItemSave As System.Windows.Forms.MenuItem
    Friend WithEvents pnlClass As System.Windows.Forms.Panel
    Friend WithEvents rtbClass As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbXML As System.Windows.Forms.RichTextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter

    Private Const CannotRunNArrangeError As String = "Cannot run NArrange to generate results because required application narrange-console.exe is not present in folder C:\Program Files\NArrange 0.2.9. Please fix this."
    Private Const CSharpTempFile As String = "C:\temp\tmp.cs"
    Private Const NArrange_Batch_File_Location As String = "C:\temp\nArrange.bat"
    Private Const NArrange_Location As String = "C:\Program Files (x86)\NArrange 0.2.9\narrange-console.exe"
    Private Const NArrange_Path As String = "C:\Program Files (x86)\NArrange 0.2.9"
    Private Const NoRegionsConfig As String = "C:\Program Files (x86)\NArrange 0.2.9\MyConfig.xml"
    Private Const UseRegionsConfig As String = "C:\Program Files (x86)\NArrange 0.2.9\DefaultConfig.xml"
    Private Const VBTempFile As String = "C:\temp\tmp.vb"

    Dim classgen As IGenerate

    'Required by the Windows Form Designer
    Private _Components As System.ComponentModel.IContainer
    Private _GlobalSettings As Settings
    Private components As System.ComponentModel.IContainer
    Private _Sb As StringBuilder

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

#End Region

#Region "Supporting Methods"

    Private Sub BuildBatchFile(ByVal classText As String)
        If _GlobalSettings.UseCSharp Then
            If File.Exists(CSharpTempFile) Then
                File.Delete(CSharpTempFile)
            End If
            Dim writer As System.IO.TextWriter = New System.IO.StreamWriter(CSharpTempFile)
            writer.Write(classText)
            writer.Close()
        Else
            If File.Exists(VBTempFile) Then
                File.Delete(VBTempFile)
            End If
            Dim writer As System.IO.TextWriter = New System.IO.StreamWriter(VBTempFile)
            writer.Write(classText)
            writer.Close()
        End If

        If _GlobalSettings.UseNArrange Then
            If Not File.Exists(NArrange_Location) Then
                Throw New Exception(CannotRunNArrangeError)
            End If

            _Sb = New StringBuilder

            If File.Exists(NArrange_Batch_File_Location) Then
                File.Delete(NArrange_Batch_File_Location)
            End If

            Dim NArrangefile As TextWriter
            NArrangefile = File.CreateText(NArrange_Batch_File_Location)

            With NArrangefile
                .WriteLine("echo off")
                .WriteLine(String.Format("set SERVICE_HOME={0}", NArrange_Path))
                .WriteLine("cd %SERVICE_HOME%")

                If _GlobalSettings.UseCSharp Then
                    .WriteLine(String.Format("narrange-console ""{0}"" /c:""{1}""  /b", CSharpTempFile, UseRegionsConfig))
                Else
                    .WriteLine(String.Format("narrange-console ""{0}"" /c:""{1}""  /b", VBTempFile, UseRegionsConfig))
                End If

                .Flush()
                .Close()
            End With
        End If


    End Sub

    Private Sub ClassGenerator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialize()
        Dim xmlString As String
        xmlString = String.Concat("<Employee>", vbCrLf, vbTab, "<ID>1</ID>", vbCrLf, vbTab, "<Name first=""Srinivas"" Last=""Miriyala"" Middle=""Rao"">Srinivas Miriyala</Name>", vbCrLf, "</Employee>")
        rtbXML.Text = xmlString
    End Sub

    Private Sub CreateFiles(ByVal FileCollection As Dictionary(Of String, StringBuilder))
        For Each f As String In FileCollection.Keys
            Dim writer As System.IO.TextWriter = New System.IO.StreamWriter(f)
            writer.Write(FileCollection(f).ToString)
            writer.Close()
        Next
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (_Components Is Nothing) Then
                _Components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub Initialize()
        _GlobalSettings = New Settings
        classgen = New VBClassGenerator(_GlobalSettings)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClassGenerator))
        Me.ClassGenMainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuItemFile = New System.Windows.Forms.MenuItem()
        Me.mnuItemOpen = New System.Windows.Forms.MenuItem()
        Me.mnuItemSave = New System.Windows.Forms.MenuItem()
        Me.mnuItemGenerate = New System.Windows.Forms.MenuItem()
        Me.mnuItemOptions = New System.Windows.Forms.MenuItem()
        Me.mnuItemExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.dlgopnflXML = New System.Windows.Forms.OpenFileDialog()
        Me.dlgsvflClass = New System.Windows.Forms.SaveFileDialog()
        Me.rtbXML = New System.Windows.Forms.RichTextBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlClass = New System.Windows.Forms.Panel()
        Me.rtbClass = New System.Windows.Forms.RichTextBox()
        Me.pnlClass.SuspendLayout()
        Me.SuspendLayout()
        '
        'ClassGenMainMenu
        '
        Me.ClassGenMainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuItemFile})
        '
        'mnuItemFile
        '
        Me.mnuItemFile.Index = 0
        Me.mnuItemFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuItemOpen, Me.mnuItemSave, Me.mnuItemGenerate, Me.mnuItemOptions, Me.mnuItemExit, Me.MenuItem1})
        Me.mnuItemFile.Text = "File"
        '
        'mnuItemOpen
        '
        Me.mnuItemOpen.Index = 0
        Me.mnuItemOpen.Text = "&Open XML"
        '
        'mnuItemSave
        '
        Me.mnuItemSave.Index = 1
        Me.mnuItemSave.Text = "&Save Class As"
        '
        'mnuItemGenerate
        '
        Me.mnuItemGenerate.Index = 2
        Me.mnuItemGenerate.Text = "&Generate Class"
        '
        'mnuItemOptions
        '
        Me.mnuItemOptions.Index = 3
        Me.mnuItemOptions.Text = "O&ptions"
        '
        'mnuItemExit
        '
        Me.mnuItemExit.Index = 4
        Me.mnuItemExit.Text = "E&xit"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 5
        Me.MenuItem1.Text = "Save Multiple Files"
        '
        'dlgopnflXML
        '
        Me.dlgopnflXML.Filter = "XML Files|*.xml"
        Me.dlgopnflXML.Title = "Open Source XML File"
        '
        'dlgsvflClass
        '
        Me.dlgsvflClass.Filter = "VB Files|*.vb|C# Files|*.cs|Text Files|*.txt|All Files|*.*"
        Me.dlgsvflClass.Title = "Save Class File"
        '
        'rtbXML
        '
        Me.rtbXML.AcceptsTab = True
        Me.rtbXML.Dock = System.Windows.Forms.DockStyle.Left
        Me.rtbXML.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbXML.Location = New System.Drawing.Point(0, 0)
        Me.rtbXML.Name = "rtbXML"
        Me.rtbXML.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.rtbXML.Size = New System.Drawing.Size(408, 525)
        Me.rtbXML.TabIndex = 4
        Me.rtbXML.Text = ""
        Me.rtbXML.WordWrap = False
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(408, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 525)
        Me.Splitter1.TabIndex = 5
        Me.Splitter1.TabStop = False
        '
        'pnlClass
        '
        Me.pnlClass.Controls.Add(Me.rtbClass)
        Me.pnlClass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClass.Location = New System.Drawing.Point(411, 0)
        Me.pnlClass.Name = "pnlClass"
        Me.pnlClass.Size = New System.Drawing.Size(373, 525)
        Me.pnlClass.TabIndex = 6
        '
        'rtbClass
        '
        Me.rtbClass.AcceptsTab = True
        Me.rtbClass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbClass.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbClass.Location = New System.Drawing.Point(0, 0)
        Me.rtbClass.Name = "rtbClass"
        Me.rtbClass.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.rtbClass.Size = New System.Drawing.Size(373, 525)
        Me.rtbClass.TabIndex = 3
        Me.rtbClass.Text = ""
        Me.rtbClass.WordWrap = False
        '
        'frmClassGenerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.ForestGreen
        Me.ClientSize = New System.Drawing.Size(784, 525)
        Me.Controls.Add(Me.pnlClass)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.rtbXML)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.ClassGenMainMenu
        Me.Name = "frmClassGenerator"
        Me.Text = "Class Generator"
        Me.pnlClass.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Dim sb As New StringBuilder
        If _GlobalSettings.UseCSharp Then
            Me.dlgsvflClass.Filter = "C# Files|*.cs|Text Files|*.txt|All Files|*.*"
        Else
            Me.dlgsvflClass.Filter = "VB Files|*.vb|Text Files|*.txt|All Files|*.*"
        End If
        Dim result As DialogResult = dlgsvflClass.ShowDialog

        If result <> DialogResult.Cancel Then
            Dim test As String = dlgsvflClass.FileName
            Dim s As String() = rtbClass.Text.Split(vbLf)

            Dim sC As New Specialized.StringCollection

            If s.Length > 0 Then
                Dim FileCollection As New Dictionary(Of String, StringBuilder)
                Dim CurrentFile As String = String.Empty

                For i As Integer = 0 To s.Length - 1
                    If _GlobalSettings.UseCSharp Then
                        If s(i).Contains("public class") Then
                            sb = New StringBuilder
                            With sb
                                .AppendFormat("using System;{0}", vbCrLf)
                                .AppendFormat("using System.IO;{0}", vbCrLf)
                                .AppendFormat("using System.Text;{0}", vbCrLf)
                                .AppendFormat("using System.Xml;{0}", vbCrLf)
                                .AppendFormat("using System.Collections.Generic;{0}", vbCrLf)
                                .Append(vbCrLf)
                            End With
                            Dim ClassName As String = Regex.Match(s(i), "public class (\w+)", RegexOptions.Singleline).Groups(1).Value
                            CurrentFile = Regex.Replace(dlgsvflClass.FileName, "(\w+)\.cs", String.Format("{0}.cs", ClassName), RegexOptions.Singleline)
                            FileCollection.Add(CurrentFile, New StringBuilder(String.Concat(sb.ToString, s(i), vbCrLf)))
                        Else
                            FileCollection(CurrentFile).AppendLine(s(i))
                        End If
                    Else
                        If s(i).Contains("Public Class") Then
                            sb = New StringBuilder
                            With sb
                                .AppendFormat("Imports System{0}", vbCrLf)
                                .AppendFormat("Imports System.IO{0}", vbCrLf)
                                .AppendFormat("Imports System.Text{0}", vbCrLf)
                                .AppendFormat("Imports System.Xml{0}", vbCrLf)
                                .AppendFormat("Imports System.Collections.Generic{0}", vbCrLf)
                                .Append(vbCrLf)
                            End With
                            Dim ClassName As String = Regex.Match(s(i), "Public Class (\w+)", RegexOptions.Singleline).Groups(1).Value
                            CurrentFile = Regex.Replace(dlgsvflClass.FileName, "(\w+)\.vb", String.Format("{0}.vb", ClassName), RegexOptions.Singleline)
                            FileCollection.Add(CurrentFile, New StringBuilder(String.Concat(sb.ToString, s(i), vbCrLf)))
                        Else
                            FileCollection(CurrentFile).AppendLine(s(i))
                        End If
                    End If

                Next
                CreateFiles(FileCollection)
            End If

        End If
    End Sub

    Private Sub mnuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemExit.Click
        Application.Exit()
    End Sub

    Private Sub mnuItemGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemGenerate.Click
        rtbClass.Clear()

        If _GlobalSettings.UseCSharp Then
            classgen = New CSharpClassGenerator(_GlobalSettings)
        Else
            classgen = New VBClassGenerator(_GlobalSettings)
        End If
        classgen.Settings = _GlobalSettings

        Dim sClassText As String = classgen.GenerateClass(rtbXML.Text)

        If Not String.IsNullOrEmpty(sClassText) AndAlso classgen.Settings.UseNArrange Then
            ScrubDataWithNArrange(sClassText)
        End If

        Parse(sClassText)

        'Dim codeColorizer As New ColorCode.CodeColorizer

        'If _GlobalSettings.UseCSharp Then
        '    rtbClass.Text = codeColorizer.Colorize(sClassText, ColorCode.Languages.CSharp)
        'Else
        '    rtbClass.Text = codeColorizer.Colorize(sClassText, ColorCode.Languages.VbDotNet)
        'End If

    End Sub

    Private Sub mnuItemOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemOpen.Click
        Dim result As DialogResult = dlgopnflXML.ShowDialog()
        If Not result = DialogResult.Cancel Then
            rtbXML.Clear()
            Dim reader As System.IO.TextReader = New System.IO.StreamReader(System.IO.File.Open(dlgopnflXML.FileName, FileMode.Open))
            rtbXML.Clear()
            rtbXML.Text = reader.ReadToEnd()
        End If
    End Sub

    Private Sub mnuItemOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemOptions.Click
        Dim frmOptions As New Options
        frmOptions.Settings = _GlobalSettings
        frmOptions.ShowDialog(Me)
        _GlobalSettings = frmOptions.Settings
    End Sub

    Private Sub mnuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemSave.Click
        Dim sb As New StringBuilder
        If _GlobalSettings.UseCSharp Then
            Me.dlgsvflClass.Filter = "C# Files|*.cs|Text Files|*.txt|All Files|*.*"
        Else
            Me.dlgsvflClass.Filter = "VB Files|*.vb|Text Files|*.txt|All Files|*.*"
        End If
        Dim result As DialogResult = dlgsvflClass.ShowDialog
        If Not result = DialogResult.Cancel Then
            Dim writer As System.IO.TextWriter = New System.IO.StreamWriter(dlgsvflClass.FileName)
            With sb
                If _GlobalSettings.UseCSharp Then
                    .AppendFormat("using System;{0}", vbCrLf)
                    .AppendFormat("using System.IO;{0}", vbCrLf)
                    .AppendFormat("using System.Text;{0}", vbCrLf)
                    .AppendFormat("using System.Xml;{0}", vbCrLf)
                    .AppendFormat("using System.Collections.Generic;{0}", vbCrLf)

                Else
                    .AppendFormat("Imports System{0}", vbCrLf)
                    .AppendFormat("Imports System.IO{0}", vbCrLf)
                    .AppendFormat("Imports System.Text{0}", vbCrLf)
                    .AppendFormat("Imports System.Xml{0}", vbCrLf)
                    .AppendFormat("Imports System.Collections.Generic{0}", vbCrLf)

                End If

                .Append(vbCrLf)
            End With

            writer.Write(String.Concat(sb.ToString, rtbClass.Text))
            writer.Close()
        End If
    End Sub

    Private Sub Parse(ByVal text As String)
        ' Foreach line in input,
        ' identify key words and format them when adding to the rich text box.
        Dim r As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(vbCrLf)
        Dim lines() As String = r.Split(text)
        For Each l As String In lines
            ParseLine(l)
        Next
    End Sub

    Private Sub ParseLine(ByVal line As String)
        Dim r As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("( )")
        Dim tokens() As String = r.Split(line)
        For Each token As String In tokens
            ' Set the token's default color and font.
            rtbClass.SelectionColor = Color.Black
            rtbClass.SelectionFont = New Font("Courier New", 10, FontStyle.Regular)
            ' Check for a comment.
            If ((token = "'") _
                OrElse token.StartsWith("'")) Then
                ' Find the start of the comment and then extract the whole comment.
                Dim index As Integer = line.IndexOf("'")
                Dim comment As String = line.Substring(index, (line.Length - index))
                rtbClass.SelectionColor = Color.LightGreen
                rtbClass.SelectionFont = New Font("Courier New", 10, FontStyle.Regular)
                rtbClass.SelectedText = comment
            End If

            If _GlobalSettings.UseCSharp Then
                Dim sharpWords() As String = {"#endregion", "#region", "abstract", "as", "ascending", "base", "bool", "break", "by", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "descending", "do", "double", "dynamic", "else", "enum", "equals", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "from", "get", "goto", "group", "if", "implicit", "in", "int", "into", "interface", "internal", "is", "join", "let", "lock", "long", "namespace", "new", "null", "object", "on", "operator", "orderby", "out", "override", "params", "partial", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "select", "set", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "var", "virtual", "void", "volatile", "where", "while", "yield"}
                ColorizeCode(token, sharpWords)
            Else
                Dim vbKeywords() As String = {"AddHandler", "AddressOf", "Aggregate", "Alias", "All", "And", "AndAlso", "Ansi", "Any", "As", "Ascending", "Average", "Boolean", "By", "ByRef", "Byte", "ByVal", "Call", "Case", "Catch", "CBool", "CByte", "CChar", "CDate", "CDec", "CDbl", "Char", "CInt", "Class", "CLng", "CObj", "Const", "Continue", "Count", "CShort", "CSng", "CStr", "CType", "Date", "Decimal", "Declare", "Default", "DefaultStyleSheet", "Delegate", "Descending", "Dim", "DirectCast", "Distinct", "Do", "Double", "Each", "Else", "ElseIf", "End", "Enum", "Equals", "Erase", "Error", "Event", "Exit", "Explicit", "False", "Finally", "For", "Friend", "From", "Function", "Get", "GetType", "GoSub", "GoTo", "Group", "Group", "Handles", "If", "Implements", "Imports", "In", "Inherits", "Integer", "Interface", "Into", "Is", "IsNot", "Join", "Let", "Lib", "Like", "Long", "LongCount", "Loop", "Max", "Me", "Min", "Mod", "Module", "MustInherit", "MustOverride", "My", "MyBase", "MyClass", "Namespace", "New", "Next", "Not", "Nothing", "NotInheritable", "NotOverridable", "(?<!\.)Object", "Off", "On", "Option", "Optional", "Or", "Order", "OrElse", "Overloads", "Overridable", "Overrides", "ParamArray", "Partial", "Preserve", "Private", "Property", "Protected", "Public", "RaiseEvent", "ReadOnly", "ReDim", "RemoveHandler", "Resume", "Return", "Select", "Set", "Shadows", "Shared", "Short", "Single", "Skip", "Static", "Step", "Stop", "String", "Structure", "Sub", "Sum", "SyncLock", "Take", "Then", "Throw", "To", "True", "Try", "TypeOf", "Unicode", "Until", "Variant", "When", "Where", "While", "With", "WithEvents", "WriteOnly", "Xor", "SByte", "UInteger", "ULong", "UShort", "Using", "CSByte", "CUInt", "CULng", "CUShort"}
                ColorizeCode(token, vbKeywords)
            End If

            rtbClass.SelectedText = token
        Next
        rtbClass.SelectedText = "" & vbLf
    End Sub

    Private Sub ColorizeCode(token As String, keywords() As String)
        Dim i As Integer = 0
        Do While (i < keywords.Length)
            If (keywords(i) = token) Then
                rtbClass.SelectionColor = Color.Blue
                rtbClass.SelectionFont = New Font("Courier New", 10, FontStyle.Regular)
            End If
            i += 1
        Loop
    End Sub

    Private Function ReadNArrangedFile() As String
        Dim objReader As StreamReader
        If _GlobalSettings.UseCSharp Then
            objReader = New StreamReader(CSharpTempFile)
        Else
            objReader = New StreamReader(VBTempFile)
        End If
        Dim response As String = objReader.ReadToEnd()
        objReader.Close()
        Return response
    End Function

    Private Sub RunScript()
        Try
            Shell(String.Format("{0} ", NArrange_Batch_File_Location), AppWinStyle.Hide, True)
        Catch ex As Exception
            Throw New Exception("Unable to produce report due to error with script", ex)
        End Try
    End Sub

    Private Sub ScrubDataWithNArrange(ByRef classText As String)
        BuildBatchFile(classText)
        RunScript()
        classText = ReadNArrangedFile()
    End Sub

#End Region

End Class