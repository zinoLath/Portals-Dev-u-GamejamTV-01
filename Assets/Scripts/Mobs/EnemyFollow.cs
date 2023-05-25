using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private PlayerController playerController;
    private Transform posicaoDoJogador;
    public float velocidadeDoInimigo;

    public void SetPlayerController(PlayerController controller){
        playerController = controller;
    }

    void Start()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("PlayerController");
        if (jogador != null)
        {
            posicaoDoJogador = jogador.transform;
        }
        else
        {
            Debug.LogError("Objeto do jogador não encontrado com a tag 'PlayerController'. Verifique se o jogador está presente na cena e possui a tag correta.");
        }
    }

    void Update()
    {
        SeguirJogador();
    }

    private void SeguirJogador()
    {
        if (posicaoDoJogador != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, posicaoDoJogador.position, velocidadeDoInimigo * Time.deltaTime);
        }
    }
}
