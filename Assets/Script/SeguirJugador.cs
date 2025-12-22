using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0f, 1.2f, 0.5f); 

    void LateUpdate()
    {
        if (player != null)
        {
            // Sigue al jugador con la distancia definida en el offset
            transform.position = player.transform.position + offset;
        }
    }
}