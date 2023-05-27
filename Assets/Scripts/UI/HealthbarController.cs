using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image insideImage;
    public PlayerController player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        insideImage.fillAmount = (float)player.currentLives/player.maxLives;
    }
}
