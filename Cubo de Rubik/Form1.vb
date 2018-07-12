' Aparte de que hay que comprobar a ver dónde da fallos de momento tal y como está, hay que comprobar que se hacen bien las asignaciones de casillas en el 
'   procedimiento AsignamosMatrizDeControlesACuboDeRubik

Option Explicit On
Option Strict On

Imports Cubo_de_Rubik.Utilidades
Imports Cubo_de_Rubik.MontarElCubo

Public Class Form1
    '0: Amarillo
    '1: Rojo
    '2: Azul
    '3: Naranja
    '4: Verde
    '5: Blanco
    Private CuboDeRubikInicial, CuboDeRubikFinal As ClaseCuboDeRubik
    Private Const DistanciaIzquierda As Integer = 50
    Private Const DistanciaArriba As Integer = 50
    Private Const DistanciaMinima As Integer = 10
    Private Const DistanciaMaxima As Integer = 20
    Private MatrizDeBotones(53) As Button
    Dim MatrizDeMuestrarios(5) As Button
    Private Const AnchuraDelBoton As Integer = 30
    Private Const AlturaDelBoton As Integer = 30
    Private Const AnchuraDelMuestrario As Integer = 55
    Private Const AlturaDelMuestrario As Integer = 55
    Private DistanciaEntreMuestrarios As Integer
    Private Colores() As Color = {Color.Yellow, Color.Red, Color.Blue, Color.Orange, Color.Green, Color.White}
    Private Const BordeMinimo As Integer = 1
    Private Const BordeMaximo As Integer = 10

    Dim ColorSeleccionado As Integer = -1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CrearMatrizDeControles()

    End Sub

    Private Sub btnSolucionarSudoku_Click(sender As Object, e As EventArgs) Handles btnSolucionarSudoku.Click

        If Not EstaCorrectamenteRellenadaLaMatrizDeControles() Then
            AdvertimosAlUsuario("Te falta por asignar algún color por ahí", "Falta asignar color")
            Exit Sub
        End If

        MessageBox.Show("Veo que has rellenado todos los botones del formulario")
        CuboDeRubikInicial = New ClaseCuboDeRubik
        AsignamosMatrizDeControlesACuboDeRubik(CuboDeRubikInicial)




        Dim MontajeDelCuboCompleto As MontarElCuboDeLuxe = New MontarElCuboDeLuxe(CuboDeRubikInicial)
        MontajeDelCuboCompleto.CompletarElCubo()
        MontajeDelCuboCompleto.MostrarSolucionDelCubo()


        MessageBox.Show("Mira a ver si ya se ha montado el cubo completo")


    End Sub

    Private Sub CrearMatrizDeControles()
        Dim Contador, XVariable, YVariable, XFija, YFija, XAux, YAux As Integer
        Dim NumeroDeCarasALaIzquierda, NumeroDeCarasPorEncima As Integer
        For Contador = 0 To 53
            MatrizDeBotones(Contador) = New Button
            MatrizDeBotones(Contador).Size = New Size(AnchuraDelBoton, AlturaDelBoton)
            MatrizDeBotones(Contador).Text = vbNullString
            Select Case Contador \ 9
                Case 2 : NumeroDeCarasALaIzquierda = 0
                Case 0, 1, 3 : NumeroDeCarasALaIzquierda = 1
                Case 4 : NumeroDeCarasALaIzquierda = 2
                Case 5 : NumeroDeCarasALaIzquierda = 3
            End Select
            Select Case Contador \ 9
                Case 3 : NumeroDeCarasPorEncima = 0
                Case 0, 2, 4, 5 : NumeroDeCarasPorEncima = 1
                Case 1 : NumeroDeCarasPorEncima = 2
            End Select
            XFija = DistanciaIzquierda + 3 * NumeroDeCarasALaIzquierda * (AnchuraDelBoton + DistanciaMinima) + NumeroDeCarasALaIzquierda * (DistanciaMaxima - DistanciaMinima)
            XVariable = (AnchuraDelBoton + DistanciaMinima) * (Contador Mod 3)
            YFija = DistanciaArriba + 3 * NumeroDeCarasPorEncima * (AlturaDelBoton + DistanciaMinima) + NumeroDeCarasPorEncima * (DistanciaMaxima - DistanciaMinima)
            YVariable = (AlturaDelBoton + DistanciaMinima) * ((Contador Mod 9) \ 3)
            MatrizDeBotones(Contador).Location = New Point(XFija + XVariable, YFija + YVariable)
            MatrizDeBotones(Contador).Name = CType(Contador, String)
            AddHandler MatrizDeBotones(Contador).Click, AddressOf CuandoPicamosUnBotonDelCubo
            Me.Controls.Add(MatrizDeBotones(Contador))
            If Contador Mod 9 = 4 Then MatrizDeBotones(Contador).BackColor = Colores(Contador \ 9)
        Next

        Panel1.Size = New Size(6 * AnchuraDelBoton + 4 * DistanciaMinima + DistanciaMaxima, 3 * AlturaDelBoton + 2 * DistanciaMinima)
        XFija = MatrizDeBotones(17).Location.X + AnchuraDelBoton + DistanciaMaxima
        YFija = MatrizDeBotones(8).Location.Y + AlturaDelBoton + DistanciaMaxima
        Panel1.Location = New Point(XFija, YFija)

        DistanciaEntreMuestrarios = CType((6 * AnchuraDelBoton + 4 * DistanciaMinima + DistanciaMaxima - 3 * AnchuraDelMuestrario) / 4, Integer)
        For Contador = 0 To 5
            MatrizDeMuestrarios(Contador) = New Button
            MatrizDeMuestrarios(Contador).Size = New Size(AnchuraDelMuestrario, AlturaDelMuestrario)
            XFija = DistanciaEntreMuestrarios
            XVariable = (AnchuraDelMuestrario + DistanciaEntreMuestrarios) * (Contador Mod 3)
            YFija = DistanciaEntreMuestrarios
            YVariable = (AlturaDelMuestrario + DistanciaEntreMuestrarios) * (Contador \ 3)
            MatrizDeMuestrarios(Contador).Location = New Point(XFija + XVariable, YFija + YVariable)
            Panel1.Controls.Add(MatrizDeMuestrarios(Contador))
            MatrizDeMuestrarios(Contador).BackColor = Colores(Contador)
            MatrizDeMuestrarios(Contador).FlatStyle = FlatStyle.Flat
            MatrizDeMuestrarios(Contador).FlatAppearance.BorderColor = Color.Black
            MatrizDeMuestrarios(Contador).FlatAppearance.BorderSize = BordeMinimo
            MatrizDeMuestrarios(Contador).Name = CType(Contador, String)
            AddHandler MatrizDeMuestrarios(Contador).Click, AddressOf CuandoPicamosUnMuestrarioDeColores
        Next
        Panel1.Size = New Size(3 * AnchuraDelMuestrario + 4 * DistanciaEntreMuestrarios, 2 * AlturaDelMuestrario + 3 * DistanciaEntreMuestrarios)
        XAux = DistanciaIzquierda + Maximo(MatrizDeBotones(53).Location.X + AnchuraDelBoton, Panel1.Location.X + Panel1.Size.Width)
        YAux = DistanciaArriba + Maximo(MatrizDeBotones(17).Location.Y + AlturaDelBoton, Panel1.Location.Y + Panel1.Size.Height)
        Me.ClientSize = New Size(XAux, YAux)
        XAux = CType((MatrizDeBotones(18).Location.X + MatrizDeBotones(20).Location.X + MatrizDeBotones(20).Size.Width) / 2 - btnSolucionarSudoku.Size.Width / 2, Integer)
        YAux = CType((MatrizDeBotones(27).Location.Y + MatrizDeBotones(33).Location.Y + MatrizDeBotones(33).Size.Height) / 2 - btnSolucionarSudoku.Size.Height / 2, Integer)
        btnSolucionarSudoku.Location = New Point(XAux, YAux)
    End Sub

    Private Sub CuandoPicamosUnBotonDelCubo(sender As Object, e As EventArgs)
        Dim BotonEnlazado As Button = CType(sender, Button)
        If CType(BotonEnlazado.Name, Integer) Mod 9 = 4 Then
            AdvertimosAlUsuario("No puedes modificar el color de las casillas centrales de cada cara", "Color no modificable")
        ElseIf ColorSeleccionado = -1 Then
            AdvertimosAlUsuario("Para poder asignar un color, primero tienes que seleccionarlo en el muestrario", "Selecciona en el muestrario")
        Else
            BotonEnlazado.BackColor = Colores(ColorSeleccionado)
        End If
    End Sub

    Private Sub CuandoPicamosUnMuestrarioDeColores(sender As Object, e As EventArgs)
        Dim MuestrarioEnlazado As Button = CType(sender, Button)
        If ColorSeleccionado <> -1 Then MatrizDeMuestrarios(ColorSeleccionado).FlatAppearance.BorderSize = BordeMinimo
        ColorSeleccionado = CType(MuestrarioEnlazado.Name, Integer)
        MatrizDeMuestrarios(ColorSeleccionado).FlatAppearance.BorderSize = BordeMaximo
    End Sub

    Private Function EstaCorrectamenteRellenadaLaMatrizDeControles() As Boolean
        Dim CuentaCasillas, CuentaColores As Integer
        For CuentaCasillas = 0 To 53
            For CuentaColores = 0 To 5
                If MatrizDeBotones(CuentaCasillas).BackColor = Colores(CuentaColores) Then Exit For
            Next
            If CuentaColores = 6 Then Return False
        Next
        Return True
    End Function

    Private Sub btnPruebas_Click(sender As Object, e As EventArgs) Handles btnPruebas.Click
        CuboDeRubikInicial = New ClaseCuboDeRubik({1377123, 3175053, 624749, 8993974, 4445530, 2183841})
        InicializarCuboDeRubik(CuboDeRubikInicial, 20, 5)
    End Sub

    Private Sub AsignamosMatrizDeControlesACuboDeRubik(ByRef CuboAuxiliar As ClaseCuboDeRubik)
        InicializarMatriz(CuboAuxiliar.MatrizDeCuboDeRubik)
        Dim Contador, CasillaAzul, CasillaVerde, CasillaBlanca As Integer
        For Contador = 0 To 8
            CuboAuxiliar.MatrizDeCuboDeRubik(0) += CualEsElCodigoDeColor(MatrizDeBotones(Contador).BackColor) * Potencia(6, Contador)
            CuboAuxiliar.MatrizDeCuboDeRubik(1) += CualEsElCodigoDeColor(MatrizDeBotones(9 + Contador).BackColor) * Potencia(6, Contador)
        Next
        For Contador = 0 To 8
            Select Case Contador
                Case 0 : CasillaAzul = 6 : CasillaVerde = 2 : CasillaBlanca = 8
                Case 1 : CasillaAzul = 3 : CasillaVerde = 5 : CasillaBlanca = 7
                Case 2 : CasillaAzul = 0 : CasillaVerde = 8 : CasillaBlanca = 6
                Case 3 : CasillaAzul = 7 : CasillaVerde = 1 : CasillaBlanca = 5
                Case 4 : CasillaAzul = 4 : CasillaVerde = 4 : CasillaBlanca = 4
                Case 5 : CasillaAzul = 1 : CasillaVerde = 7 : CasillaBlanca = 3
                Case 6 : CasillaAzul = 8 : CasillaVerde = 0 : CasillaBlanca = 2
                Case 7 : CasillaAzul = 5 : CasillaVerde = 3 : CasillaBlanca = 1
                Case 8 : CasillaAzul = 2 : CasillaVerde = 6 : CasillaBlanca = 0
                Case Else : SalimosConError(22) : Stop : End
            End Select
            CuboAuxiliar.MatrizDeCuboDeRubik(2) += CualEsElCodigoDeColor(MatrizDeBotones(18 + Contador).BackColor) * Potencia(6, CasillaAzul)
            CuboAuxiliar.MatrizDeCuboDeRubik(3) += CualEsElCodigoDeColor(MatrizDeBotones(27 + Contador).BackColor) * Potencia(6, CasillaBlanca)
            CuboAuxiliar.MatrizDeCuboDeRubik(4) += CualEsElCodigoDeColor(MatrizDeBotones(36 + Contador).BackColor) * Potencia(6, CasillaVerde)
            CuboAuxiliar.MatrizDeCuboDeRubik(5) += CualEsElCodigoDeColor(MatrizDeBotones(45 + Contador).BackColor) * Potencia(6, CasillaBlanca)
        Next
    End Sub


End Class


