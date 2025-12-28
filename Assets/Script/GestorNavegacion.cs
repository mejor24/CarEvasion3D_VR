using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorNavegacion : MonoBehaviour
{
    [Header("Paneles de Navegación")]
    public GameObject paginaLogo;
    public GameObject paginaLogin;
    public GameObject paginaRegistro;
    public GameObject paginaMenuPrincipal;
    public GameObject paginaOpciones; 
    public GameObject paginaTutorial; 

    
    public void AbrirLogin()
    {
        DesactivarTodo();
        paginaLogin.SetActive(true);
    }

   
    public void AbrirRegistro()
    {
        DesactivarTodo();
        paginaRegistro.SetActive(true);
    }

    
    public void EntrarAlMenuPrincipal()
    {
        DesactivarTodo();
        paginaMenuPrincipal.SetActive(true);
    }

    
    public void AbrirOpciones() 
    {
        DesactivarTodo();
        paginaOpciones.SetActive(true);
    }

    public void AbrirTutorial() 
    {
        DesactivarTodo();
        paginaTutorial.SetActive(true);
    }

    public void VolverAlMenu() 
    {
        DesactivarTodo();
        paginaMenuPrincipal.SetActive(true);
    }

    
    public void CerrarSesion()
    {
        DesactivarTodo();
        paginaLogin.SetActive(true);
    }

    
    public void Jugar()
    {
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