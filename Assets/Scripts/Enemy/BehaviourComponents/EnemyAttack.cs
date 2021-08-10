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
    float attackSpeed = 0.3f;
    internal float attackWind = 2.5f;
    internal float attackActiveTime = 0.5f;
    public EnemyAttack(Enemy enemy) : base(enemy)
    {
        myAnim = enemy.animationModels[(int)EnemyAnimators.Attack];
        hurtbox = enemy.hurtbox;
        hbRender = hurtbox.GetComponent<SpriteRenderer>();
        hurtCol = hbRender.color;
        hurtbox.gameObject.SetActive(false);
        hurtbox.isTrigger = true;
    }

    public void Attack(){
        if (enemy.CanAttack){
            enemy.AnimationSwitch(EnemyAnimators.Attack);
            enemy.animationModels[(int)EnemyAnimators.Attack].SetFloat("attackWind",  attackWind );
            enemy.animationModels[(int)EnemyAnimators.Attack].SetFloat("attackActiveTime", attackActiveTime);
            hurtbox.gameObject.SetActive(true);
            crAttack = crAttack == null ? enemy.StartCoroutine(this.AttackCycle()) : crAttack;
        }
        else {
            enemy.AnimationSwitch(EnemyAnimators.Default);
            if (crAttack != null){
                enemy.StopCoroutine(crAttack);
                crAttack = null;
            }
            hurtbox.gameObject.SetActive(false);
        }
        atkAction?.Invoke();
    }

    void DoAttack(){
        if(hurtbox.IsTouchingLayers(LayerMask.GetMask("Player"))){
            GameManager.RespawnPlayer();
        }
    }

    public IEnumerator AttackCycle(){
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
        }
    }    
}
