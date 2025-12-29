using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float speed = 15f; // Puedes ajustar este valor desde el Inspector

    void Update()
    {
        // Mueve el objeto hacia adelante constantemente
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }
}