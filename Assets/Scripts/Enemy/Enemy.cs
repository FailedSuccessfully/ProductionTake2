using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    internal EnemyMovement moveComp;
    internal Vector2 dir;
    internal float attackRange = 10f;
    internal float targetRange = 80f;
    internal bool HasTarget = false;
    internal int boostOnKill = 20;
    EnemyObserve obsComp;
    [SerializeReference]
    internal EnemyStats myStats;
<<<<<<< Updated upstream
    internal Animator anim;
=======
    [SerializeField]
    internal Animator[] animationModels = new Animator[2];
    private Animator activeAnimation;
    [SerializeField]
    internal BoxCollider2D hurtbox;
    
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        dir = Vector2.left;
        moveComp = new EnemyMovement(this);
        obsComp = new EnemyObserve(this);
<<<<<<< Updated upstream
=======
        atkComp = new EnemyAttack(this);

        animationModels[(int)EnemyAnimators.Default].speed = myStats.moveSpd;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dir);
        moveComp.Move();
        obsComp.CheckTarget();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
