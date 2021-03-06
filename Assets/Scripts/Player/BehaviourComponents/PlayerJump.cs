using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : PlayerComponent
{
    int jumpNum => player.myStats.extraJumps;
    float jumpForce => player.myStats.jumpForce;
    int currJump;
    bool isGrounded;

    bool IsJumping;
    public PlayerJump(Player player) : base(player)
    {
        this.ComponentAction += CheckGrounded;
        currJump = 0;
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {   
        if (value.performed && (isGrounded || currJump < jumpNum)){
            // adds a jump call to be invoked on fixed update
            this.ComponentAction += Jump;
            currJump++;
        }
    }

    void Jump() {
        
        // stop vertical momentum
        rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints2D.FreezePositionY;
        rigidbody.constraints = rigidbody.constraints ^ RigidbodyConstraints2D.FreezePositionY;

        rigidbody.AddForce(Vector2.up * Mathf.Pow(jumpForce, 2) * rigidbody.mass, ForceMode2D.Impulse);

        // jump hotfix
        if (isGrounded)
            currJump++;
        // after being called will remove self from component action
        ComponentAction -= Jump;
    }

    public override void ResetJumps() => currJump = 0;

    void CheckGrounded() {
        // Check against ground mask
        if (rigidbody.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            isGrounded = true;
            ResetJumps();
        }
        else
            isGrounded = false;
    }

}
