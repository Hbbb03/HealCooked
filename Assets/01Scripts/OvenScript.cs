using UnityEngine;

public class OvenScript : MonoBehaviour
{
    public Color cookingColor = Color.red;    // Color del horno mientras cocina
    public float cookTime = 3f;               // Tiempo de cocci�n en segundos
    public float respawnDistance = 1f;        // Distancia a la que reaparece la manzana

    private Color originalColor;              // Color original del horno
    private Renderer ovenRenderer;            // Renderer del horno
    private GameObject apple;                 // Referencia a la manzana

    public float respawnDistancew = 4f;    // Distancia en el eje X para reaparecer la manzana
    public float respawnHeightw = 2f;

    void Start()
    {
        ovenRenderer = GetComponent<Renderer>();
        originalColor = ovenRenderer.material.color;
        Debug.Log("Horno inicializado. Color original: " + originalColor);

        // Detecta si alg�n objeto con etiqueta "Apple" est� en escena
        GameObject[] apples = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject foundApple in apples)
        {
            Debug.Log("Manzana detectada en la escena: " + foundApple.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisi�n detectada con objeto: " + other.name);

        // Detecta si un objeto con la etiqueta "Apple" entra en el trigger
        if (other.CompareTag("Apple"))
        {
            apple = other.gameObject;
            Debug.Log("�Manzana detectada! Ocultando manzana.");
            apple.SetActive(false); // Oculta la manzana
            StartCooking();         // Inicia el proceso de cocci�n
        }
        else
        {
            Debug.Log("Objeto no es una manzana.");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Horno clicado.");

        // Detecta clic en el horno solo si la manzana est� oculta
        if (apple != null && !apple.activeInHierarchy)
        {
            Debug.Log("Iniciando la rutina de cocci�n.");
            StartCoroutine(CookRoutine()); // Inicia la rutina de cocci�n
        }
        else
        {
            Debug.Log("No se inicia cocci�n porque la manzana no est� oculta o es nula.");
        }
    }

    private void StartCooking()
    {
        Debug.Log("Iniciando cocci�n. Cambiando color del horno.");
        ovenRenderer.material.color = cookingColor; // Cambia el color del horno
        Invoke("RespawnApple", cookTime);           // Espera y vuelve a mostrar la manzana despu�s del tiempo de cocci�n
    }

    private System.Collections.IEnumerator CookRoutine()
    {
        ovenRenderer.material.color = cookingColor;   // Cambia el color del horno
        yield return new WaitForSeconds(cookTime);     // Espera el tiempo de cocci�n
        ovenRenderer.material.color = originalColor;   // Restaura el color original
        Debug.Log("Cocci�n finalizada. Color restaurado.");
    }

    private void RespawnApple()
    {
        if (apple != null)
        {
            // Colocamos la posici�n de reaparici�n m�s a la derecha y un poco m�s arriba
            Vector3 respawnPosition = transform.position + (Vector3.right * respawnDistancew) + (Vector3.up * respawnHeightw);
            Debug.Log("Reapareciendo la manzana en posici�n: " + respawnPosition);
            apple.transform.position = respawnPosition;
            apple.SetActive(true);  // Vuelve a mostrar la manzana
        }
        else
        {
            Debug.LogWarning("No se encontr� la referencia de la manzana para reaparecer.");
        }

        ovenRenderer.material.color = originalColor; // Restaura el color del horno
    }
}
