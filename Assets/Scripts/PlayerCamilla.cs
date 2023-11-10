using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamilla : MonoBehaviour
{
    public float velocidadeMovimento = 5.0f;
    public float velocidadeRotacao = 200.0f;
    public float forcaPulo = 10.0f;
    private float x, y;
    private bool noChao; // Indica se o personagem está no chão

    // O método Update é chamado a cada quadro
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadeRotacao, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadeMovimento);

        // Lança um raio para verificar se o personagem está no chão
        RaycastHit hit;
        float raio = 1.0f; // Ajuste o tamanho do raio conforme necessário

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raio))
        {
            noChao = true;
        }
        else
        {
            noChao = false;
        }

        // Verifica se o jogador pressionou a tecla de pulo (barra de espaço) e está no chão
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }
}
