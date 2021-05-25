using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : PlayerComponent
{
    float dashForce => player.myStats.dashForce;
    float dashCooldown => player.myStats.dashCooldown;
    float dashDuration => player.myStats.dashDuration;
    float dashTime = 0;
    

    public PlayerDash(Player player) : base(player)
    {
    }
    public override void AcceptInput(InputAction.CallbackContext value) {
        float now = Time.fixedTime;
        // check to see if dash is out of cooldown
        if (value.performed && now - dashTime >= dashCooldown) {
            dashTime = now;
            ApplyDash();
        }

    }

    void ApplyDash() {
        // stop player movement and gravity
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
        ComponentAction += Dash;
        player.StartCoroutine(player.BlockAllInputsForSeconds(dashDuration));
    }

    void Dash(){
        // Do dash
        Vector2 pos = new Vector2(rigidbody.position.x + (player.lastDirectionalInput.x * dashForce * Time.fixedDeltaTime),
                                    rigidbody.position.y);
        rigidbody.MovePosition(pos);

        // if dash duration passed reset player gravity and component action
        if (Time.fixedTime - dashTime >= dashDuration){
        rigidbody.gravityScale = player.myStats.playerGravity;
            ComponentAction -= Dash;
        }
    }


}
