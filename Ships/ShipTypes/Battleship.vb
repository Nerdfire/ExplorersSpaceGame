Public Class Battleship
    Inherits GeneralShipType
    'HP = 500000
    '4 bow slots
    '5 main body slots
    '4 stern slots

    Public Sub New()
        MyBase.New(500000)
    End Sub

    Public Sub New(ByVal bowArmamentIn As Dictionary(Of String, Integer), ByVal mainBodyArmamentIn As Dictionary(Of String, Integer), ByVal sternArmamentIn As Dictionary(Of String, Integer))
        MyBase.New(bowArmamentIn, mainBodyArmamentIn, sternArmamentIn, 500000)
    End Sub


End Class
