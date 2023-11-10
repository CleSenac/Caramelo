using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovLadrao2 : MonoBehaviour
{
    public Transform[] pontos;
    public float velocidade = 2.0f;
    public Animator animator;
    public float intervaloEntrePontos = 5.0f;
    int pontoAtual = 0;
    public int pontoDeParada = 2;
    public int pontoDeParada2 = 4;
    Rigidbody rb;
    public bool comecarMovimento = false;
    public CapsuleCollider capsuleCollider;
    public MovJuliana ponto;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        capsuleCollider = GetComponent<CapsuleCollider>();
        Debug.Log("hduashuadhudahuad");

        if (ponto.pontoAtual == 6)
        {
            comecarMovimento = true;
            Debug.Log("hahasduihasduads");
            animator.SetTrigger("correndo");
            StartCoroutine(MoverNPC());
        }
    }
    IEnumerator MoverNPC()
    {
        animator.SetTrigger("Walk");

        Vector3 pontoDestino = pontos[pontoAtual].position;

        while (Vector3.Distance(transform.position, pontoDestino) > 0.1f)
        {
            capsuleCollider.enabled = false;
            Vector3 direcao = (pontoDestino - transform.position).normalized;
            Quaternion olharRotacao = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, olharRotacao, Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, pontoDestino, velocidade * Time.deltaTime);

            yield return null;
        }

        capsuleCollider.enabled = true;
        animator.SetTrigger("kicking");

        yield return new WaitForSeconds(intervaloEntrePontos);

        // Aqui, o NPC chegou ao terceiro ponto, e você pode adicionar qualquer lógica que desejar.
        // Por exemplo, você pode desativar o NPC, interagir com objetos, etc.
        // Neste exemplo, estamos apenas parando o movimento.

    }
}
