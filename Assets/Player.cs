using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    #region Fields
    Rigidbody2D rb;
    Dictionary<Type, PlayerComponent> compDict = new Dictionary<Type, PlayerComponent>();
    internal InputActionMap inputs;
    PhysicsMaterial2D pm;

    #region ItayFields
    [SerializeReference]
    public PlayerStats myStats;
    protected internal Vector2 direction => ((PlayerMovement)compDict[typeof(PlayerMovement)]).myDirection;
    protected internal Vector2 lastDirectionalInput;


    [SerializeField]
    float  boostForce;
    internal float currSpeed = 0;
    int currJump = 0;
    #endregion
    #region UrsulaFields
    public PelletController PelletPrefab;
    public bool Aiming;
    public bool IsTouchingFloor, IsTouchingWall;
    public Vector2 WallVector;
    #endregion

    #region AvrahamFields
    public bool isGrounded = false;
    #endregion

    #endregion

    #region MonoBehaviour Functions
    private void Awake(){
            compDict.Add(typeof(PlayerMovement), new PlayerMovement(this));
            compDict.Add(typeof(PlayerJump), new PlayerJump(this));
            compDict.Add(typeof(PlayerDash), new PlayerDash(this));
    }
    private void Start() {
            rb = GetComponent<Rigidbody2D>();
            inputs = GetComponent<PlayerInput>().currentActionMap;
            compDict.Add(typeof(PlayerBoost), new PlayerBoost(this));
            rb.gravityScale = myStats.playerGravity;

            pm = GetComponent<BoxCollider2D>().sharedMaterial;
        }

        private void FixedUpdate() {
            // ComponentAction?.Invoke() will only invoke if not null;
            foreach(PlayerComponent component in compDict.Values){
                component.ComponentAction?.Invoke();
            }
        }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.tag == "Wall")
        {
            IsTouchingWall = true;
            pm.friction = 0.85f;

            WallVector = new Vector2(transform.position.x - other.transform.position.x, 0).normalized;

            //rb.velocity = Vector3.zero;
            //rb.gravityScale = 2f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            WallVector = Vector2.zero;
            IsTouchingWall = false;
            pm.friction = 0.3f;
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
        Debug.Log($"inputs disabled");
        yield return new WaitForSeconds(time);
        inputs.Enable();
        Debug.Log($"inputs enabled");
        yield return null;
    }

	#region  ItayActions
    public void OnMove(InputAction.CallbackContext value) => compDict[typeof(PlayerMovement)].AcceptInput(value);
    public void OnJump(InputAction.CallbackContext value) => compDict[typeof(PlayerJump)].AcceptInput(value);
    public void OnDash(InputAction.CallbackContext value) => compDict[typeof(PlayerDash)].AcceptInput(value);
    public void OnBoost(InputAction.CallbackContext value) => compDict[typeof(PlayerBoost)].AcceptInput(value);
    #endregion
    #region UrsulaFields
    public void OnShoot(InputAction.CallbackContext value){Debug.Log("Shoot"); //Left Mouse Button
        
        if (value.performed){
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 dir = MousePos - (Vector2)transform.position;

        PelletController pellet = Instantiate(PelletPrefab);
        pellet.transform.position =  transform.position;

        pellet.movement = dir.normalized;
        }
    }
    #endregion

    #endregion
}
