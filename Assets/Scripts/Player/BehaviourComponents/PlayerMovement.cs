using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerComponent
{
    float maxSpeed, acceleration, deceleration, currSpeed;
    Vector2 myDirection;
    Vector2 velocity => rigidbody.velocity;
    public PlayerMovement(Player player) : base(player)
    {
        myDirection = Vector2.zero;
        ComponentAction = ChangeVelocity;
    }

    public PlayerMovement(float maxSpeed, float acceleration, float deceleration, Player player) : this(player){
        
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.deceleration = deceleration;
    }
    public override void AcceptInput(InputAction.CallbackContext value){
            myDirection = value.ReadValue<Vector2>();
            Debug.Log($"phase: {value.phase} direction: {myDirection}");
    }
    void ChangeVelocity() {
        Vector2 force = Vector2.zero;
        //compare movement direction
        // if velocity matches direction
        if ( Math.Sign(myDirection.x) == Math.Sign(velocity.x)){
            //accelerate
            if (Mathf.Abs(velocity.x) < maxSpeed)
                force += (myDirection * (acceleration + velocity.x / maxSpeed));
        }
        else{
            //decelerate
            force += (Vector2.left * (deceleration - velocity.x / maxSpeed) * Mathf.Sign(velocity.x));
        }
        rigidbody.AddForce(force);
    }


}
