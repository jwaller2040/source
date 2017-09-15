''' -----------------------------------------------------------------------------
''' Project	 : ClassGenerator
''' Class	 : Settings
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Holds the global settings.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	James Waller thanks ---> Srinivas Miriyala	6/29/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Settings

#Region "Declarations"

    Private _GenerateAttributes As Boolean
    Private _MakeSerializable As Boolean
    Private _UseCSharp As Boolean
    Private _UseNArrange As Boolean
    Private _UseVB As Boolean

#End Region

#Region "Properties"

    Public Property GenerateAttributes() As Boolean
        Get
            Return _GenerateAttributes
        End Get
        Set(ByVal Value As Boolean)
            _GenerateAttributes = Value
        End Set
    End Property

    Public Property MakeSerializable() As Boolean
        Get
            Return _MakeSerializable
        End Get
        Set(ByVal Value As Boolean)
            _MakeSerializable = Value
        End Set
    End Property

    Public Property UseCSharp() As Boolean
        Get
            Return _UseCSharp
        End Get
        Set(ByVal Value As Boolean)
            _UseCSharp = Value
        End Set
    End Property

    Public Property UseNArrange() As Boolean
        Get
            Return _UseNArrange
        End Get
        Set(ByVal value As Boolean)
            _UseNArrange = value
        End Set
    End Property

    Public Property UseVB() As Boolean
        Get
            Return _UseVB
        End Get
        Set(ByVal Value As Boolean)
            _UseVB = Value
        End Set
    End Property

#End Region

#Region "Constructors"
    ''' <summary>
    ''' This is the option value setter
    ''' </summary>
    Public Sub New()
        Me._GenerateAttributes = True
        Me._UseCSharp = True
        Me._UseVB = False
        Me._MakeSerializable = False
        Me._UseNArrange = False
    End Sub

#End Region

End Class