Public Class Destroyer
    Inherits GeneralShipType
    'HP = 200000
    '1 bow slot
    '2 main body slots
    '1 stern slot

    'Non-customised
    Public Sub New()
        MyBase.New(20000)
        bowArmament.Clear() : bowArmament.Add("Small laser", 1000)
        mainBodyArmament.Clear() : mainBodyArmament.Add("Small laser", 1000)
        mainBodyArmament.Add("Small laser2", 1000)
        sternArmament.Clear() : sternArmament.Add("Small laser", 1000)
    End Sub


    'Customised
    Public Sub New(ByVal bowArmamentIn As Dictionary(Of String, Integer), ByVal mainBodyArmamentIn As Dictionary(Of String, Integer), ByVal sternArmamentIn As Dictionary(Of String, Integer))
        MyBase.New(bowArmamentIn, mainBodyArmamentIn, sternArmamentIn, 200000)
    End Sub


End Class


