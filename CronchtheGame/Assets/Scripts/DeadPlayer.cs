using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public LayerMask mask;
    public float checkDis;
    private float m_FallGravity;

    public float fallGravity{set{m_FallGravity = value;}}
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(.5f, .5f), 0, Vector2.down, .75f, mask);
        if (rb2d.velocity.y < 0) //we are falling, therefore increase gravity down
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;

        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))//Tab Jump
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
    }

    public void setVelocity(Vector2 velocity)
    {
        rb2d.velocity = velocity;
    }
    
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
    }
}
