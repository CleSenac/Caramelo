using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mensagem_inicial : MonoBehaviour
{
    public RawImage imagemParaMostrar;
    private void Start()
    {
        imagemParaMostrar.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Substitua "Player" pela tag do objeto que entrará no collider.
        {
            imagemParaMostrar.texture = Resources.Load<Texture>("Mensagem_inicial"); // Substitua "NomeDaImagem" pelo nome da imagem no projeto Resources.
            imagemParaMostrar.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Substitua "Player" pela tag do objeto que sairá do collider.
        {
            Destroy(imagemParaMostrar);
        }
    }
}
