Imports System.IO
Public Class PlanetSystem
    Inherits GeneralSystem
    Private planetName As String
    Private planetType As String
    'Types allowed:
    'Desert, Ocean, Temperate, Tundra, Tropical
    'Planet weights:
    'Desert      Ocean       Temperate      Tundra   Tropical
    '1-16         17-25         26-69       70-86       87-100  
    Private planetOptions As New Dictionary(Of String, Boolean) From {{"Land", False}, {"Repair", False}, {"Trade", False}, {"Missions", False}}
    'Options allowed:
    'Land, Repair, Trade, Missions
    Public planetCoords As String
    Public Sub New()
        MyBase.New()
        Dim filePath As String = "PlanetNames.txt"
        Dim temp() As String = File.ReadAllText(filePath).Split(" ")
        Dim lineCount As Integer = temp.Length
        Using reader As New StreamReader(filePath)
            Dim randomSelect As Integer = Int(Rnd() * lineCount) + 0
            Dim currentLineNum As Integer = 0
            Dim lineContents As String = ""
            Do Until currentLineNum = randomSelect
                lineContents = reader.ReadLine()
                currentLineNum += 1
            Loop
            planetName = lineContents
        End Using
        Dim planetTypeChooser As Integer = Int(Rnd() * 100) + 1
        If planetTypeChooser >= 1 AndAlso planetTypeChooser <= 16 Then
            planetType = "Desert"
            planetOptions.Item("Land") = True
            planetOptions.Item("Repair") = True
        ElseIf planetTypeChooser >= 17 AndAlso planetTypeChooser <= 25 Then
            planetType = "Ocean"
            planetOptions.Item("Repair") = True
        ElseIf planetTypeChooser >= 26 AndAlso planetTypeChooser <= 69 Then
            planetType = "Temperate"
            planetOptions.Item("Land") = True
            planetOptions.Item("Repair") = True
            planetOptions.Item("Trade") = True
            planetOptions.Item("Missions") = True
        ElseIf planetTypeChooser >= 70 AndAlso planetTypeChooser <= 86 Then
            planetType = "Tundra"
            planetOptions.Item("Land") = True
            planetOptions.Item("Repair") = True
            planetOptions.Item("Trade") = True
        ElseIf planetTypeChooser >= 87 AndAlso planetTypeChooser <= 100 Then
            planetType = "Tropical"
            planetOptions.Item("Land") = True
            planetOptions.Item("True") = True
        Else
            Dim PlanetTypeNotSetException As New Exception
            Throw PlanetTypeNotSetException
        End If
    End Sub

    Protected Overrides Sub GenerateSystem()
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
        AddItem((Int(Rnd() * 6)), (Int(Rnd() * 6)), " # ", "PlayerShip")
        Dim whFirstD As Integer = Int(Rnd() * 8) + 2
        Dim whSecondD As Integer = Int(Rnd() * 8) + 2
        AddItem(whFirstD, whSecondD, " @ ", "Wormhole")
        whCoords = whFirstD & "," & whSecondD
        AddItem((Int(Rnd() * 11) + 3), (Int(Rnd() * 11) + 3), " # ", "EnemyShip")
        Dim plFirstD As Integer = (Int(Rnd() * 7) + 2)
        Dim plSecondD As Integer = (Int(Rnd() * 7) + 2)
        AddItem(plFirstD, plSecondD, " O ", "Planet")
        planetCoords = plFirstD & "," & plSecondD
    End Sub

    Public Overrides Sub DisplayMap()
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
                ElseIf Tools.GetObjectInSpace(firstD, secondD) = "O" Then
                    Console.ForegroundColor = ConsoleColor.Green
                End If
                Console.Write(systemMap(firstD, secondD, 0))
                Console.ForegroundColor = ConsoleColor.Gray
            Next
            Console.WriteLine()
        Next
    End Sub

    Public Sub Land()
        Console.WriteLine("Ship has landed")
        Console.WriteLine("Press any key to continue")
        Console.ReadKey()
    End Sub

    Public Sub Repair(ByVal ship As PlayerShip)
        Console.WriteLine("Cost: 200 credits to repair damage")
        Console.WriteLine("Repair damage? (Y/N)")
        Select Case Console.ReadLine().ToLower
            Case "y"
                ship.RepairDamage(ship.GetMaxHitPoints())
                Console.WriteLine("Ship is now repaired with " & ship.GetHitPoints() & " hit points")
            Case Else
                Console.WriteLine("Repair terminated")
        End Select

        Console.WriteLine("Press any key to continue")
        Console.ReadKey()
    End Sub

    Public Sub Trade()
        Console.WriteLine("Ship has traded")
        Console.WriteLine("Press any key to continue")
        Console.ReadKey()
    End Sub

    Public Sub Missions()
        Console.WriteLine("Ship has accepted a mission")
        Console.WriteLine("Press any key to continue")
        Console.ReadKey()
    End Sub

    Public Function GetPlanetName() As String
        Return planetName
    End Function
    Public Function GetPlanetType() As String
        Return planetType
    End Function
    Public Function GetPlanetOptions() As Dictionary(Of String, Boolean)
        Return planetOptions
    End Function
End Class
