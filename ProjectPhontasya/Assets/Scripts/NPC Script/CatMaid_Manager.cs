using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMaid_Manager : NPC_Base_manager
{
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            myAnimator.SetTrigger("PlayerEnter");
        }
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
