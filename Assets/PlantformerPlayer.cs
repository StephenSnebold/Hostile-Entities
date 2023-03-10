using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private BoxCollider2D box;

    private Animator anim;
    private bool faceRight = true;

    [SerializeField] private Collider2D standing;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            anim.SetInteger("Direction", 1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            anim.SetInteger("Direction", 0);
        }

        
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        

        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
        {
            standing.enabled = false;
            anim.SetBool("Ducking", true);
        }
        else
        {
            standing.enabled = true;
            anim.SetBool("Ducking", false);
        }
        
        
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

     

        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }

        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }
        
        
        anim.SetFloat("Speed", Mathf.Abs(deltaX));
       // Vector3 pScale = Vector3.one;
       // if (platform != null)
       // {
       //     pScale = platform.transform.localScale;
       // }
       // if (Mathf.Approximately(deltaX, 0))
        //{
        //    transform.localScale = new Vector3(Mathf.Sign(deltaX)/pScale.x, 1/pScale.y, 1);
       // }
        
    }
    
 
}
