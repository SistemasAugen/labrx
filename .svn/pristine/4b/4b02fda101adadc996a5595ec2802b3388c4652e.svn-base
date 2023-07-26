Imports System.Windows.Forms
Imports System.Resources

Public Class ErrorMessage
    Private _text As String
    Private _caption As String
    Private _buttons As ErrorMessageButtons
    Private _icon As ErrorMessageIcon
    Private _dialogResult As DialogResult
    Private _point1 As Point
    Private _point2 As Point
    Private _point3 As Point
    'Dim RM As New ResourceManager("Laboratorios.Resources", Me.GetType.Assembly)

    Enum ErrorMessageButtons
        'AbortRetryIgnore
        OK
        OKCancel
        RetryCancel
        YesNo
        YesNoCancel
    End Enum
    Enum ErrorMessageIcon
        Asterisk
        [Error]
        Exclamation
        Hand
        Information
        None
        Question
        [Stop]
        Warning
    End Enum
    Public Function GetDialogResult() As DialogResult
        Return _dialogResult
    End Function

    Private Sub BTN_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Cancel.Click
        _dialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BTN_Yes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Yes.Click
        _dialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub BTN_No_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_No.Click
        _dialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub BTN_Retry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Retry.Click
        _dialogResult = Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub BTN_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_Ok.Click
        _dialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub SetImage(ByVal MyImage As Bitmap)
        PBX_ErrorImage.Image = MyImage
    End Sub
    Public Sub SetErrorText(ByVal MyError As String)
        TBX_Error.Text = MyError
        TBX_Error.Select(0, 0)
    End Sub

    Public Overloads Function ShowDialog(ByVal text As String, ByVal caption As String, ByVal icon As ErrorMessageIcon, ByVal buttons As ErrorMessageButtons) As DialogResult
        _text = text
        _caption = caption
        _buttons = buttons
        _icon = icon

        ShowError()
        Return _dialogResult
    End Function
    Public Overloads Function ShowDialog(ByVal text As String, ByVal icon As ErrorMessageIcon, ByVal buttons As ErrorMessageButtons) As DialogResult
        _text = text
        _caption = My.Application.Info.ProductName
        _buttons = buttons
        _icon = icon

        ShowError()
        Return _dialogResult
    End Function

    Public Overloads Function ShowDialog(ByVal text As String, ByVal buttons As ErrorMessageButtons) As DialogResult
        _text = text
        _caption = My.Application.Info.ProductName
        _buttons = buttons
        _icon = ErrorMessageIcon.Error

        ShowError()
        Return _dialogResult
    End Function

    Private Sub HideButtons()
        BTN_Cancel.Visible = False
        BTN_No.Visible = False
        BTN_Ok.Visible = False
        BTN_Retry.Visible = False
        BTN_Yes.Visible = False
    End Sub

    Private Function ShowError() As DialogResult
        Dim x As New ErrorMessage

        x.HideButtons()
        x.SetErrorText(_text)
        x.Text = _caption

        Select Case _icon
            Case ErrorMessageIcon.Error
                x.SetImage(My.Resources.ico_error)

            Case ErrorMessageIcon.Exclamation
                x.SetImage(My.Resources.Exclamation)

            Case ErrorMessageIcon.Information
                x.SetImage(My.Resources.info)

            Case ErrorMessageIcon.Question
                x.SetImage(My.Resources.help48)

            Case Else
                x.SetImage(My.Resources.ico_error)
        End Select

        Select Case _buttons
            Case ErrorMessageButtons.OK
                x.BTN_Ok.Visible = True
                x.BTN_Ok.Location = _point3
                x.BTN_Ok.Highlighted = True

            Case ErrorMessageButtons.OKCancel
                x.BTN_Ok.Visible = True
                x.BTN_Cancel.Visible = True
                x.BTN_Ok.Location = _point2
                x.BTN_Cancel.Location = _point3
                x.BTN_Ok.Highlighted = True

            Case ErrorMessageButtons.RetryCancel
                x.BTN_Retry.Visible = True
                x.BTN_Cancel.Visible = True
                x.BTN_Retry.Location = _point2
                x.BTN_Cancel.Location = _point3
                x.BTN_Retry.Highlighted = True

            Case ErrorMessageButtons.YesNo
                x.BTN_Yes.Visible = True
                x.BTN_No.Visible = True
                x.BTN_Yes.Location = _point2
                x.BTN_No.Location = _point3
                x.BTN_Yes.Highlighted = True

            Case ErrorMessageButtons.YesNoCancel
                x.BTN_Yes.Visible = True
                x.BTN_No.Visible = True
                x.BTN_Cancel.Visible = True
                x.BTN_Yes.Location = _point1
                x.BTN_No.Location = _point2
                x.BTN_Cancel.Location = _point3
                x.BTN_Yes.Highlighted = True

            Case Else
                x.BTN_Ok.Visible = True
                x.BTN_Ok.Highlighted = True

        End Select

        x.ShowDialog()
        _dialogResult = x.GetDialogResult
        Return _dialogResult
    End Function

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _point1 = New Point(206, 142)
        _point2 = New Point(317, 142)
        _point3 = New Point(428, 142)

        BTN_Cancel.Texto = "Cancelar"
        BTN_No.Texto = "No"
        BTN_Ok.Texto = "Ok"
        BTN_Retry.Texto = "Reintentar"
        BTN_Yes.Texto = "Si"

        HideButtons()

        BTN_Cancel.Size = New Size(105, 44)
        BTN_No.Size = New Size(105, 44)
        BTN_Ok.Size = New Size(105, 44)
        BTN_Retry.Size = New Size(105, 44)
        BTN_Yes.Size = New Size(105, 44)

        Me.Size = New Size(554, 229)
        PBX_ErrorImage.Size = New Size(70, 56)
        PBX_ErrorImage.Location = New Point(6, 12)
        PBX_ErrorImage.SizeMode = PictureBoxSizeMode.CenterImage

    End Sub



End Class
