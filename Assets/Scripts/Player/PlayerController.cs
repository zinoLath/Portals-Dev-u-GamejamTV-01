using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [Range(0,1)]
    public float acceleration;
    [Range(0,1)]
    public float airAcceleration;
    public float jumpForce;
    public float dashSpeed;
    public float dashDuration;
    public float gravityStrength;
    public float gravityOnUp;
    public float gravityOnDown;
    public bool isGrounded;
    public ContactFilter2D filter;
    public float jumpInput;
    private List<ContactPoint2D> contactList = new List<ContactPoint2D>(10);
    private Vector2 velocityVector = new Vector2(0,0);
    private float horizontalVelocity = 0.0f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D capCollider;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<BoxCollider2D>();
    }
    private void Update() {
        jumpInput -= Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = 1f; //buffer
        }
    }

    void FixedUpdate()
    {
        capCollider.GetContacts(filter,contactList);
        isGrounded = false;
        foreach (ContactPoint2D colliPoint in contactList) //bem lento mas whatever
        {
            isGrounded = true;
        }
        if(isGrounded)
        {
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, speed * Input.GetAxisRaw("Horizontal"), acceleration);
        }
        else
        {            
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, speed * Input.GetAxisRaw("Horizontal"), airAcceleration);
        }
        if(jumpInput > 0 && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            jumpInput = -1000;
        }
        rigidBody.velocity = new Vector2(horizontalVelocity,  rigidBody.velocity.y);
        float vertPut = Input.GetAxisRaw("Vertical"); //se a pessoa segurar pra cima/baixo ela cai mais rápido ou devagar
        if(vertPut > 0)
        {
            rigidBody.gravityScale = Mathf.Lerp(1,gravityOnUp,vertPut) * gravityStrength;
        }
        else
        {
            rigidBody.gravityScale = Mathf.Lerp(1,gravityOnDown,-vertPut) * gravityStrength;
        }
    }
}
