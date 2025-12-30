using UnityEngine;
using UnityEngine.InputSystem;

public class ControlesJugador : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference accionMovimiento;

    [Header("Movimiento")]
    public float velocidadAdelante = 25f;
    public float aceleracion = 0.5f;
    public float velocidadLateral = 10f;

    void OnEnable()
    {
        accionMovimiento.action.Enable();
    }

    void OnDisable()
    {
        accionMovimiento.action.Disable();
    }

    void Update()
    {
        // Aceleración automática
        velocidadAdelante += aceleracion * Time.deltaTime;

        // Avanza
        transform.Translate(Vector3.forward * velocidadAdelante * Time.deltaTime, Space.Self);

        // Leer A / D
        float x = accionMovimiento.action.ReadValue<Vector2>().x;

        // Desplazamiento lateral (SIN GIRAR)
        transform.Translate(Vector3.right * x * velocidadLateral * Time.deltaTime, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si chocamos con el Tag "Obstaculo" que ya configuraste
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            // Reinicia la escena actual
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}

