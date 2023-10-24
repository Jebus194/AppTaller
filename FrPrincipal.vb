Imports CapaLogica
Imports CapaLogica.Fn
'Imports CapaPersistencia
Public Class FrPrincipal

    'variables globales

    Dim RstSoloAutos As DataTable
    Dim RstSoloMotos As DataTable
    Dim RstRepuesto As DataTable
    Dim RstCliente As DataTable
    Dim RstPresupuestosGral As DataTable

    Dim HayPresupuestosenDB As Boolean = False
    Dim DatosAutosaCombo As New List(Of String)
    Dim DatosPatAutoaCombo As New List(Of String)
    Dim DatosMotosaCombo As New List(Of String)
    Dim DatosPatMotosaCombo As New List(Of String)
    Dim DatosClientesaCombo As New List(Of String)
    Dim DatosVehiculosaCombo As New List(Of String)
    Dim DatosPatenteaCombo As New List(Of String)
    Dim DatosRepuestosaCombo As New List(Of String)
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
        LoadClientes()
    End Sub
    Private Sub LoadClientes()
        Dim ClientesTotal As New Cliente
        Dim Clientes_DT As DataTable
        Clientes_DT = ClientesTotal.SearchClientes()

        For Each row As DataRow In Clientes_DT.Rows
            Dim valorConcatenado As String = row("Cliente_Apellido").ToString() & " - " & row("Cliente_Nombre").ToString()
            DatosClientesaCombo.Add(valorConcatenado)
        Next
        'cargo en combo
        PGCliente_cbo.Items.AddRange(DatosClientesaCombo.ToArray())
        LoadPrespuestoGral()

        'cargo array auto
        For Each row As DataRow In RstSoloAutos.Rows
            Dim ParaArmarCbo As String = row("Marca").ToString() + " - " + row("modelo").ToString()
            Dim Patente As String = row("patente").ToString
            DatosAutosaCombo.Add(ParaArmarCbo)
            DatosPatAutoaCombo.Add(Patente)
        Next

        For Each row As DataRow In RstSoloMotos.Rows
            Dim ParaArmarCbo As String = row("Marca").ToString() + " - " + row("modelo").ToString()
            Dim Patente As String = row("patente").ToString
            DatosMotosaCombo.Add(ParaArmarCbo)
            DatosPatMotosaCombo.Add(Patente)
        Next
        For Each row As DataRow In RstRepuesto.Rows
            Dim ParaArmarCbo As String = row("repuesto_nombre").ToString() + " -> " + row("repuesto_precio").ToString()
            DatosRepuestosaCombo.Add(ParaArmarCbo)
        Next



    End Sub

    Private Sub LoadPrespuestoGral()
        'dejo cargado en memoria la tabla prespuestos
        Dim BuscoPresupuestos As New Presupuesto
        Dim Haypresupuestos As Object
        HayPresupuestosenDB = False
        Haypresupuestos = BuscoPresupuestos.SearchPresupuestos()
        If Haypresupuestos = Nothing Then Exit Sub
        HayPresupuestosenDB = True
        RstPresupuestosGral = BuscoPresupuestos.SearchPresupuestos()
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

    Private Sub PGCliente_cbo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PGCliente_cbo.SelectedIndexChanged
        PGNro_txt.Text = "-"
        If Not HayPresupuestosenDB Then Exit Sub
        Dim rela_cliente As Integer
        rela_cliente = PGCliente_cbo.SelectedIndex
        RstPresupuestosGral.Rows.Find($"rela_clientes = {rela_cliente} ")
        'continuar logica
        PGNro_txt.Text = "aca va un numero"
    End Sub
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
        'Loaddata()
        CreoPresupuesto()
    End Sub
#End Region

#Region "Cargo Prepuestos"


#End Region



