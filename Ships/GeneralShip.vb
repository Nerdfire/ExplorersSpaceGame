Public Class GeneralShip
    Protected shipName As String
    Protected shipPosition As String
    Protected galaxies As List(Of GeneralSystem)
    Protected galaxyPointer As Integer
    Protected shipType As GeneralShipType
    Public Sub New(ByVal shipNameIn As String)
        shipName = shipNameIn
        shipType = New Destroyer
    End Sub

    Public Sub UpdateGal(ByVal galaxiesIn As List(Of GeneralSystem), ByVal galaxyPointerIn As Integer)
        galaxies = galaxiesIn
        galaxyPointer = galaxyPointerIn
    End Sub

    Protected Sub UpdatePosition(ByVal firstD As Integer, ByVal secondD As Integer)
        shipPosition = firstD & "," & secondD
    End Sub

    Protected Overridable Function IsMovePossible(ByVal direction As String) As Boolean
        Dim shipOriginalPosition() As String = Tools.GetObjectPos("#", "GeneralShip").Split(",")
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
            Return False
        ElseIf Tools.GetObjectInSpace(newShipFirstDposition, newShipSecondDPosition) = "O" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Overridable Sub MoveShip(ByVal direction As String)
        If IsMovePossible(direction) = True Then
            Dim shipOriginalPosition() As String = Tools.GetObjectPos("#", "GeneralShip").Split(",")
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
            galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 0) = "   "
            galaxies(galaxyPointer).GetSystemMap(shipFirstDPosition, shipSecondDPosition, 1) = "Space"
            galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 0) = " # "
            galaxies(galaxyPointer).GetSystemMap(newShipFirstDposition, newShipSecondDPosition, 1) = "PlayerShip"
            UpdatePosition(newShipFirstDposition, newShipSecondDPosition)
        Else
            Console.WriteLine(vbCrLf & "Move not possible - Press any key to continue")
            Console.ReadKey()
        End If
        Tools.ClearLine(0, 33)
    End Sub

    Public Sub RepairDamage(ByVal damageToRepair As Integer)
        shipType.AdjustHP(damageToRepair)
    End Sub
    Public Sub TakeDamage(ByVal damageToTake As Integer)
        shipType.AdjustHP("-" & damageToTake)
    End Sub
    Public Function GetHitPoints()
        Return shipType.GetHitPoints()
    End Function
    Public Function GetMaxHitPoints() As Integer
        Return shipType.GetMaxHitPoints()
    End Function

    Public Function IsAlive()
        If shipType.GetHitPoints() <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetShipPosition() As String
        Return shipPosition
    End Function
    Public Function GetShipName() As String
        Return shipName
    End Function
    Public Function GetBowArmament() As Dictionary(Of String, Integer)
        Return shipType.GetBowArmament()
    End Function
    Public Function GetMainBodyArmament() As Dictionary(Of String, Integer)
        Return shipType.GetMainBodyArmament()
    End Function
    Public Function GetSternArmament() As Dictionary(Of String, Integer)
        Return shipType.GetSternArmament()
    End Function
End Class
