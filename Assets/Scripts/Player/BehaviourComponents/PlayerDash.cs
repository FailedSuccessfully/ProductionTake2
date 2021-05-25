using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : PlayerComponent
{
    private delegate void enabler(InputAction action, float time);
    float dashForce => player.myStats.dashForce;
    float dashCooldown => player.myStats.dashCooldown;
    float dashDuration => player.myStats.dashDuration;
    float dashTime = 0;
    Vector2 velocity => rigidbody.velocity;
    

    public PlayerDash(Player player) : base(player)
    {
    }
    public override void AcceptInput(InputAction.CallbackContext value) {
        float now = Time.fixedTime;
        // check to see if dash is out of cooldown
        if (value.performed && now - dashTime >= dashCooldown) {
            dashTime = now;
            ApplyDash();
            Debug.Log("i'm called");
        }

    }

    void ApplyDash() {
        Vector2 force = player.direction * dashForce;
        //TODO: add force
        //TODO: freeze y axis
        player.StartCoroutine(player.BlockAllInputsForSeconds(dashDuration));
    }


}
