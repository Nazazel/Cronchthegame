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
    public  Transform respawnPoint; 
    public float respawnTime;
    public float deathHeight;

    private Rigidbody2D rb2d;
    private bool respawning;
    private Vector2 preJumpVelocity;
    private float jumpDistance;
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
                death();

            }
            else if(Input.GetButtonDown("HeartAttack"))
            {
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
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Death")
        {
            death();
        }
    }

    private void death()
    {
        DeadPlayer torso = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        torso.fallGravity=controller.fallGravity;
        torso.setVelocity(rb2d.velocity);

        DeadPlayer legs = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        legs.fallGravity = controller.fallGravity;
        legs.setVelocity(preJumpVelocity);
        transform.position =respawnPoint.transform.position;
        rb2d.velocity = Vector3.zero;
        StartCoroutine("respawn");

    }

    IEnumerator respawn()
    {
        respawning=true;
        yield return new WaitForSeconds(respawnTime);
        respawning=false;
    }
    private void explode()
    {
        
        death();
    }
}
