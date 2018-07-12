Option Strict On
Option Explicit On
Imports Cubo_de_Rubik.Utilidades

Public Class MontarElCuboDeLuxe
    ' Lo primero que haremos es montar la cara amarilla
    Private Const ColorAmarillo As Integer = 0
    Private CuboQueTenemosQueMontar As ClaseCuboDeRubik
    'Private ListaDeMovimientos() As Integer


    Public Sub New(CuboArgumento As ClaseCuboDeRubik)
        CuboQueTenemosQueMontar = New ClaseCuboDeRubik(ClonacionDeMatriz(CuboArgumento.MatrizDeCuboDeRubik))
    End Sub


    Public Sub MostrarSolucionDelCubo()
        Dim CadenaTotal As String = DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar, True)

        CuboQueTenemosQueMontar.MatrizDeCuboDeRubik = ClonacionDeMatriz(CuboQueTenemosQueMontar.MatrizInicial)
        Dim CadenaDeMensaje, TituloDeMensaje As String
        CadenaDeMensaje = "A continuación, vamos a ver los " & CuboQueTenemosQueMontar.ListaDeMovimientos.GetLength(0) & " movimientos necesarios"
        TituloDeMensaje = CuboQueTenemosQueMontar.ListaDeMovimientos.GetLength(0) & " movimientos"
        MessageBox.Show(CadenaDeMensaje, TituloDeMensaje)

        Dim Cadenita As String
        Do While CadenaTotal <> vbNullString
            If CadenaTotal.IndexOf(vbCrLf & vbCrLf) <> -1 Then
                Cadenita = CadenaTotal.Substring(0, CadenaTotal.IndexOf(vbCrLf & vbCrLf))
                CadenaTotal = CadenaTotal.Substring(CadenaTotal.IndexOf(vbCrLf & vbCrLf) + 4)
            ElseIf CadenaTotal <> vbNullString Then
                Cadenita = CadenaTotal
                CadenaTotal = vbNullString
            End If
            MessageBox.Show(Cadenita)
        Loop
    End Sub


    ' REVISAMOS A PARTIR DE AQUÍ:
    '------------------------

    Private Function EstaMontadaLaCaraAmarillaPeroNoSePuedeMontarLaPrimeraLinea() As Boolean
        If Not EstaMontadaLaCara(ColorAmarillo) Then Return False
        Dim CaraActual, CaraSiguiente As Integer
        Dim ColorActual, ColorSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            ColorActual = ColorDeLaCasilla(0, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual))
            ColorSiguiente = ColorDeLaCasilla(2, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente))
            If Not (ColorSiguiente = ColorActual Mod 4 + 1) Then Return True
        Next
        Return False
    End Function

    Private Function EstaMontadaLaCaraBlancaPeroNoSePuedeMontarLaUltimaLinea() As Boolean
        ' Damos por hecho que ya está montada la cara amarilla y las dos líneas superiores de cada cara adyacente
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then Return False
        If Not EstaMontadaLaCara(5) Then Return False
        Dim CaraActual, CaraSiguiente As Integer
        Dim ColorActual, ColorSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            ColorActual = ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual))
            ColorSiguiente = ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente))
            If Not (ColorSiguiente = ColorActual Mod 4 + 1) Then Return True
        Next
        Return False
    End Function

    '------------------------
    ' REVISAMOS HASTA AQUÍ.


    ' FINAL:
    ' -----------------------
    Private Sub HacerComprobacionesPrevias()
        Dim Contador, ColorActual, CuentaCaras, CuentaCasillas As Integer
        Dim AparicionesDeCadaColor(5) As Integer
        For CuentaCaras = 0 To 5
            If Not SonDelColorBuscadoTodasLasCasillas(CuentaCaras, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CuentaCaras), 4) Then SalimosConError(67) : Stop : End
        Next
        InicializarMatriz(AparicionesDeCadaColor)
        For CuentaCaras = 0 To 5
            For CuentaCasillas = 0 To 8
                ColorActual = ColorDeLaCasilla(CuentaCasillas, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CuentaCaras))
                AparicionesDeCadaColor(ColorActual) += 1
                If AparicionesDeCadaColor(ColorActual) > 9 Then SalimosConError(67) : Stop : End
            Next
        Next

        Dim AparicionesDeCadaPar(5, 5) As Integer

        Dim ColorSuperior, ColorLateralInferior, ColorLateralSuperior, ColorInferior, ColorLateralIzquierdo, ColorLateralDerecho As Integer
        Dim CasillaAmarilla, CasillaBlanca As Integer
        Dim CaraLateral As Integer
        For CaraLateral = 1 To 4
            Select Case CaraLateral
                Case 1 : CasillaAmarilla = 7 : CasillaBlanca = 1
                Case 2 : CasillaAmarilla = 3 : CasillaBlanca = 3
                Case 3 : CasillaAmarilla = 1 : CasillaBlanca = 7
                Case 4 : CasillaAmarilla = 5 : CasillaBlanca = 5
                Case Else : SalimosConError(22) : Stop
            End Select
            ColorSuperior = ColorDeLaCasilla(CasillaAmarilla, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0))
            ColorInferior = ColorDeLaCasilla(CasillaBlanca, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5))
            ColorLateralSuperior = ColorDeLaCasilla(1, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral))
            ColorLateralInferior = ColorDeLaCasilla(7, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral))
            ColorLateralDerecho = ColorDeLaCasilla(3, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral))
            ColorLateralIzquierdo = ColorDeLaCasilla(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral Mod 4 + 1))

            AparicionesDeCadaPar(ColorSuperior, ColorLateralSuperior) += 1
            AparicionesDeCadaPar(ColorLateralSuperior, ColorSuperior) += 1
            AparicionesDeCadaPar(ColorInferior, ColorLateralInferior) += 1
            AparicionesDeCadaPar(ColorLateralInferior, ColorInferior) += 1
            AparicionesDeCadaPar(ColorLateralIzquierdo, ColorLateralDerecho) += 1
            AparicionesDeCadaPar(ColorLateralDerecho, ColorLateralIzquierdo) += 1
        Next
        Dim ColorUno, ColorDos As Integer
        For ColorUno = 0 To 5
            For ColorDos = 0 To 5
                If ColorUno = ColorDos Or ColorUno = CaraOpuesta(ColorDos) Then
                    If AparicionesDeCadaPar(ColorUno, ColorDos) <> 0 Then SalimosConError(67) : Stop : End
                Else
                    If AparicionesDeCadaPar(ColorUno, ColorDos) <> 1 Then SalimosConError(67) : Stop : End
                End If
            Next
        Next

        Dim AparicionesDeCadaTrio(5, 5, 5) As Integer
        Dim TrioEsquina(2) As Integer


        Dim CaraPosterior, ColorTres As Integer


        For CaraLateral = 1 To 4
            Select Case CaraLateral
                Case 1 : CasillaAmarilla = 6 : CasillaBlanca = 0
                Case 2 : CasillaAmarilla = 0 : CasillaBlanca = 6
                Case 3 : CasillaAmarilla = 2 : CasillaBlanca = 8
                Case 4 : CasillaAmarilla = 8 : CasillaBlanca = 2
                Case Else : SalimosConError(22) : Stop
            End Select
            CaraPosterior = CaraLateral Mod 4 + 1

            ColorUno = ColorDeLaCasilla(CasillaAmarilla, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0))
            ColorDos = ColorDeLaCasilla(0, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral))
            ColorTres = ColorDeLaCasilla(2, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraPosterior))
            AparicionesDeCadaTrio(ColorUno, ColorDos, ColorTres) += 1
            AparicionesDeCadaTrio(ColorUno, ColorTres, ColorDos) += 1
            AparicionesDeCadaTrio(ColorDos, ColorUno, ColorTres) += 1
            AparicionesDeCadaTrio(ColorDos, ColorTres, ColorUno) += 1
            AparicionesDeCadaTrio(ColorTres, ColorUno, ColorDos) += 1
            AparicionesDeCadaTrio(ColorTres, ColorDos, ColorUno) += 1

            ColorUno = ColorDeLaCasilla(CasillaBlanca, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5))
            ColorDos = ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraLateral))
            ColorTres = ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraPosterior))
            AparicionesDeCadaTrio(ColorUno, ColorDos, ColorTres) += 1
            AparicionesDeCadaTrio(ColorUno, ColorTres, ColorDos) += 1
            AparicionesDeCadaTrio(ColorDos, ColorUno, ColorTres) += 1
            AparicionesDeCadaTrio(ColorDos, ColorTres, ColorUno) += 1
            AparicionesDeCadaTrio(ColorTres, ColorUno, ColorDos) += 1
            AparicionesDeCadaTrio(ColorTres, ColorDos, ColorUno) += 1
        Next

        Dim AparicionesAmarillas, AparicionesBlancas As Integer
        Dim ContadorDePruebas As Integer = 0
        For ColorUno = 0 To 5
            For ColorDos = 0 To 5
                For ColorTres = 0 To 5
                    AparicionesAmarillas = 0 : AparicionesBlancas = 0
                    If ColorUno = 0 Then AparicionesAmarillas += 1
                    If ColorDos = 0 Then AparicionesAmarillas += 1
                    If ColorTres = 0 Then AparicionesAmarillas += 1

                    If ColorUno = 5 Then AparicionesBlancas += 1
                    If ColorDos = 5 Then AparicionesBlancas += 1
                    If ColorTres = 5 Then AparicionesBlancas += 1

                    If Not (AparicionesAmarillas = 1 And AparicionesBlancas = 0) And Not (AparicionesAmarillas = 0 And AparicionesBlancas = 1) Then Continue For

                    Dim ColoresLateralesDeLaEsquina(1) As Integer
                    ColoresLateralesDeLaEsquina = {-1, -1}

                    Dim PivoteActual As Integer = 0
                    If ColorUno <> 0 And ColorUno <> 5 Then
                        ColoresLateralesDeLaEsquina(PivoteActual) = ColorUno
                        PivoteActual += 1
                    End If
                    If ColorDos <> 0 And ColorDos <> 5 Then
                        ColoresLateralesDeLaEsquina(PivoteActual) = ColorDos
                        PivoteActual += 1
                    End If
                    If ColorTres <> 0 And ColorTres <> 5 Then
                        ColoresLateralesDeLaEsquina(PivoteActual) = ColorTres
                        PivoteActual += 1
                    End If
                    Select Case Math.Abs(ColoresLateralesDeLaEsquina(0) - ColoresLateralesDeLaEsquina(1))
                        Case 0, 2 : Continue For
                        Case 1, 3
                        Case Else : SalimosConError(67) : Stop : End
                    End Select

                    ContadorDePruebas += 1
                    If AparicionesDeCadaTrio(ColorUno, ColorDos, ColorTres) <> 1 Then SalimosConError(67) : Stop : End
                Next
            Next
        Next
        If ContadorDePruebas <> 48 Then SalimosConError(67) : Stop : End
    End Sub

    Public Sub CompletarElCubo()
        HacerComprobacionesPrevias()
        If Not EstanMontadasLasCuatroEsquinasDeAbajo() Then MontarLasCuatroEsquinasDeAbajo()
        Dim NumeroDeRepeticionesDelBucle As Integer = 0
        Do While Not EstaCompletoElCubo()
            NumeroDeRepeticionesDelBucle += 1
            If NumeroDeRepeticionesDelBucle > 10 Then SalimosConError(67) : Stop : End
            If Not EstanMontadasLasCuatroEsquinasDeAbajo() Then SalimosConError(22) : Stop
            Dim NumeroDeBordesDescolocadosAbajo As Integer = CuantosBordesDescolocadosHayAbajo()
            Dim CaraInferior As Integer = 0
            Select Case NumeroDeBordesDescolocadosAbajo
                Case 4 : CaraInferior = 1
                Case 3
                    Dim Contador As Integer
                    For Contador = 1 To 4
                        If SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then
                            CaraInferior = (Contador + 1) Mod 4 + 1
                            Exit For
                        End If
                    Next
                    If CaraInferior = 0 Then SalimosConError(67) : Stop : End
                Case Else
                    SalimosConError(22) : Stop
            End Select
            If CaraInferior < 1 Or CaraInferior > 4 Then SalimosConError(67) : Stop : End
            Dim CaraIzquierda, CaraDerecha, CaraSuperior As Integer
            Dim GiramosALaDerecha As Boolean
            CaraDerecha = CaraInferior Mod 4 + 1
            CaraIzquierda = (CaraInferior + 2) Mod 4 + 1
            CaraSuperior = (CaraInferior + 1) Mod 4 + 1

            If Not SonDelColorBuscadoTodasLasCasillas(CaraSuperior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSuperior), 7) Then
                GiramosALaDerecha = True
            ElseIf SonDelColorBuscadoTodasLasCasillas(CaraDerecha, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraIzquierda), 7) Then
                GiramosALaDerecha = True
            ElseIf SonDelColorBuscadoTodasLasCasillas(CaraInferior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraIzquierda), 7) Then
                GiramosALaDerecha = False
            Else
                SalimosConError(67) : Stop : End
            End If

            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            If GiramosALaDerecha Then
                CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            Else
                CuboQueTenemosQueMontar.AlanteRotarIzquierda(5, CaraInferior)
            End If
            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.IzquierdaGirarArriba(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
            If GiramosALaDerecha Then
                CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            Else
                CuboQueTenemosQueMontar.AlanteRotarIzquierda(5, CaraInferior)
            End If
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            MontarLasCuatroEsquinasDeAbajo()
        Loop
        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar, True))
        MessageBox.Show("Parece que ya está montado el cubo completo")
    End Sub

    ' -----------------------
    ' FINAL.


    ' CUATRO BORDES DE ABAJO Y FINAL:
    ' -----------------------
    Private Function EstaCompletoElCubo() As Boolean
        Dim CuentaCaras, CuentaCasillas As Integer
        For CuentaCaras = 0 To 5
            For CuentaCasillas = 0 To 8
                If ColorDeLaCasilla(CuentaCasillas, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CuentaCaras)) <> CuentaCaras Then Return False
            Next
        Next
        Return True
    End Function

    Private Function CuantosBordesDescolocadosHayAbajo() As Integer
        ' Damos por hecho, al entrar, aquí, que todo lo que no sean los cuatro bordes de abajo está todo montado
        If Not EstanMontadasLasCuatroEsquinasDeAbajo() Then SalimosConError(22) : Stop
        Dim Contador As Integer
        Dim Acumulador As Integer = 0
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then Acumulador += 1
        Next
        If Acumulador <> 3 And Acumulador <> 4 Then SalimosConError(67) : Stop : End
        Return Acumulador
    End Function
    ' -----------------------
    ' CUATRO BORDES DE ABAJO Y FINAL.


    ' CUATRO ESQUINAS DE ABAJO:
    ' ---------------------------
    Private Function EstanMontadasLasCuatroEsquinasDeAbajo() As Boolean
        ' Aquí en la resolución damos por hecho que está montada la cara amarilla completa, las dos líneas superiores de cada una de las cuatro caras adyacentes, y la cara blanca completa
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then Return False
        If Not EstaMontadaLaCara(5) Then Return False
        Dim Contador As Integer
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 6, 8) Then Return False
        Next
        If Not SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 0, 2, 6, 8) Then SalimosConError(22) : Stop
        Return True
    End Function

    Private Function HayMontadasDosEsquinasAdyacentesAbajoYSoloDos() As Boolean
        ' Aquí en la resolución damos por hecho que está montada la cara amarilla completa, las dos líneas superiores de cada una de las cuatro caras adyacentes, y la cara blanca completa
        If EstanMontadasLasCuatroEsquinasDeAbajo() Then Return False
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then Return False
        If Not EstaMontadaLaCara(5) Then Return False

        Dim CaraActual, CaraAnterior, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            If Not SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 6, 8) Then Continue For
            If Not SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 8) Then SalimosConError(22) : Stop
            If Not SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraAnterior), 6) Then SalimosConError(22) : Stop
            Return True
        Next

        Return False
    End Function

    Private Function HayMontadasDosEsquinasEnfrentadasAbajoYSoloDos() As Boolean
        ' Aquí en la resolución damos por hecho que está montada la cara amarilla completa, las dos líneas superiores de cada una de las cuatro caras adyacentes, y la cara blanca completa
        If EstanMontadasLasCuatroEsquinasDeAbajo() Then Return False
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then Return False
        If Not EstaMontadaLaCara(5) Then Return False

        Dim CaraActual, CaraAnterior, CaraSiguiente, CaraDeEnfrente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            CaraDeEnfrente = (CaraActual + 1) Mod 4 + 1
            If Not SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 6) Then Continue For
            If Not SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 8) Then SalimosConError(22) : Stop
            If Not SonDelColorBuscadoTodasLasCasillas(CaraDeEnfrente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDeEnfrente), 6) Then Continue For
            If Not SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraAnterior), 8) Then SalimosConError(22) : Stop
            Return True
        Next
        Return False
    End Function

    Private Function HayMontadasDosYSoloDosEsquinasAbajo() As Boolean
        ' Aquí en la resolución damos por hecho que está montada la cara amarilla completa, las dos líneas superiores de cada una de las cuatro caras adyacentes, y la cara blanca completa
        Dim Uno, Dos As Boolean
        Uno = HayMontadasDosEsquinasAdyacentesAbajoYSoloDos()
        Dos = HayMontadasDosEsquinasEnfrentadasAbajoYSoloDos()
        If Uno And Dos Then SalimosConError(65) : Stop
        Return Uno Xor Dos
    End Function

    Public Sub MontarLasCuatroEsquinasDeAbajo()
        ' Aquí damos por hecho que tenemos que montar también la cara amarilla, las dos líneas superiores de cada una de las cuatro caras adyacentes, y la cara blanca
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then MontarLaCaraAmarillaConLaPrimeraYSegundaLinea()
        If Not EstaMontadaLaCara(5) Then MontarLaCaraBlancaDeAbajo()
        If EstaMontadaLaCaraBlancaPeroNoSePuedeMontarLaUltimaLinea() Then SalimosConError(67) : Stop : End
        Do While Not EstanMontadasLasCuatroEsquinasDeAbajo()
            Do While Not HayMontadasDosYSoloDosEsquinasAbajo() AndAlso Not EstanMontadasLasCuatroEsquinasDeAbajo()
                CuboQueTenemosQueMontar.AtrasRotarIzquierda()
            Loop
            If EstanMontadasLasCuatroEsquinasDeAbajo() Then Exit Do
            Dim CaraInferior As Integer = 0
            If HayMontadasDosEsquinasAdyacentesAbajoYSoloDos() Then
                Dim CaraActual, CaraAnterior, CaraSiguiente As Integer
                For CaraActual = 1 To 4
                    CaraSiguiente = CaraActual Mod 4 + 1
                    CaraAnterior = (CaraActual + 2) Mod 4 + 1
                    If Not SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 6, 8) Then Continue For
                    If Not SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 8) Then SalimosConError(22) : Stop
                    If Not SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraAnterior), 6) Then SalimosConError(22) : Stop
                    CaraInferior = (CaraActual + 1) Mod 4 + 1
                    Exit For
                Next
                If CaraActual > 4 Then SalimosConError(22) : Stop
            ElseIf HayMontadasDosEsquinasEnfrentadasAbajoYSoloDos() Then
                CaraInferior = 1
            Else
                SalimosConError(22) : Stop
            End If
            If CaraInferior = 0 Then SalimosConError(22) : Stop
            If CaraInferior < 1 Or CaraInferior > 4 Then SalimosConError(66) : Stop

            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
            CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
            CuboQueTenemosQueMontar.AbajoGirarIzquierda(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
            CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
        Loop
        '        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
        '        MessageBox.Show("Se supone que ya están montadas las cuatro esquinas de abajo, echa un vistazo a ver")
    End Sub
    ' ---------------------------
    ' CUATRO ESQUINAS DE ABAJO.


    ' CARA BLANCA DE ABAJO:
    ' ------------------------------
    Private Function HayCruzBlanca() As Boolean
        Return SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 3, 5, 7)
    End Function

    Private Function HayLineaBlancaSinPico() As Boolean
        Dim BoolAuxUno, BoolAuxDos As Boolean
        BoolAuxUno = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 7)
        BoolAuxDos = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 3, 5)
        Return BoolAuxUno Xor BoolAuxDos
    End Function

    Private Function HayPicoBlancoSinLinea() As Boolean
        Dim BoolAuxUno, BoolAuxDos As Boolean
        BoolAuxUno = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 3)
        BoolAuxDos = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 5, 7)
        If BoolAuxUno Xor BoolAuxDos Then Return True
        BoolAuxUno = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 5)
        BoolAuxDos = SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 3, 7)
        If BoolAuxUno Xor BoolAuxDos Then Return True
        Return False    ' Aquí no puede haber cruz ni otra cosa, porque ya habría saltado alguna de las funciones anteriormente
    End Function


    Private Sub HacerLaCruzAPartirDeLaLineaHorizontal(ByVal CaraInferior As Integer)
        CuboQueTenemosQueMontar.AbajoGirarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarIzquierda(5, CaraInferior)
        CuboQueTenemosQueMontar.AbajoGirarIzquierda(5, CaraInferior)
    End Sub

    Private Sub HacerLaLineaHorizontalAPartirDelPicoNorOriental(ByVal CaraInferior As Integer)
        ' Aquí hay que comprobar primero si existe pico nororiental
        CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.ArribaGirarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.ArribaGirarIzquierda(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
        CuboQueTenemosQueMontar.ArribaGirarDerecha(5, CaraInferior)
    End Sub

    Private Sub HacerLaCruzAPartirDeLaNada(ByVal CaraInferior As Integer)
        Dim CaraSiguiente As Integer = CaraInferior Mod 4 + 1
        HacerLaCruzAPartirDeLaLineaHorizontal(CaraSiguiente)
        HacerLaLineaHorizontalAPartirDelPicoNorOriental(CaraInferior)
        HacerLaCruzAPartirDeLaLineaHorizontal(CaraInferior)
    End Sub


    Private Function AbajoEstaLaNada() As Boolean
        ' Se refiere a que no está ninguno de los bordes blancos que formarían la cruz, pero las esquinas blancas pueden estar
        Return NoSonDelColorBuscadoNingunaDeLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 3, 5, 7)
    End Function

    Private Sub MontarLaCruzBlanca()
        If HayCruzBlanca() Then
            MessageBox.Show("Ya estaba montada la cruz blanca")    ' Se supone que aquí no podemos llegar, porque si estamos en este procedimiento es porque no había cruz blanca.
            Exit Sub
        ElseIf AbajoEstaLaNada() Then
            HacerLaCruzAPartirDeLaNada(1)
        ElseIf HayLineaBlancaSinPico() Then
            If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 3, 5) Then
                HacerLaCruzAPartirDeLaLineaHorizontal(1)
            ElseIf SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1, 7) Then
                HacerLaCruzAPartirDeLaLineaHorizontal(2)
            Else
                SalimosConError(22) : Stop
            End If
        ElseIf HayPicoBlancoSinLinea() Then
            Dim CaraInferior As Integer
            If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 3) Then
                    CaraInferior = 4
                ElseIf SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 5) Then
                    CaraInferior = 3
                Else
                    SalimosConError(22) : Stop
                End If
            ElseIf SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 7) Then
                If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 3) Then
                    CaraInferior = 1
                ElseIf SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), 5) Then
                    CaraInferior = 2
                Else
                    SalimosConError(22) : Stop
                End If
            Else
                SalimosConError(22) : Stop
            End If
            HacerLaLineaHorizontalAPartirDelPicoNorOriental(CaraInferior)
            HacerLaCruzAPartirDeLaLineaHorizontal(CaraInferior)
        Else
            SalimosConError(67) : Stop : End
        End If

        If Not HayCruzBlanca() Then SalimosConError(67) : Stop : End


        '        If Not CuboQueTenemosQueMontar.ListaDeMovimientos Is Nothing AndAlso CuboQueTenemosQueMontar.ListaDeMovimientos.Length <> 0 Then SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        If Not CuboQueTenemosQueMontar.ListaDeMovimientos Is Nothing AndAlso CuboQueTenemosQueMontar.ListaDeMovimientos.Length <> 0 Then Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
        '        MessageBox.Show("Se supone que ya hemos montado la cruz blanca, comprueba a ver si está bien")
    End Sub


    Public Sub MontarLaCaraBlancaDeAbajo()
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then MontarLaCaraAmarillaConLaPrimeraYSegundaLinea()
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then SalimosConError(64) : Stop
        If Not HayCruzBlanca() Then MontarLaCruzBlanca()
        If Not HayCruzBlanca() Then SalimosConError(67) : Stop : End

        Dim NumeroDeRepeticionesDelBucle As Integer = 0
        Do While Not EstaMontadaLaCara(5)
            NumeroDeRepeticionesDelBucle += 1
            If NumeroDeRepeticionesDelBucle > 10 Then SalimosConError(67) : Stop : End
            If Not HayCruzBlanca() Or Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then SalimosConError(22) : Stop : End
            Dim CaraInferior, CaraAnteriorALaInferior, Contador As Integer
            Dim NumeroDeEsquinasBlancas As Integer = 0
            For Contador = 0 To 8 Step 2
                If Contador = 4 Then Continue For
                If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), Contador) Then NumeroDeEsquinasBlancas += 1
            Next
            If Not HayCruzBlanca() Or Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then SalimosConError(67) : Stop : End
            CaraInferior = 0
            Select Case NumeroDeEsquinasBlancas
                Case 0
                    For Contador = 1 To 4
                        CaraAnteriorALaInferior = (Contador + 6) Mod 4 + 1
                        If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraAnteriorALaInferior), 6) Then
                            CaraInferior = Contador     ' A lo mejor es técnicamente imposible que pueda saltar el 4, porque a lo mejor se supone que tiene que haber más de una cara con la casilla ésa blanca, y por eso no podemos esperar a la última...
                            Exit For
                        End If
                    Next
                Case 1
                    For Contador = 0 To 8 Step 2
                        If Contador = 4 Then Continue For
                        If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), Contador) Then
                            Select Case Contador
                                Case 0 : CaraInferior = 2
                                Case 2 : CaraInferior = 1
                                Case 6 : CaraInferior = 3
                                Case 8 : CaraInferior = 4
                                Case Else : SalimosConError(67) : Stop : End
                            End Select
                            Exit For
                        End If
                    Next
                Case 2, 3, 4
                    For Contador = 1 To 4
                        If SonDelColorBuscadoTodasLasCasillas(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 8) Then
                            CaraInferior = Contador
                            Exit For
                        End If
                    Next
                Case Else
                    SalimosConError(22) : Stop : End
            End Select
            If CaraInferior = 0 Then SalimosConError(67) : Stop : End
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
            CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
            CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarArriba(5, CaraInferior)
            CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.AlanteRotarDerecha(5, CaraInferior)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(5, CaraInferior)
        Loop
        '        If Not CuboQueTenemosQueMontar.ListaDeMovimientos Is Nothing AndAlso CuboQueTenemosQueMontar.ListaDeMovimientos.Length <> 0 Then
        '        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
        '       End If

        ' MessageBox.Show("Se supone que ya tienes montada la cara de abajo, comprueba a ver")
    End Sub
    ' ------------------------------
    ' CARA BLANCA DE ABAJO.


    ' SEGUNDA LÍNEA:
    ' ------------------------
    Private Function HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea() As Boolean
        Dim CaraActual, CaraAnterior, CaraSiguiente, CaraDeEnfrente, CasillaBlanca As Integer
        For CaraActual = 1 To 4
            Select Case CaraActual
                Case 1 : CasillaBlanca = 1
                Case 2 : CasillaBlanca = 3
                Case 3 : CasillaBlanca = 7
                Case 4 : CasillaBlanca = 5
                Case Else : SalimosConError(22) : Stop
            End Select
            CaraSiguiente = CaraActual Mod 4 + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            CaraDeEnfrente = (CaraActual + 1) Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDeEnfrente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 7) Then Return True
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 7) Then Return True
            End If
        Next
        Return False
    End Function

    Private Sub ColocarDirectamenteBordeDeSegundaLinea()
        If Not HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea() Then SalimosConError(60) : Stop
        Dim CaraActual, CaraAnterior, CaraSiguiente, CaraDeEnfrente, CasillaBlanca As Integer
        For CaraActual = 1 To 4
            Select Case CaraActual
                Case 1 : CasillaBlanca = 1
                Case 2 : CasillaBlanca = 3
                Case 3 : CasillaBlanca = 7
                Case 4 : CasillaBlanca = 5
                Case Else : SalimosConError(22) : Stop
            End Select
            CaraSiguiente = CaraActual Mod 4 + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            CaraDeEnfrente = (CaraActual + 1) Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDeEnfrente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 7) Then
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                    Exit Sub
                End If
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 7) Then
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(22) : Stop
    End Sub

    Private Function HayPosibilidadDeColocarINDIRECTAMENTEBordeDeSegundaLinea() As Boolean
        Dim Resultado As Boolean = False
        Dim Contador As Integer
        For Contador = 1 To 4   ' No llegaremos hasta 4, porque 4 movimientos sería como dejarlo como está, y estando como está, no entraríamos aquí porque habría posibilidad de colocar directamente. No obstante, lo dejamos así por lo que pudiera pasar.
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
            If HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea() Then Resultado = True
        Next
        Return Resultado
    End Function

    Private Sub ColocarIndirectamenteBordeDeSegundaLinea()
        If Not HayPosibilidadDeColocarIndirectamenteBordeDeSegundaLinea() Then SalimosConError(61) : Stop
        Dim NumeroDeMovimientos, Contador As Integer
        For NumeroDeMovimientos = 4 To 1 Step -1    ' ' No nos funcionará con el 4, porque 4 movimientos sería como dejarlo como está, y estando como está, no entraríamos aquí porque habría posibilidad de colocar directamente. No obstante, lo dejamos así por lo que pudiera pasar.
            For Contador = 1 To NumeroDeMovimientos
                CuboQueTenemosQueMontar.AtrasRotarIzquierda()
            Next
            If HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea() Then
                ColocarDirectamenteBordeDeSegundaLinea()
                Exit Sub
            End If
        Next
        SalimosConError(22) : Stop
    End Sub

    Private Function EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() As Boolean
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraLinea() Then Return False
        Dim Contador As Integer
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 3, 4, 5) Then Return False
        Next
        Return True
    End Function

    Private Function HayPosibilidadDeBajarBordeInvertidoDeSegundaLinea() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 3) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 5) Then Return True
            End If
        Next
        Return False
    End Function

    Private Sub BajarBordeInvertidoDeSegundaLinea()
        If Not HayPosibilidadDeBajarBordeInvertidoDeSegundaLinea() Then SalimosConError(62) : Stop
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = CaraActual Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 3) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 5) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(22) : Stop
    End Sub

    Private Sub BajarBordeDeSegundaLinea()
        If EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then SalimosConError(63) : Stop
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4     ' Al 4 no vamos a llegar, porque para que estén todos los bordes bien colocados menos el cuarto, tiene que darse una de estas dos 
            '   condiciones:
            '   1) esté el borde invertido, ó 2) que el borde verdadero esté abajo; y tanto en un caso como en otro no podemos llegar a este procedimiento porque antes 
            '   ya ha saltado el HayPosibilidadDeBajarBordeInvertidoDeSegundaLinea o el de HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea o 
            '   HayPosibilidadDeColocarIndirectamenteBordeDeSegundaLinea()

            CaraSiguiente = CaraActual Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 3) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 5) Then Continue For
            End If
            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
            CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
            CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
            Exit Sub
        Next
        SalimosConError(22) : Stop
    End Sub

    Public Sub MontarLaCaraAmarillaConLaPrimeraYSegundaLinea()
        If EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then
            Dim CadenaDeMensaje As String = "La cara y las dos líneas superiores ya están montadas, no hay nada que montar"
            Dim TituloDeMensaje As String = "Está montado el tema ya"
            MessageBox.Show(CadenaDeMensaje, TituloDeMensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            MontarLaCaraAmarillaConLaPrimeraLinea()
        End If
        Do While Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea()
            If HayPosibilidadDeColocarDirectamenteBordeDeSegundaLinea() Then
                ColocarDirectamenteBordeDeSegundaLinea()
            ElseIf HayPosibilidadDeColocarIndirectamenteBordeDeSegundaLinea() Then
                ColocarIndirectamenteBordeDeSegundaLinea()
            ElseIf HayPosibilidadDeBajarBordeInvertidoDeSegundaLinea() Then
                BajarBordeInvertidoDeSegundaLinea()
            Else
                BajarBordeDeSegundaLinea()
            End If
        Loop
        If Not EstaMontadaLaCaraAmarillaConLaPrimeraYSegundaLinea() Then SalimosConError(22) : Stop
        '            MessageBox.Show("Se supone que ya está la cara amarilla montada con las dos líneas superiores de cada cara adycente")
        '        If Not CuboQueTenemosQueMontar.ListaDeMovimientos Is Nothing AndAlso CuboQueTenemosQueMontar.ListaDeMovimientos.Length <> 0 Then
        '        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
        '        End If
    End Sub
    ' -----------------------
    ' SEGUNDA LÍNEA


    ' PRIMERA LÍNEA:
    '------------------------
    Public Sub MontarLaCaraAmarillaConLaPrimeraLinea()
        If EstaMontadaLaCaraAmarillaConLaPrimeraLinea() Then
            Dim CadenaDeMensaje As String = "La cara ya está montada y también la primera línea, no hay nada que montar"
            Dim TituloDeMensaje As String = "La cara y la primera línea ya están montadas"
            '            MessageBox.Show(CadenaDeMensaje, TituloDeMensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        If Not EstaMontadaLaCara(ColorAmarillo) Then
            MontarLaCaraAmarilla()
        End If

        If EstaMontadaLaCaraAmarillaPeroNoSePuedeMontarLaPrimeraLinea() Then SalimosConError(67) : Stop : End

        RotarCaraAmarillaHastaAlcanzarLaPosicionOptima()

        Do While Not EstanMontadosLosBordesDeLaPrimeraLinea()
            If HayPosibilidadDeIntercambiarCuatroBordesSeguidos() Then
                IntercambiarCuatroBordesSeguidos()
            ElseIf HayPosibilidadDeIntercambiarCuatroBordesDiabolicos() Then
                IntercambiarCuatroBordesDiabolicos()
            ElseIf HayPosibilidadDeIntercambiarTresBordesSeguidos() Then
                IntercambiarTresBordesSeguidos()
            ElseIf HayPosibilidadDeIntercambiarDosBordesAdyacentes() Then
                IntercambiarDosBordesAdyacentes()
            ElseIf HayPosibilidadDeIntercambiarDosBordesEnfrentados() Then
                IntercambiarDosBordesEnfrentados()
            Else
                SalimosConError(56) : Stop
            End If
        Loop

        Do While Not EstanMontadasLasEsquinasDeLaPrimeraLinea()
            If HayPosibilidadDeIntercambiarCuatroEsquinasSeguidas() Then
                IntercambiarCuatroEsquinasSeguidas()
            ElseIf HayPosibilidadDeIntercambiarCuatroEsquinasDiabolicas() Then
                IntercambiarCuatroEsquinasDiabolicas()
            ElseIf HayPosibilidadDeIntercambiarTresEsquinasSeguidas() Then
                IntercambiarTresEsquinasSeguidas()
            ElseIf HayPosibilidadDeIntercambiarDosEsquinasAdyacentes() Then
                IntercambiarDosEsquinasAdyacentes()
            ElseIf HayPosibilidadDeIntercambiarDosEsquinasEnfrentadas() Then
                IntercambiarDosEsquinasEnfrentadas()
            Else
                SalimosConError(56) : Stop
            End If
        Loop

        '        If Not EstaMontadaLaCaraAmarillaConLaPrimeraLinea() Then SalimosConError(56) : Stop
        '        MessageBox.Show("Se supone que ya está la cara amarilla montada con la primera línea de cada cara adycente")
        '        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        If CuboQueTenemosQueMontar.ListaDeMovimientos.Length <> 0 Then Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
    End Sub


    Private Sub RotarCaraAmarillaHastaAlcanzarLaPosicionOptima()
        If Not EstaMontadaLaCara(ColorAmarillo) Then SalimosConError(59) : Stop
        Dim Contador As Integer
        Dim MenorNumeroDeMovimientosHastaAhora As Integer = 9999
        Dim MejorContadorHastaAhora As Integer = 5
        Dim MovimientosNecesarios(4) As Integer
        For Contador = 1 To 4
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
            MovimientosNecesarios(Contador) = CuantosMovimientosHayQueHacerEnLaPrimeraLinea()
            Select Case Contador
                Case 4 : MovimientosNecesarios(Contador) += 0
                Case 2 : MovimientosNecesarios(Contador) += 2
                Case 1, 3 : MovimientosNecesarios(Contador) += 1
                Case Else : SalimosConError(22) : Stop
            End Select
            If MovimientosNecesarios(Contador) < MenorNumeroDeMovimientosHastaAhora Then
                MenorNumeroDeMovimientosHastaAhora = MovimientosNecesarios(Contador)
                MejorContadorHastaAhora = Contador
            End If
        Next
        For Contador = 1 To MejorContadorHastaAhora
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Next

    End Sub

    Private Function CuantosMovimientosHayQueHacerEnLaPrimeraLinea() As Integer
        If Not EstaMontadaLaCara(ColorAmarillo) Then SalimosConError(59) : Stop
        Dim Acumulador As Integer = 0

        If HayPosibilidadDeIntercambiarCuatroBordesSeguidos() Then
            Acumulador += 29
        ElseIf HayPosibilidadDeIntercambiarCuatroBordesDiabolicos() Then
            Acumulador += 27
        ElseIf HayPosibilidadDeIntercambiarCuatroBordesAdyacentesDosADos() Then
            Acumulador += 34
        ElseIf HayPosibilidadDeIntercambiarCuatroBordesEnfrentadosDosADos() Then
            Acumulador += 30
        ElseIf HayPosibilidadDeIntercambiarTresBordesSeguidos() Then
            Acumulador += 22
        ElseIf HayPosibilidadDeIntercambiarDosBordesAdyacentes() Then
            Acumulador += 17
        ElseIf HayPosibilidadDeIntercambiarDosBordesEnfrentados() Then
            Acumulador += 15
        End If


        If HayPosibilidadDeIntercambiarCuatroEsquinasSeguidas() Then
            Acumulador += 18
        ElseIf HayPosibilidadDeIntercambiarCuatroEsquinasDiabolicas() Then
            Acumulador += 18
        ElseIf HayPosibilidadDeIntercambiarCuatroEsquinasEnfrentadasDosADos() Then
            Acumulador += 22
        ElseIf HayPosibilidadDeIntercambiarCuatroEsquinasAdyacentesDosADos() Then
            Acumulador += 18
        ElseIf HayPosibilidadDeIntercambiarTresEsquinasSeguidas() Then
            Acumulador += 13
        ElseIf HayPosibilidadDeIntercambiarDosEsquinasAdyacentes() Then
            Acumulador += 9
        ElseIf HayPosibilidadDeIntercambiarDosEsquinasEnfrentadas() Then
            Acumulador += 11
        End If

        Return Acumulador
    End Function


    Private Function EstaMontadaLaCaraAmarillaConLaPrimeraLinea() As Boolean
        If Not EstaMontadaLaCara(ColorAmarillo) Then Return False
        Dim Contador As Integer
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), {0, 1, 2}) Then Return False
        Next
        Return True
    End Function

    Private Function EstanMontadosLosBordesDeLaPrimeraLinea() As Boolean
        'Damos por hecho que la cara amarilla ya está montada, por eso sólo miramos los bordes de la primera línea sin comprobar si está montada la cara
        Dim Contador As Integer
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 1) Then Return False
        Next
        Return True
    End Function

    Private Function EstanMontadasLasEsquinasDeLaPrimeraLinea() As Boolean
        'Damos por hecho que la cara amarilla ya está montada, por eso sólo miramos las esquinas de la primera línea sin comprobar si está montada la cara
        Dim Contador As Integer
        For Contador = 1 To 4
            If Not SonDelColorBuscadoTodasLasCasillas(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), {0, 2}) Then Return False
        Next
        Return True
    End Function


    Private Function HayPosibilidadDeIntercambiarCuatroBordesAdyacentesDosADos() As Boolean
        Dim Acumulador, CaraActual, CaraSiguiente As Integer
        Acumulador = 0
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then Acumulador += 1
            End If
        Next
        If Acumulador = 2 Then
            Return True
        ElseIf Acumulador < 2 Then
            Return False
        Else
            SalimosConError(22) : Stop
        End If
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroBordesEnfrentadosDosADos() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 2
            CaraSiguiente = ((CaraActual + 1) Mod 4) + 1
            If Not SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then Return False
            If Not SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then Return False
        Next
        Return True
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasAdyacentesDosADos() As Boolean
        Dim Acumulador, CaraActual, CaraSiguiente, CaraAnterior As Integer
        Acumulador = 0
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 0) Then Acumulador += 1
            End If
        Next
        If Acumulador = 2 Then
            Return True
        ElseIf Acumulador < 4 Then
            Return False
        Else
            SalimosConError(22) : Stop
        End If
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasEnfrentadasDosADos() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 2
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1
            If Not SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then Return False
            If Not SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then Return False
            If Not SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then Return False
            If Not SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then Return False
        Next
        Return True
    End Function


    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasDiabolicas() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarCuatroEsquinasDiabolicas()
        If Not HayPosibilidadDeIntercambiarCuatroEsquinasDiabolicas() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then

                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraUno, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraDos, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraTres, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraCuatro, 5)

                                            ColocarDirectamenteEsquinaInferior()
                                            Exit Sub


                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next

        SalimosConError(57) : Stop
    End Sub


    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then Return True
                                    End If
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1

            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroEsquinasSeguidas() As Boolean
        Return HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE() Xor HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE()
    End Function


    Private Sub IntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE()
        If Not HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then

                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraUno, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraDos, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraDos, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraCuatro, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraCuatro, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraCuatro, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraCuatro, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraCuatro, 5)

                                            ColocarDirectamenteEsquinaInferior()
                                            Exit Sub

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        Next

        SalimosConError(57) : Stop
    End Sub

    Private Sub IntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE()
        If Not HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = CaraUno Mod 4 + 1
            CaraTres = CaraDos Mod 4 + 1
            CaraCuatro = CaraTres Mod 4 + 1

            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then

                                            CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraUno, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraTres, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraUno, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraUno, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraUno, 5)

                                            ColocarDirectamenteEsquinaInferior()
                                            Exit Sub

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

            End If
        Next

        SalimosConError(57) : Stop
    End Sub

    Private Sub IntercambiarCuatroEsquinasSeguidas()
        If HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE() Then
            IntercambiarCuatroEsquinasSeguidasEnOrdenASCENDENTE()
        ElseIf HayPosibilidadDeIntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE Then
            IntercambiarCuatroEsquinasSeguidasEnOrdenDESCENDENTE()
        Else
            SalimosConError(57) : Stop
        End If
    End Sub


    Private Function HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1

            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then Return True
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarTresEsquinasSeguidas() As Boolean
        Return HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE() Xor HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE()
    End Function


    Private Sub IntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE()
        If Not HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then

                                            CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraDos, 5)

                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraTres, 5)

                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraDos, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraDos, 5)

                                            Exit Sub

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next

        SalimosConError(57) : Stop
    End Sub

    Private Sub IntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE()
        If Not HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1

            If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 0) Then
                            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 2) Then
                                If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                                    If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                                        If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 0) Then

                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraDos, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraDos, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraDos, 5)

                                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)
                                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraTres, 5)
                                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraTres, 5)
                                            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraTres, 5)

                                            Exit Sub

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Next
        SalimosConError(57) : Stop
    End Sub

    Private Sub IntercambiarTresEsquinasSeguidas()
        If HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE() Then
            IntercambiarTresEsquinasSeguidasEnOrdenASCENDENTE()
        ElseIf HayPosibilidadDeIntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE() Then
            IntercambiarTresEsquinasSeguidasEnOrdenDESCENDENTE()
        Else
            SalimosConError(57) : Stop
        End If
    End Sub


    Private Function HayPosibilidadDeIntercambiarDosEsquinasAdyacentes() As Boolean
        Dim CaraActual, CaraSiguiente, CaraAnterior As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 0) Then Return True    
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarDosEsquinasAdyacentes()
        If Not HayPosibilidadDeIntercambiarDosEsquinasAdyacentes() Then SalimosConError(57) : Stop

        Dim CaraActual, CaraSiguiente, CaraAnterior As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            CaraAnterior = (CaraActual + 2) Mod 4 + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 2) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraAnterior, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 0) Then
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)    
                    CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    Exit Sub
                End If
            End If
        Next

        SalimosConError(57) : Stop
    End Sub

    Private Function HayPosibilidadDeIntercambiarDosEsquinasEnfrentadas() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then Return True 
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarDosEsquinasEnfrentadas()
        If Not HayPosibilidadDeIntercambiarDosEsquinasEnfrentadas() Then SalimosConError(57) : Stop

        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = (CaraDos Mod 4) + 1
            CaraCuatro = (CaraTres Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 0) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 2) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 0) Then
                        If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 2) Then
                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraUno, 5) 
                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraUno, 5)
                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraUno, 5)
                            CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraUno, 5)
                            CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraUno, 5)
                            CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraUno, 5)
                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraUno, 5)
                            CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)
                            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraUno, 5)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next

        SalimosConError(57) : Stop
    End Sub


    Private Function HayPosibilidadDeIntercambiarCuatroBordesDiabolicos() As Boolean
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = ((CaraDos + 1) Mod 4) + 1
            CaraCuatro = (CaraDos Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 1) Then
                        If Not SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 1) Then SalimosConError(56) : Stop
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarCuatroBordesDiabolicos()
        If Not HayPosibilidadDeIntercambiarCuatroBordesDiabolicos() Then SalimosConError(55) : Stop
        Dim CaraUno, CaraDos, CaraTres, CaraCuatro As Integer
        For CaraUno = 1 To 4
            CaraDos = (CaraUno Mod 4) + 1
            CaraTres = ((CaraDos + 1) Mod 4) + 1
            CaraCuatro = (CaraDos Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraDos, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraUno), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraTres, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraDos), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraCuatro, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraTres), 1) Then
                        If Not SonDelColorBuscadoTodasLasCasillas(CaraUno, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraCuatro), 1) Then SalimosConError(56) : Stop
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraUno, 5)    
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraUno, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraUno, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraUno, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraUno, 5)
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraUno, 5)

                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraDos, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraDos, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraDos, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraDos, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraDos, 5)

                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraTres, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraTres, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraTres, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraTres, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraTres, 5)
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraTres, 5)

                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraCuatro, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraCuatro, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraCuatro, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraCuatro, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraCuatro, 5)

                        ColocarDirectamenteBordeInferior()
                        Exit Sub
                    End If
                End If
            End If
        Next
        SalimosConError(55) : Stop
    End Sub


    Private Function HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            If NoSonDelColorBuscadoNingunaDeLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then Return False
        Next
        Return True
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = ((CaraActual + 2) Mod 4) + 1
            If NoSonDelColorBuscadoNingunaDeLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then Return False
        Next
        Return True
    End Function

    Private Function HayPosibilidadDeIntercambiarCuatroBordesSeguidos() As Boolean
        Return HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE() Xor HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE()
    End Function


    Private Sub IntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE()
        If Not HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE() Then SalimosConError(55) : Stop
        Dim CaraActual As Integer
        For CaraActual = 1 To 4
            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)

            If CaraActual = 1 Then
                CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
            Else
                CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
            End If

            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)

            If CaraActual = 1 Then
                CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
            Else
                CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
            End If
        Next
        ColocarDirectamenteBordeInferior()
    End Sub

    Private Sub IntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE()
        If Not HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE() Then SalimosConError(55) : Stop
        Dim CaraActual As Integer
        For CaraActual = 4 To 1 Step -1
            CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
            CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)

            If CaraActual = 4 Then
                CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
            Else
                CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
            End If

            CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
            CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)

            If CaraActual = 4 Then
                CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
            Else
                CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
            End If
        Next
        ColocarDirectamenteBordeInferior()

    End Sub

    Private Sub IntercambiarCuatroBordesSeguidos()
        If Not HayPosibilidadDeIntercambiarCuatroBordesSeguidos() Then SalimosConError(55) : Stop
        If HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE() Then
            IntercambiarCuatroBordesSeguidosEnOrdenASCENDENTE()
        ElseIf HayPosibilidadDeIntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE() Then
            IntercambiarCuatroBordesSeguidosEnOrdenDESCENDENTE()
        Else
            SalimosConError(55) : Stop
        End If
    End Sub


    Private Function HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenASCENDENTE() As Boolean
        Dim CaraActual, CaraSiguiente, CaraSiguienteALaSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            CaraSiguienteALaSiguiente = (CaraSiguiente Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguienteALaSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguienteALaSiguiente), 1) Then Return True

                End If
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenDESCENDENTE() As Boolean
        Dim CaraActual, CaraSiguiente, CaraSiguienteALaSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = ((CaraActual + 2) Mod 4) + 1
            CaraSiguienteALaSiguiente = ((CaraSiguiente + 2) Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguienteALaSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguienteALaSiguiente), 1) Then Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIntercambiarTresBordesSeguidos() As Boolean
        Return HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenASCENDENTE() Xor HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenDESCENDENTE()
    End Function

    Private Sub IntercambiarTresBordesSeguidosEnOrdenASCENDENTE()
        If Not HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenASCENDENTE() Then SalimosConError(55) : Stop
        Dim CaraActual, CaraSiguiente, CaraSiguienteALaSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            CaraSiguienteALaSiguiente = (CaraSiguiente Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguienteALaSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguienteALaSiguiente), 1) Then

                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        Exit Sub
                    End If
                End If
            End If
        Next
        SalimosConError(55) : Stop
    End Sub

    Private Sub IntercambiarTresBordesSeguidosEnOrdenDESCENDENTE()
        If Not HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenDESCENDENTE() Then SalimosConError(55) : Stop

        Dim CaraActual, CaraSiguiente, CaraSiguienteALaSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = ((CaraActual + 2) Mod 4) + 1
            CaraSiguienteALaSiguiente = ((CaraSiguiente + 2) Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguienteALaSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                    If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguienteALaSiguiente), 1) Then

                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                        CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                        CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                        CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                        Exit Sub
                    End If
                End If
            End If
        Next

        SalimosConError(55) : Stop
    End Sub

    Private Sub IntercambiarTresBordesSeguidos()
        If HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenASCENDENTE() Then
            IntercambiarTresBordesSeguidosEnOrdenASCENDENTE()
        ElseIf HayPosibilidadDeIntercambiarTresBordesSeguidosEnOrdenDESCENDENTE Then
            IntercambiarTresBordesSeguidosEnOrdenDESCENDENTE()
        Else
            SalimosConError(55) : Stop
        End If
    End Sub


    Private Function HayPosibilidadDeIntercambiarDosBordesEnfrentados() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = ((CaraActual + 1) Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then Return True   
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarDosBordesEnfrentados()
        If Not HayPosibilidadDeIntercambiarDosBordesEnfrentados() Then SalimosConError(55) : Stop
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = ((CaraActual + 1) Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5) 
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    Exit Sub
                End If
            End If
        Next

        SalimosConError(55) : Stop
    End Sub


    Private Function HayPosibilidadDeIntercambiarDosBordesAdyacentes() As Boolean
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then Return True   
            End If
        Next
        Return False
    End Function

    Private Sub IntercambiarDosBordesAdyacentes()
        If Not HayPosibilidadDeIntercambiarDosBordesAdyacentes() Then SalimosConError(55) : Stop
        Dim CaraActual, CaraSiguiente As Integer
        For CaraActual = 1 To 4
            CaraSiguiente = (CaraActual Mod 4) + 1
            If SonDelColorBuscadoTodasLasCasillas(CaraSiguiente, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraActual), 1) Then
                If SonDelColorBuscadoTodasLasCasillas(CaraActual, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraSiguiente), 1) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5) 
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(CaraActual, 5)
                    CuboQueTenemosQueMontar.AbajoGirarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(CaraActual, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(CaraActual, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(CaraActual, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(CaraActual, 5)
                    Exit Sub
                End If
            End If
        Next

        SalimosConError(55) : Stop
    End Sub
    '------------------------
    'PRIMERA LÍNEA.



    Public Sub MontarLaCaraAmarilla()
        If EstaMontadaLaCara(ColorAmarillo) Then
            Dim CadenaDeMensaje As String = "La cara ya está montada, no hay nada que montar"
            Dim TituloDeMensaje As String = "La cara ya está montada"
            MessageBox.Show(CadenaDeMensaje, TituloDeMensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Do While Not EstaMontadaLaCara(ColorAmarillo)
            If HayPosibilidadDeIncrustacionDirectaDeTrioColumna() Then
                IncrustarDirectamenteTrioColumna()
            ElseIf HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumna() Then
                IncrustarINDIRECTAMENTETrioColumna()

            ElseIf HayPosibilidadDeIncrustarDirectamenteTrioColumnaSubterraneo() Then
                IncrustarDirectamenteTrioColumnaSubterraneo()
            ElseIf HayPosibilidadDeIncrustarINDIRECTAMENTETrioColumnaSubterraneo() Then
                IncrustarINDIRECTAMENTETrioColumnaSubterraneo()

            ElseIf HayPosibilidadDeIncrustacionDirectaDeParVertical() Then
                IncrustarDirectamenteParVertical()
            ElseIf HayPosibilidadDeIncrustacionINDIRECTAdeParVertical() Then
                IncrustarINDIRECTAMENTEParVertical()

            ElseIf HayPosibilidadDeIncrustarDirectamenteParSubterraneo() Then
                IncrustarDirectamenteParSubterraneo()
            ElseIf HayPosibilidadDeIncrustarINDIRECTAMENTEDeParSubterraneo() Then
                IncrustarINDIRECTAMENTEParSubterraneo()

            ElseIf HayPosibilidadDeIncrustacionDirectaDeEsquinaSuperior() Then
                IncrustarDirectamenteEsquinaSuperior()
            ElseIf HayPosibilidadDeIncrustacionDirectaDeBordeLateral() Then
                IncrustarDirectamenteBordeLateral()
            ElseIf HayPosibilidadDeIncrustacionDirectaDeEsquinaInferior() Then
                IncrustarDirectamenteEsquinaInferior()

            ElseIf HayPosibilidadDeIncrustacionINDIRECTAdeBordeLateral() Then
                IncrustarINDIRECTAMENTEBordeLateral()
            ElseIf HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaInferior() Then
                IncrustarINDIRECTAMENTEEsquinaInferior()

            ElseIf HayPosibilidadDeColocacionDirectaDeParBordeEsquinaInferior() Then
                ColocarDirectamenteParBordeEsquinaInferior()
            ElseIf HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaInferior() Then
                ColocarINDIRECTAMENTEParBordeEsquinaInferior()

            ElseIf HayPosibilidadDeColocacionDirectaDeParHorizontal() Then
                ColocarDirectamenteParHorizontal()
            ElseIf HayPosibilidadDeColocacionINDIRECTAdeParHorizontal() Then
                ColocarINDIRECTAMENTEParHorizontal()

            ElseIf HayPosibilidadDeColocacionDirectaDeEsquinaInferior() Then
                ColocarDirectamenteEsquinaInferior()
            ElseIf HayPosibilidadDeColocacionDirectaDeBordeInferior() Then
                ColocarDirectamenteBordeInferior()
            ElseIf HayPosibilidadDeColocacionDirectaDeBordeLateral() Then
                ColocarDirectamenteBordeLateral()

            ElseIf HayPosibilidadDeColocacionINDIRECTAdeEsquinaInferior() Then
                ColocarINDIRECTAMENTEEsquinaInferior()
            ElseIf HayPosibilidadDeColocacionINDIRECTAdeBordeInferior() Then
                ColocarINDIRECTAMENTEBordeInferior()
            ElseIf HayPosibilidadDeColocacionINDIRECTAdeBordeLateral() Then
                ColocarINDIRECTAMENTEBordeLateral()

            ElseIf HayPosibilidadDeMeterDeCualquierManeraEsquinaSuperior() Then
                MeterDeCualquierManeraEsquinaSuperior()
            ElseIf HayPosibilidadDeMeterDeCualquierManeraBordeSuperior() Then
                MeterDeCualquierManeraBordeSuperior()

            ElseIf HayPosibilidadDeColocacionDirectaDeBordeSubterrano() Then
                ColocarDirectamenteBordeSubterrano()
            ElseIf HayPosibilidadDeColocacionINDIRECTAdeBordeSubterrano() Then
                ColocarINDIRECTAMENTEBordeSubterrano()

            ElseIf HayPosibilidadDeColocacionDirectaDeEsquinaSubterranea() Then
                ColocarDirectamenteEsquinaSubterranea()
            ElseIf HayPosibilidadDeColocacionINDIRECTADeEsquinaSubterranea() Then
                ColocarIndirectamenteEsquinaSubterranea()



            Else
                SalimosConError(49) : Stop
            End If
        Loop
        '        MessageBox.Show("Se supone que ya está la cara montada")
        '        SimplificarMatrizDeMovimientos(CuboQueTenemosQueMontar.ListaDeMovimientos)
        '        Clipboard.SetText(DeNumeroDeMovimientoACadenaDeTexto(CuboQueTenemosQueMontar.ListaDeMovimientos))
    End Sub


    Private Function HayPosibilidadDeIncrustarINDIRECTAMENTEDeParSubterraneo() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        Dim Contador As Integer
        Dim Resultado As Boolean = False
        For Contador = 1 To 4
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
            If HayPosibilidadDeIncrustarDirectamenteParSubterraneo() Then Resultado = True
        Next
        Return Resultado
    End Function


    Private Function HayPosibilidadDeIncrustarDirectamenteParSubterraneo() As Boolean
        'Como da igual ponerlo por la izquierda o por la derecha (porque si te pones enfrente es el contrario), vamos a suponer que va por la izquierda
        If Not HayArribaAlgunaLineaLibre() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(2), CasillasBlancas(2) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3, 6} : CasillasBlancas = {0, 3, 6}
                Case 2 : CasillasAmarillas = {0, 1, 2} : CasillasBlancas = {6, 7, 8}
                Case 3 : CasillasAmarillas = {2, 5, 8} : CasillasBlancas = {2, 5, 8}
                Case 4 : CasillasAmarillas = {6, 7, 8} : CasillasBlancas = {0, 1, 2}
            End Select
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                Dim ParEscogidoIndices(1), ParEscogidoValores(1) As Integer

                ParEscogidoIndices = {0, 1}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then Return True

                ParEscogidoIndices = {0, 2}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then Return True

                ParEscogidoIndices = {1, 2}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then Return True
            End If
        Next
        Return False
    End Function

    Private Sub IncrustarDirectamenteParSubterraneo()
        If Not HayPosibilidadDeIncrustarDirectamenteParSubterraneo() Then SalimosConError(53) : Stop
        'Como da igual ponerlo por la izquierda o por la derecha (porque si te pones enfrente es el contrario), vamos a suponer que va por la izquierda
        Dim Contador As Integer
        Dim CasillasAmarillas(2), CasillasBlancas(2) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3, 6} : CasillasBlancas = {0, 3, 6}
                Case 2 : CasillasAmarillas = {0, 1, 2} : CasillasBlancas = {6, 7, 8}
                Case 3 : CasillasAmarillas = {2, 5, 8} : CasillasBlancas = {2, 5, 8}
                Case 4 : CasillasAmarillas = {6, 7, 8} : CasillasBlancas = {0, 1, 2}
            End Select
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                Dim ParEscogidoIndices(1), ParEscogidoValores(1) As Integer

                ParEscogidoIndices = {0, 1}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If

                ParEscogidoIndices = {0, 2}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If

                ParEscogidoIndices = {1, 2}
                ParEscogidoValores = {CasillasBlancas(ParEscogidoIndices(0)), CasillasBlancas(ParEscogidoIndices(1))}
                If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), ParEscogidoValores) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(53) : Stop
    End Sub

    Private Sub IncrustarINDIRECTAMENTEParSubterraneo()
        If Not HayPosibilidadDeIncrustarINDIRECTAMENTEDeParSubterraneo() Then SalimosConError(54) : Stop
        Do While Not HayPosibilidadDeIncrustarDirectamenteParSubterraneo()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        IncrustarDirectamenteParSubterraneo()
    End Sub


    Private Function HayPosibilidadDeIncrustarDirectamenteTrioColumnaSubterraneo() As Boolean
        'Como da igual ponerlo por la izquierda o por la derecha (porque si te pones enfrente es el contrario), vamos a suponer que va por la izquierda
        If Not HayArribaAlgunaLineaLibre() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(), CasillasBlancas() As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3, 6} : CasillasBlancas = {0, 3, 6}
                Case 2 : CasillasAmarillas = {0, 1, 2} : CasillasBlancas = {6, 7, 8}
                Case 3 : CasillasAmarillas = {2, 5, 8} : CasillasBlancas = {2, 5, 8}
                Case 4 : CasillasAmarillas = {6, 7, 8} : CasillasBlancas = {0, 1, 2}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillasBlancas) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustarINDIRECTAMENTETrioColumnaSubterraneo() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        Dim Contador As Integer
        Dim CasillasBlancas() As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasBlancas = {0, 3, 6}
                Case 2 : CasillasBlancas = {6, 7, 8}
                Case 3 : CasillasBlancas = {2, 5, 8}
                Case 4 : CasillasBlancas = {0, 1, 2}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillasBlancas) Then Return True
        Next
        Return False
    End Function

    Private Sub IncrustarDirectamenteTrioColumnaSubterraneo()
        If Not HayPosibilidadDeIncrustarDirectamenteTrioColumnaSubterraneo() Then SalimosConError(51) : Stop
        Dim Contador As Integer
        Dim CasillasAmarillas(), CasillasBlancas() As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3, 6} : CasillasBlancas = {0, 3, 6}
                Case 2 : CasillasAmarillas = {0, 1, 2} : CasillasBlancas = {6, 7, 8}
                Case 3 : CasillasAmarillas = {2, 5, 8} : CasillasBlancas = {2, 5, 8}
                Case 4 : CasillasAmarillas = {6, 7, 8} : CasillasBlancas = {0, 1, 2}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillasBlancas) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(51) : Stop
    End Sub

    Private Sub IncrustarINDIRECTAMENTETrioColumnaSubterraneo()
        If Not HayPosibilidadDeIncrustarINDIRECTAMENTETrioColumnaSubterraneo() Then SalimosConError(52) : Stop
        Do While Not HayPosibilidadDeIncrustarDirectamenteTrioColumnaSubterraneo()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        IncrustarDirectamenteTrioColumnaSubterraneo()
    End Sub


    Private Function HayPosibilidadDeMeterDeCualquierManeraBordeSuperior() As Boolean
        Return HayAbajoAlgunBordeSuperior()
    End Function

    Private Sub MeterDeCualquierManeraBordeSuperior()
        If Not HayPosibilidadDeMeterDeCualquierManeraBordeSuperior() Then SalimosConError(58) : Stop
        Dim Contador As Integer
        For Contador = 1 To 4
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 1) Then
                CuboQueTenemosQueMontar.IzquierdaGirarAbajo(Contador, 5)
                CuboQueTenemosQueMontar.DerechaGirarAbajo(Contador, 5)
                CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                CuboQueTenemosQueMontar.ArribaGirarIzquierda(Contador, 5)
                CuboQueTenemosQueMontar.AbajoGirarIzquierda(Contador, 5)
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                Exit Sub
            End If
        Next
        SalimosConError(58) : Stop
    End Sub


    Private Function HayPosibilidadDeMeterDeCualquierManeraEsquinaIzquierdaSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 0) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeMeterDeCualquierManeraEsquinaDerechaSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 2) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeMeterDeCualquierManeraEsquinaSuperior() As Boolean
        Return HayPosibilidadDeMeterDeCualquierManeraEsquinaIzquierdaSuperior() Or HayPosibilidadDeMeterDeCualquierManeraEsquinaDerechaSuperior()
    End Function

    Private Sub MeterDeCualquierManeraEsquinaIzquierdaSuperior()
        If Not HayPosibilidadDeMeterDeCualquierManeraEsquinaIzquierdaSuperior() Then SalimosConError(50) : Stop
        Dim Contador As Integer
        For Contador = 1 To 4
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 0) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                Exit Sub
            End If
        Next
        SalimosConError(50) : Stop
    End Sub

    Private Sub MeterDeCualquierManeraEsquinaDerechaSuperior()
        If Not HayPosibilidadDeMeterDeCualquierManeraEsquinaDerechaSuperior() Then SalimosConError(50) : Stop
        Dim Contador As Integer
        For Contador = 1 To 4
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 2) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                Exit Sub
            End If
        Next
        SalimosConError(50) : Stop
    End Sub

    Private Sub MeterDeCualquierManeraEsquinaSuperior()
        If HayPosibilidadDeMeterDeCualquierManeraEsquinaIzquierdaSuperior() Then
            MeterDeCualquierManeraEsquinaIzquierdaSuperior()
        ElseIf HayPosibilidadDeMeterDeCualquierManeraEsquinaDerechaSuperior() Then
            MeterDeCualquierManeraEsquinaDerechaSuperior()
        Else
            SalimosConError(50) : Stop
        End If
    End Sub


    Private Sub ColocarDirectamenteParBordeEsquinaIzquierdoInferior()
        If Not HayPosibilidadDeColocacionDirectaDeParBordeEsquinaIzquierdoInferior() Then SalimosConError(47) : Stop
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 1}
                Case 2 : CasillasAmarillas = {2, 5}
                Case 3 : CasillasAmarillas = {7, 8}
                Case 4 : CasillasAmarillas = {3, 6}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 3, 6) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.ArribaGirarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(47) : Stop
    End Sub

    Private Sub ColocarDirectamenteParBordeEsquinaDerechoInferior()
        If Not HayPosibilidadDeColocacionDirectaDeParBordeEsquinaDerechoInferior() Then SalimosConError(47) : Stop

        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {1, 2}
                Case 2 : CasillasAmarillas = {5, 8}
                Case 3 : CasillasAmarillas = {6, 7}
                Case 4 : CasillasAmarillas = {0, 3}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 5, 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.ArribaGirarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(47) : Stop
    End Sub

    Private Sub ColocarDirectamenteParBordeEsquinaInferior()
        If Not HayPosibilidadDeColocacionDirectaDeParBordeEsquinaInferior() Then SalimosConError(47) : Stop
        If HayPosibilidadDeColocacionDirectaDeParBordeEsquinaIzquierdoInferior() Then
            ColocarDirectamenteParBordeEsquinaIzquierdoInferior()
        ElseIf HayPosibilidadDeColocacionDirectaDeParBordeEsquinaDerechoInferior Then
            ColocarDirectamenteParBordeEsquinaDerechoInferior()
        Else
            SalimosConError(47) : Stop
        End If
    End Sub

    Private Sub ColocarINDIRECTAMENTEParBordeEsquinaInferior()
        If Not HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaInferior() Then SalimosConError(48) : Stop

        If (HayAbajoAlgunParBordeEsquinaIzquierdoInferior() AndAlso HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaIzquierdoInferior()) Or
                (HayAbajoAlgunParBordeEsquinaDerechoInferior() AndAlso HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaDerechoInferior()) Then
            Do While Not HayPosibilidadDeColocacionDirectaDeParBordeEsquinaInferior()
                CuboQueTenemosQueMontar.AlanteRotarIzquierda()
            Loop
            ColocarDirectamenteParBordeEsquinaInferior()
        Else
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
            ColocarINDIRECTAMENTEParBordeEsquinaInferior()
        End If
    End Sub


    Private Function HayPosibilidadDeColocacionDirectaDeEsquinaSubterranea() As Boolean
        If Not HayAbajoDelTodoAlgunaEsquina() Then Return False
        Dim Contador, CasillaAmarilla, CasillaBlanca As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 8 : CasillaBlanca = 2
                Case 2 : CasillaAmarilla = 6 : CasillaBlanca = 0
                Case 3 : CasillaAmarilla = 0 : CasillaBlanca = 6
                Case 4 : CasillaAmarilla = 2 : CasillaBlanca = 8
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTADeEsquinaSubterranea() As Boolean
        Return HayAbajoDelTodoAlgunaEsquina()
    End Function

    Private Sub ColocarDirectamenteEsquinaSubterranea()
        If Not HayPosibilidadDeColocacionDirectaDeEsquinaSubterranea() Then SalimosConError(44) : Stop
        Dim Contador, CasillaAmarilla, CasillaBlanca As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 8 : CasillaBlanca = 2
                Case 2 : CasillaAmarilla = 6 : CasillaBlanca = 0
                Case 3 : CasillaAmarilla = 0 : CasillaBlanca = 6
                Case 4 : CasillaAmarilla = 2 : CasillaBlanca = 8
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.ArribaGirarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(44) : Stop
    End Sub

    Private Sub ColocarIndirectamenteEsquinaSubterranea()
        If Not HayPosibilidadDeColocacionIndirectaDeEsquinaSubterranea() Then SalimosConError(45) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeEsquinaSubterranea()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        ColocarDirectamenteEsquinaSubterranea()
    End Sub


    Private Function HayPosibilidadDeColocacionDirectaDeBordeSubterrano() As Boolean
        If Not HayAbajoDelTodoAlgunBorde() Then Return False
        Dim Contador, CasillaAmarilla, CasillaBlanca As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 7 : CasillaBlanca = 1
                Case 2 : CasillaAmarilla = 3 : CasillaBlanca = 3
                Case 3 : CasillaAmarilla = 1 : CasillaBlanca = 7
                Case 4 : CasillaAmarilla = 5 : CasillaBlanca = 5
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeBordeSubterrano() As Boolean
        Return HayAbajoDelTodoAlgunBorde()
    End Function

    Private Sub ColocarDirectamenteBordeSubterrano()
        If Not HayPosibilidadDeColocacionDirectaDeBordeSubterrano() Then SalimosConError(42) : Stop
        Dim Contador, CasillaAmarilla, CasillaBlanca As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 7 : CasillaBlanca = 1
                Case 2 : CasillaAmarilla = 3 : CasillaBlanca = 3
                Case 3 : CasillaAmarilla = 1 : CasillaBlanca = 7
                Case 4 : CasillaAmarilla = 5 : CasillaBlanca = 5
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(5), CasillaBlanca) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.IzquierdaGirarAbajo(Contador, 5)
                    CuboQueTenemosQueMontar.DerechaGirarAbajo(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(42) : Stop
    End Sub

    Private Sub ColocarINDIRECTAMENTEBordeSubterrano()
        If Not HayPosibilidadDeColocacionINDIRECTAdeBordeSubterrano() Then SalimosConError(43) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeBordeSubterrano()
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Loop
        ColocarDirectamenteBordeSubterrano()
    End Sub


    Private Sub ColocarDirectamenteParCentroIzquierda()
        If Not HayPosibilidadDeColocacionDirectaDeParCentroIzquierda() Then SalimosConError(40) : Stop
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3}
                Case 2 : CasillasAmarillas = {1, 2}
                Case 3 : CasillasAmarillas = {5, 8}
                Case 4 : CasillasAmarillas = {6, 7}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 6, 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(40) : Stop
    End Sub

    Private Sub ColocarDirectamenteParCentroDerecha()
        If Not HayPosibilidadDeColocacionDirectaDeParCentroDerecha() Then SalimosConError(40) : Stop
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {2, 5}
                Case 2 : CasillasAmarillas = {7, 8}
                Case 3 : CasillasAmarillas = {3, 6}
                Case 4 : CasillasAmarillas = {0, 1}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7, 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(40) : Stop
    End Sub

    Private Sub ColocarDirectamenteParHorizontal()
        If HayPosibilidadDeColocacionDirectaDeParCentroIzquierda() Then
            ColocarDirectamenteParCentroIzquierda()
        ElseIf HayPosibilidadDeColocacionDirectaDeParCentroDerecha Then
            ColocarDirectamenteParCentroDerecha()
        Else
            SalimosConError(40) : Stop
        End If
    End Sub

    Private Sub ColocarINDIRECTAMENTEParCentroIzquierda()
        If Not HayPosibilidadDeColocacionINDIRECTAdeParCentroIzquierda() Then SalimosConError(41) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeParCentroIzquierda()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        ColocarDirectamenteParCentroIzquierda()
    End Sub

    Private Sub ColocarINDIRECTAMENTEParCentroDerecha()
        If Not HayPosibilidadDeColocacionINDIRECTAdeParCentroDerecha() Then SalimosConError(41) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeParCentroDerecha()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        ColocarDirectamenteParCentroDerecha()
    End Sub

    Private Sub ColocarINDIRECTAMENTEParHorizontal()
        If HayPosibilidadDeColocacionDirectaDeParHorizontal() Then
            ColocarDirectamenteParHorizontal()
        ElseIf HayPosibilidadDeColocacionINDIRECTAdeParCentroIzquierda() Then
            ColocarINDIRECTAMENTEParCentroIzquierda()
        ElseIf HayPosibilidadDeColocacionINDIRECTAdeParCentroDerecha() Then
            ColocarINDIRECTAMENTEParCentroDerecha()
        Else
            SalimosConError(41) : Stop
        End If
    End Sub


    Private Function HayPosibilidadDeColocacionDirectaDeParCentroIzquierda() As Boolean
        If Not HayAbajoAlgunParCentroIzquierda() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 3}
                Case 2 : CasillasAmarillas = {1, 2}
                Case 3 : CasillasAmarillas = {5, 8}
                Case 4 : CasillasAmarillas = {6, 7}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 6, 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeParCentroDerecha() As Boolean
        If Not HayAbajoAlgunParCentroDerecha() Then Return False
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {2, 5}
                Case 2 : CasillasAmarillas = {7, 8}
                Case 3 : CasillasAmarillas = {3, 6}
                Case 4 : CasillasAmarillas = {0, 1}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7, 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeParHorizontal() As Boolean
        Return HayPosibilidadDeColocacionDirectaDeParCentroIzquierda() Or HayPosibilidadDeColocacionDirectaDeParCentroDerecha()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParCentroIzquierda() As Boolean
        Return HayAbajoAlgunParCentroIzquierda() And HayArribaAlgunParLibreDeCentroIzquierda()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParCentroDerecha() As Boolean
        Return HayAbajoAlgunParCentroDerecha() And HayArribaAlgunParLibreDeCentroDerecha()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParHorizontal() As Boolean
        Return HayPosibilidadDeColocacionINDIRECTAdeParCentroIzquierda() Or HayPosibilidadDeColocacionINDIRECTAdeParCentroDerecha()
    End Function


    Private Function HayPosibilidadDeColocacionDirectaDeParBordeEsquinaIzquierdoInferior() As Boolean
        If Not HayAbajoAlgunParBordeEsquinaIzquierdoInferior() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 1}
                Case 2 : CasillasAmarillas = {2, 5}
                Case 3 : CasillasAmarillas = {7, 8}
                Case 4 : CasillasAmarillas = {3, 6}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 3, 6) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeParBordeEsquinaDerechoInferior() As Boolean
        If Not HayAbajoAlgunParBordeEsquinaDerechoInferior() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {1, 2}
                Case 2 : CasillasAmarillas = {5, 8}
                Case 3 : CasillasAmarillas = {6, 7}
                Case 4 : CasillasAmarillas = {0, 3}
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 5, 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeParBordeEsquinaInferior() As Boolean
        Return HayPosibilidadDeColocacionDirectaDeParBordeEsquinaIzquierdoInferior() Or HayPosibilidadDeColocacionDirectaDeParBordeEsquinaDerechoInferior()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaIzquierdoInferior() As Boolean
        If Not HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoInferior() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {0, 1}
                Case 2 : CasillasAmarillas = {2, 5}
                Case 3 : CasillasAmarillas = {7, 8}
                Case 4 : CasillasAmarillas = {3, 6}
            End Select
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaDerechoInferior() As Boolean
        If Not HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoInferior() Then Return False
        Dim Contador As Integer
        Dim CasillasAmarillas(1) As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillasAmarillas = {1, 2}
                Case 2 : CasillasAmarillas = {5, 8}
                Case 3 : CasillasAmarillas = {6, 7}
                Case 4 : CasillasAmarillas = {0, 3}
            End Select
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillasAmarillas) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaInferior() As Boolean
        Return HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaIzquierdoInferior() Or HayPosibilidadDeColocacionINDIRECTAdeParBordeEsquinaDerechoInferior()
    End Function


    Private Function HayPosibilidadDeColocacionDirectaDeBordeIzquierdo() As Boolean
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 1
                Case 2 : CasillaAmarilla = 5
                Case 3 : CasillaAmarilla = 7
                Case 4 : CasillaAmarilla = 3
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 3) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeBordeDerecho() As Boolean
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 1
                Case 2 : CasillaAmarilla = 5
                Case 3 : CasillaAmarilla = 7
                Case 4 : CasillaAmarilla = 3
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 5) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeBordeLateral() As Boolean
        Return HayPosibilidadDeColocacionDirectaDeBordeIzquierdo() Or HayPosibilidadDeColocacionDirectaDeBordeDerecho()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeBordeIzquierdo() As Boolean
        Return HayAbajoAlgunBordeIzquierdo()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeBordeDerecho() As Boolean
        Return HayAbajoAlgunBordeDerecho()
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeBordeLateral() As Boolean
        Return HayPosibilidadDeColocacionINDIRECTAdeBordeIzquierdo() Or HayPosibilidadDeColocacionINDIRECTAdeBordeDerecho()
    End Function


    Private Sub ColocarDirectamenteBordeIzquierdo()
        If Not HayPosibilidadDeColocacionDirectaDeBordeIzquierdo() Then SalimosConError(38) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 1
                Case 2 : CasillaAmarilla = 5
                Case 3 : CasillaAmarilla = 7
                Case 4 : CasillaAmarilla = 3
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 3) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.ArribaGirarDerecha(Contador, 5)
                    If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik((Contador Mod 4) + 1), 6) Then
                        CuboQueTenemosQueMontar.AbajoGirarDerecha(Contador, 5)
                    End If
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(38) : Stop
    End Sub

    Private Sub ColocarDirectamenteBordeDerecho()
        If Not HayPosibilidadDeColocacionDirectaDeBordeDerecho() Then SalimosConError(38) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 1
                Case 2 : CasillaAmarilla = 5
                Case 3 : CasillaAmarilla = 7
                Case 4 : CasillaAmarilla = 3
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 5) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.ArribaGirarIzquierda(Contador, 5)
                    If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(((Contador + 2) Mod 4) + 1), 8) Then
                        CuboQueTenemosQueMontar.AbajoGirarIzquierda(Contador, 5)
                    End If
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(38) : Stop
    End Sub

    Private Sub ColocarDirectamenteBordeLateral()
        If Not HayPosibilidadDeColocacionDirectaDeBordeLateral() Then SalimosConError(38) : Stop
        If HayPosibilidadDeColocacionDirectaDeBordeIzquierdo() Then
            ColocarDirectamenteBordeIzquierdo()
        ElseIf HayPosibilidadDeColocacionDirectaDeBordeDerecho() Then
            ColocarDirectamenteBordeDerecho()
        Else
            SalimosConError(38) : Stop
        End If
    End Sub

    Private Sub ColocarINDIRECTAMENTEBordeLateral()
        If Not HayPosibilidadDeColocacionINDIRECTAdeBordeLateral() Then SalimosConError(39) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeBordeLateral()
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Loop
        ColocarDirectamenteBordeLateral()
    End Sub


    Private Sub ColocarDirectamenteEsquinaIzquierdaInferior()
        If Not HayPosibilidadDeColocacionDirectaDeEsquinaIzquierdaInferior() Then SalimosConError(35) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 0
                Case 2 : CasillaAmarilla = 2
                Case 3 : CasillaAmarilla = 8
                Case 4 : CasillaAmarilla = 6
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 6) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AbajoGirarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(35) : Stop
    End Sub

    Private Sub ColocarDirectamenteEsquinaDerechaInferior()
        If Not HayPosibilidadDeColocacionDirectaDeEsquinaDerechaInferior() Then SalimosConError(35) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 2
                Case 2 : CasillaAmarilla = 8
                Case 3 : CasillaAmarilla = 6
                Case 4 : CasillaAmarilla = 0
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.AbajoGirarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(35) : Stop
    End Sub

    Private Sub ColocarDirectamenteEsquinaInferior()
        If HayPosibilidadDeColocacionDirectaDeEsquinaIzquierdaInferior() Then
            ColocarDirectamenteEsquinaIzquierdaInferior()
        ElseIf HayPosibilidadDeColocacionDirectaDeEsquinaDerechaInferior Then
            ColocarDirectamenteEsquinaDerechaInferior()
        Else
            SalimosConError(35) : Stop
        End If
    End Sub

    Private Sub ColocarINDIRECTAMENTEEsquinaInferior()
        If Not HayPosibilidadDeColocacionINDIRECTAdeEsquinaInferior() Then SalimosConError(37) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeEsquinaInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        ColocarDirectamenteEsquinaInferior()
    End Sub


    Private Sub ColocarDirectamenteBordeInferiorPorLaIzquierda()
        If Not HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaIzquierda() Then SalimosConError(36) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 3
                Case 2 : CasillaAmarilla = 1
                Case 3 : CasillaAmarilla = 5
                Case 4 : CasillaAmarilla = 7
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.IzquierdaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(36) : Stop
    End Sub

    Private Sub ColocarDirectamenteBordeInferiorPorLaDerecha()
        If Not HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaDerecha() Then SalimosConError(36) : Stop
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 5
                Case 2 : CasillaAmarilla = 7
                Case 3 : CasillaAmarilla = 3
                Case 4 : CasillaAmarilla = 1
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda(Contador, 5)
                    CuboQueTenemosQueMontar.DerechaGirarArriba(Contador, 5)
                    CuboQueTenemosQueMontar.AtrasRotarDerecha(Contador, 5)
                    CuboQueTenemosQueMontar.AlanteRotarDerecha(Contador, 5)
                    Exit Sub
                End If
            End If
        Next
        SalimosConError(36) : Stop
    End Sub

    Private Sub ColocarDirectamenteBordeInferior()
        If HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaIzquierda() Then
            ColocarDirectamenteBordeInferiorPorLaIzquierda()
        ElseIf HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaDerecha() Then
            ColocarDirectamenteBordeInferiorPorLaDerecha()
        Else
            SalimosConError(36) : Stop
        End If
    End Sub

    Private Sub ColocarINDIRECTAMENTEBordeInferior()
        If Not HayPosibilidadDeColocacionINDIRECTAdeBordeInferior() Then SalimosConError(36) : Stop
        Do While Not HayPosibilidadDeColocacionDirectaDeBordeInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        ColocarDirectamenteBordeInferior()
    End Sub



    Private Function HayPosibilidadDeColocacionDirectaDeEsquinaIzquierdaInferior() As Boolean
        If Not HayAbajoAlgunaEsquinaIzquierdaInferior() Then Return False
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 0
                Case 2 : CasillaAmarilla = 2
                Case 3 : CasillaAmarilla = 8
                Case 4 : CasillaAmarilla = 6
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 6) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeEsquinaDerechaInferior() As Boolean
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 2
                Case 2 : CasillaAmarilla = 8
                Case 3 : CasillaAmarilla = 6
                Case 4 : CasillaAmarilla = 0
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 8) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeEsquinaInferior() As Boolean
        Return HayPosibilidadDeColocacionDirectaDeEsquinaIzquierdaInferior() Or HayPosibilidadDeColocacionDirectaDeEsquinaDerechaInferior()
    End Function


    Private Function HayPosibilidadDeColocacionINDIRECTAdeEsquinaIzquierdaInferior() As Boolean
        If Not HayAbajoAlgunaEsquinaIzquierdaInferior() Then Return False
        Dim Contador As Integer
        For Contador = 0 To 8 Step 2
            If Contador = 4 Then Continue For
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), Contador) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeEsquinaDerechaInferior() As Boolean
        If Not HayAbajoAlgunaEsquinaDerechaInferior() Then Return False
        Dim Contador As Integer
        For Contador = 0 To 8 Step 2
            If Contador = 4 Then Continue For
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), Contador) Then Return True
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionINDIRECTAdeEsquinaInferior() As Boolean
        Return HayPosibilidadDeColocacionINDIRECTAdeEsquinaIzquierdaInferior() Or HayPosibilidadDeColocacionINDIRECTAdeEsquinaDerechaInferior()
    End Function


    Private Function HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaIzquierda() As Boolean
        If Not HayAbajoAlgunBordeInferior() Then Return False
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 3
                Case 2 : CasillaAmarilla = 1
                Case 3 : CasillaAmarilla = 5
                Case 4 : CasillaAmarilla = 7
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaDerecha() As Boolean
        If Not HayAbajoAlgunBordeInferior() Then Return False
        Dim Contador, CasillaAmarilla As Integer
        For Contador = 1 To 4
            Select Case Contador
                Case 1 : CasillaAmarilla = 5
                Case 2 : CasillaAmarilla = 7
                Case 3 : CasillaAmarilla = 3
                Case 4 : CasillaAmarilla = 1
            End Select
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador), 7) Then
                If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), CasillaAmarilla) Then Return True
            End If
        Next
        Return False
    End Function

    Private Function HayPosibilidadDeColocacionDirectaDeBordeInferior() As Boolean
        Return HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaIzquierda() Or HayPosibilidadDeColocacionDirectaDeBordeInferiorPorLaDerecha()
    End Function


    Private Function HayPosibilidadDeColocacionINDIRECTAdeBordeInferior() As Boolean
        Return HayAbajoAlgunBordeInferior()
    End Function


    Private Sub IncrustarINDIRECTAMENTEParVertical()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeParVertical() Then SalimosConError(32)
        If HayPosibilidadDeIncrustacionDirectaDeParVertical() Then IncrustarDirectamenteParVertical() : Exit Sub
        If HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaSuperior() Then
            If HayAbajoAlgunBordeIzquierdo() Then
                Do While Not HayPosibilidadDeIncrustacionDirectaDeParVertical()
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda()
                Loop
                IncrustarDirectamenteParVertical() : Exit Sub
            ElseIf HayAbajoAlgunaEsquinaIzquierdaInferior() Then
                Do While Not HayPosibilidadDeIncrustacionDirectaDeParVertical()
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda()
                Loop
                IncrustarDirectamenteParVertical() : Exit Sub
            End If
        End If

        If HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaSuperior() Then
            If HayAbajoAlgunBordeDerecho() Then
                Do While Not HayPosibilidadDeIncrustacionDirectaDeParVertical()
                    CuboQueTenemosQueMontar.AlanteRotarIzquierda()
                Loop
                IncrustarDirectamenteParVertical() : Exit Sub
            ElseIf HayAbajoAlgunaEsquinaDerechaInferior() Then
                Do While Not HayPosibilidadDeIncrustacionDirectaDeParVertical()
                    CuboQueTenemosQueMontar.AtrasRotarIzquierda()
                Loop
                IncrustarDirectamenteParVertical() : Exit Sub
            End If
        End If

        If HayAbajoAlgunParBordeEsquinaInferior() Then
            Do While Not HayPosibilidadDeIncrustacionDirectaDeParVertical()
                CuboQueTenemosQueMontar.AlanteRotarIzquierda()
            Loop
            IncrustarDirectamenteParVertical() : Exit Sub
        Else
            FormarParBordeEsquinaInferior()
            IncrustarINDIRECTAMENTEParVertical() : Exit Sub
        End If
        SalimosConError(32) : Stop
    End Sub


    Private Sub FormarParBordeEsquinaIzquierdoInferior()
        If Not HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoInferior() Then SalimosConError(28) : Stop
        Do While Not HayAbajoAlgunParBordeEsquinaIzquierdoInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
    End Sub

    Private Sub FormarParBordeEsquinaDerechoInferior()
        If Not HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoInferior() Then SalimosConError(28) : Stop
        Do While Not HayAbajoAlgunParBordeEsquinaDerechoInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
    End Sub

    Private Sub FormarParBordeEsquinaInferior()
        If Not HayAbajoPosibilidadDeAlgunParBordeEsquinaInferior() Then SalimosConError(28) : Stop
        Do While Not HayAbajoAlgunParBordeEsquinaInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
    End Sub


    Private Sub IncrustarINDIRECTAMENTETrioColumnaPorLaIzquierda()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaIzquierda() Then SalimosConError(31) : Stop
        If HayPosibilidadDeIncrustacionDirectaDeTrioColumna() Then IncrustarDirectamenteTrioColumna() : Exit Sub
        FormarParBordeEsquinaIzquierdoInferior()
        Do While Not HayPosibilidadDeIncrustacionDirectaDeTrioColumna()
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Loop
        IncrustarDirectamenteTrioColumna()
    End Sub

    Private Sub IncrustarINDIRECTAMENTETrioColumnaPorLaDerecha()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaDerecha() Then SalimosConError(31) : Stop
        If HayPosibilidadDeIncrustacionDirectaDeTrioColumna() Then IncrustarDirectamenteTrioColumna() : Exit Sub
        FormarParBordeEsquinaDerechoInferior()
        Do While Not HayPosibilidadDeIncrustacionDirectaDeTrioColumna()
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Loop
        IncrustarDirectamenteTrioColumna()
    End Sub

    Private Sub IncrustarINDIRECTAMENTETrioColumna()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumna() Then SalimosConError(31) : Stop
        If HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaIzquierda() Then
            IncrustarINDIRECTAMENTETrioColumnaPorLaIzquierda()
        ElseIf HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaDerecha() Then
            IncrustarINDIRECTAMENTETrioColumnaPorLaDerecha()
        Else
            SalimosConError(31) : Stop
        End If
    End Sub


    Private Sub IncrustarDirectamenteTrioColumna()
        If Not HayPosibilidadDeIncrustacionDirectaDeTrioColumna() Then SalimosConError(23) : Stop
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(3, 5) : Exit Sub
            End If
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(4, 5) : Exit Sub
            End If
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(3, 5) : Exit Sub
            End If
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(4, 5) : Exit Sub
            End If
        End If
        SalimosConError(23) : Stop
    End Sub

    Private Sub IncrustarDirectamenteParVertical()
        If Not HayPosibilidadDeIncrustacionDirectaDeParVertical() Then SalimosConError(24) : Stop
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 3) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 6) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 5) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 5, 8) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 3) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 6) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 5) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 5, 8) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 5) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 8) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 3) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 6) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 5) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 8) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 5, 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 3) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 6) Or
                    SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 3, 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        SalimosConError(24) : Stop
    End Sub

    Private Sub IncrustarDirectamenteEsquinaSuperior()
        If Not HayPosibilidadDeIncrustacionDirectaDeEsquinaSuperior() Then SalimosConError(25) : Stop
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        SalimosConError(25) : Stop
    End Sub

    Private Sub IncrustarDirectamenteBordeLateral()
        If Not HayPosibilidadDeIncrustacionDirectaDeBordeLateral() Then SalimosConError(26) : Stop

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 3) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 5) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 3) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 5) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 5) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 3) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 5) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 3) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        SalimosConError(26) : Stop
    End Sub

    Private Sub IncrustarDirectamenteEsquinaInferior()
        If Not HayPosibilidadDeIncrustacionDirectaDeEsquinaInferior() Then SalimosConError(27) : Stop

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(1, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(3, 5) : Exit Sub
            End If
        End If

        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 8) Then
                CuboQueTenemosQueMontar.DerechaGirarArriba(2, 5) : Exit Sub
            End If
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 6) Then
                CuboQueTenemosQueMontar.IzquierdaGirarArriba(4, 5) : Exit Sub
            End If
        End If

        SalimosConError(27) : Stop
    End Sub


    Private Sub IncrustarINDIRECTAMENTEBordeLateral()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeBordeLateral() Then SalimosConError(33) : Stop
        Do While Not HayPosibilidadDeIncrustacionDirectaDeBordeLateral()
            CuboQueTenemosQueMontar.AlanteRotarIzquierda()
        Loop
        IncrustarDirectamenteBordeLateral()
    End Sub

    Private Sub IncrustarINDIRECTAMENTEEsquinaInferior()
        If Not HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaInferior() Then SalimosConError(34) : Stop
        Do While Not HayPosibilidadDeIncrustacionDirectaDeEsquinaInferior()
            CuboQueTenemosQueMontar.AtrasRotarIzquierda()
        Loop
        IncrustarDirectamenteEsquinaInferior()
    End Sub


    Private Function HayPosibilidadDeIncrustacionDirectaDeTrioColumna() As Boolean
        If Not (HayArribaAlgunaLineaLibre() And HayAbajoAlgunTrioColumna()) Then Return False
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 3, 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 5, 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 3, 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 5, 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 3, 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 5, 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 3, 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 5, 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaIzquierda() As Boolean
        Return HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaSuperior() And HayAbajoPosibilidadDeAlgunTrioColumnaIzquierdo()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaDerecha() As Boolean
        Return HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaSuperior() And HayAbajoPosibilidadDeAlgunTrioColumnaDerecho()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumna() As Boolean
        Return HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaIzquierda() Or HayPosibilidadDeIncrustacionINDIRECTAdeTrioColumnaPorLaDerecha()
    End Function


    Private Function HayPosibilidadDeIncrustacionDirectaDeParVertical() As Boolean
        If Not (HayArribaAlgunaLineaLibre() And (HayAbajoAlgunParBordeEsquinaInferior() Or HayAbajoAlgunParBordeEsquinaSuperior() Or HayAbajoAlgunParDeEsquinas())) Then Return False
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 3) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0, 6) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 3, 6) Then Return True

            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 5) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 5, 8) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2, 8) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 5) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 5, 8) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2, 8) Then Return True

            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 3) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 3, 6) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0, 6) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 5) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 5, 8) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2, 8) Then Return True

            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 3) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 3, 6) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0, 6) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 3) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0, 6) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 3, 6) Then Return True

            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 5) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 5, 8) Then Return True
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2, 8) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeParVertical() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If HayAbajoPosibilidadDeAlgunParBordeEsquinaInferior() Then Return True
        If HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaSuperior() Then
            If HayAbajoAlgunaEsquinaIzquierdaInferior() Or HayAbajoAlgunBordeIzquierdo() Then Return True
        End If
        If HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaSuperior() Then
            If HayAbajoAlgunaEsquinaDerechaInferior() Or HayAbajoAlgunBordeDerecho() Then Return True
        End If
        Return False
    End Function


    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaSuperior() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 0) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 0) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 0) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 0) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaSuperior() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 2) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 2) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 2) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 2) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaSuperior() As Boolean
        Return HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaSuperior() Or HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaSuperior()
    End Function


    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaInferior() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 6) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaInferior() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then Return True
        End If
        If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 8) Then
            If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeEsquinaInferior() As Boolean
        Return HayPosibilidadDeIncrustacionDirectaDeEsquinaIzquierdaInferior() Or HayPosibilidadDeIncrustacionDirectaDeEsquinaDerechaInferior()
    End Function


    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaIzquierdaInferior() As Boolean
        Return HayAbajoAlgunaEsquinaIzquierdaInferior() And HayArribaAlgunaLineaLibre()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaDerechaInferior() As Boolean
        Return HayAbajoAlgunaEsquinaDerechaInferior() And HayArribaAlgunaLineaLibre()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaInferior() As Boolean
        Return HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaIzquierdaInferior() Or HayPosibilidadDeIncrustacionINDIRECTAdeEsquinaDerechaInferior()
    End Function


    Private Function HayPosibilidadDeIncrustacionDirectaDeBordeIzquierdo() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If Not HayAbajoAlgunBordeIzquierdo() Then Return False
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 3) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 3) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 3) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 3) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeBordeDerecho() As Boolean
        If Not HayArribaAlgunaLineaLibre() Then Return False
        If Not HayAbajoAlgunBordeDerecho() Then Return False
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 3, 6) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(3), 5) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 2, 5, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(1), 5) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 0, 1, 2) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(4), 5) Then Return True
        End If
        If NoSonDelColorBuscadoNingunaDeLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(0), 6, 7, 8) Then
            If SonDelColorBuscadoTodasLasCasillas(ColorAmarillo, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(2), 5) Then Return True
        End If
        Return False
    End Function

    Private Function HayPosibilidadDeIncrustacionDirectaDeBordeLateral() As Boolean
        Return HayPosibilidadDeIncrustacionDirectaDeBordeIzquierdo() Or HayPosibilidadDeIncrustacionDirectaDeBordeDerecho()
    End Function


    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeBordeIzquierdo() As Boolean
        Return HayArribaAlgunaLineaLibre() And HayAbajoAlgunBordeIzquierdo()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeBordeDerecho() As Boolean
        Return HayArribaAlgunaLineaLibre() And HayAbajoAlgunBordeDerecho()
    End Function

    Private Function HayPosibilidadDeIncrustacionINDIRECTAdeBordeLateral() As Boolean
        Return HayArribaAlgunaLineaLibre() And HayAbajoAlgunBordeLateral()
    End Function


    Private Function SonDelColorBuscadoTodasLasCasillas(ColorBuscado As Integer, Configuracion As Integer, ParamArray ListaDeCasillas() As Integer) As Boolean
        Dim NumeroDeCara As Integer = ColorDeLaCasilla(4, Configuracion)
        Dim Contador As Integer
        For Contador = 0 To ListaDeCasillas.GetUpperBound(0)
            If ColorDeLaCasilla(ListaDeCasillas(Contador), Configuracion) <> ColorBuscado Then Return False
        Next
        Return True
    End Function

    Private Function NoSonDelColorBuscadoNingunaDeLasCasillas(ColorBuscado As Integer, Configuracion As Integer, ParamArray ListaDeCasillas() As Integer) As Boolean
        Dim NumeroDeCara As Integer = ColorDeLaCasilla(4, Configuracion)
        Dim Contador As Integer
        For Contador = 0 To ListaDeCasillas.GetUpperBound(0)
            If ColorDeLaCasilla(ListaDeCasillas(Contador), Configuracion) = ColorBuscado Then Return False
        Next
        Return True
    End Function


    Private Function HayArribaAlgunaLineaLibre() As Boolean
        Dim TrioDePosicionesAuxiliar() As Integer
        TrioDePosicionesAuxiliar = {0, 1, 2}
        If EstaLibreLaLinea(TrioDePosicionesAuxiliar) Then Return True
        TrioDePosicionesAuxiliar = {6, 7, 8}
        If EstaLibreLaLinea(TrioDePosicionesAuxiliar) Then Return True
        TrioDePosicionesAuxiliar = {0, 3, 6}
        If EstaLibreLaLinea(TrioDePosicionesAuxiliar) Then Return True
        TrioDePosicionesAuxiliar = {2, 5, 8}
        If EstaLibreLaLinea(TrioDePosicionesAuxiliar) Then Return True
        Return False
    End Function

    Private Function EstaLibreLaLinea(TrioAuxiliar() As Integer) As Boolean
        Dim Contador As Integer
        For Contador = 0 To 2
            If ColorDeLaCasilla(TrioAuxiliar(Contador), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) = ColorAmarillo Then Return False
        Next
        Return True
    End Function


    Private Function HayArribaAlgunParLibreDeCentroIzquierda() As Boolean
        ' Lo de "Centro - Izquierda" se refiere a la línea de abajo
        Dim ParesPosibles() As Integer
        ParesPosibles = {1, 2}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {5, 8}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {6, 7}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {0, 3}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        Return False
    End Function

    Private Function HayArribaAlgunParLibreDeCentroDerecha() As Boolean
        ' Lo de "Centro - Derecha" se refiere a la línea de abajo
        Dim ParesPosibles() As Integer
        ParesPosibles = {7, 8}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {3, 6}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {0, 1}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        ParesPosibles = {2, 5}
        If ColorDeLaCasilla(ParesPosibles(0), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo AndAlso
                ColorDeLaCasilla(ParesPosibles(1), CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(ColorAmarillo)) <> ColorAmarillo Then
            Return True
        End If
        Return False
    End Function

    Private Function HayArribaAlgunParLibre() As Boolean
        Return (HayArribaAlgunParLibreDeCentroIzquierda() Or HayArribaAlgunParLibreDeCentroDerecha())
    End Function


    Private Function HayAbajoAlgunaEsquinaDerechaInferior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunaEsquinaIzquierdaInferior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunaEsquinainferior() As Boolean
        Return (HayAbajoAlgunaEsquinaDerechaInferior() Or HayAbajoAlgunaEsquinaIzquierdaInferior())
    End Function


    Private Function HayAbajoAlgunaEsquinaDerechaSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(2, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunaEsquinaIzquierdaSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(0, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunaEsquinaSuperior() As Boolean
        Return (HayAbajoAlgunaEsquinaDerechaSuperior() Or HayAbajoAlgunaEsquinaIzquierdaSuperior())
    End Function


    Private Function HayAbajoAlgunBordeDerecho() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunBordeIzquierdo() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(3, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunBordeLateral() As Boolean
        Return (HayAbajoAlgunBordeDerecho() Or HayAbajoAlgunBordeIzquierdo())
    End Function


    Private Function HayAbajoAlgunBordeInferior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(7, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunBordeSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(1, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function


    Private Function HayAbajoAlgunParBordeEsquinaIzquierdoInferior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(3, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParBordeEsquinaDerechoInferior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParBordeEsquinaInferior() As Boolean
        Return (HayAbajoAlgunParBordeEsquinaIzquierdoInferior() Or HayAbajoAlgunParBordeEsquinaDerechoInferior())
    End Function


    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoInferior() As Boolean
        Return (HayAbajoAlgunaEsquinaIzquierdaInferior() And HayAbajoAlgunBordeIzquierdo())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoInferior() As Boolean
        Return (HayAbajoAlgunaEsquinaDerechaInferior() And HayAbajoAlgunBordeDerecho())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaInferior() As Boolean
        Return (HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoInferior() Or HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoInferior())
    End Function


    Private Function HayAbajoAlgunParBordeEsquinaIzquierdoSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(0, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And ColorDeLaCasilla(3, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParBordeEsquinaDerechoSuperior() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(2, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And ColorDeLaCasilla(5, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParBordeEsquinaSuperior() As Boolean
        Return (HayAbajoAlgunParBordeEsquinaIzquierdoSuperior() Or HayAbajoAlgunParBordeEsquinaDerechoSuperior())
    End Function


    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoSuperior() As Boolean
        Return (HayAbajoAlgunBordeIzquierdo() And HayAbajoAlgunaEsquinaIzquierdaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoSuperior() As Boolean
        Return (HayAbajoAlgunBordeDerecho() And HayAbajoAlgunaEsquinaDerechaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParBordeEsquinaSuperior() As Boolean
        Return (HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoSuperior() Or HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoSuperior())
    End Function


    Private Function HayAbajoAlgunParCentroIzquierda() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And
                    ColorDeLaCasilla(7, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParCentroDerecha() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(7, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And
                    ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParHorizontal() As Boolean
        Return (HayAbajoAlgunParCentroIzquierda() Or HayAbajoAlgunParCentroDerecha())
    End Function


    Private Function HayAbajoAlgunTrioColumnaIzquierdo() As Boolean
        Dim CuentaCasillas, Contador As Integer
        Dim Resultado As Boolean
        For Contador = 1 To 4
            Resultado = True
            For CuentaCasillas = 0 To 6 Step 3
                If ColorDeLaCasilla(CuentaCasillas, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) <> ColorAmarillo Then
                    Resultado = False
                    Exit For
                End If
            Next
            If Resultado = True Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunTrioColumnaDerecho() As Boolean
        Dim CuentaCasillas, Contador As Integer
        Dim Resultado As Boolean
        For Contador = 1 To 4
            Resultado = True
            For CuentaCasillas = 2 To 8 Step 3
                If ColorDeLaCasilla(CuentaCasillas, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) <> ColorAmarillo Then
                    Resultado = False
                    Exit For
                End If
            Next
            If Resultado = True Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunTrioColumna() As Boolean
        Return (HayAbajoAlgunTrioColumnaDerecho() Or HayAbajoAlgunTrioColumnaIzquierdo())
    End Function


    Private Function HayAbajoPosibilidadDeAlgunTrioColumnaIzquierdo() As Boolean
        Return (HayAbajoPosibilidadDeAlgunParBordeEsquinaIzquierdoInferior() And HayAbajoAlgunaEsquinaIzquierdaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunTrioColumnaDerecho() As Boolean
        Return (HayAbajoPosibilidadDeAlgunParBordeEsquinaDerechoInferior() And HayAbajoAlgunaEsquinaDerechaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunTrioColumna() As Boolean
        Return (HayAbajoPosibilidadDeAlgunTrioColumnaIzquierdo() Or HayAbajoPosibilidadDeAlgunTrioColumnaDerecho())
    End Function


    Private Function HayAbajoAlgunParDeEsquinasIzquierdo() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(0, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And
                    ColorDeLaCasilla(6, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParDeEsquinasDerecho() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 4
            If ColorDeLaCasilla(2, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo And
                    ColorDeLaCasilla(8, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(Contador)) = ColorAmarillo Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function HayAbajoAlgunParDeEsquinas() As Boolean
        Return (HayAbajoAlgunParDeEsquinasIzquierdo() Or HayAbajoAlgunParDeEsquinasDerecho())
    End Function


    Private Function HayAbajoPosibilidadDeAlgunParDeEsquinasIzquierdo() As Boolean
        Return (HayAbajoAlgunaEsquinaIzquierdaInferior() And HayAbajoAlgunaEsquinaIzquierdaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParDeEsquinasDerecho() As Boolean
        Return (HayAbajoAlgunaEsquinaDerechaInferior() And HayAbajoAlgunaEsquinaDerechaSuperior())
    End Function

    Private Function HayAbajoPosibilidadDeAlgunParDeEsquinas() As Boolean
        Return (HayAbajoPosibilidadDeAlgunParDeEsquinasIzquierdo() Or HayAbajoPosibilidadDeAlgunParDeEsquinasDerecho())
    End Function


    Private Function HayAbajoDelTodoAlgunaEsquina() As Boolean
        Dim Contador As Integer
        For Contador = 0 To 8 Step 2
            If Contador = 4 Then Continue For
            If ColorDeLaCasilla(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraOpuesta(ColorAmarillo))) = ColorAmarillo Then Return True
        Next
        Return False
    End Function

    Private Function HayAbajoDelTodoAlgunBorde() As Boolean
        Dim Contador As Integer
        For Contador = 1 To 7 Step 2
            If ColorDeLaCasilla(Contador, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(CaraOpuesta(ColorAmarillo))) = ColorAmarillo Then Return True
        Next
        Return False
    End Function


    Public Function EstaMontadaLaCara(NumeroDeCara As Integer) As Boolean
        Select Case NumeroDeCara
            Case 0 To 5 : Return SonDelColorBuscadoTodasLasCasillas(NumeroDeCara, CuboQueTenemosQueMontar.MatrizDeCuboDeRubik(NumeroDeCara), {0, 1, 2, 3, 4, 5, 6, 7, 8})
            Case Else
                SalimosConError(22) : Stop : End
        End Select
    End Function



End Class
