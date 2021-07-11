using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimators
{
    Default,
    Attack
}

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    internal EnemyMovement moveComp;
    internal EnemyAttack atkComp;
    internal Vector2 dir;
    internal float attackRange = 10f;
    internal float targetRange = 80f;
    internal bool HasTarget = false;
    internal bool CanAttack = false;
    internal int boostOnKill = 20;
    EnemyObserve obsComp;
    [SerializeReference]
    internal EnemyStats myStats;
    [SerializeField]
    internal Animator[] animationModels = new Animator[2];
    private Animator activeAnimation;
    [SerializeField]
    internal BoxCollider2D hurtbox;

    internal void AnimationSwitch(EnemyAnimators attack)
    {
        Animator newAnim = animationModels[(int)attack];
        if (activeAnimation != newAnim){
            activeAnimation?.gameObject.SetActive(false);
            activeAnimation = newAnim;
            activeAnimation.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        dir = Vector2.left;
        moveComp = new EnemyMovement(this);
        obsComp = new EnemyObserve(this);
        atkComp = new EnemyAttack(this);

        animationModels[(int)EnemyAnimators.Default].speed = myStats.moveSpd;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dir);
        moveComp.Move();
        obsComp.DoObserver();
        atkComp.Attack();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
