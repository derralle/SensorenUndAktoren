Public Class SundA_Form

    Event SaveExit()
   
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RaiseEvent SaveExit()
    End Sub




End Class