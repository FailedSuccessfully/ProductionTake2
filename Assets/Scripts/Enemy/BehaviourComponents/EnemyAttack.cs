using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyBehaviour
{
    BoxCollider2D hurtbox;
    SpriteRenderer hbRender;
    Color hurtCol;
    Action atkAction;
    Coroutine crAttack;
<<<<<<< Updated upstream
    float attackSpeed = 0.3f;
    internal float attackWind = 2.5f;
    internal float attackActiveTime = 0.5f;
=======
    float attackDelay = 1.5f;
    float attackActiveTime = 0.5f;
>>>>>>> Stashed changes
    public EnemyAttack(Enemy enemy) : base(enemy)
    {
        hurtbox = enemy.hurtbox;
        hbRender = hurtbox.GetComponent<SpriteRenderer>();
        hurtCol = hbRender.color;
        hurtbox.gameObject.SetActive(false);
        hurtbox.isTrigger = true;
    }

    public void Attack(){
        if (enemy.CanAttack){
            enemy.AnimationSwitch(EnemyAnimators.Attack);
<<<<<<< Updated upstream
            enemy.animationModels[(int)EnemyAnimators.Attack].SetFloat("attackWind",  attackWind );
            enemy.animationModels[(int)EnemyAnimators.Attack].SetFloat("attackActiveTime", attackActiveTime);
=======
>>>>>>> Stashed changes
            hurtbox.gameObject.SetActive(true);
            crAttack = crAttack == null ? enemy.StartCoroutine(this.AttackCycle()) : crAttack;
        }
        else {
            enemy.AnimationSwitch(EnemyAnimators.Default);
<<<<<<< Updated upstream
            if (crAttack != null){
                enemy.StopCoroutine(crAttack);
                crAttack = null;
            }
=======
            crAttack = null;
>>>>>>> Stashed changes
            hurtbox.gameObject.SetActive(false);
        }
        atkAction?.Invoke();
    }

    void DoAttack(){
        if(hurtbox.IsTouchingLayers(LayerMask.GetMask("Player"))){
            //GameManager.RespawnPlayer();
        }
    }

    public IEnumerator AttackCycle(){
<<<<<<< Updated upstream
        yield return new WaitForSeconds(0.5f);
        enemy.animationModels[(int)EnemyAnimators.Attack].SetBool("damageActive", true);
        while(true){
            hbRender.color = Color.red;
            atkAction = DoAttack;
            yield return new WaitForSeconds(attackActiveTime);
            enemy.animationModels[(int)EnemyAnimators.Attack].SetBool("damageActive", false);
            atkAction = null;
            hbRender.color = hurtCol;
            yield return new WaitForSeconds(attackWind);
            enemy.animationModels[(int)EnemyAnimators.Attack].SetBool("damageActive", true);
=======
        while(true){
            atkAction = null;
            hbRender.color = hurtCol;
            yield return new WaitForSeconds(attackDelay);
            atkAction = DoAttack;
            hbRender.color = Color.red;
            yield return new WaitForSeconds(attackActiveTime);
>>>>>>> Stashed changes
        }
    }    
}
