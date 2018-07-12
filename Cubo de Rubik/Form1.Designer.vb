<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnSolucionarSudoku = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPruebas = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSolucionarSudoku
        '
        Me.btnSolucionarSudoku.Location = New System.Drawing.Point(66, 106)
        Me.btnSolucionarSudoku.Name = "btnSolucionarSudoku"
        Me.btnSolucionarSudoku.Size = New System.Drawing.Size(112, 49)
        Me.btnSolucionarSudoku.TabIndex = 0
        Me.btnSolucionarSudoku.Text = "Solucionar Sudoku"
        Me.btnSolucionarSudoku.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(480, 120)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 100)
        Me.Panel1.TabIndex = 1
        '
        'btnPruebas
        '
        Me.btnPruebas.Location = New System.Drawing.Point(754, 325)
        Me.btnPruebas.Name = "btnPruebas"
        Me.btnPruebas.Size = New System.Drawing.Size(101, 53)
        Me.btnPruebas.TabIndex = 2
        Me.btnPruebas.Text = "Pruebas"
        Me.btnPruebas.UseVisualStyleBackColor = True
        Me.btnPruebas.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 535)
        Me.Controls.Add(Me.btnPruebas)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnSolucionarSudoku)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSolucionarSudoku As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnPruebas As Button
End Class
