Imports System.Runtime.InteropServices



Public Class Login


    Dim Conexion As Conexion = New Conexion


#Region "Form Behaviors"

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

#End Region


#Region "Drag Form"

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub

    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hwnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer)
    End Sub

    Private Sub TitleBar_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar.MouseMove
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub Login_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#End Region

#Region "Customize Controles"

    Private Sub CustomizeComponents()
        'txtUser
        txtUser.AutoSize = False
        txtUser.Size = New Size(350, 35)

        'txtPass
        txtPass.AutoSize = False
        txtPass.Size = New Size(350, 35)
        txtPass.UseSystemPasswordChar = True
    End Sub

    Private Sub btnLogin_Paint(sender As Object, e As PaintEventArgs) Handles btnLogin.Paint
        Dim buttonPath As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath()
        Dim myRectangle As Rectangle = btnLogin.ClientRectangle
        myRectangle.Inflate(0, 30)
        buttonPath.AddEllipse(myRectangle)
        btnLogin.Region = New Region(buttonPath)
    End Sub


#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CustomizeComponents()

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Me.ValidateChildren And txtUser.Text <> String.Empty And txtPass.Text <> String.Empty Then

            If txtUser.Text = "" And txtPass.Text = "" Then

                MessageBox.Show("Esta es su primera vez iniciando el sistema, por favor cree un usuario/personal para continuar!", "Requisito de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                FormPrincipal.Show()

            ElseIf Conexion.comprobarUsuario(txtUser.Text, txtPass.Text) = True Then
                ' Asignacion variable global para saber quien es el que esta accesando
                'FormPrincipal.txtTipoUsuario.Text = identidadPersonalEntro
                FormPrincipal.Show()
                FormPrincipal.txtNombreUsuario.Text = txtUser.Text


                AddHandler FormPrincipal.FormClosed, AddressOf Me.CerrarSession
                Me.Hide()


            Else
                MessageBox.Show("Usuario o Contraseña incorrectos!", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Ingrese datos, Uno o mas campos vacios!", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CerrarSession(sender As Object, es As FormClosedEventArgs)
        If checkRecuerdame.Checked = True Then
            Me.Show()

        Else
            txtUser.Clear()
            txtPass.Clear()
            Me.Show()
            txtUser.Focus()
        End If



    End Sub
End Class