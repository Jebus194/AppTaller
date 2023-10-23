Imports CapaLogica
'Imports CapaPersistencia
Public Class FrPrincipal

    'variables globales

    Dim RstSoloAutos As DataTable
    Dim RstSoloMotos As DataTable
    Dim RstRepuesto As DataTable
    Dim RstCliente As DataTable


    Public Enum Tipo_auto
        Compacto = 0
        Sedán = 1
        Monovolumen = 2
        Utlitario = 3
        Lujo = 4
    End Enum
    Enum TabsNumber
        Inicio = 1
        Vehiculos = 2
        Clientes = 3
        Repuestos = 4
        Desperfectos = 5
        Presupuestos = 6
    End Enum

    Private Sub Cancelar_btn_Click(sender As Object, e As EventArgs) Handles Cancelar_btn.Click
        'cierro ventana
        Me.Close()
    End Sub

#Region "Botones Añadir"
    Private Sub Agregar_btn_Click(sender As Object, e As EventArgs) Handles Agregar_btn.Click
        ValidarAlta("vehiculo")
        Dim EsAuto As Boolean
        Dim Auto As New Automovil
        Dim moto As New Moto
        If Auto_opt.Checked Then EsAuto = True
        Dim tipo As Integer

        If EsAuto Then
            If CShort(PuertaCilindr_txt.Text) > 5 Then Err.Raise(20200,, "El auto no puede tener mas de 5 puertas")
            tipo = Tipo_cbo.SelectedIndex
            With Auto
                .Vehiculo_Marca = Marca_txt.Text
                .Vehiculo_Modelo = Modelo_txt.Text
                .Vehiculo_Patente = Patente_txt.Text
                .Id_vehiculo = -1
                .Id_Auto = -1
                .Auto_Cant_Puertas = CShort(PuertaCilindr_txt.Text)
                .Auto_Tipo = 0
            End With
        Else 'es moto
            With Moto
                .Id_vehiculo = -1
                .Id_Moto = -1
                .Vehiculo_Marca = Marca_txt.Text
                .Vehiculo_Modelo = Modelo_txt.Text
                .Vehiculo_Patente = Patente_txt.Text
                .Moto_Cilindrada = CShort(PuertaCilindr_txt.Text)
            End With
        End If

        MsgBox(GuardarAuto(Auto))
        LimpioCampos("vehiculo")
        Loaddata()
    End Sub

    Private Sub Clienteadd_btn_Click(sender As Object, e As EventArgs) Handles Clienteadd_btn.Click
        ValidarAlta("cliente")
        Dim nombre, apellido, email As String
        Dim persona As New Cliente
        nombre = CNombre_txt.Text
        apellido = CApellido_txt.Text
        email = CEmail_txt.Text

        With persona
            .Cliente_Nombre = nombre
            .Cliente_Apellido = apellido
            .Cliente_Email = email
        End With

        MsgBox(GuardarCliente(persona))
        LimpioCampos("cliente")
        Loaddata()
    End Sub
    Private Sub RepuestoAdd_btn_Click(sender As Object, e As EventArgs) Handles RepuestoAdd_btn.Click
        ValidarAlta("repuesto")
        Dim nombre As String
        Dim precio As Decimal
        Dim rep As New Repuesto

        nombre = Rnombre_txt.Text
        precio = RPrecio_txt.Text

        With rep
            .repuesto_nombre = nombre
            .repuesto_precio = precio
        End With

        MsgBox(GuardarRepuesto(rep))
        LimpioCampos("repuesto")
        Loaddata()
    End Sub


#End Region
    Private Sub Auto_opt_CheckedChanged(sender As Object, e As EventArgs) Handles Auto_opt.CheckedChanged
        If Auto_opt.Checked Then
            Visibilizar(True)
        Else
            Visibilizar(False)
        End If

    End Sub

#Region "Carga del Form"
    Private Sub FrPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'mainform_Load
        Dim tabsOcultar As New List(Of TabPage) From {Vehiculo, Cliente, Repuesto, Desperfecto, Presupuesto, Inicio}
        For Each ta In tabsOcultar
            TabGeneral.TabPages.Remove(ta)
        Next
        TabGeneral.TabPages.Add(Inicio)
        'Cargar DT y DGV
        Loaddata()

    End Sub


