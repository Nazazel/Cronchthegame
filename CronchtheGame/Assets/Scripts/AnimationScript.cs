using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;
    public bool win;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && controller.IsGrounded())
            {
                anim.Play("RK9Running");
            }
            else if (Input.GetButton("Jump"))
            {
                anim.Play("RK9Jumping");
            }
            else if (controller.IsGrounded())
            {
                anim.Play("RK9Idle");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "win")
        {
            win = true;
            anim.Play("RK9Win");
        }
    }
}
