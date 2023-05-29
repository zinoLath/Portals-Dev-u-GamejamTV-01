using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarFase : MonoBehaviour
{
    public string proxFase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CarregarNovaFase(){
        SceneManager.LoadScene(proxFase);
    }
}
