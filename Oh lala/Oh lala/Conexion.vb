Imports System.Data.SqlClient
Public Class conexion

    Public conexion As SqlConnection = New SqlConnection("Data Source=MOY-ALIENWARE;Initial Catalog=Tienda_Ohlala;Integrated Security=True")

    Public adaptador As SqlDataAdapter
    Public comando As SqlCommand

    Public Sub abrirConexion()
        Try
            conexion.Open()
            conexion.Close()
        Catch ex As Exception
            MessageBox.Show("No se pudo Conectar: " + ex.ToString)
            conexion.Close()
        End Try
    End Sub

    Public Function comprobarUsuario(ByVal usua As String, ByVal contra As String) As Boolean

        conexion.Open()
        Dim sqlcomando As String = "SELECT count(*) FROM Usuario WHERE Username = '" & usua & "' And contrasena ='" & contra & "' "
        comando = conexion.CreateCommand
        comando.CommandText = sqlcomando

        Dim t As Object = CInt(comando.ExecuteScalar())

        conexion.Close()
        If t = 0 Then
            Return False
        End If

        Return True

    End Function
End Class


