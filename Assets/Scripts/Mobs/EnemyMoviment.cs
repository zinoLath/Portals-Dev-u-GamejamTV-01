using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    // referência ao componente 'Transform' do jogador
    private Transform posicaoDoJogador;

    // controla a velocidade do jogador
    public float velocidadeDoInimigo;
    public float jumpForce = 5f;
    public float jumpDelay = 0.5f;

    private bool canJump = true;
    private float jumpTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // guarda o componente 'Transform' do jogador na variável 'posicaoDoJogador'
        posicaoDoJogador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // chama o método 'SeguirJogador'
        SeguirJogador();

        // Atualiza o timer de delay do pulo
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void SeguirJogador()
    {
        // verifica se o gameObject do jogador está ativado na aba 'Hierarquia'
        if (posicaoDoJogador.gameObject != null)
        {
            // move o gameObject do inimigo na direção do jogador, utilizando sua velocidade
            transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.position, velocidadeDoInimigo * Time.deltaTime);

            // Verifica se o inimigo pode pular e se o timer de delay atingiu o valor necessário
            if (canJump && jumpTimer <= 0f)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        // Adiciona uma força vertical ao inimigo para simular o pulo
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce * Mathf.Sign(GetComponent<Rigidbody2D>().gravityScale), ForceMode2D.Impulse);

        // Configura o timer de delay para evitar pulos consecutivos
        jumpTimer = jumpDelay;

        // Impede que o inimigo pule novamente até que o delay seja concluído
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Permite que o inimigo pule novamente ao colidir com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
