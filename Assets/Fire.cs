using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fire : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box;

    public bool deadly = true;

    [SerializeField] private Collider2D deathbox;


// Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    IEnumerator Fireing()
    {
        anim.SetBool("Deadly",true);
        yield return new WaitForSeconds(7);
        anim.SetBool("Deadly", false);
        deadly = false;
        yield return new WaitForSeconds(.3f);
        
        Destroy(gameObject);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Fireing());
        if (deadly == false)
        {
            deathbox.enabled = false;
        }
        else
        {
            deathbox.enabled = true;
        }
    }
    
    
    
}
