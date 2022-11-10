using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : EnemyBaseComponent
{
    private float _speedRotation = 1;
    private Transform _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        InitObjectComponents();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
