Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports SolidWorks.Interop.swpublished
Imports SolidWorksTools
Imports SolidWorksTools.File

Imports System.Collections.Generic
Imports System.Diagnostics

<Guid("C8E9120E-8297-4106-87A1-7552543121AC")> _
    <ComVisible(True)> _
    <SwAddin( _
        Description:="E-Cad Austausch", _
        Title:="SensorenUndAktoren", _
        LoadAtStartup:=True _
        )> _
Public Class SwAddin
    Implements SolidWorks.Interop.swpublished.SwAddin

#Region "Local Variables"
    Dim WithEvents iSwApp As SldWorks
    Dim iCmdMgr As ICommandManager
    Dim addinID As Integer
    Dim openDocs As Hashtable
    Dim SwEventPtr As SldWorks
    Dim ppage As UserPMPage
    Dim iBmp As BitmapHandler
    Dim SundA As New SensorenUndAktoren()


    Public Const mainCmdGroupID As Integer = 0
    Public Const mainItemID1 As Integer = 0
    Public Const mainItemID2 As Integer = 0
    

    Public Const flyoutGroupID As Integer = 91

    ' Public Properties
    ReadOnly Property SwApp() As SldWorks
        Get
            Return iSwApp
        End Get
    End Property

    ReadOnly Property CmdMgr() As ICommandManager
        Get
            Return iCmdMgr
        End Get
    End Property

    ReadOnly Property OpenDocumentsTable() As Hashtable
        Get
            Return openDocs
        End Get
    End Property
#End Region

