using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerComponent
{
    int jumpNum => player.myStats.jumpNum;
    float jumpForce => player.myStats.jumpForce;
    int currJump;
    bool isAirborne;
    public PlayerJump(Player player) : base(player)
    {
        currJump = 0;
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {   
        if (currJump < jumpNum){
            this.ComponentAction += Jump;
            currJump++;
        }
    }

    void Jump() {
        rigidbody.AddForce(Vector2.up * Mathf.Pow(jumpForce, 2) * rigidbody.mass, ForceMode2D.Impulse);
        ComponentAction -= Jump;
    }

    void ResetJumps() => currJump = 0;

    // on press mark for jump
    // in fixed update check if jumped then apply

}
