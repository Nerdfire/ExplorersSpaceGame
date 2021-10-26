Public Class GeneralShipType
    Protected hitPoints = 1
    Protected maxHitPoints = 1
    Protected bowArmament As New Dictionary(Of String, Integer)
    Protected mainBodyArmament As New Dictionary(Of String, Integer)
    Protected sternArmament As New Dictionary(Of String, Integer)
    Protected bowSlots As Integer = 0
    Protected mainBodySlots As Integer = 0
    Protected sternSlots As Integer = 0


    'Base
    Public Sub New(ByVal hitPointsIn As Integer)
        maxHitPoints = hitPointsIn
        hitPoints = maxHitPoints
    End Sub

    'Custom Ship
    Public Sub New(ByVal bowArmamentIn As Dictionary(Of String, Integer), ByVal mainBodyArmamentIn As Dictionary(Of String, Integer), ByVal sternArmamentIn As Dictionary(Of String, Integer), ByVal hitPointsIn As Integer)
        bowArmament = bowArmamentIn
        mainBodyArmament = mainBodyArmamentIn
        sternArmament = sternArmamentIn
        maxHitPoints = hitPointsIn
        hitPoints = maxHitPoints
    End Sub

    Private Protected Function CorrectAmountOfSlots(ByVal sectionSlotsToCheck As Dictionary(Of String, Integer), ByVal slots As Integer) As Boolean
        If sectionSlotsToCheck.Count > slots Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Sub AdjustHP(ByVal amount As Integer)
        If hitPoints > 0 Then 'stops recovery of health when below 0
            hitPoints += amount
        End If
        If hitPoints > maxHitPoints Then
            hitPoints = maxHitPoints
        End If
    End Sub
    Public Function GetHitPoints() As Integer
        Return hitPoints
    End Function
    Public Function GetMaxHitPoints() As Integer
        Return maxHitPoints
    End Function
    Public Function GetBowArmament() As Dictionary(Of String, Integer)
        Return bowArmament
    End Function
    Public Function GetMainBodyArmament() As Dictionary(Of String, Integer)
        Return mainBodyArmament
    End Function
    Public Function GetSternArmament() As Dictionary(Of String, Integer)
        Return sternArmament
    End Function
End Class
