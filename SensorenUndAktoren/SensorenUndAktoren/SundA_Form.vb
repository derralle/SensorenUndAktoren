﻿Imports System.Windows.Forms

Public Class SundA_Form

    Event SaveExit()
    Event KnotenUebernehmen()
    Event KomponenteEinfuegen(Koppelung As String)
    Event NeuerFocus(GUID As String)

    Private _Focus_GUID As String
    Public Property Focus_GUID() As String
        Get
            Return _Focus_GUID
        End Get
        Set(ByVal value As String)
            _Focus_GUID = value
            RaiseEvent NeuerFocus(value)
        End Set
    End Property


    Private Sub Uebernehmen(sender As Object, e As EventArgs) Handles Button_Uebernehmen.Click
        RaiseEvent KnotenUebernehmen()
    End Sub


    Private Sub KomponenteAnfuegen(sender As Object, e As EventArgs) Handles Button_KompAnfuegen.Click
        If TreeView1.SelectedNode.Level = 0 Then
            RaiseEvent KomponenteEinfuegen("")
        Else
            RaiseEvent KomponenteEinfuegen(TreeView1.SelectedNode.Name)
        End If

    End Sub

    Private Sub SpeichernSchließen() Handles Button_SpeichernSchließen.Click
        RaiseEvent SaveExit()
    End Sub


    Private Sub TreeView1_Click(sender As Object, e As MouseEventArgs) Handles TreeView1.Click
        Dim Knoten As TreeNode
        Dim KnotenName As String


        Knoten = TreeView1.GetNodeAt(e.X, e.Y)
        If Knoten.Level > 0 Then
            KnotenName = Knoten.Name
            Me.Focus_GUID = KnotenName
        End If

    End Sub
End Class