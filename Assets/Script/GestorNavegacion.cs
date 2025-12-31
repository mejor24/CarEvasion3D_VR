using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorNavegacion : MonoBehaviour
{
    [Header("Paneles de Navegación (Misma Escena)")]
    public GameObject paginaLogo;
    public GameObject paginaLogin;
    public GameObject paginaRegistro;
    public GameObject paginaMenuPrincipal;
    public GameObject paginaOpciones;
    public GameObject paginaTutorial;

    // --- CAMBIO DE ESCENAS ---

    public void AbrirLogin()
    {
        // Si estamos en la escena Logo (index 0), saltamos a InicioSession
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("InicioSession");
        }
        else
        {
            // Si ya estamos en la escena, solo mostramos el panel
            DesactivarTodo();
            if (paginaLogin != null) paginaLogin.SetActive(true);
        }
    }

    // Úsalo en el botón "Entrar" del Login
    public void EntrarAlMenuPrincipal()
    {
        // Si estamos en InicioSession, carga la escena Menu
        if (SceneManager.GetActiveScene().name == "InicioSession")
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            // Si ya estamos en Menu, solo vuelve al panel principal (para botones "Atrás")
            DesactivarTodo();
            if (paginaMenuPrincipal != null) paginaMenuPrincipal.SetActive(true);
        }
    }

    // Úsalo en el botón "CerrarSecion" de la escena Menu
    public void CerrarSesion()
    {
        SceneManager.LoadScene("InicioSession"); // Nombre exacto con doble 's'
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Prototype 1"); // Carga la pista de carreras
    }

    // --- NAVEGACIÓN DE PANELES ---

    public void AbrirRegistro()
    {
        DesactivarTodo();
        if (paginaRegistro != null) paginaRegistro.SetActive(true);
    }

    public void AbrirOpciones()
    {
        DesactivarTodo();
        if (paginaOpciones != null) paginaOpciones.SetActive(true);
    }

    public void AbrirTutorial()
    {
        DesactivarTodo();
        if (paginaTutorial != null) paginaTutorial.SetActive(true);
    }

    private void DesactivarTodo()
    {
        if (paginaLogo != null) paginaLogo.SetActive(false);
        if (paginaLogin != null) paginaLogin.SetActive(false);
        if (paginaRegistro != null) paginaRegistro.SetActive(false);
        if (paginaMenuPrincipal != null) paginaMenuPrincipal.SetActive(false);
        if (paginaOpciones != null) paginaOpciones.SetActive(false);
        if (paginaTutorial != null) paginaTutorial.SetActive(false);
    }
}