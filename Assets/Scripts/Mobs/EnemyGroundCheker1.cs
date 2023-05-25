using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoroGroundChecker : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public bool IsGrounded;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Ground")
        {
            IsGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            IsGrounded = false;
            Debug.Log("Not Grounded!");
        }
    }
}
