using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int scoreCount = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameText;

    private void Awake()
    {
        // Verifica se já existem outras instâncias do GameController
        int numOfGameSessions = FindObjectsOfType<GameController>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject); // Destruir todas as instâncias recém-criadas do GameSession
                                  // para manter os dados salvos entre os níveis e as mortes
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Torna a instância atual a principal e evita a destruição
        }
    }

    void Start()
    {
        gameText.text = "";
        scoreText.text = scoreCount.ToString();
    }

    private void Update()
    {
       // FindObjectOfType<LoosingHeart>().ShowLiveNumber(lives);
    }

    // Ações quando o jogador está morto
    public void ProceedDeath()
    {
        if (lives > 1)
        {
            Invoke("MinusLife", 1.5f);
        }
        else
        {
            gameText.text = "Game Over";
            Invoke("ResetGame", 6f);
        }
    }

    public void MinusLife()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        lives--;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddCoins(int value)
    {
        scoreCount += value;
        scoreText.text = scoreCount.ToString();
    }

    // Destruir a sessão de jogo para reiniciar o jogo
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
