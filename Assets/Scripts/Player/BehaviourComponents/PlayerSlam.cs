using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlam : PlayerComponent
{
    float downForce => player.myStats.downForce;
    public PlayerSlam(Player player) : base(player)
    {
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {
        Debug.Log("hi");
        if (value.canceled)
            ComponentAction = null;
        else
            ComponentAction = Slam;
    }

    private void Slam(){
        Vector2 f = Vector2.down * downForce * rigidbody.mass;
        Debug.Log(f);
        rigidbody.AddForce(f, ForceMode2D.Impulse);
    }
}
