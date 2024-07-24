Sub InitializeAttributes()
    Dim ws As Worksheet
    Set ws = ThisWorkbook.Sheets("Sheet1") ' 사용 중인 시트 이름을 지정하세요.
    
    ' D컬럼의 유효한 값이 있는 마지막 행 번호를 찾습니다.
    Dim lastDRow As Long
    lastDRow = ws.Cells(ws.Rows.Count, "D").End(xlUp).Row
    
    ' 객체 준비
    ws.Cells(lastDRow + 1, 6).value = "data-gtm-body={}"
    ws.Cells(lastDRow + 2, 6).value = "data-gtm-body={}"
    ws.Cells(lastDRow + 3, 6).value = "data-gtm-body={}"
    ws.Cells(lastDRow + 4, 6).value = "data-gtm-body={}"
End Sub

Sub AddAttributes()
    Dim ws As Worksheet
    Set ws = ThisWorkbook.Sheets("Sheet1") ' 사용 중인 시트 이름을 지정하세요.
    
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
    Dim attributeEvent As String
    Dim attributeView As String
    
    ' 초기화
    Call InitializeAttributes
    
    ' 각 열을 순회하면서 데이터 수집 및 객체 초기화
    For col = 6 To lastCol
        attributePage = "data-ga-body={"
        attributeSection = "data-ga-body={"
        attributeEvent = "data-ga-body={"
        attributeView = "data-ga-body={"
        
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
                            attributeSection = attributeSection & vbCrLf & "    " & key & ": """ & value & """, "
                        End If
                    Case "data-gtm-event"
                        If value <> "" Then
                            attributeEvent = attributeEvent & vbCrLf & "    " & key & ": """ & value & """, "
                        End If
                End Select
            End If
        Next i
        
        ' 마지막 쉼표 제거 및 닫는 중괄호 추가
        If Right(attributePage, 2) = ", " Then attributePage = Left(attributePage, Len(attributePage) - 2)
        attributePage = attributePage & vbCrLf & "}"
        If Right(attributeSection, 2) = ", " Then attributeSection = Left(attributeSection, Len(attributeSection) - 2)
        attributeSection = attributeSection & vbCrLf & "}"
        If Right(attributeEvent, 2) = ", " Then attributeEvent = Left(attributeEvent, Len(attributeEvent) - 2)
        attributeEvent = attributeEvent & vbCrLf & "}"
        
        ' data-ga-view에 모든 객체 합치기
        attributeView = "data-gtm-body={"
        If Len(attributeSection) > 20 Then
            If Len(attributeView) > 14 Then
                attributeView = attributeView & ","
            End If
            attributeView = attributeView & vbCrLf & Mid(attributeSection, 19, Len(attributeSection) - 19 - 1)
        End If
        If Len(attributeEvent) > 17 Then
            If Len(attributeView) > 14 Then
                attributeView = attributeView & ","
            End If
            attributeView = attributeView & vbCrLf & Mid(attributeEvent, 16, Len(attributeEvent) - 16 - 1)
        End If
        attributeView = attributeView & vbCrLf & "}"
        
        ' 디버깅 메시지 추가
        Debug.Print "attributePage: " & attributePage
        Debug.Print "attributeSection: " & attributeSection
        Debug.Print "attributeEvent: " & attributeEvent
        Debug.Print "attributeView: " & attributeView
        
        ' 결과를 해당 열의 마지막 행 아래에 추가
        If ws.Cells(4, col).value = "노출" Then
            ws.Cells(lastDRow + 1, col).value = attributePage
            ws.Cells(lastDRow + 1, col).Interior.Color = RGB(128, 128, 128)
            ws.Cells(lastDRow + 2, col).value = attributeSection
            ws.Cells(lastDRow + 2, col).Interior.Color = RGB(128, 128, 128)
            ws.Cells(lastDRow + 3, col).value = attributeEvent
            ws.Cells(lastDRow + 3, col).Interior.Color = RGB(128, 128, 128)
            ws.Cells(lastDRow + 4, col).value = attributeView
        Else
            ws.Cells(lastDRow + 1, col).value = attributePage
            ws.Cells(lastDRow + 2, col).value = attributeSection
            ws.Cells(lastDRow + 3, col).value = attributeEvent
        End If
    Next col
End Sub

 