using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Eixo de rotação (y-axis por padrão)
    public float rotationSpeed = 50f; // Velocidade de rotação em graus por segundo

    void Update()
    {
        // Rotaciona o objeto no eixo especificado
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

}
