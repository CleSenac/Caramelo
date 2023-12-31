using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentar : MonoBehaviour
{ 
    public RawImage imagemParaMostrar;

    private void Start()
    {
        imagemParaMostrar.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Substitua "Player" pela tag do objeto que entrar� no collider.
        {
            imagemParaMostrar.texture = Resources.Load<Texture>("Mensagem_sentar"); // Substitua "NomeDaImagem" pelo nome da imagem no projeto Resources.
            imagemParaMostrar.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Substitua "Player" pela tag do objeto que sair� do collider.
        {
            Destroy(imagemParaMostrar);
        }
    }
}
