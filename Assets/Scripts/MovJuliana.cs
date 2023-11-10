using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MovJuliana : MonoBehaviour
{
    public Transform[] pontos;
    public float velocidade = 2.0f;
    public Animator animator;
    public float intervaloEntrePontos = 5.0f;
    public int pontoDeParada = 2;
    public int pontoDeParada2 = 4;
    Rigidbody rb;
    public int pontoAtual = 0;
    private bool comecarMovimento = false;
    public CapsuleCollider capsuleCollider;
    public float intervaloEntreLatidos = 3f;
    float latidos = 0;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
        rb.freezeRotation = true;
        capsuleCollider = GetComponent <CapsuleCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            comecarMovimento = true;
            StartCoroutine(MoverNPC());
            Debug.Log("hahasduihasduads");
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (pontoAtual == 3)
            {
                pontoAtual = 4;
                animator.SetTrigger("Sit to Stand");
            }
        }

        if (Input.GetKey(KeyCode.F) || Input.GetMouseButtonDown(0))
        {
            if (pontoAtual == 6)
            {
                StartCoroutine(LatirDuasVezes());
                {
                }
            }
        }
    }
    IEnumerator LatirDuasVezes()
    {
        // Latido 1
        Latir();
        // Espera o intervalo definido
        yield return new WaitForSeconds(intervaloEntreLatidos);
        // Latido 2
    }

    void Latir()
    {
        latidos++;
        // Coloque aqui o código para reproduzir o som de latido ou realizar outras ações
        if (latidos == 2)
        {
            animator.SetTrigger("Sit to Stand");
            Debug.Log("Woof! Woof!");
        }
    }

    IEnumerator MoverNPC()
    {

        Debug.Log("sChamou MoverNPC - "+pontoAtual);

        if (pontoAtual <= pontoDeParada)
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
            animator.SetTrigger("notWalking");
            pontoAtual++;
            capsuleCollider.enabled = true;

            yield return new WaitForSeconds(intervaloEntrePontos);
        }

        if (pontoAtual == 3)
        {
            Debug.Log("Segunda parte do script - ponto atual: "+pontoAtual);
            animator.SetTrigger("Sitting Idle");
        }

        if (pontoAtual >= pontoDeParada2)
        {
            if (pontoDeParada2 == 5)
            {
                pontoDeParada2 = 50;
            }
            Debug.Log("Terceira ------- parte do script - ponto atual: " + pontoAtual);

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

            pontoAtual++;
            animator.SetTrigger("notWalking");
            capsuleCollider.enabled = true;
            yield return new WaitForSeconds(intervaloEntrePontos);
        }
        if (pontoAtual == 6)
        {
            animator.SetTrigger("Terrified");
        }

        // Aqui, o NPC chegou ao terceiro ponto, e você pode adicionar qualquer lógica que desejar.
        // Por exemplo, você pode desativar o NPC, interagir com objetos, etc.
        // Neste exemplo, estamos apenas parando o movimento.

    }
    void OnEndWalkAnimation()
    {
        animator.SetTrigger("Idle");
    }
}