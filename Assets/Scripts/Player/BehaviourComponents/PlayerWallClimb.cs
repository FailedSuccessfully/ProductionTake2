using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallClimb : PlayerComponent
{
    float dir;
    internal float wallDir = 0f;
    float wallGrind => player.myStats.wallGrind;
    public PlayerWallClimb(Player player) : base(player)
    {
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {
        dir = value.ReadValue<float>();
            //Debug.Log($"dir: {dir}, walldir: {wallDir}");
        if (wallDir * dir < 0){
            ComponentAction = SlowFall;
            //Debug.Log("A");
        }
        else{
            ComponentAction = null;
            //Debug.Log("B");
        }
    }

    void SlowFall(){
        Vector2 f = Vector2.up * wallGrind * rigidbody.mass * rigidbody.gravityScale;
        //Debug.Log(f);
        rigidbody.AddForce(f, ForceMode2D.Force);
    }
}
