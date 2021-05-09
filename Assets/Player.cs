using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Fields
    Rigidbody2D rb;
    PlayerMovement a;
    PlayerJump b;


    #region ItayFields
    [SerializeReference]
    public PlayerStats myStats;


    [SerializeField]
    float maxSpeed, acceleration, deceleration, jumpForce, dashForce, boostForce, dashCooldown;
    int setpAngle;
    [SerializeField]
    int jumpNum;
    bool isMoving = false;
    internal float currSpeed = 0, dashTime = 0;
    int currJump = 0;
    protected Vector2 moveDirection = Vector2.zero;
    #endregion
    #region UrsulaFields
    public PelletController PelletPrefab;
    public bool Aiming;
    #endregion

    #region AvrahamFields
    public bool isGrounded = false;
    #endregion

    #endregion

    #region MonoBehaviour Functions
    private void Start() {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = myStats.playerGravity;
            
            a = new PlayerMovement(this);
            b = new PlayerJump(this);
            //Debug.Log(a.ComponentAction);

            var d = GetComponent<PlayerInput>();
            Debug.Log(d.actions.actionMaps[0].actions[0].name);
        }

        private void FixedUpdate() {
            a.ComponentAction.Invoke();
            b.ComponentAction?.Invoke();
        }
		
            



         void OnCollisionEnter2D(Collision2D other) {
            currJump = 0;
        if (other.gameObject.tag == "floor")
        {
            isGrounded = true;
            Debug.Log("isGrounded = True");

        }

    }
    
	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "floor")
        {
            isGrounded = false;
            Debug.Log("isGrounded = False");
        }
    }

	#endregion

	#region Unity Callbacks

	#endregion
	#region  Actions

	#region  ItayActions

    public void OnMove(InputAction.CallbackContext value) => a.AcceptInput(value);
    public void OnJump(InputAction.CallbackContext value){Debug.Log("Jump"); //Space
            b.AcceptInput(value);
        /*if (value.performed){
        //TODO: Apply ascension
        if (currJump < jumpNum){
                
            rb.AddForce(Vector2.up * (jumpForce * jumpForce), ForceMode2D.Impulse);
            currJump++;
        }
        }
        Debug.Log(currJump);*/
    }
    public void OnDash(InputAction.CallbackContext value){Debug.Log("Dash"); //Left Shift
        //TODO: Figure direction of Dash (right and left only for now)
        float now = Time.time;
        if (now - dashTime >= dashCooldown){
            currSpeed += dashForce;
            if (currSpeed < 0){
                currSpeed *= -0.5f;
            }
            dashTime = now;
        }
    }
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

    public void OnBoost(InputAction.CallbackContext value)
    {
        Debug.Log("Boost");
        if (value.performed){
        Time.timeScale = 1f;
        Aiming = false;
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = MousePos - (Vector2)transform.position;

        rb.AddForce(dir.normalized * boostForce * boostForce , ForceMode2D.Impulse);
        }
        currSpeed *= rb.velocity.normalized.x;

    }

    public void OnBoostAim(InputAction.CallbackContext value)
    {
        if (value.performed){
        Debug.Log("Aiming");
        Aiming = true;
        Time.timeScale = 0.1f;
        }
    }
    #endregion
}
