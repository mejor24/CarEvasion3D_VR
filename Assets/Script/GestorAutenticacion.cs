using UnityEngine;
using TMPro;
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
    public GestorNavegacion navegador;

   

    void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }

    
    public void EstablecerNombreUsuario(string nombre)
    {
        if (!string.IsNullOrEmpty(nombre))
        {
            PlayerPrefs.SetString("NombreUsuario", nombre);
            PlayerPrefs.Save();
            Debug.Log("Nombre de usuario capturado para la sesión: " + nombre);
        }
    }


    public void CrearCuenta()
    {
        string user = usuarioRegistro.text;
        string pass = contrasenaRegistro.text;

        if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
        {
            PlayerPrefs.SetString("UsuarioGuardado", user);
            PlayerPrefs.SetString("ContrasenaGuardada", pass);
            PlayerPrefs.Save();

            Debug.Log("Cuenta creada con éxito: " + user);

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
        string user = usuarioLogin.text;
        string pass = contrasenaLogin.text;

        if (user == PlayerPrefs.GetString("UsuarioGuardado") &&
            pass == PlayerPrefs.GetString("ContrasenaGuardada"))
        {
            Debug.Log("Login exitoso");

            
            PlayerPrefs.SetString("NombreUsuario", user);
            PlayerPrefs.Save();

            navegador.EntrarAlMenuPrincipal();
        }
        else
        {
            Debug.LogError("Usuario o contraseña incorrectos");
        }
    }

    public void RegistrarPuntaje(float tiempo)
    {
        string usuarioActual = PlayerPrefs.GetString("NombreUsuario", "Invitado");
        float recordActual = PlayerPrefs.GetFloat("Record_" + usuarioActual, 0f);

        if (tiempo > recordActual)
        {
            PlayerPrefs.SetFloat("Record_" + usuarioActual, tiempo);

            string lista = PlayerPrefs.GetString("ListaUsuariosRegistrados", "");
            if (!lista.Contains(usuarioActual))
            {
                PlayerPrefs.SetString("ListaUsuariosRegistrados", lista + usuarioActual + ",");
            }
            PlayerPrefs.Save();
            Debug.Log("Puntaje registrado para: " + usuarioActual + " - Tiempo: " + tiempo);
        }
    }
}