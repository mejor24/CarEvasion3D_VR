using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ControlesJugador : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference accionMovimiento;

    [Header("Movimiento")]
    public float velocidadAdelante = 25f;
    public float aceleracion = 0.5f;
    public float velocidadLateral = 15f;
    private bool puedeMoverse = false;

    [Header("Sistema de Vidas y Tiempo")]
    public int vidas = 3;
    private float tiempoTranscurrido = 0f;
    public TextMeshProUGUI textoCronometro;

    [Header("Interfaz de Inicio")]
    public TextMeshProUGUI textoCuentaRegresiva;

    [Header("Audio")]
    private AudioSource fuenteAudio;

    [Header("Referencias VR (Ocultar Manos)")]
    public GameObject manoIzquierda; // Arrastra el Left Hand Model aquí
    public GameObject manoDerecha;   // Arrastra el Right Hand Model aquí

    private string nombreUsuario;

    void Start()
    {
        nombreUsuario = PlayerPrefs.GetString("NombreUsuario", "Invitado");
        Time.timeScale = 1f;
        fuenteAudio = GetComponent<AudioSource>();

        if (textoCronometro != null) textoCronometro.text = "00:00";

        // Aseguramos que las manos estén visibles durante la cuenta regresiva
        SetManosVisibles(true);

        StartCoroutine(RutinaInicio());
    }

    IEnumerator RutinaInicio()
    {
        puedeMoverse = false;
        if (textoCuentaRegresiva != null)
        {
            textoCuentaRegresiva.gameObject.SetActive(true);
            textoCuentaRegresiva.text = "3"; yield return new WaitForSeconds(1);
            textoCuentaRegresiva.text = "2"; yield return new WaitForSeconds(1);
            textoCuentaRegresiva.text = "1"; yield return new WaitForSeconds(1);
            textoCuentaRegresiva.text = "¡LISTO!"; yield return new WaitForSeconds(1);
            textoCuentaRegresiva.gameObject.SetActive(false);
        }

        // OCULTAR MANOS justo cuando empieza el movimiento para mayor inmersión
        SetManosVisibles(false);
        puedeMoverse = true;
    }

    void Update()
    {
        if (!puedeMoverse) return;

        tiempoTranscurrido += Time.deltaTime;
        ActualizarTextoCronometro();

        transform.rotation = Quaternion.Euler(0, 0, 0);
        velocidadAdelante += aceleracion * Time.deltaTime;
        transform.Translate(Vector3.forward * velocidadAdelante * Time.deltaTime, Space.Self);

        if (accionMovimiento != null)
        {
            float x = accionMovimiento.action.ReadValue<Vector2>().x;
            transform.Translate(Vector3.right * x * velocidadLateral * Time.deltaTime, Space.Self);
        }
    }

    void ActualizarTextoCronometro()
    {
        if (textoCronometro != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
            textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            if (fuenteAudio != null) fuenteAudio.Play();

            vidas--;
            if (vidas > 0)
            {
                Destroy(collision.gameObject);
                transform.Translate(Vector3.back * 2f, Space.Self);
            }
            else
            {
                FinalizarJuego();
            }
        }
    }

    void FinalizarJuego()
    {
        puedeMoverse = false;

        // MOSTRAR MANOS para poder usar los botones del menú final
        SetManosVisibles(true);

        PlayerPrefs.SetFloat("UltimoTiempo", tiempoTranscurrido);

        GestorAutenticacion gestor = Object.FindFirstObjectByType<GestorAutenticacion>();
        if (gestor != null)
        {
            gestor.RegistrarPuntaje(tiempoTranscurrido);
        }
        else
        {
            Debug.LogWarning("No se encontró el GestorAutenticacion en la escena.");
        }

        SceneManager.LoadScene("menugameOver");
    }

    // Función auxiliar para controlar ambas manos a la vez
    private void SetManosVisibles(bool estado)
    {
        if (manoIzquierda != null) manoIzquierda.SetActive(estado);
        if (manoDerecha != null) manoDerecha.SetActive(estado);
    }

    void OnEnable() { if (accionMovimiento != null) accionMovimiento.action.Enable(); }
    void OnDisable() { if (accionMovimiento != null) accionMovimiento.action.Disable(); }
}