using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBehaviour
{
    float patrolRnage = 15f;
    float[] bounds;
    internal float heightOffset=0.1f;
    public EnemyMovement(Enemy enemy) : base(enemy)
    {
        bounds = new float[2];
        bounds[0] = transform.position.x - patrolRnage;
        bounds[1] = transform.position.x + patrolRnage;
    }

    public void Move(){
        //MaintainHeight();
        if (!enemy.HasTarget)
            Patrol();
        else
            Chase();
    }

    void Patrol(){
        Vector2 newPos = transform.position;
        newPos.x += enemy.dir.x * enemy.myStats.moveSpd * Time.deltaTime * 10;
        if (newPos.x - bounds[0] < 0 || newPos.x - bounds[1] > 0)
            enemy.dir *= -1;
        else
            transform.position = newPos;
    }
    void Chase(){
        Vector2 playerPos = GameManager.GetPlayerPosition();
        if (Mathf.Abs(Vector2.Distance(this.transform.position, playerPos)) > enemy.attackRange){
            Vector2 newPos = transform.position;
            newPos.x += Mathf.Sign((playerPos.x - this.transform.position.x)) * enemy.myStats.moveSpd * Time.deltaTime * 10;
            transform.position = newPos;
        }   
    }

    private void MaintainHeight(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, float.PositiveInfinity ,LayerMask.GetMask("Ground"));
        if (hit.collider != null){
            if (hit.distance > 5){
                transform.position = new Vector2(transform.position.x, transform.position.y - heightOffset);
            }
            else{
                transform.position = new Vector2(transform.position.x, transform.position.y + heightOffset);
            }
        }
        else {
                transform.position = new Vector2(transform.position.x, transform.position.y + heightOffset);
        }

    }
}
