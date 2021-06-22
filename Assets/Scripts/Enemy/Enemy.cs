using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    internal EnemyMovement moveComp;
    internal Vector2 dir;
    internal float attackRange = 25f;
    internal float targetRange = 40f;
    internal bool HasTarget = false;
    internal int boostOnKill = 20;
    EnemyObserve obsComp;
    [SerializeReference]
    internal EnemyStats myStats;
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector2.right;
        moveComp = new EnemyMovement(this);
        obsComp = new EnemyObserve(this);
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
