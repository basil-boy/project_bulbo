using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{   //TODO: Inherit Body class?? e.g code clean up

    // Player stats
    public float speed = 2.0f; // move speed
    public int skillPoints = 0;
    public float jump = 200.0f; // jump force
    public int jumps = 0; // number of jumps currently taken
    public int jumpLimit = 2; // max number of jumps
    private Color defaultColor;

    public float knockBack = 150.0f;

    private Collider2D cdr;
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    // For Jump Raycast
    private int LayerGround;
    public float yOffset = 0.06f; // Offset for baddie sprite - start of raycast
    public bool onGround = false;
    private Vector2 groundStart;    // For jump raycast
    private Vector2 groundEnd;  // For jump raycast
    public float groundEndDist = 0.1f;  // distance of raycast

    private bool isgrund;
    
    /*
    private bool IsGrounded()
    {

        // Set/update Raycast line
        groundStart = transform.position; // set ground start at origin of player
        groundStart.y -= yOffset; // offset start to be at bottom of player
        groundEnd = groundStart; // set ground end to bottom of player
        groundEnd.y -= groundEndDist; // offset based on the distance we want

        Debug.DrawLine(groundStart, groundEnd, Color.blue); // debug
        RaycastHit2D hit = Physics2D.Linecast(groundStart, groundEnd); // Create the linecast

        if (hit)
        {
            return true;
        }
        return false;
    }
    */
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //assign rigidbody to 2d
        cdr = GetComponent<Collider2D>(); //assign collider to 2d
        spr = GetComponent<SpriteRenderer>();
        defaultColor = spr.color;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerJump();
        playerMovement();
    }

    // Use for physics stuff
    void FixedUpdate()
    {
       // isgrund = IsGrounded();
    }
    

    //*******************Helper Functions******************//

    // Player Stuffs
    void playerMovement()
    {
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void playerJump()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMP pressed");

            Vector2 v = rb.velocity;
            v.y = 0.0f;

            rb.velocity = v; // Set Y velocity to 0 ~ avoids spam jump high af bug
            rb.AddForce(new Vector2(0, jump));
            /*
            if (isgrund)
            {
                Debug.Log("isground");
                isgrund = false;
                jumps = 0;
            }
            else
            {
                Debug.Log("not ground");
            }

            if (jumps < jumpLimit)
            {
                Vector2 v = rb.velocity;
                v.y = 0.0f;

                rb.velocity = v; // Set Y velocity to 0 ~ avoids spam jump high af bug
                rb.AddForce(new Vector2(0, jump));
                jumps++;
                //Debug.Log ("DOOOUBLE JUMP");
            }*/
        }
    }
    
    // For flicker effect
    IEnumerator flicker(int blink)
    {
        for (int i = 0; i < blink; i++)
        {
            spr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        spr.color = defaultColor;
    }

}