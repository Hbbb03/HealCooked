using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public Color firstIngredientColor = Color.yellow;     // Color para el primer ingrediente
    public Color secondIngredientColor = Color.blue;      // Color para el segundo ingrediente
    public Color thirdIngredientColor = Color.green;      // Color final cuando todos los ingredientes est�n en el plato

    public string[] requiredIngredients = { "Apple", "Kiwi", "Banana" }; // Ingredientes espec�ficos
    private int ingredientCount = 0;                     // Contador de ingredientes correctos colocados
    private Renderer plateRenderer;                      // Renderer del plato para cambiar su color

    void Start()
    {
        plateRenderer = GetComponent<Renderer>();
        plateRenderer.material.color = Color.white;      // Color inicial del plato
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene uno de los tags espec�ficos
        if (ingredientCount < requiredIngredients.Length && other.CompareTag(requiredIngredients[ingredientCount]))
        {
            ingredientCount++;
            Debug.Log("Ingrediente correcto detectado: " + other.tag);

            // Cambia el color del plato en funci�n del n�mero de ingredientes colocados
            UpdatePlateColor();
        }
        else
        {
            Debug.Log("Ingrediente incorrecto o ya colocado.");
        }
    }

    void UpdatePlateColor()
    {
        // Cambia el color del plato seg�n el n�mero de ingredientes colocados
        switch (ingredientCount)
        {
            case 1:
                plateRenderer.material.color = firstIngredientColor;
                break;
            case 2:
                plateRenderer.material.color = secondIngredientColor;
                break;
            case 3:
                plateRenderer.material.color = thirdIngredientColor;
                Debug.Log("�Todos los ingredientes colocados! Platillo completado.");
                break;
            default:
                plateRenderer.material.color = Color.white; // Color por defecto
                break;
        }
    }
}
