Imports System.Threading, System.IO
Public Class PlayerShip
    Inherits GeneralShip
    Public rePrintAll As Boolean = False
    Private captainName As String
    Public Sub New(ByVal captainNameIn As String, ByVal shipNameIn As String)
        MyBase.New(shipNameIn)
        captainName = captainNameIn
    End Sub

    Protected Overrides Function IsMovePossible(ByVal direction As String) As Boolean
        Dim shipOriginalPosition() As String = Tools.GetObjectPos("#", "PlayerShip").Split(",")
        Dim shipFirstDPosition As Integer = shipOriginalPosition(0)
        Dim shipSecondDPosition As Integer = shipOriginalPosition(1)
        Dim newShipFirstDposition As Integer = shipFirstDPosition
        Dim newShipSecondDPosition As Integer = shipSecondDPosition
        'Simulating ship movement
        If direction = "up" Then
            newShipFirstDposition -= 1
        ElseIf direction = "down" Then
            newShipFirstDposition += 1
        ElseIf direction = "left" Then
            newShipSecondDPosition -= 1
        ElseIf direction = "right" Then
            newShipSecondDPosition += 1
        End If
        'Will the ship go off the map?
        If newShipFirstDposition > 14 OrElse newShipSecondDPosition > 14 Then
            Return False
        ElseIf newShipFirstDposition < 0 OrElse newShipSecondDPosition < 0 Then
            Return False
        End If
        'Will the ship hit an obstacle?
        If Tools.GetObjectInSpace(newShipFirstDposition, newShipSecondDPosition) = "&" Then
            Return False
        ElseIf Tools.GetObjectInSpace(newShipFirstDposition, newShipSecondDPosition) = "#" Then
            Return True
        Else
            Return True
        End If
    End Function

    Public Overrides Sub MoveShip(ByVal direction As String)
        If IsMovePossible(direction) = True Then
            Dim shipOriginalPosition() As String = Tools.GetObjectPos("#", "PlayerShip").Split(",")
            Dim shipFirstDPosition As Integer = shipOriginalPosition(0)
            Dim shipSecondDPosition As Integer = shipOriginalPosition(1)
            Dim newShipFirstDposition As Integer = shipFirstDPosition
            Dim newShipSecondDPosition As Integer = shipSecondDPosition

            If direction = "up" Then
                newShipFirstDposition -= 1
            ElseIf direction = "down" Then
                newShipFirstDposition += 1
            ElseIf direction = "left" Then
                newShipSecondDPosition -= 1
            ElseIf direction = "right" Then
                newShipSecondDPosition += 1
            End If

            If Tools.GetObjectInSpace(newShipFirstDposition, newShipSecondDPosition) = "#" Then
                If Tools.GetAllInfoAboutTile(newShipFirstDposition, newShipSecondDPosition)(1) = "EnemyShip" Then
                    Dim enemyShipName As String = Tools.GetNameFromFile("ShipNames.txt")
                    Dim enemy As New EnemyShip(enemyShipName)
                    Dim fighting As New CombatMain(Me, enemy)
                    rePrintAll = True
                End If
            End If

            If galaxies(galaxyPointer).GetType Is GetType(PlanetSystem) Then
                If Tools.GetObjectInSpace(newShipFirstDposition, newShipSecondDPosition) = "O" Then
                    PlanetInteraction(galaxies(galaxyPointer))
                Else
                    galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 0) = "   "
                    galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 1) = "Space"
                    galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 0) = " # "
                    galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 1) = "PlayerShip"
                    UpdatePosition(newShipFirstDposition, newShipSecondDPosition)
                End If
            Else
                galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 0) = "   "
                galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 1) = "Space"
                galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 0) = " # "
                galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 1) = "PlayerShip"
                UpdatePosition(newShipFirstDposition, newShipSecondDPosition)
            End If

        Else
            Console.WriteLine(vbCrLf & "Move not possible - Press any key to continue")
            Console.ReadKey()
        End If

        Tools.ClearLine(0, 33)
    End Sub

    Private Sub PlanetInteraction(ByVal planet As PlanetSystem)
        Console.Clear()
        Console.WriteLine("
Planet " & planet.GetPlanetName & " reached!
".PadRight(20))
        Console.WriteLine("This is a " & planet.GetPlanetType & " planet")
        Thread.Sleep(780)
        Dim doneOnPlanet As Boolean = False
        Do While doneOnPlanet = False
            Console.Clear()
            Console.WriteLine(planet.GetPlanetName())
            Console.WriteLine("Type: " & planet.GetPlanetType() & vbCrLf & vbCrLf)
            Console.WriteLine("Options: ")
            Dim planetOptions As Dictionary(Of String, Boolean) = planet.GetPlanetOptions()
            Dim planetOptionSet As KeyValuePair(Of String, Boolean)
            Dim lineCount As Integer = 1
            For Each planetOptionSet In planetOptions
                If planetOptionSet.Value = True Then
                    Console.WriteLine(planetOptionSet.Key & " (" & Left(planetOptionSet.Key, 1) & ")")
                    lineCount += 1
                End If
            Next
            Console.WriteLine(vbCrLf & "Exit planet's orbit (E)")
            Console.ForegroundColor = ConsoleColor.Black
            Dim planetActionChoice As String = Console.ReadKey.KeyChar.ToString.ToLower
            Console.ForegroundColor = ConsoleColor.Gray
            Select Case planetActionChoice
                Case "l"
                    If planetOptions.Item("Land") = True Then : planet.Land() : End If
                Case "r"
                    If planetOptions.Item("Repair") = True Then : planet.Repair(Me) : End If
                Case "t"
                    If planetOptions.Item("Trade") = True Then : planet.Trade() : End If
                Case "m"
                    If planetOptions.Item("Missions") = True Then : planet.Missions() : End If
                Case "e"
                    Console.WriteLine(vbCrLf & "Exiting planet's orbit - press any key to continue")
                    Console.ReadKey()
                    doneOnPlanet = True
            End Select
            Console.Clear()
        Loop
        rePrintAll = True
    End Sub


    Public Function GetCaptainName() As String
        Return captainName
    End Function
End Class
