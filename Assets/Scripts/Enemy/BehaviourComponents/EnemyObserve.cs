using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObserve : EnemyBehaviour
{
    public EnemyObserve(Enemy enemy) : base(enemy)
    {
    }

    public void Observe(){
        enemy.HasTarget = CheckTarget();
        enemy.CanAttack = enemy.HasTarget ? CheckAttack() : false;
    }

    bool CheckTarget(){
       return(Physics2D.Raycast(enemy.transform.position, enemy.dir, enemy.targetRange, LayerMask.GetMask("Player")));
    }

    bool CheckAttack(){
        
        return (Vector2.Distance(enemy.transform.position, GameManager.player.transform.position) <= enemy.attackRange);
    }
}
