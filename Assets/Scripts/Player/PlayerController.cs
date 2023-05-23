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
    private ContactFilter2D groundFilter;
    private ContactFilter2D leftFilter;
    private ContactFilter2D rightFilter;
    private float jumpInput;
    private List<ContactPoint2D> contactList = new List<ContactPoint2D>(10);
    private Vector2 velocityVector = new Vector2(0,0);
    private float horizontalVelocity = 0.0f;
    private bool isWalledL;
    private bool isWalledR;

    private Rigidbody2D rigidBody;
    private BoxCollider2D capCollider;
    private Vector2 originalGravity;
    public Vector2 gravity;
    void Start()
    {
        originalGravity = Physics2D.gravity;
        rigidBody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<BoxCollider2D>();
    }
    private void Update() {
        jumpInput -= Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = 1f; //buffer
        }
        gravity = Physics2D.gravity;
    }

    public void SetSkasisTurn(float angle)//chamando de skasis por causa de doctor who e pq tbm é referência a um ataque que eu fiz
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            angle
        );
        Physics2D.gravity = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)) * -9.8f;
    }

    void FixedUpdate()
    {
        groundFilter = new ContactFilter2D();
        groundFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        groundFilter.useNormalAngle = true;
        groundFilter.maxNormalAngle = transform.eulerAngles.z + 90 + 40;
        groundFilter.minNormalAngle = transform.eulerAngles.z + 90 - 40;
        
        leftFilter = new ContactFilter2D();
        leftFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        leftFilter.useNormalAngle = true;
        leftFilter.maxNormalAngle = transform.eulerAngles.z + 0 + 40;
        leftFilter.minNormalAngle = transform.eulerAngles.z + 0 - 40;
        
        rightFilter = new ContactFilter2D();
        rightFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        rightFilter.useNormalAngle = true;
        rightFilter.maxNormalAngle = transform.eulerAngles.z + 180 + 40;
        rightFilter.minNormalAngle = transform.eulerAngles.z + 180 - 40;
        capCollider.GetContacts(groundFilter,contactList);
        isGrounded = false;
        foreach (ContactPoint2D colliPoint in contactList) //bem lento mas whatever
        {
            isGrounded = true;
        }
        capCollider.GetContacts(leftFilter,contactList);
        isWalledL = false;
        foreach (ContactPoint2D colliPoint in contactList) //bem lento mas whatever
        {
            isWalledL = true;
        }
        capCollider.GetContacts(rightFilter,contactList);
        isWalledR = false;
        foreach (ContactPoint2D colliPoint in contactList) //bem lento mas whatever
        {
            isWalledR = true;
        }
        if(isGrounded)
        {
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, speed * Input.GetAxisRaw("Horizontal"), acceleration);
        }
        else
        {            
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, speed * Input.GetAxisRaw("Horizontal"), airAcceleration);
        }

        if(isWalledL)
        {
            horizontalVelocity = Mathf.Clamp(horizontalVelocity, 0, speed);
        }
        if(isWalledR)
        {
            horizontalVelocity = Mathf.Clamp(horizontalVelocity, -speed, 0);
        }

        if(jumpInput > 0 && isGrounded)
        {
            rigidBody.velocity = MathHelper.RotateVector(new Vector2(horizontalVelocity,  jumpForce),-transform.eulerAngles.z);
            jumpInput = -1000;
        }
        else
        {
            rigidBody.velocity = MathHelper.RotateVector(new Vector2(horizontalVelocity, 0),-transform.eulerAngles.z);
        }
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
