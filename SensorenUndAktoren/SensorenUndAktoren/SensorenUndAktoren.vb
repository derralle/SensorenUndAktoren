﻿Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.Collections.Generic


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
    Dim WithEvents BgBaum As New TreeControlItem
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
            DatenAusSwxEinlesen()
            HashInTabelleSchreiben(DatenAusSwxEinlesen()) 'Hashtabelle 
            Form.DataGridView_Baugruppe.DataSource = BgDataset.TabelleBaugruppe
            Form.Show()

        End If




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


    Private Function BgDateiLesen() As Boolean

        BgDateiLesen = False

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




End Class
