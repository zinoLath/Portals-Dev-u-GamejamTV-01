using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed = 5f; // Velocidade do projetil
    public int damageAmount = 1; // Quantidade de dano causado

    private Transform player; // Referência ao transform do jogador

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontra o transform do jogador
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized; // Calcula a direção do jogador para o projetil
            float moveDistance = projectileSpeed * Time.deltaTime; // Distância a percorrer por quadro

            transform.position += direction * moveDistance; // Move o projetil na direção do jogador
        }
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Causa dano ao jogador
            PlayerController currentLives = other.gameObject.GetComponent<PlayerController>();
            if (currentLives != null)
            {
                currentLives.TakeDamage(damageAmount);
            }

            Destroy(gameObject); // Destroi o projetil após colidir com o jogador
        }
    }
}






