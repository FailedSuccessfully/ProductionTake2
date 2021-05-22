﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{

    public float HPMultiplier;

    public bool isSetectPlayer;
    public bool isIntengible;
    public Vector2 SeePlayerDir;

    public LOSController enemyLOS;
    public AttackController enemyAttack;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLOS.enabled)
        {
            SeePlayerDir = enemyLOS.isLOS();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isTouchingFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            if (collision.GetComponentInParent<AttackController>().getParentTag() == "Player")
            {
                GotHitEvent.Invoke();
            }
        }
        if (collision.tag == "Player")
        {
            enemyAttack.Attack();
        }
    }
}
