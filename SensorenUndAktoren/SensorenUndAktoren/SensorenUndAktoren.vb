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

        'Attribut definieren (Nur 1x pro Laufzeit!)
        AttributDefinition = Me.SwApp.IDefineAttribute("Elektropneumatik")
        AttributDefinition.AddParameter("Hash", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("BMK", swParamType_e.swParamTypeString, 0.0#, 0)
        AttributDefinition.AddParameter("Bezeichnung", swParamType_e.swParamTypeString, 0.0#, 0)

        'Attribut registrieren (Nur 1x pro Laufzeit!)
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


        icomp = SelMgr.GetSelectedObject6(1, -1)



        Att = Me.AttributDefinition.CreateInstance5(modeldoc, icomp, "Elektropneumatik", 0, swInConfigurationOpts_e.swAllConfiguration)

        MsgBox(Att.IGetComponent2.GetPathName())
        modeldoc.EditRebuild3()




    End Sub
End Class
