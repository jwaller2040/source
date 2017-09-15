''' -----------------------------------------------------------------------------
''' Project	 : ClassGenerator
''' Class	 : Options
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Settings Editor
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	Srinivas Miriyala	6/29/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Options
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnCancel As System.Windows.Forms.Button
    Friend  WithEvents btnOk As System.Windows.Forms.Button

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend  WithEvents chkGenAttribs As System.Windows.Forms.CheckBox
    Friend  WithEvents chkSerializable As System.Windows.Forms.CheckBox
    Friend  WithEvents CSharp_Radio_Button As System.Windows.Forms.RadioButton
    Friend  WithEvents grpGeneral As System.Windows.Forms.GroupBox
    Friend  WithEvents VB_Radio_Button As System.Windows.Forms.RadioButton

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents chkUseNArrange As System.Windows.Forms.CheckBox
    Dim m_Settings As Settings

    #End Region

    #Region "Constructors"

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    #End Region

    #Region "Properties"

    Public Property Settings() As Settings
        Get
            If m_Settings Is Nothing Then
                m_Settings = New Settings
            End If
            Return m_Settings
        End Get
        Set(ByVal Value As Settings)
            m_Settings = Value
        End Set
    End Property

    #End Region

    #Region "Methods"

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CloseForm()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        SetSettings()
        CloseForm()
    End Sub

    Private Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub CSharp_Radio_Button_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CSharp_Radio_Button.CheckedChanged
        If Me.CSharp_Radio_Button.Checked Then
            Me.VB_Radio_Button.Checked = False
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options))
        Me.chkGenAttribs = New System.Windows.Forms.CheckBox()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.chkUseNArrange = New System.Windows.Forms.CheckBox()
        Me.CSharp_Radio_Button = New System.Windows.Forms.RadioButton()
        Me.VB_Radio_Button = New System.Windows.Forms.RadioButton()
        Me.chkSerializable = New System.Windows.Forms.CheckBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpGeneral.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkGenAttribs
        '
        Me.chkGenAttribs.Location = New System.Drawing.Point(16, 24)
        Me.chkGenAttribs.Name = "chkGenAttribs"
        Me.chkGenAttribs.Size = New System.Drawing.Size(136, 24)
        Me.chkGenAttribs.TabIndex = 0
        Me.chkGenAttribs.Text = "Generate Attributes"
        '
        'grpGeneral
        '
        Me.grpGeneral.Controls.Add(Me.chkUseNArrange)
        Me.grpGeneral.Controls.Add(Me.CSharp_Radio_Button)
        Me.grpGeneral.Controls.Add(Me.VB_Radio_Button)
        Me.grpGeneral.Controls.Add(Me.chkSerializable)
        Me.grpGeneral.Controls.Add(Me.chkGenAttribs)
        Me.grpGeneral.Location = New System.Drawing.Point(8, 16)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.Size = New System.Drawing.Size(176, 174)
        Me.grpGeneral.TabIndex = 1
        Me.grpGeneral.TabStop = False
        Me.grpGeneral.Text = "General"
        '
        'chkUseNArrange
        '
        Me.chkUseNArrange.Location = New System.Drawing.Point(16, 144)
        Me.chkUseNArrange.Name = "chkUseNArrange"
        Me.chkUseNArrange.Size = New System.Drawing.Size(136, 24)
        Me.chkUseNArrange.TabIndex = 3
        Me.chkUseNArrange.Text = "Use NArrange"
        '
        'CSharp_Radio_Button
        '
        Me.CSharp_Radio_Button.AutoSize = True
        Me.CSharp_Radio_Button.Checked = True
        Me.CSharp_Radio_Button.Location = New System.Drawing.Point(16, 88)
        Me.CSharp_Radio_Button.Name = "CSharp_Radio_Button"
        Me.CSharp_Radio_Button.Size = New System.Drawing.Size(100, 17)
        Me.CSharp_Radio_Button.TabIndex = 2
        Me.CSharp_Radio_Button.TabStop = True
        Me.CSharp_Radio_Button.Text = "Translate as C#"
        Me.CSharp_Radio_Button.UseVisualStyleBackColor = True
        '
        'VB_Radio_Button
        '
        Me.VB_Radio_Button.AutoSize = True
        Me.VB_Radio_Button.Location = New System.Drawing.Point(16, 55)
        Me.VB_Radio_Button.Name = "VB_Radio_Button"
        Me.VB_Radio_Button.Size = New System.Drawing.Size(100, 17)
        Me.VB_Radio_Button.TabIndex = 1
        Me.VB_Radio_Button.Text = "Translate as VB"
        Me.VB_Radio_Button.UseVisualStyleBackColor = True
        '
        'chkSerializable
        '
        Me.chkSerializable.Location = New System.Drawing.Point(16, 114)
        Me.chkSerializable.Name = "chkSerializable"
        Me.chkSerializable.Size = New System.Drawing.Size(136, 24)
        Me.chkSerializable.TabIndex = 0
        Me.chkSerializable.Text = "Make Serializable"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(12, 196)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "Ok"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(93, 196)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        '
        'Options
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(200, 242)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.grpGeneral)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Options"
        Me.Text = "Options"
        Me.grpGeneral.ResumeLayout(False)
        Me.grpGeneral.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Private Sub LoadSettings()
        Me.chkGenAttribs.Checked = Me.Settings.GenerateAttributes
        Me.VB_Radio_Button.Checked = Me.Settings.UseVB
        Me.CSharp_Radio_Button.Checked = Me.Settings.UseCSharp
        Me.chkSerializable.Checked = Me.Settings.MakeSerializable
        Me.chkUseNArrange.Checked = Me.Settings.UseNArrange
    End Sub

    Private Sub Options_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub SetSettings()
        m_Settings.GenerateAttributes = Me.chkGenAttribs.Checked
        m_Settings.UseVB = Me.VB_Radio_Button.Checked
        m_Settings.UseCSharp = Me.CSharp_Radio_Button.Checked
        m_Settings.MakeSerializable = Me.chkSerializable.Checked
        Me.Settings.UseNArrange = Me.chkUseNArrange.Checked
    End Sub

    Private Sub VB_Radio_Button_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VB_Radio_Button.CheckedChanged
        If Me.VB_Radio_Button.Checked Then
            Me.CSharp_Radio_Button.Checked = False
        End If
    End Sub

    #End Region

End Class