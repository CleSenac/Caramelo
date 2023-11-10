using UnityEngine;

public class RotacaoElevacaoHorizontal : MonoBehaviour
{
    public float velocidadeRotacao = 60.0f; // Velocidade de rotação em graus por segundo.
    public float amplitudeVertical = 1.0f; // Amplitude do movimento vertical.
    public float velocidadeVertical = 1.0f; // Velocidade do movimento vertical.

    private Transform objetoPai; // Referência ao objeto pai.
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        objetoPai = transform.parent; // Obtém o objeto pai.
    }

    void Update()
    {
        // Rotacionar o objeto em torno do eixo X (horizontal).
        transform.Rotate(Vector3.right * velocidadeRotacao * Time.deltaTime);

        // Mover o objeto verticalmente.
        float verticalOffset = Mathf.Sin(Time.time * velocidadeVertical) * amplitudeVertical;

        // Obter a posição do objeto pai e ajustar apenas os eixos X e Z.
        Vector3 parentPosition = objetoPai.position;
        Vector3 newPosition = new Vector3(parentPosition.x, transform.position.y, parentPosition.z) + Vector3.up * verticalOffset;

        // Atualizar a posição do objeto filho.
        transform.position = newPosition;
    }
}
