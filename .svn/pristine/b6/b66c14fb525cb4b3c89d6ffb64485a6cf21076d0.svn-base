Imports Microsoft.Win32
Public Class TouchScreenKeyboard
    Public Value As Double
    Public Status As Boolean
    Private Sub ClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAll.Click
        TextValue.Text = 0
    End Sub

    Private Sub ClearChar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearChar.Click
        If TextValue.Text.Length > 1 Then
            TextValue.Text = TextValue.Text.Substring(0, TextValue.Text.Length() - 1)
        Else
            TextValue.Text = 0
        End If
    End Sub

    Private Sub Seven_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Seven.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 7
        Else
            TextValue.Text &= 7
        End If
    End Sub

    Private Sub Eight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Eight.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 8
        Else
            TextValue.Text &= 8
        End If

    End Sub

    Private Sub Nine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nine.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 9
        Else
            TextValue.Text &= 9
        End If

    End Sub

    Private Sub Four_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Four.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 4
        Else
            TextValue.Text &= 4
        End If

    End Sub

    Private Sub Five_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Five.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 5
        Else
            TextValue.Text &= 5
        End If

    End Sub

    Private Sub Six_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Six.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 6
        Else
            TextValue.Text &= 6
        End If

    End Sub

    Private Sub One_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles One.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 1
        Else
            TextValue.Text &= 1
        End If

    End Sub

    Private Sub Two_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Two.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 2
        Else
            TextValue.Text &= 2
        End If

    End Sub

    Private Sub Three_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Three.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 3
        Else
            TextValue.Text &= 3
        End If

    End Sub

    Private Sub Zero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Zero.Click
        If TextValue.Text = "0" Then
            TextValue.Text = 0
        Else
            TextValue.Text &= 0
        End If

    End Sub

    Private Sub Point_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Point.Click
        If TextValue.Text = "0" Then
            TextValue.Text = "0."
        ElseIf Not TextValue.Text.Contains(".") Then
            TextValue.Text &= "."
        End If

    End Sub

    Private Sub Plus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Plus.Click
        If TextValue.Text.StartsWith("-") Then
            TextValue.Text = TextValue.Text.Substring(1)
        ElseIf Not TextValue.Text.StartsWith("-") Then
            TextValue.Text = "-" & TextValue.Text
        End If
    End Sub

    Private Sub Minus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Minus.Click
        If TextValue.Text = "0" Then
            TextValue.Text = "-"
        ElseIf TextValue.Text.StartsWith("+") Then
            TextValue.Text = "-" & TextValue.Text.Substring(1)
        ElseIf Not TextValue.Text.StartsWith("-") Then
            TextValue.Text = "-" & TextValue.Text
        End If
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Status = True
        Value = CDbl(TextValue.Text)
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Status = False
        Value = 0
        Me.Close()
    End Sub
End Class