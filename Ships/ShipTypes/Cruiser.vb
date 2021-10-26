Public Class Cruiser
    Inherits GeneralShipType
    'HP = 340000
    '2 bow slots
    '3 main body slots
    '3 stern slots

    Public Sub New()
        MyBase.New(340000)
    End Sub

    Public Sub New(ByVal bowArmamentIn As Dictionary(Of String, Integer), ByVal mainBodyArmamentIn As Dictionary(Of String, Integer), ByVal sternArmamentIn As Dictionary(Of String, Integer))
        MyBase.New(bowArmamentIn, mainBodyArmamentIn, sternArmamentIn, 340000)
    End Sub


End Class
