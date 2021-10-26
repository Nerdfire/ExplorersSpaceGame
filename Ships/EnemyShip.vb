Public Class EnemyShip
    Inherits GeneralShip
    Sub New(ByVal shipNameIn As String)
        MyBase.New(shipNameIn)
        shipType = New Destroyer
    End Sub
End Class
