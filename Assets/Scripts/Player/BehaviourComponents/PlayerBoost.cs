using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerBoost : PlayerComponent
{
    IEnumerable<InputAction> inputsToDisable;
    float boostForce => player.myStats.boostForce;
    float boostMeter;
    Vector2 dir;
    public PlayerBoost(Player player) : base(player)
    {
        inputsToDisable = player.inputs.actions.Where(a => a.name.ToLower() != "boost" );
        boostMeter = 100f;
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
                foreach (InputAction action in inputsToDisable){
                    action.Enable();
                }
                Time.timeScale *= 10f;
                Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                dir = MousePos - (Vector2)transform.position;
                ComponentAction += ApplyForce;
                ComponentAction -= DrainBoost;

            break;
        }
    }

    void ApplyForce(){
        rigidbody.AddForce(dir.normalized * Mathf.Pow(boostForce, 2f) * rigidbody.mass, ForceMode2D.Impulse);
        ComponentAction-= ApplyForce;
    }

    void DrainBoost(){
        boostMeter = Mathf.Floor(boostMeter) > 0 ? boostMeter - Time.fixedDeltaTime * 100f : 0;
        Debug.Log($"meter at {boostMeter}");
    }
}
