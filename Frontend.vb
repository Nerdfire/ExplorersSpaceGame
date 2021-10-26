Imports System.Threading
Module Frontend
    Dim galaxies As New List(Of GeneralSystem)
    Dim galaxyPointer As Integer = -1
    Dim systemComplete As Boolean = True
    'Read y axis then x axis for 2D array

    '# is any ship
    'Player is dark blue, enemies are red
    '& is asteroid (can't go through there)
    'O is planet

    Sub EndProg()
        Console.Clear()
        Console.WriteLine("Thank you for playing! :)")
        Thread.Sleep(500)
        Console.WriteLine("Press any key to exit")
        Console.ReadKey()
        Environment.Exit(0)
    End Sub


    Sub Navigation(ByVal playership As PlayerShip)
        Dim done As Boolean = False
        Do While done = False
            Console.Clear()
            If systemComplete = True Then
                Dim isPlanet As Integer = Int(Rnd() * 3) + 1
                If isPlanet <> 2 Then
                    Console.WriteLine(playership.GetShipName & " has entered a new system!")
                    Dim toAdd As New GeneralSystem
                    galaxyPointer += 1
                    galaxies.Add(toAdd)
                    systemComplete = False
                    Tools.UpdateGal(galaxies, galaxyPointer)
                    playership.UpdateGal(galaxies, galaxyPointer)
                    Thread.Sleep(1300)
                    Console.Clear()
                Else
                    Console.WriteLine(playership.GetShipName & " has entered a new planetary system!")
                    Dim toAdd As New PlanetSystem
                    galaxyPointer += 1
                    galaxies.Add(toAdd)
                    systemComplete = False
                    Tools.UpdateGal(galaxies, galaxyPointer)
                    playership.UpdateGal(galaxies, galaxyPointer)
                    Thread.Sleep(1300)
                    Console.Clear()
                End If
            End If
                Console.WriteLine("System name: " & galaxies(galaxyPointer).GetSystemName)
            Console.WriteLine("Items on map:
# = " & playership.GetCaptainName & "'s ship
& = Asteroid
@ = Wormhole to leave the system
O = Planet
")
            Dim rePrint As Boolean = False
            Do Until systemComplete = True OrElse done = True OrElse rePrint = True
                galaxies(galaxyPointer).DisplayMap()
                Console.WriteLine(vbCrLf & "
Up (W)
Down (S)
Left (A)
Right (D)
Cockpit View (C)

Exit (E)
")
                Console.WriteLine("Select your movement: ")
                Tools.ClearLine(0, 32)
                Console.ForegroundColor = ConsoleColor.Black
                Dim uChoice As String = Console.ReadKey.KeyChar.ToString.ToLower
                Console.ForegroundColor = ConsoleColor.Gray
                Select Case uChoice
                    Case "w" : playership.MoveShip("up")
                    Case "s" : playership.MoveShip("down")
                    Case "a" : playership.MoveShip("left")
                    Case "d" : playership.MoveShip("right")
                    Case "c"
                    Case "e" : done = True
                    Case Else : rePrint = True
                End Select
                If playership.rePrintAll = True Then
                    rePrint = True
                    playership.rePrintAll = False
                End If
                If galaxies(galaxyPointer).whCoords = playership.GetShipPosition() Then
                    Console.WriteLine("You have reached a wormhole of the system!")
                    Console.WriteLine("Teleporting to new system... ")
                    systemComplete = True
                End If
            Loop
        Loop
    End Sub





    Sub Main()
        Randomize()
        Console.WriteLine("Enter your name: ")
        Dim pName As String = Console.ReadLine()
        Console.WriteLine("Enter your ship name: ")
        Dim playerShip As New PlayerShip(pName, Console.ReadLine())
        Navigation(playerShip)
        EndProg()
        Console.ReadKey()
    End Sub

End Module
