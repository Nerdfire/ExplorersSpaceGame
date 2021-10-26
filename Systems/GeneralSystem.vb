Public Class GeneralSystem
    Protected systemName As String
    Protected systemMap(14, 14, 1) As String 'First 2 Dimensions are coords, 3rd dimension is information about what is at first two dimensions
    'Values allowed:
    'Asteroid, Space, PlayerShip, Wormhole, EnemyShip, NeutralShip, Planet
    Public whCoords As String
    Public Sub New()
        systemName = Tools.GetNameFromFile("SpaceSystemNames.txt")
        GenerateSystem()
    End Sub

    Protected Overridable Sub GenerateSystem()
        For firstD = 0 To 14
            For secondD = 0 To 14
                Dim firstDychoice As Integer = Int(Rnd() * 13) + 0
                If firstDychoice = 6 Then
                    AddItem(firstD, secondD, " & ", "Asteroid")
                Else
                    AddItem(firstD, secondD, "   ", "Space")
                End If
            Next
        Next
        Dim whFirstD As Integer = Int(Rnd() * 8) + 2
        Dim whSecondD As Integer = Int(Rnd() * 8) + 2
        AddItem(whFirstD, whSecondD, " @ ", "Wormhole")
        whCoords = whFirstD & "," & whSecondD
        AddItem((Int(Rnd() * 11) + 3), (Int(Rnd() * 11) + 3), " # ", "EnemyShip")
        AddItem((Int(Rnd() * 6)), (Int(Rnd() * 6)), " # ", "PlayerShip")
    End Sub

    Protected Sub AddItem(ByVal firstD As String, ByVal secondD As String, ByVal key As String, ByVal value As String)
        systemMap(firstD, secondD, 0) = key
        systemMap(firstD, secondD, 1) = value
    End Sub


    Public Overridable Sub DisplayMap()
        Console.SetCursorPosition(0, 5)
        For firstD = 0 To 14
            For secondD = 0 To 14
                If Tools.GetObjectInSpace(firstD, secondD) = "@" Then
                    Console.ForegroundColor = ConsoleColor.Cyan
                ElseIf Tools.GetObjectInSpace(firstD, secondD) = "&" Then
                    Console.ForegroundColor = ConsoleColor.DarkYellow
                ElseIf Tools.GetObjectInSpace(firstD, secondD) = "#" Then
                    If Tools.GetAllInfoAboutTile(firstD, secondD)(1) = "PlayerShip" Then
                        Console.ForegroundColor = ConsoleColor.DarkBlue
                    ElseIf Tools.GetAllInfoAboutTile(firstD, secondD)(1) = "EnemyShip" Then
                        Console.ForegroundColor = ConsoleColor.Red
                    ElseIf Tools.GetAllInfoAboutTile(firstD, secondD)(1) = "NeutralShip" Then
                        Console.ForegroundColor = ConsoleColor.Yellow
                    End If
                End If
                Console.Write(systemMap(firstD, secondD, 0))
                Console.ForegroundColor = ConsoleColor.Gray
            Next
            Console.WriteLine()
        Next
    End Sub


    Public Function GetSystemName() As String
        Return systemName
    End Function
    Public Function GetSystemMap() As String(,,)
        Return systemMap
    End Function
End Class
