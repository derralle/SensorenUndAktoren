Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.Collections.Generic
Imports System.Windows.Forms


Public Class SensorenUndAktoren

    Private _SwApp As SldWorks
    Public Property SwApp As SldWorks
        Get
            Return _SwApp
        End Get
        Set(ByVal value As SldWorks)
            _SwApp = value
        End Set
    End Property



    Public AttributDefinition As IAttributeDef
    
    Dim WithEvents BgDataset As New SensorenUndAktoren_BG
    Dim WithEvents Form As New SundA_Form

    Public Sub New()





    End Sub

    ''' <summary>
    ''' Atributte registrieren (Nur 1x Pro Laufzeit SWX!!)
    ''' </summary>
    ''' <param name="iswapp">SolidWorks Applikation</param>
    ''' <remarks></remarks>
    Public Sub initalisierung(ByRef iswapp As SldWorks)
        _SwApp = iswapp


        'Attribut definieren (Nur 1x pro Laufzeit SWX!)
        AttributDefinition = Me.SwApp.IDefineAttribute("MAISUNDA-V0.1")
        AttributDefinition.AddParameter("Hash", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("BMK", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Bezeichnung", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Typ", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Abfrage Stellung", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Grundstellung", swParamType_e.swParamTypeString, 0.0#, 0)
        'Attribut registrieren (Nur 1x pro Laufzeit SWX!)
        AttributDefinition.Register()
    End Sub


    Public Sub AddAttributeToComponent()

        Dim modeldoc As ModelDoc2 = SwApp.ActiveDoc

        'Prüfen ob das Dokument eine Baugruppe ist
        If modeldoc.GetType <> swDocumentTypes_e.swDocASSEMBLY Then

            MsgBox("Dokument ist keine Baugruppe!")
            Throw New AggregateException

        End If

        Dim SelMgr As ISelectionMgr = modeldoc.ISelectionManager
        Dim icomp As IComponent2
        Dim Att As Attribute
        Dim Parameter As Parameter


        icomp = SelMgr.GetSelectedObject6(1, -1)

        'Test ob schon ein Attribut angehängt wurde
        If IsNothing(icomp.FindAttribute(AttributDefinition, 0)) Then

            Att = Me.AttributDefinition.CreateInstance5(modeldoc, icomp, CreateUniqueFeatureName("Elektropneumatik", modeldoc), 0, swInConfigurationOpts_e.swAllConfiguration)
            Parameter = Att.GetParameter("Hash")
            Parameter.SetStringValue2(System.Guid.NewGuid.ToString, swConfigurationOptions2_e.swConfigOption_LinkToParent, "")
            MsgBox(Att.IGetComponent2.GetPathName())
            modeldoc.EditRebuild3()
        Else
            MsgBox("Fehler: Da hängt schon was!")
        End If



        'NUR TEST
        GetAllAttributes(modeldoc)
    End Sub



    Public Sub BearbeitenForm()

        BgDataset.Clear()


        If BgDateiLesen() = False Then
            MsgBox("Keine Tabelle in der Baugruppe gefunden!")
        End If
        DatenAusSwxEinlesen()
        HashInTabelleSchreiben(DatenAusSwxEinlesen()) 'Hashtabelle 

        Form = New SundA_Form


        Form.DataGridView_Baugruppe.DataSource = BgDataset.TabelleBaugruppe
        BaumstrukturAufbauen(Form.TreeView1)
        Form.Show()

    End Sub


    ''' <summary>
    ''' Hashliste der SWX-Komponenten aus SWX auslesen
    ''' </summary>
    ''' <returns></returns>
    ''' Liste der gefundenen Hashs
    ''' <remarks></remarks>
    Private Function DatenAusSwxEinlesen() As List(Of String)

        Dim modeldoc As ModelDoc2 = SwApp.ActiveDoc
        Dim Toplevel As Boolean = True
        Dim HashListe As New List(Of String)
        HashListe.Clear()

        'Prüfen ob das Dokument eine Baugruppe ist
        If modeldoc.GetType <> swDocumentTypes_e.swDocASSEMBLY Then

            MsgBox("Dokument ist keine Baugruppe!")
            Throw New AggregateException

        End If

        Dim FeatureMgr As FeatureManager = modeldoc.FeatureManager      'Featuremanager erzeugen
        Dim Features As Object = FeatureMgr.GetFeatures(Toplevel)           'Features bekommen (True = nur Toplevel)
        Dim Feature As Feature
        Dim tmpAttribute As Attribute = Nothing
        Dim tmpParameter As Parameter


        For index = 0 To (FeatureMgr.GetFeatureCount(Toplevel) - 1)




            Feature = Features(index)
            Debug.Print("Feature: " & Feature.Name & "  Index: " & index)





            If Feature.GetTypeName2() = "Attribute" Then 'Feature swTnAttribute


                tmpAttribute = Feature.GetSpecificFeature2()
                tmpParameter = tmpAttribute.IGetParameter("Hash")
                HashListe.Add(tmpParameter.GetStringValue)
                Debug.Print("Hash: " & tmpParameter.GetStringValue)

            End If
        Next

        Return HashListe
    End Function


    ''' <summary>
    ''' Liest eine existierende Dataset-Datei ein 
    ''' </summary>
    ''' <returns></returns>
    ''' True wenn Datei vorhanden und ?eingelesen?
    ''' False wenn Datei nicht vorhanden
    ''' <remarks></remarks>
    Private Function BgDateiLesen() As Boolean
        Dim ModelDoc As ModelDoc2 = Me.SwApp.ActiveDoc
        Dim Dateiname As String = ModelDoc.GetPathName

        Dateiname = Dateiname.Substring(0, Dateiname.LastIndexOf("."))  'Dateiendung entfernen

        Dateiname = Dateiname & ".SundA" 'neue Dateiendung hinzufügen

        If FileIO.FileSystem.FileExists(Dateiname) Then

            BgDataset.Clear()
            BgDataset.ReadXml(Dateiname)
            BgDateiLesen = True
        Else
            BgDateiLesen = False
        End If
    End Function


    ''' <summary>
    ''' Tabelle in Datei Schreiben welche den selben Namen und Pfad hat wie die Baugruppe (Außer der Endung ".SundA")
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BgDateiErzeugen() Handles Form.SaveExit


        Dim ModelDoc As ModelDoc2 = Me.SwApp.ActiveDoc
        Dim Dateiname As String = ModelDoc.GetPathName

        Dateiname = Dateiname.Substring(0, Dateiname.LastIndexOf("."))

        Dateiname = Dateiname & ".SundA"
        BgDataset.WriteXml(Dateiname)

    End Sub


    ''' <summary>
    ''' Schreibt die Übergebene Liste in die Tabelle
    ''' </summary>
    ''' <param name="HashListe"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HashInTabelleSchreiben(HashListe As List(Of String)) As Boolean

        Dim tempRow As SensorenUndAktoren_BG.TabelleBaugruppeRow

        For Each item As String In HashListe

            If BgDataset.TabelleBaugruppe.Rows.Find(item) Is Nothing Then   'Prüfen ob GUID schon vorhanden

                tempRow = BgDataset.TabelleBaugruppe.NewTabelleBaugruppeRow  'Neue Zeile Erzeugen
                tempRow.GUID = item 'GUId in Zeile eintragen
                BgDataset.TabelleBaugruppe.AddTabelleBaugruppeRow(tempRow) 'Zeile in Tabelle einfügen

            End If
        Next

        Return True

    End Function


    ''' <summary>
    ''' Eindeutigen Featurenamen im Featurebaum erzeugen.
    ''' Feature -> Feature1 -> Feature2
    ''' </summary>
    ''' <param name="Name">gewünschter Featurename</param>
    ''' <param name="Modeldoc">Dokument als Modeldoc2</param>
    ''' <returns>Eindeutiger Featurename als String</returns>
    ''' <remarks></remarks>
    Private Function CreateUniqueFeatureName(ByVal Name As String, ByRef Modeldoc As ModelDoc2) As String

        Dim FeatureMgr As FeatureManager = Modeldoc.FeatureManager
        Dim RetName As String = Name
        Dim index As Long = 1


        If FeatureMgr.IsNameUsed(swNameType_e.swFeatureName, Name) Then

            While FeatureMgr.IsNameUsed(swNameType_e.swFeatureName, RetName)

                RetName = Name & index.ToString
                index = index + 1

            End While
        Else

            RetName = Name
        End If

        CreateUniqueFeatureName = RetName
    End Function


    ''' <summary>
    ''' Debugprozedur sucht alle Komponenten mit angehängtem Attribut und gibt den 
    ''' Hash des Attributes und den Pfadnamen der Komponentendatei aus.
    ''' </summary>
    ''' <param name="Modeldoc">Dokument</param>
    ''' <remarks></remarks>
    Sub GetAllAttributes(Modeldoc As ModelDoc2)

        Dim AllComponents As Object
        Dim Assy As AssemblyDoc = Modeldoc
        Dim Comp As Component2
        Dim Compcount As Integer = Assy.GetComponentCount(True)
        Dim tmpAttribute As Attribute
        Dim tmpParameter As Parameter

        AllComponents = Assy.GetComponents(True) 'True = nur Toplevel

        For index = 1 To Compcount Step 1

            Comp = AllComponents(index - 1)

            tmpAttribute = Comp.FindAttribute(AttributDefinition, 0)

            'Prüfen ob Attribut vorhanden
            If IsNothing(tmpAttribute) = False Then


                tmpParameter = tmpAttribute.GetParameter("Hash")

                Debug.Print("UUID: " & tmpParameter.GetStringValue() & " --> " & Comp.GetPathName())


            End If
        Next
    End Sub

    ''' <summary>
    ''' Baut die Baumstruktur im Form auf
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub BaumstrukturAufbauen(ByRef TreeView As Windows.Forms.TreeView)
        Dim Baum As New Baumstruktur
        Dim Knoten As TreeNode

        'Treeview löschen
        TreeView.Nodes.Clear()

        For Each row As SensorenUndAktoren_BG.TabelleBaugruppeRow In BgDataset.TabelleBaugruppe

            Baum.DatensatzHinzufügen(row.GUID, row.BMK, row.Koppelung)

        Next

        Baum.StrukturAufbauen(TreeView.Nodes.Add("Baugruppe", "Baugruppe"))

        If IsNothing(Form.Focus_GUID) = False Then

            Knoten = TreeView.Nodes.Find(Form.Focus_GUID, True)(0)
            Knoten.EnsureVisible()

        End If
    End Sub

    ''' <summary>
    ''' Fügt ein die Formdaten in die Tabelle ein
    ''' </summary>
    ''' <param name="Koppelung">GUID der Vaterzeile</param>
    ''' <remarks></remarks>
    Sub KomponenteEinfuegen(ByVal Koppelung As String) Handles Form.KomponenteEinfuegen

        Dim Zeile As SensorenUndAktoren_BG.TabelleBaugruppeRow
        Dim NeueGUID As String = Guid.NewGuid.ToString


        Zeile = BgDataset.TabelleBaugruppe.NewRow()
        Zeile.GUID = NeueGUID
        Zeile.Koppelung = Koppelung
        BgDataset.TabelleBaugruppe.Rows.Add(Zeile)

        Form.Focus_GUID = NeueGUID
        BaumstrukturAufbauen(Form.TreeView1)




    End Sub

    ''' <summary>
    ''' Liest die ausgewählte Zeile in das Form ein
    ''' </summary>
    ''' <param name="GUID"></param>
    ''' <remarks></remarks>
    Sub TabellenzeileInFormEinlesen(GUID As String) Handles Form.NeuerFocus

        Dim Tabellenname As String = BgDataset.TabelleBaugruppe.TableName
        Dim ZeilenArray() As DataRow
        Dim Zeile As DataRow
        Dim Ausdruck As String = "GUID = '"
        Ausdruck = Ausdruck & GUID & "'"

        'Form auffüllen
        ZeilenArray = BgDataset.Tables(Tabellenname).Select(Ausdruck)

        If ZeilenArray.Length = 1 Then 'Prüfen ob es nur einen Treffer gibt
            Zeile = ZeilenArray(0)
            Form.Label_GUID.Text = "ID: " & Zeile.Item("GUID").ToString
            Form.TextBox_BMK.Text = Zeile.Item("BMK").ToString
            Form.TextBox_Bezeichnung.Text = Zeile.Item("Bezeichnung").ToString
            Form.TextBox_Grundstellung.Text = Zeile.Item("Grundstellung").ToString
            Form.TextBox_Funktion.Text = Zeile.Item("Funktion").ToString
            Form.TextBox_Typ.Text = Zeile.Item("Typ").ToString

            Form.CheckBox_unterdrueckt.Checked = Zeile.Item("Unterdrückt")
        Else
            MsgBox("Fehler GUID existiert mehrfach!")
        End If
    End Sub

    ''' <summary>
    ''' Schreibt die Formdaten in die Tabelle
    ''' </summary>
    ''' <remarks></remarks>
    Sub FormdatenInTabelle() Handles Form.KnotenUebernehmen

        Dim Tabellenname As String = BgDataset.TabelleBaugruppe.TableName
        Dim ZeilenArray() As DataRow
        Dim Zeile As DataRow
        Dim Ausdruck As String = "GUID = '"
        Ausdruck = Ausdruck & Form.Focus_GUID & "'"

        ZeilenArray = BgDataset.Tables(Tabellenname).Select(Ausdruck) 'Zeile mit entsprechender GUID aus Tabelle holen

        If ZeilenArray.Length = 1 Then 'Prüfen ob es nur einen Treffer gibt
            Zeile = ZeilenArray(0)

            Zeile.Item("BMK") = Form.TextBox_BMK.Text
            Zeile.Item("Bezeichnung") = Form.TextBox_Bezeichnung.Text
            Zeile.Item("Grundstellung") = Form.TextBox_Grundstellung.Text
            Zeile.Item("Funktion") = Form.TextBox_Funktion.Text
            Zeile.Item("Typ") = Form.TextBox_Typ.Text

            Zeile.Item("Unterdrückt") = Form.CheckBox_unterdrueckt.Checked
        Else
            MsgBox("Fehler GUID existiert mehrfach!")
        End If

        KnotenAktualisieren(Form.TreeView1, Form.Focus_GUID)

    End Sub


    ''' <summary>
    ''' Aktualisiert das Treeview und stellt sicher das
    ''' der gewählte Treenode sichtbar ist
    ''' </summary>
    ''' <param name="TreeView"></param>
    ''' <param name="GUID"></param>
    ''' <remarks></remarks>
    Private Sub KnotenAktualisieren(ByRef TreeView As TreeView, ByRef GUID As String)

        Dim Knoten As TreeNode

        BaumstrukturAufbauen(Form.TreeView1)

        Knoten = TreeView.Nodes.Find(Form.Focus_GUID, True)(0)
        Knoten.EnsureVisible()


    End Sub



End Class
