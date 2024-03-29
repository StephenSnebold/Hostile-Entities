using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlantformerPlayer : MonoBehaviour, IDataPersistance
{
    
    public float jumpForce = 36.0f;

    private Rigidbody2D body;
    private BoxCollider2D box;

    public LevelSelect controller;

    private Animator anim;

    private Renderer rend;

    public int TeleportTo;

    private Color color;
    
    public bool faceRight = true;

    [SerializeField] private int Maxhealth = 100;
    
    
    
    
    [Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
    [SerializeField] LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    [SerializeField] Transform m_CeilingCheck;
    
    const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .05f; // Radius of the overlap circle to determine if the player can stand up
    //private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [SerializeField] private Collider2D standing;
    
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        
        
        
        
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    
   

    private void Damage(int damage)
    {
        Maxhealth -= damage;

        if (Maxhealth <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);//temporarycode until gameover screen.
            Maxhealth = 100;
            //DataPersistanceManager.instance.LoadGame();
            Debug.Log("You Died");
        }
        
        
        
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        Physics2D.IgnoreLayerCollision(3,7,false);
        Physics2D.IgnoreLayerCollision(3,9,false);

        //TeleportTo = controller.Teleporting();

        //if (TeleportTo != -1)
        //{
            
       // }
       
        
        DataPersistanceManager.instance.SaveGame();

        
        //controller.setMaxHealth(Maxhealth);


    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            anim.SetInteger("Direction", 1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            anim.SetInteger("Direction", 0);
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        
       
        
    }
    
    public void Move(float move, bool crouch, bool jump)
    {
        anim.SetFloat("Speed", Mathf.Abs(move));
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
                
            }
        }

        

            // If crouching
            if (crouch)
            {
                if (m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= CrouchSpeed;

                // Disable one of the colliders when crouching
                if (standing != null)
                {
                    standing.enabled = false;
                    anim.SetBool("Ducking", true);
                    
                }
                    
                
            } else
            {
                // Enable the collider when not crouching
                if (standing != null)
                {
                    standing.enabled = true;
                    anim.SetBool("Ducking", false);
                }
                    

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, body.velocity.y);
            // And then smoothing it out and applying it to the character
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

        
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            body.AddForce(new Vector2(0, jumpForce));
        }
    }


    IEnumerator Invincablity()
    {
        Physics2D.IgnoreLayerCollision(3,7,true);
        Physics2D.IgnoreLayerCollision(3,9,true);
        
        color.a = 0.5f;
        rend.material.color = color;

        yield return new WaitForSeconds(.3f);
        
        color.a = 1f;
        rend.material.color = color;
        
        yield return new WaitForSeconds(.3f);

        color.a = 0.5f;
        rend.material.color = color;

        yield return new WaitForSeconds(.3f);
        
        color.a = 1f;
        rend.material.color = color;
        
        yield return new WaitForSeconds(.3f);
        
        color.a = 0.5f;
        rend.material.color = color;

        yield return new WaitForSeconds(.3f);
        
        color.a = 1f;
        rend.material.color = color;
        
        yield return new WaitForSeconds(.3f);
        
        color.a = 0.5f;
        rend.material.color = color;

        yield return new WaitForSeconds(.3f);
        Physics2D.IgnoreLayerCollision(3,7,false);
        Physics2D.IgnoreLayerCollision(3,9,false);
        
        color.a = 1f;
        rend.material.color = color;
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" || col.tag == "EnemyShots")
        {
            Damage(10);
            //controller.setMaxHealth(Maxhealth);
            
            
            StartCoroutine(Invincablity());
            Debug.Log("hitByEnemy");
        }

        if (col.tag == "Lava")
        {
            Damage(100);
        }

        
        
    }
}
