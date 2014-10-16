Public Class SundA_Form

    Event SaveExit()
    Event KomponenteEinfuegen(Koppelung As String)
   
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RaiseEvent SaveExit()
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        RaiseEvent KomponenteEinfuegen(TreeView1.SelectedNode.Name)
    End Sub
End Class