#End Region
#Region "Movimiento de pestañas"
    Private Sub TabGeneral_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabGeneral.SelectedIndexChanged

    End Sub
    Private Function GetIndexTab()
        Dim i As Short = 0
        If Ini_opt.Checked Then i = 1
        If veh_opt.Checked Then i = 2
        If cli_opt.Checked Then i = 3
        If rep_opt.Checked Then i = 4
        If des_opt.Checked Then i = 5
        If pre_opt.Checked Then i = 6
        GetIndexTab = i
    End Function

    Private Sub Inicio_btn_Click(sender As Object, e As EventArgs) Handles Inicio_btn.Click
        Ini_opt.Checked = True
        Swipe()
    End Sub
    Private Sub Vehiculo_btn_Click(sender As Object, e As EventArgs) Handles Vehiculo_btn.Click
        veh_opt.Checked = True
        Swipe()
    End Sub
    Private Sub cliente_btn_Click(sender As Object, e As EventArgs) Handles Clientes_btn.Click
        cli_opt.Checked = True
        Swipe()
    End Sub

    Private Sub Repuesto_btn_Click(sender As Object, e As EventArgs) Handles Repuestos_btn.Click
        rep_opt.Checked = True
        Swipe()
    End Sub
    Private Sub Presupuesto_btn_Click(sender As Object, e As EventArgs) Handles Presupuestos_btn.Click
        pre_opt.Checked = True
        Swipe()
    End Sub



    Private Function GetIDTab()
        Dim i As Short
        i = TabGeneral.SelectedIndex()
        GetIDTab = i
    End Function

#End Region
#Region "SqlArea"

    'Private Function UltimoId(ByVal Tabla As String)
    '    Dim Execute As New SqlArea
    '    Dim id As Integer
    '    id = Execute.UltimoID(Tabla)
    '    Return id
    'End Function


    Private Function GuardarAuto(ByRef Auto As Automovil)
        Dim Save As New Savedb(Auto)
        If Save.Commited Then
            Return "Añadido Exitosamente"
        Else
            Return "Ha ocurrido un error"
        End If
    End Function

    Private Function GuardarMoto(ByRef Moto As Moto)
        Dim Save As New Savedb(Moto)
        If Save.Commited Then
            Return "Añadido Exitosamente"
        Else
            Return "Ha ocurrido un error"
        End If
    End Function
    Private Function GuardarCliente(ByRef cliente As Cliente)
        Dim Save As New Savedb(cliente)
        If Save.Commited Then
            Return "Añadido Exitosamente"
        Else
            Return "Ha ocurrido un error"
        End If
    End Function


    Private Function GuardarRepuesto(ByRef repuesto As Repuesto)
        Dim Save As New Savedb(repuesto)
        If Save.Commited Then
            Return "Añadido Exitosamente"
        Else
            Return "Ha ocurrido un error"
        End If
    End Function
#End Region

#Region "Acciones"
    Private Sub Visibilizar(ByVal OnOff As Boolean)
        tipo_lbl.Visible = OnOff
        Tipo_cbo.Visible = OnOff
        If OnOff = True Then PuertaCilindr_lbl.Text = "Cant de Puertas:" Else PuertaCilindr_lbl.Text = "Cilindrada:"
    End Sub

    Private Sub Swipe()
        Dim TabsIndex As Short
        Dim tabsOcultar As New List(Of TabPage) From {Vehiculo, Cliente, Repuesto, Desperfecto, Presupuesto, Inicio}
        TabsIndex = GetIndexTab()
        Select Case TabsIndex
            Case TabsNumber.Inicio
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Inicio)
            Case TabsNumber.Vehiculos
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Vehiculo)
            Case TabsNumber.Clientes
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Cliente)
            Case TabsNumber.Repuestos
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Repuesto)
            Case TabsNumber.Desperfectos
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Desperfecto)
            Case TabsNumber.Presupuestos
                For Each ta In tabsOcultar
                    TabGeneral.TabPages.Remove(ta)
                Next
                TabGeneral.TabPages.Add(Presupuesto)
        End Select
    End Sub



