using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region editor
    public bool debug;
    [SerializeField] public float speed;
    [SerializeField] public float gravity;
    [SerializeField] public float GroundCheckdist;
    #endregion

    #region enum state
    enum state
    {
        grounded,
        inAir,
        jumping,
        falling
    }
    [SerializeField] state playState;
    #endregion
    
    #region collison and movement
    private Vector2 velocity;
    private LayerMask mask;
    private Rigidbody2D rb;
    #endregion
    // Start is called before the first frame update
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Ground");
        playState = state.inAir; // if we want him to drop we good
    }


    // Update is called once per frame
    void Update()
    {
  
        Vector2 playerV2= new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(playerV2, new Vector2(0f, -1f), GroundCheckdist, mask);

        if (hit.collider != null && playState != state.jumping)
        {
            playState = state.grounded;
        }

        float verticalMove = VerticalVelocity();
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal") * speed* Time.deltaTime, verticalMove) ;
        

        if(Input.GetButtonDown("Jump")){
            playState=state.jumping;
            PlayerMove.y = speed* Time.deltaTime;
        }

    
            
        rb.MovePosition(PlayerMove + playerV2);
        velocity =  PlayerMove;
        if (debug)
            SetDebug(PlayerMove);

    }


    float VerticalVelocity() // get the vertical speed
    {
        float result=gravity*-1;
        if(playState == state.jumping)
        {   
            result = velocity.y-gravity;
            if(velocity.y<0)
            {  
                playState = state.falling;
            }
        }
        else if(playState == state.grounded)
        {
            result = 0;
        }
        return result* Time.deltaTime;
    }


    void SetDebug(Vector2 playerMove) // for deugging purposes
    {
        Debug.DrawRay(transform.position, new Vector3(0, -GroundCheckdist, 0), Color.green, 1);
        Debug.LogFormat("velocity={0}, player movement vector = {1}",velocity,playerMove);

    }
}

