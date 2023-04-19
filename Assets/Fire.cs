using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fire : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box;

    public bool delete;
    
    public bool deadly = true;

    //private Coroutine co;
    

    [SerializeField] private Collider2D deathbox;


// Start is called before the first frame update
    void Start()
    {
        delete = false;
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

        delete = true;
    }
    
    public void KillProjectile()
    {
        delete = true;
        StopAllCoroutines();
        //Debug.Log("DiedEarly");
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
        
        if (delete)
        {
            //Debug.Log("DestroyedFire");
            Destroy(gameObject);
        }


        
        
    }



    


}
