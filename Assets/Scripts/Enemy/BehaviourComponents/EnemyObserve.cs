using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserve : EnemyBehaviour
{
    public EnemyObserve(Enemy enemy) : base(enemy)
    {
    }

    public void DoObserver(){
        enemy.HasTarget = CheckTarget();
        enemy.CanAttack = CheckAttack();
    }

    public bool CheckTarget(){
       RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, enemy.dir, enemy.targetRange, LayerMask.GetMask("Player"));
       return hit ? true : false;
    }
    
    public bool CheckAttack(){
        return Vector2.Distance(GameManager.GetPlayerPosition(), enemy.transform.position) <= enemy.attackRange;
    }
}
