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
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
        ComponentAction += Dash;
        player.StartCoroutine(player.BlockAllInputsForSeconds(dashDuration));
    }

    void Dash(){
        Debug.Log($"dashing {Time.fixedTime - dashTime}");
        float dir = player.direction.x == 0f ? player.lastDirectionalInput.x : player.direction.x;
        Vector2 pos = new Vector2(rigidbody.position.x + dir * dashForce * Time.fixedDeltaTime, rigidbody.position.y);
        rigidbody.MovePosition(pos);
        if (Time.fixedTime - dashTime >= dashDuration){
        rigidbody.gravityScale = player.myStats.playerGravity;
            ComponentAction -= Dash;
        }
    }


}
