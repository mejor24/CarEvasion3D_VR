using UnityEngine;
using TMPro; // Necesario para los cuadros de texto
using UnityEngine.SceneManagement;

public class GestorAutenticacion : MonoBehaviour
{
    [Header("Campos de Login (Panel LOGIN)")]
    public TMP_InputField usuarioLogin;
    public TMP_InputField contrasenaLogin;

    [Header("Campos de Registro (Panel REGISTRO)")]
    public TMP_InputField usuarioRegistro;
    public TMP_InputField contrasenaRegistro;

    [Header("Navegación")]
    public GestorNavegacion navegador; // Referencia al objeto Sistema_Menu

    public void CrearCuenta()
    {
        // Lee los datos de los campos del panel de REGISTRO
        string user = usuarioRegistro.text;
        string pass = contrasenaRegistro.text;

        if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
        {
            // Guarda los datos en la memoria del dispositivo
            PlayerPrefs.SetString("UsuarioGuardado", user);
            PlayerPrefs.SetString("ContrasenaGuardada", pass);
            PlayerPrefs.Save();

            Debug.Log("Cuenta creada con éxito: " + user);

            // Limpia los campos y regresa al Login para entrar
            usuarioRegistro.text = "";
            contrasenaRegistro.text = "";
            if (navegador != null) navegador.AbrirLogin();
        }
        else
        {
            Debug.LogWarning("Campos de registro vacíos");
        }
    }

    public void IniciarSesion()
    {
        // Lee los datos de los campos del panel de LOGIN
        string user = usuarioLogin.text;
        string pass = contrasenaLogin.text;

        // Compara con los datos guardados en PlayerPrefs
        if (user == PlayerPrefs.GetString("UsuarioGuardado") &&
            pass == PlayerPrefs.GetString("ContrasenaGuardada"))
        {
            Debug.Log("Login exitoso");
            navegador.EntrarAlMenuPrincipal(); // Carga la escena o panel de Menú
        }
        else
        {
            Debug.LogError("Usuario o contraseña incorrectos");
        }
    }
}