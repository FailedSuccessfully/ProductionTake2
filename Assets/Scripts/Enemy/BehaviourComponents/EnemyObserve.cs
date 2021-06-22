using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserve : EnemyBehaviour
{
    public EnemyObserve(Enemy enemy) : base(enemy)
    {
    }

    public void CheckTarget(){
       RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.dir, enemy.targetRange, LayerMask.GetMask("Player"));
       if (hit){
           enemy.HasTarget = true;
       }
    }
}
