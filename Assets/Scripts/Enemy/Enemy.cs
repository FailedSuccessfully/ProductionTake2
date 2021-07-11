using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyAnimators{
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    internal Animator anim;
=======
=======
>>>>>>> Stashed changes
    [SerializeField]
    internal Animator[] animationModels = new Animator[2];
    private Animator activeAnimation;
    [SerializeField]
    internal BoxCollider2D hurtbox;
<<<<<<< Updated upstream
    
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        if (hurtbox == null)
            hurtbox = GetComponentInChildren<BoxCollider2D>();
        AnimationSwitch(EnemyAnimators.Default);
        dir = Vector2.left;

        
        moveComp = new EnemyMovement(this);
        obsComp = new EnemyObserve(this);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        atkComp = new EnemyAttack(this);

        animationModels[(int)EnemyAnimators.Default].speed = myStats.moveSpd;
>>>>>>> Stashed changes
=======
        atkComp = new EnemyAttack(this);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dir);
        moveComp?.Move();
        obsComp?.Observe();
        atkComp?.Attack();
    }

    internal void AnimationSwitch(EnemyAnimators anim){
        Animator nextAnim = animationModels[((int)anim)];
        if (activeAnimation != null && activeAnimation != nextAnim){
            activeAnimation.gameObject.SetActive(false);
        }
        activeAnimation = animationModels[((int)anim)];
        activeAnimation.gameObject.SetActive(true);
        
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
