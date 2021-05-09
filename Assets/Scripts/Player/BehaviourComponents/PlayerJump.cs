using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerComponent
{
    int jumpNum => player.myStats.jumpNum;
    float jumpForce => player.myStats.jumpForce;
    int currJump;
    public PlayerJump(Player player) : base(player)
    {
        this.ComponentAction = Jump;
        currJump = 0;
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {   
        if (currJump < jumpNum)
            ComponentAction.Invoke();
    }

    void Jump() {
            rigidbody.AddForce(Vector2.up * (jumpForce * jumpForce), ForceMode2D.Impulse);
            currJump++;
    }

    
}
