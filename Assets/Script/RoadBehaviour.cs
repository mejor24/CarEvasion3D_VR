using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    [SerializeField] float distanceTrigger = 20f;   // Distancia para activar el salto
    [SerializeField] float roadEndDistance = 20f;    // Longitud del tramo

    [SerializeField] float distanceToCamera = 0;    // Distancia actual a la cámara
    [SerializeField] bool isLastRoad;               // ¿Es este el tramo del frente?

    private static RoadBehaviour lastRoad;          // Referencia al tramo delantero
    private static RoadBehaviour firstRoad;         // Referencia al tramo trasero

    private void Awake()
    {
        // Al iniciar, asignamos quién es el primero y quién el último
        if (isLastRoad)
        {
            lastRoad = this;
        }
        else
        {
            firstRoad = this;
        }
    }

    void ChangeRoadsPosition()
    {
        // Mueve el tramo de atrás hacia adelante del todo
        firstRoad.transform.position = new Vector3(firstRoad.transform.position.x, firstRoad.transform.position.y, this.transform.position.z + roadEndDistance);

        // Intercambiamos los roles para el siguiente ciclo
        lastRoad = firstRoad;
        firstRoad = this;
    }

    private void FixedUpdate()
    {
        // Calculamos la distancia respecto a la cámara principal
        distanceToCamera = this.transform.position.z + distanceTrigger - Camera.main.transform.position.z;

        // Si el jugador llega al punto de activación, saltamos la carretera
        if (distanceToCamera <= 1 && lastRoad == this)
        {
            ChangeRoadsPosition();
        }
    }

    // Estas cajitas de colores te ayudan a ver las distancias en el Editor de Unity
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.4F); // Cyan: Inicio
        Gizmos.DrawCube(this.transform.position, new Vector3(80, 44, 1));

        Gizmos.color = new Color(1, 1, 0, 0.4F); // Amarillo: Trigger
        Gizmos.DrawCube(this.transform.position + new Vector3(0, 0, distanceTrigger), new Vector3(80, 44, 1));

        Gizmos.color = new Color(0, 1, 0, 0.4F); // Verde: Final
        Gizmos.DrawCube(this.transform.position + new Vector3(0, 0, roadEndDistance), new Vector3(80, 44, 1));
    }
#endif
}