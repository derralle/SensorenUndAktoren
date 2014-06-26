Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.Collections.Generic


Public Class SensorenUndAktoren



    Public Sub New()


    End Sub



    Public Sub AddAttributeToComponent(ByVal swapp As SldWorks)

        Dim modeldoc As ModelDoc2 = swapp.ActiveDoc

        'Prüfen ob das Dokument eine Baugruppe ist
        If modeldoc.GetType <> swDocumentTypes_e.swDocASSEMBLY Then

            MsgBox("Dokument ist keine Baugruppe!")
            Throw New AggregateException

        End If



        Dim SelMgr As ISelectionMgr = modeldoc.ISelectionManager
        Dim icomp As IComponent2
        Dim AttDef As AttributeDef
        Dim Att As Attribute


        icomp = SelMgr.GetSelectedObject6(1, -1)

        'Attribut definieren (Nur 1x pro Laufzeit!)
        AttDef = swapp.IDefineAttribute("Elektropneumatik")
        AttDef.AddParameter("Hash", swParamType_e.swParamTypeString, 0.0#, 0)
        AttDef.AddParameter("BMK", swParamType_e.swParamTypeString, 0.0#, 0)
        AttDef.AddParameter("Bezeichnung", swParamType_e.swParamTypeString, 0.0#, 0)

        'Attribut registrieren (Nur 1x pro Laufzeit!)
        AttDef.Register()

        Att = AttDef.CreateInstance5(modeldoc, icomp, "Elektropneumatik", 0, swInConfigurationOpts_e.swAllConfiguration)

        MsgBox(Att.IGetComponent2.GetPathName())
        modeldoc.EditRebuild3()




    End Sub
End Class
