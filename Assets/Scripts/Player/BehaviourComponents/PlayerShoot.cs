using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : PlayerComponent
{
    PelletController shot => player.PelletPrefab;
    Vector2 dir;
    public PlayerShoot(Player player) : base(player)
    {
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {
        if (value.performed){
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            dir = MousePos - (Vector2)transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, Mathf.Infinity, LayerMask.GetMask("Enemy"));
            if (hit){
                // check to see if on screen
                if (hit.collider.GetComponent<SpriteRenderer>().isVisible){
                    //apply hit
                    GameManager.HandleEnemyDeath(hit.collider.GetComponent<Enemy>());
                }
            }
            ComponentAction += ApplyShot;
        }
    }

    void ApplyShot(){
        PelletController pellet = GameObject.Instantiate(shot);
        pellet.transform.position =  transform.position;
        pellet.movement = dir.normalized;
        ComponentAction -= ApplyShot;
    }

}
