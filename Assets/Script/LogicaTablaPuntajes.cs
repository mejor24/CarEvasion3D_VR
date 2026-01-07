using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LogicaTablaPuntajes : MonoBehaviour
{
    public TextMeshProUGUI textoTiempoActual;
    public TextMeshProUGUI textoListaPuntajes; // El cuadro grande de la tabla
    public GameObject panelTabla;

    void Start()
    {
        // 1. Mostrar el tiempo de la partida que acaba de terminar
        float ultimo = PlayerPrefs.GetFloat("UltimoTiempo", 0f);
        if (textoTiempoActual != null) textoTiempoActual.text = "Tu Tiempo: " + Formatear(ultimo);

        // 2. Generar la tabla de posiciones
        CargarTabla();
        if (panelTabla != null) panelTabla.SetActive(false);
    }

    void CargarTabla()
    {
        // 1. Leer la lista de nombres guardados
        string datosUsuarios = PlayerPrefs.GetString("ListaUsuariosRegistrados", "");

        if (string.IsNullOrEmpty(datosUsuarios))
        {
            if (textoListaPuntajes != null) textoListaPuntajes.text = "No hay récords registrados.";
            return;
        }

        // 2. Limpiar y separar los nombres por la coma
        string[] usuarios = datosUsuarios.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 3. Crear una lista de pares (Nombre, Tiempo) para poder ordenarlos
        List<KeyValuePair<string, float>> records = new List<KeyValuePair<string, float>>();
        foreach (string u in usuarios)
        {
            float tiempoRecord = PlayerPrefs.GetFloat("Record_" + u, 0f);
            records.Add(new KeyValuePair<string, float>(u, tiempoRecord));
        }

        // 4. Ordenar de mayor a menor tiempo (OrderByDescending)
        var listaOrdenada = records.OrderByDescending(x => x.Value).ToList();

        // 5. Construir el string final para el TextMeshPro
        string tablaTexto = "<color=#FFCC00>TABLA DE POSICIONES</color>\n\n";
        for (int i = 0; i < listaOrdenada.Count; i++)
        {
            tablaTexto += (i + 1) + ". " + listaOrdenada[i].Key + " - " + Formatear(listaOrdenada[i].Value) + "\n";
        }

        if (textoListaPuntajes != null)
        {
            textoListaPuntajes.text = tablaTexto;
            Debug.Log("Tabla cargada con " + listaOrdenada.Count + " jugadores.");
        }
    }

    string Formatear(float t)
    {
        int m = Mathf.FloorToInt(t / 60);
        int s = Mathf.FloorToInt(t % 60);
        return string.Format("{0:00}:{1:00}", m, s);
    }

    // Funciones para los botones
    public void BotonJugar() => SceneManager.LoadScene("Prototype 1");
    public void BotonMenu() => SceneManager.LoadScene("Menu");
    public void MostrarTabla() => panelTabla.SetActive(!panelTabla.activeSelf);
}