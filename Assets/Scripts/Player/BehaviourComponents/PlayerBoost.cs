using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerBoost : PlayerComponent
{
    IEnumerable<InputAction> inputsToDisable;
    float boostForce => player.myStats.boostForce;
    internal float maxBoostMeter = 100f;
    internal float curBoostMeter;
    Vector2 dir;
    public PlayerBoost(Player player) : base(player)
    {
        inputsToDisable = player.inputs.actions.Where(a => a.name.ToLower() != "boost" );
        curBoostMeter = maxBoostMeter;
    }

    public override void AcceptInput(InputAction.CallbackContext value)
    {
        switch (value.phase){
            case (InputActionPhase.Started):
                foreach (InputAction action in inputsToDisable){
                    action.Disable();
                }
                Time.timeScale *= 0.1f;
                ComponentAction += DrainBoost;
            break;
            case (InputActionPhase.Canceled):
                BoostCancel();
                if (curBoostMeter > 0){
                    Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    dir = MousePos - (Vector2)transform.position;
                    ComponentAction += ApplyForce;
                }
            break;
        }
    }

    void ApplyForce(){
        rigidbody.AddForce(dir.normalized * Mathf.Pow(boostForce, 2f) * rigidbody.mass, ForceMode2D.Impulse);
        ComponentAction-= ApplyForce;
    }

    void DrainBoost(){
        curBoostMeter = Mathf.Floor(curBoostMeter) > 0 ? curBoostMeter - Time.fixedDeltaTime * 200f : 0;
        if (curBoostMeter == 0){
            BoostCancel();
        }
    }

    void BoostCancel(){
                foreach (InputAction action in inputsToDisable){
                    action.Enable();
                }
                Time.timeScale /= Time.timeScale;
                ComponentAction -= DrainBoost;
    }

    public void AddBoost(int boost){
        if (curBoostMeter < maxBoostMeter){
            curBoostMeter = Mathf.Clamp((curBoostMeter + boost), 0f, maxBoostMeter);
        }
    }
}

