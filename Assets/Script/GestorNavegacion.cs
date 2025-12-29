using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorNavegacion : MonoBehaviour
{
    [Header("Paneles de Navegación")]
    public GameObject paginaLogo;
    public GameObject paginaLogin;
    public GameObject paginaRegistro;
    public GameObject paginaMenuPrincipal; // Este se usará si el menú está en la misma escena
    public GameObject paginaOpciones;
    public GameObject paginaTutorial;

    public void AbrirLogin()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1); // Carga InicioSession
        }
        else
        {
            DesactivarTodo();
            if (paginaLogin != null) paginaLogin.SetActive(true);
        }
    }

    public void AbrirRegistro()
    {
        DesactivarTodo();
        if (paginaRegistro != null) paginaRegistro.SetActive(true);
    }

    // ACTUALIZADO: Ahora carga la escena del Menú
    public void EntrarAlMenuPrincipal()
    {
        // Si tu escena de menú se llama "Menu", cámbialo aquí
        // Esto es necesario para que realmente saltes de escena al loguearte
        SceneManager.LoadScene("Menu");
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

    public void Jugar()
    {
        // Carga la escena de la carretera
        SceneManager.LoadScene("Prototype 1");
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