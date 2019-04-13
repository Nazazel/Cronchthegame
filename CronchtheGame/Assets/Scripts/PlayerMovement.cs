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
    public  Transform respawn; 
    private Rigidbody2D rb2d;
    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()      
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if(controller.jumpsRemaining == 0)
            death();
        
        if(Input.GetButtonDown("HeartAttack"))
        {
            death();
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
        Instantiate(DeadPlayer, transform.position, transform.rotation);
        transform.position =respawn.transform.position;
        rb2d.velocity = Vector3.zero;

    }

    private void explode()
    {
    }
}
