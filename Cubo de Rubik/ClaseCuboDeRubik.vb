Option Explicit On
Option Strict On

Imports Cubo_de_Rubik.Utilidades

Public Class ClaseCuboDeRubik
    Public MatrizDeCuboDeRubik(5) As Integer
    Private ParesFijos(11) As Boolean
    Private ParesObjetivo(11) As Boolean
    Private EsquinasFijas(11) As Boolean
    Private EsquinasObjetivo(11) As Boolean
    Public ListaDeMovimientosNecesarios As String
    Public MatrizInicial(5) As Integer
    Public ListaDeMovimientos() As Integer 'Ésta es la matriz en la que nos vamos a basar

    Public MatrizDeRepeticionDeMovimientos() As Integer


    Public Sub New(MatrizAuxiliarDeCuboDeRubik() As Integer)
        Dim Contador As Integer
        For Contador = 0 To 5
            MatrizDeCuboDeRubik(Contador) = MatrizAuxiliarDeCuboDeRubik(Contador)
            MatrizInicial(Contador) = MatrizAuxiliarDeCuboDeRubik(Contador)
        Next
        ListaDeMovimientosNecesarios = vbNullString
    End Sub

    Public Sub New(MatrizAuxiliarDeCuboDeRubik() As Integer, ListaDeMovimientosHastaAhora As String)
        Dim Contador As Integer
        For Contador = 0 To 5
            MatrizDeCuboDeRubik(Contador) = MatrizAuxiliarDeCuboDeRubik(Contador)
            MatrizInicial(Contador) = MatrizAuxiliarDeCuboDeRubik(Contador)
        Next
        ListaDeMovimientosNecesarios = ListaDeMovimientosHastaAhora
    End Sub

    Public Sub New()

    End Sub


    Public Function EstaMontadaLaCara(NumeroDeCara As Integer) As Boolean
        Select Case NumeroDeCara
            Case 0 To 5
                Dim Contador As Integer
                For Contador = 0 To 8
                    If ColorDeLaCasilla(Contador, MatrizDeCuboDeRubik(NumeroDeCara)) <> NumeroDeCara Then Return False
                Next
                Return True
            Case Else
                SalimosConError(22) : Stop
        End Select
    End Function

    Public Sub ArribaGirarIzquierda()
        AnnadirElementoAMatriz(0, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(4)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(0) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(0)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(1) = 0
        MatrizDeSumandos(2) = ColorDeLaCasilla(2, MatrizDeCuboDeRubik(0)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(2) -= ColorDeLaCasilla(0, MatrizDeCuboDeRubik(2)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(3) = ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(3) -= MatrizDeCuboDeRubik(3)
        MatrizDeSumandos(4) = CType(6 ^ 2, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(5)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(5))
        MatrizDeSumandos(4) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(4)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(5) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(2)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(5) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(5)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(5))
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub ArribaGirarIzquierda(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 1
                Select Case CaraFrontal
                    Case 0, 2, 4, 5 : ArribaGirarIzquierda()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 3
                Select Case CaraFrontal
                    Case 0, 2, 4, 5 : AbajoGirarDerecha()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 0, 2, 4, 5
                If CaraFrontal = CaraInferior Or CaraFrontal = CaraOpuesta(CaraInferior) Then SalimosConError(21) : Stop
                Select Case 6 * CaraInferior + CaraFrontal
                    Case 0, 5, 2 * 6 + 2, 2 * 6 + 4, 4 * 6 + 2, 4 * 6 + 4, 5 * 6 + 0, 5 * 6 + 5 : SalimosConError(22) : Stop
                    Case 1 To 4 : AtrasRotarIzquierda()
                    Case 2 * 6 + 0, 2 * 6 + 1, 2 * 6 + 3, 2 * 6 + 5 : DerechaGirarArriba()
                    Case 4 * 6 + 0, 4 * 6 + 1, 4 * 6 + 3, 4 * 6 + 5 : IzquierdaGirarAbajo()
                    Case 5 * 6 + 1 To 5 * 6 + 4 : AlanteRotarDerecha()
                    Case Else
                        Select Case 6 * CaraInferior + CaraFrontal
                            Case 0 To 35 : SalimosConError(22) : Stop
                            Case Is > 35 : SalimosConError(5) : Stop
                        End Select
                End Select
            Case Else
                SalimosConError(5) : Stop
        End Select
    End Sub

    Public Sub ArribaGirarDerecha()
        ArribaGirarIzquierda() : ArribaGirarIzquierda() : ArribaGirarIzquierda()
    End Sub

    Public Sub ArribaGirarDerecha(CaraFrontal%, CaraInferior%)
        ArribaGirarIzquierda(CaraFrontal, CaraInferior) : ArribaGirarIzquierda(CaraFrontal, CaraInferior) : ArribaGirarIzquierda(CaraFrontal, CaraInferior)
    End Sub

    Public Sub AbajoGirarIzquierda()
        AnnadirElementoAMatriz(2, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(4)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(0) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(0)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(1) += ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaIzquierda(MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(1) -= MatrizDeCuboDeRubik(1)
        MatrizDeSumandos(2) += CType(6 ^ 2, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(0)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(2) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(2)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(3) += 0
        MatrizDeSumandos(4) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(5)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(5))
        MatrizDeSumandos(4) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(4)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(5) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(2)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(5) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(5)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(5))
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub AbajoGirarIzquierda(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 1
                Select Case CaraFrontal
                    Case 0, 2, 4, 5 : AbajoGirarIzquierda()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 3
                Select Case CaraFrontal
                    Case 0, 2, 4, 5 : ArribaGirarDerecha()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 0, 2, 4, 5
                If CaraInferior = CaraFrontal Or CaraOpuesta(CaraInferior) = CaraFrontal Then SalimosConError(21)
                Select Case 6 * CaraInferior + CaraFrontal
                    Case 0, 5 : SalimosConError(22) : Stop
                    Case 1 To 4 : AlanteRotarIzquierda()

                    Case 2 * 6 + 0, 2 * 6 + 1, 2 * 6 + 3, 2 * 6 + 5 : IzquierdaGirarArriba()
                    Case 2 * 6 + 2, 2 * 6 + 4 : SalimosConError(22) : Stop

                    Case 4 * 6 + 0, 4 * 6 + 1, 4 * 6 + 3, 4 * 6 + 5 : DerechaGirarAbajo()
                    Case 4 * 6 + 2, 4 * 6 + 4 : SalimosConError(22) : Stop

                    Case 5 + 6 * 0, 5 * 6 + 5 : SalimosConError(22) : Stop
                    Case 5 * 6 + 1, 5 * 6 + 2, 5 * 6 + 3, 5 * 6 + 4 : AtrasRotarDerecha()

                    Case Else
                        Select Case 6 * CaraInferior + CaraFrontal
                            Case 0 To 35 : SalimosConError(22) : Stop
                            Case Else : SalimosConError(5) : Stop
                        End Select
                End Select
            Case Else
                SalimosConError(5) : Stop
        End Select
    End Sub

    Public Sub AbajoGirarDerecha()
        AbajoGirarIzquierda() : AbajoGirarIzquierda() : AbajoGirarIzquierda()
    End Sub

    Public Sub AbajoGirarDerecha(CaraFrontal%, CaraInferior%)
        AbajoGirarIzquierda(CaraFrontal, CaraInferior) : AbajoGirarIzquierda(CaraFrontal, CaraInferior) : AbajoGirarIzquierda(CaraFrontal, CaraInferior)
    End Sub

    Public Sub IzquierdaGirarArriba()
        AnnadirElementoAMatriz(4, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(1)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(0) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(0)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(1) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(5)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(5))
        MatrizDeSumandos(1) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(1)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(2) += ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaIzquierda(MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(2) -= MatrizDeCuboDeRubik(2)
        MatrizDeSumandos(3) += CType(6 ^ 2, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(0)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(3) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(3)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(4) = 0
        MatrizDeSumandos(5) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(3)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(5) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(5)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(5))
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub IzquierdaGirarArriba(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 2
                Select Case CaraFrontal
                    Case 0 : ArribaGirarDerecha()
                    Case 1 : AlanteRotarIzquierda()
                    Case 2, 4 : SalimosConError(22) : Stop
                    Case 3 : AtrasRotarDerecha()
                    Case 5 : AbajoGirarIzquierda()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 4
                Select Case CaraFrontal
                    Case 0 : AbajoGirarIzquierda()
                    Case 1 : AtrasRotarDerecha()
                    Case 2, 4 : SalimosConError(22) : Stop
                    Case 3 : AlanteRotarIzquierda()
                    Case 5 : ArribaGirarDerecha()
                End Select
            Case 0, 1, 3, 5
                If CaraFrontal = CaraInferior Or CaraFrontal = CaraOpuesta(CaraInferior) Then SalimosConError(21) : Stop
                Select Case 6 * CaraInferior + CaraFrontal
                    Case 0, 5 : SalimosConError(22) : Stop
                    Case 1 : DerechaGirarAbajo()
                    Case 2 : AbajoGirarIzquierda()
                    Case 3 : IzquierdaGirarArriba()
                    Case 4 : ArribaGirarDerecha()

                    Case 1 * 6 + 0 : IzquierdaGirarArriba()
                    Case 1 * 6 + 1, 1 * 6 + 3 : SalimosConError(22) : Stop
                    Case 1 * 6 + 2 : AtrasRotarDerecha()
                    Case 1 * 6 + 4 : AlanteRotarIzquierda()
                    Case 1 * 6 + 5 : DerechaGirarAbajo()

                    Case 3 * 6 + 0 : DerechaGirarAbajo()
                    Case 3 * 6 + 1, 3 * 6 + 3 : SalimosConError(22) : Stop
                    Case 3 * 6 + 2 : AlanteRotarIzquierda()
                    Case 3 * 6 + 4 : AtrasRotarDerecha()
                    Case 3 * 6 + 5 : IzquierdaGirarArriba()

                    Case 5 * 6 + 0, 5 * 6 + 5 : SalimosConError(22) : Stop
                    Case 5 * 6 + 1 : IzquierdaGirarArriba()
                    Case 5 * 6 + 2 : ArribaGirarDerecha()
                    Case 5 * 6 + 3 : DerechaGirarAbajo()
                    Case 5 * 6 + 4 : AbajoGirarIzquierda()
                    Case Else
                        Select Case 6 * CaraInferior + CaraFrontal
                            Case 0 To 35 : SalimosConError(22) : Stop
                            Case Is > 35 : SalimosConError(5) : Stop
                        End Select
                End Select
            Case Else
                SalimosConError(5) : Stop
        End Select
    End Sub

    Public Sub IzquierdaGirarAbajo()
        IzquierdaGirarArriba() : IzquierdaGirarArriba() : IzquierdaGirarArriba()
    End Sub

    Public Sub IzquierdaGirarAbajo(CaraFrontal%, CaraInferior%)
        IzquierdaGirarArriba(CaraFrontal, CaraInferior) : IzquierdaGirarArriba(CaraFrontal, CaraInferior) : IzquierdaGirarArriba(CaraFrontal, CaraInferior)
    End Sub

    Public Sub DerechaGirarArriba()
        AnnadirElementoAMatriz(6, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) += CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(1)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(0) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(0)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(1) += CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(5)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(5))
        MatrizDeSumandos(1) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(1)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(2) = 0
        MatrizDeSumandos(3) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(0)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(0)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(3) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(3)) + CType(6 ^ 3, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(4) += ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(4) -= MatrizDeCuboDeRubik(4)
        MatrizDeSumandos(5) += CType(6 ^ 2, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(3)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(3, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(5) -= CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(5)) + CType(6 ^ 5, Integer) * ColorDeLaCasilla(5, MatrizDeCuboDeRubik(5)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(5))
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub DerechaGirarArriba(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 2
                Select Case CaraFrontal
                    Case 0 : AbajoGirarDerecha()
                    Case 1 : AtrasRotarIzquierda()
                    Case 2, 4 : SalimosConError(21) : Stop
                    Case 3 : AlanteRotarDerecha()
                    Case 5 : ArribaGirarIzquierda()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 4
                Select Case CaraFrontal
                    Case 0 : ArribaGirarIzquierda()
                    Case 1 : AlanteRotarDerecha()
                    Case 2, 4 : SalimosConError(21) : Stop
                    Case 3 : AtrasRotarIzquierda()
                    Case 5 : AbajoGirarDerecha()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 0, 1, 3, 5
                If CaraInferior = CaraFrontal Or CaraInferior = CaraOpuesta(CaraFrontal) Then SalimosConError(21) : Stop
                Select Case 6 * CaraInferior + CaraFrontal
                    Case 0, 5 : SalimosConError(22) : Stop
                    Case 1 : IzquierdaGirarAbajo()
                    Case 2 : ArribaGirarIzquierda()
                    Case 3 : DerechaGirarArriba()
                    Case 4 : AbajoGirarDerecha()

                    Case 1 * 6 + 0 : DerechaGirarArriba()
                    Case 1 * 6 + 1, 1 * 6 + 3 : SalimosConError(22) : Stop
                    Case 1 * 6 + 2 : AlanteRotarDerecha()
                    Case 1 * 6 + 4 : AtrasRotarIzquierda()
                    Case 1 * 6 + 5 : IzquierdaGirarAbajo()

                    Case 3 * 6 + 0 : IzquierdaGirarAbajo()
                    Case 3 * 6 + 1, 3 * 6 + 3 : SalimosConError(22) : Stop
                    Case 3 * 6 + 2 : AtrasRotarIzquierda()
                    Case 3 * 6 + 4 : AlanteRotarDerecha()
                    Case 3 * 6 + 5 : DerechaGirarArriba()

                    Case 5 * 6 + 0, 5 * 6 + 5 : SalimosConError(22) : Stop
                    Case 5 * 6 + 1 : DerechaGirarArriba()
                    Case 5 * 6 + 2 : AbajoGirarDerecha()
                    Case 5 * 6 + 3 : IzquierdaGirarAbajo()
                    Case 5 * 6 + 4 : ArribaGirarIzquierda()

                    Case Else
                        Select Case 6 * CaraInferior + CaraFrontal
                            Case 0 To 35 : SalimosConError(22) : Stop
                            Case Else : SalimosConError(5) : Stop
                        End Select
                End Select
        End Select
    End Sub

    Public Sub DerechaGirarAbajo()
        DerechaGirarArriba() : DerechaGirarArriba() : DerechaGirarArriba()
    End Sub

    Public Sub DerechaGirarAbajo(CaraFrontal%, CaraInferior%)
        DerechaGirarArriba(CaraFrontal, CaraInferior) : DerechaGirarArriba(CaraFrontal, CaraInferior) : DerechaGirarArriba(CaraFrontal, CaraInferior)
    End Sub

    Public Sub AlanteRotarIzquierda()
        AnnadirElementoAMatriz(8, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) += ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaIzquierda(MatrizDeCuboDeRubik(0))
        MatrizDeSumandos(0) -= MatrizDeCuboDeRubik(0)
        MatrizDeSumandos(1) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(2)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(1) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(1)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(2) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(3)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(2) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(2)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(3) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(4)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(3) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(3)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(4) += CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(1)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(4) -= CType(6 ^ 0, Integer) * ColorDeLaCasilla(0, MatrizDeCuboDeRubik(4)) + CType(6 ^ 1, Integer) * ColorDeLaCasilla(1, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 2, Integer) * ColorDeLaCasilla(2, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(5) = 0
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub AlanteRotarIzquierda(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 0, 5
                Select Case CaraFrontal
                    Case 0, 5 : SalimosConError(21) : Stop
                    Case 1 : AbajoGirarIzquierda()
                    Case 2 : IzquierdaGirarArriba()
                    Case 3 : ArribaGirarDerecha()
                    Case 4 : DerechaGirarAbajo()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 1, 3
                Select Case CaraFrontal
                    Case 0 : AlanteRotarIzquierda()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case 2 : IzquierdaGirarArriba()
                    Case 4 : DerechaGirarAbajo()
                    Case 5 : AtrasRotarDerecha()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 2, 4
                Select Case CaraFrontal
                    Case 0 : AlanteRotarIzquierda()
                    Case 1 : AbajoGirarIzquierda()
                    Case 2, 4 : SalimosConError(21) : Stop
                    Case 3 : ArribaGirarDerecha()
                    Case 5 : AtrasRotarDerecha()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case Else
                SalimosConError(5) : Stop
        End Select
    End Sub

    Public Sub AlanteRotarDerecha()
        AlanteRotarIzquierda() : AlanteRotarIzquierda() : AlanteRotarIzquierda()
    End Sub

    Public Sub AlanteRotarDerecha(CaraFrontal%, CaraInferior%)
        AlanteRotarIzquierda(CaraFrontal, CaraInferior) : AlanteRotarIzquierda(CaraFrontal, CaraInferior) : AlanteRotarIzquierda(CaraFrontal, CaraInferior)
    End Sub

    Public Sub AtrasRotarIzquierda()
        AnnadirElementoAMatriz(10, ListaDeMovimientos)
        Dim MatrizDeSumandos(5) As Integer
        MatrizDeSumandos(0) = 0
        MatrizDeSumandos(1) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(2)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(1) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(1)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(2) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(3)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(2) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(2)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(2)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(2))
        MatrizDeSumandos(3) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(4)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(3) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(3)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(3)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(3))
        MatrizDeSumandos(4) += CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(1)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(1)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(1))
        MatrizDeSumandos(4) -= CType(6 ^ 6, Integer) * ColorDeLaCasilla(6, MatrizDeCuboDeRubik(4)) + CType(6 ^ 7, Integer) * ColorDeLaCasilla(7, MatrizDeCuboDeRubik(4)) +
            CType(6 ^ 8, Integer) * ColorDeLaCasilla(8, MatrizDeCuboDeRubik(4))
        MatrizDeSumandos(5) += ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(MatrizDeCuboDeRubik(5))
        MatrizDeSumandos(5) -= MatrizDeCuboDeRubik(5)
        AplicarSumandos(MatrizDeSumandos)
    End Sub

    Public Sub AtrasRotarIzquierda(CaraFrontal%, CaraInferior%)
        Select Case CaraInferior
            Case 0, 5
                Select Case CaraFrontal
                    Case 0, 5 : SalimosConError(21) : Stop
                    Case 1 : ArribaGirarIzquierda()
                    Case 2 : DerechaGirarArriba()
                    Case 3 : AbajoGirarDerecha()
                    Case 4 : IzquierdaGirarAbajo()
                    Case Else : SalimosConError(5)
                End Select
            Case 1, 3
                Select Case CaraFrontal
                    Case 0 : AtrasRotarIzquierda()
                    Case 1, 3 : SalimosConError(21) : Stop
                    Case 2 : DerechaGirarArriba()
                    Case 4 : IzquierdaGirarAbajo()
                    Case 5 : AlanteRotarDerecha()
                    Case Else : SalimosConError(5) : Stop
                End Select
            Case 2, 4
                Select Case CaraFrontal
                    Case 0 : AtrasRotarIzquierda()
                    Case 1 : ArribaGirarIzquierda()
                    Case 2, 4 : SalimosConError(21) : Stop
                    Case 3 : AbajoGirarDerecha()
                    Case 5 : AlanteRotarDerecha()
                    Case Else : SalimosConError(5) : Stop
                End Select
        End Select
    End Sub

    Public Sub AtrasRotarDerecha()
        AtrasRotarIzquierda() : AtrasRotarIzquierda() : AtrasRotarIzquierda()
    End Sub

    Public Sub AtrasRotarDerecha(CaraFrontal%, CaraInferior%)
        AtrasRotarIzquierda(CaraFrontal, CaraInferior) : AtrasRotarIzquierda(CaraFrontal, CaraInferior) : AtrasRotarIzquierda(CaraFrontal, CaraInferior)
    End Sub

    Private Sub AplicarSumandos(MatrizDeSumandos() As Integer)
        Dim Contador As Integer
        For Contador = 0 To 5
            MatrizDeCuboDeRubik(Contador) += MatrizDeSumandos(Contador)
        Next
    End Sub


    Public Sub EjecutarMovimiento(NumeroDeMovimiento As Integer)
        Select Case NumeroDeMovimiento
            Case 0 : ArribaGirarIzquierda()
            Case 1 : ArribaGirarDerecha()
            Case 2 : AbajoGirarIzquierda()
            Case 3 : AbajoGirarDerecha()
            Case 4 : IzquierdaGirarArriba()
            Case 5 : IzquierdaGirarAbajo()
            Case 6 : DerechaGirarArriba()
            Case 7 : DerechaGirarAbajo()
            Case 8 : AlanteRotarIzquierda()
            Case 9 : AlanteRotarDerecha()
            Case 10 : AtrasRotarIzquierda()
            Case 11 : AtrasRotarDerecha()
            Case Else : SalimosConError(19) : Stop
        End Select
    End Sub

    Public Sub EjecutarMovimiento(NumeroDeMovimiento%, CaraFrontal%, CaraInferior%)
        Select Case NumeroDeMovimiento
            Case 0 : ArribaGirarIzquierda(CaraFrontal, CaraInferior)
            Case 1 : ArribaGirarDerecha(CaraFrontal, CaraInferior)
            Case 2 : AbajoGirarIzquierda(CaraFrontal, CaraInferior)
            Case 3 : AbajoGirarDerecha(CaraFrontal, CaraInferior)
            Case 4 : IzquierdaGirarArriba(CaraFrontal, CaraInferior)
            Case 5 : IzquierdaGirarAbajo(CaraFrontal, CaraInferior)
            Case 6 : DerechaGirarArriba(CaraFrontal, CaraInferior)
            Case 7 : DerechaGirarAbajo(CaraFrontal, CaraInferior)
            Case 8 : AlanteRotarIzquierda(CaraFrontal, CaraInferior)
            Case 9 : AlanteRotarDerecha(CaraFrontal, CaraInferior)
            Case 10 : AtrasRotarIzquierda(CaraFrontal, CaraInferior)
            Case 11 : AtrasRotarDerecha(CaraFrontal, CaraInferior)
            Case Else : SalimosConError(19) : Stop
        End Select
    End Sub



End Class

