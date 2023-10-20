Imports CapaLogica
'Imports CapaPersistencia
Public Class FrPrincipal

    Public Enum Tipo_auto
        Compacto
        Sedán
        Monovolumen
        Utlitario
        Lujo
    End Enum
    Enum TabsNumber
        Inicio = 1
        Vehiculos = 2
        Clientes = 3
        Repuestos = 4
        Desperfectos = 5
        Presupuestos = 6
    End Enum






    Private Sub Agregar_btn_Click(sender As Object, e As EventArgs) Handles Agregar_btn.Click
        ValidarAlta()
        Dim EsAuto As Boolean
        If Auto_opt.Checked Then EsAuto = True

        If EsAuto Then
            If CShort(PuertaCilindr_txt.Text) > 5 Then Err.Raise(20200,, "El auto no puede tener mas de 5 puertas")
            Dim Auto As New Automovil
            With Auto
                .Vehiculo_Marca = Marca_txt.Text
                .Vehiculo_Modelo = Modelo_txt.Text
                .Vehiculo_Patente = Patente_txt.Text
                .Id_vehiculo = -1
                .Id_Auto = -1
                .Auto_Cant_Puertas = CShort(PuertaCilindr_txt.Text)
                .Auto_Tipo = Tipo_cbo.SelectedIndex
            End With
        Else 'es moto
            Dim moto As New Moto
            With moto
                .Id_vehiculo = -1
                .Id_Moto = -1
                .Vehiculo_Marca = Marca_txt.Text
                .Vehiculo_Modelo = Modelo_txt.Text
                .Vehiculo_Patente = Patente_txt.Text
                .Moto_Cilindrada = CShort(PuertaCilindr_txt.Text)
            End With
        End If


    End Sub

    Private Sub ValidarAlta()
        Dim Errores As String = "Por favor ingrese: "
        Dim Moto, hayErrores As Boolean
        Try
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
                Errores = Errores + " la marca"
                Marca_txt.Focus()
                hayErrores = True
            End If
            If PuertaCilindr_txt.Text = "" Then
                Errores = Errores + $" {IIf(Moto = True, "la cilindrada", "la cantidad de puertas")}"
                PuertaCilindr_txt.Focus()
                hayErrores = True
            End If
            If Tipo_cbo.Text = "" And Moto = False Then
                Errores = Errores + " tipo"
                Tipo_cbo.Focus()
                hayErrores = True
            End If

            If hayErrores Then Err.Raise(20200,, $"{Errores}")
        Catch

        End Try
    End Sub

    Private Sub Auto_opt_CheckedChanged(sender As Object, e As EventArgs) Handles Auto_opt.CheckedChanged
        If Auto_opt.Checked Then
            Visibilizar(True)
        Else
            Visibilizar(False)
        End If

    End Sub
    Private Sub Visibilizar(ByVal OnOff As Boolean)
        tipo_lbl.Visible = OnOff
        Tipo_cbo.Visible = OnOff
        If OnOff = True Then PuertaCilindr_lbl.Text = "Cant de Puertas:" Else PuertaCilindr_lbl.Text = "Cilindrada:"
    End Sub
#Region "Carga del Form"
    Private Sub FrPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'mainform_Load
        Dim tabsOcultar As New List(Of TabPage) From {Vehiculo, Cliente, Repuesto, Desperfecto, Presupuesto, Inicio}
        For Each ta In tabsOcultar
            TabGeneral.TabPages.Remove(ta)
        Next
        TabGeneral.TabPages.Add(Inicio)
        'invisiblizar tabs

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

    Private Function GetIDTab()
        Dim i As Short
        i = TabGeneral.SelectedIndex()
        GetIDTab = i
    End Function

#End Region
#Region "SqlArea"

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim query As String = "Select * from VehiAutoMoto"
        Dim Rst As DataTable
        Dim txn As New SqlArea()

        txn.EjecutoQuery("Select * from Clientes")
    End Sub
    'Private Sub LoadData(ByRef Resultado As DataTable, ByRef Query As String)

    '    Dim miConexion As New ConexionSQLServer()

    '    ' Abre la conexión
    '    miConexion.AbrirConexion()

    '    ' Ejecuta una consulta
    '    Dim consulta As String = Query
    '    Resultado = miConexion.EjecutarConsulta(consulta)

    '    ' Cierra la conexión
    '    miConexion.CerrarConexion()

    'End Sub



#End Region

    'prueba conecion



End Class
