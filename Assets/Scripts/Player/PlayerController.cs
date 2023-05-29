using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int maxLives = 3; // Número máximo de vidas
    private ContactFilter2D groundFilter;
    private ContactFilter2D leftFilter;
    private ContactFilter2D rightFilter;
    private float jumpInput;
    private List<ContactPoint2D> contactList = new List<ContactPoint2D>(10);
    private Vector2 velocityVector = new Vector2(0,0);
    private float horizontalVelocity = 0.0f;
    private bool isWalledL;
    private bool isWalledR;
    public int currentLives; // Vidas atual do jogador
    private Rigidbody2D rigidBody;
    private BoxCollider2D capCollider;
    private Vector2 originalGravity;
    private bool isPaused = false;


    void Start()
    {
        originalGravity = Physics2D.gravity;
        rigidBody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<BoxCollider2D>();
        currentLives = maxLives; // Define as vidas iniciais para o máximo
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) // Verifica se o objeto que entrou na colisão é uma tag "Enemy"
        {
            currentLives--; // Decrementa uma vida

            if (currentLives <= 0)
            {
                // O jogador perdeu todas as vidas, faça aqui o que for necessário
                Debug.Log("Game Over");
            }
        }
    }

     public void TakeDamage(int damageAmount)
    {
        currentLives -= damageAmount; // Reduz a vida do jogador pelo valor do dano

        if (currentLives <= 0)
        {
            // Se as vidas do jogador chegarem a zero, realiza a ação de fim de jogo
            GameOver();
        }
    }
        private void GameOver()
    {
        // Exibe tela de game over ou realiza outras ações
        // Exemplo: Reiniciar o jogo após um atraso de 3 segundos

        // Aqui você pode adicionar a lógica específica para exibir uma tela de game over ou reiniciar o jogo
        // Neste exemplo, vamos reiniciar o jogo após um atraso de 3 segundos

        // Usamos a função Invoke para aguardar 3 segundos antes de chamar o método RestartGame
        Invoke("RestartGame", 3f);
    }

    private void RestartGame()
    {
        // Reinicia o jogo carregando novamente a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() {
        jumpInput -= Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = 1f; //buffer
        }
    }
    public void SetPositionOfTransform(Transform trans)
    {
        transform.position = trans.position;
    }

    public void SetSkasisTurn(float angle)//chamando de skasis por causa de doctor who e pq tbm é referência a um ataque que eu fiz
    {
        transform.localScale = new Vector3(transform.localScale.x,-transform.localScale.y,transform.localScale.z);
        gravityStrength *= -1;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,  rigidBody.velocity.y/3 + 5f * gravityStrength);
    }

    void FixedUpdate()
    {
        groundFilter = new ContactFilter2D();
        groundFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        groundFilter.useNormalAngle = true;
        groundFilter.maxNormalAngle = 90 + 40 + 90 + 90 * Mathf.Sign(-transform.localScale.y);
        groundFilter.minNormalAngle = 90 - 40 + 90 + 90 * Mathf.Sign(-transform.localScale.y);
        
        leftFilter = new ContactFilter2D();
        leftFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        leftFilter.useNormalAngle = true;
        leftFilter.maxNormalAngle = 0 + 40;
        leftFilter.minNormalAngle = 0 - 40;
        
        rightFilter = new ContactFilter2D();
        rightFilter.SetLayerMask(LayerMask.GetMask("Terrain"));
        rightFilter.useNormalAngle = true;
        rightFilter.maxNormalAngle = 180 + 40;
        rightFilter.minNormalAngle = 180 - 40;
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
        
        rigidBody.velocity = new Vector2(horizontalVelocity,  rigidBody.velocity.y);

        if(jumpInput > 0 && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x,  rigidBody.velocity.y) + (Physics2D.gravity/-9.8f) * Mathf.Sign(gravityStrength) * jumpForce;
            jumpInput = -1000;
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

    public void doYouOnPause(bool isGamePaused){
        isPaused = !isGamePaused;
        if (isPaused){
            rigidBody.velocity = Vector2.zero;
            rigidBody.isKinematic = true;
        }
        else{
            rigidBody.isKinematic = false;
        }
    }
}
