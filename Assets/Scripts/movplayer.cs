using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class movplayer : MonoBehaviour
{
    public float velocidadeMovimento = 5.0f;
    public float velocidadeRotacao = 200.0f;
    public float forcaPulo = 0.5f;
    public Animator animator;
    bool isAttacking = false;
    bool isBarking = false;
    bool isSitting = false;
    bool isLying = false;
    bool stopped = false;
    private bool noChao;
    private float x, y;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // resumir GetCompon...
        rb.freezeRotation = true; // travar rotacao de personagem ao inserir for�a
        Cursor.lockState = CursorLockMode.Locked; // travar mouse na tela
        Cursor.visible = false; // esconder mouse na tela
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadeRotacao, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadeMovimento);

        // Lança um raio para verificar se o personagem está no chão
        RaycastHit hit;
        float raio = 0.1f; // Ajuste o tamanho do raio conforme necessário

        if (Input.GetMouseButtonDown(1) && isAttacking == false && !Input.GetKey(KeyCode.W) && stopped == false)
        {
            velocidadeMovimento = 0f;
            stopped = true;
            isAttacking = true;
            animator.SetTrigger("Atk");

        }

        if (Input.GetMouseButtonDown(0) || (Input.GetKey(KeyCode.F) && isBarking == false && !Input.GetKey(KeyCode.W) && stopped == false))
        {
            velocidadeMovimento = 0f;
            stopped = true;
            isBarking = true;
            animator.SetTrigger("isBarking");
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raio))
        {
            noChao = true;
        }
        else
        {
            noChao = false;
        }

        if (Input.GetKey(KeyCode.W) && stopped == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("EstaCorrendo", true);
                velocidadeMovimento = 7.5f;
            }
            else
            {
                animator.SetBool("EstaCorrendo", false);
                velocidadeMovimento = 5f;
            }
            animator.SetBool("EstaAndando", true);
        }
        if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("EstaAndando", false);
            animator.SetBool("EstaCorrendo", false);
        }

        if (Input.GetKey(KeyCode.S) && stopped == false)
        {
            animator.SetBool("EstaVoltando", true);
        }

        if (!Input.GetKey(KeyCode.S))
        {
            animator.SetBool("EstaVoltando", false);
        }

        // ainda esta andando enquanto deitado:
        if (Input.GetKey(KeyCode.R) && isBarking == false && stopped == false)
        {
            velocidadeMovimento = 0f;
            stopped = true;
            isLying = true;
            animator.SetBool("isLying", true);
        }
        
        if (Input.GetKey(KeyCode.C) && isSitting == false && stopped == false)
        {
            velocidadeMovimento = 0f;
            stopped = true;
            isSitting = true;
            animator.SetBool("isSitting", true);
        }

        // Verifica se o jogador pressionou a tecla de pulo (barra de espaço) e está no chão
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }
    void OnAttackAnimationEnd()
    {
        isAttacking = false;
        stopped = false;
        velocidadeMovimento = 5f;
    }
    void OnBarkAnimationEnd()
    {
        isBarking = false;
        stopped = false;
        velocidadeMovimento = 5f;
    }
    void OnLieAnimationEnd()
    {
        isLying = false;
        stopped = false;
        velocidadeMovimento = 5f;
    }
    
    void OnSitAnimationEnd()
    {
        isSitting = false;
        stopped = false;
        velocidadeMovimento = 5f;
    }
}