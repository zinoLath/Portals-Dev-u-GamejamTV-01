using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharJogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // verifica se a tecla 'ESC' foi apertada
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // roda o método 'SairDoJogo()'
            SairDoJogo();
        }
    }

    public void SairDoJogo()
    {
        // escreve uma mensagem na aba 'Console' para termos certeza de que esse método foi chamado
        Debug.Log("Saiu do jogo");
        // fecha o nosso jogo
        Application.Quit();
    }
}