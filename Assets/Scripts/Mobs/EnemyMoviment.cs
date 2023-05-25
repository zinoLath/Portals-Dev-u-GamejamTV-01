using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoroMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D capCollider;
    public float speed = 5f;             // Velocidade de movimento do poro
    public float jumpForce = 5f;         // Força do pulo do poro
    public float jumpDelay = 0.5f;       // Delay entre os pulos do poro

    private bool canJump = true;         // Flag para controlar o pulo do poro
    private float jumpTimer = 0f;        // Timer para controlar o delay entre os pulos

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Verifica se o poro pode pular e se o timer de delay atingiu o valor necessário
        if (canJump && jumpTimer <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        // Movimentação horizontal com delay
        float horizontalInput = Input.GetAxis("Horizontal");

        // Verifica se o poro pode se mover horizontalmente com base no timer de delay
        if (canJump)
        {
            // Calcula a direção de movimento com base nas teclas pressionadas
            Vector2 movement = new Vector2(horizontalInput, 0f);

            // Normaliza o vetor de movimento para manter a mesma velocidade em todas as direções
            movement.Normalize();

            // Move o poro na direção calculada com um leve delay
            StartCoroutine(MoveWithDelay(movement));
        }

        // Atualiza o timer de delay do pulo
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    void Jump()
    {
        // Adiciona uma força vertical ao poro para simular o pulo
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Configura o timer de delay para evitar pulos consecutivos
        jumpTimer = jumpDelay;

        // Impede que o poro pule novamente até que o delay seja concluído
        canJump = false;
    }

    IEnumerator MoveWithDelay(Vector2 movement)
    {
        // Delay para a movimentação do poro
        yield return new WaitForSeconds(0.1f);

        // Move o poro na direção calculada
        rigidBody.velocity = new Vector2(movement.x * speed, rigidBody.velocity.y);

        // Permite que o poro pule novamente após a movimentação
        canJump = true;
    }
}
