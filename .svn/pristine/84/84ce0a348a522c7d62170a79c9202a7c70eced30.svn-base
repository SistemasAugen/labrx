Public Class InfoPastillas
    Public Partnum As String
    Public Material As String
    Public Diseño As String
    Public Esfera As Single
    Public Cilindro As Single
    Public Base As Single
    Public Adicion As Single
    Public Clase As String
    Public Lado As String
    Public Coated As Boolean
    Public AR As Boolean
    Public ProdCode As String
    Public IsSV As Boolean
    Public IsFT As Boolean
    Public IsPG As Boolean

    Sub GetInfo()
        Dim t As New SqlDB(My.Settings.LocalServer, My.Settings.DBUser, My.Settings.DBPassword, My.Settings.LocalDBName)
        Try
            t.OpenConn()
            Dim sqlstr As String = ""
            sqlstr = "select P.prodcode, P.shortchar07 as MaterialID, P.shortchar08 as DisenoID,P.shortchar10 as Lado, P.number01 as Esfera,P.number02 as Cilindro, P.shortchar09 as Clase, P.checkbox01 as Coated, P.checkbox02 as AR, " & _
            "D.IsSV, D.IsFT, D.IsPG " & _
            "FROM part P with(nolock) LEFT JOIN TblDesigns D ON P.shortchar08=D.DisenoID " & _
            "where P.company = 'TRECEUX' and P.partnum = '" & Me.Partnum & "'"
            Dim ds As DataSet = t.SQLDS(sqlstr, "t1")
            If ds.Tables("t1").Rows.Count > 0 Then
                With ds.Tables("t1").Rows(0)
                    Me.Esfera = CSng(.Item("Esfera"))
                    Me.Cilindro = CSng(.Item("Cilindro"))
                    Me.Base = CSng(.Item("Esfera"))
                    Me.Adicion = CSng(.Item("Cilindro"))
                    Me.Clase = .Item("Clase")
                    Me.Material = .Item("MaterialID")
                    Me.Diseño = .Item("DisenoID")
                    Me.Lado = .Item("Lado")
                    If Clase = "T" Then
                        Base = 0
                        If Lado = "N" Then
                            Adicion = 0
                        Else
                            Cilindro = 0
                        End If
                    Else
                        Esfera = 0
                        If Lado = "N" Then
                            Adicion = 0
                        Else
                            Cilindro = 0
                        End If
                    End If
                    Me.Coated = CBool(.Item("Coated"))
                    Me.AR = CBool(.Item("AR"))
                    Me.ProdCode = .Item("prodcode")
                    IsSV = .Item("IsSV")
                    IsFT = .Item("IsFT")
                    IsPG = .Item("IsPG")
                End With
            End If
        Catch ex As Exception
        Finally
            t.CloseConn()

        End Try

    End Sub

    Public Sub New(ByVal Partnum As String)
        Me.Partnum = Partnum
        Me.GetInfo()
    End Sub
End Class
