using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseComponent : MonoBehaviour
{
    protected int life = 100;
    protected Rigidbody myRigidbody;
    protected Animator myAnimator;

    protected void InitObjectComponents()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myAnimator = gameObject.GetComponent<Animator>();
        myRigidbody.freezeRotation = true;
    }
}
