using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : BehaviourComponent
{
    protected Animator myAnim;

    protected int moveTriggerHash = Animator.StringToHash("MoveTrigger");
    protected int isMoveHash = Animator.StringToHash("IsMove");
    public EnemyBehaviour(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    protected Enemy enemy;
}
