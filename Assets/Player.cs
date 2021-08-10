using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayAnimStates
{
    Idle,
    Walk,
    Run
}

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    #region Fields
    Rigidbody2D rb;
    internal Animator anim;
    internal Dictionary<Type, PlayerComponent> compDict = new Dictionary<Type, PlayerComponent>();
    internal InputActionMap inputs;
    PhysicsMaterial2D pm;

    #region ItayFields
    [SerializeReference]
    public PlayerStats myStats;
    public float boostMeter => ((PlayerBoost)compDict[typeof(PlayerBoost)]).curBoostMeter;
    protected internal Vector2 direction => ((PlayerMovement)compDict[typeof(PlayerMovement)]).myDirection;
    protected internal Vector2 lastDirectionalInput;
    public Transform shotRlease;
    #endregion
    
    #region UrsulaFields
    public PelletController PelletPrefab;
    #endregion

    #endregion

    #region MonoBehaviour Functions
    private void InputInit(){

            inputs = GetComponent<PlayerInput>().currentActionMap;
            compDict.Add(typeof(PlayerMovement), new PlayerMovement(this));
            compDict.Add(typeof(PlayerJump), new PlayerJump(this));
            compDict.Add(typeof(PlayerDash), new PlayerDash(this));
            compDict.Add(typeof(PlayerSlam), new PlayerSlam(this));
            compDict.Add(typeof(PlayerBoost), new PlayerBoost(this));
            compDict.Add(typeof(PlayerShoot), new PlayerShoot(this));
            compDict.Add(typeof(PlayerWallClimb), new PlayerWallClimb(this));

            inputs.FindAction("WallControl")?.Disable();
    }
    private void Start() {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            rb.gravityScale = myStats.playerGravity;
            InputInit();
        }

        private void FixedUpdate() {
            // ComponentAction?.Invoke() will only invoke if not null;
            foreach(PlayerComponent component in compDict.Values){
                component.ComponentAction?.Invoke();
            }
        }
        private void Update() {
            AnimationSelect();
        }

    private void AnimationSelect()
    {
        int select;
        float velX = Mathf.Abs(rb.velocity.x);
        
        switch(velX){

            case float x when x > myStats.runThreshold:
            {
                select = 2;
                break;
            }
            case float x when x > 0.1f:
            {
                select  = 1;
                anim.speed = Mathf.Clamp(velX / myStats.runThreshold, 0f, 1f);
                break;
            }
            default:
            {
                anim.speed = 1;
                select = 0;
                break;
            }
        }
        //Debug.Log($"select is {select}");
        anim.SetInteger("AnimState", select);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall")){
            inputs.FindAction("WallControl")?.Enable();
            (compDict[typeof(PlayerWallClimb)] as PlayerWallClimb).wallDir = col.relativeVelocity.x;

            compDict[typeof(PlayerJump)].ResetJumps();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall")){
            inputs.FindAction("WallControl")?.Disable();
            (compDict[typeof(PlayerWallClimb)] as PlayerWallClimb).wallDir = 0f;
        }
    }

    #endregion


	#region Unity Callbacks

	#endregion
	#region  Actions
    internal IEnumerator BlockInputForSeconds(String actionName, float time = 0.5f){
        InputAction action = inputs.FindAction(actionName);
        action?.Disable();
        Debug.Log($"action {action.name} is disabled");
        yield return new WaitForSeconds(time);
        action?.Enable();
        Debug.Log($"action {action.name} is enabled");
        yield return null;
    }
    internal IEnumerator BlockAllInputsForSeconds(float time = 0.5f) {
        inputs.Disable();
        yield return new WaitForSeconds(time);
        inputs.Enable();
        yield return null;
    }

    public void OnMove(InputAction.CallbackContext value) => compDict[typeof(PlayerMovement)].AcceptInput(value);
    public void OnJump(InputAction.CallbackContext value) => compDict[typeof(PlayerJump)].AcceptInput(value);
    public void OnDash(InputAction.CallbackContext value) => compDict[typeof(PlayerDash)].AcceptInput(value);
    public void OnSlam(InputAction.CallbackContext value) => compDict[typeof(PlayerSlam)].AcceptInput(value);
    public void OnBoost(InputAction.CallbackContext value) => compDict[typeof(PlayerBoost)].AcceptInput(value);
    public void OnShoot(InputAction.CallbackContext value) => compDict[typeof(PlayerShoot)].AcceptInput(value);
    public void OnWallControl(InputAction.CallbackContext value) => compDict[typeof(PlayerWallClimb)].AcceptInput(value);

    #endregion
}
