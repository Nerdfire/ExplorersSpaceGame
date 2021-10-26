Imports System.Threading
Public Class CombatMain
    Dim player As PlayerShip
    Dim computer As GeneralShip 'Always taking computer as second ship - general ship because could be neutral
    Public Sub New(ByVal playerIn As PlayerShip, ByVal computerIn As GeneralShip)
        Console.Clear()
        player = playerIn
        computer = computerIn
        Console.WriteLine(player.GetCaptainName & "'s ship " & player.GetShipName & " is entering into combat with ship: " & computer.GetShipName)
        Thread.Sleep(1000)
        Console.Clear()
        Fight()
    End Sub

    Private Sub Fight()
        Do Until player.IsAlive() = False OrElse computer.IsAlive() = False
            Console.WriteLine(player.GetShipName() & "'s hit points: " & player.GetHitPoints())
            Console.WriteLine(computer.GetShipName() & "'s hit points: " & computer.GetHitPoints())
            Attack(player, computer)
            If computer.IsAlive() = True Then
                Console.WriteLine(player.GetShipName() & "'s hit points: " & player.GetHitPoints())
                Console.WriteLine(computer.GetShipName() & "'s hit points: " & computer.GetHitPoints())
                Attack(computer, player)
            End If
        Loop
        If player.IsAlive() = False Then
            Console.WriteLine("You are dead! - Press any key to continue")
            Console.ReadKey()
            Console.Clear()
            Console.WriteLine("Thank you for playing! :)")
            Thread.Sleep(500)
            Console.WriteLine("Press any key to exit")
            Console.ReadKey()
            Environment.Exit(0)
        Else
            Console.WriteLine(computer.GetShipName() & " has been destroyed, congratulations! - Press any key to continue")
            Console.ReadKey()
        End If
        Console.Clear()
    End Sub


    Private Sub Attack(ByVal attacker As GeneralShip, ByVal defender As GeneralShip)
        Console.WriteLine(vbCrLf & attacker.GetShipName & "'s combat turn!" & vbCrLf)
        Console.WriteLine(attacker.GetShipName & " can fire " & attacker.GetBowArmament().Count() & " weapons from its bow!")
        Console.WriteLine(attacker.GetShipName & " can fire " & attacker.GetMainBodyArmament().Count() & " weapons from its main body!")
        Console.WriteLine(attacker.GetShipName & " can fire " & attacker.GetSternArmament().Count() & " weapons from its stern!")
        Console.WriteLine(vbCrLf & "Armaments list for " & attacker.GetShipName & ":")
        Dim tempArmamentsPair As KeyValuePair(Of String, Integer)
        Dim bowArmaments As Dictionary(Of String, Integer) = attacker.GetBowArmament()
        Dim mainBodyArmaments As Dictionary(Of String, Integer) = attacker.GetMainBodyArmament()
        Dim sternArmaments As Dictionary(Of String, Integer) = attacker.GetSternArmament()
        Dim totalDamage As Integer = 0
        For Each tempArmamentsPair In bowArmaments
            Console.WriteLine(tempArmamentsPair.Key & " - Damage: " & tempArmamentsPair.Value)
            totalDamage += tempArmamentsPair.Value
        Next
        For Each tempArmamentsPair In mainBodyArmaments
            Console.WriteLine(tempArmamentsPair.Key & " - Damage: " & tempArmamentsPair.Value)
            totalDamage += tempArmamentsPair.Value
        Next
        For Each tempArmamentsPair In sternArmaments
            Console.WriteLine(tempArmamentsPair.Key & " - Damage: " & tempArmamentsPair.Value)
            totalDamage += tempArmamentsPair.Value
        Next
        Console.WriteLine(vbCrLf & totalDamage & " done to " & defender.GetShipName())
        defender.TakeDamage(totalDamage)
        Console.WriteLine(vbCrLf & "Press any key to continue")
        Console.ReadKey()
        Console.Clear()
    End Sub

End Class
