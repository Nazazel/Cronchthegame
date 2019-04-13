using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private Rigidbody2D rb2d;
    private bool respawning;
    
    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        rb2d = GetComponent<Rigidbody2D>();
        respawning=false;
    }

    // Update is called once per frame
    void Update()      
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if(respawning!=true){
            if(controller.jumpsRemaining == 0)
            {
                death();
            }
            
            if(Input.GetButtonDown("HeartAttack"))
            {
                death();
            }
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
        DeadPlayer body = Instantiate(DeadPlayer, transform.position, transform.rotation).GetComponent<DeadPlayer>();
        body.fallGravity=controller.fallGravity;
        body.setVelocity(rb2d.velocity);
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
    }
}
