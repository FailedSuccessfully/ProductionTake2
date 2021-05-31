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
