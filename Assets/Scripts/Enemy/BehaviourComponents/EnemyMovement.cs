using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBehaviour
{
    float patrolRnage = 15f;
    float[] bounds;
    float dir;
    public EnemyMovement(Enemy enemy) : base(enemy)
    {
        bounds = new float[2];
        bounds[0] = transform.position.x - patrolRnage;
        bounds[1] = transform.position.x + patrolRnage;
        dir = 1;
    }

    public void Move(){
        Vector2 newPos = transform.position;
        newPos.x += dir * 0.1f;
        if (newPos.x - bounds[0] < 0 || newPos.x - bounds[1] > 0)
            dir *= -1;
        else
            transform.position = newPos;
    }
}
