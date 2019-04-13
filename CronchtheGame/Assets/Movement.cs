using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    #region editor
    [HideInInspector] public bool debug;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float GroundCheckdist;
    #endregion

    #region enum state
    enum state
    {
        grounded,
        inAir,
        jumping,
        falling
    }
    state playState;
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
        if(debug)
            SetDebug();

        Vector2 playerV2= new Vector2(transform.position.x, transform.position.y);

        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal") * speed, -gravity) * Time.deltaTime;
        
        RaycastHit2D hit = Physics2D.Raycast(playerV2, new Vector2(0f, -1f), GroundCheckdist,mask);    
        
        if(hit.collider!=null && playState!=state.jumping)
        {
            PlayerMove.y = 0;
            playState = state.grounded;
        }   

        if(Input.GetButton("Jump")){
            playState=state.jumping;
            PlayerMove.y = speed* Time.deltaTime;
        }

        PlayerMove += playerV2;
            
        rb.MovePosition(PlayerMove);
        velocity =  PlayerMove;
    }


    void SetDebug() // for deugging purposes
    {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.green, 3);

    }
}

