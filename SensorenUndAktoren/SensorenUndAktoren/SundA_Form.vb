Imports System.Windows.Forms

Public Class SundA_Form

    Event SaveExit()
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


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button_Uebernehmen.Click
        RaiseEvent SaveExit()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_KompAnfuegen.Click
        RaiseEvent KomponenteEinfuegen(TreeView1.SelectedNode.Name)
    End Sub



    Private Sub TreeView1_Click(sender As Object, e As MouseEventArgs) Handles TreeView1.Click
        Dim Knoten As TreeNode


        Knoten = TreeView1.GetNodeAt(e.X, e.Y)
        If Knoten.Level > 0 Then
            Me.Focus_GUID = Knoten.Name
        End If

    End Sub
End Class