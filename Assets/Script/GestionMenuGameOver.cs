using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionMenuGameOver : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelTablaPuntajes;
    public TextMeshProUGUI textoTiempoPartida;

    void Start()
    {
        // Recuperar y mostrar el tiempo de la última partida
        float ultimoTiempo = PlayerPrefs.GetFloat("UltimoTiempo", 0f);
        if (textoTiempoPartida != null)
        {
            int min = Mathf.FloorToInt(ultimoTiempo / 60);
            int seg = Mathf.FloorToInt(ultimoTiempo % 60);
            textoTiempoPartida.text = string.Format("{0:00}:{1:00}", min, seg);
        }

        if (panelTablaPuntajes != null) panelTablaPuntajes.SetActive(false);
    }

    // Botón JUGAR: Regresa a la carrera
    public void ClickJugar()
    {
        SceneManager.LoadScene("Prototype 1");
    }

    // Botón MENÚ: Regresa al Login/Inicio
    public void ClickMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    // Botón PUNTAJE: Muestra/Oculta la tabla
    public void AlternarTablaPuntajes()
    {
        if (panelTablaPuntajes != null)
        {
            bool estadoActual = panelTablaPuntajes.activeSelf;
            panelTablaPuntajes.SetActive(!estadoActual);
        }
    }
}