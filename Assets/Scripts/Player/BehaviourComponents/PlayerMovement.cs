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
        ComponentAction = Move;
    }

    public PlayerMovement(float maxSpeed, float acceleration, float deceleration, Player player) : this(player){
        
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.deceleration = deceleration;
    }


    public void Move(){
        if (myDirection != Vector2.zero){
            Vector2 newPos = (Vector2)rigidbody.position + (myDirection * currSpeed * Time.deltaTime);
            rigidbody.MovePosition(newPos);
        }
        Debug.Log(velocity);
        Debug.Log(rigidbody.velocity);

        myDirection = player.currSpeed < 0 ? Vector2.zero : myDirection;  
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
            Debug.Log($"phase: {value.phase} input: {direction}");
        if (value.canceled){
            ComponentAction -= Acceleration; 
            ComponentAction += Deceleration;           
        }
        else {
            ComponentAction -= Deceleration;
            ComponentAction += Acceleration;
            Debug.Log(currSpeed);
            //currSpeed *= (Mathf.Sign(direction.x * myDirection.x));
            Debug.Log(currSpeed);
            myDirection = direction;
        }



        // if direction changed
        /*int a = Math.Sign(((myDirection * direction)).x);
        if (a <= 0) {
            myDirection = direction;
            ComponentAction += Deceleration;
            ComponentAction -= Acceleration;
            currSpeed *= a;
        }*/
    }

    private void Acceleration(){
        float velocityX = velocity.x;
        if (velocityX < maxSpeed){
            velocityX += acceleration * Time.deltaTime;
        }
        else{
            velocityX -= acceleration * Time.deltaTime;
        }

        velocity.Set(velocityX, velocity.y);    
    }
    private void Deceleration(){
        currSpeed -= deceleration * Time.deltaTime;
}
}
