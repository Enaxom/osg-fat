Public Module demarrage

    Public Sub Main()
        Dim inter As MC.Init_MC = New MC.Init_MC
        'If inter.Init("sql_CEA.xml", 1, 2) Then
        If inter.Init("sqlsslia.xml", 1, 2) Then
            If Not inter.execute() Then
                End
            End If
        End If
    End Sub


End Module