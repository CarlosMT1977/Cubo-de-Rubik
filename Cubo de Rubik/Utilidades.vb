Option Explicit On
Option Strict On


Public Class Utilidades
    Shared Function ImpresionDeMatriz(Matriz() As Integer, Optional Delimitador As String = " ") As String
        Dim Resultado(Matriz.Length - 1) As String
        Dim Contador As Integer
        For Contador = 0 To Matriz.GetUpperBound(0)
            Resultado(Contador) = CType(Matriz(Contador), String)
        Next
        Return Join(Resultado, Delimitador)
    End Function

    Shared Function SonIgualesLasMatrices(MatrizUno() As Integer, MatrizDos() As Integer) As Boolean
        If MatrizUno.GetLength(0) <> MatrizDos.GetLength(0) Then Return False
        Dim Contador As Integer
        For Contador = 0 To MatrizUno.GetUpperBound(0)
            If MatrizUno(Contador) <> MatrizDos(Contador) Then Return False
        Next
        Return True
    End Function

    Shared Function ColorDeLaCasilla(NumeroDeCasilla%, ConfiguracionDeColorDeLaCara%) As Integer
        If NumeroDeCasilla < 0 Or NumeroDeCasilla > 8 Then SalimosConError(1) : Stop
        If ConfiguracionDeColorDeLaCara < 0 Or ConfiguracionDeColorDeLaCara >= (6 ^ 9) Then SalimosConError(2) : Stop
        Return (ConfiguracionDeColorDeLaCara Mod CType((6 ^ (NumeroDeCasilla + 1)), Integer)) \ CType(6 ^ NumeroDeCasilla, Integer)
    End Function

    Shared Function CaraOpuesta(NumeroDeCara As Integer) As Integer
        Select Case NumeroDeCara
            Case 0 : Return 5
            Case 1 : Return 3
            Case 2 : Return 4
            Case 3 : Return 1
            Case 4 : Return 2
            Case 5 : Return 0
            Case Else : SalimosConError(5) : Stop
        End Select
    End Function

    Shared Function Maximo(Uno As Integer, Dos As Integer) As Integer
        If Uno > Dos Then Return Uno Else Return Dos
    End Function

    Shared Sub ProcesarParesDeColores(CasillaUno%, CaraUno%, CasillaDos%, CaraDos%, MatrizDeCuboDeRubik() As Integer, ByRef MatrizDeColoresDePrueba() As Integer)
        If (CasillaUno < 0 Or CasillaDos < 0 Or CasillaUno > 8 Or CasillaDos > 8) Then SalimosConError(1) : Stop
        If CaraUno < 0 Or CaraDos < 0 Or CaraUno > 5 Or CaraDos > 5 Then SalimosConError(5) : Stop
        If MatrizDeCuboDeRubik.GetLength(0) <> 6 Then SalimosConError(3) : Stop
        MatrizDeColoresDePrueba(0) = ColorDeLaCasilla(CasillaUno, MatrizDeCuboDeRubik(CaraUno))
        MatrizDeColoresDePrueba(1) = ColorDeLaCasilla(CasillaDos, MatrizDeCuboDeRubik(CaraDos))
        If MatrizDeColoresDePrueba(0) < MatrizDeColoresDePrueba(1) Then
            MatrizDeColoresDePrueba(0) += MatrizDeColoresDePrueba(1)
            MatrizDeColoresDePrueba(1) = MatrizDeColoresDePrueba(0) - MatrizDeColoresDePrueba(1)
            MatrizDeColoresDePrueba(0) -= MatrizDeColoresDePrueba(1)
        End If
    End Sub

    Function DeNumeroDeTrioAMatrizDeTresColores(NumeroDeTrio As Integer) As Integer()
        Select Case NumeroDeTrio
            Case 0 : Return {0, 1, 2}
            Case 1 : Return {0, 2, 3}
            Case 2 : Return {0, 3, 4}
            Case 3 : Return {0, 4, 1}
            Case 4 : Return {1, 2, 6}
            Case 5 : Return {2, 3, 6}
            Case 6 : Return {3, 4, 6}
            Case 7 : Return {4, 1, 6}
            Case Else : SalimosConError(8) : Stop
        End Select
    End Function

    Shared Function DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores() As Integer) As Integer
        If MatrizDeTresColores.GetLength(0) <> 3 Then SalimosConError(9) : Stop
        If MatrizDeTresColores(0) > MatrizDeTresColores(1) Then Return DeMatrizDeTresColoresANumeroDeTrio({MatrizDeTresColores(1), MatrizDeTresColores(0), MatrizDeTresColores(2)})
        If MatrizDeTresColores(1) > MatrizDeTresColores(2) Then Return DeMatrizDeTresColoresANumeroDeTrio({MatrizDeTresColores(0), MatrizDeTresColores(2), MatrizDeTresColores(1)})
        Dim Contador As Integer
        For Contador = 0 To 2
            If MatrizDeTresColores(Contador) < 0 Or MatrizDeTresColores(Contador) > 5 Then SalimosConError(10) : Stop
        Next
        If MatrizDeTresColores(0) = MatrizDeTresColores(1) Or MatrizDeTresColores(1) = MatrizDeTresColores(2) Then SalimosConError(11) : Stop
        If MatrizDeTresColores(0) = 0 And MatrizDeTresColores(1) = 1 And MatrizDeTresColores(2) = 2 Then Return 0
        If MatrizDeTresColores(0) = 0 And MatrizDeTresColores(1) = 2 And MatrizDeTresColores(2) = 3 Then Return 1
        If MatrizDeTresColores(0) = 0 And MatrizDeTresColores(1) = 3 And MatrizDeTresColores(2) = 4 Then Return 2
        If MatrizDeTresColores(0) = 0 And MatrizDeTresColores(1) = 1 And MatrizDeTresColores(2) = 4 Then Return 3
        If MatrizDeTresColores(0) = 1 And MatrizDeTresColores(1) = 2 And MatrizDeTresColores(2) = 5 Then Return 4
        If MatrizDeTresColores(0) = 2 And MatrizDeTresColores(1) = 3 And MatrizDeTresColores(2) = 5 Then Return 5
        If MatrizDeTresColores(0) = 3 And MatrizDeTresColores(1) = 4 And MatrizDeTresColores(2) = 5 Then Return 6
        If MatrizDeTresColores(0) = 1 And MatrizDeTresColores(1) = 4 And MatrizDeTresColores(2) = 5 Then Return 7
        SalimosConError(12) : Stop
    End Function

    Shared Function ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(ConfiguracionDeColoresDeLaCara As Integer) As Integer
        Dim Resultado As Integer = 0
        Resultado += CType(6 ^ 0, Integer) * ColorDeLaCasilla(6, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 1, Integer) * ColorDeLaCasilla(3, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 2, Integer) * ColorDeLaCasilla(0, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 5, Integer) * ColorDeLaCasilla(1, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 8, Integer) * ColorDeLaCasilla(2, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 7, Integer) * ColorDeLaCasilla(5, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 6, Integer) * ColorDeLaCasilla(8, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 3, Integer) * ColorDeLaCasilla(7, ConfiguracionDeColoresDeLaCara)
        Resultado += CType(6 ^ 4, Integer) * ColorDeLaCasilla(4, ConfiguracionDeColoresDeLaCara)
        Return Resultado
    End Function

    Shared Function ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaIzquierda(ConfiguracionDeColoresDeLaCara As Integer) As Integer
        Return ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(ConfiguracionDeColoresDeLaCaraDespuesDeRotadaALaDerecha(ConfiguracionDeColoresDeLaCara)))
    End Function

    Shared Sub ComprobarSiElCuboEsCorrecto(CubitoDeRubikAuxiliar As ClaseCuboDeRubik)
        Dim MatrizDeCuboDeRubik(5) As Integer
        MatrizDeCuboDeRubik = ClonacionDeMatriz(CubitoDeRubikAuxiliar.MatrizDeCuboDeRubik)
        If MatrizDeCuboDeRubik.GetLength(0) <> 6 Then SalimosConError(3) : Stop : End
        Dim Contador, CuentaUno, CuentaDos As Integer

        Dim NumeroDeAparicionesDelColor(5) As Integer
        Dim CuentaCaras, CuentaCasillas As Integer
        For CuentaCaras = 0 To 5
            For CuentaCasillas = 0 To 8
                NumeroDeAparicionesDelColor(ColorDeLaCasilla(CuentaCasillas, MatrizDeCuboDeRubik(CuentaCaras))) += 1
            Next
        Next
        For Contador = 0 To 5
            If NumeroDeAparicionesDelColor(Contador) <> 9 Then SalimosConError(4) : Stop : End
        Next

        Dim NumeroDeAparicionesDelParDeColores(5, 5) As Integer
        Dim MatrizDeDosColoresDePrueba(1) As Integer
        ProcesarParesDeColores(7, 0, 1, 1, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 0, 1, 2, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(1, 0, 1, 3, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(5, 0, 1, 4, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 1, 5, 2, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 2, 5, 3, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 3, 5, 4, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 4, 5, 1, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(1, 5, 7, 1, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(3, 5, 7, 2, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(7, 5, 7, 3, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        ProcesarParesDeColores(5, 5, 7, 4, MatrizDeCuboDeRubik, MatrizDeDosColoresDePrueba)
        NumeroDeAparicionesDelParDeColores(MatrizDeDosColoresDePrueba(0), MatrizDeDosColoresDePrueba(1)) += 1
        For CuentaUno = 1 To 5
            For CuentaDos = 0 To CuentaUno - 1
                If (CuentaUno = 5 And CuentaDos = 0) Or (CuentaUno = 3 And CuentaDos = 1) Or (CuentaUno = 4 And CuentaDos = 2) Then Continue For
                If NumeroDeAparicionesDelParDeColores(CuentaUno, CuentaDos) <> 1 Then SalimosConError(6) : Stop
            Next
        Next
        For Contador = 0 To 5
            If NumeroDeAparicionesDelParDeColores(Contador, Contador) <> 0 Then SalimosConError(7) : Stop
        Next

        Dim NumeroDeAparicionesDelTrioDeColores(7) As Integer
        Dim MatrizDeTresColores(2) As Integer
        MatrizDeTresColores = {ColorDeLaCasilla(6, MatrizDeCuboDeRubik(0)), ColorDeLaCasilla(0, MatrizDeCuboDeRubik(1)), ColorDeLaCasilla(2, MatrizDeCuboDeRubik(2))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(0, MatrizDeCuboDeRubik(0)), ColorDeLaCasilla(0, MatrizDeCuboDeRubik(2)), ColorDeLaCasilla(2, MatrizDeCuboDeRubik(3))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(2, MatrizDeCuboDeRubik(0)), ColorDeLaCasilla(0, MatrizDeCuboDeRubik(3)), ColorDeLaCasilla(2, MatrizDeCuboDeRubik(4))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(8, MatrizDeCuboDeRubik(0)), ColorDeLaCasilla(0, MatrizDeCuboDeRubik(4)), ColorDeLaCasilla(2, MatrizDeCuboDeRubik(1))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(0, MatrizDeCuboDeRubik(5)), ColorDeLaCasilla(6, MatrizDeCuboDeRubik(1)), ColorDeLaCasilla(8, MatrizDeCuboDeRubik(2))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(6, MatrizDeCuboDeRubik(5)), ColorDeLaCasilla(6, MatrizDeCuboDeRubik(2)), ColorDeLaCasilla(8, MatrizDeCuboDeRubik(3))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(8, MatrizDeCuboDeRubik(5)), ColorDeLaCasilla(6, MatrizDeCuboDeRubik(3)), ColorDeLaCasilla(8, MatrizDeCuboDeRubik(4))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        MatrizDeTresColores = {ColorDeLaCasilla(2, MatrizDeCuboDeRubik(5)), ColorDeLaCasilla(6, MatrizDeCuboDeRubik(4)), ColorDeLaCasilla(8, MatrizDeCuboDeRubik(1))}
        NumeroDeAparicionesDelTrioDeColores(DeMatrizDeTresColoresANumeroDeTrio(MatrizDeTresColores)) += 1
        For Contador = 0 To 7
            If NumeroDeAparicionesDelTrioDeColores(Contador) <> 1 Then SalimosConError(13) : Stop
        Next

        MessageBox.Show("Hemos terminado la comprobación", "Comprobación terminada")
    End Sub

    Shared Function MovimientoInverso(NumeroDeMovimiento As Integer) As Integer
        If NumeroDeMovimiento Mod 2 = 1 Then Return NumeroDeMovimiento - 1 Else Return NumeroDeMovimiento + 1
    End Function

    Shared Function UltimoMovimientoSegunCadena(CadenaDeMovimientos As String) As Integer
        If InStr(CadenaDeMovimientos, ",") = 0 Then Return CType(CadenaDeMovimientos, Integer)
        Return CType(CadenaDeMovimientos.Substring(CadenaDeMovimientos.IndexOf(",") + 1), Integer)
        SalimosConError(20) : Stop
    End Function

    Shared Function DeNumeroDeMovimientoACadenaDeTexto(NumeroDeMovimiento As Integer) As String
        Select Case NumeroDeMovimiento
            Case 0 : Return "Arriba - Girar a la izquierda"
            Case 1 : Return "Arriba - Girar a la derecha"
            Case 2 : Return "Abajo - Girar a la izquierda"
            Case 3 : Return "Abajo - Girar a la derecha"
            Case 4 : Return "Izquierda - Girar hacia arriba"
            Case 5 : Return "Izquierda - Girar hacia abajo"
            Case 6 : Return "Derecha - Girar hacia arriba"
            Case 7 : Return "Derecha - Girar hacia abajo"
            Case 8 : Return "Alante - Rotar hacia la izquierda"
            Case 9 : Return "Alante - Rotar hacia la derecha"
            Case 10 : Return "Atrás - Rotar hacia la izquierda"
            Case 11 : Return "Atrás - Rotar hacia la derecha"
            Case Else : SalimosConError(19) : Stop
        End Select
    End Function

    Shared Function DeNumeroDeMovimientoACadenaDeTexto(Matriz() As Integer) As String
        Dim Resultado As String = vbNullString
        Dim Contador As Integer
        For Contador = 0 To Matriz.GetUpperBound(0)
            Resultado &= (Contador + 1) & ") " & DeNumeroDeMovimientoACadenaDeTexto(Matriz(Contador)) & vbCrLf
            If (Contador + 1) Mod 5 = 0 Then Resultado &= vbCrLf
        Next
        Return Resultado
    End Function

    Shared Function DeNumeroDeMovimientoACadenaDeTexto(CuboDeRubikArgumento As ClaseCuboDeRubik, Optional ConLineasDeSeparacion As Boolean = False) As String
        Dim CuboAuxiliar As ClaseCuboDeRubik
        If ConLineasDeSeparacion Then
            CuboAuxiliar = New ClaseCuboDeRubik(ClonacionDeMatriz(CuboDeRubikArgumento.MatrizInicial))
        End If
        Dim Resultado As String = vbNullString
        Dim Contador As Integer
        For Contador = 0 To CuboDeRubikArgumento.ListaDeMovimientos.GetUpperBound(0)
            Resultado &= (Contador + 1) & ") " & DeNumeroDeMovimientoACadenaDeTexto(CuboDeRubikArgumento.ListaDeMovimientos(Contador)) & vbCrLf
            If (Contador + 1) Mod 5 = 0 Then Resultado &= vbCrLf
            If ConLineasDeSeparacion Then
                CuboAuxiliar.EjecutarMovimiento(CuboDeRubikArgumento.ListaDeMovimientos(Contador))
                If CuboAuxiliar.EstaMontadaLaCara(0) Then
                    Resultado &= "----------" & vbCrLf
                    If (Contador + 1) Mod 5 = 0 Then Resultado &= vbCrLf
                End If
            End If
        Next
        Return Resultado
    End Function

    Shared Sub SalimosConError(NumeroDeError As Integer)
        Dim TituloDeMensaje, CadenaDeMensaje As String
        Select Case NumeroDeError
            Case 1
                CadenaDeMensaje = "El número de casilla tiene que ser un entero entre 0 y 9, ambos inclusive"
                TituloDeMensaje = "Número de casilla inválido"
            Case 2
                CadenaDeMensaje = "El número de configuración de color de la cara tiene que ser un entero entre 0 y 6^9-1, ambos inclusive"
                TituloDeMensaje = "Número de configuración de color inválido"
            Case 3
                CadenaDeMensaje = "La matriz de cubo de Rubik tiene que tener 6 y sólo 6 elementos"
                TituloDeMensaje = "Número de elementos de matriz inválido"
            Case 4
                CadenaDeMensaje = "Cada color tiene que aparecer en 9 y sólo 9 casillas en todo el cubo de Rubik"
                TituloDeMensaje = "Número inválido de apariciones de algún color"
            Case 5
                CadenaDeMensaje = "El número de cara tiene que ser un número entre 0 y 5, cambos inclusive"
                TituloDeMensaje = "Número inválido de cara"
            Case 6
                CadenaDeMensaje = "Cada par de colores de la lista tiene que aparecer una y sólo una vez"
                TituloDeMensaje = "Número inválido de apariciones de un par de colores"
            Case 7
                CadenaDeMensaje = "No puede coincidir en un cubito de dos caras visibles que las dos caras tengan el mismo color"
                TituloDeMensaje = "Coincidencia inválida del mismo color en las dos caras visibles de un cubito"
            Case 8
                CadenaDeMensaje = "El número de trío tiene que ser un número entre 0 y 7, ambos inclusive"
                TituloDeMensaje = "Número inválido de trío"
            Case 9
                CadenaDeMensaje = "Tienes que meter como parámetro una matriz de tres y sólo tres colores"
                TituloDeMensaje = "Número inválido de colores"
            Case 10
                CadenaDeMensaje = "El color tiene que ser un número entre 0 y 5, ambos inclusive"
                TituloDeMensaje = "Número de color inválido"
            Case 11
                CadenaDeMensaje = "En un trío cubito de los que hacen esquina y tienen tres caras visibles, no puede aparecer más de una vez el mismo color"
                TituloDeMensaje = "Repetición no autorizada de algún color en un cubito esquinero"
            Case 12
                CadenaDeMensaje = "El trío de colores introducido como argumento no corresponde a ninguna de las 8 esquinas del Cubo de Rubik"
                TituloDeMensaje = "Trío de colores inexistente"
            Case 13
                CadenaDeMensaje = "Cada uno de los 7 tríos de colores tiene que aparecer una y sólo una vez"
                TituloDeMensaje = "Trío que no aparece o que aparece más de una vez"
            Case 14
                CadenaDeMensaje = "El número de esquina tiene que estar comprendido entre 0 y 7, ambos inclusive"
                TituloDeMensaje = "Número de esquina inválido"
            Case 15
                CadenaDeMensaje = "El número de par tiene que estar comprendido entre 0 y 11, ambos inclusive"
                TituloDeMensaje = "Número de par inválido"
            Case 16
                CadenaDeMensaje = "El número de cara tiene que ser un número entre 0 y 5, cambos inclusive"
                TituloDeMensaje = "Número inválido de cara"
            Case 17
                CadenaDeMensaje = "Cuando la cola está vacía, el principio y el final apuntan a Nothing; y cuando la cola no está vacía, ninguno de los dos elementos apunta _
                    a Nothing. Pero no puede ser que uno apunte a Nothing y otro no."
                TituloDeMensaje = "No puede apuntar a Nothing uno sí y otro no"
            Case 18
                CadenaDeMensaje = "No se puede desencolar nada, porque la cola está vacía"
                TituloDeMensaje = "No es posible desencolar en colas vacías"
            Case 19
                CadenaDeMensaje = "Tienes que meter un número de movimiento del 0 al 11, ambos inclusive"
                TituloDeMensaje = "Número de movimiento inválido"
            Case 20
                CadenaDeMensaje = "Revisa esa cadena, que de ahí no se puede extraer cuál fue el último movimiento"
                TituloDeMensaje = "Cadena de movimientos inválida"
            Case 21
                CadenaDeMensaje = "No se puede poner una cara sobre sí misma o sobre su opuesta"
                TituloDeMensaje = "Par de caras inválido"
            Case 22
                CadenaDeMensaje = "No deberíamos estar aquí, porque la situación en que estamos debería haber sido cortada en alguno de los condicionales anteriores"
                TituloDeMensaje = "No deberíamos estar aquí"
            Case 23
                CadenaDeMensaje = "No es posible hacer una incrustación directa de trío-columna"
                TituloDeMensaje = "Movimiento imposible"
            Case 24
                CadenaDeMensaje = "No es posible hacer una incrustación directa de par vertical"
                TituloDeMensaje = "Movimiento imposible"
            Case 25
                CadenaDeMensaje = "No es posible hacer una incrustación directa de esquina superior"
                TituloDeMensaje = "Movimiento imposible"
            Case 26
                CadenaDeMensaje = "No es posible hacer una incrustación directa de borde lateral"
                TituloDeMensaje = "Movimiento imposible"
            Case 27
                CadenaDeMensaje = "No es posible hacer una incrustación directa de esquina inferior"
                TituloDeMensaje = "Movimiento imposible"
            Case 28
                CadenaDeMensaje = "No es posible formar el par que se busca"
                TituloDeMensaje = "Movimiento imposible"
            Case 29
                CadenaDeMensaje = "El número de cara tiene que ser un número entre 1 y 4, ambos inclusive"
                TituloDeMensaje = "Movimiento imposible"
            Case 30
                CadenaDeMensaje = "No es posible formar el trío que se busca"
                TituloDeMensaje = "Movimiento imposible"
            Case 31
                CadenaDeMensaje = "No es posible hacer una incrustación INDIRECTA de trío-columna"
                TituloDeMensaje = "Movimiento imposible"
            Case 32
                CadenaDeMensaje = "No es posible hacer una incrustación INDIRECTA de par vertical"
                TituloDeMensaje = "Movimiento imposible"
            Case 33
                CadenaDeMensaje = "No es posible hacer una incrustación INDIRECTA de borde lateral"
                TituloDeMensaje = "Movimiento imposible"
            Case 34
                CadenaDeMensaje = "No es posible hacer una incrustación INDIRECTA de esquina inferior"
                TituloDeMensaje = "Movimiento imposible"
            Case 35
                CadenaDeMensaje = "No es posible hacer la colocación directa de esquina inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 36
                CadenaDeMensaje = "No es posible hacer la colocación directa de borde inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 36
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de borde inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 37
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de esquina inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 38
                CadenaDeMensaje = "No es posible hacer la colocación directa de borde lateral que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 39
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de borde lateral que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 40
                CadenaDeMensaje = "No es posible hacer la colocación directa de par horizontal que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 41
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de par horizontal que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 42
                CadenaDeMensaje = "No es posible hacer la colocación directa de borde subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 43
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de borde subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 44
                CadenaDeMensaje = "No es posible hacer la colocación directa de esquina subterránea que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 45
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de esquina subterránea que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 46
                CadenaDeMensaje = "No es posible hacer la incrustación INDIRECTA de par vertical que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 47
                CadenaDeMensaje = "No es posible hacer la colocación directa de par borde-esquina inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 48
                CadenaDeMensaje = "No es posible hacer la colocación INDIRECTA de par borde-esquina inferior que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 49
                CadenaDeMensaje = "Esto es raro, porque en teoría todavía no está la cara montada, pero tampoco hay ningún movimiento que se pueda hacer"
                TituloDeMensaje = "Estamos en una paradoja"
            Case 50
                CadenaDeMensaje = "No es posible meter de cualquier manera la esquina superior que quieres meter"
                TituloDeMensaje = "Movimiento imposible"
            Case 51
                CadenaDeMensaje = "No es posible hacer la incrustación directa de trío-columna subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 52
                CadenaDeMensaje = "No es posible hacer la incrustación INDIRECTA de trío-columna subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 53
                CadenaDeMensaje = "No es posible hacer la incrustación directa de par subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 54
                CadenaDeMensaje = "No es posible hacer la incrustación INDIRECTA de par subterráneo que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 55
                CadenaDeMensaje = "No es posible hacer el intercambio de bordes que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 56
                CadenaDeMensaje = "Aquí algo falla, porque si se ha cumplido la condición en los casos anteriores, debería cumplirse también aquí"
                TituloDeMensaje = "No deberíamos estar aquí"
            Case 57
                CadenaDeMensaje = "No es posible hacer el intercambio de esquinas que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 58
                CadenaDeMensaje = "No es posible meter de cualquier manera ningún borde superior"
                TituloDeMensaje = "Movimiento imposible"
            Case 59
                CadenaDeMensaje = "Antes de hacer nada de esto, debería estar montada la cara amarilla, y no lo está"
                TituloDeMensaje = "No deberíamos estar aquí"
            Case 60
                CadenaDeMensaje = "No es posible hacer la colocación directa de borde de segunda línea que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 61
                CadenaDeMensaje = "No es posible hacer la colocación indirecta de borde de segunda línea que quieres hacer"
                TituloDeMensaje = "Movimiento imposible"
            Case 62
                CadenaDeMensaje = "No es posible bajar un borde invertido de segunda línea"
                TituloDeMensaje = "Movimiento imposible"
            Case 63
                CadenaDeMensaje = "¿Cómo quieres bajar un borde de segunda línea, si ya está montada la segunda línea?"
                TituloDeMensaje = "Movimiento imposible"
            Case 64
                CadenaDeMensaje = "Para poder montar la cara blanca de abajo, hace falta que primero esté montada la amarilla y las dos líneas superiores de cada una de las cuatro caras adyacentes"
                TituloDeMensaje = "Movimiento imposible"
            Case 65
                CadenaDeMensaje = "Esas dos posibilidades son mutuamente excluyentes"
                TituloDeMensaje = "Situación imposible"
            Case 66
                CadenaDeMensaje = "La cara inferior tiene que ser en este caso un número comprendido entre 1 y 4, ambos inclusive"
                TituloDeMensaje = "Valor inválido"
            Case 67
                CadenaDeMensaje = "Los datos que has introducido son incorrectos, por eso no podemos resolver el cubo"
                TituloDeMensaje = "Cubo irresoluble"
        End Select



        MessageBox.Show(CadenaDeMensaje, TituloDeMensaje, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Shared Sub AdvertimosAlUsuario(CadenaDeMensaje As String, Optional CadenaDeTitulo As String = "¡CUIDADO!")
        MessageBox.Show(CadenaDeMensaje, CadenaDeTitulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Shared Function CualEsElCodigoDeColor(Colorete As Color) As Integer
        Select Case Colorete
            Case Color.Yellow : Return 0
            Case Color.Red : Return 1
            Case Color.Blue : Return 2
            Case Color.Orange : Return 3
            Case Color.Green : Return 4
            Case Color.White : Return 5
            Case Else : SalimosConError(22) : Stop : End
        End Select
    End Function

    Shared Function Potencia(Base%, Exponente%) As Integer
        Dim Resultado As Integer = 1
        Dim Contador As Integer
        For Contador = 1 To Exponente
            Resultado *= Base
        Next
        Return Resultado
    End Function

    Shared Function ClonacionDeMatriz(Matriz() As Integer) As Integer()
        Dim Resultado(Matriz.GetUpperBound(0)) As Integer
        Dim Contador As Integer
        For Contador = 0 To Matriz.GetUpperBound(0)
            Resultado(Contador) = Matriz(Contador)
        Next
        Return Resultado
    End Function


    Shared Sub SimplificarMatrizDeMovimientos(ByRef MatrizAuxiliar() As Integer)
        Dim Contador As Integer
        Do
            For Contador = 1 To MatrizAuxiliar.GetUpperBound(0)
                If MatrizAuxiliar(Contador) = MovimientoInverso(MatrizAuxiliar(Contador - 1)) Then
                    EliminarElementoDeMatriz(Contador - 1, MatrizAuxiliar)
                    EliminarElementoDeMatriz(Contador - 1, MatrizAuxiliar)
                    Continue Do
                End If
            Next
            For Contador = 2 To MatrizAuxiliar.GetUpperBound(0)
                If MatrizAuxiliar(Contador) = MatrizAuxiliar(Contador - 1) AndAlso MatrizAuxiliar(Contador) = MatrizAuxiliar(Contador - 2) Then
                    MatrizAuxiliar(Contador - 2) = MovimientoInverso(MatrizAuxiliar(Contador - 2))
                    EliminarElementoDeMatriz(Contador - 1, MatrizAuxiliar)
                    EliminarElementoDeMatriz(Contador - 1, MatrizAuxiliar)
                    Continue Do
                End If
            Next
            For Contador = 0 To MatrizAuxiliar.GetUpperBound(0) - 2
                Dim IndiceAuxiliar, SegundoContador As Integer
                IndiceAuxiliar = 0
                For SegundoContador = Contador + 1 To MatrizAuxiliar.GetUpperBound(0)
                    If MatrizAuxiliar(SegundoContador) \ 4 <> MatrizAuxiliar(Contador) \ 4 Then Exit For
                    If MatrizAuxiliar(SegundoContador) = MovimientoInverso(MatrizAuxiliar(Contador)) Then
                        EliminarElementoDeMatriz(SegundoContador, MatrizAuxiliar)
                        EliminarElementoDeMatriz(Contador, MatrizAuxiliar)
                        Continue Do
                    End If
                    If MatrizAuxiliar(SegundoContador) = MatrizAuxiliar(Contador) Then
                        If IndiceAuxiliar = 0 Then
                            IndiceAuxiliar = SegundoContador
                        Else
                            EliminarElementoDeMatriz(SegundoContador, MatrizAuxiliar)
                            EliminarElementoDeMatriz(IndiceAuxiliar, MatrizAuxiliar)
                            EliminarElementoDeMatriz(Contador, MatrizAuxiliar)
                            Continue Do
                        End If
                    End If
                Next
            Next
            Exit Do
        Loop
    End Sub

    Shared Function ObtenerMatrizDeRepeticionesDeMovimientos(MatrizDeMovimientosAuxiliar() As Integer) As Integer()
        Dim Resultado(11) As Integer
        Dim Contador As Integer
        For Contador = 0 To MatrizDeMovimientosAuxiliar.GetUpperBound(0)
            Resultado(MatrizDeMovimientosAuxiliar(Contador)) += 1
        Next
        Return Resultado
    End Function

    Shared Sub AnnadirElementoAMatriz(ByVal Elemento As Integer, ByRef Matriz() As Integer)
        If Matriz Is Nothing Then
            ReDim Matriz(0)
        Else
            ReDim Preserve Matriz(Matriz.GetLength(0))
        End If
        Matriz(Matriz.GetUpperBound(0)) = Elemento
    End Sub

    Shared Sub EliminarElementoDeMatriz(ByVal Indice As Integer, ByRef Matriz() As Integer)
        Dim Contador As Integer
        For Contador = Indice To Matriz.GetUpperBound(0) - 1
            Matriz(Contador) = Matriz(Contador + 1)
        Next
        ReDim Preserve Matriz(Matriz.GetUpperBound(0) - 1)
    End Sub

    Shared Sub GenerarMatrizDeMovimientosAleatorios(ByRef Matriz() As Integer, NumeroDeMovimientos As Integer, Semilla As Long)
        Matriz = Nothing
        Randomize(Semilla)
        Dim EnteroAuxiliar, Contador As Integer
        Dim RealAuxiliar As Single
        For Contador = 1 To NumeroDeMovimientos
            RealAuxiliar = 12 * Rnd()
            RealAuxiliar = Int(RealAuxiliar)
            EnteroAuxiliar = CType(RealAuxiliar, Integer)
            AnnadirElementoAMatriz(EnteroAuxiliar, Matriz)
        Next
        SimplificarMatrizDeMovimientos(Matriz)
        Do While Matriz.GetLength(0) < NumeroDeMovimientos
            For Contador = Matriz.GetLength(0) + 1 To NumeroDeMovimientos
                RealAuxiliar = 12 * Rnd()
                RealAuxiliar = Int(RealAuxiliar)
                EnteroAuxiliar = CType(RealAuxiliar, Integer)
                AnnadirElementoAMatriz(EnteroAuxiliar, Matriz)
            Next
            SimplificarMatrizDeMovimientos(Matriz)
        Loop
    End Sub

    Shared Sub InicializarCuboDeRubik(CuboAuxiliar As ClaseCuboDeRubik)
        CuboAuxiliar.MatrizDeCuboDeRubik = {0, 2015539, 4031078, 6046617, 8062156, 10077695}
    End Sub

    Shared Sub InicializarCuboDeRubik(ByRef CuboApuntado As ClaseCuboDeRubik, NumeroDeMovimientos As Integer, Semilla As Long)
        Dim CuboAuxiliar As ClaseCuboDeRubik = New ClaseCuboDeRubik
        InicializarCuboDeRubik(CuboAuxiliar)
        Dim MatrizDeMovimientos() As Integer
        GenerarMatrizDeMovimientosAleatorios(MatrizDeMovimientos, NumeroDeMovimientos, Semilla)
        MessageBox.Show(DeNumeroDeMovimientoACadenaDeTexto(MatrizDeMovimientos))
        Dim Contador As Integer
        For Contador = 0 To MatrizDeMovimientos.GetUpperBound(0)
            CuboAuxiliar.EjecutarMovimiento(MatrizDeMovimientos(Contador))
        Next
        CuboApuntado.MatrizDeCuboDeRubik = ClonacionDeMatriz(CuboAuxiliar.MatrizDeCuboDeRubik)
    End Sub

    Shared Sub InicializarMatriz(Matriz() As Integer)
        Dim Contador As Integer
        For Contador = 0 To Matriz.GetUpperBound(0)
            Matriz(Contador) = 0
        Next
    End Sub

    Shared Sub InicializarMatriz(Matriz(,) As Integer)
        Dim Uno, Cero As Integer
        For Cero = 0 To Matriz.GetUpperBound(0)
            For Uno = 0 To Matriz.GetUpperBound(1)
                Matriz(Cero, Uno) = 0
            Next
        Next
    End Sub

    Shared Sub InicializarMatriz(Matriz(,,) As Integer)
        Dim Cero, Uno, Dos As Integer
        For Cero = 0 To Matriz.GetUpperBound(0)
            For Uno = 0 To Matriz.GetUpperBound(1)
                For Dos = 0 To Matriz.GetUpperBound(2)
                    Matriz(Cero, Uno, Dos) = 0
                Next
            Next
        Next
    End Sub



End Class


