using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCallback : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnStay;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player")
        OnEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.tag == "Player")
        OnExit.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.tag == "Player")
        OnStay.Invoke();
    }

}
