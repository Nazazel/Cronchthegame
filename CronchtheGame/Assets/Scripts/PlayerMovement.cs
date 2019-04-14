using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// PlayeMovement handles the movement of the player by specifying player speed, reading user Input,
    /// and calling CharacterController2D to move the Player Object  
    /// </summary>

    [SerializeField] private float runSpeed;
    float horizontalMove = 0f;
    bool jump = false;
    public CharacterController2D controller;
    public  GameObject DeadPlayer;
    public GameObject ElectricBody;
    public  Transform respawnPoint; 
    public float respawnTime;
    public float deathHeight;
    public LayerMask explosionLayers;

    private Rigidbody2D rb2d;
    private bool respawning;
    private Vector2 preJumpVelocity;
    private float jumpDistance;
    private bool electric;
    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        rb2d = GetComponent<Rigidbody2D>();
        respawning=false;
        jumpDistance = transform.position.y;
        StartCoroutine("respawn");

    }

    // Update is called once per frame
    void Update()      
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpDistance=transform.position.y;
            preJumpVelocity = rb2d.velocity;
        }
       

        if(respawning!=true){
            if(controller.jumpsRemaining == 0)
            {
                explode(rb2d.velocity,preJumpVelocity);

            }
            else if(Input.GetButtonDown("explode"))
            {
                transform.position = new Vector2(transform.position.x,transform.position.y+1);
                explode(new Vector2(rb2d.velocity.x,10f),new Vector2(rb2d.velocity.x,0));
            }
            else if(Input.GetButtonDown("HeartAttack"))
            {
                if(electric == true)
                {
                    electricDeath();
                }
                else
                    death();
            }
            else if(controller.IsGrounded()==true && Mathf.Abs(jumpDistance-transform.position.y)>deathHeight)
            {
                death();
            }
        }
        if (Input.GetButtonDown("restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 

        }
        if (controller.IsGrounded())
            {
                jumpDistance = transform.position.y;
        }
    }

    // FixedUpdate is called multiple times per x amount of frames
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
    

#region  triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Death")
        {
            death();
        }
        if(other.tag == "electric")
        {
            electric = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "electric")
        {
            electric = false;
        }
    }
#endregion 

#region  deaths
    private void death()
    {
        DeadPlayer dead = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        dead.fallGravity=controller.fallGravity;
        dead.setVelocity(rb2d.velocity);


        transform.position =respawnPoint.transform.position;
        rb2d.velocity = Vector3.zero;
        StartCoroutine("respawn");

    }

    private void explode(Vector2 top, Vector2 bottom)
    {
        var hit = Physics2D.CircleCastAll(transform.position, 5, Vector2.zero, 0, explosionLayers);
        foreach (var wall in hit) // I WANNA PUKE;
        {
            wall.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        }
   
        DeadPlayer torso = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        torso.fallGravity = controller.fallGravity;
        torso.setVelocity(top);
        
        DeadPlayer legs = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        legs.fallGravity = controller.fallGravity;
        legs.setVelocity(bottom);
        transform.position = respawnPoint.transform.position;
        rb2d.velocity = Vector3.zero;
        StartCoroutine("respawn");
    }

    private void electricDeath()
    {
        Instantiate(ElectricBody, transform.position, transform.rotation);
        transform.position = respawnPoint.transform.position;
        rb2d.velocity = Vector3.zero;
        StartCoroutine("respawn");
    }
    #endregion

    IEnumerator respawn()
    {
        respawning = true;
        yield return new WaitForSeconds(respawnTime);
        respawning = false;
    }

}
