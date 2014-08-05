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
    



    Public Sub New()





    End Sub

    Public Sub initalisierung(ByRef iswapp As SldWorks)
        _SwApp = iswapp

        'Attribut definieren (Nur 1x pro Laufzeit SWX!)
        AttributDefinition = Me.SwApp.IDefineAttribute("MAISUNDA-V0.1")
        AttributDefinition.AddParameter("Hash", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("BMK", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Bezeichnung", swParamType_e.swParamTypeString, 0.0#, 0)
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

    Private Function CreateUniqueFeatureName(ByVal Name As String, ByRef Modeldoc As ModelDoc2) As String



        Dim FeatureMgr As FeatureManager = ModelDoc.FeatureManager

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



    Sub GetAllAttributes(Modeldoc As ModelDoc2)

        Dim AllComponents As Object
        Dim Assy As AssemblyDoc = Modeldoc
        Dim Comp As Component2
        Dim Compcount As Integer = Assy.GetComponentCount(True)
        Dim tmpAttribute As Attribute
        Dim tmpParameter As Parameter

        AllComponents = Assy.GetComponents(True)


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