using UnityEngine;

public class PosicionAleatoriaObstaculo : MonoBehaviour
{
    // Rango de la carretera (ajusta según el ancho de tu carril)
    public float rangoX = 4.0f;

    void Start()
    {
        EstablecerPosicionAleatoria();
    }

    void EstablecerPosicionAleatoria()
    {
        // Genera un valor aleatorio para el carril (Izquierda a Derecha)
        float x = Random.Range(-rangoX, rangoX);

        // Mantiene la Y y la Z originales para no enterrarse ni moverse de carril adelantado
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        // Mensaje en consola para verificar (opcional)
        Debug.Log("Obstáculo generado en X: " + x.ToString("F2"));
    }
}
