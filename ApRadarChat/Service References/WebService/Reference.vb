﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30128.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace WebService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="services.apradar.com", ConfigurationName:="WebService.ValidationServicePortType")>  _
    Public Interface ValidationServicePortType
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/CheckMapVersion", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function CheckMapVersion() As <System.ServiceModel.MessageParameterAttribute(Name:="return")> WebService.VersionInfo
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/Activate", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function Activate(ByVal ComputerKey As String, ByVal UserName As String, ByVal Password As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> WebService.ValidationResult
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/Deactivate", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function Deactivate(ByVal ComputerKey As String, ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/ValidateUser", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function ValidateUser(ByVal UserName As String, ByVal Pssword As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/ValidateUserEx", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function ValidateUserEx(ByVal UserName As String, ByVal Password As String, ByVal ComputerKey As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> WebService.ValidationInfo
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/ValidateChatUser", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function ValidateChatUser(ByVal UserName As String, ByVal Password As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> WebService.ChatLoginResult
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/CheckChatAdmin", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function CheckChatAdmin(ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/CheckChatBan", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function CheckChatBan(ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/PromoteToAdmin", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function PromoteToAdmin(ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/DemoteAdmin", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function DemoteAdmin(ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/BanUser", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function BanUser(ByVal UserName As String, ByVal Reason As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/RemoveBan", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function RemoveBan(ByVal UserName As String) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/GetFileList", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function GetFileList() As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/GetFileListEx", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function GetFileListEx() As <System.ServiceModel.MessageParameterAttribute(Name:="return")> FileEx()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/SetMapVersion", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function SetMapVersion(ByVal FileDate As String, ByVal UpdateType As Integer) As <System.ServiceModel.MessageParameterAttribute(Name:="return")> Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/GetApRadarVersion", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function GetApRadarVersion() As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://services.apradar.com/validationservice.php/GetNews", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(Style:=System.ServiceModel.OperationFormatStyle.Rpc, Use:=System.ServiceModel.OperationFormatUse.Encoded),  _
         System.ServiceModel.ServiceKnownTypeAttribute(GetType(FileEx))>  _
        Function GetNews() As <System.ServiceModel.MessageParameterAttribute(Name:="return")> String
    End Interface
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="services.apradar.com")>  _
    Partial Public Class VersionInfo
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private mapPackVersionField As Date
        
        Private mapIniVersionField As Date
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="date")>  _
        Public Property MapPackVersion() As Date
            Get
                Return Me.mapPackVersionField
            End Get
            Set
                Me.mapPackVersionField = value
                Me.RaisePropertyChanged("MapPackVersion")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="date")>  _
        Public Property MapIniVersion() As Date
            Get
                Return Me.mapIniVersionField
            End Get
            Set
                Me.mapIniVersionField = value
                Me.RaisePropertyChanged("MapIniVersion")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="services.apradar.com")>  _
    Partial Public Class FileEx
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private fileNameField As String
        
        Private fileSizeField As Integer
        
        '''<remarks/>
        Public Property FileName() As String
            Get
                Return Me.fileNameField
            End Get
            Set
                Me.fileNameField = value
                Me.RaisePropertyChanged("FileName")
            End Set
        End Property
        
        '''<remarks/>
        Public Property FileSize() As Integer
            Get
                Return Me.fileSizeField
            End Get
            Set
                Me.fileSizeField = value
                Me.RaisePropertyChanged("FileSize")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="services.apradar.com")>  _
    Partial Public Class ChatLoginResult
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private canChatField As Boolean
        
        Private userIDField As Integer
        
        '''<remarks/>
        Public Property CanChat() As Boolean
            Get
                Return Me.canChatField
            End Get
            Set
                Me.canChatField = value
                Me.RaisePropertyChanged("CanChat")
            End Set
        End Property
        
        '''<remarks/>
        Public Property UserID() As Integer
            Get
                Return Me.userIDField
            End Get
            Set
                Me.userIDField = value
                Me.RaisePropertyChanged("UserID")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="services.apradar.com")>  _
    Partial Public Class ValidationInfo
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private validationResultField As String
        
        Private activationCountField As Integer
        
        Private expirationDateField As Date
        
        '''<remarks/>
        Public Property ValidationResult() As String
            Get
                Return Me.validationResultField
            End Get
            Set
                Me.validationResultField = value
                Me.RaisePropertyChanged("ValidationResult")
            End Set
        End Property
        
        '''<remarks/>
        Public Property ActivationCount() As Integer
            Get
                Return Me.activationCountField
            End Get
            Set
                Me.activationCountField = value
                Me.RaisePropertyChanged("ActivationCount")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="date")>  _
        Public Property ExpirationDate() As Date
            Get
                Return Me.expirationDateField
            End Get
            Set
                Me.expirationDateField = value
                Me.RaisePropertyChanged("ExpirationDate")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30128.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.SoapTypeAttribute([Namespace]:="services.apradar.com")>  _
    Partial Public Class ValidationResult
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private validField As Boolean
        
        Private activationCountField As Integer
        
        Private expirationDateField As Date
        
        Private resultMessageField As String
        
        Private errorMessageField As String
        
        '''<remarks/>
        Public Property Valid() As Boolean
            Get
                Return Me.validField
            End Get
            Set
                Me.validField = value
                Me.RaisePropertyChanged("Valid")
            End Set
        End Property
        
        '''<remarks/>
        Public Property ActivationCount() As Integer
            Get
                Return Me.activationCountField
            End Get
            Set
                Me.activationCountField = value
                Me.RaisePropertyChanged("ActivationCount")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.SoapElementAttribute(DataType:="date")>  _
        Public Property ExpirationDate() As Date
            Get
                Return Me.expirationDateField
            End Get
            Set
                Me.expirationDateField = value
                Me.RaisePropertyChanged("ExpirationDate")
            End Set
        End Property
        
        '''<remarks/>
        Public Property ResultMessage() As String
            Get
                Return Me.resultMessageField
            End Get
            Set
                Me.resultMessageField = value
                Me.RaisePropertyChanged("ResultMessage")
            End Set
        End Property
        
        '''<remarks/>
        Public Property ErrorMessage() As String
            Get
                Return Me.errorMessageField
            End Get
            Set
                Me.errorMessageField = value
                Me.RaisePropertyChanged("ErrorMessage")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ValidationServicePortTypeChannel
        Inherits WebService.ValidationServicePortType, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class ValidationServicePortTypeClient
        Inherits System.ServiceModel.ClientBase(Of WebService.ValidationServicePortType)
        Implements WebService.ValidationServicePortType
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function CheckMapVersion() As WebService.VersionInfo Implements WebService.ValidationServicePortType.CheckMapVersion
            Return MyBase.Channel.CheckMapVersion
        End Function
        
        Public Function Activate(ByVal ComputerKey As String, ByVal UserName As String, ByVal Password As String) As WebService.ValidationResult Implements WebService.ValidationServicePortType.Activate
            Return MyBase.Channel.Activate(ComputerKey, UserName, Password)
        End Function
        
        Public Function Deactivate(ByVal ComputerKey As String, ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.Deactivate
            Return MyBase.Channel.Deactivate(ComputerKey, UserName)
        End Function
        
        Public Function ValidateUser(ByVal UserName As String, ByVal Pssword As String) As String() Implements WebService.ValidationServicePortType.ValidateUser
            Return MyBase.Channel.ValidateUser(UserName, Pssword)
        End Function
        
        Public Function ValidateUserEx(ByVal UserName As String, ByVal Password As String, ByVal ComputerKey As String) As WebService.ValidationInfo Implements WebService.ValidationServicePortType.ValidateUserEx
            Return MyBase.Channel.ValidateUserEx(UserName, Password, ComputerKey)
        End Function
        
        Public Function ValidateChatUser(ByVal UserName As String, ByVal Password As String) As WebService.ChatLoginResult Implements WebService.ValidationServicePortType.ValidateChatUser
            Return MyBase.Channel.ValidateChatUser(UserName, Password)
        End Function
        
        Public Function CheckChatAdmin(ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.CheckChatAdmin
            Return MyBase.Channel.CheckChatAdmin(UserName)
        End Function
        
        Public Function CheckChatBan(ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.CheckChatBan
            Return MyBase.Channel.CheckChatBan(UserName)
        End Function
        
        Public Function PromoteToAdmin(ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.PromoteToAdmin
            Return MyBase.Channel.PromoteToAdmin(UserName)
        End Function
        
        Public Function DemoteAdmin(ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.DemoteAdmin
            Return MyBase.Channel.DemoteAdmin(UserName)
        End Function
        
        Public Function BanUser(ByVal UserName As String, ByVal Reason As String) As Boolean Implements WebService.ValidationServicePortType.BanUser
            Return MyBase.Channel.BanUser(UserName, Reason)
        End Function
        
        Public Function RemoveBan(ByVal UserName As String) As Boolean Implements WebService.ValidationServicePortType.RemoveBan
            Return MyBase.Channel.RemoveBan(UserName)
        End Function
        
        Public Function GetFileList() As String() Implements WebService.ValidationServicePortType.GetFileList
            Return MyBase.Channel.GetFileList
        End Function
        
        Public Function GetFileListEx() As FileEx() Implements WebService.ValidationServicePortType.GetFileListEx
            Return MyBase.Channel.GetFileListEx
        End Function
        
        Public Function SetMapVersion(ByVal FileDate As String, ByVal UpdateType As Integer) As Boolean Implements WebService.ValidationServicePortType.SetMapVersion
            Return MyBase.Channel.SetMapVersion(FileDate, UpdateType)
        End Function
        
        Public Function GetApRadarVersion() As String Implements WebService.ValidationServicePortType.GetApRadarVersion
            Return MyBase.Channel.GetApRadarVersion
        End Function
        
        Public Function GetNews() As String Implements WebService.ValidationServicePortType.GetNews
            Return MyBase.Channel.GetNews
        End Function
    End Class
End Namespace