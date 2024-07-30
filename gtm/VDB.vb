Sub InitializeAttributes()
    Dim ws As Worksheet
    Set ws = ThisWorkbook.Sheets("5_정기결제(완료)") ' 사용 중인 시트 이름을 지정하세요.
    
    ' D컬럼의 유효한 값이 있는 마지막 행 번호를 찾습니다.
    Dim lastDRow As Long
    lastDRow = ws.Cells(ws.Rows.Count, "D").End(xlUp).Row
    
    ' 4행의 유효한 값이 있는 마지막 열 번호를 찾습니다.
    Dim last4Col As Long
    last4Col = ws.Cells(4, ws.Columns.Count).End(xlToLeft).Column
    
    ' 객체 준비
    Dim col As Long
    For col = 6 To last4Col
        ws.Cells(lastDRow + 1, col).value = "data-gtm-body={}"
        ws.Cells(lastDRow + 2, col).value = "data-gtm-body={}"
        ws.Cells(lastDRow + 3, col).value = "data-gtm-body={}"
        ws.Cells(lastDRow + 4, col).value = "data-gtm-body={}"
    Next col
End Sub

Sub AddAttributes()
    Dim ws As Worksheet
    Set ws = ThisWorkbook.Sheets("5_정기결제(완료)") ' 사용 중인 시트 이름을 지정하세요.
    
    Dim lastRow As Long
    Dim lastDRow As Long
    Dim lastCol As Long
    lastRow = ws.Cells(ws.Rows.Count, "C").End(xlUp).Row
    lastDRow = ws.Cells(ws.Rows.Count, "D").End(xlUp).Row
    lastCol = ws.Cells(4, ws.Columns.Count).End(xlToLeft).Column
    
    Dim i As Long, col As Long
    Dim key As String
    Dim value As String
    Dim attributePage As String
    Dim attributeSection As String
    Dim attributeClick As String
    Dim attributeVisibility As String
    Dim sectionLabels As Collection
    Dim labelStr As String
    
    ' 초기화
    Call InitializeAttributes
    
    ' 각 열을 순회하면서 데이터 수집 및 객체 초기화
    For col = 6 To lastCol
        attributePage = "data-gtm-body={"
        attributeSection = "data-gtm-body={"
        attributeClick = "data-gtm-body={"
        attributeVisibility = "data-gtm-body={"
        
        Set sectionLabels = New Collection
        
        ' 행을 순회하면서 값 추가
        For i = 2 To lastRow ' Assuming the first row is the header
            If ws.Cells(i, 4).value <> "" And ws.Cells(i, col).value <> "" Then
                key = ws.Cells(i, 4).value
                value = ws.Cells(i, col).value
                
                Select Case ws.Cells(i, 3).value
                    Case "data-gtm-page"
                        If value <> "" Then
                            attributePage = attributePage & vbCrLf & "    " & key & ": """ & value & """, "
                        End If
                    Case "data-gtm-section"
                        If value <> "" Then
                            sectionLabels.Add value
                        End If
                    Case "data-gtm-event"
                        If value <> "" Then
                            If ws.Cells(4, col).value = "클릭" Then
                                attributeClick = attributeClick & vbCrLf & "    " & key & ": """ & value & """, "
                            ElseIf ws.Cells(4, col).value = "노출" Then
                                attributeVisibility = attributeVisibility & vbCrLf & "    " & key & ": """ & value & """, "
                            End If
                        End If
                End Select
            End If
        Next i
        
        ' 여러 개의 label 값을 ">"로 결합하여 하나의 label 키로만 남김
        If sectionLabels.Count > 0 Then
            labelStr = ""
            For Each Label In sectionLabels
                labelStr = labelStr & Label & ">"
            Next Label
            If Right(labelStr, 1) = ">" Then
                labelStr = Left(labelStr, Len(labelStr) - 1)
            End If
            attributeSection = attributeSection & vbCrLf & "    label: """ & labelStr & """, "
        End If
        
        ' 마지막 쉼표 제거 및 닫는 중괄호 추가
        If Right(attributePage, 2) = ", " Then attributePage = Left(attributePage, Len(attributePage) - 2)
        attributePage = attributePage & vbCrLf & "}"
        If Right(attributeSection, 2) = ", " Then attributeSection = Left(attributeSection, Len(attributeSection) - 2)
        attributeSection = attributeSection & vbCrLf & "}"
        If Right(attributeClick, 2) = ", " Then attributeClick = Left(attributeClick, Len(attributeClick) - 2)
        attributeClick = attributeClick & vbCrLf & "}"
        If Right(attributeVisibility, 2) = ", " Then attributeVisibility = Left(attributeVisibility, Len(attributeVisibility) - 2)
        attributeVisibility = attributeVisibility & vbCrLf & "}"
        
        ' 디버깅 메시지 추가
        Debug.Print "attributePage: " & attributePage
        Debug.Print "attributeSection: " & attributeSection
        Debug.Print "attributeClick: " & attributeClick
        Debug.Print "attributeVisibility: " & attributeVisibility
        
        ' 결과를 해당 열의 마지막 행 아래에 추가
        If ws.Cells(4, col).value = "클릭" Then
            ws.Cells(lastDRow + 1, col).value = attributePage
            ws.Cells(lastDRow + 2, col).value = attributeSection
            ws.Cells(lastDRow + 3, col).value = attributeClick
            If InStr(attributePage, ":") > 0 Then ws.Cells(lastDRow + 1, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeSection, ":") > 0 Then ws.Cells(lastDRow + 2, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeClick, ":") > 0 Then ws.Cells(lastDRow + 3, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
        ElseIf ws.Cells(4, col).value = "노출" Then
            ws.Cells(lastDRow + 1, col).value = attributePage
            ws.Cells(lastDRow + 2, col).value = attributeSection
            ws.Cells(lastDRow + 4, col).value = attributeVisibility
            If InStr(attributePage, ":") > 0 Then ws.Cells(lastDRow + 1, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeSection, ":") > 0 Then ws.Cells(lastDRow + 2, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeVisibility, ":") > 0 Then ws.Cells(lastDRow + 4, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
        Else
            ws.Cells(lastDRow + 1, col).value = attributePage
            ws.Cells(lastDRow + 2, col).value = attributeSection
            ws.Cells(lastDRow + 3, col).value = attributeClick
            ws.Cells(lastDRow + 4, col).value = attributeVisibility
            If InStr(attributePage, ":") > 0 Then ws.Cells(lastDRow + 1, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeSection, ":") > 0 Then ws.Cells(lastDRow + 2, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeClick, ":") > 0 Then ws.Cells(lastDRow + 3, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
            If InStr(attributeVisibility, ":") > 0 Then ws.Cells(lastDRow + 4, col).Interior.Color = RGB(144, 238, 144) ' 연한 초록색
        End If
    Next col
End Sub