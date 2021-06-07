using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyMovement moveComp;
    EnemyObserve obsComp;
    // Start is called before the first frame update
    void Start()
    {
        moveComp = new EnemyMovement(this);
    }

    // Update is called once per frame
    void Update()
    {
        moveComp.Move();
    }
}