#End Region

#Region "Funcionalidad"
    Private Sub ValidarAlta(ByVal Instancia As String)
        Dim Errores As String = "Por favor ingrese: "
        Dim Moto, hayErrores As Boolean
        Try
            Select Case Instancia
                Case "vehiculo"
                    Moto = Moto_opt.Checked
                    If Patente_txt.Text = "" Then
                        Errores = Errores + " el número de patente"
                        Patente_txt.Focus()
                        hayErrores = True
                    End If
                    If Modelo_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el modelo"
                        Modelo_txt.Focus()
                        hayErrores = True
                    End If
                    If Marca_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} la marca"
                        Marca_txt.Focus()
                        hayErrores = True
                    End If
                    If PuertaCilindr_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} {IIf(Moto = True, "la cilindrada", "la cantidad de puertas")}"
                        PuertaCilindr_txt.Focus()
                        hayErrores = True
                    End If
                    If Tipo_cbo.Text = "" And Moto = False Then
                        Errores = Errores + $"{vbCrLf} tipo"
                        Tipo_cbo.Focus()
                        hayErrores = True
                    End If
                Case "cliente"
                    If CNombre_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el nombre"
                        hayErrores = True
                    End If
                    If CApellido_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el apellido"
                        hayErrores = True
                    End If
                    If CEmail_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el correo"
                        hayErrores = True
                    End If
                Case "repuesto"
                    If Rnombre_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el nombre"
                        hayErrores = True
                    End If
                    If RPrecio_txt.Text = "" Then
                        Errores = Errores + $"{vbCrLf} el precio"
                        hayErrores = True
                    End If

                Case "presupuesto"

            End Select
            If hayErrores Then Err.Raise(20200,, $"{Errores}")
        Catch

        End Try
    End Sub

    Private Sub LimpioCampos(ByVal vista As String)
        Select Case vista
            Case "vehiculo"
                Marca_txt.Text = ""
                Modelo_txt.Text = ""
                Patente_txt.Text = ""
                PuertaCilindr_txt.Text = ""
                Tipo_cbo.Text = ""
                Auto_opt.Checked = True
            Case "cliente"
                CNombre_txt.Text = ""
                CApellido_txt.Text = ""
                CEmail_txt.Text = ""
            Case "repuesto"
                Rnombre_txt.Text = ""
                RPrecio_txt.Text = ""
            Case "arreglo"
            Case "presupuesto"
        End Select
    End Sub

    Private Sub Loaddata()
        Dim QueryAutos As String = "Select * from SoloAutos"
        Dim QueryMotos As String = "Select * from SoloMotos"
        Dim QueryClientes As String = "Select * from clientes"
        Dim QueryRepuestos As String = "Select * from Repuestos"

        Dim txnA, txnM, txnC, txnR As New SqlArea()

        RstSoloAutos = txnA.EjecutoQuery(QueryAutos)
        RstSoloMotos = txnM.EjecutoQuery(QueryMotos)
        RstCliente = txnC.EjecutoQuery(QueryClientes)
        RstRepuesto = txnR.EjecutoQuery(QueryRepuestos)

        'cargo y actualizo datagrid
        SoloAutos_dgv.DataSource = RstSoloAutos
        SoloAutos_dgv.Refresh()

        SoloMotos_dgv.DataSource = RstSoloMotos
        SoloMotos_dgv.Refresh()

        clientes_dgv.DataSource = RstCliente
        clientes_dgv.Refresh()

        Repuestos_dgv.DataSource = RstRepuesto
        Repuestos_dgv.Refresh()
        'cargo combobox
        Dim valoresEnum As Array = [Enum].GetValues(GetType(Tipo_auto))
        For Each valor As Tipo_auto In valoresEnum
            Tipo_cbo.Items.Add(valor)
        Next
    End Sub


#End Region

#Region "testing"
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Loaddata()
    End Sub



#End Region

End Class
