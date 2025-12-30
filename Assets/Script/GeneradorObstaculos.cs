using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    [Header("Configuración de Cajas")]
    public GameObject obstaculoPrefab;
    public float tiempoEntreSpawns = 2.5f;
    public float distanciaAdelante = 50f;
    public float anchoCarretera = 4f;

    [Header("Referencia del Jugador")]
    public Transform jugador;

    void Start()
    {
        // Empieza a crear obstáculos 2 segundos después de iniciar
        InvokeRepeating("SpawnObstaculo", 2f, tiempoEntreSpawns);
    }

    void SpawnObstaculo()
    {
        if (jugador == null || obstaculoPrefab == null) return;

        // Nombres sin espacios para que Unity no de error
        float posXAleatoria = Random.Range(-anchoCarretera, anchoCarretera);
        float posZAdelante = jugador.position.z + distanciaAdelante;

        Vector3 posicionFinal = new Vector3(posXAleatoria, 0.5f, posZAdelante);

        // Creamos la pirámide
        GameObject nuevoObstaculo = Instantiate(obstaculoPrefab, posicionFinal, Quaternion.identity);

        // Se destruye tras 15 segundos para no ralentizar el juego
        Destroy(nuevoObstaculo, 15f);
    }
}