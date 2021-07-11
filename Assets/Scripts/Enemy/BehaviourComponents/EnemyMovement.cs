using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBehaviour
{
    
    float patrolRnage = 40f;    float patrolWait = 5f;
    float waitTimestamp;
    float[] bounds;
    Action patrolAction;
    internal float heightOffset=0.1f;
    public EnemyMovement(Enemy enemy) : base(enemy)
    {
        myAnim = enemy.animationModels[(int)EnemyAnimators.Default];
        bounds = new float[2];
        SetBounds();
        waitTimestamp = Time.time;
        myAnim.SetBool(isMoveHash, false);
        patrolAction = Wait;
    }

    public void Move(){
        //Debug.Log($"time: {Time.time} timestamp: {waitTimestamp}");
        if (!enemy.HasTarget)
            patrolAction.Invoke();
        else
            Chase();
    }

    private void Wait()
    {
        if (Time.time - waitTimestamp > patrolWait){
            transform.Rotate(Vector3.up, 180);
            enemy.dir *= -1;
            myAnim.SetBool(isMoveHash, true);
            patrolAction = Patrol;
        }
    }

    void Patrol(){
        Vector2 newPos = transform.localPosition;
        newPos.x += enemy.dir.x * enemy.myStats.moveSpd * Time.deltaTime * 10;
        if (newPos.x - bounds[0] < 0 || newPos.x - bounds[1] > 0){
            //Debug.Log($"newpos x: {newPos.x} bounds: {bounds[0]}, {bounds[1]} ");
            HandleEdge();
        }
        else
            transform.position = newPos;
    }
    void Chase(){
        Vector2 playerPos = GameManager.GetPlayerPosition();
        float dist = Vector2.Distance(this.transform.position, playerPos);
        if (Mathf.Abs(dist) > enemy.attackRange){
            transform.rotation.SetLookRotation(Vector3.right * Mathf.Sign(dist));
            Vector2 newPos = transform.position;
            newPos.x += Mathf.Sign((playerPos.x - this.transform.position.x)) * enemy.myStats.moveSpd * 2 * Time.deltaTime * 10;
            transform.position = newPos;
        }
        else {
            SetBounds();
        }  
    }

    void HandleEdge(){
        //Debug.Log("edging");
        waitTimestamp = Time.time;
        patrolAction = Wait;
        myAnim.SetBool(isMoveHash, false);
    }
    void SetBounds(){
        bounds[0] = transform.localPosition.x - patrolRnage;
        bounds[1] = transform.localPosition.x + patrolRnage;
    }

}
