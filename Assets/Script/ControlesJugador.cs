using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro; // Necesario para el texto de la cuenta regresiva
using System.Collections; // Necesario para las Coroutines

public class ControlesJugador : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference accionMovimiento;

    [Header("Movimiento")]
    public float velocidadAdelante = 25f;
    public float aceleracion = 0.5f;
    public float velocidadLateral = 10f;
    private bool puedeMoverse = false; // Bloquea el auto al inicio

    [Header("Sistema de Vidas")]
    public int vidas = 3;

    [Header("Interfaz de Inicio")]
    public TextMeshProUGUI textoCuentaRegresiva; // Arrastra tu texto aquí

    void Start()
    {
        // Al iniciar la escena, lanzamos el conteo
        StartCoroutine(RutinaInicio());
    }

    IEnumerator RutinaInicio()
    {
        puedeMoverse = false; // Aseguramos que el auto no acelere todavía

        textoCuentaRegresiva.text = "3";
        yield return new WaitForSeconds(1);

        textoCuentaRegresiva.text = "2";
        yield return new WaitForSeconds(1);

        textoCuentaRegresiva.text = "1";
        yield return new WaitForSeconds(1);

        textoCuentaRegresiva.text = "¡LISTO!";
        puedeMoverse = true; // El taxi comienza a acelerar automáticamente

        yield return new WaitForSeconds(1);
        textoCuentaRegresiva.gameObject.SetActive(false); // Ocultamos el texto
    }

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
        // Si la cuenta regresiva no ha terminado, no hacemos nada
        if (!puedeMoverse) return;

        // CORRECCIÓN: Forzamos la rotación para que el taxi no se gire al chocar
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Aceleración automática progresiva
        velocidadAdelante += aceleracion * Time.deltaTime;

        // Avanzar hacia adelante
        transform.Translate(Vector3.forward * velocidadAdelante * Time.deltaTime, Space.Self);

        // Leer Input lateral (VR o Teclado)
        float x = accionMovimiento.action.ReadValue<Vector2>().x;

        // Desplazamiento lateral
        transform.Translate(Vector3.right * x * velocidadLateral * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica impacto con el Tag "Obstaculo"
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            vidas--;
            Debug.Log("¡CHOQUE! Vidas restantes: " + vidas);

            if (vidas > 0)
            {
                // Evitamos que el auto traspase o pierda todas las vidas juntas
                Destroy(collision.gameObject);

                // Pequeño retroceso visual del impacto
                transform.Translate(Vector3.back * 2f, Space.Self);
            }
            else
            {
                // Fin del juego: Reinicia la escena
                Debug.Log("GAME OVER. Reiniciando nivel...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}