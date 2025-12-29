using UnityEngine;
using TMPro; // Necesario para leer tus InputFields

public class GestorAutenticacion : MonoBehaviour
{
    [Header("Campos de Registro")]
    public TMP_InputField usuarioRegistro;
    public TMP_InputField contraRegistro;

    [Header("Campos de Login")]
    public TMP_InputField usuarioLogin;
    public TMP_InputField contraLogin;

    public GestorNavegacion navegacion; // Para cambiar de panel automáticamente

    // FUNCIÓN PARA CREAR CUENTA
    public void RegistrarUsuario()
    {
        string user = usuarioRegistro.text;
        string pass = contraRegistro.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            Debug.Log("Error: Debes llenar todos los campos");
            return;
        }

        // Guardamos los datos localmente
        PlayerPrefs.SetString("User_" + user, pass);
        PlayerPrefs.Save();

        Debug.Log("Cuenta creada con éxito para: " + user);

        // Limpiamos campos y mandamos al Login
        usuarioRegistro.text = "";
        contraRegistro.text = "";
        navegacion.AbrirLogin();
    }

    // FUNCIÓN PARA INICIAR SESIÓN
    public void VerificarLogin()
    {
        string user = usuarioLogin.text;
        string pass = contraLogin.text;

        // Validamos si el usuario existe
        if (PlayerPrefs.HasKey("User_" + user))
        {
            string passGuardada = PlayerPrefs.GetString("User_" + user);

            if (pass == passGuardada)
            {
                Debug.Log("¡Acceso concedido!");
                navegacion.EntrarAlMenuPrincipal();
            }
            else
            {
                Debug.Log("Error: Contraseña incorrecta");
            }
        }
        else
        {
            Debug.Log("Error: El usuario no existe");
        }
    }
}