#Region "creando nuevo Presupuesto"



    Dim Total As Decimal = 0
    Dim Presu_Carga As New Presupuesto


    Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) Handles PMoto_opt.CheckedChanged
        If PMoto_opt.Checked Then
            PMarcaM_cbo.BringToFront()
            PPatenteM_cbo.BringToFront()
        Else
            PMarcaA_cbo.BringToFront()
            PPatenteA_cbo.BringToFront()
        End If
    End Sub


    Private Sub PCrear_Click(sender As Object, e As EventArgs) Handles PCrear_btn.Click
        PFinalizar_btn.Enabled = False
        PTotal_lbl.Text = FormatCurrency(Total)
        TabPresup.Visible = True
        Dim tabsOcultar As New List(Of TabPage) From {Crear, Busqueda}
        For Each tabs In tabsOcultar
            TabPresup.TabPages.Remove(tabs)
        Next
        TabPresup.TabPages.Add(Crear)
        Lockear(False)
        PCliente_cbo.Items.AddRange(DatosClientesaCombo.ToArray())
        PreparoCombos()

    End Sub

    Private Sub PreparoCombos()

        PMarcaA_cbo.Items.AddRange(DatosAutosaCombo.ToArray())
        PMarcaM_cbo.Items.AddRange(DatosMotosaCombo.ToArray())
        PPatenteA_cbo.Items.AddRange(DatosPatAutoaCombo.ToArray())
        PPatenteM_cbo.Items.AddRange(DatosPatMotosaCombo.ToArray())
        PRepuesto_cbo.Items.AddRange(DatosRepuestosaCombo.ToArray())



    End Sub


    Private Sub Lockear(ByVal OnOff As Boolean)
        PGCliente_cbo.Enabled = OnOff
        PGFecha_dtp.Enabled = OnOff

    End Sub

    Private Sub Ppaso1_btn_Click(sender As Object, e As EventArgs) Handles Ppaso1_btn.Click
        Try
            Control1()
            PCliente_gbx.Enabled = False
            PVehiculo_gbx.Visible = True
            PRepuesto_cbo.Items.AddRange(DatosVehiculosaCombo.ToArray())
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub


    Private Sub Ppaso2_btn_Click(sender As Object, e As EventArgs) Handles Ppaso2_btn.Click
        Try
            Control2()
            PVehiculo_gbx.Enabled = False
            PDesperfecto_gbx.Visible = True
            CreoPresupuesto()
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub Control1()
        If PCliente_cbo.Text = "" Then Err.Raise(20200,, "Por favor seleccione un cliente")
    End Sub
    Private Sub Control2()
        If PAuto_opt.Checked Then
            If PMarcaA_cbo.Text = "" Then Err.Raise(20200,, "Por favor seleccione una marca")
        Else
            If PMarcaM_cbo.Text = "" Then Err.Raise(20200,, "Por favor seleccione una marca")
        End If
    End Sub

    Private Sub CreoPresupuesto()
        Dim buscoIDcliente As New SqlArea
        Dim buscoIDVehiculo As New SqlArea
        Dim relacliente As DataTable
        Dim relavehiculo As DataTable
        Dim marcaModelo As String
        If PAuto_opt.Checked Then
            marcaModelo = PMarcaA_cbo.Text
        Else
            marcaModelo = PMarcaM_cbo.Text
        End If
        Dim MarcaSeparada() As String = marcaModelo.Split(New Char() {"-"c})
        Dim idVehiculo, idCliente As Integer
        Dim NombreCompleto As String = PCliente_cbo.Text
        Dim NombreSeparado() As String = NombreCompleto.Split(New Char() {"-"c})
        Dim dtr As DataRow

        relacliente = buscoIDcliente.EjecutoQuery($"Select id_clientes from clientes where cliente_apellido = '{NombreSeparado(0).Trim}' and cliente_nombre = '{NombreSeparado(1).Trim}'")

        relavehiculo = buscoIDVehiculo.EjecutoQuery($"select id_vehiculo from vehiculo where vehiculo_marca = '{MarcaSeparada(0).Trim}' and vehiculo_modelo = '{MarcaSeparada(1).Trim}'")

        dtr = relacliente.Rows(0)
        idCliente = Convert.ToInt32(dtr("id_clientes"))

        dtr = relavehiculo.Rows(0)
        idVehiculo = Convert.ToInt32(dtr("id_vehiculo"))
        Dim fecha As String = Now.ToString("dd-MM-yyyy")
        With Presu_Carga
            .presupuesto_fecha = fecha
            .presupuesto_total = 0
            .rela_clientes = idCliente
            .rela_vehiculos = idVehiculo
        End With

        Dim toSql As New Savedb

        toSql.Presupuesto(Presu_Carga)
        If toSql.Commited Then MsgBox("Ok")


    End Sub

    Private Sub Ppaso3_btn_Click(sender As Object, e As EventArgs) Handles Ppaso3_btn.Click

    End Sub

    Private Sub Ppaso4_btn_Click(sender As Object, e As EventArgs) Handles Ppaso4_btn.Click

    End Sub

    Private Sub PFinalizar_btn_Click(sender As Object, e As EventArgs) Handles PFinalizar_btn.Click

    End Sub

    Private Sub Desperfecto_btn_Click(sender As Object, e As EventArgs) Handles Desperfecto_btn.Click
        MsgBox("Funcionalidad en preparación")
    End Sub

    Private Sub PBuscar_btn_Click(sender As Object, e As EventArgs) Handles PBuscar_btn.Click
        MsgBox("Funcionalidad en preparación")
    End Sub




#End Region

End Class
