Module OLSDATE
    Friend TypeDate As String
    Friend TypeHeure As String

    Friend Sub Parametrer_DateEdit(ByVal dateEdit As DevExpress.XtraEditors.DateEdit)
        If (UCase(TypeDate).Equals("MM/DD/YYYY")) Then
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            dateEdit.Properties.Mask.EditMask = "(0?[1-9]|1[012])/([012]?[1-9]|[123]0|31)/([123][0-9])?[0-9][0-9]"


            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            dateEdit.Properties.DisplayFormat.FormatString = "MM/dd/yyyy"

            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            dateEdit.Properties.EditFormat.FormatString = "MM/dd/yyyy"
        Else
            'AJOUT DU 190 CAR POLICE RAPPEL PLUS GRANDE QUE STANDARD
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
            dateEdit.Properties.Mask.EditMask = "d"
        End If
    End Sub

    Friend Sub Parametrer_TimeEdit(ByVal TimeEdit As DevExpress.XtraEditors.TextEdit)
        If ((TypeHeure).Equals("24")) Then
            TimeEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            TimeEdit.Properties.Mask.EditMask = "(0?\d|1\d|2[0-3])\:[0-5]\d"

        Else
            'AJOUT DU 190 CAR POLICE RAPPEL PLUS GRANDE QUE STANDARD
            TimeEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
            TimeEdit.Properties.Mask.EditMask = "(0?[1-9]|1[012]):[0-5]\d(AM|PM)"
        End If
    End Sub

    Friend Function DateToLong(ByVal _date As Date) As Long
        Return _date.ToOADate
    End Function

    Friend Function timeToLong(ByVal dat As Date, ByVal time As String) As Double
        Dim datetime As New Date(dat.Year, dat.Month, dat.Day)
        datetime = datetime.AddHours(CInt(time.Split(":")(0)))
        datetime = datetime.AddMinutes(CInt(time.Split(":")(1)))

        Return datetime.ToOADate
    End Function

    Friend Function DateToLong(ByVal _date As Date, ByVal _heure As String) As Long
        Return _date.ToOADate + (CDate(_heure)).ToOADate
    End Function

    Friend Function LongTodate(ByVal _date As Double) As Date
        Return Date.FromOADate(_date).ToShortDateString
    End Function

    Friend Function LongToTime(ByVal _date As Double) As Date
        Return Date.FromOADate(_date - Fix(_date)).ToShortTimeString
    End Function


    Friend Function LongToDateText(ByVal _date As Date) As String

        Dim Month As String
        Dim Year As String
        Dim Day As String

        If _date.Day < 10 Then
            Day = "0" & _date.Day
        Else
            Day = _date.Day
        End If

        If _date.Month < 10 Then
            Month = "0" & _date.Month
        Else
            Month = _date.Month
        End If

        If _date.Year < 10 Then
            Year = "0" & _date.Year
        Else
            Year = _date.Year
        End If


        If (UCase(TypeDate).Equals("MM/DD/YYYY")) Then
            Return Month & "/" & Day & "/" & Year
        Else
            Return Day & "/" & Month & "/" & Year
        End If
    End Function

    Friend Function TimeToTimeText(ByVal _date As Date) As String

        Dim time As String
        Dim Minute As String
        Dim Heure As String

        If _date.Minute < 10 Then
            Minute = "0" & _date.Minute
        Else
            Minute = _date.Minute
        End If

        If _date.Hour < 10 Then
            Heure = "0" & _date.Hour
        Else
            Heure = _date.Hour
        End If

        If (UCase(TypeHeure).Equals("24")) Then
            time = Heure & ":" & Minute
        Else
            If (Heure > 12) Then
                Heure = _date.Hour - 12
                If CInt(Heure) < 10 Then
                    Heure = "0" & Heure
                Else
                    Heure = Heure
                End If

                time = Heure & ":" & Minute & " PM"
            ElseIf Heure = 0 Then
                time = "12" & ":" & Minute & " AM"
            ElseIf (Heure = 12) Then
                time = "12" & ":" & Minute & " PM"
            Else
                time = Heure & ":" & Minute & " AM"
            End If
        End If
        Return time
    End Function

    Friend Function Time12ToTime24(ByVal _date As String) As String

        Dim time As String

        Dim RegexPM As New Text.RegularExpressions.Regex("(0?[1-9]|1[012]):[0-5]\d(PM)")
        Dim Regex12AM As New Text.RegularExpressions.Regex("(12):[0-5]\d(AM)")
        Dim Regex12PM As New Text.RegularExpressions.Regex("(12):[0-5]\d(PM)")
        Dim RegexAM As New Text.RegularExpressions.Regex("(0?[1-9]|1[012]):[0-5]\d(AM)")


        If Regex12AM.IsMatch(_date) Then
            time = "00" & ":" & _date.Replace("AM", "").Split(":")(1)
        ElseIf (Regex12PM.IsMatch(_date)) Then
            time = "12" & ":" & _date.Replace("PM", "").Split(":")(1)
        ElseIf RegexAM.IsMatch(_date) Then
            time = _date.Replace("AM", "")
        ElseIf RegexPM.IsMatch(_date) Then
            time = (CInt(_date.Replace("PM", "").Split(":")(0)) + 12) & ":" & _date.Replace("PM", "").Split(":")(1)
        Else
            time = _date
        End If

        Return time
    End Function

End Module

