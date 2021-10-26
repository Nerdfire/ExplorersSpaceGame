Imports System.IO
Public Class Tools
    Private Shared galaxies As List(Of GeneralSystem)
    Private Shared galaxyPointer As Integer


    Public Shared Sub UpdateGal(ByVal galaxiesIn As List(Of GeneralSystem), ByVal galaxyPointerIn As Integer)
        galaxies = galaxiesIn
        galaxyPointer = galaxyPointerIn
    End Sub


    Public Shared Function GetObjectInSpace(ByVal FirstD As Integer, ByVal SecondD As Integer) As String
        Dim contents As String = galaxies(galaxyPointer).GetSystemMap(FirstD, SecondD, 0)
        Return Mid(contents, 2, 1)
    End Function


    Public Shared Function GetAllInfoAboutTile(ByVal firstD As Integer, ByVal secondD As Integer) As String()
        Dim contents(1) As String
        For i = 0 To 1
            contents(i) = galaxies(galaxyPointer).GetSystemMap(firstD, secondD, i)
        Next
        Return contents
    End Function


    Public Shared Function GetObjectPos(ByVal searchingForKey As String, ByVal searchingForValue As String) As String
        For FirstD = 0 To 14
            For SecondD = 0 To 14
                If GetObjectInSpace(FirstD, SecondD) = searchingForKey Then
                    If galaxies(galaxyPointer).GetSystemMap(FirstD, SecondD, 1) = searchingForValue Then
                        Return FirstD & "," & SecondD
                    End If
                End If
            Next
        Next
        Return "NULL"
    End Function


    Public Shared Sub ClearLine(ByVal left As Integer, ByVal top As Integer)
        Console.SetCursorPosition(left, top)
        Console.Write(vbCr & " ")
        For i = 0 To Console.BufferWidth()
            Console.Write(" ")
        Next
        Console.SetCursorPosition(left, top)
    End Sub


    Public Shared Function GetNameFromFile(ByVal filePath As String) As String
        Dim temp() As String = File.ReadAllText(filePath).Split(" ")
        Dim lineCount As Integer = temp.Length
        Dim name As String
        Using reader As New StreamReader(filePath)
            Dim randomSelect As Integer = Int(Rnd() * lineCount) + 0
            Dim currentLineNum As Integer = 0
            Dim lineContents As String = ""
            Do Until currentLineNum = randomSelect
                lineContents = reader.ReadLine()
                currentLineNum += 1
            Loop
            name = lineContents
        End Using
        Return name
    End Function
End Class
