using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del jugador
    private Vector3 targetPosition;  // Posición a la que el jugador se moverá
    private bool isMoving = false;  // Si el jugador está moviéndose o no

    void Start()
    {
        // Inicializar la posición de destino en la posición actual del jugador
        targetPosition = transform.position;
    }

    void Update()
    {
        // Detectar clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))  // 0 es para el botón izquierdo del ratón
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // Crear un rayo desde la cámara
            RaycastHit hit;

            // Verificar si el rayo colisiona con algo en el mundo (como el suelo)
            if (Physics.Raycast(ray, out hit))
            {
                // Si golpea algo, mover al jugador hacia la posición donde se hizo clic
                targetPosition = hit.point;
                isMoving = true;  // Activar el movimiento
            }
        }

        // Si el jugador está en movimiento
        if (isMoving)
        {
            MovePlayer();  // Llamar a la función para mover al jugador
        }
    }

    void MovePlayer()
    {
        // Mover al jugador hacia la posición de destino
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Detener el movimiento cuando llegue al destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;  // El jugador ha llegado al destino
        }
    }
}

