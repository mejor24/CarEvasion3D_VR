using UnityEngine;

public class ControlesJugador : MonoBehaviour
{
    public float velocidadInicial = 15.0f;
    public float aceleracion = 0.5f; // Cuánto aumenta la velocidad por segundo
    public float velocidadLateral = 10.0f; // Qué tan rápido cambia de carril
    
    private float velocidadActual;
    private float entradaHorizontal;

    void Start()
    {
        // Empezamos con la velocidad mínima que pediste
        velocidadActual = velocidadInicial;
    }

    void Update()
    {
        // 1. Aumento progresivo de velocidad
        velocidadActual += aceleracion * Time.deltaTime;

        // 2. Obtener entrada lateral (A/D o Flechas)
        entradaHorizontal = Input.GetAxis("Horizontal");

        // 3. Movimiento hacia adelante constante y progresivo
        transform.Translate(Vector3.forward * Time.deltaTime * velocidadActual);

        // 4. Cambio de carril (Movimiento lateral en el eje X)
        // Usamos Vector3.right para que se mueva de lado en lugar de girar
        transform.Translate(Vector3.right * Time.deltaTime * velocidadLateral * entradaHorizontal);
    }
}