#Region "SolidWorks Registration"

    <ComRegisterFunction()> Public Shared Sub RegisterFunction(ByVal t As Type)

        ' Get Custom Attribute: SwAddinAttribute
        Dim attributes() As Object
        Dim SWattr As SwAddinAttribute = Nothing

        attributes = System.Attribute.GetCustomAttributes(GetType(SwAddin), GetType(SwAddinAttribute))

        If attributes.Length > 0 Then
            SWattr = DirectCast(attributes(0), SwAddinAttribute)
        End If
        Try
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine
            Dim hkcu As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser

            Dim keyname As String = "SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}"
            Dim addinkey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey(keyname)
            addinkey.SetValue(Nothing, 0)
            addinkey.SetValue("Description", SWattr.Description)
            addinkey.SetValue("Title", SWattr.Title)

            keyname = "Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}"
            addinkey = hkcu.CreateSubKey(keyname)
            addinkey.SetValue(Nothing, SWattr.LoadAtStartup, Microsoft.Win32.RegistryValueKind.DWord)
        Catch nl As System.NullReferenceException
            Console.WriteLine("There was a problem registering this dll: SWattr is null.\n " & nl.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: SWattr is null.\n" & nl.Message)
        Catch e As System.Exception
            Console.WriteLine("There was a problem registering this dll: " & e.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem registering this dll: " & e.Message)
        End Try
    End Sub

    <ComUnregisterFunction()> Public Shared Sub UnregisterFunction(ByVal t As Type)
        Try
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine
            Dim hkcu As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser

            Dim keyname As String = "SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}"
            hklm.DeleteSubKey(keyname)

            keyname = "Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}"
            hkcu.DeleteSubKey(keyname)
        Catch nl As System.NullReferenceException
            Console.WriteLine("There was a problem unregistering this dll: SWattr is null.\n " & nl.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: SWattr is null.\n" & nl.Message)
        Catch e As System.Exception
            Console.WriteLine("There was a problem unregistering this dll: " & e.Message)
            System.Windows.Forms.MessageBox.Show("There was a problem unregistering this dll: " & e.Message)
        End Try

    End Sub

#End Region

#Region "ISwAddin Implementation"

    Function ConnectToSW(ByVal ThisSW As Object, ByVal Cookie As Integer) As Boolean Implements SolidWorks.Interop.swpublished.SwAddin.ConnectToSW
        iSwApp = ThisSW
        addinID = Cookie

        ' Setup callbacks
        iSwApp.SetAddinCallbackInfo(0, Me, addinID)

        ' Setup the Command Manager
        iCmdMgr = iSwApp.GetCommandManager(Cookie)
        AddCommandMgr()

        'Setup the Event Handlers
        SwEventPtr = iSwApp
        openDocs = New Hashtable
        AttachEventHandlers()

        'Setup Sample Property Manager
        AddPMP()

        ConnectToSW = True

        '
        SundA.initalisierung(Me.iSwApp)

    End Function

    Function DisconnectFromSW() As Boolean Implements SolidWorks.Interop.swpublished.SwAddin.DisconnectFromSW

        RemoveCommandMgr()
        RemovePMP()
        DetachEventHandlers()

        System.Runtime.InteropServices.Marshal.ReleaseComObject(iCmdMgr)
        iCmdMgr = Nothing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(iSwApp)
        iSwApp = Nothing
        'The addin _must_ call GC.Collect() here in order to retrieve all managed code pointers 
        GC.Collect()
        GC.WaitForPendingFinalizers()

        GC.Collect()
        GC.WaitForPendingFinalizers()

        DisconnectFromSW = True
    End Function
#End Region

#Region "UI Methods"
    Public Sub AddCommandMgr()

        Dim cmdGroup As ICommandGroup

        If iBmp Is Nothing Then
            iBmp = New BitmapHandler()
        End If

        Dim thisAssembly As Assembly

        '#Index der Funktionen
        Dim cmdIndex0 As Integer
        Dim cmdIndex1 As Integer


        Dim Title As String = "SensorenUndAktoren"
        Dim ToolTip As String = "Firmenspezifische Anpassungen"


        Dim docTypes() As Integer = {swDocumentTypes_e.swDocASSEMBLY, _
                                       swDocumentTypes_e.swDocDRAWING, _
                                       swDocumentTypes_e.swDocPART}

        thisAssembly = System.Reflection.Assembly.GetAssembly(Me.GetType())

        Dim cmdGroupErr As Integer = 0
        Dim ignorePrevious As Boolean = False

        Dim registryIDs As Object = Nothing
        Dim getDataResult As Boolean = iCmdMgr.GetGroupDataFromRegistry(mainCmdGroupID, registryIDs)

        Dim knownIDs As Integer() = New Integer(0) {mainItemID1}

        If getDataResult Then
            If Not CompareIDs(registryIDs, knownIDs) Then 'if the IDs don't match, reset the commandGroup
                ignorePrevious = True
            End If
        End If

        cmdGroup = iCmdMgr.CreateCommandGroup2(mainCmdGroupID, Title, ToolTip, "", -1, ignorePrevious, cmdGroupErr)
        If cmdGroup Is Nothing Or thisAssembly Is Nothing Then
            Throw New NullReferenceException()
        End If



        cmdGroup.LargeIconList = iBmp.CreateFileFromResourceBitmap("SensorenUndAktoren.ToolbarLarge.bmp", thisAssembly)
        cmdGroup.SmallIconList = iBmp.CreateFileFromResourceBitmap("SensorenUndAktoren.ToolbarSmall.bmp", thisAssembly)
        cmdGroup.LargeMainIcon = iBmp.CreateFileFromResourceBitmap("SensorenUndAktoren.MainIconLarge.bmp", thisAssembly)
        cmdGroup.SmallMainIcon = iBmp.CreateFileFromResourceBitmap("SensorenUndAktoren.MainIconSmall.bmp", thisAssembly)

        Dim menuToolbarOption As Integer = swCommandItemType_e.swMenuItem Or swCommandItemType_e.swToolbarItem

        cmdIndex0 = cmdGroup.AddCommandItem2("Elektrokomponenten Einf�gen", _
                                             -1, _
                                             "Komponenten einf�gen", _
                                             "Komponenten einf�gen", _
                                             0, _
                                             "SAeinfuegen", _
                                             "", _
                                             mainItemID1, _
                                             menuToolbarOption)

        cmdIndex1 = cmdGroup.AddCommandItem2("E-Komponenten bearbeiten", _
                                             -1, _
                                             "Bearbeiten der Sensoren und Aktoren in dieser Baugruppe", _
                                             "Bearbeiten der Sensoren und Aktoren in dieser Baugruppe", _
                                             11, _
                                             "SATabelle", _
                                             "", _
                                             mainItemID2, _
                                             menuToolbarOption)



        'cmdIndex1 = cmdGroup.AddCommandItem2("Show PMP", -1, "Display sample property manager", "Show PMP", 2, "ShowPMP", "PMPEnable", mainItemID2, menuToolbarOption)

        cmdGroup.HasToolbar = False
        cmdGroup.HasMenu = True
        cmdGroup.Activate()

        'Dim flyGroup As FlyoutGroup
        'flyGroup = iCmdMgr.CreateFlyoutGroup(flyoutGroupID, "Dynamic Flyout", "Flyout Tooltip", "Flyout Hint", _
        ' cmdGroup.SmallMainIcon, cmdGroup.LargeMainIcon, cmdGroup.SmallIconList, cmdGroup.LargeIconList, "FlyoutCallback", "FlyoutEnable")

        'flyGroup.AddCommandItem("FlyoutCommand 1", "test", 0, "FlyoutCommandItem1", "FlyoutEnableCommandItem1")

        'flyGroup.FlyoutType = swCommandFlyoutStyle_e.swCommandFlyoutStyle_Simple


        'For Each docType As Integer In docTypes
        '    Dim cmdTab As ICommandTab = iCmdMgr.GetCommandTab(docType, Title)
        '    Dim bResult As Boolean

        '    If Not cmdTab Is Nothing And Not getDataResult Or ignorePrevious Then 'if tab exists, but we have ignored the registry info, re-create the tab.  Otherwise the ids won't matchup and the tab will be blank
        '        Dim res As Boolean = iCmdMgr.RemoveCommandTab(cmdTab)
        '        cmdTab = Nothing
        '    End If

        '    'If cmdTab Is Nothing Then

        '    '    cmdTab = iCmdMgr.AddCommandTab(docType, Title)

        '    '    Dim cmdBox As CommandTabBox = cmdTab.AddCommandTabBox

        '    '    Dim cmdIDs(3) As Integer
        '    '    Dim TextType(3) As Integer

        '    '    cmdIDs(0) = cmdGroup.CommandID(cmdIndex0)
        '    '    TextType(0) = swCommandTabButtonTextDisplay_e.swCommandTabButton_TextHorizontal

        '    '    cmdIDs(1) = cmdGroup.CommandID(cmdIndex1)
        '    '    TextType(1) = swCommandTabButtonTextDisplay_e.swCommandTabButton_TextHorizontal

        '    '    cmdIDs(2) = cmdGroup.ToolbarId
        '    '    TextType(2) = swCommandTabButtonTextDisplay_e.swCommandTabButton_TextHorizontal


        '    '    bResult = cmdBox.AddCommands(cmdIDs, TextType)

        '    '    Dim cmdBox1 As CommandTabBox = cmdTab.AddCommandTabBox()
        '    '    ReDim cmdIDs(1)
        '    '    ReDim TextType(1)

        '    '    'cmdIDs(0) = flyGroup.CmdID
        '    '    TextType(0) = swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow

        '    '    bResult = cmdBox1.AddCommands(cmdIDs, TextType)

        '    '    cmdTab.AddSeparator(cmdBox1, cmdIDs(0))

        '    'End If
        'Next

        thisAssembly = Nothing

    End Sub


    Public Sub RemoveCommandMgr()
        Try
            iBmp.Dispose()
            iCmdMgr.RemoveCommandGroup(mainCmdGroupID)
            iCmdMgr.RemoveFlyoutGroup(flyoutGroupID)
        Catch e As Exception
        End Try
    End Sub


    Function AddPMP() As Boolean
        'ppage = New UserPMPage
        'ppage.Init(iSwApp, Me)
    End Function

    Function RemovePMP() As Boolean
        'ppage = Nothing
    End Function

    Function CompareIDs(ByVal storedIDs() As Integer, ByVal addinIDs() As Integer) As Boolean

        Dim storeList As New List(Of Integer)(storedIDs)
        Dim addinList As New List(Of Integer)(addinIDs)

        addinList.Sort()
        storeList.Sort()

        If Not addinList.Count = storeList.Count Then

            Return False
        Else

            For i As Integer = 0 To addinList.Count - 1
                If Not addinList(i) = storeList(i) Then

                    Return False
                End If
            Next
        End If

        Return True
    End Function
#End Region

#Region "Event Methods"
    Sub AttachEventHandlers()
        AttachSWEvents()

        'Listen for events on all currently open docs
        AttachEventsToAllDocuments()
    End Sub

    Sub DetachEventHandlers()
        DetachSWEvents()

        'Close events on all currently open docs
        Dim docHandler As DocumentEventHandler
        Dim key As ModelDoc2
        Dim numKeys As Integer
        numKeys = openDocs.Count
        If numKeys > 0 Then
            Dim keys() As Object = New Object(numKeys - 1) {}

            'Remove all document event handlers
            openDocs.Keys.CopyTo(keys, 0)
            For Each key In keys
                docHandler = openDocs.Item(key)
                docHandler.DetachEventHandlers() 'This also removes the pair from the hash
                docHandler = Nothing
                key = Nothing
            Next
        End If
    End Sub

    Sub AttachSWEvents()
        Try
            AddHandler iSwApp.ActiveDocChangeNotify, AddressOf Me.SldWorks_ActiveDocChangeNotify
            AddHandler iSwApp.DocumentLoadNotify2, AddressOf Me.SldWorks_DocumentLoadNotify2
            AddHandler iSwApp.FileNewNotify2, AddressOf Me.SldWorks_FileNewNotify2
            AddHandler iSwApp.ActiveModelDocChangeNotify, AddressOf Me.SldWorks_ActiveModelDocChangeNotify
            AddHandler iSwApp.FileOpenPostNotify, AddressOf Me.SldWorks_FileOpenPostNotify
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Sub DetachSWEvents()
        Try
            RemoveHandler iSwApp.ActiveDocChangeNotify, AddressOf Me.SldWorks_ActiveDocChangeNotify
            RemoveHandler iSwApp.DocumentLoadNotify2, AddressOf Me.SldWorks_DocumentLoadNotify2
            RemoveHandler iSwApp.FileNewNotify2, AddressOf Me.SldWorks_FileNewNotify2
            RemoveHandler iSwApp.ActiveModelDocChangeNotify, AddressOf Me.SldWorks_ActiveModelDocChangeNotify
            RemoveHandler iSwApp.FileOpenPostNotify, AddressOf Me.SldWorks_FileOpenPostNotify
        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Sub AttachEventsToAllDocuments()
        Dim modDoc As ModelDoc2
        modDoc = iSwApp.GetFirstDocument()
        While Not modDoc Is Nothing
            If Not openDocs.Contains(modDoc) Then
                AttachModelDocEventHandler(modDoc)
            End If
            modDoc = modDoc.GetNext()
        End While
    End Sub

    Function AttachModelDocEventHandler(ByVal modDoc As ModelDoc2) As Boolean
        If modDoc Is Nothing Then
            Return False
        End If
        Dim docHandler As DocumentEventHandler = Nothing

        If Not openDocs.Contains(modDoc) Then
            Select Case modDoc.GetType
                Case swDocumentTypes_e.swDocPART
                    docHandler = New PartEventHandler()
                Case swDocumentTypes_e.swDocASSEMBLY
                    docHandler = New AssemblyEventHandler()
                Case swDocumentTypes_e.swDocDRAWING
                    docHandler = New DrawingEventHandler()
            End Select

            docHandler.Init(iSwApp, Me, modDoc)
            docHandler.AttachEventHandlers()
            openDocs.Add(modDoc, docHandler)
        End If
    End Function

    Sub DetachModelEventHandler(ByVal modDoc As ModelDoc2)
        Dim docHandler As DocumentEventHandler
        docHandler = openDocs.Item(modDoc)
        openDocs.Remove(modDoc)
        modDoc = Nothing
        docHandler = Nothing
    End Sub
#End Region

#Region "Event Handlers"
    Function SldWorks_ActiveDocChangeNotify() As Integer


    End Function

    Function SldWorks_DocumentLoadNotify2(ByVal docTitle As String, ByVal docPath As String) As Integer

    End Function

    Function SldWorks_FileNewNotify2(ByVal newDoc As Object, ByVal doctype As Integer, ByVal templateName As String) As Integer
        AttachEventsToAllDocuments()
    End Function

    Function SldWorks_ActiveModelDocChangeNotify() As Integer
        'TODO: Add your implementation here
    End Function

    Function SldWorks_FileOpenPostNotify(ByVal FileName As String) As Integer
        AttachEventsToAllDocuments()
    End Function
#End Region

#Region "UI Callbacks"

    'Eigenschaften von Schunk-Teilen �bernehmen


    Sub SAeinfuegen()

        SundA.AddAttributeToComponent()

    End Sub


    Sub SATabelle()

        SundA.BearbeitenForm()

    End Sub

    'Beispiel
    Sub CreateCube()

        'make sure we have a part open
        Dim partTemplate As String
        Dim model As ModelDoc2
        Dim featMan As FeatureManager

        partTemplate = iSwApp.GetUserPreferenceStringValue(swUserPreferenceStringValue_e.swDefaultTemplatePart)
        If Not partTemplate = "" Then
            model = iSwApp.NewDocument(partTemplate, swDwgPaperSizes_e.swDwgPaperA2size, 0.0, 0.0)

            model.InsertSketch2(True)
            model.SketchRectangle(0, 0, 0, 0.1, 0.1, 0.1, False)

            'Extrude the sketch
            featMan = model.FeatureManager
            featMan.FeatureExtrusion(True, _
                                      False, False, _
                                      swEndConditions_e.swEndCondBlind, swEndConditions_e.swEndCondBlind, _
                                      0.1, 0.0, _
                                      False, False, _
                                      False, False, _
                                      0.0, 0.0, _
                                      False, False, _
                                      False, False, _
                                      True, _
                                      False, False)
        Else
            System.Windows.Forms.MessageBox.Show("There is no part template available. Please check your options and make sure there is a part template selected, or select a new part template.")
        End If
    End Sub

    'Noch ein Beispiel
    Sub ShowPMP()
        If Not ppage Is Nothing Then
            ppage.Show()
        End If
    End Sub

    Function PMPEnable() As Integer
        If iSwApp.ActiveDoc Is Nothing Then
            PMPEnable = 0
        Else
            PMPEnable = 1
        End If
    End Function

    Sub FlyoutCallback()

        Dim flyGroup As FlyoutGroup = iCmdMgr.GetFlyoutGroup(flyoutGroupID)
        flyGroup.RemoveAllCommandItems()

        flyGroup.AddCommandItem(System.DateTime.Now.ToLongTimeString(), "test", 0, "FlyoutCommandItem1", "FlyoutEnableCommandItem1")

    End Sub

    Function FlyoutEnable() As Integer
        Return 1
    End Function

    Sub FlyoutCommandItem1()
        iSwApp.SendMsgToUser("Flyout command 1")
    End Sub

    Function FlyoutEnableCommandItem1() As Integer
        Return 1
    End Function


#End Region

End Class

