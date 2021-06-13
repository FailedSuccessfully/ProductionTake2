using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BehaviourComponent
{
    public EnemyBehaviour(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    protected Enemy enemy;
}
